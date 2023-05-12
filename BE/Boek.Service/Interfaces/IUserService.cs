using Boek.Infrastructure.Requests.Users;
using Boek.Infrastructure.Requests.Users.Customers;
using Boek.Infrastructure.Requests.Users.Issuers;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.Users;
using Boek.Infrastructure.ViewModels.Users.Customers;
using Boek.Infrastructure.ViewModels.Users.Issuers;
using Boek.Service.Commons;

namespace Boek.Service.Interfaces
{
    public interface IUserService
    {
        #region Gets
        /// <summary>
        /// Get all users (<paramref name="filter"/>, <paramref name="paging"/>)
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns>Returns users</returns>
        BaseResponsePagingModel<MultiUserViewModel> GetUsers(UserRequestModel filter, PagingModel paging);
        /// <summary>
        /// Get a user by id (<paramref name="id"/>)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return a user's detail if found a user. Otherwise, it returns a not found message</returns>
        /// <exception cref="ErrorResponse">
        /// Throw a ErrorResponse if there is no matched user
        /// </exception>
        UserViewModel GetUserById(Guid userId, bool WithAddressDetail = false);
        /// <summary>
        /// Get an authenticated user
        /// </summary>
        /// <returns>Return a user's detail if found a user. Otherwise, it returns a not found message</returns>
        /// <exception cref="ErrorResponse">
        /// Throw a ErrorResponse if there is no matched user
        /// </exception>
        object GetCurrentLoginUser();
        #endregion
        #region CUD
        /// <summary>
        /// Create a customer (<paramref name="createCustomer"/>)
        /// </summary>
        /// <param name="createCustomer"></param>
        /// <returns>If a customer is valid, it returns a created customer's detail</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched customer
        /// 2. Throw a ErrorResponse if creating a customer is failed
        /// </exception>
        Task<object> CreateCustomer(CreateCustomerRequestModel createCustomer);
        /// <summary>
        /// Create a user (<paramref name="createUser"/>)
        /// </summary>
        /// <param name="createUser"></param>
        /// <returns>If a user is valid, it returns a created user's detail</returns>
        /// <exception cref="ErrorResponse">
        /// Throw a ErrorResponse if creating a user is failed
        /// </exception>
        Task<UserViewModel> CreateUser(CreateUserRequestModel createUser);
        /// <summary>
        /// Update a user (<paramref name="updateUser"/>)
        /// </summary>
        /// <param name="updateUser"></param>
        /// <returns>If a user is valid, it returns an updated user's detail</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched user
        /// 2. Throw a ErrorResponse if updating a user is failed
        /// </exception>
        UserViewModel UpdateUser(UpdateUserRequestModel updateUser);
        /// <summary>
        /// Update a customer (<paramref name="updateCustomer"/>)
        /// </summary>
        /// <param name="updateCustomer"></param>
        /// <returns>If a customer is valid, it returns an updated customer's detail</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched user
        /// 2. Throw a ErrorResponse if updating a customer is failed
        /// </exception>
        CustomerUserViewModel UpdateCustomerProfile(UpdateCustomerRequestModel updateCustomer);
        /// <summary>
        /// Update an issuer (<paramref name="updateIssuer"/>)
        /// </summary>
        /// <param name="updateIssuer"></param>
        /// <returns>If an issuer is valid, it returns an updated issuer's detail</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched issuer
        /// 2. Throw a ErrorResponse if updating an issuer is failed
        /// </exception>
        IssuerViewModel UpdateIssuerProfile(UpdateIssuerRequestModel updateIssuer);
        /// <summary>
        /// Update a user by admin (<paramref name="updateUser"/>)
        /// </summary>
        /// <param name="updateUser"></param>
        /// <returns>If a user is valid, it returns an updated user's detail</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched user
        /// 2. Throw a ErrorResponse if updating a user is failed
        /// </exception>
        UserViewModel UpdateUserByAdmin(UpdateUserRequestModel updateUser);
        /// <summary>
        /// Delete an user (<paramref name="id"/>)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>If an user is valid, then it returns the result of deleted user</returns>
        void DeleteUser(Guid id);
        /// <summary>
        /// login by email (<paramref name="id"/>)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return a user's detail if found a user. Otherwise, it returns a not found message</returns>
        /// <exception cref="ErrorResponse">
        /// Throw a ErrorResponse if there is no matched user
        /// </exception>
        Task<object> LoginByEmail(string idToken, string fcmToken);
        /// <summary>
        /// Check duplicated email (<paramref name="email"/>)
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Return true if found a duplicated user. Otherwise, it returns false</returns>
        #endregion
        #region Utils
        void CheckDuplicatedEmail(string email);
        #endregion
    }
}
