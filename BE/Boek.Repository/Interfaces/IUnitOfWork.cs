using Boek.Core.Entities;
using System;

namespace Boek.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        #region 
        ICacheProvider CacheProvider { get; }
        #endregion
        #region Books
        IBookRepository Books { get; }
        IBookAuthorRepository BookAuthors { get; }
        IAuthorRepository Authors { get; }
        IGenreRepository Genres { get; }
        IPublisherRepository Publishers { get; }
        IBookItemRepository BookItems { get; }
        #endregion

        #region Users
        IUserRepository Users { get; }
        ICustomerRepository Customers { get; }
        IIssuerRepository Issuers { get; }
        #endregion

        #region User details
        ICustomerGroupRepository CustomerGroups { get; }
        ICustomerOrganizationRepository CustomerOrganizations { get; }
        #endregion

        #region Campaigns
        ICampaignOrganizationRepository CampaignOrganizations { get; }
        IScheduleRepository Schedules { get; }
        ICampaignRepository Campaigns { get; }
        ICampaignCommissionRepository CampaignCommissions { get; }
        ICampaignLevelRepository CampaignLevels { get; }
        IGroupRepository Groups { get; }
        ILevelRepository Levels { get; }
        IOrderDetailRepository OrderDetails { get; }
        IOrderRepository Orders { get; }
        IOrganizationRepository Organizations { get; }
        IOrganizationMemberRepository OrganizationMembers { get; }
        IParticipantRepository Participants { get; }
        IBookProductRepository BookProducts { get; }
        IBookProductItemRepository BookProductItems { get; }
        ICampaignGroupRepository CampaignGroups { get; }
        ICampaignStaffRepository CampaignStaffs { get; }
        #endregion
        bool Save();
        Task SaveAsync();
    }
}
