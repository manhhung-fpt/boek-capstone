using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boek.Core.Constants;
using Boek.Core.Constants.Mobile;
using Boek.Infrastructure.Requests.Campaigns.Mobile;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.Campaigns;
using Boek.Infrastructure.ViewModels.Campaigns.Mobile;
using Boek.Infrastructure.ViewModels.Campaigns.Mobile.Customers;
using Boek.Infrastructure.ViewModels.CampaignStaffs;
using Boek.Infrastructure.ViewModels.Groups;
using Boek.Infrastructure.ViewModels.Organizations;
using Boek.Infrastructure.ViewModels.Users.Issuers;
using Boek.Repository.Interfaces;
using Boek.Service.Commons;
using Boek.Service.Interfaces.Mobile;
using Boek.Service.Utils;
using Boek.Core.Enums;
using Microsoft.AspNetCore.Http;
using Boek.Core.Extensions;
using Boek.Infrastructure.ViewModels.Organizations.Mobile;
using Boek.Infrastructure.ViewModels.BookProducts.Mobile;
using System.Net;
using Boek.Infrastructure.ViewModels.Schedules;

namespace Boek.Service.Services.Mobile
{
    public class CampaignMobileService : ICampaignMobileService
    {
        #region Fields and constructor
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CampaignMobileService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
        #endregion

        #region Get
        public CampaignMobileViewModel GetCampaignMobileById(int id)
        {
            var campaign = _unitOfWork.Campaigns
            .Get(c => c.Id.Equals(id))
            .ProjectTo<CampaignsMobileViewModel>(_mapper.ConfigurationProvider)
            .SingleOrDefault();
            if (campaign == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.CAMPAIGN_ID,
                });
            var _campaign = _mapper.Map<CampaignMobileViewModel>(campaign);
            var invalidBookProductStatuses = new List<byte?>()
            {
                (byte) BookProductStatus.Pending,
                (byte) BookProductStatus.Rejected
            };
            var bookProducts = _unitOfWork.BookProducts.Get(bps => bps.CampaignId.Equals(id) &&
            !invalidBookProductStatuses.Contains(bps.Status))
            .ProjectTo<MobileBookProductsViewModel>(_mapper.ConfigurationProvider)
            .OrderBy(bps => bps.Status)
            .ToList();
            if (bookProducts.Any())
                _campaign.BookProducts = bookProducts.ToList();
            if (campaign.CampaignOrganizations.Any())
            {
                var _organizations = campaign.CampaignOrganizations.Select(cos => new
                {
                    organization = cos.Organization,
                    schedules = cos.Schedules
                });
                _campaign.Organizations = new List<OrganizationsMobileViewModel>();
                foreach (var item in _organizations)
                {
                    var organization = _mapper.Map<OrganizationsMobileViewModel>(item.organization);
                    organization.Schedules = item.schedules;
                    _campaign.Organizations.Add(organization);
                }
            }
            if (campaign.CampaignGroups.Any())
                _campaign.Groups = campaign.CampaignGroups.Select(cl => cl.Group).ToList();
            if (campaign.Participants.Any())
            {
                var _issuers = campaign.Participants.Where(p => p.Status.Equals((byte)ParticipantStatus.Approved) ||
                p.Status.Equals((byte)ParticipantStatus.Accepted)).Select(p => p.Issuer);
                if (_issuers.Any())
                    _campaign.Issuers = _issuers.ToList();
            }
            if (campaign.CampaignLevels.Any())
                _campaign.Levels = campaign.CampaignLevels.Select(cl => cl.Level).ToList();
            return ServiceUtils.GetCampaignDetail(_campaign);
        }

        public BaseResponsePagingModel<CampaignViewModel> GetCampaigns(CampaignMobileRequestModel filter, PagingModel paging)
        {
            var _filter = _mapper.Map<CampaignMobileFilterViewModel>(filter);
            var list = new List<CampaignViewModel>();
            int count = 0;
            var query = GetCampaignParticipantsByFilter(filter);
            query = query.DynamicOtherFilter(_filter);
            if (query.Any())
            {
                var result = query.DynamicOtherFilter(_filter)
                   .PagingQueryable(paging.Page, paging.Size, PageConstant.LimitPaging, PageConstant.LimitPaging);
                count = result.Item1;
                foreach (var campaign in result.Item2)
                {
                    var temp = campaign;
                    temp.Participants = temp.Participants.Where(p =>
                    p.Status.Equals((byte)ParticipantStatus.Approved) ||
                    p.Status.Equals((byte)ParticipantStatus.Accepted)).ToList();
                    list.Add(ServiceUtils.GetResponseDetail(temp));
                }
            }
            return new BaseResponsePagingModel<CampaignViewModel>()
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

        public List<HierarchicalStaffCampaignsViewModel> GetStaffCampaigns(StaffCampaignMobileRequestModel filter)
        {
            var result = new List<HierarchicalStaffCampaignsViewModel>();
            var StaffId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            var Status = filter.CampaignStaffs != null ? filter.CampaignStaffs.Status : (byte)CampaignStaffStatus.Attended;
            var _campaignStaffs = _unitOfWork.CampaignStaffs
            .Get(cs => cs.StaffId.Equals(StaffId) && cs.Status.Equals(Status))
            .ToList();
            if (_campaignStaffs.Any())
            {
                var campaignIds = _campaignStaffs.Select(cs => cs.CampaignId).ToList();
                var list = _unitOfWork.Campaigns.Get(c => campaignIds.Contains(c.Id))
                .ProjectTo<StaffCampaignMobilesViewModel>(_mapper.ConfigurationProvider).ToList();
                var statuses = filter.Status != null ? filter.Status : new List<byte?>()
                {
                    (byte) CampaignStatus.NotStarted,
                    (byte) CampaignStatus.Start,
                    (byte) CampaignStatus.End,
                    (byte) CampaignStatus.Cancelled,
                };
                var filterStatus = GetStaffFilterStatus(statuses);
                foreach (var item in filterStatus)
                {
                    var temp = GenerateStaffCampaignsByStatus(list, filter, item.Key);
                    if (temp != null)
                        result.Add(new HierarchicalStaffCampaignsViewModel()
                        {
                            Title = item.Value,
                            Campaigns = temp
                        });
                }
            }
            if (result.Any())
                result.ForEach(c => ServiceUtils.GetResponseDetails(c));
            return result;
        }

        public CustomerCampaignMobileViewModel GetCustomerCampaigns(int size = 5)
        {
            var _campaigns = _unitOfWork.Campaigns.Get(c =>
            c.Status.Equals((byte)CampaignStatus.NotStarted) ||
            c.Status.Equals((byte)CampaignStatus.Start))
            .ProjectTo<CampaignViewModel>(_mapper.ConfigurationProvider)
            .ToList();
            if (_campaigns.Any())
            {
                var list = new CustomerCampaignMobileViewModel()
                {
                    hierarchicalCustomerCampaigns = new List<HierarchicalCustomerCampaignMobileViewModel>(),
                    unhierarchicalCustomerCampaigns = new List<UnhierarchicalCustomerCampaignMobileViewModel>()
                };
                if (ServiceUtils.CheckRole(_httpContextAccessor, BoekRole.Customer))
                {
                    var UserId = ServiceUtils.GetUserInfo(_httpContextAccessor);
                    var customer = _unitOfWork.Customers.Get(c => c.Id.Equals(UserId))
                    .ProjectTo<CustomerMobileViewModel>(_mapper.ConfigurationProvider)
                    .SingleOrDefault();
                    if (customer != null)
                    {
                        customer.Organizations = _mapper.Map<List<BasicOrganizationViewModel>>(_unitOfWork.CustomerOrganizations.GetOrganizations(customer.Id));
                        customer.Groups = _mapper.Map<List<BasicGroupViewModel>>(_unitOfWork.CustomerGroups.GetGroups(customer.Id));
                        //Address
                        var _TempCampaigns = GenerateCustomerCampaignsByAddress(_campaigns, customer.User.Address, size);
                        if (_TempCampaigns != null)
                        {
                            list.unhierarchicalCustomerCampaigns.Add(new UnhierarchicalCustomerCampaignMobileViewModel()
                            {
                                Title = MobileConstants.TITLE_CUSTOMER_CAMPAIGN_ADDRESS,
                                campaigns = _TempCampaigns
                            });
                        }
                        //Level
                        _TempCampaigns = GenerateCustomerCampaignsByLevel(_campaigns, customer.LevelId);
                        if (_TempCampaigns != null)
                        {
                            list.unhierarchicalCustomerCampaigns.Add(new UnhierarchicalCustomerCampaignMobileViewModel()
                            {
                                Title = MobileConstants.TITLE_CUSTOMER_CAMPAIGN_LEVEL,
                                campaigns = _TempCampaigns
                            });
                        }
                        //Organization
                        if (customer.Organizations.Any())
                        {
                            var _organizations = GenerateCustomerCampaignsByOrganization(_campaigns, customer.Organizations);
                            if (_organizations != null)
                                list.hierarchicalCustomerCampaigns.Add(_organizations);
                        }
                        //Group
                        if (customer.Groups.Any())
                        {
                            var _groups = GenerateCustomerCampaignsByGroup(_campaigns, customer.Groups);
                            if (_groups != null)
                                list.hierarchicalCustomerCampaigns.Add(_groups);
                        }
                    }
                }
                //Offline
                var temp = GenerateOfflineCampaigns(_campaigns, size);
                if (temp != null)
                    list.hierarchicalCustomerCampaigns.Add(temp);
                //Online
                temp = GenerateOnlineCampaigns(_campaigns, size);
                if (temp != null)
                    list.hierarchicalCustomerCampaigns.Add(temp);
                return ServiceUtils.GetResponseDetail(list);
            }
            return null;
        }
        #endregion

        #region Utils
        private void CheckCampaignDate(DateTime? startDate, DateTime? endDate)
        {
            if (startDate.HasValue && endDate.HasValue)
            {
                if (DateTime.Compare((DateTime)startDate, (DateTime)endDate) > 0)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                {
                    ErrorMessageConstants.CAMPAIGN_START_DATE,
                    ErrorMessageConstants.CAMPAIGN_NOT_LATER_START_DATE,
                    ErrorMessageConstants.CAMPAIGN_END_DATE
                });
            }
        }

        private IDictionary<byte?, string> GetStaffFilterStatus(List<byte?> status)
        {
            if (!status.Any())
                return null;
            var result = new Dictionary<byte?, string>();
            var Titles = new List<string>()
            {
                MobileConstants.TITLE_CAMPAIGN_NOT_START,
                MobileConstants.TITLE_CAMPAIGN_STARTED,
                MobileConstants.TITLE_CAMPAIGN_ENDED,
                MobileConstants.TITLE_CAMPAIGN_CANCELLED,
                MobileConstants.TITLE_CAMPAIGN_POSTPONED,
            };
            status.OrderBy(s => s).ToList().ForEach(s =>
            {
                var title = ServiceUtils.GetTitleByStatus(s, Titles);
                if (!string.IsNullOrEmpty(title))
                    result.Add(s, title);
            });
            return result.Any() ? result : null;
        }

        private List<StaffCampaignMobilesViewModel> GenerateStaffCampaignsByStatus(List<StaffCampaignMobilesViewModel> result, StaffCampaignMobileRequestModel filter, byte? status)
        {
            var EnumStatus = StatusExtension<CampaignStatus>.GetEnumStatus(status);
            var list = GetCampaignByStatus(result, EnumStatus);
            if (list.Any())
            {
                if (filter.IsHomePage)
                    list = list.Take(10).ToList();
                var _filter = _mapper.Map<StaffCampaignMobileFilterRequestModel>(filter);
                list = GetCampaignIssuers(list);
                list = GetCampaignStaffs(list);
                list = GetCampaignSchedules(list);
                var query = list.AsQueryable().DynamicOtherFilter(_filter).ToList();
                if (query.Any())
                    return query;
            }
            return null;
        }
        private List<StaffCampaignMobilesViewModel> GetCampaignByStatus(List<StaffCampaignMobilesViewModel> result, CampaignStatus status)
        => result.Where(c => c.Status.Equals((byte)status)).ToList();
        private List<CampaignViewModel> GetCampaignByStatus(List<CampaignViewModel> result, CampaignStatus status)
        => result.Where(c => c.Status.Equals((byte)status)).ToList();
        private List<CampaignViewModel> GetCampaignByStatus(List<CampaignViewModel> result, byte? format, CampaignStatus status, int size = 5)
        {
            result = result.Where(c => c.Status.Equals((byte)status) && c.Format.Equals(format)).Take(size).ToList();
            if (result.Any())
                result = result.Take(size).ToList();
            return result;
        }

        private List<StaffCampaignMobilesViewModel> GetCampaignIssuers(List<StaffCampaignMobilesViewModel> list)
        {
            list.ForEach(c =>
            {
                var issuers = _unitOfWork.Participants.GetIssuers(c.Id);
                if (issuers.Any())
                    c.Issuers = _mapper.Map<List<IssuerViewModel>>(issuers);
            });
            return list;
        }
        private List<StaffCampaignMobilesViewModel> GetCampaignStaffs(List<StaffCampaignMobilesViewModel> list)
        {
            list.ForEach(c =>
            {
                var campaignStaffs = _unitOfWork.CampaignStaffs.Get(cs => cs.CampaignId.Equals(c.Id)).ToList();
                if (campaignStaffs.Any())
                    c.CampaignStaffs = _mapper.Map<List<MobileCampaignStaffsViewModel>>(campaignStaffs);
            });
            return list;
        }
        private List<StaffCampaignMobilesViewModel> GetCampaignSchedules(List<StaffCampaignMobilesViewModel> list)
        {
            var scheduleStatus = new List<byte?>() { (byte)CampaignStatus.Start };
            list.ForEach(c =>
            {
                var temp = _unitOfWork.Schedules.GetSchedules(c.Id, scheduleStatus);
                if (temp != null)
                    c.Schedules = ServiceUtils.GetResponseDetails(_mapper.Map<List<SchedulesViewModel>>(temp));
            });
            return list;
        }

        private HierarchicalCustomerCampaignMobileViewModel GenerateOfflineCampaigns(List<CampaignViewModel> result, int size = 5)
        {
            var campaigns = new HierarchicalCustomerCampaignMobileViewModel()
            {
                Title = MobileConstants.TITLE_CUSTOMER_OFFLINE_CAMPAIGN,
                subHierarchicalCustomerCampaigns = new List<SubHierarchicalCustomerCampaignMobileViewModel>()
            };
            var format = (byte)CampaignFormat.Offline;
            var status = (byte)CampaignStatus.NotStarted;
            //Not started
            var list = GetCampaignByStatus(result, format, CampaignStatus.NotStarted, size);
            if (list.Any())
            {
                var StatusName = MobileConstants.TITLE_CUSTOMER_NOT_STARTED_CAMPAIGN;
                campaigns.subHierarchicalCustomerCampaigns.Add(new SubHierarchicalCustomerCampaignMobileViewModel()
                {
                    SubTitle = StatusName,
                    Status = status,
                    Format = format,
                    campaigns = list
                });
            }
            //Start
            status = (byte)CampaignStatus.Start;
            list = GetCampaignByStatus(result, format, CampaignStatus.Start, size);
            if (list.Any())
            {
                var StatusName = MobileConstants.TITLE_CUSTOMER_START_CAMPAIGN;
                campaigns.subHierarchicalCustomerCampaigns.Add(new SubHierarchicalCustomerCampaignMobileViewModel()
                {
                    SubTitle = StatusName,
                    Status = status,
                    Format = format,
                    campaigns = list
                });
            }
            return campaigns.subHierarchicalCustomerCampaigns.Any() ? campaigns : null;
        }

        private HierarchicalCustomerCampaignMobileViewModel GenerateOnlineCampaigns(List<CampaignViewModel> result, int size = 5)
        {
            var campaigns = new HierarchicalCustomerCampaignMobileViewModel()
            {
                Title = MobileConstants.TITLE_CUSTOMER_ONLINE_CAMPAIGN,
                subHierarchicalCustomerCampaigns = new List<SubHierarchicalCustomerCampaignMobileViewModel>()
            };
            var format = (byte)CampaignFormat.Online;
            var status = (byte)CampaignStatus.NotStarted;
            //Not started
            var list = GetCampaignByStatus(result, format, CampaignStatus.NotStarted, size);
            if (list.Any())
            {
                var StatusName = MobileConstants.TITLE_CUSTOMER_NOT_STARTED_CAMPAIGN;
                campaigns.subHierarchicalCustomerCampaigns.Add(new SubHierarchicalCustomerCampaignMobileViewModel()
                {
                    SubTitle = StatusName,
                    Status = status,
                    Format = format,
                    campaigns = list
                });
            }
            //Start
            status = (byte)CampaignStatus.Start;
            list = GetCampaignByStatus(result, format, CampaignStatus.Start, size);
            if (list.Any())
            {
                var StatusName = MobileConstants.TITLE_CUSTOMER_START_CAMPAIGN;
                campaigns.subHierarchicalCustomerCampaigns.Add(new SubHierarchicalCustomerCampaignMobileViewModel()
                {
                    SubTitle = StatusName,
                    Status = status,
                    Format = format,
                    campaigns = list
                });
            }
            return campaigns.subHierarchicalCustomerCampaigns.Any() ? campaigns : null;
        }

        private HierarchicalCustomerCampaignMobileViewModel GenerateCustomerCampaignsByOrganization(List<CampaignViewModel> result, List<BasicOrganizationViewModel> Organizations, int size = 5)
        {
            var _organizations = new HierarchicalCustomerCampaignMobileViewModel()
            {
                Title = MobileConstants.TITLE_CUSTOMER_CAMPAIGN_ORGANIZATION,
                subHierarchicalCustomerCampaigns = new List<SubHierarchicalCustomerCampaignMobileViewModel>()
            };
            result = result.Take(size).ToList();
            Organizations.ForEach(ois =>
            {
                var list = new List<CampaignViewModel>();
                result.ForEach(c =>
                {
                    if (c.CampaignOrganizations.Any(cos =>
                    ois.Id.Equals(cos.OrganizationId)) &&
                    !list.Contains(c))
                        list.Add(c);
                });
                if (list.Any())
                {
                    _organizations.subHierarchicalCustomerCampaigns.Add(new SubHierarchicalCustomerCampaignMobileViewModel()
                    {
                        SubTitle = ois.Name,
                        OrganizationId = ois.Id,
                        campaigns = list
                    });
                }
            });
            return _organizations.subHierarchicalCustomerCampaigns.Any() ? _organizations : null;
        }
        private HierarchicalCustomerCampaignMobileViewModel GenerateCustomerCampaignsByGroup(List<CampaignViewModel> result, List<BasicGroupViewModel> Groups, int size = 5)
        {
            var _groups = new HierarchicalCustomerCampaignMobileViewModel()
            {
                Title = MobileConstants.TITLE_CUSTOMER_CAMPAIGN_GROUP,
                subHierarchicalCustomerCampaigns = new List<SubHierarchicalCustomerCampaignMobileViewModel>()
            };
            result = result.Take(size).ToList();
            Groups.ForEach(gs =>
            {
                var list = new List<CampaignViewModel>();
                result.ForEach(c =>
                {
                    if (c.CampaignGroups.Any(cgs => gs.Id.Equals(cgs.GroupId)) && !list.Contains(c))
                        list.Add(c);
                });
                if (list.Any())
                {
                    _groups.subHierarchicalCustomerCampaigns.Add(new SubHierarchicalCustomerCampaignMobileViewModel()
                    {
                        SubTitle = gs.Name,
                        OrganizationId = gs.Id,
                        campaigns = list
                    });
                }
            });
            return _groups.subHierarchicalCustomerCampaigns.Any() ? _groups : null;
        }
        private List<CampaignViewModel> GenerateCustomerCampaignsByLevel(List<CampaignViewModel> result, int? LevelId, int size = 5)
        {
            var list = new List<CampaignViewModel>();
            result.Take(size).ToList().ForEach(c =>
            {
                if (c.CampaignLevels.Any(cls => cls.LevelId.Equals(LevelId)) && !list.Contains(c))
                    list.Add(c);
            });
            return list.Any() ? list : null;
        }
        private List<CampaignViewModel> GenerateCustomerCampaignsByAddress(List<CampaignViewModel> result, string Address, int size = 5)
        {
            var province = GetProvinceFromAddress(Address);
            var list = new List<CampaignViewModel>();
            if (string.IsNullOrEmpty(province))
                return null;
            result = result.Where(c => c.Format.Equals((byte)CampaignFormat.Offline)).Take(10).ToList();
            result.Take(size).ToList().ForEach(c =>
            {
                if (!String.IsNullOrEmpty(c.Address))
                {
                    if (c.Address.Trim().ToLower().Contains(province) && !list.Contains(c))
                        list.Add(c);
                }
            });
            return list.Any() ? list : null;
        }

        private string GetProvinceFromAddress(string Address)
        {
            var list = Address.Split(",").ToList();
            if (!list.Any())
                return null;
            return list[list.Count() - 1].Trim().ToLower();
        }

        private IQueryable<CampaignViewModel> GetCampaignParticipantsByFilter(CampaignMobileRequestModel filter)
        {
            IQueryable<CampaignViewModel> result = _unitOfWork.Campaigns.Get()
            .ProjectTo<CampaignViewModel>(_mapper.ConfigurationProvider);
            if (filter.Participants != null)
            {
                if (filter.Participants.IssuerIds != null)
                {
                    if (filter.Participants.IssuerIds.Any())
                    {
                        var Status = new List<byte?>()
                            {
                                (byte)ParticipantStatus.Accepted,
                                (byte)ParticipantStatus.Approved
                            };
                        result = result.Where(c => c.Participants.Any(p =>
                        filter.Participants.IssuerIds.Contains(p.IssuerId) &&
                        Status.Contains(p.Status)));
                    }
                }
            }
            return result;
        }
        #endregion
    }
}