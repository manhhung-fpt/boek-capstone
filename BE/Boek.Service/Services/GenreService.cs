using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boek.Core.Constants;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.Genres;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.Genres;
using Boek.Repository.Interfaces;
using Boek.Service.Commons;
using Boek.Service.Interfaces;
using Boek.Service.Utils;
using Microsoft.EntityFrameworkCore;

namespace Boek.Service.Services
{
    public class GenreService : IGenreService
    {
        #region Fields and constructor
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Gets
        public List<GenreViewModel> GetChildGenres()
        {
            var _genres = _unitOfWork.Genres.Get(g => g.ParentId != null)
            .ProjectTo<GenreViewModel>(_mapper.ConfigurationProvider).ToList();
            _genres.ForEach(g => g = ServiceUtils.GetResponseDetail(g));
            return _genres;
        }

        public GenreBooksViewModel GetGenreById(int id, bool WithBooks = false)
        {
            var _genre = _unitOfWork.Genres.Get(g => g.Id.Equals(id))
                .Include(g => g.Books)
                .SingleOrDefault();
            if (_genre == null)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.GENRE_ID
                });
            }
            var response = _mapper.Map<GenreBooksViewModel>(_genre);
            if (!WithBooks)
                response.Books = null;
            return ServiceUtils.GetResponseDetail(response);
        }

        public BaseResponsePagingModel<GenreBooksViewModel> GetGenres(GenreRequestModel filter, PagingModel paging, bool WithBooks = false)
        {
            var _filter = _mapper.Map<GenreBooksViewModel>(filter);
            var query = _unitOfWork.Genres.Get()
                 .ProjectTo<GenreBooksViewModel>(_mapper.ConfigurationProvider)
                 .DynamicFilter(_filter);
            var list = new List<GenreBooksViewModel>();
            int count = 0;
            if (filter.IsParentGenre.HasValue)
            {
                if ((bool)filter.IsParentGenre)
                    query = query.Where(g => g.ParentId == null);
                else
                    query = query.Where(g => g.ParentId != null);
            }
            if (query.Any())
            {
                var result = query.PagingQueryable(paging.Page, paging.Size, PageConstant.LimitPaging, PageConstant.LimitPaging);
                if (result.Item1 > 0)
                {
                    count = result.Item1;
                    foreach (var item in result.Item2)
                    {
                        if (!WithBooks)
                            item.Books = null;
                        list.Add(ServiceUtils.GetResponseDetail(item));
                    }
                }
            }
            return new BaseResponsePagingModel<GenreBooksViewModel>()
            {
                Metadata = new PagingMetadata()
                {
                    Page = paging.Page,
                    Size = paging.Size,
                    Total = count
                },
                Data = list
            };
        }
        #endregion

        #region CUD
        public GenreViewModel CreateGenre(CreateGenreRequestModel createGenre)
        {
            var _genre = _mapper.Map<Genre>(createGenre);
            CheckGenre(_genre);
            if (CheckDuplicatedGenre(createGenre.Name) != null)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    MessageConstants.MESSAGE_DUPLICATED_INFO,
                    ErrorMessageConstants.GENRE.ToLower(),
                });
            }
            _genre.DisplayIndex = _unitOfWork.Genres.Get().Max(g => g.DisplayIndex) + 1;
            _genre.Status = true;
            _unitOfWork.Genres.Create(_genre);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.INSERT,
                    ErrorMessageConstants.GENRE.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            _genre = _unitOfWork.Genres.Get(g => g.Name.Equals(createGenre.Name)).SingleOrDefault();
            return GetResponse(_genre);
        }

        public GenreViewModel DeleteGenre(int id)
        {
            //cSpell:disable
            var _genre = new Genre();
            _genre = _unitOfWork.Genres.Get(g => g.Id.Equals(id))
                .Include(g => g.InverseParent)
                .ThenInclude(inverseParent => inverseParent.Books)
                .Include(g => g.Books)
                .SingleOrDefault();
            if (_genre == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.GENRE_ID.ToLower()
                });
            var _genres = _genre.InverseParent.Select(gipc => gipc.Books);
            if (_genre.Books.Any() || _genres.Any())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.DELETE,
                    ErrorMessageConstants.GENRE_ID.ToLower(),
                    MessageConstants.MESSAGE_FAILED,
                    MessageConstants.MESSAGE_LINKED_INFO
                });
            _unitOfWork.Genres.Delete(_genre);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.DELETE,
                    ErrorMessageConstants.GENRE.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            _genre = _unitOfWork.Genres.Get(id);
            return GetResponse(_genre);
        }

        public GenreViewModel UpdateGenre(UpdateGenreRequestModel updateGenre)
        {
            var genre = _unitOfWork.Genres.Get(g => g.Id.Equals(updateGenre.Id))
                .Include(g => g.InverseParent)
                .Include(g => g.Books)
                .SingleOrDefault();
            if (genre == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.GENRE_ID.ToLower()
                });
            var _genre = _mapper.Map<Genre>(updateGenre);
            CheckGenre(_genre);
            _genre = CheckDuplicatedGenre(updateGenre.Name);
            if (_genre != null && !_genre.Id.Equals(updateGenre.Id))
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    MessageConstants.MESSAGE_DUPLICATED_INFO,
                    ErrorMessageConstants.GENRE.ToLower(),
                });
            if (!(bool)updateGenre.Status &&
                (genre.Books.Any() || genre.InverseParent.Any()))
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.UPDATE,
                    ErrorMessageConstants.GENRE_STATUS,
                    MessageConstants.MESSAGE_FAILED,
                    MessageConstants.MESSAGE_LINKED_INFO
                });
            _mapper.Map(updateGenre, genre);
            _unitOfWork.Genres.Update(genre);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.UPDATE,
                    ErrorMessageConstants.GENRE.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            genre = _unitOfWork.Genres.Get(genre.Id);
            return GetResponse(genre);
        }
        #endregion

        #region Campaign Commission
        public List<GenreViewModel> GetOtherGenresForCampaignCommission(int campaignId)
        {
            var _campaign = _unitOfWork.Campaigns.Get(campaignId);
            if (_campaign == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.CAMPAIGN_ID
                });
            var _genres = _unitOfWork.Genres.Get();
            var _campaignCommission = _unitOfWork.CampaignCommissions
                .Get(cc => cc.CampaignId.Equals(campaignId))
                .Include(cc => cc.Genre);
            if (_campaignCommission != null)
            {
                var _existedCommissionGenres = _campaignCommission.Select(cc => cc.Genre).ToList();
                _genres = _genres.Except(_existedCommissionGenres);
            }
            return _mapper.Map<List<GenreViewModel>>(_genres);
        }
        #endregion

        #region Utils
        private Genre CheckDuplicatedGenre(string name) => _unitOfWork.Genres.CheckDuplicatedGenre(name);

        private GenreViewModel GetResponse(Genre genre)
        {
            var response = _mapper.Map<GenreViewModel>(genre);
            return ServiceUtils.GetResponseDetail(response);
        }

        private void CheckGenre(Genre genre)
        {
            if (string.IsNullOrEmpty(genre.Name))
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.PreconditionRequired), new string[]
                {
                    MessageConstants.MESSAGE_REQUIRED,
                    ErrorMessageConstants.GENRE_NAME
                });
            if (_unitOfWork.Genres.Get(genre.ParentId) == null && genre.ParentId != null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.PARENT_GENRE_ID.ToLower()
                });
        }
        #endregion
    }
}
