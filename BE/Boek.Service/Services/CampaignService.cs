using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boek.Core.Constants;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.Campaigns;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.Campaigns;
using Boek.Infrastructure.ViewModels.Users;
using Boek.Repository.Interfaces;
using Boek.Service.Commons;
using Boek.Service.Interfaces;
using Boek.Service.Utils;
using Boek.Core.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Boek.Infrastructure.Requests.Addresses;
using Boek.Infrastructure.Requests.CampaignOrganizations;
using Boek.Infrastructure.ViewModels.Schedules;
using System.Net;

namespace Boek.Service.Services
{
    public class CampaignService : ICampaignService
    {
        #region Fields and constructor
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CampaignService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Gets

        #region General
        public CampaignViewModel GetCampaignById(int id)
        {
            var _campaign = _unitOfWork.Campaigns.Get(id);
            if (_campaign == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.CAMPAIGN_ID,
                });
            return GetRespond(_campaign, BoekRole.Customer);
        }
        public BaseResponsePagingModel<CampaignViewModel> GetCampaigns(CampaignRequestModel filter, PagingModel paging)
        {
            var _filter = _mapper.Map<CampaignViewModel>(filter);
            var result = _unitOfWork.Campaigns.Get()
                    .ProjectTo<CampaignViewModel>(_mapper.ConfigurationProvider)
                    .DynamicFilter(_filter)
                    .PagingQueryable(paging.Page, paging.Size, PageConstant.LimitPaging, PageConstant.LimitPaging);
            var list = new List<CampaignViewModel>();
            foreach (var campaign in result.Item2)
            {
                var temp = campaign;
                temp.Participants = temp.Participants.Where(p =>
                p.Status.Equals((byte)ParticipantStatus.Approved) ||
                p.Status.Equals((byte)ParticipantStatus.Accepted)).ToList();
                list.Add(ServiceUtils.GetResponseDetail(temp, filter.WithAddressDetail));
            }
            return new BaseResponsePagingModel<CampaignViewModel>()
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
        #endregion

        #region Admin

        public CampaignViewModel GetCampaignByIdByAdmin(int id, bool WithAddressDetail = false)
        {
            var _campaign = _unitOfWork.Campaigns.Get(id);
            if (_campaign == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.CAMPAIGN_ID,
                });
            return GetRespond(_campaign, BoekRole.Admin, WithAddressDetail);
        }

        public BaseResponsePagingModel<CampaignViewModel> GetCampaignsByAdmin(CampaignRequestModel filter, PagingModel paging)
        {
            var _filter = _mapper.Map<CampaignViewModel>(filter);
            var result = _unitOfWork.Campaigns.Get()
                    .ProjectTo<CampaignViewModel>(_mapper.ConfigurationProvider)
                    .DynamicFilter(_filter)
                    .PagingQueryable(paging.Page, paging.Size, PageConstant.LimitPaging, PageConstant.LimitPaging);
            var list = new List<CampaignViewModel>();
            foreach (var campaign in result.Item2.ToList())
            {
                var temp = campaign;
                list.Add(ServiceUtils.GetResponseDetail(temp, filter.WithAddressDetail));
            }
            return new BaseResponsePagingModel<CampaignViewModel>()
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

        public IEnumerable<UserViewModel> GetUnparticipatedIssuers(int id)
        {
            var _campaign = _unitOfWork.Campaigns.Get(c => c.Id.Equals(id))
            .Include(c => c.Participants)
            .SingleOrDefault();
            if (_campaign == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                        ErrorMessageConstants.NOT_FOUND,
                        ErrorMessageConstants.CAMPAIGN_ID,
                });
            var statuses = new List<byte?>()
            {
                (byte)ParticipantStatus.PendingInvitation,
                (byte)ParticipantStatus.PendingRequest,
                (byte)ParticipantStatus.Accepted,
                (byte)ParticipantStatus.Approved
            };
            var _participants = _campaign.Participants.Where(p => statuses.Contains(p.Status));
            var users = new List<UserViewModel>();
            if (_participants.Any())
            {
                var _issuerIds = _participants.Select(p => p.IssuerId);
                users = _unitOfWork.Users.Get(u => u.Role.Equals((byte)BoekRole.Issuer) && !_issuerIds.Contains(u.Id))
                .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider).ToList();
            }
            else
            {
                users = _unitOfWork.Users.Get(u => u.Role.Equals((byte)BoekRole.Issuer))
                .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider).ToList();
            }

            if (users.Any())
                users.ForEach(u => u = ServiceUtils.GetResponseDetail(u));
            return users;
        }
        #endregion

        #region Issuer
        public CampaignViewModel GetCampaignByIdByIssuer(int id)
        {
            var _campaign = _unitOfWork.Campaigns
                .Get(c => c.Id.Equals(id))
                .Include(c => c.Participants)
                .SingleOrDefault();
            if (_campaign == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.CAMPAIGN_ID,
                });
            var IssuerId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            //TO-DO: Edit after having participant
            /*if (!_campaign.Participants.Any(p => p.IssuerId.Equals(IssuerId)))
                BoekErrorMessage.ShowErrorMessage(400, new string[]
                {
                        ErrorMessageConstants.INVALID_OTHER_ORGANIZATION_ISSUER
                });*/
            return GetRespond(_campaign);
        }
        public BaseResponsePagingModel<CampaignViewModel> GetCampaignsByIssuer(CampaignRequestModel filter, PagingModel paging)
        {
            var IssuerId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            var validStatus = new List<byte?>()
            {
                (byte) ParticipantStatus.Accepted,
                (byte) ParticipantStatus.Approved
            };
            var _filter = _mapper.Map<CampaignViewModel>(filter);
            var list = new List<CampaignViewModel>();
            var result = _unitOfWork.Campaigns.Get(c =>
            c.Participants.Any(p => p.IssuerId.Equals(IssuerId) &&
            validStatus.Contains(p.Status)))
            .ProjectTo<CampaignViewModel>(_mapper.ConfigurationProvider)
            .DynamicFilter(_filter)
            .PagingQueryable(paging.Page, paging.Size, PageConstant.LimitPaging, PageConstant.LimitPaging);
            if (result.Item1 > 0)
                result.Item2.ToList().ForEach(c => list.Add(ServiceUtils.GetResponseDetail(c, filter.WithAddressDetail)));
            return new BaseResponsePagingModel<CampaignViewModel>()
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

        public CampaignViewModel GetOtherCampaignByIdByIssuer(int id)
        {
            var _campaign = _unitOfWork.Campaigns
                .Get(c => c.Id.Equals(id))
                .Include(c => c.Participants)
                .SingleOrDefault();
            if (_campaign == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                        ErrorMessageConstants.NOT_FOUND,
                        ErrorMessageConstants.CAMPAIGN_ID,
                });
            var IssuerId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            //TO-DO: Edit after having participant
            /*if (_campaign.Participants.Any(p => p.IssuerId.Equals(IssuerId)))
                BoekErrorMessage.ShowErrorMessage(400, new string[]
                {
                        ErrorMessageConstants.INVALID_OTHER_ORGANIZATION_ISSUER
                });*/
            return GetRespond(_campaign);
        }

        public BaseResponsePagingModel<CampaignViewModel> GetOtherCampaignsByIssuer(CampaignRequestModel filter, PagingModel paging)
        {
            var IssuerId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            var validStatus = new List<byte?>()
            {
                (byte) ParticipantStatus.Accepted,
                (byte) ParticipantStatus.Approved
            };
            var list = new List<CampaignViewModel>();
            var participatedCampaigns = _unitOfWork.Campaigns.Get(c =>
            c.Participants.Any(p => p.IssuerId.Equals(IssuerId) &&
            validStatus.Contains(p.Status))).Select(c => c.Id).ToList();
            var _filter = _mapper.Map<CampaignViewModel>(filter);
            (int, IQueryable<CampaignViewModel>) result;

            if (participatedCampaigns.Any())
            {
                result = _unitOfWork.Campaigns.Get(c => !participatedCampaigns.Contains(c.Id))
                .ProjectTo<CampaignViewModel>(_mapper.ConfigurationProvider)
                .DynamicFilter(_filter)
                .PagingQueryable(paging.Page, paging.Size, PageConstant.LimitPaging, PageConstant.LimitPaging);
            }
            else
            {
                result = _unitOfWork.Campaigns.Get()
                .ProjectTo<CampaignViewModel>(_mapper.ConfigurationProvider)
                .DynamicFilter(_filter)
                .PagingQueryable(paging.Page, paging.Size, PageConstant.LimitPaging, PageConstant.LimitPaging);
            }
            if (result.Item1 > 0)
                result.Item2.ToList().ForEach(c => list.Add(ServiceUtils.GetResponseDetail(c, filter.WithAddressDetail)));
            return new BaseResponsePagingModel<CampaignViewModel>()
            {
                Metadata = new PagingMetadata()
                {
                    Page = paging.Page,
                    Size = paging.Size,
                    Total = result.Item1,
                },
                Data = list
            };
        }

        #endregion

        #endregion

        #region Offline Campaign
        public CampaignViewModel CreateOfflineCampaign(CreateOfflineCampaignRequestModel createOfflineCampaign)
        {
            var _campaign = _mapper.Map<Campaign>(createOfflineCampaign);
            _campaign = CheckCampaignOrganizationAddress(_campaign, createOfflineCampaign.AddressRequest, createOfflineCampaign.CampaignOrganizations);
            var format = CampaignFormat.Offline;
            CheckCampaign(ref _campaign, format);
            _campaign.Code = Guid.NewGuid();
            var status = (byte)CampaignStatus.NotStarted;
            _campaign.Status = (byte)CampaignStatus.NotStarted;
            _campaign.Format = (byte)format;
            _campaign.CreatedDate = DateTime.Now;
            _campaign.CampaignOrganizations.ToList().ForEach(cos => cos.Schedules.OrderBy(s => s.StartDate));
            _campaign = ChangeCampaignOrganizationScheduleStatus(_campaign, status);
            var organizations = _campaign.CampaignOrganizations.ToList();
            _unitOfWork.Campaigns.Create(_campaign);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.INSERT,
                    ErrorMessageConstants.CAMPAIGN.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            _campaign = _unitOfWork.Campaigns
                .Get(c => c.Code.Equals(_campaign.Code))
                .SingleOrDefault();
            AddCreateCampaignOrganizations(_campaign.Id, organizations);
            return GetRespond(_campaign);
        }

        public CampaignViewModel UpdateOfflineCampaign(UpdateOfflineCampaignRequestModel updateOfflineCampaign)
        {
            var _campaign = _unitOfWork.Campaigns.Get(c => c.Id.Equals(updateOfflineCampaign.Id))
            .Include(Campaign => Campaign.CampaignOrganizations)
            .ThenInclude(campaignOrganization => campaignOrganization.Schedules)
            .SingleOrDefault();
            if (_campaign == null)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.CAMPAIGN_ID,
                });
            }
            var _updateCampaign = _mapper.Map<Campaign>(updateOfflineCampaign);
            _updateCampaign = CheckCampaignOrganizationAddress(_updateCampaign, updateOfflineCampaign.AddressRequest, updateOfflineCampaign.CampaignOrganizations);
            var format = CampaignFormat.Offline;
            CheckCampaign(ref _updateCampaign, format, false, _campaign);
            var _campaignOthers = new Campaign()
            {
                Id = _updateCampaign.Id,
                CampaignCommissions = _updateCampaign.CampaignCommissions,
                CampaignOrganizations = _updateCampaign.CampaignOrganizations,
            };
            var basicUpdate = _mapper.Map<BasicUpdateCampaignRequestModel>(updateOfflineCampaign);
            _mapper.Map(basicUpdate, _campaign);
            _campaign.Address = _updateCampaign.Address;
            _campaign.IsRecurring = _updateCampaign.IsRecurring;
            _campaign.UpdatedDate = DateTime.Now;
            _unitOfWork.Campaigns.Update(_campaign);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.UPDATE,
                    ErrorMessageConstants.CAMPAIGN.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            _campaign = _unitOfWork.Campaigns.Get(updateOfflineCampaign.Id);
            UpdateCampaignOrganizations(_campaignOthers.CampaignOrganizations.ToList(), _campaignOthers.Id);
            UpdateCampaignCommissions(_campaignOthers.CampaignCommissions.ToList(), _campaignOthers.Id);
            return GetRespond(_campaign);
        }

        public CampaignViewModel CancelOfflineCampaign(int id)
        {
            var _campaign = _unitOfWork.Campaigns.Get(c => c.Id.Equals(id))
            .Include(c => c.CampaignOrganizations)
            .SingleOrDefault();
            if (_campaign == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.CAMPAIGN_ID,
                });
            var _format = CampaignFormat.Offline;
            CheckCampaignFormat(_campaign.Format, _format);
            CheckCancelDate((DateTime)_campaign.StartDate, (DateTime)_campaign.EndDate);
            var status = (byte)CampaignStatus.Cancelled;
            _campaign.Status = status;
            _campaign = ChangeCampaignOrganizationScheduleStatus(_campaign, status);
            _campaign.UpdatedDate = DateTime.Now;
            _unitOfWork.Campaigns.Update(_campaign);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.CANCEL,
                    ErrorMessageConstants.CAMPAIGN.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            return GetRespond(_campaign);
        }

        public CampaignViewModel PostponeOfflineCampaign(int id)
        {
            var result = CheckPostponedCampaign(id, CampaignFormat.Offline);
            var campaign = _mapper.Map<Campaign>(result);
            _unitOfWork.Campaigns.Update(campaign);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.POSTPONE,
                    ErrorMessageConstants.CAMPAIGN.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            UpdateCampaignOrganizationByPostponedCampaign(result);
            return GetRespond(campaign);
        }

        public CampaignViewModel RestartOfflineCampaign(int id)
        {
            var result = CheckRestartedCampaign(id, CampaignFormat.Offline);
            var campaign = _mapper.Map<Campaign>(result);
            _unitOfWork.Campaigns.Update(campaign);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.RESTART,
                    ErrorMessageConstants.CAMPAIGN.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            UpdateCampaignOrganizationRestartCampaign(result);
            return GetRespond(campaign);
        }
        #endregion

        #region Online Campaign
        public CampaignViewModel CreateOnlineCampaign(CreateOnlineCampaignRequestModel createOnlineCampaign)
        {
            var _campaign = _mapper.Map<Campaign>(createOnlineCampaign);
            var format = CampaignFormat.Online;
            GetCampaignGroups(ref _campaign, createOnlineCampaign.Groups);
            GetCampaignLevels(ref _campaign, createOnlineCampaign.Levels);
            CheckCampaign(ref _campaign, format);
            _campaign.Code = Guid.NewGuid();
            _campaign.Status = (byte)CampaignStatus.NotStarted;
            _campaign.Format = (byte)format;
            _campaign.CreatedDate = DateTime.Now;
            var groups = _campaign.CampaignGroups.ToList();
            var levels = _campaign.CampaignLevels.ToList();
            _unitOfWork.Campaigns.Create(_campaign);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.INSERT,
                    ErrorMessageConstants.CAMPAIGN.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            _campaign = _unitOfWork.Campaigns
                .Get(c => c.Code.Equals(_campaign.Code))
                .SingleOrDefault();
            AddCreateCampaignGroups(_campaign.Id, groups);
            AddCreateCampaignLevels(_campaign.Id, levels);
            return GetRespond(_campaign);
        }

        public CampaignViewModel UpdateOnlineCampaign(UpdateOnlineCampaignRequestModel updateOnlineCampaign)
        {
            var _campaign = _unitOfWork.Campaigns.Get(updateOnlineCampaign.Id);
            if (_campaign == null)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                        ErrorMessageConstants.NOT_FOUND,
                        ErrorMessageConstants.CAMPAIGN_ID,
                });
            }
            var _updateCampaign = _mapper.Map<Campaign>(updateOnlineCampaign);
            var format = CampaignFormat.Online;
            GetCampaignGroups(ref _updateCampaign, updateOnlineCampaign.Groups);
            GetCampaignLevels(ref _updateCampaign, updateOnlineCampaign.Levels);
            CheckCampaign(ref _updateCampaign, format, false, _campaign);
            var _campaignOthers = new Campaign()
            {
                Id = _updateCampaign.Id,
                CampaignCommissions = _updateCampaign.CampaignCommissions,
                CampaignGroups = _updateCampaign.CampaignGroups,
                CampaignLevels = _updateCampaign.CampaignLevels
            };
            var basicUpdate = _mapper.Map<BasicUpdateCampaignRequestModel>(updateOnlineCampaign);
            _mapper.Map(basicUpdate, _campaign);
            _campaign.UpdatedDate = DateTime.Now;
            _unitOfWork.Campaigns.Update(_campaign);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.UPDATE,
                    ErrorMessageConstants.CAMPAIGN.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            _campaign = _unitOfWork.Campaigns
                .Get(c => c.Code.Equals(_campaign.Code))
                .SingleOrDefault();
            UpdateCampaignCommissions(_campaignOthers.CampaignCommissions.ToList(), _campaignOthers.Id);
            UpdateCampaignGroups(_campaignOthers.CampaignGroups.ToList(), _campaignOthers.Id);
            UpdateCampaignLevels(_campaignOthers.CampaignLevels.ToList(), _campaignOthers.Id);
            return GetRespond(_campaign);
        }

        public CampaignViewModel CancelOnlineCampaign(int id)
        {
            var _campaign = _unitOfWork.Campaigns.Get(id);
            if (_campaign == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.CAMPAIGN_ID,
                });
            var _format = CampaignFormat.Online;
            CheckCampaignFormat(_campaign.Format, _format);
            CheckCancelDate((DateTime)_campaign.StartDate, (DateTime)_campaign.EndDate);
            var status = (byte)CampaignStatus.Cancelled;
            _campaign.Status = status;
            _campaign.UpdatedDate = DateTime.Now;
            _unitOfWork.Campaigns.Update(_campaign);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.CANCEL,
                    ErrorMessageConstants.CAMPAIGN.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            return GetRespond(_campaign);
        }

        public CampaignViewModel PostponeOnlineCampaign(int id)
        {
            var result = CheckPostponedCampaign(id, CampaignFormat.Online);
            var campaign = _mapper.Map<Campaign>(result);
            _unitOfWork.Campaigns.Update(campaign);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.POSTPONE,
                    ErrorMessageConstants.CAMPAIGN.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            return GetRespond(campaign);
        }

        public CampaignViewModel RestartOnlineCampaign(int id)
        {
            var result = CheckRestartedCampaign(id, CampaignFormat.Online);
            var campaign = _mapper.Map<Campaign>(result);
            _unitOfWork.Campaigns.Update(campaign);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.RESTART,
                    ErrorMessageConstants.CAMPAIGN.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            return GetRespond(campaign);
        }
        #endregion

        #region Update started campaign
        public CampaignViewModel UpdateStartedCampaign(UpdateCampaignRequestModel updateCampaign)
        {
            var _campaign = CheckUpdatedStartedCampaign(_mapper.Map<Campaign>(updateCampaign));
            _mapper.Map(updateCampaign, _campaign);
            _unitOfWork.Campaigns.Update(_campaign);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.UPDATE,
                    ErrorMessageConstants.CAMPAIGN.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            return GetRespond(_campaign);
        }
        #endregion

        #region Campaign Schedules
        public BaseResponsePagingModel<CampaignSchedulesViewModel> GetCampaignSchedules(CampaignRequestModel filter, PagingModel paging, List<byte?> scheduleStatus, bool WithOccurringProvinces = false)
        {
            if (!scheduleStatus.Any())
                scheduleStatus = new List<byte?>() { (byte)CampaignStatus.Start };
            var StaffId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            var list = new List<CampaignSchedulesViewModel>();
            var count = 0;
            var _filter = _mapper.Map<CampaignSchedulesViewModel>(filter);
            var query = _unitOfWork.Campaigns.Get(c => c.Status.Equals((byte)CampaignStatus.Start) && c.CampaignStaffs.Any(cs => StaffId.Equals(cs.StaffId) && cs.Status.Equals((byte)CampaignStaffStatus.Attended)))
                        .ProjectTo<CampaignSchedulesViewModel>(_mapper.ConfigurationProvider)
                        .DynamicFilter(_filter);
            if (query.Any())
            {
                var result = query.PagingQueryable(paging.Page, paging.Size, PageConstant.LimitPaging, PageConstant.LimitPaging);
                if (result.Item2.Any())
                {
                    count = result.Item1;
                    list = result.Item2.ToList();
                    list.ForEach(c =>
                    {
                        var temp = _unitOfWork.Schedules.GetSchedules(c.Id, scheduleStatus);
                        if (temp != null)
                        {
                            c.Schedules = _mapper.Map<List<SchedulesViewModel>>(temp);
                            if (WithOccurringProvinces)
                                c.OccurringProvinces = _unitOfWork.Schedules.GetProvincesFromSchedules(temp);
                        }
                        c = ServiceUtils.GetResponseDetail(c);
                    });
                }
            }
            return new BaseResponsePagingModel<CampaignSchedulesViewModel>()
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

        public CampaignSchedulesViewModel GetCampaignSchedule(int id, List<byte?> scheduleStatus, bool WithOccurringProvinces = false)
        {
            var _campaign = _unitOfWork.Campaigns.Get(c => c.Id.Equals(id) &&
            c.CampaignOrganizations.Any(co => co.Schedules.Any()))
            .ProjectTo<CampaignSchedulesViewModel>(_mapper.ConfigurationProvider)
            .SingleOrDefault();
            if (_campaign == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.CAMPAIGN_ID,
                });
            var temp = _unitOfWork.Schedules.GetSchedules(id, scheduleStatus);
            if (temp != null)
            {
                _campaign.Schedules = _mapper.Map<List<SchedulesViewModel>>(temp);
                if (WithOccurringProvinces)
                    _campaign.OccurringProvinces = _unitOfWork.Schedules.GetProvincesFromSchedules(temp);
            }
            return ServiceUtils.GetResponseDetail(_campaign);
        }
        #endregion

        #region Utils

        #region Gets
        private void GetCampaignGroups(ref Campaign _campaign, List<int?> CampaignGroups)
        {
            if (CampaignGroups.Any())
            {
                var list = new List<CampaignGroup>();
                CampaignGroups.ForEach(cg =>
                {
                    list.Add(new CampaignGroup()
                    {
                        GroupId = cg
                    });
                });
                _campaign.CampaignGroups = list;
            }

        }
        private void GetCampaignLevels(ref Campaign _campaign, List<int?> CampaignLevels)
        {
            if (CampaignLevels.Any())
            {
                var list = new List<CampaignLevel>();
                CampaignLevels.ForEach(cls =>
                {
                    list.Add(new CampaignLevel()
                    {
                        LevelId = cls
                    });
                });
                _campaign.CampaignLevels = list;
            }
        }

        CampaignViewModel GetRespond(Campaign campaign, BoekRole role = BoekRole.Admin, bool WithAddressDetail = false)
        {
            var campaignRespond = _unitOfWork.Campaigns.Get(c => c.Id == campaign.Id)
                    .ProjectTo<CampaignViewModel>(_mapper.ConfigurationProvider)
                    .SingleOrDefault();
            if (role.Equals(BoekRole.Customer))
            {
                campaignRespond.Participants = campaignRespond.Participants.Where(p =>
                p.Status.Equals((byte)ParticipantStatus.Approved) ||
                p.Status.Equals((byte)ParticipantStatus.Accepted)).ToList();
            }
            return ServiceUtils.GetResponseDetail(campaignRespond, WithAddressDetail);
        }
        #endregion

        #region Checks
        private void CheckCampaign(ref Campaign Campaign, CampaignFormat validFormat, bool IsCreate = true, Campaign oldCampaign = null)
        {
            if (!IsCreate)
            {
                CheckCampaignFormat(oldCampaign.Format, validFormat);
                CheckUpdateCampaignStatusByCancelledStatus(oldCampaign);
                CheckUpdateDate(Campaign, oldCampaign);
            }
            else
                CheckDate((DateTime)Campaign.StartDate, (DateTime)Campaign.EndDate, IsCreate);
            CheckCampaignCommissions(Campaign, IsCreate);
            if (validFormat.Equals(CampaignFormat.Offline))
                Campaign.IsRecurring = CheckCampaignOrganizations(Campaign, oldCampaign, IsCreate);
            else
            {
                CheckCampaignGroups(Campaign);
                CheckCampaignLevel(Campaign);
            }
        }

        private void CheckCampaignFormat(byte? Format, CampaignFormat validFormat)
        {
            if (!((byte)Format).Equals((byte)validFormat))
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                {
                    MessageConstants.MESSAGE_INVALID,
                    ErrorMessageConstants.CAMPAIGN_FORMAT
                });
        }

        private void CheckCampaignCommissions(Campaign campaign, bool IsCreate = true)
        {
            var _campaignCommission = campaign.CampaignCommissions.ToList();
            if (!_campaignCommission.Any())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.PreconditionRequired), new string[]
                    {
                        IsCreate ? ErrorMessageConstants.INSERT : ErrorMessageConstants.UPDATE,
                        ErrorMessageConstants.CAMPAIGN,
                        MessageConstants.MESSAGE_FAILED,
                        ErrorMessageConstants.CAMPAIGN_COMMISSION_INVALID
                    });
            var InvalidMinimalCommissions = _campaignCommission.Where(cc => !cc.MinimalCommission.HasValue || cc.MinimalCommission <= 0);
            if (InvalidMinimalCommissions.Any())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                {
                    ErrorMessageConstants.CAMPAIGN_MINIMAL_COMMISSION,
                    MessageConstants.MESSAGE_INVALID
                });
            var DuplicatedGenres = _campaignCommission.GroupBy(cc => cc.GenreId).Where(cc => cc.Count() > 1);
            if (DuplicatedGenres.Any())
            {
                var message = "";
                DuplicatedGenres.Select(cc => cc.Key).ToList().ForEach(k =>
                {
                    if (string.IsNullOrEmpty(message))
                        message = k.ToString();
                    else
                        message += $", {k}";
                });
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                {
                    MessageConstants.MESSAGE_DUPLICATED_INFO,
                    ErrorMessageConstants.CAMPAIGN_MINIMAL_COMMISSION,
                    message.Trim()
                });
            }
            _campaignCommission.ForEach(cc =>
            {
                var _genre = _unitOfWork.Genres.Get(cc.GenreId);
                if (_genre == null)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.GENRE_ID,
                    cc.GenreId.ToString()
                });
                if (!(bool)_genre.Status)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                    {
                        IsCreate ? ErrorMessageConstants.INSERT : ErrorMessageConstants.UPDATE,
                        ErrorMessageConstants.CAMPAIGN,
                        MessageConstants.MESSAGE_FAILED,
                        "vì",
                        ErrorMessageConstants.GENRE_STATUS
                    });
            });
        }

        private void CheckCampaignGroups(Campaign campaign)
        {
            var _campaignGroups = campaign.CampaignGroups;
            if (_campaignGroups == null || !_campaignGroups.Any())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.PreconditionRequired), new string[]
                {
                    MessageConstants.MESSAGE_REQUIRED,
                    ErrorMessageConstants.CAMPAIGN_GROUP.ToLower()
                });
            var duplicatedGroups = _campaignGroups.GroupBy(cg => cg.GroupId).Where(x => x.Count() > 1).ToList();
            if (duplicatedGroups.Any())
            {
                var message = "";
                duplicatedGroups.Select(cc => cc.Key).ToList().ForEach(k =>
                {
                    if (string.IsNullOrEmpty(message))
                        message = k.ToString();
                    else
                        message += $", {k}";
                });
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                {
                    MessageConstants.MESSAGE_DUPLICATED_INFO,
                    ErrorMessageConstants.CAMPAIGN_GROUP_ID,
                    message.Trim()
                });
            }
            _campaignGroups.ToList().ForEach(cg =>
            {
                var temp = cg.GroupId;
                if (temp == null)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.PreconditionRequired), new string[]
                    {
                            MessageConstants.MESSAGE_REQUIRED,
                            ErrorMessageConstants.GROUP_ID
                    });
                var _group = _unitOfWork.Groups.Get(temp);
                if (_group == null)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                    {
                            ErrorMessageConstants.NOT_FOUND,
                            ErrorMessageConstants.GROUP_ID,
                            cg.GroupId.ToString()
                    });
                if (!(bool)_group.Status)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                    {
                            ErrorMessageConstants.GROUP_ID,
                            cg.GroupId.ToString(),
                            ErrorMessageConstants.DISABLE
                    });
            });
        }
        private void CheckCampaignLevel(Campaign campaign)
        {
            var _campaignLevels = campaign.CampaignLevels;
            if (_campaignLevels != null || _campaignLevels.Any())
            {
                var duplicatedLevels = _campaignLevels.GroupBy(cl => cl.LevelId).Where(x => x.Count() > 1).ToList();
                if (duplicatedLevels.Any())
                {
                    var message = "";
                    duplicatedLevels.Select(cc => cc.Key).ToList().ForEach(k =>
                    {
                        if (string.IsNullOrEmpty(message))
                            message = k.ToString();
                        else
                            message += $", {k}";
                    });
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                    {
                        MessageConstants.MESSAGE_DUPLICATED_INFO,
                        ErrorMessageConstants.CAMPAIGN_LEVEL_ID,
                        message.Trim()
                    });
                }
                _campaignLevels.ToList().ForEach(cg =>
                {
                    var temp = cg.LevelId;
                    if (temp == null)
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.PreconditionRequired), new string[]
                    {
                        MessageConstants.MESSAGE_REQUIRED,
                        ErrorMessageConstants.LEVEL_ID
                    });
                    var _level = _unitOfWork.Levels.Get(temp);
                    if (_level == null)
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                    {
                        ErrorMessageConstants.NOT_FOUND,
                        ErrorMessageConstants.LEVEL_ID,
                        cg.LevelId.ToString()
                    });
                    if (!(bool)_level.Status)
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                    {
                        ErrorMessageConstants.GROUP_ID,
                        cg.LevelId.ToString(),
                        ErrorMessageConstants.DISABLE
                    });
                });
            }
        }
        private bool CheckCampaignOrganizations(Campaign campaign, Campaign oldCampaign = null, bool IsCreate = true)
        {
            campaign.CampaignOrganizations.ToList().ForEach(co => co.Schedules.OrderBy(s => s.StartDate));
            var campaignOrganizations = campaign.CampaignOrganizations.ToList();
            if (campaignOrganizations == null || !campaignOrganizations.Any())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.PreconditionRequired), new string[]
                {
                    MessageConstants.MESSAGE_REQUIRED,
                    ErrorMessageConstants.CAMPAIGN_ORGANIZATION.ToLower()
                });
            campaignOrganizations.ToList().ForEach(co =>
            {
                var temp = co.OrganizationId;
                if (temp == null)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.PreconditionRequired), new string[]
                {
                    MessageConstants.MESSAGE_REQUIRED,
                    ErrorMessageConstants.ORGANIZATION_ID
                });
                if (_unitOfWork.Organizations.Get(temp) == null)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.ORGANIZATION_ID,
                    co.OrganizationId.ToString()
                });
            });
            var IsRecurring = CheckCampaignOrganizationDetail(campaign);
            if (!IsCreate)
            {
                oldCampaign.CampaignOrganizations.ToList().ForEach(co => co.Schedules.OrderBy(s => s.StartDate));
                CheckUpdateCampaignOrganizationDetail(campaign, oldCampaign, IsRecurring);
            }
            return IsRecurring;
        }

        private bool CheckCampaignOrganizationDetail(Campaign campaign, bool IsCheckEmpty = true)
        {
            var campaignOrganizations = campaign.CampaignOrganizations.ToList();
            var schedule = new List<Schedule>();
            campaignOrganizations.Select(cos => cos.Schedules).ToList().ForEach(cos => schedule.AddRange(cos));
            var IsRecurring = false;

            if (schedule.Any())
            {
                var total = schedule.Count();
                //Get valid item
                var _address = (schedule.Select(cos => cos.Address))
                .GroupBy(a => !String.IsNullOrEmpty(a)).ToList();
                var _startDate = (schedule.Select(cos => cos.StartDate))
                .GroupBy(sd => sd.HasValue).ToList();
                var _endDate = (schedule.Select(cos => cos.EndDate))
                .GroupBy(sd => sd.HasValue).ToList();

                if (IsCheckEmpty)
                {
                    if (total == 1)
                    {
                        CheckEmptyCampaignOrganization<string>(_address, total);
                        CheckEmptyCampaignOrganization<DateTime?>(_startDate, total);
                        CheckEmptyCampaignOrganization<DateTime?>(_endDate, total);
                    }
                    else if (total >= 2)
                    {
                        //Check valid item
                        CheckEmptyCampaignOrganization<string>(_address, total);
                        CheckEmptyCampaignOrganization<DateTime?>(_startDate, total);
                        CheckEmptyCampaignOrganization<DateTime?>(_endDate, total);
                    }
                }
                //Check whether campaign is recurring or not
                IsRecurring = CheckRecurringCampaignOrganizationItem<string>(_address, total) &&
                CheckRecurringCampaignOrganizationItem<DateTime?>(_startDate, total) &&
                CheckRecurringCampaignOrganizationItem<DateTime?>(_endDate, total);

                if (IsRecurring)
                {
                    CheckValidRecurringCampaignDateTime(campaign, schedule);
                    CheckDuplicatedSchedule(campaignOrganizations);
                    schedule.ForEach(a => CheckScheduleDate((DateTime)a.StartDate, (DateTime)a.EndDate));
                }
            }
            return IsRecurring;
        }

        private void CheckEmptyCampaignOrganization<T>(List<IGrouping<bool, T>> list, int total, bool? IsEmpty = null)
        {
            var flag = false;
            if (IsEmpty.Equals(true))
                flag = list.Any(l => l.Key.Equals(true) && l.Select(item => item).Count().Equals(total));
            else if (IsEmpty.Equals(false))
                flag = list.Any(l => l.Key.Equals(false) && l.Select(item => item).Count().Equals(total));
            else if (IsEmpty == null)
                flag = list.Any(l => l.Select(item => item).Count().Equals(total));
            if (!flag)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                {
                    MessageConstants.MESSAGE_INVALID,
                    ErrorMessageConstants.CAMPAIGN_ORGANIZATION_SCHEDULE
                });
        }

        private bool CheckRecurringCampaignOrganizationItem<T>(List<IGrouping<bool, T>> list, int total)
        => list.Any(l => l.Key.Equals(true) && l.Select(item => item).Count().Equals(total));

        private void CheckValidRecurringCampaignDateTime(Campaign campaign, List<Schedule> schedule)
        {
            var startDate = (DateTime)campaign.StartDate;
            var endDate = (DateTime)campaign.EndDate;

            //Check if start date of campaign organization schedule is invalid in given situations
            //1. it is earlier than campaign's start date
            //2. it is equal to or after campaign's end date 
            var result = schedule.Any(cos =>
            DateTime.Compare(((DateTime)cos.StartDate), startDate) < 0 ||
            DateTime.Compare(((DateTime)cos.StartDate), endDate) >= 0);
            if (result)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                {
                    ErrorMessageConstants.CAMPAIGN_ORGANIZATION_SCHEDULE_START_DATE,
                    MessageConstants.MESSAGE_INVALID
                });

            //Check if end date of campaign organization schedule is invalid in given situations
            //1. it is equal to or earlier than campaign's start date
            //2. it is after campaign's end date
            result = schedule.Any(cos =>
            DateTime.Compare(((DateTime)cos.EndDate), startDate) <= 0 ||
            DateTime.Compare(((DateTime)cos.EndDate), endDate) > 0);
            if (result)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                {
                    ErrorMessageConstants.CAMPAIGN_ORGANIZATION_SCHEDULE_END_DATE,
                    MessageConstants.MESSAGE_INVALID
                });
        }

        private void CheckDuplicatedSchedule(List<CampaignOrganization> campaignOrganizations)
        {
            //Check same all fields within an organization
            var duplicatedOrganizationIds = new List<int?>();
            campaignOrganizations.ForEach(cos =>
            {
                cos.Schedules.ToList().ForEach(s =>
                {
                    var duplicatedItem = cos.Schedules.Where(sitem =>
                    sitem.Address == s.Address &&
                    sitem.StartDate == s.StartDate &&
                    sitem.EndDate == s.EndDate).Count();
                    duplicatedItem -= 1;
                    if (duplicatedItem > 0 && !duplicatedOrganizationIds.Contains(cos.OrganizationId))
                        duplicatedOrganizationIds.Add(cos.OrganizationId);
                });
            });
            if (duplicatedOrganizationIds.Any())
            {
                var message = "";
                duplicatedOrganizationIds.ForEach(d => message += string.IsNullOrEmpty(message) ? d : $", {d}");
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    MessageConstants.MESSAGE_DUPLICATED_INFO,
                    ErrorMessageConstants.CAMPAIGN_ORGANIZATION_SCHEDULE,
                    message
                });
            }

            //Check boundaries of schedule dates
            campaignOrganizations.ForEach(cos =>
            {
                cos.Schedules
                .GroupBy(s => s.Address)
                .Where(x => x.Count() > 1)
                .Select(y => y).ToList().ForEach(list =>
                {
                    var query = list.Select(l => l).ToList();
                    var errorOrganizationId = $"- {cos.OrganizationId.ToString()}";
                    //Check same start dates or same end dates
                    var duplicatedStartDates = query.GroupBy(q => q.StartDate).Where(x => x.Count() > 1);
                    var duplicatedEndDates = query.GroupBy(q => q.EndDate).Where(x => x.Count() > 1);
                    if (duplicatedStartDates.Any() || duplicatedEndDates.Any())
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                        {
                            ErrorMessageConstants.CAMPAIGN_ORGANIZATION_SCHEDULE_DUPLICATED_DATE,
                            errorOrganizationId
                        });

                    //Check dates as follows:
                    //1. 2 durations occurs at the same interval
                    //2. the intersect durations 
                    foreach (var item in query)
                    {
                        var SameInterval = query.Where(q => (q.StartDate < item.StartDate && q.EndDate > item.EndDate) ||
                        (q.StartDate > item.StartDate && q.EndDate < item.EndDate)).Count();
                        if (SameInterval > 0)
                            BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                            {
                                ErrorMessageConstants.CAMPAIGN_ORGANIZATION_SCHEDULE_DATE_DURATION,
                                errorOrganizationId
                            });
                        var IntersectDuration = query.Where(q => (q.StartDate > item.StartDate && q.EndDate > item.EndDate) ||
                        (q.StartDate < item.StartDate && q.EndDate < item.EndDate)).Count();
                        if (IntersectDuration > 0)
                            BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                            {
                                ErrorMessageConstants.CAMPAIGN_ORGANIZATION_SCHEDULE_DATE_DURATION,
                                errorOrganizationId
                            });

                    }
                });
            });
        }

        private void CheckUpdateCampaignOrganizationDetail(Campaign campaign, Campaign oldCampaign, bool IsRecurring)
        {
            var IsOldRecurring = CheckCampaignOrganizationDetail(oldCampaign, false);

            if (IsRecurring.Equals(true) && IsOldRecurring.Equals(true))
            {
                campaign.CampaignOrganizations = campaign.CampaignOrganizations.OrderBy(cos => cos.OrganizationId).ToList();
                var campaignOrganizations = GetUpdateCampaignOrganization(campaign, oldCampaign);
                if (campaignOrganizations != null)
                {
                    var newCampaignOrganizations = campaign.CampaignOrganizations.ToList();
                    newCampaignOrganizations.ForEach(ncos =>
                    {
                        var oldOrganization = campaignOrganizations.SingleOrDefault(cos =>
                        cos.OrganizationId.Equals(ncos.OrganizationId));
                        var organizationSchedule = ncos.Schedules.ToList();
                        organizationSchedule.ForEach(ca =>
                            CheckUpdateCampaignOrganizationDate((DateTime)ca.StartDate, (DateTime)ca.EndDate, (DateTime)oldCampaign.CreatedDate));
                    });
                }
            }
        }

        private List<CampaignOrganization> GetUpdateCampaignOrganization(Campaign campaign, Campaign oldCampaign)
        {
            var newCampaignOrganizations = campaign.CampaignOrganizations;
            var newOrganizationIds = newCampaignOrganizations.Select(cos => cos.OrganizationId).ToList();
            var campaignOrganizations = oldCampaign.CampaignOrganizations.Where(cos =>
            newOrganizationIds.Contains(cos.OrganizationId));
            return campaignOrganizations.Any() ? campaignOrganizations.OrderBy(cos => cos.OrganizationId).ToList() : null;
        }

        private void CheckUpdateCampaignOrganizationDate(DateTime updateStartDate, DateTime updateEndDate, DateTime createdCampaignDate)
        {
            if (DateTime.Compare(updateStartDate, createdCampaignDate) <= 0)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.UPDATE,
                    ErrorMessageConstants.CAMPAIGN_START_DATE,
                    ErrorMessageConstants.CAMPAIGN_NOT_LATER_START_DATE,
                    ErrorMessageConstants.CAMPAIGN_CREATED_DATE
                });
            if (DateTime.Compare(updateEndDate, createdCampaignDate) <= 0)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.UPDATE,
                    ErrorMessageConstants.CAMPAIGN_END_DATE,
                    ErrorMessageConstants.CAMPAIGN_NOT_LATER_START_DATE,
                    ErrorMessageConstants.CAMPAIGN_CREATED_DATE
                });
        }

        private Campaign CheckCampaignOrganizationAddress(Campaign Campaign, AddressRequestModel CampaignAddress, List<CreateCampaignOrganizationsRequestModel> OrganizationAddresses)
        {
            Campaign.Address = ServiceUtils.CheckAddress(CampaignAddress).Address;
            var flag = false;
            if (OrganizationAddresses != null)
            {
                if (OrganizationAddresses.Any())
                {
                    OrganizationAddresses.ForEach(oas =>
                    {
                        if (oas.Schedules != null)
                        {
                            if (oas.Schedules.Any())
                            {
                                oas.Schedules.ForEach(s =>
                                {
                                    if (!s.AddressRequest.IsEmptyAllFields())
                                    {
                                        s.Address = ServiceUtils.CheckAddress(s.AddressRequest).Address;
                                        flag = true;
                                    }
                                });
                            }
                        }
                    });
                    if (flag)
                        _mapper.Map(OrganizationAddresses, Campaign.CampaignOrganizations);
                }
            }
            return Campaign;
        }

        private void CheckUpdateCampaignStatusByCancelledStatus(Campaign oldCampaign)
        {
            if (oldCampaign.Status.Equals((byte)CampaignStatus.Cancelled))
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.UPDATE,
                    ErrorMessageConstants.CAMPAIGN.ToLower(),
                    MessageConstants.MESSAGE_FAILED,
                    ErrorMessageConstants.CAMPAIGN_CANCEL
                });
        }

        private void CheckUpdateCampaignStatusByPostponedStatus(Campaign oldCampaign)
        {
            if (oldCampaign.Status.Equals((byte)CampaignStatus.Postponed))
                BoekErrorMessage.ShowErrorMessage((int)HttpStatusCode.NotFound, new string[]
                {
                    ErrorMessageConstants.CAMPAIGN_STATUS,
                    MessageConstants.MESSAGE_INVALID
                });
        }

        private void CheckUpdateDate(Campaign campaign, Campaign oldCampaign)
        {
            var updateStartDate = (DateTime)campaign.StartDate;
            var updateEndDate = (DateTime)campaign.EndDate;
            var oldStartDate = (DateTime)oldCampaign.StartDate;
            var oldEndDate = (DateTime)oldCampaign.StartDate;
            var createdCampaignDate = (DateTime)oldCampaign.CreatedDate;

            //Check if update campaign is not started or not based on 1 of 2 conditions
            //1. Check if both start date and end date are later than date time now
            //2. Check if campaign status is not started or not
            //Check start date and end date because updating status is done by worker service
            if (!(DateTime.Compare(oldStartDate, DateTime.Now) > 0 && (DateTime.Compare(oldEndDate, DateTime.Now) > 0)) ||
            !oldCampaign.Status.Equals((byte)CampaignStatus.NotStarted))
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                    {
                        ErrorMessageConstants.UPDATE,
                        ErrorMessageConstants.CAMPAIGN.ToLower(),
                        MessageConstants.MESSAGE_FAILED,
                        ErrorMessageConstants.CAMPAIGN_INVALID_NOT_STARTED
                    });

            if (!updateStartDate.Equals(oldStartDate))
            {
                if (DateTime.Compare(updateStartDate, updateEndDate) >= 0)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                    {
                        ErrorMessageConstants.UPDATE,
                        ErrorMessageConstants.CAMPAIGN_START_DATE,
                        ErrorMessageConstants.CAMPAIGN_NOT_LATER_START_DATE,
                        ErrorMessageConstants.CAMPAIGN_END_DATE
                    });
                if (DateTime.Compare(updateStartDate, createdCampaignDate) <= 0)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                    {
                        ErrorMessageConstants.UPDATE,
                        ErrorMessageConstants.CAMPAIGN_START_DATE,
                        ErrorMessageConstants.CAMPAIGN_NOT_LATER_START_DATE,
                        ErrorMessageConstants.CAMPAIGN_CREATED_DATE
                    });
            }

            if (!updateEndDate.Equals(oldEndDate))
            {
                if (DateTime.Compare(updateStartDate, updateEndDate) >= 0)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                    {
                        ErrorMessageConstants.UPDATE,
                        ErrorMessageConstants.CAMPAIGN_START_DATE,
                        ErrorMessageConstants.CAMPAIGN_NOT_LATER_START_DATE,
                        ErrorMessageConstants.CAMPAIGN_END_DATE
                    });
                if (DateTime.Compare(updateEndDate, createdCampaignDate) <= 0)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                    {
                        ErrorMessageConstants.UPDATE,
                        ErrorMessageConstants.CAMPAIGN_END_DATE,
                        ErrorMessageConstants.CAMPAIGN_NOT_LATER_START_DATE,
                        ErrorMessageConstants.CAMPAIGN_CREATED_DATE
                    });
            }
        }

        private void CheckDate(DateTime startDate, DateTime endDate, bool IsCreate = true)
        {
            var _preDate = startDate.Subtract(new TimeSpan(1, 0, 0, 0));
            if (IsCreate)
            {
                if (DateTime.Compare(_preDate.Date, DateTime.Now.Date) < 0)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                    {
                        ErrorMessageConstants.CAMPAIGN_START_DATE,
                        ErrorMessageConstants.CAMPAIGN_START_DATE_NOT_TODAY
                    });
            }
            if (DateTime.Compare(startDate, endDate) >= 0)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                {
                    ErrorMessageConstants.CAMPAIGN_START_DATE,
                    ErrorMessageConstants.CAMPAIGN_NOT_LATER_START_DATE,
                    ErrorMessageConstants.CAMPAIGN_END_DATE
                });
        }

        private void CheckScheduleDate(DateTime startDate, DateTime endDate)
        {
            if (DateTime.Compare(startDate, endDate) >= 0)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                {
                    ErrorMessageConstants.CAMPAIGN_ORGANIZATION_SCHEDULE_START_DATE,
                    ErrorMessageConstants.CAMPAIGN_NOT_LATER_START_DATE,
                    ErrorMessageConstants.CAMPAIGN_ORGANIZATION_SCHEDULE_END_DATE
                });
        }

        private void CheckCancelDate(DateTime startDate, DateTime endDate)
        {
            var _startDate = startDate;
            var _endDate = endDate;
            var _now = DateTime.Now;
            if (DateTime.Compare(_startDate, _now) <= 0)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.CANCEL,
                    ErrorMessageConstants.CAMPAIGN,
                    MessageConstants.MESSAGE_FAILED,
                    "vì",
                    ErrorMessageConstants.CAMPAIGN_START_DATE
                });
            if (DateTime.Compare(_endDate, _now) <= 0)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.CANCEL,
                    ErrorMessageConstants.CAMPAIGN,
                    MessageConstants.MESSAGE_FAILED,
                    "vì",
                    ErrorMessageConstants.CAMPAIGN_END_DATE
                });
        }

        public Campaign ChangeCampaignOrganizationScheduleStatus(Campaign campaign, byte status)
        {
            campaign.CampaignOrganizations.ToList().ForEach(cos =>
            {
                if (cos.Schedules != null)
                {
                    if (cos.Schedules.Any())
                        cos.Schedules.ToList().ForEach(s => s.Status = status);
                }
            });
            return campaign;
        }

        private void UpdateCampaignOrganizations(List<CampaignOrganization> campaignOrganizations, int? CampaignId)
        {
            var _campaignOrganizations = _unitOfWork.CampaignOrganizations
                .Get(co => co.CampaignId.Equals(CampaignId))
                .Include(co => co.Schedules)
                .ToList();
            if (_campaignOrganizations.Any())
            {
                var deletedCampaignOrganizations = new List<CampaignOrganization>();
                var existedCampaignOrganizations = new List<CampaignOrganization>();
                _campaignOrganizations.ForEach(cos =>
                {
                    if (!campaignOrganizations.Any(co =>
                co.OrganizationId.Equals(cos.OrganizationId)))
                        deletedCampaignOrganizations.Add(cos);
                    else
                        existedCampaignOrganizations.Add(cos);
                });
                if (deletedCampaignOrganizations.Any())
                {
                    if (deletedCampaignOrganizations.Any(cos => cos.Schedules.Any()))
                    {
                        var deletedSchedules = new List<Schedule>();
                        deletedCampaignOrganizations.ForEach(dcos =>
                        {
                            if (dcos.Schedules.Any())
                                deletedSchedules.AddRange(dcos.Schedules);
                        });
                        _unitOfWork.Schedules.RemoveRange(deletedSchedules);
                        if (deletedSchedules.Any())
                        {
                            if (!_unitOfWork.Save())
                                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                                {
                                    ErrorMessageConstants.DELETE,
                                    ErrorMessageConstants.CAMPAIGN_ORGANIZATION.ToLower(),
                                    MessageConstants.MESSAGE_FAILED
                                });
                        }
                    }
                    _unitOfWork.CampaignOrganizations.RemoveRange(deletedCampaignOrganizations);
                    if (!_unitOfWork.Save())
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                        {
                            ErrorMessageConstants.DELETE,
                            ErrorMessageConstants.CAMPAIGN_ORGANIZATION.ToLower(),
                            MessageConstants.MESSAGE_FAILED
                        });
                }
                if (existedCampaignOrganizations.Any())
                {
                    //New schedule
                    var existedOrganizations = existedCampaignOrganizations
                        .Select(eco => eco.OrganizationId).ToList();
                    var newOrganizations = new List<CampaignOrganization>();
                    campaignOrganizations.ForEach(cos =>
                    {
                        if (!existedOrganizations.Contains(cos.OrganizationId))
                            newOrganizations.Add(cos);
                    });
                    if (newOrganizations.Any())
                    {
                        newOrganizations.ForEach(no =>
                        {
                            no.CampaignId = CampaignId;
                            no.Schedules = SetStatusUpdateCampaignOrganizationSchedule(no.Schedules.ToList(), (byte)CampaignStatus.NotStarted);
                        });
                        _unitOfWork.CampaignOrganizations
                        .AddRange(_mapper.Map<List<CampaignOrganization>>(newOrganizations));
                        if (!_unitOfWork.Save())
                            BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                            {
                                ErrorMessageConstants.INSERT,
                                ErrorMessageConstants.CAMPAIGN_ORGANIZATION.ToLower(),
                                MessageConstants.MESSAGE_FAILED
                            });
                    }

                    //Update existed schedule
                    GetUpdatedSchedule(existedCampaignOrganizations, campaignOrganizations);
                }
            }
        }
        private List<Schedule> SetStatusUpdateCampaignOrganizationSchedule(List<Schedule> list, byte status)
        {
            if (list != null)
            {
                if (list.Any())
                {
                    list.ForEach(s =>
                    {
                        if (!IsEmptyCampaignOrganizationSchedule(s))
                            s.Status = status;
                    });
                }
            }
            return list;
        }

        private bool IsEmptyCampaignOrganizationSchedule(Schedule schedule)
        => String.IsNullOrEmpty(schedule.Address) && !schedule.StartDate.HasValue && !schedule.EndDate.HasValue;

        private void GetUpdatedSchedule(List<CampaignOrganization> existedOrganizations, List<CampaignOrganization> newOrganizations)
        {
            existedOrganizations = existedOrganizations.OrderBy(eos => eos.OrganizationId).ToList();
            newOrganizations = newOrganizations.OrderBy(nos => nos.OrganizationId).ToList();

            //Get updated schedule from old campaign organizations
            var newOrganizationIds = newOrganizations.Select(nos => nos.OrganizationId).ToList();
            var updateOrganizations = new List<CampaignOrganization>();
            existedOrganizations.ForEach(eos =>
            {
                if (newOrganizationIds.Contains(eos.OrganizationId))
                    updateOrganizations.Add(eos);
            });
            if (updateOrganizations.Any())
            {
                //Get new schedule from new campaign organizations
                newOrganizationIds = updateOrganizations.Select(nos => nos.OrganizationId).ToList();
                //Get list to delete and update
                var list = new UpdatedSchedule();
                newOrganizationIds.ForEach(nois =>
                {
                    var newOrganization = newOrganizations.SingleOrDefault(nos => nos.OrganizationId.Equals(nois));
                    var oldOrganization = updateOrganizations.SingleOrDefault(uos => uos.OrganizationId.Equals(nois));
                    list = GetUpdatedScheduleDetail(oldOrganization.Id, newOrganization.Schedules.ToList(), oldOrganization.Schedules.ToList(), list);
                });

                if (list.updatedSchedule.Any())
                {
                    _unitOfWork.Schedules.UpdateRange(list.updatedSchedule);
                    if (!_unitOfWork.Save())
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                        {
                            ErrorMessageConstants.UPDATE,
                            ErrorMessageConstants.CAMPAIGN_ORGANIZATION_SCHEDULE.ToLower(),
                            MessageConstants.MESSAGE_FAILED
                        });
                }
                if (list.deletedSchedule.Any())
                {
                    _unitOfWork.Schedules.RemoveRange(list.deletedSchedule);
                    if (!_unitOfWork.Save())
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                        {
                            ErrorMessageConstants.DELETE,
                            ErrorMessageConstants.CAMPAIGN_ORGANIZATION_SCHEDULE.ToLower(),
                            MessageConstants.MESSAGE_FAILED
                        });
                }
            }
        }

        private UpdatedSchedule GetUpdatedScheduleDetail(int CampaignOrganizationId, List<Schedule> newSchedule, List<Schedule> oldSchedule, UpdatedSchedule list)
        {
            if (oldSchedule.Any() || newSchedule.Any())
            {
                if (oldSchedule.Count() == 0 && newSchedule.Count() > 0)
                {
                    newSchedule.ForEach(na =>
                    {
                        var temp = new Schedule()
                        {
                            CampaignOrganizationId = CampaignOrganizationId,
                            Address = na.Address,
                            StartDate = na.StartDate,
                            EndDate = na.EndDate,
                            Status = (byte)CampaignStatus.NotStarted
                        };
                        list.updatedSchedule.Add(temp);
                    });
                }

                if (oldSchedule.Count() > 0 && newSchedule.Count() == 0)
                    list.deletedSchedule.AddRange(oldSchedule);
            }
            if (oldSchedule.Any() && newSchedule.Any())
            {
                var newTemp = newSchedule.Where(na =>
                !string.IsNullOrEmpty(na.Address) &&
                na.StartDate.HasValue &&
                na.EndDate.HasValue).ToList();

                var oldTemp = oldSchedule.Where(na =>
                !string.IsNullOrEmpty(na.Address) &&
                na.StartDate.HasValue &&
                na.EndDate.HasValue).ToList();

                //Remove same schedules
                var sameSchedules = CheckSameSchedules(newTemp, oldTemp);
                if (sameSchedules != null)
                {
                    sameSchedules.ForEach(ss =>
                    {
                        var item = oldTemp.SingleOrDefault(to => to.Address.Equals(ss.Address) &&
                        to.StartDate.Equals(ss.StartDate) &&
                        to.EndDate.Equals(ss.EndDate));
                        oldTemp.Remove(item);
                        item = newTemp.SingleOrDefault(nt => nt.Address.Equals(ss.Address) &&
                        nt.StartDate.Equals(ss.StartDate) &&
                        nt.EndDate.Equals(ss.EndDate));
                        newTemp.Remove(item);
                    });
                }
                if (newTemp.Any() || oldTemp.Any())
                {
                    //Same amount
                    if (newTemp.Count() == oldTemp.Count())
                    {
                        var keys = oldTemp.Select(os => os.Id).OrderBy(os => os).ToList();
                        newTemp = newTemp.OrderBy(nt => nt.StartDate).ToList();
                        for (int i = 0; i < keys.Count(); i++)
                        {
                            var updatedSchedule = newTemp[i];
                            var oldScheduleDetail = oldTemp.SingleOrDefault(s => s.Id.Equals(keys[i]));
                            oldScheduleDetail = GenerateSchedule(updatedSchedule, oldScheduleDetail, CampaignOrganizationId);
                            list.updatedSchedule.Add(oldScheduleDetail);
                        }
                    }

                    //Different amount
                    if (newTemp.Count() != oldTemp.Count())
                    {
                        var min = newTemp.Count() < oldTemp.Count() ? newTemp.Count() : oldTemp.Count();
                        var keys = oldTemp.Select(os => os.Id).Take(min).OrderBy(os => os).ToList();
                        newTemp = newTemp.OrderBy(nt => nt.StartDate).ToList();
                        for (int i = 0; i < newTemp.Count(); i++)
                        {
                            var updatedSchedule = newTemp[i];
                            var id = i < keys.Count() ? keys[i] : 0;
                            var oldScheduleDetail = oldTemp.SingleOrDefault(s => s.Id.Equals(id));
                            oldScheduleDetail = GenerateSchedule(updatedSchedule, oldScheduleDetail, CampaignOrganizationId);
                            list.updatedSchedule.Add(oldScheduleDetail);
                        }
                        if (oldTemp.Count() > newTemp.Count())
                        {
                            oldTemp = oldTemp.OrderBy(os => os.Id).Skip(min).ToList();
                            list.deletedSchedule.AddRange(oldTemp);
                        }
                    }
                }
            }
            return list;
        }

        private List<Schedule> CheckSameSchedules(List<Schedule> newSchedule, List<Schedule> oldSchedule)
        {
            var sameSchedules = new List<Schedule>();
            newSchedule.ForEach(ns =>
            {
                var temp = oldSchedule.SingleOrDefault(os => os.Address == ns.Address &&
                os.StartDate == ns.StartDate &&
                os.EndDate == ns.EndDate);
                if (temp != null)
                    sameSchedules.Add(temp);
            });
            return sameSchedules.Any() ? sameSchedules : null;
        }

        private Schedule GenerateSchedule(Schedule updatedSchedule, Schedule oldSchedule, int CampaignOrganizationId)
        {
            if (updatedSchedule != null && oldSchedule != null)
            {
                oldSchedule.CampaignOrganizationId = CampaignOrganizationId;
                oldSchedule.Address = updatedSchedule.Address;
                oldSchedule.StartDate = updatedSchedule.StartDate;
                oldSchedule.EndDate = updatedSchedule.EndDate;
                oldSchedule.Status = (byte)CampaignStatus.NotStarted;
            }
            return oldSchedule;
        }
        #endregion

        #region Updates
        //TO-DO: Commission can be updated and inserted, but can it be deleted?
        private void UpdateCampaignCommissions(List<CampaignCommission> campaignCommissions, int? CampaignId)
        {
            var _campaignCommissions = _unitOfWork.CampaignCommissions
                .Get(co => co.CampaignId.Equals(CampaignId))
                .ToList();
            if (_campaignCommissions.Any())
            {
                var deletedCampaignCommissions = new List<CampaignCommission>();
                var existedCampaignCommissions = new List<CampaignCommission>();
                _campaignCommissions.ForEach(cc =>
                {
                    if (!campaignCommissions.Any(co => co.GenreId.Equals(cc.GenreId)))
                    {
                        if (CheckBookProduct(cc))
                            deletedCampaignCommissions.Add(cc);
                    }
                    else
                        existedCampaignCommissions.Add(cc);
                });
                _unitOfWork.CampaignCommissions.RemoveRange(deletedCampaignCommissions);
                var _result = _unitOfWork.Save();
                if (deletedCampaignCommissions.Any() && !_result)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                    {
                        ErrorMessageConstants.DELETE,
                        ErrorMessageConstants.CAMPAIGN_COMMISSION.ToLower(),
                        MessageConstants.MESSAGE_FAILED
                    });
                if (existedCampaignCommissions.Any())
                {
                    var existedCommissions = existedCampaignCommissions.Select(ecc => ecc.GenreId).ToList();
                    var newCommissions = new List<CampaignCommission>();
                    campaignCommissions.ForEach(co =>
                    {
                        if (!existedCommissions.Contains(co.GenreId))
                        {
                            co.CampaignId = CampaignId;
                            newCommissions.Add(co);
                        }
                    });
                    _unitOfWork.CampaignCommissions.AddRange(newCommissions);
                    _result = _unitOfWork.Save();
                    if (newCommissions.Any() && !_result)
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                        {
                            ErrorMessageConstants.INSERT,
                            ErrorMessageConstants.CAMPAIGN_COMMISSION.ToLower(),
                            MessageConstants.MESSAGE_FAILED
                        });
                    var updatedCommission = new List<CampaignCommission>();
                    existedCampaignCommissions.ForEach(ecc =>
                    {
                        if (campaignCommissions.Any(co => co.GenreId.Equals(ecc.GenreId)
                        && !co.MinimalCommission.Equals(ecc.MinimalCommission)))
                            updatedCommission.Add(ecc);
                    });
                    updatedCommission.ForEach(uc =>
                    {
                        var temp = campaignCommissions.SingleOrDefault(cc => cc.GenreId.Equals(uc.GenreId));
                        uc.MinimalCommission = temp == null ? 0 : temp.MinimalCommission;
                    });
                    _unitOfWork.CampaignCommissions.UpdateRange(updatedCommission);
                    _result = _unitOfWork.Save();
                    if (updatedCommission.Any() && !_result)
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                        {
                            ErrorMessageConstants.UPDATE,
                            ErrorMessageConstants.CAMPAIGN_COMMISSION.ToLower(),
                            MessageConstants.MESSAGE_FAILED
                        });
                }
            }
        }
        private void UpdateCampaignGroups(List<CampaignGroup> campaignGroups, int? CampaignId)
        {
            var _campaignGroups = _unitOfWork.CampaignGroups
                .Get(co => co.CampaignId.Equals(CampaignId)).ToList();
            if (_campaignGroups.Any())
            {
                var deletedCampaignGroups = new List<CampaignGroup>();
                var existedCampaignGroups = new List<CampaignGroup>();
                _campaignGroups.ForEach(cgs =>
                {
                    if (!campaignGroups.Any(co =>
                    co.GroupId.Equals(cgs.GroupId)))
                        deletedCampaignGroups.Add(cgs);
                    else
                        existedCampaignGroups.Add(cgs);
                });
                _unitOfWork.CampaignGroups.RemoveRange(deletedCampaignGroups);
                var result = _unitOfWork.Save();
                if (deletedCampaignGroups.Any() && !result)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                    {
                        ErrorMessageConstants.DELETE,
                        ErrorMessageConstants.CAMPAIGN_GROUP.ToLower(),
                        MessageConstants.MESSAGE_FAILED
                    });
                if (existedCampaignGroups.Any())
                {
                    var existedGroups = existedCampaignGroups
                        .Select(ecg => ecg.GroupId).ToList();
                    var newGroups = new List<CampaignGroup>();
                    campaignGroups.ForEach(cgs =>
                    {
                        if (!existedGroups.Contains(cgs.GroupId))
                        {
                            cgs.CampaignId = CampaignId;
                            newGroups.Add(cgs);
                        }
                    });
                    _unitOfWork.CampaignGroups
                        .AddRange(_mapper.Map<List<CampaignGroup>>(newGroups));
                    result = _unitOfWork.Save();
                    if (newGroups.Any() && !result)
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                        {
                            ErrorMessageConstants.INSERT,
                            ErrorMessageConstants.CAMPAIGN_GROUP.ToLower(),
                            MessageConstants.MESSAGE_FAILED
                        });
                }
            }
        }
        private void UpdateCampaignLevels(List<CampaignLevel> campaignLevels, int? CampaignId)
        {
            var _campaignLevels = _unitOfWork.CampaignLevels
                .Get(co => co.CampaignId.Equals(CampaignId)).ToList();
            if (_campaignLevels.Any())
            {
                var deletedCampaignLevels = new List<CampaignLevel>();
                var existedCampaignLevels = new List<CampaignLevel>();
                _campaignLevels.ForEach(cls =>
                {
                    if (!campaignLevels.Any(co => cls.LevelId.Equals(co.LevelId)))
                        deletedCampaignLevels.Add(cls);
                    else
                        existedCampaignLevels.Add(cls);
                });
                _unitOfWork.CampaignLevels.RemoveRange(deletedCampaignLevels);
                var result = _unitOfWork.Save();
                if (deletedCampaignLevels.Any() && !result)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                    {
                        ErrorMessageConstants.DELETE,
                        ErrorMessageConstants.CAMPAIGN_LEVEL.ToLower(),
                        MessageConstants.MESSAGE_FAILED
                    });
                if (existedCampaignLevels.Any())
                {
                    var existedLevels = existedCampaignLevels.Select(ecl => ecl.LevelId).ToList();
                    var newLevels = new List<CampaignLevel>();
                    campaignLevels.ForEach(cls =>
                    {
                        if (!existedLevels.Contains(cls.LevelId))
                        {
                            cls.CampaignId = CampaignId;
                            newLevels.Add(cls);
                        }
                    });
                    _unitOfWork.CampaignLevels.AddRange(_mapper.Map<List<CampaignLevel>>(newLevels));
                    result = _unitOfWork.Save();
                    if (newLevels.Any() && !result)
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                        {
                            ErrorMessageConstants.INSERT,
                            ErrorMessageConstants.CAMPAIGN_LEVEL.ToLower(),
                            MessageConstants.MESSAGE_FAILED
                        });
                }
            }

            if (!_campaignLevels.Any() && campaignLevels.Any())
            {
                campaignLevels.ForEach(cg => cg.CampaignId = CampaignId);
                _unitOfWork.CampaignLevels.AddRange(campaignLevels);
                if (!_unitOfWork.Save())
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                    {
                        ErrorMessageConstants.INSERT,
                        ErrorMessageConstants.CAMPAIGN_GROUP.ToLower(),
                        MessageConstants.MESSAGE_FAILED
                    });
            }
        }
        private bool CheckBookProduct(CampaignCommission campaignCommission)
        {
            //cSpell:disable
            var _bookProducts = _unitOfWork.BookProducts
                .Get(bp => bp.CampaignId.Equals(campaignCommission.CampaignId))
                .Include(bp => bp.BookProductItems)
                .ThenInclude(bookProductItem => bookProductItem.Book)
                .Include(bp => bp.Book)
                .ToList();
            var _genre = _unitOfWork.Genres.Get(g => g.Id.Equals(campaignCommission.GenreId))
                .Include(g => g.InverseParent)
                .SingleOrDefault();
            var _ChildrenBookProducts = new List<BookProduct>();
            var _ParentBookProducts = new List<BookProduct>();
            if (_genre.InverseParent.Any())
            {
                var _childgenres = _genre.InverseParent.Select(ip => ip.Id);
                _bookProducts.ForEach(bp =>
                {
                    var flag = false;
                    if (_childgenres.Contains((int)bp.Book.GenreId) &&
                    !_ChildrenBookProducts.Contains(bp))
                        _ChildrenBookProducts.Add(bp);
                    bp.BookProductItems.ToList().ForEach(b =>
                    {
                        var _book = b.Book;
                        if (_childgenres.Contains((int)_book.GenreId))
                            flag = true;
                    });
                    if (flag && !_ChildrenBookProducts.Contains(bp))
                        _ChildrenBookProducts.Add(bp);
                });
            }
            _bookProducts.ForEach(bp =>
                {
                    var flag = false;
                    if (_genre.Id.Equals((int)bp.Book.GenreId) &&
                    !_ParentBookProducts.Contains(bp))
                        _ParentBookProducts.Add(bp);
                    bp.BookProductItems.ToList().ForEach(b =>
                    {
                        var _book = b.Book;
                        if (_genre.Id.Equals((int)_book.GenreId))
                            flag = true;
                    });
                    if (flag && !_ParentBookProducts.Contains(bp))
                        _ParentBookProducts.Add(bp);
                });
            return _ChildrenBookProducts.Any() || _ParentBookProducts.Any() ? false : true;
        }
        #endregion

        #region Adds
        private void AddCreateCampaignOrganizations(int CampaignId, List<CampaignOrganization> list)
        {
            var _organizations = _unitOfWork.CampaignOrganizations
                .Get(oc => oc.CampaignId.Equals(CampaignId));
            if (_organizations == null)
            {
                list.ForEach(co => co.CampaignId = CampaignId);
                _unitOfWork.CampaignOrganizations.AddRange(list);
                if (!_unitOfWork.Save())
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.INSERT,
                    ErrorMessageConstants.CAMPAIGN_ORGANIZATION.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            }
        }
        private void AddCreateCampaignGroups(int CampaignId, List<CampaignGroup> list)
        {
            var _groups = _unitOfWork.CampaignGroups
                .Get(oc => oc.CampaignId.Equals(CampaignId));
            if (_groups == null)
            {
                list.ForEach(co => co.CampaignId = CampaignId);
                _unitOfWork.CampaignGroups.AddRange(list);
                if (!_unitOfWork.Save())
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.INSERT,
                    ErrorMessageConstants.CAMPAIGN_GROUP.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            }
        }
        private void AddCreateCampaignLevels(int CampaignId, List<CampaignLevel> list)
        {
            if (list.Any())
            {
                var _levels = _unitOfWork.CampaignLevels
                .Get(cl => cl.CampaignId.Equals(CampaignId));
                if (_levels == null)
                {
                    list.ForEach(cls => cls.CampaignId = CampaignId);
                    _unitOfWork.CampaignLevels.AddRange(list);
                    if (!_unitOfWork.Save())
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                    {
                        ErrorMessageConstants.INSERT,
                        ErrorMessageConstants.CAMPAIGN_LEVEL.ToLower(),
                        MessageConstants.MESSAGE_FAILED
                    });
                }
            }
        }
        #endregion

        #region Check updating started campaign
        private Campaign CheckUpdatedStartedCampaign(Campaign campaign)
        {
            campaign = _unitOfWork.Campaigns.Get(campaign.Id);
            if (campaign == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.CAMPAIGN_ID,
                });

            CheckUpdateCampaignStatusByCancelledStatus(campaign);
            CheckUpdateCampaignStatusByPostponedStatus(campaign);

            var present = CheckUpdatedCampaignDates((DateTime)campaign.StartDate, (DateTime)campaign.EndDate);

            campaign.UpdatedDate = present;
            return campaign;
        }

        private DateTime CheckUpdatedCampaignDates(DateTime startDate, DateTime endDate, DateTime? present = null)
        {
            if (!present.HasValue)
                present = DateTime.Now;
            if (!(DateTime.Compare(startDate, (DateTime)present) <= 0 &&
                    DateTime.Compare(endDate, (DateTime)present) > 0))
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.UPDATE,
                    ErrorMessageConstants.CAMPAIGN.ToLower(),
                    MessageConstants.MESSAGE_FAILED,
                    ErrorMessageConstants.CAMPAIGN_DATE
                });
            return (DateTime)present;
        }
        #endregion

        #region Check Postponed campaign
        private CampaignViewModel CheckPostponedCampaign(int id, CampaignFormat format)
        {
            var campaign = _unitOfWork.Campaigns.Get(c => c.Id.Equals(id))
            .ProjectTo<CampaignViewModel>(_mapper.ConfigurationProvider)
            .SingleOrDefault();
            //Check if campaign is null
            if (campaign == null)
                BoekErrorMessage.ShowErrorMessage((int)HttpStatusCode.NotFound, new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.CAMPAIGN_ID
                });

            //Check format
            CheckCampaignFormat(campaign.Format, format);

            //Check if campaign is cancelled
            var _campaign = new Campaign()
            {
                Status = campaign.Status
            };
            CheckUpdateCampaignStatusByCancelledStatus(_campaign);

            //Check dates of campaign
            var present = CheckUpdatedCampaignDates((DateTime)campaign.StartDate, (DateTime)campaign.EndDate);
            campaign.UpdatedDate = present;
            campaign.Status = (byte)CampaignStatus.Postponed;

            return campaign;
        }

        private void UpdateCampaignOrganizationByPostponedCampaign(CampaignViewModel campaign)
        {
            if (campaign.Format.Equals((byte)CampaignFormat.Offline) && (bool)campaign.IsRecurring)
            {
                var keys = new List<int?>();
                campaign.CampaignOrganizations.Select(cos => cos.Schedules).ToList().ForEach(ss =>
                {
                    var temp = ss.Where(s => !s.Status.Equals((byte)CampaignStatus.End)).Select(s => s.Id);
                    if (temp.Any())
                        keys.AddRange(temp);
                });
                if (keys.Any())
                {
                    var schedules = _unitOfWork.Schedules.Get(s => keys.Contains(s.Id)).ToList();
                    schedules.ForEach(s => s.Status = (byte)CampaignStatus.Postponed);
                    _unitOfWork.Schedules.UpdateRange(schedules);
                    if (!_unitOfWork.Save())
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                        {
                            ErrorMessageConstants.POSTPONE,
                            ErrorMessageConstants.CAMPAIGN_ORGANIZATION_SCHEDULE.ToLower(),
                            MessageConstants.MESSAGE_FAILED
                        });
                }
            }
        }
        #endregion

        #region Check Restart campaign
        private CampaignViewModel CheckRestartedCampaign(int id, CampaignFormat format)
        {
            var campaign = _unitOfWork.Campaigns.Get(c => c.Id.Equals(id))
            .ProjectTo<CampaignViewModel>(_mapper.ConfigurationProvider)
            .SingleOrDefault();

            //Check if campaign is null
            if (campaign == null)
                BoekErrorMessage.ShowErrorMessage((int)HttpStatusCode.NotFound, new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.CAMPAIGN_ID
                });

            //Check format
            CheckCampaignFormat(campaign.Format, format);

            //Check if campaign is cancelled
            var _campaign = new Campaign()
            {
                Status = campaign.Status
            };
            CheckUpdateCampaignStatusByCancelledStatus(_campaign);

            //check if campaign is postponed
            if (!campaign.Status.Equals((byte)CampaignStatus.Postponed))
                BoekErrorMessage.ShowErrorMessage((int)HttpStatusCode.NotFound, new string[]
                {
                    ErrorMessageConstants.CAMPAIGN_STATUS,
                    MessageConstants.MESSAGE_INVALID
                });

            //Check dates of campaign
            var present = CheckUpdatedCampaignDates((DateTime)campaign.StartDate, (DateTime)campaign.EndDate);
            campaign.UpdatedDate = present;
            campaign.Status = (byte)CampaignStatus.Start;

            return campaign;
        }

        private void UpdateCampaignOrganizationRestartCampaign(CampaignViewModel campaign)
        {
            if (campaign.Format.Equals((byte)CampaignFormat.Offline) && (bool)campaign.IsRecurring)
            {
                var keys = new List<int?>();
                campaign.CampaignOrganizations.Select(cos => cos.Schedules).ToList().ForEach(ss =>
                {
                    var temp = ss.Where(s => !s.Status.Equals((byte)CampaignStatus.End)).Select(s => s.Id);
                    if (temp.Any())
                        keys.AddRange(temp);
                });
                if (keys.Any())
                {
                    var schedules = _unitOfWork.Schedules.Get(s => keys.Contains(s.Id)).ToList();
                    var present = DateTime.Now;
                    schedules.ForEach(s =>
                    {
                        s = UpdateScheduleStatus(s, present);
                    });
                    _unitOfWork.Schedules.UpdateRange(schedules);
                    if (!_unitOfWork.Save())
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                        {
                            ErrorMessageConstants.RESTART,
                            ErrorMessageConstants.CAMPAIGN_ORGANIZATION_SCHEDULE.ToLower(),
                            MessageConstants.MESSAGE_FAILED
                        });
                }
            }
        }

        private Schedule UpdateScheduleStatus(Schedule schedule, DateTime? present = null)
        {
            if (!present.HasValue)
                present = DateTime.Now;
            //Not started
            if (DateTime.Compare((DateTime)schedule.StartDate, DateTime.Now) > 0 &&
                DateTime.Compare((DateTime)schedule.EndDate, DateTime.Now) > 0)
                schedule.Status = (byte)CampaignStatus.NotStarted;
            //Start
            else if (DateTime.Compare((DateTime)schedule.StartDate, DateTime.Now) <= 0 &&
                DateTime.Compare((DateTime)schedule.EndDate, DateTime.Now) > 0)
                schedule.Status = (byte)CampaignStatus.Start;
            //End
            else if (DateTime.Compare((DateTime)schedule.StartDate, DateTime.Now) < 0 &&
                DateTime.Compare((DateTime)schedule.EndDate, DateTime.Now) <= 0 &&
                !schedule.Status.Equals((byte)CampaignStatus.End))
                schedule.Status = (byte)CampaignStatus.End;
            return schedule;
        }
        #endregion

        #endregion
    }

    internal class UpdatedSchedule
    {
        public List<Schedule> updatedSchedule { get; set; } = new List<Schedule>();
        public List<Schedule> deletedSchedule { get; set; } = new List<Schedule>();

    }
}
