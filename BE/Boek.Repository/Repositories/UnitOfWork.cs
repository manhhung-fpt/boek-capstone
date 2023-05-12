using Boek.Core.Data;
using Boek.Repository.Interfaces;

namespace Boek.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields
        private readonly BoekCapstoneContext _context;
        private readonly IBookAuthorRepository _bookAuthorRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;
        private readonly ICampaignOrganizationRepository _campaignOrganizationRepository;
        private readonly ICampaignRepository _campaignRepository;
        private readonly ICampaignCommissionRepository _campaignCommissionRepository;
        private readonly ICustomerGroupRepository _customerGroupRepository;
        private readonly ICustomerOrganizationRepository _customerOrganizationRepository;
        private readonly IBookProductItemRepository _bookProductItemRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly ILevelRepository _levelRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IParticipantRepository _participantRepository;
        private readonly IPublisherRepository _publisherRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBookProductRepository _bookProductRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly ICampaignGroupRepository _campaignGroupRepository;
        private readonly ICampaignStaffRepository _campaignStaffRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IIssuerRepository _issuerRepository;
        private readonly IBookItemRepository _bookItemRepository;
        private readonly ICampaignLevelRepository _campaignLevelRepository;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IOrganizationMemberRepository _organizationMemberRepository;
        private readonly ICacheProvider _cacheProvider;
        private bool _disposed = false;
        #endregion

        #region Constructor
        public UnitOfWork(BoekCapstoneContext context,
            IBookAuthorRepository bookAuthorRepository,
            IAuthorRepository authorRepository,
            IBookRepository bookRepository,
            ICampaignOrganizationRepository campaignOrganizationRepository,
            ICampaignRepository campaignRepository,
            ICampaignCommissionRepository campaignCommissionRepository,
            ICustomerGroupRepository customerGroupRepository,
            ICustomerOrganizationRepository customerOrganizationRepository,
            IBookProductItemRepository bookProductItemRepository,
            IGroupRepository groupRepository,
            ILevelRepository levelRepository,
            IOrderDetailRepository orderDetailRepository,
            IOrderRepository orderRepository,
            IOrganizationRepository organizationRepository,
            IParticipantRepository participantRepository,
            IPublisherRepository publisherRepository,
            IUserRepository userRepository,
            IBookProductRepository bookProductRepository,
            IGenreRepository genreRepository,
            ICampaignGroupRepository campaignGroupRepository,
            ICampaignStaffRepository campaignStaffRepository,
            ICustomerRepository customerRepository,
            IIssuerRepository issuerRepository,
            IBookItemRepository bookItemRepository,
            ICampaignLevelRepository campaignLevelRepository,
            IScheduleRepository scheduleRepository,
            IOrganizationMemberRepository organizationMemberRepository,
            ICacheProvider cacheProvider)
        {
            _context = context;
            _bookAuthorRepository = bookAuthorRepository;
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
            _campaignRepository = campaignRepository;
            _campaignOrganizationRepository = campaignOrganizationRepository;
            _campaignCommissionRepository = campaignCommissionRepository;
            _customerGroupRepository = customerGroupRepository;
            _customerOrganizationRepository = customerOrganizationRepository;
            _bookProductItemRepository = bookProductItemRepository;
            _groupRepository = groupRepository;
            _levelRepository = levelRepository;
            _orderDetailRepository = orderDetailRepository;
            _orderRepository = orderRepository;
            _organizationRepository = organizationRepository;
            _participantRepository = participantRepository;
            _publisherRepository = publisherRepository;
            _userRepository = userRepository;
            _bookProductRepository = bookProductRepository;
            _genreRepository = genreRepository;
            _campaignGroupRepository = campaignGroupRepository;
            _campaignStaffRepository = campaignStaffRepository;
            _customerRepository = customerRepository;
            _issuerRepository = issuerRepository;
            _bookItemRepository = bookItemRepository;
            _campaignLevelRepository = campaignLevelRepository;
            _scheduleRepository = scheduleRepository;
            _organizationMemberRepository = organizationMemberRepository;
            _cacheProvider = cacheProvider;
        }
        #endregion

        #region Methods
        public IBookAuthorRepository BookAuthors => _bookAuthorRepository;

        public IAuthorRepository Authors => _authorRepository;

        public IBookRepository Books => _bookRepository;

        public ICampaignOrganizationRepository CampaignOrganizations => _campaignOrganizationRepository;

        public ICampaignRepository Campaigns => _campaignRepository;

        public ICampaignCommissionRepository CampaignCommissions => _campaignCommissionRepository;

        public ICustomerGroupRepository CustomerGroups => _customerGroupRepository;

        public ICustomerOrganizationRepository CustomerOrganizations => _customerOrganizationRepository;

        public IBookProductItemRepository BookProductItems => _bookProductItemRepository;

        public IGroupRepository Groups => _groupRepository;

        public ILevelRepository Levels => _levelRepository;

        public IOrderDetailRepository OrderDetails => _orderDetailRepository;

        public IOrderRepository Orders => _orderRepository;

        public IOrganizationRepository Organizations => _organizationRepository;

        public IParticipantRepository Participants => _participantRepository;

        public IPublisherRepository Publishers => _publisherRepository;

        public IUserRepository Users => _userRepository;

        public IGenreRepository Genres => _genreRepository;

        public ICustomerRepository Customers => _customerRepository;

        public IIssuerRepository Issuers => _issuerRepository;

        public IBookProductRepository BookProducts => _bookProductRepository;

        public ICampaignGroupRepository CampaignGroups => _campaignGroupRepository;

        public ICampaignStaffRepository CampaignStaffs => _campaignStaffRepository;

        public IBookItemRepository BookItems => _bookItemRepository;

        public ICampaignLevelRepository CampaignLevels => _campaignLevelRepository;

        public IScheduleRepository Schedules => _scheduleRepository;

        public IOrganizationMemberRepository OrganizationMembers => _organizationMemberRepository;

        public ICacheProvider CacheProvider => _cacheProvider;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }


        public bool Save()
        {
            var result = _context.SaveChanges();
            return result > 0;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
