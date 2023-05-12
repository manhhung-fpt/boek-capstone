using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boek.Core.Constants;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.BookProducts;
using Boek.Infrastructure.Requests.BookProducts.BookComboProducts;
using Boek.Infrastructure.Requests.BookProducts.BookSeriesProducts;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.BookProducts;
using Boek.Repository.Interfaces;
using Boek.Service.Commons;
using Boek.Service.Interfaces;
using Boek.Service.Utils;
using Boek.Core.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Boek.Core.Extensions;
using System.Net;
using Boek.Infrastructure.Requests.Notifications;
using Boek.Infrastructure.ViewModels.Books;
using System.Security.Claims;

namespace Boek.Service.Services
{
    public class BookProductService : IBookProductService
    {
        #region Fields and constructor
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly INotificationService _notificationService;

        public BookProductService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _notificationService = notificationService;
        }
        #endregion

        #region Gets
        public BookProductViewModel GetBookProductById(Guid id)
        {
            var BookProduct = _unitOfWork.BookProducts.Get(id);
            if (BookProduct == null)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.BOOK_PRODUCT_ID
                });
            }
            return GetResponse(BookProduct);
        }

        public BaseResponsePagingModel<BookProductViewModel> GetBookProducts(BookProductRequestModel filter, PagingModel paging)
        {
            filter.GenreIds = ServiceUtils.CheckBookGenreFilter(filter.GenreIds, _unitOfWork);
            var result = _unitOfWork.BookProducts.Get()
                    .ProjectTo<BookProductViewModel>(_mapper.ConfigurationProvider)
                    .DynamicOtherFilter(filter)
                    .PagingQueryable(paging.Page, paging.Size, PageConstant.LimitPaging, PageConstant.LimitPaging);
            var list = result.Item2.ToList();
            if (list.Any())
                list.ForEach(b => b = ServiceUtils.GetResponseDetail(b));

            return new BaseResponsePagingModel<BookProductViewModel>()
            {
                Metadata = new PagingMetadata()
                {
                    Page = paging.Page,
                    Size = paging.Size,
                    Total = result.Item1
                },
                Data = list
            };
        }

        #region Issuer
        public BaseResponsePagingModel<BookProductViewModel> GetBookProductsByIssuer(BookProductRequestModel filter, PagingModel paging)
        {
            filter.GenreIds = ServiceUtils.CheckBookGenreFilter(filter.GenreIds, _unitOfWork);
            filter.IssuerId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            var result = _unitOfWork.BookProducts.Get()
            .ProjectTo<BookProductViewModel>(_mapper.ConfigurationProvider)
            .DynamicOtherFilter(filter)
            .PagingQueryable(paging.Page, paging.Size, PageConstant.LimitPaging, PageConstant.LimitPaging);
            var list = result.Item2.ToList();
            if (list.Any())
                list.ForEach(bp => bp = ServiceUtils.GetResponseDetail(bp));

            return new BaseResponsePagingModel<BookProductViewModel>()
            {
                Metadata = new PagingMetadata()
                {
                    Page = paging.Page,
                    Size = paging.Size,
                    Total = result.Item1
                },
                Data = list
            };
        }

        public BookProductViewModel GetBookProductByIdByIssuer(Guid id)
        {
            var BookProduct = _unitOfWork.BookProducts.Get(id);
            if (BookProduct == null)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                        ErrorMessageConstants.NOT_FOUND,
                        ErrorMessageConstants.BOOK_PRODUCT_ID
                });
            }
            var issuerId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            if (!BookProduct.IssuerId.Equals(issuerId))
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                        ErrorMessageConstants.BOOK_PRODUCT,
                        MessageConstants.MESSAGE_NOT_BELONGING,
                        ErrorMessageConstants.ISSUER
                });
            }
            return GetResponse(BookProduct);
        }

        public List<BookProductViewModel> GetExistedComboBookProductsByIssuer(IssuerComboBookProductRequestModel filter)
        {
            filter.GenreIds = ServiceUtils.CheckBookGenreFilter(filter.GenreIds, _unitOfWork);
            filter.IssuerId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            filter.Type = (byte)BookType.Combo;
            var result = _unitOfWork.BookProducts.Get(bp => !bp.CampaignId.Equals(filter.CurrentCampaignId))
            .ProjectTo<BookProductViewModel>(_mapper.ConfigurationProvider)
            .DynamicOtherFilter(filter).ToList();
            var currentCombos = _unitOfWork.BookProducts.Get(bp => bp.CampaignId.Equals(filter.CurrentCampaignId))
            .ProjectTo<BookProductViewModel>(_mapper.ConfigurationProvider)
            .DynamicOtherFilter(filter).ToList();
            if (result.Any() && currentCombos.Any())
            {
                var currentBookIds = currentCombos.Select(cc =>
                cc.BookProductItems.Select(bpi =>
                bpi.BookId).ToList()).ToList();
                var list = new List<BookProductViewModel>();
                result.ForEach(bps =>
                {
                    //cSpell:disable
                    var existedBookProductItemIds = bps.BookProductItems
                    .Select(bpis => bpis.BookId).ToList().OrderBy(ebpi => ebpi);
                    currentBookIds.ForEach(cbis =>
                    {
                        if (!cbis.OrderBy(cbi => cbi).Equals(existedBookProductItemIds) && !list.Contains(bps))
                            list.Add(bps);
                    });
                });
                result = list;
            }
            return result;
        }
        #endregion
        #endregion

        #region Book Product
        public BookProductViewModel CreateBookProduct(CreateBookProductRequestModel createdBookProduct)
        {
            var _bookProduct = _mapper.Map<BookProduct>(createdBookProduct);
            List<BookProductItem> _bookProductItems = null;
            checkBookProduct(ref _bookProduct, BookType.Odd, ref _bookProductItems);
            _unitOfWork.BookProducts.Create(_bookProduct);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[] {
                        ErrorMessageConstants.INSERT,
                        ErrorMessageConstants.BOOK_PRODUCT.ToLower(),
                        MessageConstants.MESSAGE_FAILED
                    });
            UpdateCreatedDateBookProduct(_bookProduct);
            return GetResponse(_bookProduct);
        }
        public BookProductViewModel UpdateBookProduct(UpdateBookProductRequestModel updatedBookProduct)
        {
            var _updateBookProduct = _mapper.Map<BookProduct>(updatedBookProduct);
            List<BookProductItem> _bookProductItems = null;
            checkBookProduct(ref _updateBookProduct, BookType.Odd, ref _bookProductItems, false);
            var _temp = ConvertToBookProduct(_updateBookProduct);
            _unitOfWork.BookProducts.Update(_temp);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[] {
                        ErrorMessageConstants.UPDATE,
                        ErrorMessageConstants.BOOK_PRODUCT.ToLower(),
                        MessageConstants.MESSAGE_FAILED
                    });
            return GetResponse(_updateBookProduct);
        }
        #endregion

        #region Book Combo Product
        public BookProductViewModel CreateBookComboProduct(CreateBookComboProductRequestModel createBookCombo)
        {
            var _bookProduct = _mapper.Map<BookProduct>(createBookCombo);
            var _bookProductItems = _bookProduct.BookProductItems.ToList();
            var type = BookType.Combo;
            checkBookProduct(ref _bookProduct, type, ref _bookProductItems);
            _bookProduct.BookProductItems = _bookProductItems;
            _unitOfWork.BookProducts.Create(_bookProduct);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[] {
                        ErrorMessageConstants.INSERT,
                        ErrorMessageConstants.BOOK_PRODUCT.ToLower(),
                        MessageConstants.MESSAGE_FAILED
                    });
            UpdateCreatedDateBookProduct(_bookProduct);
            return GetResponse(_bookProduct);
        }

        public BookProductViewModel UpdateBookComboProduct(UpdateBookComboProductRequestModel updateBookCombo)
        {
            var _updateBookProduct = _mapper.Map<BookProduct>(updateBookCombo);
            var _bookProductItems = _updateBookProduct.BookProductItems.ToList();
            _updateBookProduct.BookProductItems.Clear();
            checkBookProduct(ref _updateBookProduct, BookType.Combo, ref _bookProductItems, false);
            var _temp = ConvertToBookProduct(_updateBookProduct);
            _unitOfWork.BookProducts.Update(_temp);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[] {
                        ErrorMessageConstants.UPDATE,
                        ErrorMessageConstants.BOOK_PRODUCT.ToLower(),
                        MessageConstants.MESSAGE_FAILED
                    });
            UpdateBookProductItems(_bookProductItems, _updateBookProduct.Id);
            return GetResponse(_updateBookProduct);
        }
        #endregion

        #region Book Series Product
        public BookProductViewModel CreateBookSeriesProduct(CreateBookSeriesProductRequestModel createBookSeries)
        {
            var _createBookProduct = _mapper.Map<BasicCreateBookSeriesProductRequestModel>(createBookSeries);
            var _bookProductItems = _mapper.Map<List<BookProductItem>>(createBookSeries.BookProductItems);
            var _bookProduct = _mapper.Map<BookProduct>(_createBookProduct);
            var type = BookType.Series;
            checkBookProduct(ref _bookProduct, type, ref _bookProductItems);
            _bookProductItems.ForEach(bpi => bpi.ParentBookProductId = _bookProduct.Id);
            var bookItems = GetSeriesBookItems(_bookProduct.BookId);
            _unitOfWork.BookProducts.Create(_bookProduct);
            _bookProduct.BookProductItems = _bookProductItems;
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[] {
                        ErrorMessageConstants.INSERT,
                        ErrorMessageConstants.BOOK_PRODUCT.ToLower(),
                        MessageConstants.MESSAGE_FAILED
                    });
            UpdateSeriesBookItems(_bookProduct.BookId, bookItems);
            UpdateCreatedDateBookProduct(_bookProduct);
            return GetResponse(_bookProduct);
        }

        public BookProductViewModel UpdateBookSeriesProduct(UpdateBookSeriesProductRequestModel updateBookSeries)
        {
            var _basicUpdateBookProduct = _mapper.Map<BasicUpdateBookSeriesProductRequestModel>(updateBookSeries);
            var _bookProductItems = _mapper.Map<List<BookProductItem>>(updateBookSeries.BookProductItems);
            var _updateBookProduct = _mapper.Map<BookProduct>(_basicUpdateBookProduct);
            var type = BookType.Series;
            checkBookProduct(ref _updateBookProduct, type, ref _bookProductItems, false);
            _bookProductItems.ForEach(bpi => bpi.ParentBookProductId = _updateBookProduct.Id);
            var _temp = ConvertToBookProduct(_updateBookProduct);
            var bookItems = GetSeriesBookItems(_updateBookProduct.BookId);
            _unitOfWork.BookProducts.Update(_temp);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[] {
                        ErrorMessageConstants.UPDATE,
                        ErrorMessageConstants.BOOK_PRODUCT.ToLower(),
                        MessageConstants.MESSAGE_FAILED
                    });
            UpdateSeriesBookItems(_updateBookProduct.BookId, bookItems);
            UpdateBookProductItems(_bookProductItems, _updateBookProduct.Id);
            return GetResponse(_updateBookProduct);
        }
        #endregion

        #region Check Book Product
        public BookProductViewModel AcceptBookProduct(CheckBookProductRequestModel checkBook)
        {
            var BookProduct = _mapper.Map<BookProduct>(checkBook);
            BookProduct = CheckPendingBookProduct(BookProduct, checkBook.Note);
            BookProduct.Status = BookProduct.SaleQuantity > 0 ?
            (byte)BookProductStatus.Sale : (byte)BookProductStatus.OutOfStock;
            _unitOfWork.BookProducts.Update(BookProduct);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[] {
                        ErrorMessageConstants.UPDATE,
                        ErrorMessageConstants.BOOK_PRODUCT.ToLower(),
                        MessageConstants.MESSAGE_FAILED
                    });
            return GetResponse(BookProduct);
        }

        public BookProductViewModel RejectBookProduct(CheckBookProductRequestModel checkBook)
        {
            var BookProduct = _mapper.Map<BookProduct>(checkBook);
            BookProduct = CheckPendingBookProduct(BookProduct, checkBook.Note);
            BookProduct.Status = (byte)BookProductStatus.Rejected;
            _unitOfWork.BookProducts.Update(BookProduct);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[] {
                        ErrorMessageConstants.UPDATE,
                        ErrorMessageConstants.BOOK_PRODUCT.ToLower(),
                        MessageConstants.MESSAGE_FAILED
                    });
            return GetResponse(BookProduct);
        }
        #endregion

        #region Update quantity and status
        public BookProductViewModel UpdateBookProductStartedCampaign(UpdateBookProductStartedCampaignRequestModel updateBookProduct)
        {
            var bookProduct = _mapper.Map<BookProduct>(updateBookProduct);
            bookProduct = CheckUpdateBookProductDetailOfStartCampaign(bookProduct);
            bookProduct.SaleQuantity = updateBookProduct.SaleQuantity;
            bookProduct.Status = bookProduct.SaleQuantity <= 0 ? (byte)BookProductStatus.OutOfStock : updateBookProduct.Status;
            _unitOfWork.BookProducts.Update(bookProduct);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                    {
                        ErrorMessageConstants.UPDATE,
                        ErrorMessageConstants.BOOK_PRODUCT.ToLower(),
                        MessageConstants.MESSAGE_FAILED
                    });
            return GetResponse(bookProduct);
        }
        #endregion

        #region Notification
        public void SendCheckingNotification(BookProductViewModel bookProduct)
        {
            var notification = new NotificationRequestModel()
            {
                UserRoles = new List<byte>() { (byte)BoekRole.Admin },
                Status = bookProduct.Status,
                StatusName = bookProduct.StatusName,
                Message = $"Sách {bookProduct.Title} cần duyệt.",
            };
            _notificationService.PushCheckingBookProductNotification(notification);
        }
        public void SendDoneCheckingNotification(BookProductViewModel bookProduct)
        {
            var Message = $"Sách {bookProduct.Title} ";
            Message += bookProduct.Status.Equals((byte)BookProductStatus.Rejected) ? "bị từ chối." : "được chấp nhận.";
            var notification = new NotificationRequestModel()
            {
                UserIds = new List<Guid?>() { bookProduct.IssuerId },
                Status = bookProduct.Status,
                StatusName = bookProduct.StatusName,
                Message = Message
            };
            _notificationService.PushDoneCheckingBookProductNotification(notification);
        }
        #endregion

        #region Utils
        private BookProductViewModel GetResponse(BookProduct bookProduct)
        {
            var response = _unitOfWork.BookProducts
            .Get(bp => bp.Id.Equals(bookProduct.Id))
            .ProjectTo<BookProductViewModel>(_mapper.ConfigurationProvider)
            .SingleOrDefault();
            if (response != null)
            {
                if (response.Type.Equals((byte)BookType.Series))
                {
                    var book = _unitOfWork.Books.Get(bp => bp.Id.Equals(bookProduct.BookId))
                    .ProjectTo<BookViewModel>(_mapper.ConfigurationProvider)
                    .SingleOrDefault();
                    if (book != null)
                    {
                        book = ServiceUtils.GetBookPdfAndAudioDetail(book);
                        if (response.Book != null)
                        {
                            response.Book.FullPdfAndAudio = book.FullPdfAndAudio;
                            response.Book.OnlyAudio = book.OnlyAudio;
                            response.Book.OnlyPdf = book.OnlyPdf;
                        }
                    }
                }
                response = ServiceUtils.GetResponseDetail(response);
            }
            return response ?? new BookProductViewModel();
        }

        private void UpdateCreatedDateBookProduct(BookProduct bookProduct)
        {
            var response = _unitOfWork.BookProducts
            .Get(bp => bp.Id.Equals(bookProduct.Id)).SingleOrDefault();
            if (response != null)
            {
                response.CreatedDate = DateTime.Now;
                _unitOfWork.BookProducts.Update(response);
                if (!_unitOfWork.Save())
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[] {
                        ErrorMessageConstants.INSERT,
                        ErrorMessageConstants.BOOK_PRODUCT.ToLower(),
                        MessageConstants.MESSAGE_FAILED
                    });
            }
        }
        private void UpdateBookProductItems(List<BookProductItem> bookProductItems, Guid? ParentBookProductId)
        {
            var bookproductitems = _unitOfWork.BookProductItems.Get(f =>
            f.ParentBookProductId.Equals(ParentBookProductId)).ToList();

            if (bookproductitems.Any())
            {
                var deletedBookProductItems = new List<BookProductItem>();
                var existedBookProductItems = new List<BookProductItem>();
                bookproductitems.ForEach(bpis =>
                {
                    if (bookProductItems.Any(bis => bis.BookId.Equals(bpis.BookId)))
                        existedBookProductItems.Add(bpis);
                    else
                        deletedBookProductItems.Add(bpis);
                });
                _unitOfWork.BookProductItems.RemoveRange(deletedBookProductItems);
                var result = _unitOfWork.Save();
                if (deletedBookProductItems.Any() && !result)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                    {
                        ErrorMessageConstants.DELETE,
                        ErrorMessageConstants.BOOK_ITEM.ToLower(),
                        ErrorMessageConstants.BOOK_COMBO.ToLower(),
                        MessageConstants.MESSAGE_FAILED
                    });
                if (existedBookProductItems.Any())
                {
                    var newBookItems = new List<BookProductItem>();
                    bookProductItems.ForEach(bpis =>
                    {
                        if (!existedBookProductItems.Any(ebi => ebi.BookId.Equals(bpis.BookId)))
                        {
                            bpis.ParentBookProductId = ParentBookProductId;
                            newBookItems.Add(bpis);
                        }
                    });
                    _unitOfWork.BookProductItems.AddRange(newBookItems);
                    result = _unitOfWork.Save();
                    if (newBookItems.Any() && !result)
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                        {
                            ErrorMessageConstants.INSERT,
                            ErrorMessageConstants.BOOK_ITEM.ToLower(),
                            ErrorMessageConstants.BOOK_COMBO.ToLower(),
                            MessageConstants.MESSAGE_FAILED
                        });
                    var updatedBookProductItems = new List<BookProductItem>();
                    bookProductItems.ForEach(bpis =>
                    {
                        var temp = existedBookProductItems.SingleOrDefault(ebi => ebi.BookId.Equals(bpis.BookId));
                        if (temp != null)
                        {
                            var result = CheckUpdatedBookProductItem(bpis, temp);
                            if (result != null)
                            {
                                result.Id = temp.Id;
                                result.ParentBookProductId = ParentBookProductId;
                                updatedBookProductItems.Add(result);
                            }
                        }
                    });
                    _unitOfWork.BookProductItems.UpdateRange(updatedBookProductItems);
                    result = _unitOfWork.Save();
                    if (updatedBookProductItems.Any() && !result)
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                        {
                            ErrorMessageConstants.UPDATE,
                            ErrorMessageConstants.BOOK_ITEM.ToLower(),
                            ErrorMessageConstants.BOOK_COMBO.ToLower(),
                            MessageConstants.MESSAGE_FAILED
                        });

                }
            }
        }

        private BookProductItem CheckUpdatedBookProductItem(BookProductItem newBookProductItem, BookProductItem oldBookProductItem)
        {
            var flag = false;
            if (newBookProductItem == null || oldBookProductItem == null)
                return null;
            if (!newBookProductItem.Format.Equals(oldBookProductItem.Format))
            {
                oldBookProductItem.Format = newBookProductItem.Format;
                flag = true;
            }
            if (!newBookProductItem.DisplayIndex.Equals(oldBookProductItem.DisplayIndex))
            {
                oldBookProductItem.DisplayIndex = newBookProductItem.DisplayIndex;
                flag = true;
            }
            if (!newBookProductItem.WithPdf.Equals(oldBookProductItem.WithPdf))
            {
                oldBookProductItem.WithPdf = newBookProductItem.WithPdf;
                oldBookProductItem.PdfExtraPrice = newBookProductItem.PdfExtraPrice;
                flag = true;
            }
            if (!newBookProductItem.DisplayPdfIndex.Equals(oldBookProductItem.DisplayPdfIndex))
            {
                oldBookProductItem.DisplayPdfIndex = newBookProductItem.DisplayPdfIndex;
                flag = true;
            }
            if (!newBookProductItem.WithAudio.Equals(oldBookProductItem.WithAudio))
            {
                oldBookProductItem.WithAudio = newBookProductItem.WithAudio;
                oldBookProductItem.AudioExtraPrice = newBookProductItem.AudioExtraPrice;
                flag = true;
            }
            if (!newBookProductItem.DisplayAudioIndex.Equals(oldBookProductItem.DisplayAudioIndex))
            {
                oldBookProductItem.DisplayAudioIndex = newBookProductItem.DisplayAudioIndex;
                flag = true;
            }
            return flag ? oldBookProductItem : null;
        }

        private void checkBookProduct(ref BookProduct bookProduct, BookType Type, ref List<BookProductItem> bookProductItems, bool IsCreate = true)
        {
            var _book = new Book();
            var _bookProduct = new BookProduct();
            var _campaign = new Campaign();
            var _books = new List<Book>();
            var _genre = new Genre();
            CheckBookProductInfo(bookProduct, Type, ref _book, ref _bookProduct, ref _campaign, IsCreate);
            if (Type.Equals(BookType.Combo))
                CheckBookComboProductGenre(bookProduct, _campaign, ref _genre);
            CheckSaleQuantity(bookProduct.SaleQuantity);
            CheckParticipant(_campaign.Id);
            CheckDate((DateTime)_campaign.StartDate, (DateTime)_campaign.EndDate, IsCreate);
            CheckBookProductByType(_book, ref bookProduct, Type, IsCreate, _bookProduct, _campaign);
            if (!Type.Equals(BookType.Odd))
                GetBookProductItems(ref bookProductItems, _genre, Type, Type.Equals(BookType.Combo) ? null : bookProduct.BookId, _campaign);
        }

        private void CheckBookProductInfo(
            BookProduct bookProduct,
            BookType Type,
            ref Book _book,
            ref BookProduct _bookProduct,
            ref Campaign _campaign, bool IsCreate = true)
        {
            switch (Type)
            {
                case BookType.Odd:
                    CheckBookProductDetail(bookProduct, ref _book, ref _bookProduct, ref _campaign, IsCreate);
                    break;
                case BookType.Combo:
                    CheckBookComboProductDetail(bookProduct, ref _book, ref _bookProduct, ref _campaign, IsCreate);
                    break;
                case BookType.Series:
                    CheckBookProductDetail(bookProduct, ref _book, ref _bookProduct, ref _campaign, IsCreate);
                    break;
                default:
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                    {
                        ErrorMessageConstants.BOOK_PRODUCT_TYPE,
                        MessageConstants.MESSAGE_INVALID,
                    });
                    break;
            }
        }

        private void CheckBookProductDetail(
            BookProduct bookProduct,
            ref Book _book,
            ref BookProduct _bookProduct,
            ref Campaign _campaign, bool IsCreate = true)
        {
            var _temp = bookProduct;
            if (IsCreate)
            {
                _campaign = _unitOfWork.Campaigns.Get(c => c.Id.Equals(_temp.CampaignId))
                .Include(c => c.CampaignCommissions)
                .SingleOrDefault();
                if (_campaign == null)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                    {
                        ErrorMessageConstants.NOT_FOUND,
                        ErrorMessageConstants.CAMPAIGN_ID,
                    });
                var CampaignId = _campaign.Id;
                _bookProduct = _unitOfWork.BookProducts
                .Get(bp => bp.BookId.Equals(_temp.BookId) &&
                bp.CampaignId.Equals(CampaignId))
                .SingleOrDefault();
                if (_bookProduct != null)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[] {
                        ErrorMessageConstants.BOOK_PRODUCT,
                        MessageConstants.MESSAGE_EXISTED
                    });
                _book = _unitOfWork.Books.Get(b => b.Id.Equals(_temp.BookId))
                .Include(b => b.Genre)
                .SingleOrDefault();
                if (_book == null)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[] {
                        ErrorMessageConstants.NOT_FOUND,
                        ErrorMessageConstants.BOOK_ID
                    });
                var _bookItems = _unitOfWork.BookItems.Get(bi => bi.ParentBookId.Equals(_temp.BookId))
                .Include(bib => bib.Book).ToList();
                _book.BookItemBooks = _bookItems;
            }
            else
            {
                _bookProduct = _unitOfWork.BookProducts
                .Get(bp => bp.Id.Equals(_temp.Id))
                .Include(bp => bp.Book)
                .ThenInclude(book => book.Genre)
                .Include(bp => bp.Campaign)
                .ThenInclude(campaign => campaign.CampaignCommissions)
                .SingleOrDefault();
                if (_bookProduct == null)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                    {
                        ErrorMessageConstants.NOT_FOUND,
                        ErrorMessageConstants.BOOK_PRODUCT.ToLower(),
                    });
                _temp = _bookProduct;
                var _bookItems = _unitOfWork.BookItems.Get(bi => bi.ParentBookId.Equals(_temp.Book.Id))
                .Include(bib => bib.Book).ToList();
                _bookProduct.Book.BookItemBooks = _bookItems;
                _book = _bookProduct.Book;
                _campaign = _bookProduct.Campaign;
            }
            CheckBookProductGenre(_book, _campaign);
            CheckBookProductCommission(bookProduct, _book, _campaign);
        }
        private void CheckBookComboProductDetail(
            BookProduct bookProduct,
            ref Book _book,
            ref BookProduct _bookProduct,
            ref Campaign _campaign, bool IsCreate = true)
        {
            if (IsCreate)
            {
                _campaign = _unitOfWork.Campaigns
                .Get(c => c.Id.Equals(bookProduct.CampaignId))
                .Include(c => c.CampaignCommissions)
                .SingleOrDefault();
                if (_campaign == null)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                    {
                        ErrorMessageConstants.NOT_FOUND,
                        ErrorMessageConstants.CAMPAIGN_ID,
                    });
            }
            else
            {
                _bookProduct = _unitOfWork.BookProducts
                .Get(bp => bp.Id.Equals(bookProduct.Id))
                .Include(bp => bp.Campaign)
                .ThenInclude(campaign => campaign.CampaignCommissions)
                .SingleOrDefault();
                if (_bookProduct == null)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                    {
                        ErrorMessageConstants.NOT_FOUND,
                        ErrorMessageConstants.BOOK_PRODUCT.ToLower(),
                    });
                _campaign = _bookProduct.Campaign;
                _book.IssuerId = _bookProduct.IssuerId;
            }
            CheckComboTitle(bookProduct.Title, _campaign.Id, bookProduct.Id, IsCreate);
        }

        private void CheckBookProductByType(Book book, ref BookProduct bookProduct, BookType Type, bool IsCreate = true, BookProduct _bookProduct = null, Campaign campaign = null)
        {
            switch (Type)
            {
                case BookType.Odd:
                    CheckIssuer(book.IssuerId);
                    CheckIssuerStatus();
                    CheckOddBook(book);
                    CheckBookStatus(book);
                    CheckFormat(book, bookProduct, bookProduct.Format, campaign);
                    CheckDiscount(bookProduct);
                    CheckBookProductStatus(bookProduct, IsCreate, _bookProduct);
                    GetOddOrSeriesBookProduct(ref bookProduct, book, Type, IsCreate, _bookProduct);
                    break;
                case BookType.Combo:
                    CheckIssuerStatus();
                    bookProduct.IssuerId = IsCreate ? ServiceUtils.GetUserInfo(_httpContextAccessor) : book.IssuerId;
                    if (!IsCreate)
                    {
                        bookProduct.Type = _bookProduct.Type;
                        CheckIssuer(book.IssuerId);
                    }
                    CheckIssuerComboStatus(bookProduct.IssuerId);
                    CheckComboBook(bookProduct, IsCreate);
                    CheckBookProductStatus(bookProduct, IsCreate, _bookProduct);
                    GetComboBookProduct(ref bookProduct, Type, IsCreate, _bookProduct);
                    break;
                case BookType.Series:
                    CheckIssuer(book.IssuerId);
                    CheckIssuerStatus();
                    CheckSeriesBook(book);
                    CheckBookStatus(book);
                    CheckDiscount(bookProduct);
                    CheckBookProductStatus(bookProduct, IsCreate, _bookProduct);
                    GetOddOrSeriesBookProduct(ref bookProduct, book, Type, IsCreate, _bookProduct);
                    break;
                default:
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                    {
                        ErrorMessageConstants.BOOK_PRODUCT_TYPE,
                        MessageConstants.MESSAGE_INVALID,
                    });
                    break;
            }
        }
        private void CheckOddBook(Book book)
        {
            if ((bool)book.IsSeries)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                    {
                        ErrorMessageConstants.BOOK,
                        MessageConstants.MESSAGE_NOT_BELONGING,
                        ErrorMessageConstants.ODD_BOOK.ToLower()
                    });
        }

        private void CheckComboBook(BookProduct bookProduct, bool IsCreate = true)
        {
            if (!IsCreate)
            {
                if (!bookProduct.Type.Equals((byte)BookType.Combo))
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                    {
                        ErrorMessageConstants.BOOK_PRODUCT,
                        MessageConstants.MESSAGE_NOT_BELONGING,
                        ErrorMessageConstants.BOOK_COMBO.ToLower()
                    });
            }
            var ValidFormat = new List<byte>()
            {
                (byte) BookFormat.Audio,
                (byte) BookFormat.Pdf,
                (byte) BookFormat.PrintBook
            };
            CheckComboFormat(bookProduct.Format, ValidFormat);
        }

        private void CheckComboFormat(byte? Format, List<byte> ValidFormat)
        {
            if (!ValidFormat.Contains((byte)Format))
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                    {
                        ErrorMessageConstants.BOOK_PRODUCT_FORMAT,
                        MessageConstants.MESSAGE_INVALID,
                    });
        }

        private void CheckSeriesBook(Book book)
        {
            if (!(bool)book.IsSeries)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                {
                    ErrorMessageConstants.BOOK,
                    MessageConstants.MESSAGE_NOT_BELONGING,
                    ErrorMessageConstants.BOOK_SERIES.ToLower()
                });
        }

        private void CheckBookStatus(Book book)
        {
            if (book.Status.Equals((byte)BookStatus.Unreleased))
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                {
                    ErrorMessageConstants.BOOK_STATUS,
                    MessageConstants.MESSAGE_INVALID
                });
        }
        private void CheckIssuer(Guid? IssuerId)
        {
            Guid? issuerId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            if (IssuerId != issuerId)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                {
                    ErrorMessageConstants.BOOK,
                    MessageConstants.MESSAGE_NOT_BELONGING,
                    ErrorMessageConstants.ISSUER
                });
        }
        private void CheckIssuerStatus()
        {
            var issuerId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            var Issuer = _unitOfWork.Users.Get(issuerId);
            if (!(bool)Issuer.Status)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                {
                    ErrorMessageConstants.ACCOUNT_STATUS,
                    ErrorMessageConstants.ISSUER,
                    MessageConstants.MESSAGE_INVALID,
                });
        }
        private void CheckIssuerComboStatus(Guid? IssuerId, bool IsCreate = true)
        {
            if (!IsCreate)
            {
                Guid? issuerId = ServiceUtils.GetUserInfo(_httpContextAccessor);
                if (IssuerId != issuerId)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                    {
                        ErrorMessageConstants.BOOK,
                        MessageConstants.MESSAGE_NOT_BELONGING,
                        ErrorMessageConstants.ISSUER
                    });
            }
        }
        private void CheckSaleQuantity(int? SaleQuantity, bool IsCreate = true)
        {
            if (SaleQuantity == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.PreconditionRequired), new string[]
                    {
                        MessageConstants.MESSAGE_REQUIRED,
                        ErrorMessageConstants.BOOK_PRODUCT_SALE_QUANTITY
                    });
            if (IsCreate)
            {
                if (SaleQuantity <= 0)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                    {
                        ErrorMessageConstants.BOOK_PRODUCT_SALE_QUANTITY,
                        MessageConstants.MESSAGE_INVALID
                    });
            }
            else
            {
                if (SaleQuantity < 0)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                    {
                        ErrorMessageConstants.BOOK_PRODUCT_SALE_QUANTITY,
                        MessageConstants.MESSAGE_INVALID
                    });
            }
        }

        private void CheckBookProductGenre(Book _book, Campaign _campaign)
        {
            if (_campaign.CampaignCommissions.Any())
            {
                var genreId = _book.Genre.ParentId ?? _book.Genre.Id;
                if (!_campaign.CampaignCommissions.Any(cc => cc.GenreId.Equals(genreId)))
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                    {
                        ErrorMessageConstants.BOOK_PRODUCT_GENRE,
                        MessageConstants.MESSAGE_NOT_BELONGING,
                        ErrorMessageConstants.CAMPAIGN_COMMISSION_GENRE,
                    });
            }
        }

        private void CheckBookComboProductGenre(BookProduct bookProduct, Campaign _campaign, ref Genre genre)
        {
            var genreId = bookProduct.GenreId;
            if (genreId == null)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.PreconditionRequired), new string[]
                {
                    MessageConstants.MESSAGE_REQUIRED,
                    ErrorMessageConstants.BOOK_PRODUCT_GENRE
                });
            }
            genre = _unitOfWork.Genres.Get(g => g.Id.Equals(genreId))
            .Include(g => g.InverseParent)
            .SingleOrDefault();
            if (genre == null)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.BOOK_PRODUCT_GENRE
                });
            }
            if (genre.ParentId.HasValue)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                {
                    ErrorMessageConstants.BOOK_PRODUCT_COMBO_INVALID_GENRE
                });
            }
            var _book = new Book()
            {
                Genre = genre
            };
            CheckBookProductGenre(_book, _campaign);
            CheckBookProductCommission(bookProduct, _book, _campaign);
        }

        private void CheckBookProductCommission(BookProduct bookProduct, Book _book, Campaign _campaign)
        {
            var commission = bookProduct.Commission;
            if (commission <= 0)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                {
                    ErrorMessageConstants.BOOK_PRODUCT_COMMISSION,
                    MessageConstants.MESSAGE_INVALID
                });
            }
            var genreId = _book.Genre.ParentId ?? _book.Genre.Id;
            var _commission = _campaign.CampaignCommissions.SingleOrDefault(cc => cc.GenreId.Equals(genreId));
            if (_commission != null)
            {
                if (commission < _commission.MinimalCommission)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                    {
                        ErrorMessageConstants.BOOK_PRODUCT_COMMISSION,
                        MessageConstants.MESSAGE_INVALID
                    });
            }
        }
        private void CheckParticipant(int? CampaignId)
        {
            var issuerId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            var participants = _unitOfWork.Participants
            .Get(p => p.CampaignId.Equals(CampaignId) && p.IssuerId.Equals(issuerId));
            if (!participants.Any())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]{
                    ErrorMessageConstants.UNATTENDED_PARTICIPANT
                });
            participants = participants.OrderByDescending(p => p.UpdatedDate);
            var ValidList = new List<byte>()
            {
                (byte)ParticipantStatus.Accepted,
                (byte)ParticipantStatus.Approved
            };
            var IsApprovedOrAccepted = participants.Any(p => ValidList.Contains(p.Status));
            if (!IsApprovedOrAccepted)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]{
                    ErrorMessageConstants.PARTICIPANT_STATUS_OF,
                    ErrorMessageConstants.PARTICIPANT.ToLower(),
                    MessageConstants.MESSAGE_INVALID
                });
        }
        private void CheckFormat(Book book, BookProduct bookProduct, byte? Format, Campaign campaign)
        {
            switch (Format)
            {
                case (byte)BookFormat.PrintBook:
                    CheckPrintBook(book, bookProduct);
                    break;
                case (byte)BookFormat.Audio:
                    CheckBookProductFormatByCampaign(Format, campaign);
                    if (!book.AudioExtraPrice.HasValue && string.IsNullOrEmpty(book.AudioTrialUrl))
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                    {
                        ErrorMessageConstants.BOOK_PRODUCT_FORMAT,
                        MessageConstants.MESSAGE_INVALID,
                        ErrorMessageConstants.BOOK_PRODUCT_FORMAT_AUDIO
                    });
                    if (!bookProduct.DisplayAudioIndex.HasValue)
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.PreconditionRequired), new string[]
                        {
                            MessageConstants.MESSAGE_REQUIRED,
                            ErrorMessageConstants.BOOK_PRODUCT_FORMAT_AUDIO_INDEX,
                        });
                    break;
                case (byte)BookFormat.Pdf:
                    CheckBookProductFormatByCampaign(Format, campaign);
                    if (!book.PdfExtraPrice.HasValue && string.IsNullOrEmpty(book.PdfTrialUrl))
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                        {
                            ErrorMessageConstants.BOOK_PRODUCT_FORMAT,
                            MessageConstants.MESSAGE_INVALID,
                            ErrorMessageConstants.BOOK_PRODUCT_FORMAT_PDF
                        });
                    if (!bookProduct.DisplayPdfIndex.HasValue)
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.PreconditionRequired), new string[]
                        {
                            MessageConstants.MESSAGE_REQUIRED,
                            ErrorMessageConstants.BOOK_PRODUCT_FORMAT_PDF_INDEX,
                        });
                    break;
                default:
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                    {
                        ErrorMessageConstants.BOOK_PRODUCT_FORMAT,
                        MessageConstants.MESSAGE_INVALID,
                    });
                    break;
            }
        }
        private void CheckDiscount(BookProduct bookProduct)
        {
            if (!bookProduct.Discount.HasValue)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.PreconditionRequired), new string[]
                        {
                        MessageConstants.MESSAGE_REQUIRED,
                        ErrorMessageConstants.BOOK_PRODUCT_DISCOUNT
                        });
            if (bookProduct.Discount < 0)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                    {
                        ErrorMessageConstants.BOOK_PRODUCT_DISCOUNT,
                        MessageConstants.MESSAGE_INVALID
                    });
        }

        private void CheckBookProductStatus(BookProduct bookProduct, bool IsCreate = true, BookProduct _bookProduct = null)
        {
            if (!IsCreate)
            {
                if (_bookProduct == null)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.PreconditionRequired), new string[]
                         {
                        MessageConstants.MESSAGE_REQUIRED,
                        ErrorMessageConstants.BOOK_PRODUCT
                         });
                var validStatus = new List<byte?>()
                    {
                        (byte) BookProductStatus.Sale,
                        (byte) BookProductStatus.NotSale,
                        (byte) BookProductStatus.OutOfStock
                    };
                if (!bookProduct.Status.Equals(_bookProduct.Status))
                {
                    if (!validStatus.Contains(bookProduct.Status))
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                        {
                            ErrorMessageConstants.BOOK_PRODUCT_STATUS,
                            MessageConstants.MESSAGE_INVALID
                        });
                }
            }
        }

        private void CheckPrintBook(Book book, BookProduct bookProduct)
        {
            if ((bool)bookProduct.WithAudio)
            {
                if (!bookProduct.DisplayAudioIndex.HasValue)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.PreconditionRequired), new string[]
                    {
                            MessageConstants.MESSAGE_REQUIRED,
                            ErrorMessageConstants.BOOK_PRODUCT_FORMAT_AUDIO_INDEX,
                    });
                if (!book.AudioExtraPrice.HasValue && string.IsNullOrEmpty(book.AudioTrialUrl))
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                {
                        ErrorMessageConstants.BOOK_PRODUCT_FORMAT,
                        MessageConstants.MESSAGE_INVALID,
                        ErrorMessageConstants.BOOK_PRODUCT_FORMAT_AUDIO
                });
            }
            if ((bool)bookProduct.WithPdf)
            {
                if (!bookProduct.DisplayPdfIndex.HasValue)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.PreconditionRequired), new string[]
                    {
                            MessageConstants.MESSAGE_REQUIRED,
                            ErrorMessageConstants.BOOK_PRODUCT_FORMAT_PDF_INDEX,
                    });
                if (!book.PdfExtraPrice.HasValue && string.IsNullOrEmpty(book.PdfTrialUrl))
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                    {
                            ErrorMessageConstants.BOOK_PRODUCT_FORMAT,
                            MessageConstants.MESSAGE_INVALID,
                            ErrorMessageConstants.BOOK_PRODUCT_FORMAT_PDF
                    });
            }
            if ((bool)bookProduct.WithAudio && (bool)bookProduct.WithPdf)
            {
                if (bookProduct.DisplayAudioIndex.Equals(bookProduct.DisplayPdfIndex))
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    MessageConstants.MESSAGE_DUPLICATED_INFO,
                    ErrorMessageConstants.BOOK_PRODUCT_FORMAT_INDEX
                });
            }
        }

        private void CheckBookProductFormatByCampaign(byte? Format, Campaign campaign)
        {
            if (Format.Equals((byte)BookFormat.Audio) ||
            Format.Equals((byte)BookFormat.Pdf))
            {
                if (campaign.Format.Equals((byte)CampaignFormat.Offline))
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                    {
                        ErrorMessageConstants.BOOK_PRODUCT_FORMAT,
                        MessageConstants.MESSAGE_INVALID,
                        ErrorMessageConstants.CAMPAIGN_FORMAT_OFFLINE
                    });
            }
        }

        private void CheckDate(DateTime startDate, DateTime endDate, bool IsCreate = true)
        {
            if (DateTime.Compare(endDate, DateTime.Now) <= 0)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[] {
                        IsCreate ? ErrorMessageConstants.INSERT :
                        ErrorMessageConstants.UPDATE,
                        ErrorMessageConstants.BOOK_PRODUCT.ToLower(),
                        MessageConstants.MESSAGE_FAILED,
                        ErrorMessageConstants.CAMPAIGN_ENDED
                        });
            if (DateTime.Compare(startDate, DateTime.Now) <= 0)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[] {
                        IsCreate ? ErrorMessageConstants.INSERT :
                        ErrorMessageConstants.UPDATE,
                        ErrorMessageConstants.BOOK_PRODUCT.ToLower(),
                        MessageConstants.MESSAGE_FAILED,
                        ErrorMessageConstants.CAMPAIGN_STARTED
                        });
        }
        private void CheckBookProductItemFormat(Book book, BookProductItem bookProductItem, byte? Format, Campaign campaign)
        {
            switch (Format)
            {
                case (byte)BookFormat.PrintBook:
                    CheckPrintBook(book, bookProductItem);
                    break;
                case (byte)BookFormat.Audio:
                    CheckBookProductFormatByCampaign(Format, campaign);
                    if (!book.AudioExtraPrice.HasValue && string.IsNullOrEmpty(book.AudioTrialUrl))
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                    {
                        ErrorMessageConstants.BOOK_PRODUCT_FORMAT,
                        MessageConstants.MESSAGE_INVALID,
                        ErrorMessageConstants.BOOK_PRODUCT_FORMAT_AUDIO
                    });
                    if (!bookProductItem.DisplayAudioIndex.HasValue)
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.PreconditionRequired), new string[]
                        {
                            MessageConstants.MESSAGE_REQUIRED,
                            ErrorMessageConstants.BOOK_PRODUCT_FORMAT_AUDIO_INDEX,
                        });
                    break;
                case (byte)BookFormat.Pdf:
                    CheckBookProductFormatByCampaign(Format, campaign);
                    if (!book.PdfExtraPrice.HasValue && string.IsNullOrEmpty(book.PdfTrialUrl))
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                        {
                            ErrorMessageConstants.BOOK_PRODUCT_FORMAT,
                            MessageConstants.MESSAGE_INVALID,
                            ErrorMessageConstants.BOOK_PRODUCT_FORMAT_PDF
                        });
                    if (!bookProductItem.DisplayPdfIndex.HasValue)
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.PreconditionRequired), new string[]
                        {
                            MessageConstants.MESSAGE_REQUIRED,
                            ErrorMessageConstants.BOOK_PRODUCT_FORMAT_PDF_INDEX,
                        });
                    break;
                default:
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                    {
                        ErrorMessageConstants.BOOK_PRODUCT_FORMAT,
                        MessageConstants.MESSAGE_INVALID,
                    });
                    break;
            }
        }
        private void CheckPrintBook(Book book, BookProductItem bookProductItem)
        {
            if ((bool)bookProductItem.WithAudio)
            {
                if (!bookProductItem.DisplayAudioIndex.HasValue)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.PreconditionRequired), new string[]
                    {
                            MessageConstants.MESSAGE_REQUIRED,
                            ErrorMessageConstants.BOOK_PRODUCT_FORMAT_AUDIO_INDEX,
                    });
                if (!book.AudioExtraPrice.HasValue && string.IsNullOrEmpty(book.AudioTrialUrl))
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                {
                        ErrorMessageConstants.BOOK_PRODUCT_FORMAT,
                        MessageConstants.MESSAGE_INVALID,
                        ErrorMessageConstants.BOOK_PRODUCT_FORMAT_AUDIO
                });
            }
            if ((bool)bookProductItem.WithPdf)
            {
                if (!bookProductItem.DisplayPdfIndex.HasValue)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.PreconditionRequired), new string[]
                    {
                            MessageConstants.MESSAGE_REQUIRED,
                            ErrorMessageConstants.BOOK_PRODUCT_FORMAT_PDF_INDEX,
                    });
                if (!book.PdfExtraPrice.HasValue && string.IsNullOrEmpty(book.PdfTrialUrl))
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                    {
                            ErrorMessageConstants.BOOK_PRODUCT_FORMAT,
                            MessageConstants.MESSAGE_INVALID,
                            ErrorMessageConstants.BOOK_PRODUCT_FORMAT_PDF
                    });
            }
            if ((bool)bookProductItem.WithAudio && (bool)bookProductItem.WithPdf)
            {
                if (bookProductItem.DisplayAudioIndex.Equals(bookProductItem.DisplayPdfIndex))
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    MessageConstants.MESSAGE_DUPLICATED_INFO,
                    ErrorMessageConstants.BOOK_PRODUCT_FORMAT_INDEX
                });
            }
        }
        private void CheckComboTitle(string comboTitle, int campaignId, Guid? bookProductId = null, bool IsCreate = true)
        {
            var bookProduct = _unitOfWork.BookProducts.CheckDuplicatedComboName(comboTitle, campaignId);
            if (IsCreate)
            {
                if (bookProduct != null)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                    {
                        MessageConstants.MESSAGE_DUPLICATED_INFO,
                        ErrorMessageConstants.BOOK_PRODUCT_COMBO_TITLE
                    });
            }
            else
            {
                if (bookProduct != null)
                {
                    if (!bookProduct.Id.Equals(bookProductId))
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                        {
                            MessageConstants.MESSAGE_DUPLICATED_INFO,
                            ErrorMessageConstants.BOOK_PRODUCT_COMBO_TITLE
                        });
                }
            }
        }
        private void GetOddOrSeriesBookProduct(ref BookProduct bookProduct, Book _book, BookType Type, bool IsCreate = true, BookProduct _bookProduct = null)
        {
            if (Type.Equals(BookType.Odd))
            {
                GetFormat(ref bookProduct, _book);
                bookProduct.Book = _book;
            }
            if (IsCreate)
            {
                bookProduct.Id = Guid.NewGuid();
                bookProduct.SalePrice = ServiceUtils.GetSalePrice(_book.CoverPrice, bookProduct.Discount);
                bookProduct.CreatedDate = DateTime.Now;
            }
            else
            {
                bookProduct.UpdatedDate = DateTime.Now;
                bookProduct.BookId = _bookProduct.BookId;
                bookProduct.CampaignId = _bookProduct.CampaignId;
                var _discount = bookProduct.Discount;
                if (!_bookProduct.Discount.Equals(_discount))
                    bookProduct.SalePrice = ServiceUtils.GetSalePrice(_book.CoverPrice, _discount);
                else
                    bookProduct.SalePrice = _bookProduct.SalePrice;
            }
            bookProduct.Status = (byte)BookProductStatus.Pending;
            bookProduct.IssuerId = _book.IssuerId;
            bookProduct.GenreId = _book.GenreId;
            bookProduct.Title = _book.Name;
            bookProduct.Description = _book.Description;
            bookProduct.Type = (byte)Type;
            bookProduct.ImageUrl = _book.ImageUrl;
        }
        private void GetComboBookProduct(ref BookProduct bookProduct, BookType Type, bool IsCreate = true, BookProduct _bookProduct = null)
        {
            if (IsCreate)
            {
                bookProduct.Id = Guid.NewGuid();
                bookProduct.CreatedDate = DateTime.Now;
            }
            else
            {
                bookProduct.UpdatedDate = DateTime.Now;
                bookProduct.IssuerId = _bookProduct.IssuerId;
                bookProduct.CampaignId = _bookProduct.CampaignId;
                if (String.IsNullOrEmpty(bookProduct.ImageUrl))
                    bookProduct.ImageUrl = _bookProduct.ImageUrl;
            }
            bookProduct.Status = (byte)BookProductStatus.Pending;
            bookProduct.Type = (byte)Type;
        }

        private void GetBookProductItems(ref List<BookProductItem> bookProductItems, Genre genre, BookType type, int? bookId = null, Campaign _campaign = null)
        {
            var validTypes = new List<BookType>()
            {
                BookType.Combo,
                BookType.Series
            };
            if (!validTypes.Contains(type))
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                    {
                        ErrorMessageConstants.BOOK_PRODUCT_TYPE,
                        MessageConstants.MESSAGE_INVALID,
                    });
            }
            CheckBookProductItems(bookProductItems, genre, type, bookId, _campaign);
            GetFormats(ref bookProductItems);
        }
        private void CheckBookProductItems(List<BookProductItem> bookProductItems, Genre genre, BookType Type, int? bookId = null, Campaign _campaign = null)
        {
            #region General
            //Check book product items is null
            if (!bookProductItems.Any())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.PreconditionRequired), new string[] {
                        MessageConstants.MESSAGE_REQUIRED,
                        ErrorMessageConstants.BOOK_PRODUCT_ITEM.ToLower(),
                        ErrorMessageConstants.BOOK_PRODUCT.ToLower()
                    });
            //Check the amount of book product items
            if (bookProductItems.Count < 2)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[] {
                        ErrorMessageConstants.BOOK_PRODUCT_ITEM.ToLower(),
                        ErrorMessageConstants.BOOK_PRODUCT.ToLower(),
                        ErrorMessageConstants.BOOK_PRODUCT_ITEM_INVALID_AMOUNT
                    });
            //Check duplicated display index of book product items
            var duplicates = bookProductItems.GroupBy(bpis => bpis.DisplayIndex).ToList();
            duplicates.ForEach(d =>
            {
                if (d.Count() > 1)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    MessageConstants.MESSAGE_DUPLICATED_INFO,
                    ErrorMessageConstants.BOOK_ITEM_DISPLAY_INDEX.ToLower(),
                    ErrorMessageConstants.BOOK_PRODUCT.ToLower()
                });
            });
            //Check existed book items
            bookProductItems.ForEach(bpis =>
            {
                var bookProductItem = _unitOfWork.Books
                .Get(b => b.Id.Equals(bpis.BookId) && !(bool)b.IsSeries)
                .SingleOrDefault();
                if (bookProductItem == null)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                    {
                        ErrorMessageConstants.BOOK_ID,
                        MessageConstants.MESSAGE_INVALID,
                        $"[Book Product Item Id: {bpis.BookId}]"
                    });
            });
            #endregion

            switch (Type)
            {
                case BookType.Series:
                    if (bookId == null)
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                        {
                            ErrorMessageConstants.BOOK_ID,
                            MessageConstants.MESSAGE_INVALID,
                        });
                    //Check book items
                    var _bookItems = _unitOfWork.BookItems.Get(bi => bi.ParentBookId.Equals(bookId))
                    .Include(bib => bib.Book).ToList();
                    if (!_bookItems.Any())
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                        {
                            ErrorMessageConstants.NOT_FOUND,
                            ErrorMessageConstants.BOOK_PRODUCT_ITEM.ToLower(),
                            ErrorMessageConstants.BOOK_SERIES.ToLower()
                        });
                    //Check amount of book product items and book items
                    var bookItemsIds = _bookItems.Select(bis => bis.BookId);
                    var IsValidAmounts = bookProductItems.Any(bpi => bookItemsIds.Contains(bpi.BookId));
                    if (!IsValidAmounts)
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                        {
                            ErrorMessageConstants.BOOK_PRODUCT_ITEM_AMOUNT,
                            ErrorMessageConstants.BOOK_PRODUCT.ToLower(),
                            MessageConstants.MESSAGE_INVALID
                        });
                    bookProductItems.ForEach(bpis =>
                    {
                        var _book = _bookItems.SingleOrDefault(b => b.BookId.Equals(bpis.BookId));
                        if (_book != null)
                            CheckBookProductItemFormat(_book.Book, bpis, bpis.Format, _campaign);
                    });
                    break;
                case BookType.Combo:
                    bookProductItems.ForEach(bpis =>
                    {
                        var _book = _unitOfWork.Books.Get(b => b.Id.Equals(bpis.BookId))
                        .Include(b => b.Genre)
                        .SingleOrDefault();
                        if (_book == null)
                            BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[] {
                            ErrorMessageConstants.NOT_FOUND,
                            ErrorMessageConstants.BOOK_PRODUCT_ITEM_ID.ToLower(),
                            ErrorMessageConstants.BOOK_PRODUCT.ToLower()
                        });
                        if (!genre.InverseParent.Any(ip => ip.Id.Equals(_book.GenreId)))
                            BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[] {
                            ErrorMessageConstants.BOOK_PRODUCT_ITEM_GENRE,
                            ErrorMessageConstants.BOOK_PRODUCT.ToLower(),
                            MessageConstants.MESSAGE_INVALID
                        });
                        CheckBookProductGenre(_book, _campaign);
                        CheckBookProductItemFormat(_book, bpis, bpis.Format, _campaign);
                    });
                    break;
            }
        }
        private void GetFormat(ref BookProduct bookProduct, Book _book)
        {
            if ((bool)bookProduct.WithPdf)
                bookProduct.PdfExtraPrice = _book.PdfExtraPrice;
            else
                bookProduct.PdfExtraPrice = null;
            if ((bool)bookProduct.WithAudio)
                bookProduct.AudioExtraPrice = _book.AudioExtraPrice;
            else
                bookProduct.AudioExtraPrice = null;
        }
        private void GetFormats(ref List<BookProductItem> bookProductItems)
        {
            bookProductItems.ForEach(bpis =>
            {
                var _book = _unitOfWork.Books.Get(bpis.BookId);
                if (_book != null)
                {
                    if ((bool)bpis.WithPdf)
                        bpis.PdfExtraPrice = _book.PdfExtraPrice;
                    else
                        bpis.PdfExtraPrice = null;
                    if ((bool)bpis.WithAudio)
                        bpis.AudioExtraPrice = _book.AudioExtraPrice;
                    else
                        bpis.AudioExtraPrice = null;
                }
            });
        }
        private BookProduct ConvertToBookProduct(BookProduct updateBookProduct)
        {
            var _temp = _unitOfWork.BookProducts.Get(updateBookProduct.Id);
            var _response = _mapper.Map<BookProductViewModel>(updateBookProduct);
            _mapper.Map(_response, _temp);
            return _temp;
        }

        private List<BookItem> GetSeriesBookItems(int? BookId)
        {
            var list = _unitOfWork.BookItems.Get(b => b.ParentBookId.Equals(BookId)).OrderBy(b => b.DisplayIndex).ToList();
            var bookItemIds = new List<BookItem>();
            list.ForEach(bis =>
            {
                bookItemIds.Add(new BookItem()
                {
                    Id = bis.Id,
                    BookId = bis.BookId,
                    ParentBookId = bis.ParentBookId,
                    DisplayIndex = bis.DisplayIndex
                });
            });
            return bookItemIds;
        }

        private void UpdateSeriesBookItems(int? BookId, List<BookItem> bookItems)
        {
            var list = _unitOfWork.BookItems.Get(b => b.ParentBookId.Equals(BookId)).ToList();
            list.ForEach(bis =>
            {
                var temp = bookItems.SingleOrDefault(b => b.Id.Equals(bis.Id));
                if (temp != null)
                    bis.BookId = temp.BookId;
            });
            _unitOfWork.BookItems.UpdateRange(list);
            if (!_unitOfWork.Save())
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.UPDATE,
                    ErrorMessageConstants.BOOK_ITEM.ToLower(),
                    ErrorMessageConstants.BOOK_SERIES.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            }
        }

        private BookProduct CheckPendingBookProduct(BookProduct bookProduct, string updateNote)
        {
            bookProduct = _unitOfWork.BookProducts.Get(bp => bp.Id.Equals(bookProduct.Id))
            .Include(bp => bp.Campaign)
            .SingleOrDefault();
            if (bookProduct == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.BOOK_PRODUCT.ToLower(),
                });

            var status = (byte)BookProductStatus.Pending;
            var StatusName = StatusExtension<BookProductStatus>.GetStringStatus(status);
            if (!bookProduct.Status.Equals(status))
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.UPDATE,
                    ErrorMessageConstants.BOOK_PRODUCT.ToLower(),
                    MessageConstants.MESSAGE_FAILED,
                    ErrorMessageConstants.BOOK_PRODUCT_INVALID_STATUS,
                    StatusName.ToLower()
                });

            CheckDate((DateTime)bookProduct.Campaign.StartDate, (DateTime)bookProduct.Campaign.EndDate, false);
            bookProduct.UpdatedDate = DateTime.Now;
            bookProduct.Note = ServiceUtils.GetUpdateNote(updateNote, bookProduct.Note, _httpContextAccessor, ClaimTypes.Role, true);
            return bookProduct;
        }

        private BookProduct CheckUpdateBookProductDetailOfStartCampaign(BookProduct bookProduct)
        {
            bookProduct = _unitOfWork.BookProducts.Get(bp => bp.Id.Equals(bookProduct.Id))
            .Include(bp => bp.Campaign)
            .SingleOrDefault();
            if (bookProduct == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.BOOK_PRODUCT.ToLower(),
                });
            //Check if campaign starts
            var DateTimeNow = DateTime.Now;
            if (!(DateTime.Compare((DateTime)bookProduct.Campaign.StartDate, DateTimeNow) <= 0 &&
            DateTime.Compare((DateTime)bookProduct.Campaign.EndDate, DateTimeNow) > 0))
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.UPDATE,
                    ErrorMessageConstants.BOOK_PRODUCT.ToLower(),
                    MessageConstants.MESSAGE_FAILED,
                    ErrorMessageConstants.CAMPAIGN_NOT_STARTED,
                });

            //Check invalid status of campaign
            var invalidStatus = new List<byte?>()
            {
                (byte) CampaignStatus.Cancelled,
                (byte) CampaignStatus.Postponed
            };
            CheckInvalidCampaignStatus(bookProduct.Campaign.Status, invalidStatus);

            //Check sale quantity
            CheckSaleQuantity(bookProduct.SaleQuantity, false);

            //Check valid status of book product
            var validStatus = new List<byte?>()
            {
                (byte) BookProductStatus.Sale,
                (byte) BookProductStatus.OutOfStock,
                (byte) BookProductStatus.NotSale
            };
            if (!validStatus.Contains(bookProduct.Status))
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                    {
                        ErrorMessageConstants.BOOK_PRODUCT_STATUS,
                        MessageConstants.MESSAGE_INVALID
                    });
            //Check issuer
            CheckIssuer(bookProduct.IssuerId);
            CheckIssuerStatus();

            return bookProduct;
        }

        private void CheckInvalidCampaignStatus(byte? status, List<byte?> invalidStatus)
        {
            if (invalidStatus.Contains(status))
            {
                var StatusName = StatusExtension<CampaignStatus>.GetStatus(status);
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.UPDATE,
                    ErrorMessageConstants.BOOK_PRODUCT.ToLower(),
                    MessageConstants.MESSAGE_FAILED,
                    ErrorMessageConstants.CAMPAIGN_INVALID_STATUS,
                    StatusName.ToLower(),
                });
            }
        }
        #endregion
    }
}