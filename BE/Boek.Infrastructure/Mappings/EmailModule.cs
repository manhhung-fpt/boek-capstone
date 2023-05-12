using AutoMapper;
using Boek.Core.Constants;
using Boek.Infrastructure.Requests.Verifications;

namespace Boek.Infrastructure.Mappings
{
    public static class EmailModule
    {
        public static void ConfigEmailModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<EmailRequestModel, EmailDetail>()
            .ForMember(src => src.SubTitle, o => o.Ignore())
            .ForMember(src => src.IsValid, o => o.Ignore())
            .ReverseMap();
        }
    }
}