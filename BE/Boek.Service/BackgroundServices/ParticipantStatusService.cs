using Boek.Core.Entities;
using Boek.Core.Enums;
using Boek.Core.Extensions;
using Boek.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Boek.Service.BackgroundServices
{
    public class ParticipantStatusService : BackgroundService
    {
        #region Fields and constructor
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ParticipantStatusService> _logger;
        private Timer _timer = null;
        public ParticipantStatusService(IServiceProvider services, ILogger<ParticipantStatusService> logger)
        {
            var scope = services.CreateScope();
            _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            _logger = logger;
        }
        #endregion

        #region Background service
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(10));
            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            try
            {
                var participants = await _unitOfWork.Participants.Get()
                .Include(p => p.Campaign)
                .ToListAsync();
                if (participants.Any())
                {
                    var validStatus = new List<byte?>()
                    {
                        (byte) ParticipantStatus.PendingInvitation,
                        (byte) ParticipantStatus.PendingRequest
                    };

                    var campaignValidStatus = new List<byte?>()
                    {
                        (byte) CampaignStatus.Start,
                        (byte) CampaignStatus.End,
                        (byte) CampaignStatus.Cancelled
                    };
                    var list = participants.Where(p =>
                    validStatus.Contains(p.Status) &&
                    campaignValidStatus.Contains(p.Campaign.Status)).ToList();
                    _logger.LogInformation("============= Participant Status Info =============");
                    if (list.Any())
                        UpdateParticipants(list);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
        #endregion

        #region Utils
        private void UpdateParticipants(List<Participant> list)
        {
            var campaignParticipantStatuses = new List<CampaignParticipantStatus>()
            {
                new CampaignParticipantStatus(CampaignStatus.Start, ParticipantStatus.CancelledDueStartDate),
                new CampaignParticipantStatus(CampaignStatus.End, ParticipantStatus.CancelledDueEndDate),
                new CampaignParticipantStatus(CampaignStatus.Cancelled, ParticipantStatus.CancelledDueCancelledCampaign),
            };
            campaignParticipantStatuses.ForEach(cps =>
            {
                var temp = list.Where(c =>
                c.Campaign.Status.Equals((byte)cps.CampaignStatus) &&
                !cps.OtherParticipantStatus.Contains(c.Campaign.Status)).ToList();
                if (temp.Any())
                    UpdateParticipantByStatus(temp, cps.ParticipantStatus);
            });
        }

        private void UpdateParticipantByStatus(List<Participant> list, ParticipantStatus participantStatus)
        {
            var statusName = StatusExtension<ParticipantStatus>.GetStatus((byte)participantStatus);
            _logger.LogInformation($"[>> {statusName}]");
            list.ForEach(async p =>
                {
                    if (!p.Status.Equals((byte)participantStatus))
                    {
                        p.Status = (byte)participantStatus;
                        p.UpdatedDate = DateTime.Now;
                        await _unitOfWork.Participants.UpdateAsync(p);
                        if (_unitOfWork.Save())
                            _logger.LogInformation($"[Change] : <{p.Id}> - Campaign: {p.CampaignId} - Issuer: {p.IssuerId}");
                        else
                            _logger.LogError($"[Error] : <{p.Id}> - Campaign: {p.CampaignId} - Issuer: {p.IssuerId}");
                    }
                });
        }
        #endregion
    }

    internal class CampaignParticipantStatus
    {
        public CampaignParticipantStatus(CampaignStatus campaignStatus, ParticipantStatus participantStatus)
        {
            this.CampaignStatus = campaignStatus;
            this.ParticipantStatus = participantStatus;
            SetOtherCancelledStatus();
        }
        public CampaignStatus CampaignStatus { get; set; }
        public ParticipantStatus ParticipantStatus { get; set; }
        public List<byte?> OtherParticipantStatus { get; set; }

        private void SetOtherCancelledStatus()
        {
            switch (this.ParticipantStatus)
            {
                case ParticipantStatus.CancelledDueStartDate:
                    this.OtherParticipantStatus = new List<byte?>
                    {
                        (byte) ParticipantStatus.CancelledDueEndDate,
                        (byte) ParticipantStatus.CancelledDueCancelledCampaign
                    };
                    break;
                case ParticipantStatus.CancelledDueEndDate:
                    this.OtherParticipantStatus = new List<byte?>
                    {
                        (byte) ParticipantStatus.CancelledDueStartDate,
                        (byte) ParticipantStatus.CancelledDueCancelledCampaign
                    };
                    break;
                case ParticipantStatus.CancelledDueCancelledCampaign:
                    this.OtherParticipantStatus = new List<byte?>
                    {
                        (byte) ParticipantStatus.CancelledDueEndDate,
                        (byte) ParticipantStatus.CancelledDueStartDate
                    };
                    break;
            }
        }
    }
}