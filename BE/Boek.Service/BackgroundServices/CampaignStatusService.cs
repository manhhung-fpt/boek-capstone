using Boek.Core.Entities;
using Boek.Repository.Interfaces;
using Boek.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Boek.Service.BackgroundServices
{
    public class CampaignStatusService : BackgroundService
    {
        #region Fields and constructor
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CampaignStatusService> _logger;
        private Timer _timer = null;
        public CampaignStatusService(IServiceProvider services, ILogger<CampaignStatusService> logger)
        {
            var scope = services.CreateScope();
            _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            _logger = logger;
        }
        #endregion

        #region Background service
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(30));
            return Task.CompletedTask;
        }

        //TO-DO: Check status of campaign organization
        private async void DoWork(object state)
        {
            try
            {
                var _campaigns = await _unitOfWork.Campaigns.Get()
                .Include(c => c.CampaignOrganizations)
                .ThenInclude(CampaignOrganization => CampaignOrganization.Schedules)
                .ToListAsync();
                if (_campaigns.Any())
                {
                    var _list = _campaigns.Where(c => !c.Status.Equals((byte)CampaignStatus.Cancelled)).ToList();
                    _logger.LogInformation("============= Campaign Status Info =============");
                    _logger.LogInformation("[>> Not start]");
                    CheckCampaignDates(_list, CampaignStatus.NotStarted);
                    _logger.LogInformation("[>> Start]");
                    CheckCampaignDates(_list, CampaignStatus.Start);
                    _logger.LogInformation("[>> End]");
                    CheckCampaignDates(_list, CampaignStatus.End);
                    _logger.LogInformation("============= Campaign Organization Status Info =============");
                    CheckCampaignOrganizationDates(_list);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
        #endregion

        #region Utils
        #region Campaigns
        private void CheckCampaignDates(List<Campaign> campaigns, CampaignStatus status)
        {
            if (campaigns.Any())
            {
                campaigns = GetCampaignsByStatus(campaigns, status);
                CheckDate(campaigns, status);
            }
        }
        private void CheckDate(List<Campaign> campaigns, CampaignStatus status)
        {
            if (campaigns.Any())
            {
                campaigns.ForEach(async c =>
                {
                    c.Status = (byte)status;
                    await _unitOfWork.Campaigns.UpdateAsync(c);
                    var result = _unitOfWork.Save();
                    if (result)
                        _logger.LogInformation($"[Change] : <{c.Id}> - {c.Name}");
                    else
                        _logger.LogError($"[Error] : <{c.Id}> - {c.Name}");
                });
            }
        }
        private List<Campaign> GetCampaignsByStatus(List<Campaign> campaigns, CampaignStatus status)
        {
            switch (status)
            {
                case CampaignStatus.NotStarted:
                    campaigns = campaigns.Where(c => DateTime.Compare((DateTime)c.StartDate, DateTime.Now) > 0 &&
                    DateTime.Compare((DateTime)c.EndDate, DateTime.Now) > 0).ToList();
                    break;
                case CampaignStatus.Start:
                    campaigns = campaigns.Where(c => DateTime.Compare((DateTime)c.StartDate, DateTime.Now) <= 0 &&
                    DateTime.Compare((DateTime)c.EndDate, DateTime.Now) > 0).ToList();
                    break;
                case CampaignStatus.End:
                    campaigns = campaigns.Where(c => DateTime.Compare((DateTime)c.StartDate, DateTime.Now) < 0 &&
                    DateTime.Compare((DateTime)c.EndDate, DateTime.Now) <= 0).ToList();
                    break;
            }
            var invalidStatus = new List<byte?>()
            {
                (byte) CampaignStatus.Postponed,
                (byte) CampaignStatus.Cancelled
            };
            return campaigns.Where(c => !c.Status.Equals((byte)status) && !invalidStatus.Contains(c.Status)).ToList();
        }
        #endregion

        #region  Campaign Organization
        private void CheckCampaignOrganizationDates(List<Campaign> campaigns)
        {
            var recurringCampaignOrganizations = new List<CampaignOrganization>();
            var invalidStatus = new List<byte?>()
            {
                (byte)CampaignStatus.Postponed,
                (byte)CampaignStatus.Cancelled
            };
            campaigns.Where(c => c.CampaignOrganizations.Any() &&
            !invalidStatus.Contains(c.Status)).ToList()
            .ForEach(c =>
            {
                if ((bool)c.IsRecurring)
                    recurringCampaignOrganizations.AddRange(c.CampaignOrganizations);
            });
            if (recurringCampaignOrganizations.Any())
                UpdateRecurringCampaign(recurringCampaignOrganizations);
        }
        private void UpdateRecurringCampaign(List<CampaignOrganization> campaignOrganizations)
        {
            var list = new List<Schedule>();
            campaignOrganizations.Select(co => co.Schedules).ToList().ForEach(coa => list.AddRange(coa));

            if (list.Any())
            {
                var NotStartedSchedule = GetScheduleByStatus(list, CampaignStatus.NotStarted);
                var StartSchedule = GetScheduleByStatus(list, CampaignStatus.Start);
                var EndSchedule = GetScheduleByStatus(list, CampaignStatus.End);

                if (NotStartedSchedule.Any())
                    UpdateCampaignOrganization(NotStartedSchedule, CampaignStatus.NotStarted, "[>> Not Start]");
                if (StartSchedule.Any())
                    UpdateCampaignOrganization(StartSchedule, CampaignStatus.Start, "[>> Start]");
                if (EndSchedule.Any())
                    UpdateCampaignOrganization(EndSchedule, CampaignStatus.End, "[>> End]");
            }
        }
        private List<Schedule> GetScheduleByStatus(List<Schedule> schedule, CampaignStatus status)
        {
            switch (status)
            {
                case CampaignStatus.NotStarted:
                    schedule = schedule.Where(cos => DateTime.Compare((DateTime)cos.StartDate, DateTime.Now) > 0 &&
                    DateTime.Compare((DateTime)cos.EndDate, DateTime.Now) > 0).ToList();
                    break;
                case CampaignStatus.Start:
                    schedule = schedule.Where(cos => DateTime.Compare((DateTime)cos.StartDate, DateTime.Now) <= 0 &&
                    DateTime.Compare((DateTime)cos.EndDate, DateTime.Now) > 0).ToList();
                    break;
                case CampaignStatus.End:
                    schedule = schedule.Where(cos => DateTime.Compare((DateTime)cos.StartDate, DateTime.Now) < 0 &&
                    DateTime.Compare((DateTime)cos.EndDate, DateTime.Now) <= 0).ToList();
                    break;
            }
            return schedule.Where(cos => !cos.Status.Equals((byte)status)).ToList();
        }
        private void UpdateCampaignOrganization(List<Schedule> list, CampaignStatus status, string Message)
        {
            _logger.LogInformation(Message);
            list.ForEach(async coa =>
            {
                coa.Status = (byte)status;
                await _unitOfWork.Schedules.UpdateAsync(coa);
                var result = _unitOfWork.Save();
                if (result)
                    _logger.LogInformation($"[Change] : <{coa.Id}> - {coa.Status}");
                else
                    _logger.LogError($"[Error] : <{coa.Id}> - {coa.Status}");
            });
        }
        #endregion
        #endregion
    }
}