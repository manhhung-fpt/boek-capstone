using AutoMapper;
using Boek.Infrastructure.Requests.Dashboards;
using Boek.Infrastructure.ViewModels.Dashboards;

namespace Boek.Infrastructure.Mappings
{
    public static class DashboardModule
    {
        public static void ConfigDashboardModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<TimeLineRequestModel, TimeLineViewModel>().ReverseMap();
        }
    }
}
