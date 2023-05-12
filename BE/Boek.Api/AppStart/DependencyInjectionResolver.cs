using Boek.Repository.Interfaces;
using Boek.Repository.Repositories;
using Boek.Service.Interfaces;
using Boek.Service.Interfaces.Mobile;
using Boek.Service.Services;
using Boek.Service.Services.Mobile;

namespace Boek.Api.AppStart
{
    public static class DependencyInjectionResolver
    {
        public static IServiceCollection
        ConfigureDependencyInjection(this IServiceCollection services)
        {
            //FireBase
            services.AddScoped<IFireBaseService, FireBaseService>();

            //Generic
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //User
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            //Campaign
            services.AddScoped<ICampaignRepository, CampaignRepository>();
            services.AddScoped<ICampaignService, CampaignService>();

            //Organization
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<IOrganizationService, OrganizationService>();

            //Campaign Organization
            services.AddScoped<ICampaignOrganizationRepository, CampaignOrganizationRepository>();
            services.AddScoped<ICampaignOrganizationService, CampaignOrganizationService>();

            //Participant
            services.AddScoped<IParticipantRepository, ParticipantRepository>();
            services.AddScoped<IParticipantService, ParticipantService>();

            //Book
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookService, BookService>();

            //Author
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IAuthorService, AuthorService>();

            //Publisher
            services.AddScoped<IPublisherRepository, PublisherRepository>();
            services.AddScoped<IPublisherService, PublisherService>();

            //Customer Organization
            services.AddScoped<ICustomerOrganizationRepository, CustomerOrganizationRepository>();
            services.AddScoped<ICustomerOrganizationService, CustomerOrganizationService>();

            //Order
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();

            //Order Detail
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();

            //Author Book
            services.AddScoped<IBookAuthorRepository, BookAuthorRepository>();

            //Unit of work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Commission
            services.AddScoped<ICampaignCommissionRepository, CampaignCommissionRepository>();
            services.AddScoped<ICampaignCommissionService, CampaignCommissionService>();

            //Customer Group
            services.AddScoped<ICustomerGroupRepository, CustomerGroupRepository>();
            services.AddScoped<ICustomerGroupService, CustomerGroupService>();

            //Genre
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IGenreService, GenreService>();

            //Group
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IGroupService, GroupService>();

            //Level
            services.AddScoped<ILevelRepository, LevelRepository>();
            services.AddScoped<ILevelService, LevelService>();

            //BookProduct
            services.AddScoped<IBookProductRepository, BookProductRepository>();
            services.AddScoped<IBookProductService, BookProductService>();

            //CampaignGroup
            services.AddScoped<ICampaignGroupRepository, CampaignGroupRepository>();
            services.AddScoped<ICampaignGroupService, CampaignGroupService>();

            //CampaignStaff
            services.AddScoped<ICampaignStaffRepository, CampaignStaffRepository>();
            services.AddScoped<ICampaignStaffService, CampaignStaffService>();

            //Customer
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            //Issuer
            services.AddScoped<IIssuerRepository, IssuerRepository>();

            //BookItem
            services.AddScoped<IBookItemRepository, BookItemRepository>();

            //Book Product Item
            services.AddScoped<IBookProductItemRepository, BookProductItemRepository>();

            //Campaign Level
            services.AddScoped<ICampaignLevelRepository, CampaignLevelRepository>();

            // Campaign Organization Schedule
            services.AddScoped<IScheduleRepository, ScheduleRepository>();

            //Dashboard
            services.AddScoped<IDashboardService, DashboardService>();

            //Dashboard
            services.AddScoped<IVerificationService, VerificationService>();

            //Notification
            services.AddScoped<INotificationService, NotificationService>();

            //Organization Member
            services.AddScoped<IOrganizationMemberRepository, OrganizationMemberRepository>();

            //Cache
            services.AddScoped<ICacheProvider, CacheProvider>();

            #region Mobile
            //Book Product
            services.AddScoped<IBookProductMobileService, BookProductMobileService>();

            //Campaign
            services.AddScoped<ICampaignMobileService, CampaignMobileService>();
            #endregion

            return services;
        }
    }
}
