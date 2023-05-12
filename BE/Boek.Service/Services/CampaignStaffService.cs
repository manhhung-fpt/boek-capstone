using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boek.Core.Constants;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.CampaignStaffs;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.CampaignStaffs;
using Boek.Repository.Interfaces;
using Boek.Service.Commons;
using Boek.Service.Interfaces;
using Boek.Service.Utils;
using Microsoft.EntityFrameworkCore;
using Boek.Core.Extensions;
using Boek.Core.Enums;
using Boek.Infrastructure.ViewModels.Campaigns;
using Boek.Infrastructure.ViewModels.Users;
using Microsoft.AspNetCore.Http;
using System.Net;
using DateTimeExtensions;

namespace Boek.Service.Services
{
    public class CampaignStaffService : ICampaignStaffService
    {
        #region Fields and constructor
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CampaignStaffService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Gets
        public CampaignStaffViewModel GetCampaignStaffById(int id)
        {
            var CampaignStaff = _unitOfWork.CampaignStaffs.Get(id);
            if (CampaignStaff == null)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.CAMPAIGN_STAFF_ID
                });
            }
            return GetResponse(CampaignStaff);
        }

        public BaseResponsePagingModel<CampaignStaffViewModel> GetCampaignStaffs(CampaignStaffRequestModel filter, PagingModel paging)
        {
            var _filter = _mapper.Map<CampaignStaffViewModel>(filter);
            var result = _unitOfWork.CampaignStaffs.Get()
                .ProjectTo<CampaignStaffViewModel>(_mapper.ConfigurationProvider)
                .DynamicFilter(_filter)
                .PagingQueryable(paging.Page, paging.Size, PageConstant.LimitPaging, PageConstant.LimitPaging);
            var list = result.Item2.ToList();
            if (list.Any())
                list.ForEach(cs => cs = ServiceUtils.GetResponseDetail(cs));
            return new BaseResponsePagingModel<CampaignStaffViewModel>()
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

        public StaffCampaignsViewModel GetStaffCampaignsByStaffId()
        {
            var StaffId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            var CampaignStaff = _unitOfWork.CampaignStaffs
            .Get(cs => cs.StaffId.Equals(StaffId))
            .ProjectTo<StaffCampaignsViewModel>(_mapper.ConfigurationProvider)
            .SingleOrDefault();
            if (CampaignStaff == null)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.CAMPAIGN_ID
                });
            }
            var campaigns = _unitOfWork.CampaignStaffs.GetCampaigns(StaffId);
            CampaignStaff.Campaigns = _mapper.Map<List<BasicCampaignViewModel>>(campaigns);
            return ServiceUtils.GetResponseDetails(CampaignStaff);
        }

        public CampaignStaffsViewModel GetCampaignStaffsByCampaignId(int CampaignId)
        {
            var _campaign = _unitOfWork.Campaigns.Get(CampaignId);
            if (_campaign == null)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.CAMPAIGN_ID
                });
            }
            var CampaignStaff = new CampaignStaffsViewModel();
            _mapper.Map(_campaign, CampaignStaff);
            var staffs = _unitOfWork.CampaignStaffs.GetStaffs(CampaignId);
            CampaignStaff.Staffs = _mapper.Map<List<UserViewModel>>(staffs);
            return ServiceUtils.GetResponseDetails(CampaignStaff);
        }

        public IEnumerable<UserViewModel> GetUnattendedCampaignStaffs(int CampaignId)
        {
            var _campaign = CheckUnattendedCampaignStaff(CampaignId);
            var _staffs = _unitOfWork.Users
            .Get(u => u.Role.Equals((byte)BoekRole.Staff) && (u.CampaignStaffs.Any(css => !css.CampaignId.Equals(CampaignId)) || !u.CampaignStaffs.Any()))
            .Include(u => u.CampaignStaffs)
            .ThenInclude(CampaignStaff => CampaignStaff.Campaign)
            .ToList();
            if (_staffs.Any())
            {
                var startDate = (DateTime)_campaign.StartDate;
                var endDate = (DateTime)_campaign.EndDate;
                var list = new List<UserViewModel>();
                _staffs.ForEach(ss =>
                {
                    var result = CheckUnattendedStaffByCampaignDatesAndStatus(ss.CampaignStaffs.ToList(), startDate, endDate);
                    if (result && !list.Any(item => item.Id.Equals(ss.Id)))
                        list.Add(_mapper.Map<UserViewModel>(ss));
                });
                return list.Any() ? list : null;
            }
            return null;
        }
        #endregion

        #region Create and Updates 
        public List<BasicCampaignStaffViewModel> CreateCampaignStaffs(CreateCampaignStaffRequestModel createdCampaignStaff)
        {
            CheckCreateCampaignStaffs(createdCampaignStaff);
            var list = GetCampaignStaffs(createdCampaignStaff);
            _unitOfWork.CampaignStaffs.AddRange(list);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.INSERT,
                    ErrorMessageConstants.CAMPAIGN_STAFF.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            return GetResponses(list);
        }

        public BasicCampaignStaffViewModel UpdateCampaignStaffStatusIntoAttendedStatus(int id)
        {
            var CampaignStaff = new CampaignStaff();
            CheckUpdateCampaignStaffStatus(ref CampaignStaff, id);
            if (CampaignStaff.Status.Equals((byte)CampaignStaffStatus.Unattended))
            {
                CampaignStaff.Status = (byte)CampaignStaffStatus.Attended;
                _unitOfWork.CampaignStaffs.Update(CampaignStaff);
                if (!_unitOfWork.Save())
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                    {
                        ErrorMessageConstants.UPDATE,
                        ErrorMessageConstants.CAMPAIGN_STAFF,
                        MessageConstants.MESSAGE_FAILED
                    });
            }
            var responses = GetResponses(new List<CampaignStaff>()
            {
                CampaignStaff
            });
            return responses.First();
        }

        public BasicCampaignStaffViewModel UpdateCampaignStaffStatusIntoUnAttendedStatus(int id)
        {
            var CampaignStaff = new CampaignStaff();
            CheckUpdateCampaignStaffStatus(ref CampaignStaff, id);
            if (CampaignStaff.Status.Equals((byte)CampaignStaffStatus.Attended))
            {
                CampaignStaff.Status = (byte)CampaignStaffStatus.Unattended;
                _unitOfWork.CampaignStaffs.Update(CampaignStaff);
                if (!_unitOfWork.Save())
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                    {
                        ErrorMessageConstants.UPDATE,
                        ErrorMessageConstants.CAMPAIGN_STAFF,
                        MessageConstants.MESSAGE_FAILED
                    });
            }
            var responses = GetResponses(new List<CampaignStaff>()
            {
                CampaignStaff
            });
            return responses.First();
        }
        #endregion

        #region Utils
        private CampaignStaffViewModel GetResponse(CampaignStaff campaignStaff)
        {
            var _response = _unitOfWork.CampaignStaffs.Get(cs => cs.Id.Equals(campaignStaff.Id))
            .ProjectTo<CampaignStaffViewModel>(_mapper.ConfigurationProvider)
            .SingleOrDefault();
            return _response != null ? ServiceUtils.GetResponseDetail(_response) : new CampaignStaffViewModel();
        }

        private List<BasicCampaignStaffViewModel> GetResponses(List<CampaignStaff> list)
        {
            var result = new List<BasicCampaignStaffViewModel>();
            list.ForEach(cs =>
            {
                var _campaignStaff = _unitOfWork.CampaignStaffs.Get(css =>
                css.CampaignId.Equals(cs.CampaignId) &&
                css.StaffId.Equals(cs.StaffId))
                .ProjectTo<BasicCampaignStaffViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefault();
                if (_campaignStaff == null)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                    {
                        ErrorMessageConstants.NOT_FOUND,
                        ErrorMessageConstants.CAMPAIGN_STAFF.ToLower(),
                    });
                _campaignStaff.StatusName = StatusExtension<CampaignStaffStatus>.GetStatus(_campaignStaff.Status);
                result.Add(_campaignStaff);
            });
            return result;
        }

        private void CheckCreateCampaignStaffs(CreateCampaignStaffRequestModel createdCampaignStaff)
        {
            var _campaign = CheckNullCampaign(createdCampaignStaff.CampaignId);
            CheckCampaignDetail(_campaign);
            if (!createdCampaignStaff.StaffIds.Any())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.PreconditionRequired), new string[]
                {
                    MessageConstants.MESSAGE_REQUIRED,
                    ErrorMessageConstants.CAMPAIGN_STAFF_ID,
                });
            var startDate = (DateTime)_campaign.StartDate;
            var endDate = (DateTime)_campaign.EndDate;
            createdCampaignStaff.StaffIds.ForEach(sis =>
            {
                var _staff = _unitOfWork.Users.Get(u => u.Id.Equals(sis))
                .Include(u => u.CampaignStaffs)
                .ThenInclude(CampaignStaff => CampaignStaff.Campaign)
                .SingleOrDefault();
                if (_staff == null)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                    {
                        ErrorMessageConstants.NOT_FOUND,
                        ErrorMessageConstants.USER_ID,
                        sis.ToString()
                    });
                if (!_staff.Role.Equals((byte)BoekRole.Staff))
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                        ErrorMessageConstants.USER_ROLE,
                        MessageConstants.MESSAGE_INVALID,
                        sis.ToString()
                });
                var _campaignStaff = _unitOfWork.CampaignStaffs
                .Get(cs =>
                cs.CampaignId.Equals(createdCampaignStaff.CampaignId) &&
                cs.StaffId.Equals(sis))
                .SingleOrDefault();
                if (_campaignStaff != null)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                    {
                        ErrorMessageConstants.CAMPAIGN_STAFF,
                        MessageConstants.MESSAGE_EXISTED,
                        sis.ToString()
                    });
                CheckUnattendedStaffByCampaignDatesAndStatus(_staff.CampaignStaffs.ToList(), startDate, endDate, true);
            });
        }

        private void CheckUpdateCampaignStaffStatus(ref CampaignStaff CampaignStaff, int id)
        {
            CampaignStaff = _unitOfWork.CampaignStaffs
            .Get(cs => cs.Id.Equals(id))
            .Include(c => c.Campaign)
            .SingleOrDefault();
            if (CampaignStaff == null)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.CAMPAIGN_STAFF_ID
                });
            }
            CheckCampaignDetail(CampaignStaff.Campaign);
        }

        private Campaign CheckNullCampaign(int? CampaignId)
        {
            var _campaign = _unitOfWork.Campaigns.Get(CampaignId);
            if (_campaign == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                        ErrorMessageConstants.NOT_FOUND,
                        ErrorMessageConstants.CAMPAIGN_ID,
                });
            return _campaign;
        }

        private void CheckCampaignDetail(Campaign campaign)
        {
            CheckCampaignFormat(campaign.Format, new List<byte>()
            {
                (byte)CampaignFormat.Offline
            });
            CheckCampaignStatus(campaign);
        }

        private void CheckCampaignFormat(byte? Format, List<byte> validFormat)
        {
            if (!validFormat.Contains((byte)Format))
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    MessageConstants.MESSAGE_INVALID,
                    ErrorMessageConstants.CAMPAIGN_FORMAT
                });
        }

        private void CheckCampaignStatus(Campaign campaign)
        {
            var invalidStatus = new List<byte?>()
            {
                (byte)CampaignStatus.Cancelled,
                (byte)CampaignStatus.Postponed,
                (byte)CampaignStatus.End
            };
            if (invalidStatus.Contains(campaign.Status))
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.UPDATE,
                    ErrorMessageConstants.CAMPAIGN.ToLower(),
                    MessageConstants.MESSAGE_FAILED,
                    ErrorMessageConstants.CAMPAIGN_CANCEL
                });
        }

        private List<CampaignStaff> GetCampaignStaffs(CreateCampaignStaffRequestModel createdCampaignStaff)
        {
            var list = new List<CampaignStaff>();
            var _campaignId = createdCampaignStaff.CampaignId;
            createdCampaignStaff.StaffIds.ForEach(sis => list.Add(new CampaignStaff()
            {
                CampaignId = _campaignId,
                StaffId = sis,
                Status = (byte)CampaignStaffStatus.Attended
            }));
            return list;
        }

        private Campaign CheckUnattendedCampaignStaff(int CampaignId)
        {
            var _campaign = CheckNullCampaign(CampaignId);
            CheckCampaignDetail(_campaign);
            return _campaign;
        }
        /// <summary>
        /// Check valid staff to attend new campaign by previous campaigns' dates (<paramref name="campaignStaff"/>, <paramref name="ShowErrorMessage"/>)
        /// </summary>
        /// <param name="campaignStaff"></param>
        /// <param name="ShowErrorMessage"></param>
        /// <example>
        /// New campaigns (1/2 - 31/3)
        /// Invalid examples when it compares between new campaigns and old starting campaigns, and staff has attended old ones:
        /// 1. Same dates: Old campaign 1 (1/2 - 31/3)
        /// 2. Intersect Old campaign 2 (1/1 - 31/2)
        /// 3. Reverse intersect: Old campaign 3 (25/3 - 4/4)
        /// 4. Inside: Old campaign 4 (25/2 - 1/3)
        /// 5. Reverse inside: Old campaign 5 (1/1 - 4/4)
        /// => There are 3 situations (same, intersect, and inside) to check whether it is valid or not.
        /// </example>
        /// <returns></returns>
        private bool CheckUnattendedStaffByCampaignDatesAndStatus(List<CampaignStaff> campaignStaff, DateTime startDate, DateTime endDate, bool ShowErrorMessage = false)
        {
            //Get campaign staff which campaign starts, and staff has attended it
            var validStatus = new List<byte?>()
            {
                (byte) CampaignStatus.NotStarted,
                (byte) CampaignStatus.Start
            };
            campaignStaff = campaignStaff.Where(cf => validStatus.Contains(cf.Campaign.Status) &&
            cf.Status.Equals((byte)CampaignStaffStatus.Attended)).ToList();

            //Check same dates
            var sameDates = campaignStaff.Where(cf =>
            ((DateTime)cf.Campaign.StartDate).IsTheSameAs((DateTime)cf.Campaign.EndDate, startDate, endDate));
            if (sameDates.Any())
            {
                if (ShowErrorMessage)
                {
                    var list = sameDates.Select(cs => cs.Id).ToList();
                    var message = GenerateMessage(list);
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                    {
                        ErrorMessageConstants.CAMPAIGN_STAFF_ID,
                        list.First().ToString(),
                        MessageConstants.MESSAGE_INVALID,
                        ErrorMessageConstants.CAMPAIGN_STAFF_SAME_DATES,
                        ErrorMessageConstants.CAMPAIGN_STAFF_WITH,
                        message
                    });
                }
                else
                    return false;
            }

            //Check intersect dates
            var intersectDates = campaignStaff.Where(cf =>
            //Intersect
            (((DateTime)cf.Campaign.StartDate) <= startDate && ((DateTime)cf.Campaign.EndDate) <= endDate) ||
            //Reserve intersect
            (((DateTime)cf.Campaign.StartDate) > startDate && ((DateTime)cf.Campaign.EndDate) > endDate));
            if (intersectDates.Any())
            {
                if (ShowErrorMessage)
                {
                    var list = intersectDates.Select(cs => cs.Id).ToList();
                    var message = GenerateMessage(list);
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                    {
                        ErrorMessageConstants.CAMPAIGN_STAFF_ID,
                        list.First().ToString(),
                        MessageConstants.MESSAGE_INVALID,
                        ErrorMessageConstants.CAMPAIGN_STAFF_INTERSECT_DATES,
                        ErrorMessageConstants.CAMPAIGN_STAFF_WITH,
                        message
                    });
                }
                else
                    return false;
            }

            //Check inside dates
            var insideDates = campaignStaff.Where(cf =>
            //Inside
            (((DateTime)cf.Campaign.StartDate) >= startDate && ((DateTime)cf.Campaign.EndDate) <= endDate) ||
            //Reserve inside
            (((DateTime)cf.Campaign.StartDate) < startDate && ((DateTime)cf.Campaign.EndDate) > endDate));
            if (insideDates.Any())
            {
                if (ShowErrorMessage)
                {
                    var list = insideDates.Select(cs => cs.Id).ToList();
                    var message = GenerateMessage(list);
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                    {
                        ErrorMessageConstants.CAMPAIGN_STAFF_ID,
                        list.First().ToString(),
                        MessageConstants.MESSAGE_INVALID,
                        ErrorMessageConstants.CAMPAIGN_STAFF_INSIDE_DATES,
                        ErrorMessageConstants.CAMPAIGN_STAFF_WITH,
                        message
                    });
                }
                else
                    return false;
            }
            return true;
        }

        private string GenerateMessage<T>(List<T> list)
        {
            var message = "";
            list.ForEach(item =>
            {
                if (string.IsNullOrEmpty(message))
                    message = item.ToString();
                else
                    message += ", " + item.ToString();
            });
            return message;
        }
        #endregion
    }
}