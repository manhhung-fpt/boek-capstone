using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boek.Core.Constants;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.CampaignGroups;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.CampaignGroups;
using Boek.Repository.Interfaces;
using Boek.Service.Commons;
using Boek.Service.Interfaces;
using Boek.Service.Utils;

namespace Boek.Service.Services
{
    public class CampaignGroupService : ICampaignGroupService
    {
        #region Fields and constructor
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public CampaignGroupService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Gets
        public CampaignGroupViewModel GetCampaignGroupById(int id)
        {
            var _campaignGroup =
                _unitOfWork.CampaignGroups.Get(id);
            if (_campaignGroup == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.CAMPAIGN_GROUP_ID
                });
            return GetResponse(_campaignGroup);
        }

        public BaseResponsePagingModel<CampaignGroupViewModel> GetCampaignGroups(CampaignGroupRequestModel filter, PagingModel paging)
        {
            var _filter = new CampaignGroupViewModel();
            _mapper.Map(filter, _filter);
            var result = _unitOfWork.CampaignGroups.Get()
            .ProjectTo<CampaignGroupViewModel>(_mapper.ConfigurationProvider)
            .DynamicFilter(_filter)
            .PagingQueryable(paging.Page, paging.Size, PageConstant.LimitPaging, PageConstant.LimitPaging);
            var list = result.Item2.ToList();
            if (list.Any())
                list.ForEach(l => l = GetResponseDetail(l));
            return new BaseResponsePagingModel<CampaignGroupViewModel>()
            {
                Metadata = new PagingMetadata()
                {
                    Page = paging.Page,
                    Size = paging.Size,
                    Total = result.Item1
                },
                Data = result.Item2.ToList()
            };
        }
        #endregion

        #region CD
        public CampaignGroupViewModel CreateCampaignGroup(CreateCampaignGroupRequestModel createCampaignGroup)
        {
            var _campaign = _unitOfWork.Campaigns.Get(createCampaignGroup.CampaignId);
            var _group = _unitOfWork.Groups.Get(createCampaignGroup.GroupId);
            var _campaignGroup = _unitOfWork.CampaignGroups.Get(co => co.CampaignId.Equals(createCampaignGroup.CampaignId) &&
            co.GroupId.Equals(createCampaignGroup.GroupId)).SingleOrDefault();
            if (_campaign == null)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.CAMPAIGN_ID
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
            if (_campaignGroup != null)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.CAMPAIGN_GROUP.ToLower(),
                    MessageConstants.MESSAGE_EXISTED
                });
            }
            _campaignGroup = _mapper.Map<CampaignGroup>(createCampaignGroup);
            _unitOfWork.CampaignGroups.Create(_campaignGroup);
            if (!_unitOfWork.Save())
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.INSERT,
                    ErrorMessageConstants.CAMPAIGN_GROUP.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            }
            _campaignGroup = _unitOfWork.CampaignGroups.Get(co =>
            co.CampaignId.Equals(createCampaignGroup.CampaignId) &&
            co.GroupId.Equals(createCampaignGroup.GroupId)).SingleOrDefault();
            return GetResponse(_campaignGroup);
        }

        public CampaignGroupViewModel DeleteCampaignGroupById(int id)
        {
            var _campaignGroup = _unitOfWork.CampaignGroups.Get(id);
            if (_campaignGroup == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.CAMPAIGN_GROUP_ID
                });
            _unitOfWork.CampaignGroups.Delete(_campaignGroup);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.DELETE,
                    ErrorMessageConstants.CAMPAIGN_GROUP.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            return _mapper.Map<CampaignGroupViewModel>(_campaignGroup);
        }
        #endregion

        #region Utils

        private CampaignGroupViewModel GetResponse(CampaignGroup campaignGroup)
        {
            var _response = _unitOfWork.CampaignGroups.Get(cc => cc.Id.Equals(campaignGroup.Id))
                .ProjectTo<CampaignGroupViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefault();
            return _response != null ? GetResponseDetail(_response) : new CampaignGroupViewModel();
        }

        private CampaignGroupViewModel GetResponseDetail(CampaignGroupViewModel response)
        {
            if (response.Campaign != null)
                response.Campaign = ServiceUtils.GetCampaignDetail(response.Campaign);
            if (response.Group != null)
                response.Group = ServiceUtils.GetResponseDetail(response.Group);
            return response;
        }
        #endregion
    }
}
