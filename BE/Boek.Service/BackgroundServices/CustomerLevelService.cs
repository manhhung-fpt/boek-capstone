using Boek.Core.Entities;
using Boek.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Boek.Service.BackgroundServices
{
    public class CustomerLevelService : BackgroundService
    {
        #region Fields and constructor
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CustomerLevelService> _logger;
        private Timer _timer = null;

        public CustomerLevelService(IServiceProvider services, ILogger<CustomerLevelService> logger)
        {
            var scope = services.CreateScope();
            _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            _logger = logger;
        }
        #endregion

        #region Background service
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(2));
            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            try
            {
                var levels = await _unitOfWork.Levels
                .Get(l => l.Status == true)
                .OrderBy(l => l.ConditionalPoint)
                .ToListAsync();
                var customers = await _unitOfWork.Customers.Get().ToListAsync();
                if (levels.Any())
                {
                    var duplicates = levels.GroupBy(l => l.ConditionalPoint).ToList();
                    var flag = false;
                    duplicates.ForEach(d =>
                    {
                        if (d.Count() > 2)
                        {
                            flag = true;
                            var values = d.Select(l => l).ToList();
                            _logger.LogError($"[Duplicated level]");
                            values.ForEach(v =>
                            {
                                _logger.LogError($"<{v.Id}> - {v.Name} - {v.ConditionalPoint}");
                            });
                        }
                    });
                    if (!flag && customers.Any())
                    {
                        _logger.LogInformation("============= Customer Level Info =============");
                        var ConditionalPoints = levels.GroupBy(l => l.ConditionalPoint).ToList();
                        for (int i = 0; i < ConditionalPoints.Count(); i++)
                        {
                            int count = i;
                            count += 1;
                            var level = ConditionalPoints[i].Select(l => l).First();
                            var LevelMinPoint = ConditionalPoints[i].Key;
                            var LevelMaxPoint = count != ConditionalPoints.Count() ? ConditionalPoints[count].Key : null;
                            UpdateCustomerLevels(customers, level, LevelMinPoint, LevelMaxPoint, i == 0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
        #endregion
        #region Utils
        private void UpdateCustomerLevels(List<Customer> customers, Level level, int? LevelMinPoint, int? LevelMaxPoint = null, bool isFirst = false)
        {
            _logger.LogInformation($"[>> {level.Name}]");
            if (isFirst)
            {
                customers.ForEach(async c =>
                    {
                        if (c.Point <= LevelMinPoint && !c.LevelId.Equals(level.Id))
                        {
                            c.LevelId = level.Id;
                            await _unitOfWork.Customers.UpdateAsync(c);
                            if (_unitOfWork.Save())
                                _logger.LogInformation($"[Change] : <{c.Id}> - LevelId: {c.LevelId}");
                            else
                                _logger.LogError($"[Error] : <{c.Id}> - LevelId: {c.LevelId}");
                        }
                    });
            }

            if (LevelMaxPoint != null)
            {
                customers.ForEach(async c =>
                {
                    if (c.Point >= LevelMinPoint && c.Point < LevelMaxPoint && !c.LevelId.Equals(level.Id))
                    {
                        c.LevelId = level.Id;
                        await _unitOfWork.Customers.UpdateAsync(c);
                        if (_unitOfWork.Save())
                            _logger.LogInformation($"[Change] : <{c.Id}> - LevelId: {c.LevelId}");
                        else
                            _logger.LogError($"[Error] : <{c.Id}> - LevelId: {c.LevelId}");
                    }
                });
            }
            else
            {
                customers.ForEach(async c =>
                {
                    if (c.Point >= LevelMinPoint && !c.LevelId.Equals(level.Id))
                    {
                        c.LevelId = level.Id;
                        await _unitOfWork.Customers.UpdateAsync(c);
                        if (_unitOfWork.Save())
                            _logger.LogInformation($"[Change] : <{c.Id}> - LevelId: {c.LevelId}");
                        else
                            _logger.LogError($"[Error] : <{c.Id}> - LevelId: {c.LevelId}");
                    }
                });
            }
        }
        #endregion
    }
}