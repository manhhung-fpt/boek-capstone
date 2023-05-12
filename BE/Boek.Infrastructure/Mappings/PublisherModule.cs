using AutoMapper;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.Publishers;
using Boek.Infrastructure.ViewModels.Publishers;

namespace Boek.Infrastructure.Mappings
{
    public static class PublisherModule
    {
        public static void ConfigPublisherModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Publisher, PublisherViewModel>().ReverseMap();
            mc.CreateMap<Publisher, CreatePublisherRequestModel>().ReverseMap();
            mc.CreateMap<Publisher, UpdatePublisherRequestModel>().ReverseMap()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcPublisher) => srcPublisher != null));
        }
    }
}
