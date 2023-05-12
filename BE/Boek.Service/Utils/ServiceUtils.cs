using System.Security.Claims;
using Boek.Core.Constants;
using Boek.Core.Entities;
using Boek.Core.Extensions;
using Boek.Infrastructure.ViewModels.BookProductItems;
using Boek.Infrastructure.ViewModels.BookProducts;
using Boek.Infrastructure.ViewModels.BookProducts.Mobile;
using Boek.Infrastructure.ViewModels.Books;
using Boek.Infrastructure.ViewModels.Campaigns;
using Boek.Infrastructure.ViewModels.Campaigns.Mobile;
using Boek.Infrastructure.ViewModels.CampaignStaffs;
using Boek.Infrastructure.ViewModels.CustomerGroups;
using Boek.Infrastructure.ViewModels.Genres;
using Boek.Infrastructure.ViewModels.Groups;
using Boek.Infrastructure.ViewModels.Levels;
using Boek.Infrastructure.ViewModels.Participants;
using Boek.Infrastructure.ViewModels.Users;
using Boek.Service.Commons;
using Boek.Core.Enums;
using Microsoft.AspNetCore.Http;
using Boek.Infrastructure.ViewModels.CampaignOrganizations;
using Boek.Infrastructure.Requests.Addresses;
using Boek.Infrastructure.ViewModels.Campaigns.Mobile.Customers;
using Boek.Infrastructure.ViewModels.Schedules;
using Boek.Infrastructure.ViewModels.Addresses;
using Boek.Infrastructure.ViewModels.Orders;
using Boek.Infrastructure.ViewModels.OrderDetails;
using Boek.Infrastructure.ViewModels.Organizations.Mobile;
using System.Net;
using System.Text.Json;
using Boek.Infrastructure.ViewModels.Books.Issuers;
using Boek.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Boek.Infrastructure.ViewModels.Orders.Calculation;
using Boek.Infrastructure.ViewModels.OrganizationMembers;

namespace Boek.Service.Utils
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
    public static class ServiceUtils
    {
        public static Guid? GetUserInfo(IHttpContextAccessor _httpContextAccessor)
        {
            Guid? UserId = new Guid(_httpContextAccessor.HttpContext?.Items["UserId"]?.ToString());
            if (UserId == null || UserId == Guid.Empty)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Unauthorized), new string[]
                {
                    ErrorMessageConstants.USER,
                    MessageConstants.MESSAGE_UNAUTHENTICATION
                });
            return UserId;
        }
        public static bool IsAuthorize(IHttpContextAccessor _httpContextAccessor)
        {
            var result = GetUserInfoValue(_httpContextAccessor, ClaimTypes.Role);
            return !string.IsNullOrEmpty(result);
        }

        public static bool CheckRole(IHttpContextAccessor _httpContextAccessor, BoekRole role)
        {
            var result = GetUserInfoValue(_httpContextAccessor, ClaimTypes.Role);
            if (!string.IsNullOrEmpty(result))
                return result.Equals(role.ToEnumMemberAttrValue());
            return false;
        }
        public static string GetUserInfoValue(IHttpContextAccessor _httpContextAccessor, string claimTypes = ClaimTypes.NameIdentifier)
        {
            try
            {
                var result = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type.Equals(claimTypes)).Value;
                return result;
            }
            catch
            {
                return null;
            }
        }

        public static decimal GetSalePrice(decimal CoverPrice, int? Discount)
        {
            if (Discount > 0 && Discount.HasValue)
            {
                var _discount = 1 - (decimal)Discount / 100;
                var result = (decimal)CoverPrice * _discount;
                return result;
            }
            return CoverPrice;
        }

        public static UserViewModel GetResponseDetail(UserViewModel response, bool WithAddressDetail = false, bool ShowErrorMessage = true)
        {
            if (response != null)
            {
                response.RoleName = StatusExtension<BoekRole>.GetStatus(response.Role, ShowErrorMessage);
                response.StatusName = StatusExtension<BoekStatus>.GetStatus(response.Status, ShowErrorMessage);
                if (WithAddressDetail)
                    response.AddressViewModel = GetResponseDetail(response.Address);
            }
            return response;
        }

        public static CampaignOrganizationViewModel GetResponseDetail(CampaignOrganizationViewModel response, bool WithAddressDetail = false)
        {
            response.Schedules = GetResponseDetails(response.Schedules, WithAddressDetail);
            if (response.Campaign != null)
                response.Campaign = GetCampaignDetail(response.Campaign, WithAddressDetail);
            return response;
        }

        public static List<SchedulesViewModel> GetResponseDetails(List<SchedulesViewModel> response, bool WithAddressDetail = false)
        {
            if (response != null)
            {
                if (response.Any())
                    response.ForEach(coa => coa = GetResponseDetail(coa, WithAddressDetail));
            }
            return response;
        }

        public static CampaignSchedulesViewModel GetResponseDetail(CampaignSchedulesViewModel response)
        {
            response.FormatName = StatusExtension<CampaignFormat>.GetStatus(response.Format);
            response.StatusName = StatusExtension<CampaignStatus>.GetStatus(response.Status);
            if (response.Schedules != null)
            {
                if (response.Schedules.Any())
                    response.Schedules.ForEach(s => s = GetResponseDetail(s));
            }
            return response;
        }
        public static CampaignViewModel GetResponseDetail(CampaignViewModel _response, bool WithAddressDetail = false)
        {
            _response.FormatName = StatusExtension<CampaignFormat>.GetStatus(_response.Format);
            _response.StatusName = StatusExtension<CampaignStatus>.GetStatus(_response.Status);
            if (WithAddressDetail)
                _response.AddressViewModel = GetResponseDetail(_response.Address);
            if (_response.CampaignOrganizations != null)
            {
                if (_response.CampaignOrganizations.Any())
                    _response.CampaignOrganizations.ForEach(cc => cc = GetResponseDetail(cc, WithAddressDetail));
            }
            if (_response.CampaignCommissions != null)
            {
                if (_response.CampaignCommissions.Any())
                    _response.CampaignCommissions.ForEach(cc =>
                    {
                        if (cc.Genre != null)
                            cc.Genre.StatusName = StatusExtension<BoekStatus>.GetStatus(cc.Genre.Status);
                    });
            }
            if (_response.CampaignGroups != null)
            {
                if (_response.CampaignGroups.Any())
                {
                    _response.CampaignGroups.ForEach(cg =>
                    {
                        if (cg.Group != null)
                            cg.Group = GetResponseDetail(cg.Group);
                    });
                }
            }
            if (_response.Participants != null)
            {
                if (_response.Participants.Any())
                {
                    _response.Participants.ForEach(p =>
                    {
                        p.StatusName = StatusExtension<ParticipantStatus>.GetStatus(p.Status);
                        if (p.Issuer != null)
                        {
                            p.Issuer.User = GetResponseDetail(p.Issuer.User);
                        }
                    });
                }
            }
            if (_response.CampaignLevels != null)
            {
                if (_response.CampaignLevels.Any())
                {
                    _response.CampaignLevels.ForEach(cls =>
                    {
                        if (cls.Level != null)
                            cls.Level = GetResponseDetail(cls.Level);
                    });
                }
            }
            return _response;
        }

        public static CampaignParticipantsViewModel GetResponseDetail(CampaignParticipantsViewModel _response, bool WithAddressDetail = false)
        {
            _response.FormatName = StatusExtension<CampaignFormat>.GetStatus(_response.Format);
            _response.StatusName = StatusExtension<CampaignStatus>.GetStatus(_response.Status);
            if (WithAddressDetail)
                _response.AddressViewModel = GetResponseDetail(_response.Address);
            if (_response.Participants != null)
            {
                if (_response.Participants.Any())
                {
                    _response.Participants.ForEach(p =>
                    {
                        p.StatusName = StatusExtension<ParticipantStatus>.GetStatus(p.Status);
                        if (p.Issuer != null)
                        {
                            p.Issuer.User = GetResponseDetail(p.Issuer.User);
                        }
                    });
                }
            }
            return _response;
        }

        public static BasicCampaignViewModel GetCampaignDetail(BasicCampaignViewModel Campaign, bool WithAddressDetail = false)
        {
            Campaign.FormatName = StatusExtension<CampaignFormat>.GetStatus(Campaign.Format);
            Campaign.StatusName = StatusExtension<CampaignStatus>.GetStatus(Campaign.Status);
            if (WithAddressDetail)
                Campaign.AddressViewModel = GetResponseDetail(Campaign.Address);
            return Campaign;
        }
        public static StaffCampaignMobilesViewModel GetCampaignDetail(StaffCampaignMobilesViewModel Campaign)
        {
            Campaign.FormatName = StatusExtension<CampaignFormat>.GetStatus(Campaign.Format);
            Campaign.StatusName = StatusExtension<CampaignStatus>.GetStatus(Campaign.Status);
            if (Campaign.Issuers != null)
            {
                if (Campaign.Issuers.Any())
                    Campaign.Issuers.ForEach(i => i.User = GetResponseDetail(i.User));
            }
            if (Campaign.CampaignStaffs != null)
            {
                if (Campaign.CampaignStaffs.Any())
                {
                    Campaign.CampaignStaffs.ForEach(cs =>
                    {
                        cs.StatusName = StatusExtension<CampaignStaffStatus>.GetStatus(cs.Status);
                        if (cs.Staff != null)
                            cs.Staff = GetResponseDetail(cs.Staff);
                    });
                }
            }
            return Campaign;
        }

        public static CampaignMobileViewModel GetCampaignDetail(CampaignMobileViewModel Campaign, bool WithAddressDetail = false)
        {
            Campaign.FormatName = StatusExtension<CampaignFormat>.GetStatus(Campaign.Format);
            Campaign.StatusName = StatusExtension<CampaignStatus>.GetStatus(Campaign.Status);
            if (Campaign.Organizations != null)
            {
                if (Campaign.Organizations.Any())
                    Campaign.Organizations.ForEach(o => o = GetResponseDetail(o, WithAddressDetail));
            }
            if (Campaign.Issuers != null)
            {
                if (Campaign.Issuers.Any())
                    Campaign.Issuers.ForEach(i => i.User = GetResponseDetail(i.User));
            }
            Campaign.HierarchicalBookProducts = GetResponseDetails(Campaign.HierarchicalBookProducts);
            Campaign.UnhierarchicalBookProducts = GetResponseDetails(Campaign.UnhierarchicalBookProducts);
            if (Campaign.Levels != null)
            {
                if (Campaign.Levels.Any())
                    Campaign.Levels.ForEach(l => l = GetResponseDetail(l));
            }
            return Campaign;
        }

        public static List<HierarchicalBookProductsViewModel> GetResponseDetails(List<HierarchicalBookProductsViewModel> list)
        {
            if (list != null)
            {
                if (list.Any())
                    list.ForEach(u => u = GetResponseDetail(u));
            }
            return list;
        }

        public static HierarchicalBookProductsViewModel GetResponseDetail(HierarchicalBookProductsViewModel response)
        {
            if (response.subHierarchicalBookProducts != null)
            {
                if (response.subHierarchicalBookProducts.Any())
                    response.subHierarchicalBookProducts = GetResponseDetails(response.subHierarchicalBookProducts);
            }
            return response;
        }
        public static List<SubHierarchicalBookProductsViewModel> GetResponseDetails(List<SubHierarchicalBookProductsViewModel> list)
        {
            if (list != null)
            {
                //cSpell:disable
                if (list.Any())
                    list.ForEach(shbps => shbps = GetResponseDetail(shbps));
            }
            return list;
        }

        public static SubHierarchicalBookProductsViewModel GetResponseDetail(SubHierarchicalBookProductsViewModel response)
        {
            if (response.Genre != null)
                response.Genre = GetResponseDetail(response.Genre);
            if (response.Issuer != null)
                response.Issuer = GetResponseDetail(response.Issuer);
            if (response.BookProducts.Any())
                response.BookProducts.ForEach(bp => bp = GetResponseDetail(bp));
            return response;
        }
        public static List<UnhierarchicalBookProductsViewModel> GetResponseDetails(List<UnhierarchicalBookProductsViewModel> list)
        {
            if (list != null)
            {
                if (list.Any())
                    list.ForEach(u => u = GetResponseDetail(u));
            }
            return list;
        }
        public static UnhierarchicalBookProductsViewModel GetResponseDetail(UnhierarchicalBookProductsViewModel response)
        {

            if (response.BookProducts.Any())
                response.BookProducts.ForEach(bp => bp = GetResponseDetail(bp));
            return response;
        }

        public static CustomerCampaignMobileViewModel GetResponseDetail(CustomerCampaignMobileViewModel response)
        {
            if (response.hierarchicalCustomerCampaigns != null)
            {
                if (response.hierarchicalCustomerCampaigns.Any())
                {
                    response.hierarchicalCustomerCampaigns.ForEach(hccs =>
                    hccs.subHierarchicalCustomerCampaigns =
                    GetResponseDetails(hccs.subHierarchicalCustomerCampaigns));
                }
            }
            if (response.unhierarchicalCustomerCampaigns != null)
            {
                if (response.unhierarchicalCustomerCampaigns.Any())
                    response.unhierarchicalCustomerCampaigns = GetResponseDetails(response.unhierarchicalCustomerCampaigns);
            }
            return response;
        }

        public static List<SubHierarchicalCustomerCampaignMobileViewModel> GetResponseDetails(List<SubHierarchicalCustomerCampaignMobileViewModel> response)
        {
            if (response != null)
            {
                if (response.Any())
                    response.ForEach(cs => cs = GetResponseDetail(cs));
            }
            return response;
        }

        public static SubHierarchicalCustomerCampaignMobileViewModel GetResponseDetail(SubHierarchicalCustomerCampaignMobileViewModel response)
        {
            if (response != null)
            {
                if (response.campaigns != null)
                {
                    if (response.campaigns.Any())
                        response.campaigns.ForEach(c => c = GetResponseDetail(c));
                }
            }
            return response;
        }

        public static List<UnhierarchicalCustomerCampaignMobileViewModel> GetResponseDetails(List<UnhierarchicalCustomerCampaignMobileViewModel> response)
        {
            if (response != null)
            {
                if (response.Any())
                    response.ForEach(cs => cs = GetResponseDetail(cs));
            }
            return response;
        }
        public static UnhierarchicalCustomerCampaignMobileViewModel GetResponseDetail(UnhierarchicalCustomerCampaignMobileViewModel response)
        {
            if (response != null)
            {
                if (response.campaigns != null)
                {
                    if (response.campaigns.Any())
                        response.campaigns.ForEach(c => c = GetResponseDetail(c));
                }
            }
            return response;
        }
        public static List<OrganizationMemberViewModel> GetResponsesDetails(List<OrganizationMemberViewModel> responses)
        {
            if (responses != null)
            {
                if (responses.Any())
                    responses.ForEach(o => o = GetResponseDetail(o));
            }
            return responses;
        }
        public static OrganizationMemberViewModel GetResponseDetail(OrganizationMemberViewModel response)
        {
            if (response != null)
                response.StatusName = StatusExtension<BoekStatus>.GetStatus(response.Status);
            return response;
        }
        public static List<OrganizationsMobileViewModel> GetResponsesDetail(List<OrganizationsMobileViewModel> responses, bool WithAddressDetail = false)
        {
            if (responses != null)
            {
                if (responses.Any())
                    responses.ForEach(o => o = GetResponseDetail(o));
            }
            return responses;
        }
        public static OrganizationsMobileViewModel GetResponseDetail(OrganizationsMobileViewModel response, bool WithAddressDetail = false)
        {
            response.Schedules = GetResponsesDetails(response.Schedules, WithAddressDetail);
            return response;
        }
        public static CampaignOrganizationsViewModel GetResponseDetail(CampaignOrganizationsViewModel response, bool WithAddressDetail = false)
        {
            response.Schedules = GetResponsesDetails(response.Schedules, WithAddressDetail);
            return response;
        }

        public static List<SchedulesViewModel> GetResponsesDetails(List<SchedulesViewModel> response, bool WithAddressDetail = false)
        {
            if (response != null)
            {
                if (response.Any())
                    response.ForEach(coa => coa = GetResponseDetail(coa, WithAddressDetail));
            }
            return response;
        }
        public static SchedulesViewModel GetResponseDetail(SchedulesViewModel response, bool WithAddressDetail = false)
        {
            if (response.Status.HasValue)
                response.StatusName = StatusExtension<CampaignStatus>.GetStatus(response.Status);
            if (WithAddressDetail)
                response.AddressViewModel = GetResponseDetail(response.Address);
            return response;
        }

        public static List<int?> CheckBookGenreFilter(List<int?> GenreIds, IUnitOfWork _unitOfWork)
        {
            if (GenreIds != null)
            {
                var genreIds = new List<int>();
                GenreIds.ForEach(gis =>
                {
                    if (!genreIds.Contains((int)gis))
                        genreIds.Add((int)gis);
                });
                var parentGenre = new List<int>();
                var childGenre = new List<int>();
                genreIds.ForEach(gis =>
                {
                    var genre = _unitOfWork.Genres
                    .Get(g => g.Id.Equals(gis))
                    .Include(g => g.InverseParent)
                    .SingleOrDefault();
                    if (genre != null)
                    {
                        if (!genre.ParentId.HasValue && !parentGenre.Contains(genre.Id))
                        {
                            parentGenre.Add(genre.Id);
                            var temp = genre.InverseParent.Select(ip => ip.Id).Except(childGenre);
                            if (temp.Any())
                                childGenre.AddRange(temp);
                        }
                    }
                });
                if (parentGenre.Any())
                {
                    var temp = childGenre.Except(genreIds);
                    if (temp.Any())
                        genreIds.AddRange(temp);
                    genreIds = genreIds.OrderBy(gis => gis).ToList();
                    var result = new List<int?>();
                    genreIds.ForEach(gis => result.Add(gis));
                    GenreIds.Clear();
                    GenreIds = result;
                }
            }
            return GenreIds;
        }


        public static IssuerBookViewModel GetBookResponseDetail(IssuerBookViewModel response)
        {
            response = GetResponseDetail(response);
            response.Genre = GetResponseDetail(response.Genre);
            if (response.Issuer.User != null)
                response.Issuer.User = GetResponseDetail(response.Issuer.User);
            if (response.BookItems.Any())
            {
                response.BookItems.ForEach(bi =>
                {
                    bi.Book = GetResponseDetail(bi.Book);
                });
                response.BookItems = response.BookItems.OrderBy(bi => bi.DisplayIndex).ToList();
            }
            response = GetBookPdfAndAudioDetail(response);
            return response;
        }
        public static BookViewModel GetBookResponseDetail(BookViewModel response)
        {
            response = GetResponseDetail(response);
            response.Genre = GetResponseDetail(response.Genre);
            if (response.Issuer.User != null)
                response.Issuer.User = GetResponseDetail(response.Issuer.User);
            if (response.BookItems.Any())
            {
                response.BookItems.ForEach(bi =>
                {
                    bi.Book = GetResponseDetail(bi.Book);
                });
                response.BookItems = response.BookItems.OrderBy(bi => bi.DisplayIndex).ToList();
            }
            response = GetBookPdfAndAudioDetail(response);
            return response;
        }
        public static BookProductDetailViewModel GetBookResponseDetail(BookProductDetailViewModel response)
        {
            response = GetResponseDetail(response);
            response.Genre = GetResponseDetail(response.Genre);
            if (response.Issuer.User != null)
                response.Issuer.User = GetResponseDetail(response.Issuer.User);
            return response;
        }

        public static IssuerBookViewModel GetResponseDetail(IssuerBookViewModel response)
        {
            response.StatusName = StatusExtension<BookStatus>.GetStatus(response.Status);
            return response;
        }
        public static BookViewModel GetResponseDetail(BookViewModel response)
        {
            response.StatusName = StatusExtension<BookStatus>.GetStatus(response.Status);
            return response;
        }

        public static BookProductDetailViewModel GetResponseDetail(BookProductDetailViewModel response)
        {
            response.StatusName = StatusExtension<BookStatus>.GetStatus(response.Status);
            if (!(bool)response.IsSeries)
                response = GetBookPdfAndAudioStatuses(response);
            return response;
        }

        public static BasicBookViewModel GetResponseDetail(BasicBookViewModel response)
        {
            response.StatusName = StatusExtension<BookStatus>.GetStatus(response.Status);
            return response;
        }

        public static GenreViewModel GetResponseDetail(GenreViewModel response)
        {
            response.StatusName = StatusExtension<BoekStatus>.GetStatus(response.Status);
            return response;
        }
        public static BasicLevelViewModel GetResponseDetail(BasicLevelViewModel response)
        {
            response.StatusName = StatusExtension<BoekStatus>.GetStatus(response.Status);
            return response;
        }

        public static LevelViewModel GetResponseDetail(LevelViewModel response)
        {
            response.StatusName = StatusExtension<BoekStatus>.GetStatus(response.Status);
            return response;
        }
        public static BasicGroupViewModel GetResponseDetail(BasicGroupViewModel response)
        {
            response.StatusName = StatusExtension<BoekStatus>.GetStatus(response.Status);
            return response;
        }
        public static GroupViewModel GetResponseDetail(GroupViewModel response)
        {
            response.StatusName = StatusExtension<BoekStatus>.GetStatus(response.Status);
            return response;
        }

        public static GenreBooksViewModel GetResponseDetail(GenreBooksViewModel response)
        {
            response = GetGenreBooksResponseDetail(response);
            if (response.Books != null)
                response.Books.ForEach(b => b.StatusName = StatusExtension<BookStatus>.GetStatus(b.Status));
            return response;
        }
        public static GenreBooksViewModel GetGenreBooksResponseDetail(GenreBooksViewModel response)
        {
            response.StatusName = StatusExtension<BoekStatus>.GetStatus(response.Status);
            return response;
        }

        public static ParticipantViewModel GetResponseDetail(ParticipantViewModel response)
        {
            response.StatusName = StatusExtension<ParticipantStatus>.GetStatus(response.Status);
            if (response.Campaign != null)
            {
                response.Campaign = GetCampaignDetail(response.Campaign);
            }
            if (response.Issuer != null)
            {
                response.Issuer.User = GetResponseDetail(response.Issuer.User);
            }
            return response;
        }

        public static CampaignStaffViewModel GetResponseDetail(CampaignStaffViewModel response)
        {
            response.StatusName = StatusExtension<CampaignStaffStatus>.GetStatus(response.Status);
            if (response.Campaign != null)
            {
                response.Campaign = GetCampaignDetail(response.Campaign);
            }
            if (response.Staff != null)
            {
                response.Staff = GetResponseDetail(response.Staff);
            }
            return response;
        }

        public static CampaignStaffsViewModel GetResponseDetails(CampaignStaffsViewModel response)
        {
            if (response != null)
            {
                if (response.Campaign != null)
                {
                    response.Campaign = GetCampaignDetail(response.Campaign);
                }
                if (response.Staffs.Any())
                {
                    response.Staffs.ForEach(staff =>
                    {
                        staff = GetResponseDetail(staff);
                    });
                    response.GetTotal();
                }
            }
            return response;
        }
        public static StaffCampaignsViewModel GetResponseDetails(StaffCampaignsViewModel response)
        {
            response.StatusName = StatusExtension<CampaignStaffStatus>.GetStatus(response.Status);
            if (response.Campaigns.Any())
            {
                response.Campaigns.ForEach(campaign =>
                {
                    campaign = GetCampaignDetail(campaign);
                });
            }
            if (response.Staff != null)
            {
                response.Staff = GetResponseDetail(response.Staff);
            }
            return response;
        }

        public static HierarchicalStaffCampaignsViewModel GetResponseDetails(HierarchicalStaffCampaignsViewModel response)
        {
            if (response.Campaigns != null)
            {
                if (response.Campaigns.Any())
                    response.Campaigns.ForEach(c => c = GetCampaignDetail(c));
            }
            return response;
        }
        public static BookProductViewModel GetResponseDetail(BookProductViewModel response)
        {
            response.StatusName = StatusExtension<BookProductStatus>.GetStatus(response.Status);
            response.TypeName = StatusExtension<BookType>.GetStatus(response.Type);
            response.FormatName = StatusExtension<BookFormat>.GetStatus(response.Format);
            if (response.Campaign != null)
                response.Campaign = GetCampaignDetail(response.Campaign);
            if (response.Book != null)
                response.Book = GetBookResponseDetail(response.Book);
            if (response.Issuer != null)
                response.Issuer.User = GetResponseDetail(response.Issuer.User);
            if (response.BookProductItems.Any())
                response.BookProductItems = GetBookProductItemDetails(response.BookProductItems);
            response = GetBookPdfAndAudioDetail(response);
            return response;
        }
        public static BookProductOrderDetailsViewModel GetResponseDetail(BookProductOrderDetailsViewModel response)
        {
            response.StatusName = StatusExtension<BookProductStatus>.GetStatus(response.Status);
            response.TypeName = StatusExtension<BookType>.GetStatus(response.Type);
            response.FormatName = StatusExtension<BookFormat>.GetStatus(response.Format);
            return response;
        }
        public static MobileBookProductsViewModel GetResponseDetail(MobileBookProductsViewModel response)
        {
            response.StatusName = StatusExtension<BookProductStatus>.GetStatus(response.Status);
            response.TypeName = StatusExtension<BookType>.GetStatus(response.Type);
            response.FormatName = StatusExtension<BookFormat>.GetStatus(response.Format);
            if (response.Book != null)
                response.Book = GetBookResponseDetail(response.Book);
            if (response.Campaign != null)
                response.Campaign = GetCampaignDetail(response.Campaign);
            if (response.BookProductItems.Any())
                response.BookProductItems = GetBookProductItemDetails(response.BookProductItems);
            return response;
        }
        public static MobileBookProductViewModel GetResponseDetail(MobileBookProductViewModel response)
        {
            response.StatusName = StatusExtension<BookProductStatus>.GetStatus(response.Status);
            response.TypeName = StatusExtension<BookType>.GetStatus(response.Type);
            response.FormatName = StatusExtension<BookFormat>.GetStatus(response.Format);
            if (response.Campaign != null)
                response.Campaign = GetCampaignDetail(response.Campaign);
            if (response.Book != null)
                response.Book = GetBookResponseDetail(response.Book);
            if (response.Issuer != null)
                response.Issuer.User = GetResponseDetail(response.Issuer.User);
            if (response.BookProductItems.Any())
                response.BookProductItems = GetBookProductItemDetails(response.BookProductItems);
            response.UnhierarchicalBookProducts = GetResponseDetails(response.UnhierarchicalBookProducts);
            return response;
        }

        public static List<BookProductItemViewModel> GetBookProductItemDetails(List<BookProductItemViewModel> list)
        {
            list.ForEach(bpis =>
            {
                if (bpis.Book != null)
                    bpis.Book = GetBookResponseDetail(bpis.Book);
            });
            return list;
        }

        public static bool CheckCampaignEndDateAndStatus(ref Campaign campaign, DateTime today)
        => DateTime.Compare((DateTime)campaign.EndDate, today) <= 0 && !campaign.Status.Equals((byte)CampaignStatus.Cancelled);

        #region Pdf and Audio
        public static IssuerBookViewModel GetBookPdfAndAudioDetail(IssuerBookViewModel book)
        {
            if ((bool)book.IsSeries)
            {
                if (book.BookItems.Any())
                {
                    var OnlyPdfs = new List<bool?>();
                    var OnlyAudios = new List<bool?>();
                    var FullPdfAndAudios = new List<bool?>();
                    book.BookItems.ForEach(bi =>
                    {
                        bi.Book = GetBookPdfAndAudioStatuses(bi.Book);
                        OnlyPdfs.Add(bi.Book.OnlyPdf);
                        OnlyAudios.Add(bi.Book.OnlyAudio);
                        FullPdfAndAudios.Add(bi.Book.FullPdfAndAudio);
                    });
                    var resultAudioAndPdf = false;
                    var list = FullPdfAndAudios.GroupBy(p => p).ToList();
                    list.ForEach(p =>
                    {
                        if (p.Key.Equals(true) && p.Count().Equals(FullPdfAndAudios.Count))
                            resultAudioAndPdf = true;
                    });
                    book.FullPdfAndAudio = resultAudioAndPdf;
                    if (!(bool)book.FullPdfAndAudio)
                    {
                        var resultOnlyPdf = false;
                        var resultOnlyAudio = false;
                        list = OnlyAudios.GroupBy(a => a).ToList();
                        list.ForEach(a =>
                        {
                            if (a.Key.Equals(true) && a.Count().Equals(OnlyAudios.Count))
                                resultOnlyAudio = true;
                        });
                        list = OnlyPdfs.GroupBy(p => p).ToList();
                        list.ForEach(p =>
                        {
                            if (p.Key.Equals(true) && p.Count().Equals(OnlyPdfs.Count))
                                resultOnlyPdf = true;
                        });
                        book.OnlyAudio = resultOnlyAudio;
                        book.OnlyPdf = resultOnlyPdf;
                    }
                }
            }
            else
                book = GetBookPdfAndAudioStatuses(book);
            return book;
        }
        public static BookViewModel GetBookPdfAndAudioDetail(BookViewModel book)
        {
            if ((bool)book.IsSeries)
            {
                if (book.BookItems.Any())
                {
                    var OnlyPdfs = new List<bool?>();
                    var OnlyAudios = new List<bool?>();
                    var FullPdfAndAudios = new List<bool?>();
                    book.BookItems.ForEach(bi =>
                    {
                        bi.Book = GetBookPdfAndAudioStatuses(bi.Book);
                        OnlyPdfs.Add(bi.Book.OnlyPdf);
                        OnlyAudios.Add(bi.Book.OnlyAudio);
                        FullPdfAndAudios.Add(bi.Book.FullPdfAndAudio);
                    });
                    var resultAudioAndPdf = false;
                    var list = FullPdfAndAudios.GroupBy(p => p).ToList();
                    list.ForEach(p =>
                    {
                        if (p.Key.Equals(true) && p.Count().Equals(FullPdfAndAudios.Count))
                            resultAudioAndPdf = true;
                    });
                    book.FullPdfAndAudio = resultAudioAndPdf;
                    if (!(bool)book.FullPdfAndAudio)
                    {
                        var resultOnlyPdf = false;
                        var resultOnlyAudio = false;
                        list = OnlyAudios.GroupBy(a => a).ToList();
                        list.ForEach(a =>
                        {
                            if (a.Key.Equals(true) && a.Count().Equals(OnlyAudios.Count))
                                resultOnlyAudio = true;
                        });
                        list = OnlyPdfs.GroupBy(p => p).ToList();
                        list.ForEach(p =>
                        {
                            if (p.Key.Equals(true) && p.Count().Equals(OnlyPdfs.Count))
                                resultOnlyPdf = true;
                        });
                        book.OnlyAudio = resultOnlyAudio;
                        book.OnlyPdf = resultOnlyPdf;
                    }
                }
            }
            else
                book = GetBookPdfAndAudioStatuses(book);
            return book;
        }
        public static BookProductViewModel GetBookPdfAndAudioDetail(BookProductViewModel bookProduct)
        {
            if (bookProduct.Type.Equals((byte)BookType.Odd))
                bookProduct = GetOddBookPdfAndAudioStatuses(bookProduct);
            else if (bookProduct.Type.Equals((byte)BookType.Combo) || bookProduct.Type.Equals((byte)BookType.Series))
            {
                if (bookProduct.BookProductItems.Any())
                {
                    var OnlyPdfs = new List<bool?>();
                    var OnlyAudios = new List<bool?>();
                    var FullPdfAndAudios = new List<bool?>();
                    bookProduct.BookProductItems.ForEach(bpi =>
                    {
                        FullPdfAndAudios.Add((bool)bpi.WithAudio && (bool)bpi.WithPdf);
                        OnlyPdfs.Add(bpi.WithPdf);
                        OnlyAudios.Add(bpi.WithAudio);
                    });
                    var resultAudioAndPdf = false;
                    var list = FullPdfAndAudios.GroupBy(p => p).ToList();
                    list.ForEach(p =>
                    {
                        if (p.Key.Equals(true) && p.Count().Equals(FullPdfAndAudios.Count))
                            resultAudioAndPdf = true;
                    });
                    bookProduct.FullPdfAndAudio = resultAudioAndPdf;
                    if (!(bool)bookProduct.FullPdfAndAudio)
                    {
                        var resultOnlyPdf = false;
                        var resultOnlyAudio = false;
                        list = OnlyAudios.GroupBy(a => a).ToList();
                        list.ForEach(a =>
                        {
                            if (a.Key.Equals(true) && a.Count().Equals(OnlyAudios.Count))
                                resultOnlyAudio = true;
                        });
                        list = OnlyPdfs.GroupBy(p => p).ToList();
                        list.ForEach(p =>
                        {
                            if (p.Key.Equals(true) && p.Count().Equals(OnlyPdfs.Count))
                                resultOnlyPdf = true;
                        });
                        bookProduct.OnlyAudio = resultOnlyAudio;
                        bookProduct.OnlyPdf = resultOnlyPdf;
                    }
                }
            }
            return bookProduct;
        }
        public static OrderBookProductsViewModel GetBookPdfAndAudioDetail(OrderBookProductsViewModel bookProduct)
        {
            if (bookProduct.Type.Equals((byte)BookType.Odd))
                bookProduct = GetOddBookPdfAndAudioStatuses(bookProduct);
            else if (bookProduct.Type.Equals((byte)BookType.Combo) || bookProduct.Type.Equals((byte)BookType.Series))
            {
                if (bookProduct.BookProductItems.Any())
                {
                    var OnlyPdfs = new List<bool?>();
                    var OnlyAudios = new List<bool?>();
                    var FullPdfAndAudios = new List<bool?>();
                    bookProduct.BookProductItems.ForEach(bpi =>
                    {
                        bpi.Book = GetBookPdfAndAudioStatuses(bpi.Book);
                        OnlyPdfs.Add(bpi.Book.OnlyPdf);
                        OnlyAudios.Add(bpi.Book.OnlyAudio);
                        FullPdfAndAudios.Add(bpi.Book.FullPdfAndAudio);
                    });
                    var resultAudioAndPdf = false;
                    var list = FullPdfAndAudios.GroupBy(p => p).ToList();
                    list.ForEach(p =>
                    {
                        if (p.Key.Equals(true) && p.Count().Equals(FullPdfAndAudios.Count))
                            resultAudioAndPdf = true;
                    });
                    bookProduct.FullPdfAndAudio = resultAudioAndPdf;
                    if (!(bool)bookProduct.FullPdfAndAudio)
                    {
                        var resultOnlyPdf = false;
                        var resultOnlyAudio = false;
                        list = OnlyAudios.GroupBy(a => a).ToList();
                        list.ForEach(a =>
                        {
                            if (a.Key.Equals(true) && a.Count().Equals(OnlyAudios.Count))
                                resultOnlyAudio = true;
                        });
                        list = OnlyPdfs.GroupBy(p => p).ToList();
                        list.ForEach(p =>
                        {
                            if (p.Key.Equals(true) && p.Count().Equals(OnlyPdfs.Count))
                                resultOnlyPdf = true;
                        });
                        bookProduct.OnlyAudio = resultOnlyAudio;
                        bookProduct.OnlyPdf = resultOnlyPdf;
                    }
                }
            }
            return bookProduct;
        }
        public static BookProductViewModel GetOddBookPdfAndAudioStatuses(BookProductViewModel bookProduct)
        {
            if (bookProduct.Type.Equals((byte)BookType.Odd))
            {
                var PdfNotNull = bookProduct.PdfExtraPrice.HasValue && !String.IsNullOrEmpty(bookProduct.Book.PdfTrialUrl);
                var AudioNotNull = bookProduct.AudioExtraPrice.HasValue && !String.IsNullOrEmpty(bookProduct.Book.AudioTrialUrl);
                bookProduct.FullPdfAndAudio = PdfNotNull && AudioNotNull;
                if (!(bool)bookProduct.FullPdfAndAudio)
                {
                    bookProduct.OnlyAudio = AudioNotNull;
                    bookProduct.OnlyPdf = PdfNotNull;
                }
            }
            return bookProduct;
        }
        public static OrderBookProductsViewModel GetOddBookPdfAndAudioStatuses(OrderBookProductsViewModel bookProduct)
        {
            if (bookProduct.Type.Equals((byte)BookType.Odd))
            {
                var PdfNotNull = bookProduct.PdfExtraPrice.HasValue && !String.IsNullOrEmpty(bookProduct.Book.PdfTrialUrl);
                var AudioNotNull = bookProduct.AudioExtraPrice.HasValue && !String.IsNullOrEmpty(bookProduct.Book.AudioTrialUrl);
                bookProduct.FullPdfAndAudio = PdfNotNull && AudioNotNull;
                if (!(bool)bookProduct.FullPdfAndAudio)
                {
                    bookProduct.OnlyAudio = AudioNotNull;
                    bookProduct.OnlyPdf = PdfNotNull;
                }
            }
            return bookProduct;
        }
        public static BookProductDetailViewModel GetBookPdfAndAudioStatuses(BookProductDetailViewModel book)
        {
            var PdfNotNull = book.PdfExtraPrice.HasValue && !String.IsNullOrEmpty(book.PdfTrialUrl);
            var AudioNotNull = book.AudioExtraPrice.HasValue && !String.IsNullOrEmpty(book.AudioTrialUrl);
            book.FullPdfAndAudio = PdfNotNull && AudioNotNull;
            if (!(bool)book.FullPdfAndAudio)
            {
                book.OnlyAudio = AudioNotNull;
                book.OnlyPdf = PdfNotNull;
            }
            return book;
        }
        public static IssuerBookViewModel GetBookPdfAndAudioStatuses(IssuerBookViewModel book)
        {
            var PdfNotNull = book.PdfExtraPrice.HasValue && !String.IsNullOrEmpty(book.PdfTrialUrl);
            var AudioNotNull = book.AudioExtraPrice.HasValue && !String.IsNullOrEmpty(book.AudioTrialUrl);
            book.FullPdfAndAudio = PdfNotNull && AudioNotNull;
            if (!(bool)book.FullPdfAndAudio)
            {
                book.OnlyAudio = AudioNotNull;
                book.OnlyPdf = PdfNotNull;
            }
            return book;
        }
        public static BookViewModel GetBookPdfAndAudioStatuses(BookViewModel book)
        {
            var PdfNotNull = book.PdfExtraPrice.HasValue && !String.IsNullOrEmpty(book.PdfTrialUrl);
            var AudioNotNull = book.AudioExtraPrice.HasValue && !String.IsNullOrEmpty(book.AudioTrialUrl);
            book.FullPdfAndAudio = PdfNotNull && AudioNotNull;
            if (!(bool)book.FullPdfAndAudio)
            {
                book.OnlyAudio = AudioNotNull;
                book.OnlyPdf = PdfNotNull;
            }
            return book;
        }
        public static BasicBookViewModel GetBookPdfAndAudioStatuses(BasicBookViewModel book)
        {
            var PdfNotNull = book.PdfExtraPrice.HasValue && !String.IsNullOrEmpty(book.PdfTrialUrl);
            var AudioNotNull = book.AudioExtraPrice.HasValue && !String.IsNullOrEmpty(book.AudioTrialUrl);
            book.FullPdfAndAudio = PdfNotNull && AudioNotNull;
            if (!(bool)book.FullPdfAndAudio)
            {
                book.OnlyAudio = AudioNotNull;
                book.OnlyPdf = PdfNotNull;
            }
            return book;
        }
        #endregion

        public static CustomerGroupViewModel GetResponseDetail(CustomerGroupViewModel response)
        {
            if (response.Customer != null)
            {
                response.Customer.User = GetResponseDetail(response.Customer.User);
            }
            if (response.Group != null)
                response.Group = GetResponseDetail(response.Group);
            return response;
        }

        public static OwnedCustomerGroupViewModel GetResponseDetail(OwnedCustomerGroupViewModel response)
        {
            if (response.Customer != null)
            {
                if (response.Customer.User != null)
                    response.Customer.User = GetResponseDetail(response.Customer.User);
            }
            if (response.Groups.Any())
                response.Groups.ForEach(g => g.Group = GetResponseDetail(g.Group));
            return response;
        }

        public static OrderViewModel GetResponseDetail(OrderViewModel response)
        {
            response.PaymentName = StatusExtension<OrderPayment>.GetStatus(response.Payment);
            response.TypeName = StatusExtension<OrderType>.GetStatus(response.Type);
            response.StatusName = StatusExtension<OrderStatus>.GetStatus(response.Status);
            response.FreightName = StatusExtension<OrderFreight>.GetStatus(response.Freight);
            if (response.Campaign != null)
                response.Campaign = GetCampaignDetail(response.Campaign);
            if (response.CampaignStaff != null)
                response.CampaignStaff = GetResponseDetail(response.CampaignStaff);
            if (response.Customer != null)
            {
                if (response.Customer.User != null)
                {
                    var ShowErrorMessage = response.Customer.User.Role.HasValue;
                    response.Customer.User = GetResponseDetail(response.Customer.User, false, ShowErrorMessage);
                }
            }
            response.OrderDetails = GetResponsesDetail(response.OrderDetails);
            return response;
        }
        public static OrdersViewModel GetResponseDetail(OrdersViewModel response)
        {
            response.PaymentName = StatusExtension<OrderPayment>.GetStatus(response.Payment);
            response.TypeName = StatusExtension<OrderType>.GetStatus(response.Type);
            response.StatusName = StatusExtension<OrderStatus>.GetStatus(response.Status);
            response.FreightName = StatusExtension<OrderFreight>.GetStatus(response.Freight);
            if (response.CampaignStaff != null)
                response.CampaignStaff = GetResponseDetail(response.CampaignStaff);
            if (response.Customer != null)
            {
                if (response.Customer.User != null)
                {
                    var ShowErrorMessage = response.Customer.User.Role.HasValue;
                    response.Customer.User = GetResponseDetail(response.Customer.User, false, ShowErrorMessage);
                }
            }
            response.OrderDetails = GetResponsesDetail(response.OrderDetails);
            return response;
        }

        public static OrderCampaignStaffViewModel GetResponseDetail(OrderCampaignStaffViewModel response)
        {
            response.StatusName = StatusExtension<CampaignStaffStatus>.GetStatus(response.Status);
            if (response.Staff != null)
                response.Staff = GetResponseDetail(response.Staff);
            return response;
        }

        public static List<OrderDetailsViewModel> GetResponsesDetail(List<OrderDetailsViewModel> responses)
        {
            if (responses != null)
            {
                if (responses.Any())
                    responses.ForEach(r => r = GetResponseDetail(r));
            }
            return responses;
        }
        public static OrderDetailsViewModel GetResponseDetail(OrderDetailsViewModel response)
        {
            if (response != null)
            {
                if (response.BookProduct != null)
                    response.BookProduct = GetResponseDetail(response.BookProduct);
            }
            return response;
        }

        public static OrderBookProductsViewModel GetResponseDetail(OrderBookProductsViewModel response)
        {
            response.StatusName = StatusExtension<BookProductStatus>.GetStatus(response.Status);
            response.TypeName = StatusExtension<BookType>.GetStatus(response.Type);
            response.FormatName = StatusExtension<BookFormat>.GetStatus(response.Format);
            if (response.Issuer != null)
                response.Issuer.User = GetResponseDetail(response.Issuer.User);
            if (response.Type.Equals((byte)BookType.Combo))
                response = GetBookPdfAndAudioDetail(response);
            return response;
        }
        public static List<string> CheckOrderDetailsQuantities(List<string> errorMessages, Campaign campaign)
        {
            DateTime dateTimeNow = DateTime.Now;
            if (!(campaign.StartDate <= dateTimeNow && campaign.EndDate >= dateTimeNow))
            {
                errorMessages.Add(ErrorMessageConstants.INVALID_SCHEDULED_DATE);
            }
            return errorMessages;
        }

        public static string GetTitleByStatus(byte? status, List<string> Titles)
        {
            if (status > Titles.Count())
                return null;
            return Titles[(int)status - 1];
        }

        public static OrderViewModel GetTotal(OrderViewModel order, IMapper _mapper, IUnitOfWork _unitOfWork)
        {
            var flag = false;
            if (order.OrderDetails != null)
            {
                if (order.OrderDetails.Any())
                {
                    var temp = _mapper.Map<Order>(order);
                    order.Total = (decimal?)_unitOfWork.Orders.GetTotal(temp);
                    order.SubTotal = (decimal?)_unitOfWork.Orders.GetSubTotal(temp);
                    if (order.SubTotal > order.Total)
                        order.DiscountTotal = order.SubTotal - order.Total;
                    else
                        order.DiscountTotal = order.Total - order.SubTotal;
                    if (order.Freight.HasValue)
                    {
                        if (order.Freight > 0)
                            order.DiscountTotal -= (decimal?)order.Freight;
                    }

                    order.OrderDetails.ForEach(od =>
                    {
                        var temp = _mapper.Map<OrderDetail>(od);
                        od.Total = (decimal?)_unitOfWork.OrderDetails.GetTotal(temp);
                        od.SubTotal = (decimal?)_unitOfWork.OrderDetails.GetSubTotal(temp);
                    });
                    order = GetTotalFormat(order);
                }
                else
                {
                    order.Total = 0;
                    order.SubTotal = 0;
                    order.DiscountTotal = 0;
                    flag = true;
                }
            }
            else
            {
                order.Total = 0;
                order.SubTotal = 0;
                order.DiscountTotal = 0;
            }
            if (flag)
            {
                order.OrderDetails.ForEach(od =>
                {
                    od.Total = 0;
                    od.SubTotal = 0;
                });
            }
            return order;
        }
        public static OrdersViewModel GetTotal(OrdersViewModel order, IMapper _mapper, IUnitOfWork _unitOfWork)
        {
            var flag = false;
            if (order.OrderDetails != null)
            {
                if (order.OrderDetails.Any())
                {
                    var temp = _mapper.Map<Order>(order);
                    order.Total = (decimal?)_unitOfWork.Orders.GetTotal(temp);
                    order.SubTotal = (decimal?)_unitOfWork.Orders.GetSubTotal(temp);
                    if (order.SubTotal > order.Total)
                        order.DiscountTotal = order.SubTotal - order.Total;
                    else
                        order.DiscountTotal = order.Total - order.SubTotal;
                    if (order.Freight.HasValue)
                    {
                        if (order.Freight > 0)
                            order.DiscountTotal -= (decimal?)order.Freight;
                    }

                    order.OrderDetails.ForEach(od =>
                    {
                        var temp = _mapper.Map<OrderDetail>(od);
                        od.Total = (decimal?)_unitOfWork.OrderDetails.GetTotal(temp);
                        od.SubTotal = (decimal?)_unitOfWork.OrderDetails.GetSubTotal(temp);
                    });
                    order = GetTotalFormat(order);
                }
                else
                {
                    order.Total = 0;
                    order.SubTotal = 0;
                    order.DiscountTotal = 0;
                    flag = true;
                }
            }
            else
            {
                order.Total = 0;
                order.SubTotal = 0;
                order.DiscountTotal = 0;
            }
            if (flag)
            {
                order.OrderDetails.ForEach(od =>
                {
                    od.Total = 0;
                    od.SubTotal = 0;
                });
            }
            return order;
        }

        public static OrderViewModel GetTotalFormat(OrderViewModel order)
        {
            if (order.Total != 0)
                order.Total = ConvertDecimalNumber(order.Total);
            if (order.SubTotal != 0)
                order.SubTotal = ConvertDecimalNumber(order.SubTotal);
            if (order.DiscountTotal != 0)
            {
                order.DiscountTotal = ConvertDecimalNumber(order.DiscountTotal);
                if (order.DiscountTotal > 0)
                    order.DiscountTotal *= -1;
            }
            order.OrderDetails.ForEach(od =>
            {
                if (od.Total != null)
                    od.Total = ConvertDecimalNumber(od.Total);
                if (od.SubTotal != null)
                    od.SubTotal = ConvertDecimalNumber(od.SubTotal);
            });
            return order;
        }
        public static OrdersViewModel GetTotalFormat(OrdersViewModel order)
        {
            if (order.Total != 0)
                order.Total = ConvertDecimalNumber(order.Total);
            if (order.SubTotal != 0)
                order.SubTotal = ConvertDecimalNumber(order.SubTotal);
            if (order.DiscountTotal != 0)
            {
                order.DiscountTotal = ConvertDecimalNumber(order.DiscountTotal);
                if (order.DiscountTotal > 0)
                    order.DiscountTotal *= -1;
            }
            order.OrderDetails.ForEach(od =>
            {
                if (od.Total != null)
                    od.Total = ConvertDecimalNumber(od.Total);
                if (od.SubTotal != null)
                    od.SubTotal = ConvertDecimalNumber(od.SubTotal);
            });
            return order;
        }

        public static decimal? ConvertDecimalNumber(decimal? number)
        {
            var index = number.ToString().IndexOf(".");
            if (index != -1)
            {
                var temp = number.ToString().Substring(0, index);
                decimal result = 0;
                Decimal.TryParse(temp, out result);
                number = (decimal?)result;
            }
            return number;
        }


        public static string GetUpdateNote(string updateNote, string note, IHttpContextAccessor _httpContextAccessor, string claimTypes, bool WithDateTime = false)
        {
            if (!String.IsNullOrEmpty(updateNote))
            {
                var _user = GetUserInfoValue(_httpContextAccessor, claimTypes);
                if (string.IsNullOrEmpty(_user))
                {
                    BoekErrorMessage.ShowErrorMessage((int)HttpStatusCode.NotFound, new string[]
                    {
                        ErrorMessageConstants.NOT_FOUND,
                        ErrorMessageConstants.USER.ToLower()
                    });
                }
                note = GetUpdateNote(updateNote, note, _user, WithDateTime);
            }
            return note;
        }

        public static string GetUpdateNote(string updateNote, string note, string name, bool WithDateTime = false)
        {
            var message = $"{name}: {updateNote}";
            if (WithDateTime)
                message = $"({DateTime.Now}) " + message;
            if (String.IsNullOrEmpty(note))
                note = message;
            else
                note += $";{message}";
            return note;
        }

        public static AddressViewModel GetResponseDetail(string Address)
        {
            AddressViewModel result = null;
            if (string.IsNullOrEmpty(Address))
                return null;
            var address = Address.ToLower().Trim();
            try
            {
                var ward = WardsList.WARDS.SingleOrDefault(w => address.Contains(w.NameWithFullPath.ToLower().Trim()));
                var district = DistrictsList.DISTRICTS.SingleOrDefault(d => d.Code.Equals(ward.ParentCode));
                var province = ProvincesList.PROVINCES.SingleOrDefault(p => p.Code.Equals(district.ParentCode));
                if (ward == null || district == null || province == null)
                    return null;
                var detail = Address.Substring(0, address.IndexOf(ward.NameWithFullPath.ToLower().Trim())).Trim();
                var lastCharacter = detail.Substring(detail.Length - 1).ToCharArray();
                if (!char.IsLetterOrDigit(lastCharacter[0]))
                    detail = detail.Substring(0, detail.Length - 1);
                result = new AddressViewModel()
                {
                    Detail = detail,
                    WardCode = ward.Code,
                    DistrictCode = district.Code,
                    ProvinceCode = province.Code
                };
            }
            catch (Exception)
            {
                return null;
            }
            return result;
        }
        public static AddressResponse CheckAddress(AddressRequestModel addressRequest, bool ReturnErrorMessages = false)
        {
            var address = GetAddress(addressRequest, ReturnErrorMessages);
            if (address == null)
            {
                var error = MessageConstants.MESSAGE_REQUIRED + " " + ErrorMessageConstants.ADDRESS;
                if (ReturnErrorMessages)
                {
                    address = new AddressResponse()
                    {
                        ErrorMessages = new List<string>() { error },
                        IsSuccess = false
                    };
                }
                else
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[] { error });
            }
            return address;
        }
        public static AddressResponse GetAddress(AddressRequestModel address, bool ReturnErrorMessages = false)
        {
            if (!address.IsEmptyAllFields())
            {
                var response = new AddressResponse();
                var provinces = ProvincesList.PROVINCES;
                var districts = DistrictsList.DISTRICTS;
                var wards = WardsList.WARDS;
                response = CheckDetailByProvinces(address, provinces, ReturnErrorMessages, response);
                response = CheckDetailByDistricts(address, districts, ReturnErrorMessages, response);
                response = CheckDetailByWards(address, wards, ReturnErrorMessages, response);
                if (!response.ErrorMessages.Any())
                {
                    response.Address = address.Detail.Trim() + ", " + wards.SingleOrDefault(w => w.Code.Equals(address.WardCode)).NameWithFullPath;
                    response.IsSuccess = true;
                }
                return response;
            }
            return null;
        }

        private static AddressResponse CheckDetailByProvinces(AddressRequestModel address, Province[] list, bool ReturnErrorMessages = false, AddressResponse response = null)
        {
            var detail = address.Detail.ToLower().Trim();
            if (!list.Any())
                list = ProvincesList.PROVINCES;
            var Names = list.Select(l => l.Name).ToList();
            var NameWithTypes = list.Select(l => l.NameWithType).ToList();
            var Codes = list.Select(l => l.Code).ToList();
            var error = ErrorMessageConstants.ADDRESS_PROVINCE + " " + MessageConstants.MESSAGE_INVALID;
            Names.ForEach(n =>
            {
                if (detail.Contains(n.ToLower().Trim()))
                {
                    var temp = error + $" - {n}";
                    if (ReturnErrorMessages)
                        response.ErrorMessages.Add(temp);
                    else
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[] { temp });
                }
            });
            NameWithTypes.ForEach(nwt =>
            {
                if (detail.Contains(nwt.ToLower().Trim()))
                {
                    var temp = error + $" - {nwt}";
                    if (ReturnErrorMessages)
                        response.ErrorMessages.Add(temp);
                    else
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[] { temp });
                }
            });
            if (!Codes.Contains((int)address.ProvinceCode))
            {
                var temp = ErrorMessageConstants.ADDRESS_CODE + " " + error;
                if (ReturnErrorMessages)
                    response.ErrorMessages.Add(temp);
                else
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[] { temp });
            }
            return response;
        }
        private static AddressResponse CheckDetailByDistricts(AddressRequestModel address, District[] list, bool ReturnErrorMessages = false, AddressResponse response = null)
        {
            var detail = address.Detail.ToLower().Trim();
            if (!list.Any())
                list = DistrictsList.DISTRICTS;
            var NameWithTypes = list.Select(l => l.NameWithType).ToList();
            var NameWithProvince = list.Select(l => l.NameWithProvince).ToList();
            var Code = list.SingleOrDefault(l =>
            l.ParentCode.Equals(address.ProvinceCode) &&
            l.Code.Equals(address.DistrictCode));
            var error = ErrorMessageConstants.ADDRESS_DISTRICT + " " + MessageConstants.MESSAGE_INVALID;
            NameWithTypes.ForEach(nwt =>
            {
                if (detail.Contains(nwt.ToLower().Trim()))
                {
                    var temp = error + $" - {nwt}";
                    if (ReturnErrorMessages)
                        response.ErrorMessages.Add(temp);
                    else
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[] { temp });
                }
            });
            NameWithProvince.ForEach(nwp =>
            {
                if (detail.Contains(nwp.ToLower().Trim()))
                {
                    var temp = error + $" - {nwp}";
                    if (ReturnErrorMessages)
                        response.ErrorMessages.Add(temp);
                    else
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[] { temp });
                }
            });

            if (Code == null)
            {
                var temp = ErrorMessageConstants.ADDRESS_CODE + " " + error;
                if (ReturnErrorMessages)
                    response.ErrorMessages.Add(temp);
                else
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[] { temp });
            }
            return response;
        }
        private static AddressResponse CheckDetailByWards(AddressRequestModel address, Ward[] list, bool ReturnErrorMessages = false, AddressResponse response = null)
        {
            var detail = address.Detail.ToLower().Trim();
            if (!list.Any())
                list = WardsList.WARDS;
            var NameWithTypes = list.Select(l => l.NameWithType).ToList();
            var NameWithFullPaths = list.Select(l => l.NameWithFullPath).ToList();
            var Code = list.SingleOrDefault(l =>
            l.ParentCode.Equals(address.DistrictCode) &&
            l.Code.Equals(address.WardCode));
            var error = ErrorMessageConstants.ADDRESS_WARDS + " " + MessageConstants.MESSAGE_INVALID;
            NameWithTypes.ForEach(nwt =>
            {
                if (detail.Contains(nwt.ToLower().Trim()))
                {
                    var temp = error + $" - {nwt}";
                    if (ReturnErrorMessages)
                        response.ErrorMessages.Add(temp);
                    else
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[] { temp });
                }
            });
            NameWithFullPaths.ForEach(nwfp =>
            {
                if (detail.Contains(nwfp.ToLower().Trim()))
                {
                    var temp = error + $" - {nwfp}";
                    if (ReturnErrorMessages)
                        response.ErrorMessages.Add(temp);
                    else
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[] { temp });
                }
            });
            if (Code == null)
            {
                var temp = ErrorMessageConstants.ADDRESS_CODE + " " + error;
                if (ReturnErrorMessages)
                    response.ErrorMessages.Add(temp);
                else
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[] { temp });
            }
            return response;
        }
    }

    public class AddressResponse
    {
        public string Address { get; set; } = null;
        public List<string> ErrorMessages { get; set; } = new List<string>();
        public bool IsSuccess { get; set; } = false;
    }
}