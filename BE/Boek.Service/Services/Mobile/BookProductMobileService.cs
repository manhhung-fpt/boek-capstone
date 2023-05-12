using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boek.Core.Constants;
using Boek.Core.Constants.Mobile;
using Boek.Infrastructure.Requests.BookProducts.Mobile;
using Boek.Infrastructure.Requests.Books.Mobile;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.BookProducts.Mobile;
using Boek.Infrastructure.ViewModels.Genres;
using Boek.Infrastructure.ViewModels.Users;
using Boek.Repository.Interfaces;
using Boek.Service.Commons;
using Boek.Service.Interfaces.Mobile;
using Boek.Service.Utils;
using Boek.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Boek.Infrastructure.ViewModels.Campaigns.Mobile;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace Boek.Service.Services.Mobile
{
    public class BookProductMobileService : IBookProductMobileService
    {
        #region Fields and constructor
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BookProductMobileService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Gets
        public HierarchicalBookProductsViewModel GetHierarchicalBookProducts(HierarchicalBookProductsRequestModel filter)
        {
            var result = new HierarchicalBookProductsViewModel();
            if (filter.IsNotEmpty())
            {
                switch (filter.Title)
                {
                    case MobileConstants.TITLE_GENRE:
                        result = filter.GenreId.HasValue ?
                        GetGenreBookProduct(filter.CampaignId, filter.GenreId) :
                        GetGenreBookProduct(filter.CampaignId);
                        if (result == null)
                            BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                            {
                                ErrorMessageConstants.NOT_FOUND,
                                ErrorMessageConstants.GENRE_ID,
                                filter.GenreId.ToString()
                            });
                        break;
                    case MobileConstants.TITLE_ISSUER:
                        result = filter.IssuerId.HasValue ?
                        GetIssuerBookProducts(filter.CampaignId, filter.IssuerId) :
                        GetIssuerBookProducts(filter.CampaignId);
                        if (result == null)
                            BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                            {
                                ErrorMessageConstants.NOT_FOUND,
                                ErrorMessageConstants.ISSUER,
                                filter.IssuerId.ToString()
                            });
                        break;
                }
            }
            return result;
        }

        public UnhierarchicalBookProductsViewModel GetUnhierarchicalBookProducts(UnhierarchicalBookProductsRequestModel filter)
        {
            var result = new UnhierarchicalBookProductsViewModel();
            if (filter.IsNotEmpty())
            {
                switch (filter.Title)
                {
                    case MobileConstants.TITLE_DISCOUNT:
                        result = GetDiscountBookProducts(filter.CampaignId);
                        if (result == null)
                            BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                            {
                                ErrorMessageConstants.NOT_FOUND,
                                ErrorMessageConstants.BOOK_PRODUCT_DISCOUNT
                            });
                        break;
                    case MobileConstants.TITLE_COMBO:
                        result = GetComboBookProducts(filter.CampaignId);
                        if (result == null)
                            BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                            {
                                ErrorMessageConstants.NOT_FOUND,
                                ErrorMessageConstants.BOOK_COMBO
                            });
                        break;
                }
            }
            return result;
        }

        public BaseResponsePagingModel<MobileBookProductViewModel> GetMobileBookProducts(BookProductMobileRequestModel filter, PagingModel paging)
        {
            filter = CheckBookGenreFilter(filter);
            var _basicFilter = _mapper.Map<BasicBookProductMobileRequestModel>(filter);
            var list = new List<MobileBookProductViewModel>();
            var count = 0;
            IQueryable<MobileBookProductViewModel> query = null;
            var invalidBookProductStatus = new List<byte?>()
            {
                    (byte)BookProductStatus.Pending,
                    (byte)BookProductStatus.Rejected
            };
            if (IsNotEmptyHierarchicalOrUnhierarchicalRequest(filter))
                query = GetSearchBookFromCampaignDetail(filter, _basicFilter, invalidBookProductStatus);
            else
            {
                query = _unitOfWork.BookProducts.Get(bp => !invalidBookProductStatus.Contains(bp.Status))
                        .ProjectTo<MobileBookProductViewModel>(_mapper.ConfigurationProvider)
                        .DynamicOtherFilter(_basicFilter);
            }
            if (query.Any())
            {
                var temp = FilterBookProductsByType(query, filter);
                if (temp.Any())
                {
                    var result = temp.PagingQueryable(paging.Page, paging.Size, PageConstant.LimitPaging, PageConstant.LimitPaging);

                    if (result.Item1 > 0)
                    {
                        count = result.Item1;
                        int? LevelId = null;
                        var IsCustomer = ServiceUtils.CheckRole(_httpContextAccessor, BoekRole.Customer);
                        if (IsCustomer)
                        {
                            var UserId = ServiceUtils.GetUserInfo(_httpContextAccessor);
                            var customer = _unitOfWork.Customers.Get(UserId);
                            if (customer != null)
                                LevelId = customer.LevelId;
                        }
                        result.Item2.ToList().ForEach(bp =>
                        {
                            if (bp.CampaignId.HasValue)
                            {
                                var campaignLevels = _unitOfWork.CampaignLevels.Get(cls => cls.CampaignId.Equals(bp.CampaignId)).ToList();
                                if (campaignLevels.Any())
                                {
                                    bp.WithLevel = true;
                                    bp.AllowPurchasingByLevel = LevelId.HasValue ? campaignLevels.Select(cls => cls.LevelId).Contains(LevelId) : false;
                                }
                            }
                            list.Add(ServiceUtils.GetResponseDetail(bp));
                        });
                    }
                }
            }
            return new BaseResponsePagingModel<MobileBookProductViewModel>()
            {
                Metadata = new PagingMetadata()
                {
                    Page = paging.Page,
                    Size = paging.Size,
                    Total = count
                },
                Data = list
            };
        }

        public MobileBookProductViewModel GetMobileBookProductById(Guid? id)
        {
            var invalidStatus = new List<byte?>()
            {
                (byte)BookProductStatus.Pending,
                (byte)BookProductStatus.Rejected
            };
            var BookProduct = _unitOfWork.BookProducts.Get(bp => bp.Id.Equals(id) &&
            !invalidStatus.Contains(bp.Status))
            .ProjectTo<MobileBookProductViewModel>(_mapper.ConfigurationProvider)
            .SingleOrDefault();
            if (BookProduct == null)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.BOOK_PRODUCT_ID
                });
            }
            if (!BookProduct.Type.Equals((byte)BookType.Combo))
            {
                var OtherBookProducts = _unitOfWork.BookProducts
                .Get(bp => bp.BookId.Equals(BookProduct.BookId) && !bp.Id.Equals(id) &&
                !invalidStatus.Contains(bp.Status))
                .OrderBy(bp => bp.SalePrice)
                .ProjectTo<OtherMobileBookProductsViewModel>(_mapper.ConfigurationProvider)
                .ToList();
                if (OtherBookProducts.Any())
                    BookProduct.OtherMobileBookProducts = OtherBookProducts;
            }
            BookProduct = ServiceUtils.GetResponseDetail(BookProduct);
            BookProduct.UnhierarchicalBookProducts = new List<UnhierarchicalBookProductsViewModel>();
            //genre
            var temp = GetSameGenreBookProducts(BookProduct.CampaignId, BookProduct.GenreId, id, invalidStatus);
            if (temp != null)
            {
                temp.BookProducts = temp.BookProducts.OrderBy(bp => bp.Status).ToList();
                BookProduct.UnhierarchicalBookProducts.Add(temp);
            }
            //issuer
            temp = GetSameIssuerBookProducts(BookProduct.CampaignId, BookProduct.IssuerId, id, invalidStatus);
            if (temp != null)
            {
                temp.BookProducts = temp.BookProducts.OrderBy(bp => bp.Status).ToList();
                BookProduct.UnhierarchicalBookProducts.Add(temp);
            }
            var campaignLevels = _unitOfWork.CampaignLevels.Get(cls => cls.CampaignId.Equals(BookProduct.CampaignId)).ToList();
            if (campaignLevels.Any())
            {
                BookProduct.WithLevel = true;
                var IsCustomer = ServiceUtils.CheckRole(_httpContextAccessor, BoekRole.Customer);
                if (IsCustomer)
                {
                    var UserId = ServiceUtils.GetUserInfo(_httpContextAccessor);
                    var customer = _unitOfWork.Customers.Get(UserId);
                    if (customer != null)
                        BookProduct.AllowPurchasingByLevel = campaignLevels.Select(cls => cls.LevelId).Contains(customer.LevelId);
                }
            }
            return BookProduct;
        }
        #endregion

        #region Generates
        public List<HierarchicalBookProductsViewModel> GenerateHierarchicalBookProducts(int? CampaignId)
        {
            CheckCampaign(CampaignId);
            var result = new List<HierarchicalBookProductsViewModel>();
            //genre
            var temp = GetGenreBookProduct(CampaignId);
            if (temp != null)
                result.Add(temp);
            //issuer
            temp = GetIssuerBookProducts(CampaignId);
            if (temp != null)
                result.Add(temp);
            return result;
        }

        public List<UnhierarchicalBookProductsViewModel> GenerateUnhierarchicalBookProducts(int? CampaignId)
        {
            CheckCampaign(CampaignId);
            var result = new List<UnhierarchicalBookProductsViewModel>();
            //discount
            var temp = GetDiscountBookProducts(CampaignId);
            if (temp != null)
                result.Add(temp);
            //combo
            temp = GetComboBookProducts(CampaignId);
            if (temp != null)
                result.Add(temp);
            return result;
        }

        public List<HierarchicalBookProductsViewModel> GenerateHierarchicalBookProducts(CampaignMobileViewModel campaign)
        {
            var result = new List<HierarchicalBookProductsViewModel>();
            //genre
            var temp = GetGenreBookProduct(campaign);
            if (temp != null)
                result.Add(temp);
            //issuer
            temp = GetIssuerBookProducts(campaign);
            if (temp != null)
                result.Add(temp);
            return result;
        }

        public List<UnhierarchicalBookProductsViewModel> GenerateUnhierarchicalBookProducts(CampaignMobileViewModel campaign)
        {
            var result = new List<UnhierarchicalBookProductsViewModel>();
            //discount
            var temp = GetDiscountBookProducts(campaign);
            if (temp != null)
                result.Add(temp);
            //combo
            temp = GetComboBookProducts(campaign);
            if (temp != null)
                result.Add(temp);
            return result;
        }
        #endregion

        #region Utils
        private void CheckCampaign(int? CampaignId)
        {
            var _campaign = _unitOfWork.Campaigns.Get(CampaignId);
            if (_campaign == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.CAMPAIGN_ID
                });
        }

        private bool IsNotEmptyHierarchicalOrUnhierarchicalRequest(BookProductMobileRequestModel filter)
        => filter.hierarchicalBook != null || filter.unhierarchicalBook != null;

        private IQueryable<MobileBookProductViewModel> GetSearchBookFromCampaignDetail(BookProductMobileRequestModel filter, BasicBookProductMobileRequestModel basicFilter, List<byte?> invalidBookProductStatus)
        {
            var result = new List<MobileBookProductViewModel>();
            if (filter.hierarchicalBook != null)
            {
                if (filter.hierarchicalBook.IsNotEmpty())
                {
                    var temp = GetHierarchicalBookProductFromCampaignDetail(filter.hierarchicalBook, invalidBookProductStatus);
                    if (temp.Any())
                        result.AddRange(temp);
                }
            }
            else if (filter.unhierarchicalBook != null)
            {
                if (filter.unhierarchicalBook.IsNotEmpty())
                {
                    var temp = GetUnhierarchicalBookProductFromCampaignDetail(filter.unhierarchicalBook, invalidBookProductStatus);
                    if (temp.Any())
                        result.AddRange(temp);
                }
            }
            return result.Any() ?
            result.AsQueryable().DynamicOtherFilter(basicFilter) :
            result.AsQueryable();
        }

        private List<MobileBookProductViewModel> GetHierarchicalBookProductFromCampaignDetail(HierarchicalBookProductsRequestModel filter, List<byte?> invalidBookProductStatus)
        {
            var result = new List<MobileBookProductViewModel>();
            if (filter.IsNotEmpty())
            {
                result = _unitOfWork.BookProducts.Get(bp => bp.CampaignId.Equals(filter.CampaignId) &&
                !invalidBookProductStatus.Contains(bp.Status))
                .ProjectTo<MobileBookProductViewModel>(_mapper.ConfigurationProvider)
                .ToList();
                switch (filter.Title)
                {
                    case MobileConstants.TITLE_GENRE:
                        if (filter.GenreId.HasValue)
                            result = result.Where(bp => filter.GenreId.Equals(bp.GenreId)).ToList();
                        break;
                    case MobileConstants.TITLE_ISSUER:
                        if (filter.IssuerId.HasValue)
                            result = result.Where(bp => filter.IssuerId.Equals(bp.IssuerId)).ToList();
                        break;
                }
            }
            return result;
        }
        private List<MobileBookProductViewModel> GetUnhierarchicalBookProductFromCampaignDetail(UnhierarchicalBookProductsRequestModel filter, List<byte?> invalidBookProductStatus)
        {
            var result = new List<MobileBookProductViewModel>();
            if (filter.IsNotEmpty())
            {
                result = _unitOfWork.BookProducts.Get(bp => bp.CampaignId.Equals(filter.CampaignId) &&
                !invalidBookProductStatus.Contains(bp.Status))
                .ProjectTo<MobileBookProductViewModel>(_mapper.ConfigurationProvider)
                .ToList();
                switch (filter.Title)
                {
                    case MobileConstants.TITLE_DISCOUNT:
                        result = result.Where(bp => bp.Discount.HasValue).ToList();
                        break;
                    case MobileConstants.TITLE_COMBO:
                        result = result.Where(bp => bp.Type.Equals((byte)BookType.Combo)).ToList();
                        break;
                }
            }
            return result;
        }

        private UnhierarchicalBookProductsViewModel GetDiscountBookProducts(int? CampaignId)
        {
            var list = _unitOfWork.BookProducts.Get(bps => bps.Discount.HasValue
            && bps.CampaignId.Equals(CampaignId))
            .ProjectTo<MobileBookProductsViewModel>(_mapper.ConfigurationProvider).ToList();
            if (list.Any())
            {
                var WithLevel = CheckCampaignLevel(CampaignId);
                if (WithLevel != null)
                {
                    list.ForEach(bp =>
                    {
                        bp.WithLevel = WithLevel.First();
                        bp.AllowPurchasingByLevel = WithLevel.Last();
                    });
                }
                var result = new UnhierarchicalBookProductsViewModel()
                {
                    CampaignId = CampaignId,
                    Title = MobileConstants.TITLE_DISCOUNT,
                    BookProducts = list
                };
                return ServiceUtils.GetResponseDetail(result);
            }
            return null;
        }
        private UnhierarchicalBookProductsViewModel GetDiscountBookProducts(CampaignMobileViewModel campaign)
        {
            var list = campaign.BookProducts.Where(bps => bps.Discount.HasValue).ToList();
            if (list.Any())
            {
                var WithLevel = CheckCampaignLevel(campaign);
                if (WithLevel != null)
                {
                    list.ForEach(bp =>
                    {
                        bp.WithLevel = WithLevel.First();
                        bp.AllowPurchasingByLevel = WithLevel.Last();
                    });
                }
                var result = new UnhierarchicalBookProductsViewModel()
                {
                    CampaignId = campaign.Id,
                    Title = MobileConstants.TITLE_DISCOUNT,
                    BookProducts = list
                };
                return ServiceUtils.GetResponseDetail(result);
            }
            return null;
        }

        private UnhierarchicalBookProductsViewModel GetComboBookProducts(int? CampaignId)
        {
            var list = _unitOfWork.BookProducts.Get(bps => bps.Type.Equals((byte)BookType.Combo)
            && bps.CampaignId.Equals(CampaignId))
            .ProjectTo<MobileBookProductsViewModel>(_mapper.ConfigurationProvider).ToList();
            if (list.Any())
            {
                var WithLevel = CheckCampaignLevel(CampaignId);
                if (WithLevel != null)
                {
                    list.ForEach(bp =>
                    {
                        bp.WithLevel = WithLevel.First();
                        bp.AllowPurchasingByLevel = WithLevel.Last();
                    });
                }
                var result = new UnhierarchicalBookProductsViewModel()
                {
                    CampaignId = CampaignId,
                    Title = MobileConstants.TITLE_COMBO,
                    BookProducts = list
                };
                return ServiceUtils.GetResponseDetail(result);
            }
            return null;
        }
        private UnhierarchicalBookProductsViewModel GetComboBookProducts(CampaignMobileViewModel campaign)
        {
            var list = campaign.BookProducts.Where(bps => bps.Type.Equals((byte)BookType.Combo)).ToList();
            if (list.Any())
            {
                var WithLevel = CheckCampaignLevel(campaign);
                if (WithLevel != null)
                {
                    list.ForEach(bp =>
                    {
                        bp.WithLevel = WithLevel.First();
                        bp.AllowPurchasingByLevel = WithLevel.Last();
                    });
                }
                var result = new UnhierarchicalBookProductsViewModel()
                {
                    CampaignId = campaign.Id,
                    Title = MobileConstants.TITLE_COMBO,
                    BookProducts = list
                };
                return ServiceUtils.GetResponseDetail(result);
            }
            return null;
        }
        private UnhierarchicalBookProductsViewModel GetSameGenreBookProducts(int? CampaignId, int? GenreId, Guid? id = null, List<byte?> invalidStatus = null)
        {
            var _genre = _unitOfWork.Genres.Get(g => g.Id.Equals(GenreId))
            .Include(g => g.InverseParent)
            .SingleOrDefault();
            if (_genre == null)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.GENRE_ID
                });
            }
            if (invalidStatus == null)
                invalidStatus = new List<byte?>()
            {
                (byte)BookProductStatus.Pending,
                (byte)BookProductStatus.Rejected
            };
            var list = _unitOfWork.BookProducts.Get(bps => bps.CampaignId.Equals(CampaignId) && !invalidStatus.Contains(bps.Status))
            .ProjectTo<MobileBookProductsViewModel>(_mapper.ConfigurationProvider).ToList();
            if (id != null)
                list = list.Where(l => !l.Id.Equals(id)).ToList();
            if (list.Any())
            {
                var temp = new List<MobileBookProductsViewModel>();
                if (_genre.InverseParent.Any())
                {
                    var genres = _genre.InverseParent.Select(ip => ip.Id).ToList();
                    list.ForEach(mbps =>
                    {
                        if (genres.Contains((int)mbps.GenreId) && !temp.Contains(mbps))
                            temp.Add(mbps);
                    });
                }
                list.ForEach(mbps =>
                    {
                        if (_genre.Id.Equals((int)mbps.GenreId) && !temp.Contains(mbps))
                            temp.Add(mbps);
                    });
                list = temp;
                var result = new UnhierarchicalBookProductsViewModel()
                {
                    CampaignId = CampaignId,
                    Title = MobileConstants.TITLE_SAME_GENRE,
                    BookProducts = list
                };
                return ServiceUtils.GetResponseDetail(result);
            }
            return null;
        }
        private UnhierarchicalBookProductsViewModel GetSameIssuerBookProducts(int? CampaignId, Guid? IssuerId, Guid? id = null, List<byte?> invalidStatus = null)
        {
            if (invalidStatus == null)
                invalidStatus = new List<byte?>()
            {
                (byte)BookProductStatus.Pending,
                (byte)BookProductStatus.Rejected
            };
            var list = _unitOfWork.BookProducts.Get(bps => bps.IssuerId.Equals(IssuerId)
            && bps.CampaignId.Equals(CampaignId)
            && !invalidStatus.Contains(bps.Status))
            .ProjectTo<MobileBookProductsViewModel>(_mapper.ConfigurationProvider).ToList();
            if (id != null)
                list = list.Where(l => !l.Id.Equals(id)).ToList();
            if (list.Any())
            {
                var result = new UnhierarchicalBookProductsViewModel()
                {
                    CampaignId = CampaignId,
                    Title = MobileConstants.TITLE_SAME_ISSUER,
                    BookProducts = list
                };
                return ServiceUtils.GetResponseDetail(result);
            }
            return null;
        }
        private HierarchicalBookProductsViewModel GetIssuerBookProducts(int? CampaignId)
        {
            var list = _unitOfWork.BookProducts.Get(bps => bps.IssuerId.HasValue
           && bps.CampaignId.Equals(CampaignId))
           .ProjectTo<MobileBookProductsViewModel>(_mapper.ConfigurationProvider).ToList();
            var groups = list.GroupBy(bps => bps.IssuerId).ToList();
            var tempList = new List<SubHierarchicalBookProductsViewModel>();
            HierarchicalBookProductsViewModel result = null;
            groups.ForEach(g =>
            {
                var Issuer = _unitOfWork.Users.Get(u => u.Id.Equals(g.Key))
                .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefault();
                if (Issuer == null)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                    {
                        ErrorMessageConstants.NOT_FOUND,
                        ErrorMessageConstants.ISSUER,
                        g.Key.ToString()
                    });
                var temp = new SubHierarchicalBookProductsViewModel()
                {
                    SubTitle = Issuer.Name,
                    IssuerId = g.Key,
                    Issuer = Issuer,
                    BookProducts = g.ToList()
                };
                tempList.Add(temp);
            });
            if (tempList.Any())
            {
                var WithLevel = CheckCampaignLevel(CampaignId);
                if (WithLevel != null)
                {
                    tempList.ForEach(item =>
                    {
                        item.BookProducts.ForEach(bp =>
                        {
                            bp.WithLevel = WithLevel.First();
                            bp.AllowPurchasingByLevel = WithLevel.Last();
                        });
                    });
                }
                result = new HierarchicalBookProductsViewModel()
                {
                    CampaignId = CampaignId,
                    Title = MobileConstants.TITLE_ISSUER,
                    subHierarchicalBookProducts = ServiceUtils.GetResponseDetails(tempList)
                };
            }
            return result;
        }
        private HierarchicalBookProductsViewModel GetIssuerBookProducts(CampaignMobileViewModel campaign)
        {
            var list = campaign.BookProducts.Where(bps => bps.IssuerId.HasValue).ToList();
            HierarchicalBookProductsViewModel result = null;
            if (list.Any())
            {
                var groups = list.GroupBy(bps => bps.IssuerId).ToList();
                var tempList = new List<SubHierarchicalBookProductsViewModel>();
                groups.ForEach(g =>
                {
                    var Issuer = _unitOfWork.Users.Get(u => u.Id.Equals(g.Key))
                    .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider)
                    .SingleOrDefault();
                    if (Issuer == null)
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                        {
                            ErrorMessageConstants.NOT_FOUND,
                            ErrorMessageConstants.ISSUER,
                            g.Key.ToString()
                        });
                    var temp = new SubHierarchicalBookProductsViewModel()
                    {
                        SubTitle = Issuer.Name,
                        IssuerId = g.Key,
                        Issuer = Issuer,
                        BookProducts = g.ToList()
                    };
                    tempList.Add(temp);
                });
                if (tempList.Any())
                {
                    var WithLevel = CheckCampaignLevel(campaign);
                    if (WithLevel != null)
                    {
                        tempList.ForEach(item => item.BookProducts.ForEach(bp =>
                        {
                            bp.WithLevel = WithLevel.First();
                            bp.AllowPurchasingByLevel = WithLevel.Last();
                        }));
                    }
                    result = new HierarchicalBookProductsViewModel()
                    {
                        CampaignId = campaign.Id,
                        Title = MobileConstants.TITLE_ISSUER,
                        subHierarchicalBookProducts = ServiceUtils.GetResponseDetails(tempList)
                    };
                }
            }
            return result;
        }
        private HierarchicalBookProductsViewModel GetIssuerBookProducts(int? CampaignId, Guid? IssuerId)
        {
            var list = _unitOfWork.BookProducts.Get(bps => bps.IssuerId.Equals(IssuerId)
           && bps.CampaignId.Equals(CampaignId))
           .ProjectTo<MobileBookProductsViewModel>(_mapper.ConfigurationProvider).ToList();
            if (!list.Any())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.ISSUER,
                    IssuerId.ToString()
                });
            var Issuer = _unitOfWork.Users.Get(u => u.Id.Equals(IssuerId))
            .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider)
            .SingleOrDefault();
            if (Issuer == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.ISSUER,
                    IssuerId.ToString()
                });
            var groups = list.GroupBy(bps => bps.IssuerId).ToList();
            var WithLevel = CheckCampaignLevel(CampaignId);
            if (WithLevel != null)
            {
                list.ForEach(bp =>
                {
                    bp.WithLevel = WithLevel.First();
                    bp.AllowPurchasingByLevel = WithLevel.Last();
                });
            }
            var tempList = new List<SubHierarchicalBookProductsViewModel>()
            {
                new SubHierarchicalBookProductsViewModel()
                {
                    SubTitle = Issuer.Name,
                    IssuerId = IssuerId,
                    Issuer = Issuer,
                    BookProducts = list
                }
            };
            HierarchicalBookProductsViewModel result = new HierarchicalBookProductsViewModel()
            {
                CampaignId = CampaignId,
                Title = MobileConstants.TITLE_ISSUER,
                subHierarchicalBookProducts = ServiceUtils.GetResponseDetails(tempList)
            };
            return result;
        }

        private HierarchicalBookProductsViewModel GetGenreBookProduct(int? CampaignId)
        {
            var list = _unitOfWork.BookProducts.Get(bps => !bps.Type.Equals((byte)BookType.Combo)
           && bps.CampaignId.Equals(CampaignId))
           .ProjectTo<MobileBookProductsViewModel>(_mapper.ConfigurationProvider).ToList();
            var groups = list.GroupBy(bps => bps.Book.GenreId).ToList();
            var tempList = new List<SubHierarchicalBookProductsViewModel>();
            HierarchicalBookProductsViewModel result = null;
            groups.ForEach(g =>
            {
                var Genre = _unitOfWork.Genres.Get(u => u.Id.Equals(g.Key))
                .ProjectTo<GenreViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefault();
                if (Genre == null)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                    {
                        ErrorMessageConstants.NOT_FOUND,
                        ErrorMessageConstants.GENRE_ID,
                        g.Key.ToString()
                    });
                var temp = new SubHierarchicalBookProductsViewModel()
                {
                    SubTitle = Genre.Name,
                    GenreId = g.Key,
                    Genre = Genre,
                    BookProducts = g.ToList()
                };
                tempList.Add(temp);
            });
            if (tempList.Any())
            {
                var WithLevel = CheckCampaignLevel(CampaignId);
                if (WithLevel != null)
                {
                    tempList.ForEach(item =>
                    {
                        item.BookProducts.ForEach(bp =>
                        {
                            bp.WithLevel = WithLevel.First();
                            bp.AllowPurchasingByLevel = WithLevel.Last();
                        });
                    });
                }
                result = new HierarchicalBookProductsViewModel()
                {
                    CampaignId = CampaignId,
                    Title = MobileConstants.TITLE_GENRE,
                    subHierarchicalBookProducts = ServiceUtils.GetResponseDetails(tempList)
                };
            }
            return result;
        }
        private HierarchicalBookProductsViewModel GetGenreBookProduct(CampaignMobileViewModel campaign)
        {
            var list = campaign.BookProducts.Where(bps => !bps.Type.Equals((byte)BookType.Combo)).ToList();
            HierarchicalBookProductsViewModel result = null;
            if (list.Any())
            {
                var groups = list.GroupBy(bps => bps.Book.GenreId).ToList();
                var tempList = new List<SubHierarchicalBookProductsViewModel>();
                groups.ForEach(g =>
                {
                    var Genre = _unitOfWork.Genres.Get(u => u.Id.Equals(g.Key))
                    .ProjectTo<GenreViewModel>(_mapper.ConfigurationProvider)
                    .SingleOrDefault();
                    if (Genre == null)
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                        {
                            ErrorMessageConstants.NOT_FOUND,
                            ErrorMessageConstants.GENRE_ID,
                            g.Key.ToString()
                        });
                    var temp = new SubHierarchicalBookProductsViewModel()
                    {
                        SubTitle = Genre.Name,
                        GenreId = g.Key,
                        Genre = Genre,
                        BookProducts = g.ToList()
                    };
                    tempList.Add(temp);
                });
                if (tempList.Any())
                {
                    var WithLevel = CheckCampaignLevel(campaign);
                    if (WithLevel != null)
                    {
                        tempList.ForEach(item => item.BookProducts.ForEach(bp =>
                        {
                            bp.WithLevel = WithLevel.First();
                            bp.AllowPurchasingByLevel = WithLevel.Last();
                        }));
                    }
                    result = new HierarchicalBookProductsViewModel()
                    {
                        CampaignId = campaign.Id,
                        Title = MobileConstants.TITLE_GENRE,
                        subHierarchicalBookProducts = ServiceUtils.GetResponseDetails(tempList)
                    };
                }
            }
            return result;
        }
        private HierarchicalBookProductsViewModel GetGenreBookProduct(int? CampaignId, int? GenreId)
        {
            var list = _unitOfWork.BookProducts.Get(bps => !bps.Type.Equals((byte)BookType.Combo)
           && bps.CampaignId.Equals(CampaignId))
           .ProjectTo<MobileBookProductsViewModel>(_mapper.ConfigurationProvider).ToList();
            var groups = list.GroupBy(bps => bps.Book.GenreId).ToList();
            var tempList = new List<SubHierarchicalBookProductsViewModel>();
            HierarchicalBookProductsViewModel result = null;
            groups.ForEach(g =>
            {
                if (g.Key.Equals(GenreId))
                {
                    var Genre = _unitOfWork.Genres.Get(u => u.Id.Equals(g.Key))
                    .ProjectTo<GenreViewModel>(_mapper.ConfigurationProvider)
                    .SingleOrDefault();
                    if (Genre == null)
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                        {
                        ErrorMessageConstants.NOT_FOUND,
                        ErrorMessageConstants.GENRE_ID,
                        g.Key.ToString()
                        });
                    var temp = new SubHierarchicalBookProductsViewModel()
                    {
                        SubTitle = Genre.Name,
                        GenreId = g.Key,
                        Genre = Genre,
                        BookProducts = g.ToList()
                    };
                    tempList.Add(temp);
                }
            });
            if (!tempList.Any())
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                    {
                        ErrorMessageConstants.NOT_FOUND,
                        ErrorMessageConstants.GENRE_ID,
                        GenreId.ToString()
                    });
            }
            var WithLevel = CheckCampaignLevel(CampaignId);
            if (WithLevel != null)
            {
                list.ForEach(bp =>
                {
                    bp.WithLevel = WithLevel.First();
                    bp.AllowPurchasingByLevel = WithLevel.Last();
                });
            }
            result = new HierarchicalBookProductsViewModel()
            {
                CampaignId = CampaignId,
                Title = MobileConstants.TITLE_GENRE,
                subHierarchicalBookProducts = ServiceUtils.GetResponseDetails(tempList)
            };
            return result;
        }

        private bool IsNotNullBookFilter(BookMobileRequestModel book)
        {
            var result = book.GenreIds != null || book.Languages != null || book.IssuerIds != null || book.PublisherIds != null;
            if (book.BookAuthors != null)
                result = book.BookAuthors.AuthorIds.Any();
            return result;
        }

        private bool IsNotNullBookGenresFilter(BookProductMobileRequestModel request)
        {
            var result = false;
            if (request.Book != null)
                result = request.Book.GenreIds != null;
            return result;
        }

        private BookProductMobileRequestModel CheckBookGenreFilter(BookProductMobileRequestModel request)
        {
            if (IsNotNullBookGenresFilter(request))
                request.Book.GenreIds =
                ServiceUtils.CheckBookGenreFilter(request.Book.GenreIds, _unitOfWork);
            return request;
        }

        private IQueryable<MobileBookProductViewModel> FilterBookProductsByType(IQueryable<MobileBookProductViewModel> query, BookProductMobileRequestModel filter)
        {
            if (filter.Book != null)
            {
                query = FilterBookProductsByAuthorIds(query.ToList(), filter).AsQueryable();
                var oddBookProducts = query.Where(mbp => mbp.Type.Equals((byte)BookType.Odd));
                var seriesBookProducts = query.Where(mbp => mbp.Type.Equals((byte)BookType.Series));
                var comboBookProducts = query.Where(mbp => mbp.Type.Equals((byte)BookType.Combo));
                var list = new List<MobileBookProductViewModel>();
                if (oddBookProducts.Any())
                {
                    var _filter = new BookProductMobileTypeRequestModel(filter.Book, BookType.Odd);
                    var temp = oddBookProducts.DynamicOtherFilter(_filter);
                    if (temp.Any())
                        list.AddRange(temp);
                }
                if (seriesBookProducts.Any())
                {
                    var _filter = new BookProductMobileTypeRequestModel(filter.Book, BookType.Series);
                    var temp = seriesBookProducts.DynamicOtherFilter(_filter);
                    if (temp.Any())
                        list.AddRange(temp);
                }
                if (comboBookProducts.Any())
                {
                    var _filter = new BookProductMobileTypeRequestModel(filter.Book, BookType.Combo);
                    var temp = comboBookProducts.DynamicOtherFilter(_filter);
                    if (temp.Any())
                        list.AddRange(temp);
                }
                if (list.Any())
                {
                    if (!string.IsNullOrEmpty(filter.Sort))
                    {
                        var _filter = new BookProductMobileRequestModel()
                        {
                            Sort = filter.Sort
                        };
                        list = list.AsQueryable().DynamicOtherFilter(_filter).ToList();
                    }
                    else
                        list = list.OrderBy(item => item.Status).ToList();
                }
                return list.AsQueryable();
            }
            return query;
        }

        private List<MobileBookProductViewModel> FilterBookProductsByAuthorIds(List<MobileBookProductViewModel> list, BookProductMobileRequestModel filter)
        {
            if (filter.Book != null)
            {
                if (list.Any() && filter.Book.BookAuthors != null)
                {
                    var AuthorIds = filter.Book.BookAuthors.AuthorIds;
                    var temp = new List<MobileBookProductViewModel>();
                    var oddBookProducts = list.Where(l => l.Type.Equals((byte)BookType.Odd) &&
                    l.Book.BookAuthors.Any(bas => AuthorIds.Contains(bas.AuthorId)));
                    var seriesBookProducts = list.Where(l => l.Type.Equals((byte)BookType.Series) &&
                    l.BookProductItems.Any(bpi => bpi.Book.BookAuthors.Any(bas => AuthorIds.Contains(bas.AuthorId))));
                    var comboBookProducts = list.Where(l => l.Type.Equals((byte)BookType.Combo) &&
                    l.BookProductItems.Any(bpi => bpi.Book.BookAuthors.Any(bas => AuthorIds.Contains(bas.AuthorId))));
                    if (oddBookProducts.Any())
                        temp.AddRange(oddBookProducts);
                    if (seriesBookProducts.Any())
                        temp.AddRange(seriesBookProducts);
                    if (comboBookProducts.Any())
                        temp.AddRange(comboBookProducts);
                    list = temp;
                }
            }
            return list;
        }

        private List<bool> CheckCampaignLevel(int? CampaignId)
        {
            if (CampaignId.HasValue)
            {
                var campaignLevels = _unitOfWork.CampaignLevels.Get(cls => cls.CampaignId.Equals(CampaignId)).ToList();
                if (campaignLevels.Any())
                {
                    var list = new List<bool>() { true };
                    var IsCustomer = ServiceUtils.CheckRole(_httpContextAccessor, BoekRole.Customer);
                    if (IsCustomer)
                    {
                        var UserId = ServiceUtils.GetUserInfo(_httpContextAccessor);
                        var customer = _unitOfWork.Customers.Get(UserId);
                        if (customer != null)
                            list.Add(campaignLevels.Select(cls => cls.LevelId).Contains(customer.LevelId));
                        else
                            list.Add(false);
                    }
                    else
                        list.Add(false);
                    return list;
                }
            }
            return null;
        }
        private List<bool> CheckCampaignLevel(CampaignMobileViewModel campaign)
        {
            if (campaign.Levels != null)
            {
                if (campaign.Levels.Any())
                {
                    var list = new List<bool>() { true };
                    var IsCustomer = ServiceUtils.CheckRole(_httpContextAccessor, BoekRole.Customer);
                    if (IsCustomer)
                    {
                        var UserId = ServiceUtils.GetUserInfo(_httpContextAccessor);
                        var customer = _unitOfWork.Customers.Get(UserId);
                        if (customer != null)
                            list.Add(campaign.Levels.Select(l => l.Id).Contains(customer.LevelId));
                        else
                            list.Add(false);
                    }
                    else
                        list.Add(false);
                    return list;
                }
            }
            return null;
        }
        #endregion
    }
}