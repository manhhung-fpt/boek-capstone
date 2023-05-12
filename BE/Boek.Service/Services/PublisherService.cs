using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boek.Core.Constants;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.Publishers;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.Publishers;
using Boek.Repository.Interfaces;
using Boek.Service.Commons;
using Boek.Service.Interfaces;
using Boek.Service.Utils;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text;

namespace Boek.Service.Services
{
    public class PublisherService : IPublisherService
    {
        #region Fields and constructor
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PublisherService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Gets
        public PublisherViewModel GetPublisherById(int AuthorId)
        {
            var Publisher = _unitOfWork.Publishers.Get(AuthorId);
            if (Publisher == null)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.PUBLISHER_ID
                });
            }
            return _mapper.Map<PublisherViewModel>(Publisher);
        }

        public BaseResponsePagingModel<PublisherViewModel> GetPublishers(PublisherViewModel filter, PagingModel paging)
        {
            var result = _unitOfWork.Publishers.Get()
                .ProjectTo<PublisherViewModel>(_mapper.ConfigurationProvider)
                .DynamicFilter(filter)
                .PagingQueryable(paging.Page, paging.Size, PageConstant.LimitPaging, PageConstant.LimitPaging);
            return new BaseResponsePagingModel<PublisherViewModel>()
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

        #region CUD
        public PublisherViewModel CreatePublisher(CreatePublisherRequestModel createdPublisher)
        {
            if (CheckDuplicatedPublisherName(createdPublisher.Name) != null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    MessageConstants.MESSAGE_DUPLICATED_INFO,
                    ErrorMessageConstants.PUBLISHER_NAME
                });
            if (CheckDuplicatedPublisherEmail(createdPublisher.Email) != null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    MessageConstants.MESSAGE_DUPLICATED_INFO,
                    ErrorMessageConstants.PUBLISHER_EMAIL
                });
            var Publisher = _mapper.Map<Publisher>(createdPublisher);
            var publisherCode = GeneratePublisherCode();
            Publisher.Code = publisherCode;
            _unitOfWork.Publishers.Create(Publisher);
            if (!_unitOfWork.Save())
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.INSERT,
                    ErrorMessageConstants.PUBLISHER,
                    MessageConstants.MESSAGE_FAILED
                });
            }
            var _publisher = _unitOfWork.Publishers.Get(p => p.Name.Equals(createdPublisher.Name)).SingleOrDefault();
            return _mapper.Map<PublisherViewModel>(_publisher);
        }

        public PublisherViewModel UpdatePublisher(UpdatePublisherRequestModel updatedPublisher)
        {
            var Publisher = _unitOfWork.Publishers.Get(updatedPublisher.Id);
            if (Publisher == null)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.PUBLISHER_ID
                });
            }
            var _name = CheckDuplicatedPublisherName(updatedPublisher.Name);
            if (_name != null)
            {
                if (!_name.Id.Equals(updatedPublisher.Id))
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    MessageConstants.MESSAGE_DUPLICATED_INFO,
                    ErrorMessageConstants.PUBLISHER_NAME
                });
            }
            var _email = CheckDuplicatedPublisherEmail(updatedPublisher.Email);
            if (_email != null)
            {
                if (!_email.Id.Equals(updatedPublisher.Id))
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    MessageConstants.MESSAGE_DUPLICATED_INFO,
                    ErrorMessageConstants.PUBLISHER_EMAIL
                });
            }
            _mapper.Map(updatedPublisher, Publisher);
            _unitOfWork.Publishers.Update(Publisher);
            if (!_unitOfWork.Save())
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.UPDATE,
                    ErrorMessageConstants.PUBLISHER,
                    MessageConstants.MESSAGE_FAILED
                });
            }
            var _publisher = _unitOfWork.Publishers.Get(Publisher.Id);
            return _mapper.Map<PublisherViewModel>(_publisher);
        }

        public PublisherViewModel DeletePublisher(int? id)
        {
            var _publisher = _unitOfWork.Publishers
                .Get(p => p.Id.Equals(id))
                .Include(p => p.Books)
                .SingleOrDefault();
            if (_publisher == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.PUBLISHER_ID
                });
            if (_publisher.Books.Any())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.DELETE,
                    ErrorMessageConstants.PUBLISHER,
                    MessageConstants.MESSAGE_FAILED,
                    MessageConstants.MESSAGE_LINKED_INFO
                });
            _unitOfWork.Publishers.Delete(_publisher);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.DELETE,
                    ErrorMessageConstants.PUBLISHER,
                    MessageConstants.MESSAGE_FAILED
                });
            return _mapper.Map<PublisherViewModel>(_publisher);
        }
        #endregion

        #region Utils
        private string GeneratePublisherCode()
        {
            var UserCode = new StringBuilder();
            UserCode.Append("P");
            var _random = new Random();
            UserCode.Append(_random.Next(100000000, 999999999));
            return UserCode.ToString();
        }

        private Publisher CheckDuplicatedPublisherName(string name) => _unitOfWork.Publishers.CheckDuplicatedPublisherName(name);
        private Publisher CheckDuplicatedPublisherEmail(string email) => _unitOfWork.Publishers.CheckDuplicatedPublisherEmail(email);
        #endregion
    }
}
