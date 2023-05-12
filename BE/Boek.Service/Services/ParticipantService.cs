using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boek.Core.Constants;
using Boek.Core.Entities;
using Boek.Core.Extensions;
using Boek.Infrastructure.Requests.Participants;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.Participants;
using Boek.Repository.Interfaces;
using Boek.Service.Commons;
using Boek.Service.Interfaces;
using Boek.Service.Utils;
using Boek.Core.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Boek.Infrastructure.Requests.Notifications;

namespace Boek.Service.Services
{
    public class ParticipantService : IParticipantService
    {
        #region Fields and constructor
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly INotificationService _notificationService;

        public ParticipantService(IUnitOfWork unitOfWork,
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

        #region CUD
        public ParticipantViewModel ApplyParticipant(ApplyParticipantRequestModel appliedParticipant)
        {
            var _newParticipant = _mapper.Map<Participant>(appliedParticipant);
            _newParticipant.IssuerId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            Campaign _campaign;
            User _issuer;
            Participant _participant;
            string Note;
            CheckParticipant(_newParticipant, out _participant, out _campaign, out _issuer, out Note);
            var createdDate = DateTime.Now;
            _participant = new Participant()
            {
                CampaignId = appliedParticipant.CampaignId,
                IssuerId = _issuer.Id,
                CreatedDate = createdDate,
                UpdatedDate = null,
                Status = (byte)ParticipantStatus.PendingRequest,
                Note = $"{_issuer.Name} {MessageConstants.MESSAGE_REQUEST_PARTICIPANT} {_campaign.Name}" + Note ?? ""
            };
            _participant.Note = _participant.Note.Trim();
            _unitOfWork.Participants.Create(_participant);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[] {
                    ErrorMessageConstants.APPLY,
                    ErrorMessageConstants.PARTICIPANT.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            _participant = _unitOfWork.Participants.Get(p =>
            p.CampaignId.Equals(_participant.CampaignId) &&
            p.IssuerId.Equals(_participant.IssuerId) &&
            p.CreatedDate.Equals(createdDate)).SingleOrDefault();
            return GetRespond(_participant);
        }

        public IEnumerable<ParticipantViewModel> CreateParticipants(InviteParticipantRequestModel invitedParticipant)
        {
            var createdDate = DateTime.Now;
            var _newParticipants = ConvertToParticipants(new List<int?>(){
                invitedParticipant.CampaignId
            }, invitedParticipant.Issuers, createdDate);
            ICollection<Participant> list = new List<Participant>();
            foreach (var _newParticipant in _newParticipants)
            {
                Campaign _campaign;
                User _issuer;
                Participant _participant;
                string Note;
                CheckParticipant(_newParticipant, out _participant, out _campaign, out _issuer, out Note);
                _participant = new Participant()
                {
                    CampaignId = _newParticipant.CampaignId,
                    IssuerId = _newParticipant.IssuerId,
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Note = $"{_issuer.Name} {MessageConstants.MESSAGE_INVITED_PARTICIPANT} {_campaign.Name}" + Note ?? "",
                    Status = (byte)ParticipantStatus.PendingInvitation
                };
                _participant.Note = _participant.Note.Trim();
                list.Add(_participant);
            }
            _unitOfWork.Participants.AddRange(list);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[] {
                    ErrorMessageConstants.APPLY,
                    ErrorMessageConstants.PARTICIPANT.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            return GetResponds(_newParticipants);
        }

        public ParticipantViewModel UpdateParticipant(UpdateParticipantRequestModel updatedParticipant, bool isAdmin = true)
        {
            if (!isAdmin)
                CheckLoginUser(updatedParticipant.IssuerId);
            var _participant = _unitOfWork.Participants
                .Get(p => p.Id.Equals(updatedParticipant.Id))
                .Include(p => p.Campaign)
                .SingleOrDefault();
            CheckUpdateParticipant(_participant);
            _mapper.Map(updatedParticipant, _participant);
            _participant.UpdatedDate = DateTime.Now;
            _unitOfWork.Participants.Update(_participant);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[] {
                    ErrorMessageConstants.UPDATE,
                    ErrorMessageConstants.PARTICIPANT.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            return GetRespond(_participant);
        }

        public ParticipantViewModel DeleteParticipant(int id)
        {
            var _participant = _unitOfWork.Participants.Get(p => p.Id.Equals(id))
            .Include(p => p.Campaign)
            .SingleOrDefault();
            if (_participant == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[] {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.PARTICIPANT_ID
                });
            var ValidList = new List<byte>()
            {
                (byte) ParticipantStatus.PendingRequest,
                (byte) ParticipantStatus.PendingInvitation,
                (byte) ParticipantStatus.RejectedInvitation,
                (byte) ParticipantStatus.RejectedRequest,
                (byte) ParticipantStatus.Approved,
                (byte) ParticipantStatus.Accepted
            };
            var status = ParticipantStatus.Cancelled;
            CheckCancelParticipant(_participant, ValidList, status);
            _participant.Status = (byte)status;
            _unitOfWork.Participants.Update(_participant);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[] {
                    ErrorMessageConstants.UPDATE,
                    ErrorMessageConstants.PARTICIPANT_STATUS,
                    ParticipantStatus.Cancelled.ToEnumMemberAttrValue().ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            return GetRespond(_participant);
        }
        #endregion

        #region Gets
        public ParticipantViewModel GetParticipantById(int participantId)
        {
            var _participant = _unitOfWork.Participants.Get(participantId);
            if (_participant == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[] {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.PARTICIPANT_ID
                });
            return GetRespond(_participant);
        }

        public ParticipantViewModel GetParticipantByIdByIssuer(int participantId)
        {
            var _participant = _unitOfWork.Participants.Get(participantId);
            if (_participant == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[] {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.PARTICIPANT_ID
                });
            CheckLoginUser(_participant.IssuerId);
            return GetRespond(_participant);
        }

        public BaseResponsePagingModel<ParticipantViewModel> GetParticipants(ParticipantRequestModel filter, PagingModel paging)
        {
            var query = _unitOfWork.Participants.Get()
                .ProjectTo<ParticipantViewModel>(_mapper.ConfigurationProvider)
                .OrderByDescending(x => x.UpdatedDate)
                .ThenByDescending(x => x.CreatedDate)
                .ThenBy(x => x.IssuerId)
                .DynamicOtherFilter(filter);
            var list = new List<ParticipantViewModel>();
            var count = 0;
            if (query.Any())
            {
                if (!filter.NotPaging)
                {
                    var result = query.PagingQueryable(paging.Page, paging.Size, PageConstant.LimitPaging, PageConstant.LimitPaging);
                    count = result.Item1;
                    list = result.Item2.ToList();
                }
                else
                {
                    list = query.ToList();
                    count = list.Count();
                }
                list.ForEach(p => p = ServiceUtils.GetResponseDetail(p));
            }
            return new BaseResponsePagingModel<ParticipantViewModel>()
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

        public BaseResponsePagingModel<ParticipantViewModel> GetParticipantsByIssuer(ParticipantRequestModel filter, PagingModel paging)
        {
            var IssuerId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            var query = _unitOfWork.Participants.Get(p => p.IssuerId.Equals(IssuerId))
                .OrderByDescending(p => p.UpdatedDate)
                .ThenByDescending(p => p.CreatedDate)
                .ThenBy(p => p.Status)
                .ProjectTo<ParticipantViewModel>(_mapper.ConfigurationProvider)
                .DynamicOtherFilter(filter);
            var list = new List<ParticipantViewModel>();
            var count = 0;
            if (query.Any())
            {
                if (!filter.NotPaging)
                {
                    var result = query.PagingQueryable(paging.Page, paging.Size, PageConstant.LimitPaging, PageConstant.LimitPaging);
                    count = result.Item1;
                    list = result.Item2.ToList();
                }
                else
                {
                    list = query.ToList();
                    count = list.Count();
                }
                list.ForEach(p => p = ServiceUtils.GetResponseDetail(p));
            }
            return new BaseResponsePagingModel<ParticipantViewModel>()
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
        #endregion

        #region Update Statuses
        public ParticipantViewModel UpdateAcceptedParticipant(int id)
        {
            var _participant = _unitOfWork.Participants
                .Get(p => p.Id.Equals(id))
                .Include(p => p.Campaign)
                .SingleOrDefault();
            var ValidList = new List<byte>()
            {
                (byte) ParticipantStatus.PendingInvitation
            };
            var _status = ParticipantStatus.Accepted;
            CheckUpdateStatus(_participant, ValidList, _status, true);
            CheckLoginUser(_participant.IssuerId);
            UpdateParticipantDetailByStatus(_participant, _status);
            return GetRespond(_participant);
        }

        public ParticipantViewModel UpdateApprovedParticipant(int id)
        {
            var _participant = _unitOfWork.Participants
                .Get(p => p.Id.Equals(id))
                .Include(p => p.Campaign)
                .SingleOrDefault();
            var ValidList = new List<byte>()
            {
                (byte) ParticipantStatus.PendingRequest
            };
            var _status = ParticipantStatus.Approved;
            CheckUpdateStatus(_participant, ValidList, _status, true);
            UpdateParticipantDetailByStatus(_participant, _status);
            return GetRespond(_participant);
        }

        public ParticipantViewModel UpdateCancelledStartDueDateParticipant(int id)
        {
            var _participant = _unitOfWork.Participants
                .Get(p => p.Id.Equals(id))
                .Include(p => p.Campaign)
                .SingleOrDefault();
            var ValidList = new List<byte>()
            {
                (byte) ParticipantStatus.PendingRequest,
                (byte) ParticipantStatus.PendingInvitation,
                (byte) ParticipantStatus.RejectedInvitation,
                (byte) ParticipantStatus.RejectedRequest,
                (byte) ParticipantStatus.Approved,
                (byte) ParticipantStatus.Accepted
            };
            var _status = ParticipantStatus.CancelledDueStartDate;
            CheckUpdateStatus(_participant, ValidList, _status);
            UpdateParticipantDetailByStatus(_participant, _status);
            return GetRespond(_participant);
        }

        public ParticipantViewModel UpdateCancelledParticipant(int id)
        {
            var _participant = _unitOfWork.Participants
                .Get(p => p.Id.Equals(id))
                .Include(p => p.Campaign)
                .SingleOrDefault();
            var ValidList = new List<byte>()
            {
                (byte) ParticipantStatus.PendingRequest,
                (byte) ParticipantStatus.PendingInvitation,
                (byte) ParticipantStatus.RejectedInvitation,
                (byte) ParticipantStatus.RejectedRequest,
                (byte) ParticipantStatus.Approved,
                (byte) ParticipantStatus.Accepted
            };
            var _status = ParticipantStatus.Cancelled;
            CheckUpdateStatus(_participant, ValidList, _status);
            UpdateParticipantDetailByStatus(_participant, _status);
            return GetRespond(_participant);
        }

        public ParticipantViewModel UpdateRejectedInvitationParticipant(int id)
        {
            var _participant = _unitOfWork.Participants
                .Get(p => p.Id.Equals(id))
                .Include(p => p.Campaign)
                .SingleOrDefault();
            var ValidList = new List<byte>()
            {
                (byte) ParticipantStatus.PendingInvitation
            };
            var _status = ParticipantStatus.RejectedInvitation;
            CheckUpdateStatus(_participant, ValidList, _status, true);
            CheckLoginUser(_participant.IssuerId);
            UpdateParticipantDetailByStatus(_participant, _status);
            return GetRespond(_participant);
        }

        public ParticipantViewModel UpdateRejectedRequestParticipant(int id)
        {
            var _participant = _unitOfWork.Participants
                .Get(p => p.Id.Equals(id))
                .Include(p => p.Campaign)
                .SingleOrDefault();
            var ValidList = new List<byte>()
            {
                (byte) ParticipantStatus.PendingRequest
            };
            var _status = ParticipantStatus.RejectedRequest;
            CheckUpdateStatus(_participant, ValidList, _status, true);
            UpdateParticipantDetailByStatus(_participant, _status);
            return GetRespond(_participant);
        }
        #endregion

        #region Notification
        public void SendCheckingIssuerRequestNotification(ParticipantViewModel participant)
        {
            var notification = new NotificationRequestModel()
            {
                UserRoles = new List<byte>() { (byte)BoekRole.Admin },
                Status = participant.Status,
                StatusName = participant.StatusName,
                Message = participant.Note
            };
            _notificationService.PushCheckingIssuerRequestNotification(notification);
        }

        public void SendCheckingAdminInvitationNotifications(IEnumerable<ParticipantViewModel> participants)
        {
            var notifications = new List<NotificationRequestModel>();
            foreach (var participant in participants)
            {
                var notification = new NotificationRequestModel()
                {
                    UserIds = new List<Guid?>() { participant.IssuerId },
                    Status = participant.Status,
                    StatusName = participant.StatusName,
                    Message = participant.Note
                };
                notifications.Add(notification);
            }
            if (notifications.Any())
                _notificationService.PushCheckingAdminInvitationNotification(notifications);
        }

        public void SendParticipantNotification(ParticipantViewModel participant)
        {
            Dictionary<byte?, string> statuses = new Dictionary<byte?, string>()
            {
                {(byte?)ParticipantStatus.Approved, MessageConstants.NOTI_PARTICIPANT_ADMIN_APPROVAL_MESS},
                {(byte?)ParticipantStatus.RejectedRequest, MessageConstants.NOTI_PARTICIPANT_ADMIN_REJECTION_MESS},
                {(byte?)ParticipantStatus.Accepted, MessageConstants.NOTI_PARTICIPANT_ISSUER_APPROVAL_MESS},
                {(byte?)ParticipantStatus.RejectedInvitation, MessageConstants.NOTI_PARTICIPANT_ISSUER_REJECTION_MESS},
                {(byte?)ParticipantStatus.Cancelled, MessageConstants.NOTI_PARTICIPANT_ADMIN_CANCELLATION_MESS},
            };
            var issuerStatuses = new List<byte?>()
            {
                (byte?)ParticipantStatus.Accepted,
                (byte?)ParticipantStatus.RejectedInvitation
            };
            var result = statuses.SingleOrDefault(s => s.Key.Equals(participant.Status));
            if (!result.Equals(default(KeyValuePair<byte?, string>)))
            {
                NotificationRequestModel notification = null;
                if (issuerStatuses.Contains(result.Key))
                {
                    notification = new NotificationRequestModel()
                    {
                        UserRoles = new List<byte>() { (byte)BoekRole.Admin },
                        Status = participant.Status,
                        StatusName = participant.StatusName,
                        Message = participant.Issuer.User.Name + " " + result.Value
                    };
                }
                else
                {
                    notification = new NotificationRequestModel()
                    {
                        UserIds = new List<Guid?>() { participant.IssuerId },
                        Status = participant.Status,
                        StatusName = participant.StatusName,
                        Message = result.Value
                    };
                }
                if (notification != null)
                    _notificationService.PushParticipantNotification(notification);
            }
        }
        #endregion

        #region Utils

        private ParticipantViewModel GetRespond(Participant participant)
        {
            var _response = _unitOfWork.Participants.Get(p => p.Id.Equals(participant.Id))
                .ProjectTo<ParticipantViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefault();
            if (_response != null)
                ServiceUtils.GetResponseDetail(_response);
            return _response ?? new ParticipantViewModel();
        }

        private IEnumerable<ParticipantViewModel> GetResponds(IEnumerable<Participant> participants)
        {
            ICollection<ParticipantViewModel> _responses = new List<ParticipantViewModel>();
            foreach (var participant in participants)
            {
                var response = _unitOfWork.Participants.Get(p =>
                p.CampaignId.Equals(participant.CampaignId) &&
                p.IssuerId.Equals(participant.IssuerId) &&
                p.CreatedDate.Equals(participant.CreatedDate))
                    .ProjectTo<ParticipantViewModel>(_mapper.ConfigurationProvider)
                    .SingleOrDefault();
                if (response != null)
                    _responses.Add(ServiceUtils.GetResponseDetail(response));
            }
            return _responses.Any() ? _responses : Enumerable.Empty<ParticipantViewModel>();
        }

        private void CheckLoginUser(Guid? Id)
        {
            var IssuerId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            if (!Id.Equals(IssuerId))
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[] {
                    ErrorMessageConstants.PARTICIPANT,
                    MessageConstants.MESSAGE_NOT_BELONGING,
                    ErrorMessageConstants.ISSUER
                });
        }

        //TO-DO: Check campaign postponed
        private void CheckParticipant(Participant _newParticipant,
            out Participant _participant,
            out Campaign _campaign,
            out User _issuer, out string Note)
        {
            _campaign = _unitOfWork.Campaigns.Get(c => c.Id.Equals(_newParticipant.CampaignId)).SingleOrDefault();
            _issuer = _unitOfWork.Users.Get(u =>
            u.Id.Equals(_newParticipant.IssuerId) &&
            u.Role.Equals((byte)BoekRole.Issuer)).SingleOrDefault();
            var cancelledList = new List<byte>()
            {
                (byte) ParticipantStatus.Cancelled,
                (byte) ParticipantStatus.CancelledDueStartDate,
                (byte) ParticipantStatus.CancelledDueEndDate,
                (byte) ParticipantStatus.CancelledDueCancelledCampaign,
                (byte) ParticipantStatus.RejectedRequest,
                (byte) ParticipantStatus.RejectedInvitation
            };
            var list = _unitOfWork.Participants.Get(
                p => p.CampaignId.Equals(_newParticipant.CampaignId) &&
                p.IssuerId.Equals(_newParticipant.IssuerId));
            Note = list.Any(p => cancelledList.Contains(p.Status)) ? $" lần {list.Count() + 1}" : "";
            _participant = list.SingleOrDefault(p => !cancelledList.Contains(p.Status));
            if (_campaign == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[] {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.CAMPAIGN.ToLower()
                });
            CheckDate((DateTime)_campaign.StartDate, (DateTime)_campaign.EndDate);
            if (_issuer == null)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[] {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.ISSUER
                });
            }
            if (_participant != null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[] {
                    ErrorMessageConstants.PARTICIPANT,
                    MessageConstants.MESSAGE_EXISTED
                });
        }

        private void CheckUpdateParticipant(Participant _participant)
        {
            var _campaign = _participant.Campaign;
            if (_participant == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[] {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.PARTICIPANT_ID
                });
            if (_campaign == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[] {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.CAMPAIGN.ToLower()
                });
            CheckDate((DateTime)_campaign.StartDate, (DateTime)_campaign.EndDate);
            CheckCancelledUpdateParticipant(_participant);
        }
        private void CheckUpdateStatus(Participant _participant, List<byte> ValidList, ParticipantStatus status, bool CheckCancelled = false)
        {
            if (_participant == null)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[] {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.PARTICIPANT.ToLower()
                });
            }
            var _campaign = _participant.Campaign;
            if (_campaign == null)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[] {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.CAMPAIGN.ToLower()
                });
            }
            CheckDate((DateTime)_campaign.StartDate, (DateTime)_campaign.EndDate, false);
            if (_participant.Status.Equals((byte)status))
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[] {
                    ErrorMessageConstants.UPDATED,
                    ErrorMessageConstants.PARTICIPANT_STATUS,
                    status.ToEnumMemberAttrValue().ToLower()
                });
            }
            if (CheckCancelled)
                CheckCancelledUpdateParticipant(_participant);
            CheckValidStatus(_participant, ValidList, status);
        }

        private void CheckCancelledUpdateParticipant(Participant _participant)
        {
            var participantValidStatus = new List<byte?>()
            {
                (byte)ParticipantStatus.Cancelled,
                (byte)ParticipantStatus.CancelledDueStartDate,
                (byte)ParticipantStatus.CancelledDueEndDate,
                (byte)ParticipantStatus.CancelledDueCancelledCampaign,
            };
            if (participantValidStatus.Contains(_participant.Status))
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[] {
                    ErrorMessageConstants.UPDATE,
                    ErrorMessageConstants.PARTICIPANT_STATUS,
                    MessageConstants.MESSAGE_FAILED,
                    ErrorMessageConstants.CANCELLED_PARTICIPANT_STATUS
                });
        }

        private void CheckCancelParticipant(Participant _participant, List<byte> ValidList, ParticipantStatus status)
        {
            var _campaign = _participant.Campaign;
            CheckDate((DateTime)_campaign.StartDate, (DateTime)_campaign.EndDate, false);
            CheckValidStatus(_participant, ValidList, status);
        }

        private void CheckValidStatus(Participant _participant, List<byte> ValidList, ParticipantStatus status)
        {
            if (!ValidList.Contains(_participant.Status))
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[] {
                    ErrorMessageConstants.UPDATE,
                    ErrorMessageConstants.PARTICIPANT_STATUS,
                    MessageConstants.MESSAGE_FAILED,
                    status.ToEnumMemberAttrValue().ToLower()
                });
        }

        private Participant UpdateParticipantDetailByStatus(Participant _participant, ParticipantStatus status)
        {
            _participant.UpdatedDate = DateTime.Now;
            _participant.Status = (byte)status;
            _unitOfWork.Participants.Update(_participant);
            if (!_unitOfWork.Save())
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[] {
                    ErrorMessageConstants.UPDATE,
                    ErrorMessageConstants.PARTICIPANT_STATUS,
                    status.ToEnumMemberAttrValue().ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            }
            _participant = _unitOfWork.Participants.Get(_participant.Id);
            return _participant;
        }

        private void CheckDate(DateTime startDate, DateTime endDate, bool IsCreate = true)
        {
            if (DateTime.Compare(endDate, DateTime.Now) <= 0)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[] {
                        IsCreate ? ErrorMessageConstants.INSERT :
                        ErrorMessageConstants.UPDATE,
                        ErrorMessageConstants.PARTICIPANT.ToLower(),
                        MessageConstants.MESSAGE_FAILED,
                        ErrorMessageConstants.CAMPAIGN_ENDED
                        });
            if (DateTime.Compare(startDate, DateTime.Now) <= 0)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[] {
                        IsCreate ? ErrorMessageConstants.INSERT :
                        ErrorMessageConstants.UPDATE,
                        ErrorMessageConstants.PARTICIPANT.ToLower(),
                        MessageConstants.MESSAGE_FAILED,
                        ErrorMessageConstants.CAMPAIGN_STARTED
                        });
        }

        private List<Participant> ConvertToParticipants(List<int?> campaigns, List<Guid?> issuers, DateTime createdDate, bool IsOnlyCampaign = true)
        {
            var list = new List<Participant>();
            if (IsOnlyCampaign)
            {
                issuers.ForEach(i =>
                {
                    list.Add(new Participant()
                    {
                        CampaignId = campaigns.First(),
                        IssuerId = i,
                        CreatedDate = createdDate
                    });
                });
            }
            else
            {
                campaigns.ForEach(c =>
                {
                    list.Add(new Participant()
                    {
                        CampaignId = c,
                        IssuerId = issuers.First(),
                        CreatedDate = createdDate
                    });
                });
            }
            return list;
        }
        #endregion
    }
}