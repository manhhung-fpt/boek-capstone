using AutoMapper;
using Boek.Infrastructure.Mappings;

namespace Boek.Api.AppStart
{
    public static class AutoMapperConfig
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(mc =>
            {
                mc.ConfigBookAuthorModule();
                mc.ConfigBookItemModule();
                mc.ConfigAuthorModule();
                mc.ConfigBookModule();
                mc.ConfigGenreModule();
                mc.ConfigPublisherModule();
                mc.ConfigUserModule();
                mc.ConfigIssuerModule();
                mc.ConfigCustomerModule();
                mc.ConfigOrganizationModule();
                mc.ConfigCustomerOrganizationModule();
                mc.ConfigGroupModule();
                mc.ConfigCustomerGroupModule();
                mc.ConfigCampaignModule();
                mc.ConfigCampaignCommissionModule();
                mc.ConfigLevelModule();
                mc.ConfigCampaignStaffModule();
                mc.ConfigCampaignOrganizationModule();
                mc.ConfigCampaignGroupModule();
                mc.ConfigParticipantModule();
                mc.ConfigBookProductModule();
                mc.ConfigBookProductItemModule();
                mc.ConfigCampaignLevelModule();
                mc.OrderMappingConfigure();
                mc.ConfigOrderDetailModule();
                mc.ConfigScheduleModule();
                mc.ConfigAddressModule();
                mc.ConfigDashboardModule();
                mc.ConfigEmailModule();
                mc.ConfigOrganizationMemberModule();
            });
            IMapper mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
