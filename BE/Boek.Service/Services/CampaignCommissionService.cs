using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boek.Core.Constants;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.CampaignCommissions;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.CampaignCommissions;
using Boek.Repository.Interfaces;
using Boek.Service.Commons;
using Boek.Service.Interfaces;
using Boek.Service.Utils;
using Boek.Core.Enums;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Boek.Service.Services
{
    public class CampaignCommissionService : ICampaignCommissionService
    {
        #region Fields and constructor
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CampaignCommissionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Gets
        public CampaignCommissionViewModel GetCampaignCommissionById(int id)
        {
            var _campaignCommission = _unitOfWork.CampaignCommissions.Get(id);
            if (_campaignCommission == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.CAMPAIGN_COMMISSION_ID
                });
            return GetResponse(_campaignCommission);
        }

        public BaseResponsePagingModel<CampaignCommissionViewModel> GetCampaignsCommissions(CampaignCommissionRequestModel filter, PagingModel paging)
        {
            var _filter = _mapper.Map<CampaignCommissionViewModel>(filter);
            var result = _unitOfWork.CampaignCommissions.Get()
                .ProjectTo<CampaignCommissionViewModel>(_mapper.ConfigurationProvider)
                .DynamicFilter(_filter)
                .PagingQueryable(paging.Page, paging.Size, PageConstant.LimitPaging, PageConstant.LimitPaging);
            var list = result.Item2.ToList();
            if (list.Any())
                list.ForEach(cc => cc = GetResponseDetail(cc));
            return new BaseResponsePagingModel<CampaignCommissionViewModel>()
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
        public CampaignCommissionViewModel CreateCampaignCommission(CreateCampaignCommissionRequestModel createCampaignCommission)
        {
            var _campaignCommission = _mapper.Map<CampaignCommission>(createCampaignCommission);
            CheckCampaignCommission(_campaignCommission);
            _unitOfWork.CampaignCommissions.Create(_campaignCommission);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.INSERT,
                    ErrorMessageConstants.CAMPAIGN_COMMISSION.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            _campaignCommission = _unitOfWork.CampaignCommissions
                .Get(cc =>
                cc.CampaignId.Equals(_campaignCommission.CampaignId) &&
                cc.GenreId.Equals(_campaignCommission.GenreId))
                .SingleOrDefault();
            return GetResponse(_campaignCommission);
        }
        public CampaignCommissionViewModel DeleteCampaignCommission(int id)
        {
            var _campaignCommission = _unitOfWork.CampaignCommissions.Get(cc => cc.Id.Equals(id))
                .Include(cc => cc.Campaign)
                .SingleOrDefault();
            if (_campaignCommission == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.CAMPAIGN_COMMISSION_ID
                });
            CheckDate((DateTime)_campaignCommission.Campaign.StartDate, (DateTime)_campaignCommission.Campaign.EndDate, false, true);
            CheckBookProduct(_campaignCommission);
            _unitOfWork.CampaignCommissions.Delete(_campaignCommission);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.DELETE,
                    ErrorMessageConstants.CAMPAIGN_COMMISSION.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            return _mapper.Map<CampaignCommissionViewModel>(_campaignCommission);
        }
        public CampaignCommissionViewModel UpdateCampaignCommission(UpdateCampaignCommissionRequestModel updateCampaignCommission)
        {
            CheckCampaignCommission(_mapper.Map<CampaignCommission>(updateCampaignCommission), false);
            var _campaignCommission = _unitOfWork.CampaignCommissions.Get(updateCampaignCommission.Id);
            _mapper.Map(updateCampaignCommission, _campaignCommission);
            _unitOfWork.CampaignCommissions.Update(_campaignCommission);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.INSERT,
                    ErrorMessageConstants.CAMPAIGN_COMMISSION.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            _campaignCommission = _unitOfWork.CampaignCommissions.Get(updateCampaignCommission.Id);
            return GetResponse(_campaignCommission);
        }
        #endregion

        #region Utils

        private CampaignCommissionViewModel GetResponse(CampaignCommission campaignCommission)
        {
            var _response = _unitOfWork.CampaignCommissions
                .Get(cc => cc.Id.Equals(campaignCommission.Id))
                .ProjectTo<CampaignCommissionViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefault();
            return _response != null ? GetResponseDetail(_response) : new CampaignCommissionViewModel(); ;
        }
        private CampaignCommissionViewModel GetResponseDetail(CampaignCommissionViewModel _response)
        {
            if (_response.Genre != null)
            {
                _response.Genre = ServiceUtils.GetResponseDetail(_response.Genre);
            }
            if (_response.Campaign != null)
            {
                _response.Campaign = ServiceUtils.GetCampaignDetail(_response.Campaign);
            }
            return _response;
        }

        private void CheckCampaignCommission(CampaignCommission campaignCommission, bool IsCreate = true)
        {
            if (!IsCreate)
            {
                if (_unitOfWork.CampaignCommissions.Get(campaignCommission.Id) == null)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                    {
                        ErrorMessageConstants.NOT_FOUND,
                        ErrorMessageConstants.CAMPAIGN_COMMISSION_ID
                    });
            }
            else
            {
                if (_unitOfWork.CampaignCommissions.Get(cc =>
                cc.CampaignId.Equals(campaignCommission.CampaignId) &&
                cc.GenreId.Equals(campaignCommission.GenreId)).SingleOrDefault() != null)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                    {
                        ErrorMessageConstants.CAMPAIGN_COMMISSION,
                        MessageConstants.MESSAGE_EXISTED
                    });
            }
            if (campaignCommission.CampaignId == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.PreconditionRequired), new string[]
                {
                    MessageConstants.MESSAGE_REQUIRED,
                    ErrorMessageConstants.CAMPAIGN_ID
                });
            var _campaign = _unitOfWork.Campaigns.Get(campaignCommission.CampaignId);
            if (_campaign == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.CAMPAIGN_ID
                });
            CheckCampaignStatus(_campaign, _campaign.Format);
            CheckDate((DateTime)_campaign.StartDate, (DateTime)_campaign.EndDate, IsCreate);
            if (campaignCommission.GenreId == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.PreconditionRequired), new string[]
                {
                    MessageConstants.MESSAGE_REQUIRED,
                    ErrorMessageConstants.GENRE_ID
                });
            var _genre = _unitOfWork.Genres.Get(campaignCommission.GenreId);
            if (_genre == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.GENRE_ID
                });
            if (!(bool)_genre.Status)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    IsCreate ? ErrorMessageConstants.INSERT : ErrorMessageConstants.UPDATE,
                    ErrorMessageConstants.CAMPAIGN_COMMISSION,
                    MessageConstants.MESSAGE_FAILED,
                    "vì",
                    ErrorMessageConstants.GENRE_STATUS
                });
        }

        private void CheckDate(DateTime startDate, DateTime endDate, bool IsCreate = true, bool IsDelete = false)
        {
            var _startDate = startDate;
            var _endDate = endDate;
            var _now = DateTime.Now;
            if (DateTime.Compare(_startDate.Date, _now.Date) <= 0)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    IsDelete? ErrorMessageConstants.DELETE :
                    IsCreate ? ErrorMessageConstants.INSERT :
                    ErrorMessageConstants.UPDATE,
                    ErrorMessageConstants.CAMPAIGN_COMMISSION,
                    MessageConstants.MESSAGE_FAILED,
                    "vì",
                    ErrorMessageConstants.CAMPAIGN_START_DATE
                });
            if (DateTime.Compare(_endDate.Date, _now.Date) <= 0)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    IsDelete? ErrorMessageConstants.DELETE :
                    IsCreate ? ErrorMessageConstants.INSERT :
                    ErrorMessageConstants.UPDATE,
                    ErrorMessageConstants.CAMPAIGN_COMMISSION,
                    MessageConstants.MESSAGE_FAILED,
                    "vì",
                    ErrorMessageConstants.CAMPAIGN_END_DATE
                });
        }

        private void CheckBookProduct(CampaignCommission campaignCommission)
        {
            //cSpell:disable
            var _bookProducts = _unitOfWork.BookProducts
                .Get(bp => bp.CampaignId.Equals(campaignCommission.CampaignId))
                .Include(bp => bp.BookProductItems)
                .ThenInclude(bookProductItem => bookProductItem.Book)
                .Include(bp => bp.Book)
                .ToList();
            var _genre = _unitOfWork.Genres.Get(g => g.Id.Equals(campaignCommission.GenreId))
                .Include(g => g.InverseParent)
                .SingleOrDefault();
            var _ChildrenBookProducts = new List<BookProduct>();
            var _ParentBookProducts = new List<BookProduct>();
            if (_genre.InverseParent.Any())
            {
                var _childgenres = _genre.InverseParent.Select(ip => ip.Id);
                _bookProducts.ForEach(bp =>
                {
                    var flag = false;
                    if (_childgenres.Contains((int)bp.Book.GenreId) &&
                    !_ChildrenBookProducts.Contains(bp))
                        _ChildrenBookProducts.Add(bp);
                    bp.BookProductItems.ToList().ForEach(b =>
                    {
                        var _book = b.Book;
                        if (_childgenres.Contains((int)_book.GenreId))
                            flag = true;
                    });
                    if (flag && !_ChildrenBookProducts.Contains(bp))
                        _ChildrenBookProducts.Add(bp);
                });
            }
            _bookProducts.ForEach(bp =>
                {
                    var flag = false;
                    if (_genre.Id.Equals((int)bp.Book.GenreId) &&
                    !_ParentBookProducts.Contains(bp))
                        _ParentBookProducts.Add(bp);
                    bp.BookProductItems.ToList().ForEach(b =>
                    {
                        var _book = b.Book;
                        if (_genre.Id.Equals((int)_book.GenreId))
                            flag = true;
                    });
                    if (flag && !_ParentBookProducts.Contains(bp))
                        _ParentBookProducts.Add(bp);
                });
            if (_ChildrenBookProducts.Any() || _ParentBookProducts.Any())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.DELETE,
                    ErrorMessageConstants.CAMPAIGN_COMMISSION.ToLower(),
                    MessageConstants.MESSAGE_FAILED,
                    MessageConstants.MESSAGE_LINKED_INFO
                });
        }

        private void CheckCampaignStatus(Campaign Campaign, byte? format)
        {
            if (Campaign.Status.Equals((byte)CampaignStatus.Cancelled))
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
            {
                ErrorMessageConstants.UPDATE,
                ErrorMessageConstants.CAMPAIGN_COMMISSION.ToLower(),
                MessageConstants.MESSAGE_FAILED,
                ErrorMessageConstants.CAMPAIGN_CANCEL
            });
        }
        #endregion
    }
}
