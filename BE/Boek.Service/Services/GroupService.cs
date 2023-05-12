using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boek.Core.Constants;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.Groups;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.Campaigns;
using Boek.Infrastructure.ViewModels.Groups;
using Boek.Infrastructure.ViewModels.Users.Customers;
using Boek.Repository.Interfaces;
using Boek.Service.Commons;
using Boek.Service.Interfaces;
using Boek.Service.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Boek.Service.Services
{
    public class GroupService : IGroupService
    {
        #region Field(s) and constructor
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GroupService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Gets
        public BasicGroupViewModel GetGroupById(int id)
        {
            var _group = _unitOfWork.Groups.Get(id);
            if (_group == null)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.GROUP_ID
                });
            }
            return ServiceUtils.GetResponseDetail(_mapper.Map<BasicGroupViewModel>(_group));
        }

        public BaseResponsePagingModel<GroupViewModel> GetGroups(GroupRequestModel filter, PagingModel paging, bool WithCustomers = false, bool WithCampaigns = false)
        {
            var _filter = _mapper.Map<GroupViewModel>(filter);
            var result = _unitOfWork.Groups.Get()
                    .ProjectTo<GroupViewModel>(_mapper.ConfigurationProvider)
                    .DynamicFilter(_filter)
                    .PagingQueryable(paging.Page, paging.Size, PageConstant.LimitPaging, PageConstant.LimitPaging);
            var list = result.Item2.ToList();
            if (WithCustomers)
                GetUserViewModels(ref list);
            if (WithCampaigns)
                GetCampaignModels(ref list);
            list.ForEach(o => o = ServiceUtils.GetResponseDetail(o));
            return new BaseResponsePagingModel<GroupViewModel>()
            {
                Metadata =
                    new PagingMetadata()
                    {
                        Page = paging.Page,
                        Size = paging.Size,
                        Total = result.Item1
                    },
                Data = list
            };
        }
        #endregion

        #region CUD
        public BasicGroupViewModel CreateGroup(CreateGroupRequestModel createGroup)
        {
            var _group = _mapper.Map<Group>(createGroup);
            if (CheckDuplicatedGroupName(_group.Name) != null)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    MessageConstants.MESSAGE_DUPLICATED_INFO,
                    ErrorMessageConstants.GROUP_NAME
                });
            }
            _group.Status = true;
            _unitOfWork.Groups.Create(_group);
            if (!_unitOfWork.Save())
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.INSERT,
                    ErrorMessageConstants.GROUP.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            }
            _group = _unitOfWork.Groups
                    .Get(o => o.Name.Equals(_group.Name))
                    .SingleOrDefault();
            return ServiceUtils.GetResponseDetail(_mapper.Map<BasicGroupViewModel>(_group));
        }

        public BasicGroupViewModel UpdateGroup(UpdateGroupRequestModel updateGroup)
        {
            var _group =
                _unitOfWork.Groups.Get(g => g.Id.Equals(updateGroup.Id))
                .Include(g => g.CustomerGroups)
                .Include(g => g.CampaignGroups)
                .SingleOrDefault();
            if (_group == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.GROUP_ID
                });
            var _name = CheckDuplicatedGroupName(updateGroup.Name);
            if (_name != null && !_name.Id.Equals(updateGroup.Id))
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    MessageConstants.MESSAGE_DUPLICATED_INFO,
                    ErrorMessageConstants.GROUP_NAME
                });
            if (!updateGroup.Status.Equals(_group.Status) && updateGroup.Status == false &&
                (_group.CustomerGroups.Any() || _group.CampaignGroups.Any()))
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.UPDATE,
                    ErrorMessageConstants.GROUP.ToLower(),
                    MessageConstants.MESSAGE_FAILED,
                    MessageConstants.MESSAGE_LINKED_INFO
                });
            _mapper.Map(updateGroup, _group);
            _unitOfWork.Groups.Update(_group);
            if (!_unitOfWork.Save())
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.UPDATE,
                    ErrorMessageConstants.GROUP.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            }
            _group = _unitOfWork.Groups.Get(updateGroup.Id);
            return ServiceUtils.GetResponseDetail(_mapper.Map<BasicGroupViewModel>(_group));
        }
        #endregion

        #region Utils

        private void GetUserViewModels(ref List<GroupViewModel> list)
        {
            list
                .ForEach(o =>
                {
                    var customers =
                        _mapper.Map<List<CustomerUserViewModel>>(_unitOfWork
                                .CustomerGroups
                                .GetCustomers(o.Id));
                    customers
                        .ForEach(c => c.User = ServiceUtils.GetResponseDetail(c.User));
                    o.Customers = customers;
                });
        }
        private void GetCampaignModels(ref List<GroupViewModel> list)
        {
            list
                .ForEach(o =>
                {
                    var campaigns =
                        _mapper.Map<List<BasicCampaignViewModel>>(_unitOfWork
                                .CampaignGroups
                                .GetCampaigns(o.Id));
                    campaigns
                        .ForEach(c => c = ServiceUtils.GetCampaignDetail(c));
                    o.Campaigns = campaigns;
                });
        }

        public Group CheckDuplicatedGroupName(string name) =>
            _unitOfWork.Groups.CheckDuplicatedGroupName(name);
        #endregion
    }
}
