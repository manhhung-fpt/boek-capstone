using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boek.Core.Constants;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.CampaignOrganizations;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.CampaignOrganizations;
using Boek.Repository.Interfaces;
using Boek.Service.Commons;
using Boek.Service.Interfaces;
using Boek.Service.Utils;
namespace Boek.Service.Services
{
    public class CampaignOrganizationService : ICampaignOrganizationService
    {
        #region Fields and constructor
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public CampaignOrganizationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Gets
        public CampaignOrganizationViewModel GetCampaignOrganizationById(int id)
        {
            var _campaignOrganization = _unitOfWork.CampaignOrganizations.Get(id);
            if (_campaignOrganization == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.CAMPAIGN_ORGANIZATION_ID
                });
            return GetResponse(_campaignOrganization);
        }

        public BaseResponsePagingModel<CampaignOrganizationViewModel> GetCampaignOrganizations(CampaignOrganizationRequestModel filter, PagingModel paging)
        {
            var _filter = new CampaignOrganizationViewModel();
            _mapper.Map(filter, _filter);
            var result = _unitOfWork.CampaignOrganizations.Get()
            .ProjectTo<CampaignOrganizationViewModel>(_mapper.ConfigurationProvider)
            .DynamicFilter(_filter)
            .PagingQueryable(paging.Page, paging.Size, PageConstant.LimitPaging, PageConstant.LimitPaging);
            var list = result.Item2.ToList();
            if (list.Any())
                list.ForEach(l => l = ServiceUtils.GetResponseDetail(l));
            return new BaseResponsePagingModel<CampaignOrganizationViewModel>()
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
        public CampaignOrganizationViewModel CreateCampaignOrganization(CreateCampaignOrganizationRequestModel createCampaignOrganization)
        {
            var _campaign =
                _unitOfWork.Campaigns.Get(createCampaignOrganization.CampaignId);
            var _organization =
                _unitOfWork.Organizations.Get(createCampaignOrganization.OrganizationId);
            var _campaignOrganization =
                _unitOfWork.CampaignOrganizations.Get(co =>
                co.CampaignId.Equals(createCampaignOrganization.CampaignId) &&
                co.OrganizationId.Equals(createCampaignOrganization.OrganizationId))
                .SingleOrDefault();
            if (_campaign == null)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.CAMPAIGN_ID
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
            if (_campaignOrganization != null)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.CAMPAIGN_ORGANIZATION.ToLower(),
                    MessageConstants.MESSAGE_EXISTED
                });
            }
            _campaignOrganization = _mapper.Map<CampaignOrganization>(createCampaignOrganization);
            _unitOfWork.CampaignOrganizations.Create(_campaignOrganization);
            if (!_unitOfWork.Save())
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.INSERT,
                    ErrorMessageConstants.CAMPAIGN_ORGANIZATION.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            }
            _campaignOrganization = _unitOfWork.CampaignOrganizations.Get(co =>
            co.CampaignId.Equals(createCampaignOrganization.CampaignId) &&
            co.OrganizationId.Equals(createCampaignOrganization.OrganizationId))
            .SingleOrDefault();
            return GetResponse(_campaignOrganization);
        }

        public CampaignOrganizationViewModel DeleteCampaignOrganizationById(int id)
        {
            var _campaignOrganization = _unitOfWork.CampaignOrganizations.Get(id);
            if (_campaignOrganization == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.CAMPAIGN_ORGANIZATION_ID
                });
            _unitOfWork.CampaignOrganizations.Delete(_campaignOrganization);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.DELETE,
                    ErrorMessageConstants.CAMPAIGN_ORGANIZATION.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            return _mapper.Map<CampaignOrganizationViewModel>(_campaignOrganization);
        }
        #endregion

        #region Utils

        private CampaignOrganizationViewModel GetResponse(CampaignOrganization campaignOrganization, bool WithAddressDetail = false)
        {
            var _response = _unitOfWork.CampaignOrganizations.Get(cc => cc.Id.Equals(campaignOrganization.Id))
                .ProjectTo<CampaignOrganizationViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefault();
            return _response != null ? ServiceUtils.GetResponseDetail(_response, WithAddressDetail) : new CampaignOrganizationViewModel();
        }
        #endregion
    }
}
