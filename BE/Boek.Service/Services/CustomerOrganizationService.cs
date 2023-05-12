using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boek.Core.Constants;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.CustomerOrganizations;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.CustomerOrganizations;
using Boek.Infrastructure.ViewModels.Organizations;
using Boek.Repository.Interfaces;
using Boek.Service.Commons;
using Boek.Service.Interfaces;
using Boek.Service.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Boek.Service.Services
{
    public class CustomerOrganizationService : ICustomerOrganizationService
    {
        #region Fields and constructor
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomerOrganizationService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Gets
        public CustomerOrganizationViewModel GetCustomerOrganizationById(int id)
        {
            var _customerOrganization =
                _unitOfWork.CustomerOrganizations.Get(id);
            if (_customerOrganization == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.CUSTOMER_ORGANIZATION_ID.ToLower()
                });
            return _mapper.Map<CustomerOrganizationViewModel>(_customerOrganization);
        }

        public BaseResponsePagingModel<CustomerOrganizationViewModel> GetCustomerOrganizations(CustomerOrganizationRequestModel filter, PagingModel paging)
        {
            var _filter = new CustomerOrganizationViewModel();
            _mapper.Map(filter, _filter);
            var result = _unitOfWork.CustomerOrganizations.Get()
                    .ProjectTo<CustomerOrganizationViewModel>(_mapper.ConfigurationProvider)
                    .DynamicFilter(_filter)
                    .PagingQueryable(paging.Page, paging.Size, PageConstant.LimitPaging, PageConstant.LimitPaging);
            return new BaseResponsePagingModel<CustomerOrganizationViewModel>()
            {
                Metadata =
                    new PagingMetadata()
                    {
                        Page = paging.Page,
                        Size = paging.Size,
                        Total = result.Item1
                    },
                Data = result.Item2.ToList()
            };
        }

        public OwnedCustomerOrganizationViewModel GetCustomerOrganizationsByCustomer()
        {
            var user = ServiceUtils.GetUserInfo(_httpContextAccessor);
            var result = new OwnedCustomerOrganizationViewModel();
            var _customerOrganizations = _unitOfWork.CustomerOrganizations
                    .Get(co => co.CustomerId.Equals(user))
                    .Include(co => co.Customer)
                    .Include(co => co.Organization)
                    .ThenInclude(o => o.CustomerOrganizations)
                    .ToList();
            if (_customerOrganizations.Any())
            {
                var _firstItem = _customerOrganizations.First();
                _mapper.Map(_firstItem, result);
                result.Organizations = new List<CustomerOrganizationsViewModel>();
                _customerOrganizations
                    .ForEach(co =>
                    {
                        var _organization =
                            _mapper.Map<BasicOrganizationViewModel>(co.Organization);
                        result.Organizations.Add(new CustomerOrganizationsViewModel()
                        {
                            Organization = _organization,
                            Total = co.Organization.CustomerOrganizations.Count()
                        });
                    });
            }
            else
                result = null;
            return result;
        }
        #endregion

        #region CUD
        public CustomerOrganizationViewModel CreateCustomerOrganization(CreateCustomerOrganizationRequestModel createCustomerOrganization)
        {
            var UserId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            var _user =
                _unitOfWork.Users.Get(UserId);
            var _organization = _unitOfWork.Organizations
                    .Get(createCustomerOrganization.OrganizationId);
            var _customerOrganization = _unitOfWork.CustomerOrganizations
                    .Get(co =>
                        co.CustomerId.Equals(UserId) &&
                        co.OrganizationId.Equals(createCustomerOrganization.OrganizationId))
                    .SingleOrDefault();
            if (_user == null)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.USER_ID
                });
            }
            if (_organization == null)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.ORGANIZATION_ID
                });
            }
            if (_customerOrganization != null)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[] {
                        ErrorMessageConstants.CUSTOMER_ORGANIZATION.ToLower(),
                        MessageConstants.MESSAGE_EXISTED
                    });
            }
            _customerOrganization =
                _mapper.Map<CustomerOrganization>(createCustomerOrganization);
            _customerOrganization.CustomerId = UserId;
            _unitOfWork.CustomerOrganizations.Create(_customerOrganization);
            if (!_unitOfWork.Save())
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[] {
                        ErrorMessageConstants.INSERT,
                        ErrorMessageConstants.CUSTOMER_ORGANIZATION.ToLower(),
                        MessageConstants.MESSAGE_FAILED
                    });
            }
            _customerOrganization = _unitOfWork.CustomerOrganizations.Get(co =>
                        co.CustomerId.Equals(UserId) &&
                        co.OrganizationId
                            .Equals(createCustomerOrganization.OrganizationId))
                    .SingleOrDefault();
            return _mapper.Map<CustomerOrganizationViewModel>(_customerOrganization);
        }

        public CustomerOrganizationViewModel DeleteCustomerOrganizationById(int OrganizationId)
        {
            var _user = ServiceUtils.GetUserInfo(_httpContextAccessor);
            var _customerOrganization =
                _unitOfWork.CustomerOrganizations.Get(cg =>
                cg.OrganizationId.Equals(OrganizationId) &&
                cg.CustomerId.Equals(_user))
                .SingleOrDefault();
            if (_customerOrganization == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.CUSTOMER_ORGANIZATION_ID
                });
            _unitOfWork.CustomerOrganizations.Delete(_customerOrganization);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[] {
                        ErrorMessageConstants.DELETE,
                        ErrorMessageConstants.CUSTOMER_ORGANIZATION.ToLower(),
                        MessageConstants.MESSAGE_FAILED
                    });
            return _mapper
                .Map<CustomerOrganizationViewModel>(_customerOrganization);
        }
        #endregion
    }
}
