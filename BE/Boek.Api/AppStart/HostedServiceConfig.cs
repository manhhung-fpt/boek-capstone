using Boek.Service.BackgroundServices;
using Boek.Service.Services;

namespace Boek.Api.AppStart
{
    public static class HostedServiceConfig
    {
        public static IServiceCollection HostedServices(this IServiceCollection services)
        {
            services.AddHostedService<CampaignStatusService>();
            services.AddHostedService<BookProductStatusService>();
            services.AddHostedService<CustomerLevelService>();
            services.AddHostedService<ParticipantStatusService>();
            services.AddHostedService<OrderStatusService>();
            //services.AddHostedService<CachingService>();
            return services;
        }
    }
}