using Boek.Core.Constants;
using Boek.Infrastructure.Requests.Verifications;
using Boek.Repository.Interfaces;
using Boek.Service.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Boek.Service.BackgroundServices
{
    public class CachingService : BackgroundService
    {
        #region Fields and constructor
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CachingService> _logger;
        private Timer _timer = null;
        #endregion

        #region Background service
        public CachingService(IServiceProvider services, ILogger<CachingService> logger)
        {
            var scope = services.CreateScope();
            _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            try
            {
                var cache = RedisUtil.GetDataFromCache<List<RedisOtpModel>>(_unitOfWork.CacheProvider, MessageConstants.REDIS_OTP_VERIFICATION);
                if (cache != null)
                {
                    if (cache.Any())
                    {
                        _logger.LogInformation("============= Log redis data =============");
                        CheckExpiredRedis(cache);
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

        public void CheckExpiredRedis(IEnumerable<RedisOtpModel> cache)
        {
            var now = DateTimeOffset.Now;
            var validData = cache.Where(data => data.Expire > now).ToList();
            var expiredData = cache.Where(data => data.Expire <= now);
            if (expiredData.Any())
            {
                _logger.LogInformation("============= Expired redis data =============");
                expiredData.ToList().ForEach(data =>
                {
                    _logger.LogInformation(data.ToString());
                });
                _unitOfWork.CacheProvider.SetValue(MessageConstants.REDIS_OTP_VERIFICATION, validData);
            }
        }
        #endregion
    }
}