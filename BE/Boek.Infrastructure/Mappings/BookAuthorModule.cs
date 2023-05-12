using AutoMapper;
using Boek.Core.Entities;
using Boek.Infrastructure.ViewModels.BookAuthors;

namespace Boek.Infrastructure.Mappings
{
    public static class BookAuthorModule
    {
        public static void ConfigBookAuthorModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<BookAuthor, BookAuthorViewModel>().ReverseMap();
        }
    }
}
