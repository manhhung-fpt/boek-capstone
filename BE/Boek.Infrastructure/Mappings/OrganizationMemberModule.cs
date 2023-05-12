using AutoMapper;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.OrganizationMembers;
using Boek.Infrastructure.ViewModels.OrganizationMembers;

namespace Boek.Infrastructure.Mappings
{
    public static class OrganizationMemberModule
    {
        public static void ConfigOrganizationMemberModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<OrganizationMember, OrganizationMemberViewModel>()
            .ReverseMap();
            mc.CreateMap<OrganizationMember, CreateOrganizationMemberRequestModel>()
            .ReverseMap();
            mc.CreateMap<OrganizationMember, UpdateOrganizationMemberRequestModel>()
            .ReverseMap();
        }
    }
}