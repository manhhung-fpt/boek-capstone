using AutoMapper;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.Groups;
using Boek.Infrastructure.ViewModels.Groups;

namespace Boek.Infrastructure.Mappings
{
    public static class GroupModule
    {
        public static void ConfigGroupModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Group, BasicGroupViewModel>().ReverseMap();
            mc.CreateMap<Group, GroupViewModel>().ReverseMap();
            mc.CreateMap<GroupRequestModel, GroupViewModel>().ReverseMap();
            mc.CreateMap<Group, CreateGroupRequestModel>().ReverseMap();
            mc.CreateMap<Group, UpdateGroupRequestModel>().ReverseMap();
            mc.CreateMap<Group, GroupRequestModel>().ReverseMap();
        }
    }
}
