using Boek.Infrastructure.Requests.Publishers;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.Publishers;
using Boek.Service.Commons;

namespace Boek.Service.Interfaces
{
    public interface IPublisherService
    {
        /// <summary>
        /// Get all publishers (<paramref name="filter"/>, <paramref name="paging"/>)
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns>Returns publishers</returns>
        BaseResponsePagingModel<PublisherViewModel> GetPublishers(PublisherViewModel filter, PagingModel paging);
        /// <summary>
        /// Get a publisher by id (<paramref name="id"/>)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return a publisher's detail if found a publisher. Otherwise, it returns a not found message</returns>
        /// <exception cref="ErrorResponse">
        /// Throw a ErrorResponse if there is no matched publisher
        /// </exception>
        PublisherViewModel GetPublisherById(int publisherId);
        /// <summary>
        /// Update a publisher (<paramref name="updatePublisher"/>)
        /// </summary>
        /// <param name="updatePublisher"></param>
        /// <returns>If a publisher is valid, it returns an updated publisher's detail</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched publisher
        /// 2. Throw a ErrorResponse if a publisher's name is duplicated
        /// 3. Throw a ErrorResponse if a publisher's email is duplicated
        /// 2. Throw a ErrorResponse if updating a publisher is failed
        /// </exception>
        PublisherViewModel UpdatePublisher(UpdatePublisherRequestModel updatePublisher);
        /// <summary>
        /// Create a publisher (<paramref name="createPublisher"/>)
        /// </summary>
        /// <param name="createPublisher"></param>
        /// <returns>If a publisher is valid, it returns a created publisher's detail</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched publisher
        /// 2. Throw a ErrorResponse if a publisher's name is duplicated
        /// 3. Throw a ErrorResponse if a publisher's email is duplicated
        /// 4. Throw a ErrorResponse if creating a publisher is failed
        /// </exception>
        PublisherViewModel CreatePublisher(CreatePublisherRequestModel createPublisher);
        /// <summary>
        /// Delete a publisher (<paramref name="id"/>)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>If a publisher is valid, it returns a Deleted publisher's detail</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched publisher
        /// 2. Throw a ErrorResponse if a publisher is linked to other information
        /// 2. Throw a ErrorResponse if deleting a publisher is failed
        /// </exception>
        PublisherViewModel DeletePublisher(int? id);
    }
}
