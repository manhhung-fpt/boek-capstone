using AutoMapper;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.Authors;
using Boek.Infrastructure.ViewModels.Authors;

namespace Boek.Infrastructure.Mappings
{
    public static class AuthorModule
    {
        public static void ConfigAuthorModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<AuthorBooksViewModel, AuthorViewModel>().ReverseMap();
            mc.CreateMap<Author, AuthorViewModel>().ReverseMap();
            mc.CreateMap<AuthorBooksViewModel, Author>().ReverseMap();
            mc.CreateMap<Author, CreateAuthorRequestModel>().ReverseMap();
            mc.CreateMap<Author, CreateAuthorByIssuerRequestModel>().ReverseMap();
            mc.CreateMap<Author, UpdateAuthorRequestModel>().ReverseMap()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcAuthor) => srcAuthor != null));
        }
    }
}
