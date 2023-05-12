using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boek.Core.Constants;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.Authors;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.Authors;
using Boek.Infrastructure.ViewModels.Books;
using Boek.Repository.Interfaces;
using Boek.Service.Commons;
using Boek.Service.Interfaces;
using Boek.Service.Utils;
using Microsoft.EntityFrameworkCore;

namespace Boek.Service.Services
{
    public class AuthorService : IAuthorService
    {
        #region Fields and constructor
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Gets
        public BaseResponsePagingModel<AuthorBooksViewModel> GetAuthors(AuthorViewModel filter, PagingModel paging, bool WithBooks = false)
        {
            var _filter = _mapper.Map<AuthorBooksViewModel>(filter);
            var result = _unitOfWork.Authors.Get()
                .ProjectTo<AuthorBooksViewModel>(_mapper.ConfigurationProvider)
                .DynamicFilter(_filter)
                .PagingQueryable(paging.Page, paging.Size, PageConstant.LimitPaging, PageConstant.LimitPaging);
            var list = new List<AuthorBooksViewModel>();
            list = result.Item2.ToList();
            if (WithBooks)
                list.ForEach(a => a.Books = GetResponseDetails(a.Id));
            return new BaseResponsePagingModel<AuthorBooksViewModel>()
            {
                Metadata = new PagingMetadata()
                {
                    Page = paging.Page,
                    Size = paging.Size,
                    Total = result.Item1
                },
                Data = list
            };
        }

        public AuthorBooksViewModel GetAuthorById(int authorId, bool WithBooks = false)
        {
            var Author = _unitOfWork.Authors.Get(authorId);
            if (Author == null)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.AUTHOR_ID
                });
            }
            var result = _mapper.Map<AuthorBooksViewModel>(Author);
            if (WithBooks)
                result.Books = GetResponseDetails(authorId);
            return result;
        }
        #endregion

        #region CUD
        public AuthorViewModel CreateAuthor(CreateAuthorRequestModel createdAuthor)
        {
            if (CheckDuplicatedAuthorName(createdAuthor.Name) != null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    MessageConstants.MESSAGE_DUPLICATED_INFO,
                    ErrorMessageConstants.AUTHOR_NAME
                });
            var Author = _mapper.Map<Author>(createdAuthor);
            _unitOfWork.Authors.Create(Author);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.INSERT,
                    ErrorMessageConstants.AUTHOR,
                    MessageConstants.MESSAGE_FAILED
                });
            Author = _unitOfWork.Authors.Get(a => a.Name.ToLower().Equals(Author.Name.ToLower())).SingleOrDefault();
            return _mapper.Map<AuthorViewModel>(Author);
        }

        public AuthorViewModel CreateAuthorByIssuer(CreateAuthorByIssuerRequestModel createdAuthor)
        {
            if (CheckDuplicatedAuthorName(createdAuthor.Name) != null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    MessageConstants.MESSAGE_DUPLICATED_INFO,
                    ErrorMessageConstants.AUTHOR_NAME
                });
            var Author = _mapper.Map<Author>(createdAuthor);
            _unitOfWork.Authors.Create(Author);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.INSERT,
                    ErrorMessageConstants.AUTHOR,
                    MessageConstants.MESSAGE_FAILED
                });
            Author = _unitOfWork.Authors.Get(a => a.Name.ToLower().Equals(Author.Name.ToLower())).SingleOrDefault();
            return _mapper.Map<AuthorViewModel>(Author);
        }

        public AuthorViewModel DeleteAuthor(int? authorId)
        {
            var _author = _unitOfWork.Authors.Get(a => a.Id.Equals(authorId))
                .Include(a => a.BookAuthors)
                .SingleOrDefault();
            if (_author == null)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.AUTHOR_ID
                });
            }
            if (_author.BookAuthors.Any())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.DELETE,
                    ErrorMessageConstants.AUTHOR,
                    MessageConstants.MESSAGE_FAILED,
                    MessageConstants.MESSAGE_LINKED_INFO
                });
            _unitOfWork.Authors.Delete(_author);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.DELETE,
                    ErrorMessageConstants.AUTHOR,
                    MessageConstants.MESSAGE_FAILED
                });
            return _mapper.Map<AuthorViewModel>(_author);
        }

        public AuthorViewModel UpdateAuthor(UpdateAuthorRequestModel updatedAuthor)
        {
            var Author = _unitOfWork.Authors.Get(updatedAuthor.Id);
            if (Author == null)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.AUTHOR_ID
                });
            }
            var _name = CheckDuplicatedAuthorName(updatedAuthor.Name);
            if (_name != null)
            {
                if (!_name.Id.Equals(updatedAuthor.Id))
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    MessageConstants.MESSAGE_DUPLICATED_INFO,
                    ErrorMessageConstants.AUTHOR_NAME
                });
            }
            _mapper.Map(updatedAuthor, Author);
            _unitOfWork.Authors.Update(Author);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.UPDATE,
                    ErrorMessageConstants.AUTHOR,
                    MessageConstants.MESSAGE_FAILED
                });
            return _mapper.Map<AuthorViewModel>(_unitOfWork.Authors.Get(Author.Id));
        }
        #endregion

        #region Utils
        private Author CheckDuplicatedAuthorName(string name) => _unitOfWork.Authors.CheckDuplicatedAuthorName(name);

        public List<BasicBookViewModel> GetResponseDetails(int? authorId)
        {
            var result = _mapper.Map<List<BasicBookViewModel>>(_unitOfWork.BookAuthors.GetBooks(authorId));
            result.ForEach(b => b = ServiceUtils.GetResponseDetail(b));
            return result;
        }
        #endregion
    }
}
