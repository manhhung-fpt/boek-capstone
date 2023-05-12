using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boek.Core.Constants;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.CustomerGroups;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.CustomerGroups;
using Boek.Infrastructure.ViewModels.Groups;
using Boek.Repository.Interfaces;
using Boek.Service.Commons;
using Boek.Service.Interfaces;
using Boek.Service.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Boek.Service.Services
{
    public class CustomerGroupService : ICustomerGroupService
    {
        #region Fields and constructor
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomerGroupService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Gets
        public CustomerGroupViewModel GetCustomerGroupById(int id)
        {
            var _customerGroup =
                _unitOfWork.CustomerGroups.Get(id);
            if (_customerGroup == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.CUSTOMER_GROUP_ID.ToLower()
                });
            return GetResponse(_customerGroup);
        }

        public BaseResponsePagingModel<CustomerGroupViewModel> GetCustomerGroups(CustomerGroupRequestModel filter, PagingModel paging)
        {
            var _filter = new CustomerGroupViewModel();
            _mapper.Map(filter, _filter);
            var result = _unitOfWork.CustomerGroups.Get()
                    .ProjectTo<CustomerGroupViewModel>(_mapper.ConfigurationProvider)
                    .DynamicFilter(_filter)
                    .PagingQueryable(paging.Page, paging.Size, PageConstant.LimitPaging, PageConstant.LimitPaging);
            var list = result.Item2.ToList();
            if (list.Any())
                list.ForEach(cg => cg = ServiceUtils.GetResponseDetail(cg));
            return new BaseResponsePagingModel<CustomerGroupViewModel>()
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

        public OwnedCustomerGroupViewModel GetCustomerGroupsByCustomer()
        {
            var user = ServiceUtils.GetUserInfo(_httpContextAccessor);
            var result = new OwnedCustomerGroupViewModel();
            var _customerGroups = _unitOfWork.CustomerGroups
                    .Get(co => co.CustomerId.Equals(user))
                    .Include(co => co.Customer)
                    .Include(co => co.Group)
                    .ThenInclude(g => g.CustomerGroups)
                    .ToList();
            if (_customerGroups.Any())
            {
                var _firstItem = _customerGroups.First();
                _mapper.Map(_firstItem, result);
                result.Groups = new List<CustomerGroupsViewModel>();
                _customerGroups
                    .ForEach(co =>
                    {
                        var _group = _mapper.Map<BasicGroupViewModel>(co.Group);
                        result.Groups.Add(new CustomerGroupsViewModel()
                        {
                            Group = _group,
                            Total = co.Group.CustomerGroups.Count()
                        });
                    });
                ServiceUtils.GetResponseDetail(result);
            }
            else
                result = null;
            return result;
        }
        #endregion

        #region CD
        public CustomerGroupViewModel CreateCustomerGroup(CreateCustomerGroupRequestModel createCustomerGroup)
        {
            var UserId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            var _user = _unitOfWork.Users.Get(UserId);
            var _group = _unitOfWork.Groups.Get(createCustomerGroup.GroupId);
            var _customerGroup = _unitOfWork.CustomerGroups.Get(cg =>
            cg.CustomerId.Equals(UserId) &&
            cg.GroupId.Equals(createCustomerGroup.GroupId))
            .SingleOrDefault();
            if (_user == null)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.USER_ID
                });
            }
            if (_group == null)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.GROUP_ID
                });
            }
            if (_customerGroup != null)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.CUSTOMER_GROUP.ToLower(),
                    MessageConstants.MESSAGE_EXISTED
                });
            }
            _customerGroup = _mapper.Map<CustomerGroup>(createCustomerGroup);
            _customerGroup.CustomerId = UserId;
            _unitOfWork.CustomerGroups.Create(_customerGroup);
            if (!_unitOfWork.Save())
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.INSERT,
                    ErrorMessageConstants.CUSTOMER_GROUP.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            }
            _customerGroup = _unitOfWork.CustomerGroups.Get(cg =>
            cg.CustomerId.Equals(UserId) &&
            cg.GroupId.Equals(createCustomerGroup.GroupId))
            .SingleOrDefault();
            return GetResponse(_customerGroup);
        }

        public CustomerGroupViewModel DeleteCustomerGroupById(int GroupId)
        {
            var _user = ServiceUtils.GetUserInfo(_httpContextAccessor);
            var _customerGroup = _unitOfWork.CustomerGroups.Get(cg =>
                cg.GroupId.Equals(GroupId) &&
                cg.CustomerId.Equals(_user))
                .SingleOrDefault();
            if (_customerGroup == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.CUSTOMER_GROUP_ID
                });
            _unitOfWork.CustomerGroups.Delete(_customerGroup);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[] {
                        ErrorMessageConstants.DELETE,
                        ErrorMessageConstants.CUSTOMER_GROUP.ToLower(),
                        MessageConstants.MESSAGE_FAILED
                    });
            return _mapper
                .Map<CustomerGroupViewModel>(_customerGroup);
        }
        #endregion

        #region Utils

        private CustomerGroupViewModel GetResponse(CustomerGroup customerGroup)
        {
            var _response = _unitOfWork.CustomerGroups
                .Get(cg => cg.Id.Equals(customerGroup.Id))
                .ProjectTo<CustomerGroupViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefault();
            return _response != null ? ServiceUtils.GetResponseDetail(_response) : new CustomerGroupViewModel();
        }
        #endregion
    }
}
