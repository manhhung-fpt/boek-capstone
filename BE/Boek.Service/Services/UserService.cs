using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boek.Core.Constants;
using Boek.Core.Entities;
using Boek.Core.Extensions;
using Boek.Infrastructure.Requests.Users;
using Boek.Infrastructure.Requests.Users.Customers;
using Boek.Infrastructure.Requests.Users.Issuers;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.Users;
using Boek.Infrastructure.ViewModels.Users.Customers;
using Boek.Infrastructure.ViewModels.Users.Issuers;
using Boek.Repository.Interfaces;
using Boek.Service.Commons;
using Boek.Service.Interfaces;
using Boek.Service.Utils;
using Boek.Core.Enums;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Net;

namespace Boek.Service.Services
{
    public class UserService : IUserService
    {
        #region Fields and constructor
        private readonly IConfiguration _configuration;
        private readonly IFireBaseService _fireBaseService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IConfiguration configuration, IFireBaseService fireBaseService, IMapper mapper, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _fireBaseService = fireBaseService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Gets
        public object GetCurrentLoginUser()
        {
            Guid? userId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            var user = _unitOfWork.Users.Get().SingleOrDefault(user => user.Id == userId);
            if (user == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.USER_ID
                });
            if (user.Role.Equals((byte)BoekRole.Issuer))
            {
                return GetIssuerRespond(user, true);
            }
            if (user.Role.Equals((byte)BoekRole.Customer))
            {
                return GetCustomerRespond(user, true);
            }
            return GetRespond(user, true);
        }

        public UserViewModel GetUserById(Guid userId, bool WithAddressDetail = false)
        {
            var user = _unitOfWork.Users.Get(userId);
            if (user == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.USER_ID
                });
            return GetRespond(user, true);
        }

        public BaseResponsePagingModel<MultiUserViewModel> GetUsers(UserRequestModel filter, PagingModel paging)
        {
            var _filter = new MultiUserViewModel();
            _mapper.Map(filter, _filter);
            var result = _unitOfWork.Users.Get()
                .ProjectTo<MultiUserViewModel>(_mapper.ConfigurationProvider)
                .DynamicFilter(_filter)
                .PagingQueryable(paging.Page, paging.Size, PageConstant.LimitPaging, PageConstant.LimitPaging);
            var list = result.Item2.ToList();
            list.ForEach(mu => mu = GetMultiUserResponse(mu, (bool)filter.WithAddressDetail));
            return new BaseResponsePagingModel<MultiUserViewModel>()
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
        #endregion

        #region CRU
        public async Task<UserViewModel> CreateUser(CreateUserRequestModel createUser)
        {
            var _user = _unitOfWork.Users.Get(x => x.Email.Equals(createUser.Email)).FirstOrDefault();
            if (_user != null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.USER_ACCOUNT,
                    MessageConstants.MESSAGE_EXISTED
                });
            if (createUser.Role.Equals((byte)BoekRole.Customer))
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
               {
                    ErrorMessageConstants.INSERT,
                    MessageConstants.MESSAGE_FAILED,
                    "vì",
                    ErrorMessageConstants.USER_ROLE
               });
            var UserId = Guid.NewGuid();
            var Role = StatusExtension<BoekRole>.GetEnumStatus(createUser.Role);
            var UserCode = GenerateUserCode(Role);
            var user = new User()
            {
                Id = UserId,
                Code = UserCode,
                Name = createUser.Name.Trim(),
                Email = createUser.Email.ToLower().Trim(),
                Role = (byte)createUser.Role,
                Address = "",
                Phone = "",
                Status = true,
                CreatedDate = DateTime.Now
            };
            if (Role.Equals(BoekRole.Issuer))
            {
                user.Issuer = new Issuer()
                {
                    Id = UserId
                };
            }
            _unitOfWork.Users.Create(user);
            _unitOfWork.Save();
            user = _unitOfWork.Users.Get(user.Id);
            if (user == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
               {
                    ErrorMessageConstants.INSERT,
                    ErrorMessageConstants.USER_ACCOUNT,
                    MessageConstants.MESSAGE_FAILED
               });
            return GetRespond(user);
        }

        public async Task<object> CreateCustomer(CreateCustomerRequestModel createCustomer)
        {
            UserRecord userRecord;
            var address = new AddressResponse();
            var ErrorMessages = new List<string>();
            int status = (int)HttpStatusCode.InternalServerError;
            try
            {
                userRecord = await _fireBaseService.GetUserRecordByIdToken(createCustomer.IdToken);
                if (string.IsNullOrEmpty(userRecord.Email))
                {
                    return null;
                }
                var user = _unitOfWork.Users.Get(x => x.Email.Equals(userRecord.Email)).FirstOrDefault();
                if (user == null)
                {
                    var UserGuid = Guid.NewGuid();
                    var userCode = GenerateUserCode(BoekRole.Customer);
                    address = ServiceUtils.CheckAddress(createCustomer.Address, true);
                    if (!address.IsSuccess)
                    {
                        ErrorMessages = address.ErrorMessages;
                        status = (int)HttpStatusCode.BadRequest;
                        throw new Exception();
                    }
                    var Organizations = GetCustomerOrganizations(userRecord.Email, UserGuid, ref ErrorMessages, ref status);
                    var Groups = GetCustomerGroups(createCustomer.GroupIds, UserGuid, ref ErrorMessages, ref status);
                    //Create new user
                    User newUser = new User()
                    {
                        Id = UserGuid,
                        Code = userCode,
                        Name = createCustomer.Name,
                        Email = userRecord.Email,
                        ImageUrl = userRecord.PhotoUrl,
                        Phone = createCustomer.Phone,
                        Address = address.Address,
                        Status = true,
                        Role = (byte)BoekRole.Customer,
                        CreatedDate = DateTime.Now,
                        Customer = new Customer()
                        {
                            Id = UserGuid,
                            LevelId = 1,
                            Dob = createCustomer.Dob ?? null,
                            Gender = createCustomer.Gender ?? null,
                            CustomerGroups = Groups ?? new List<CustomerGroup>(),
                            CustomerOrganizations = Organizations ?? new List<CustomerOrganization>(),
                        }
                    };

                    _unitOfWork.Users.Create(newUser);
                    if (_unitOfWork.Save())
                    {
                        var userInDb = _unitOfWork.Users.Get(x => x.Id == UserGuid).Include(u => u.Customer).FirstOrDefault();
                        var roleName = StatusExtension<BoekRole>.GetStatus(userInDb?.Role);
                        string[] roles = { roleName };
                        var newToken =
                            AccessTokenManager.GenerateJwtToken(string.IsNullOrEmpty(userInDb.Name)
                                ? ""
                                : userInDb.Name, roles, userInDb.Id, _configuration);

                        var responseSuccess = new BaseResponseModel<LoginViewModel>()
                        {
                            Status = new StatusModel()
                            {
                                Status = 200,
                                Message = MessageConstants.MESSAGE_SUCCESS,
                                Success = true
                            },
                            Data = new LoginViewModel()
                            {
                                AccessToken = newToken,
                                Id = userInDb.Id,
                                Phone = userInDb.Phone,
                                Address = userInDb.Address,
                                Email = userInDb.Email,
                                Name = userInDb.Name,
                                Dob = userInDb.Customer.Dob ?? null,
                                ImageUrl = userInDb.ImageUrl,
                                IsFirstLogin = true,
                                Role = userInDb.Role
                            }
                        };
                        return responseSuccess;
                    }
                    else
                    {
                        status = (int)HttpStatusCode.Conflict;
                        ErrorMessages = new List<string>()
                        {
                            ErrorMessageConstants.INSERT,
                            ErrorMessageConstants.USER_ACCOUNT,
                            MessageConstants.MESSAGE_FAILED
                        };
                        throw new Exception();
                    }
                }
                else
                {
                    ErrorMessages = new List<string>()
                    {
                        ErrorMessageConstants.ACCOUNT,
                        MessageConstants.MESSAGE_EXISTED
                    };
                    status = (int)HttpStatusCode.BadRequest;
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                BoekErrorMessage.ShowErrorMessage(status, ErrorMessages.Any() ?
                ErrorMessages.ToArray() :
                new string[] { e.Message });
            }
            var response = new BaseResponseModel<object>()
            {
                Status = new StatusModel()
                {
                    Success = false,
                    Message = MessageConstants.MESSAGE_FAILED,
                    Status = 400
                },
                Data = null
            };
            return response;
        }

        public void DeleteUser(Guid id)
        {
            var user = _unitOfWork.Users.Get(id);
            if (user == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
               {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.USER_ID
               });
            user.Status = false;
            _unitOfWork.Users.Update(user);
        }

        public async Task<object> LoginByEmail(string idToken, string fcmToken)
        {
            UserRecord userRecord;
            try
            {
                userRecord = await _fireBaseService.GetUserRecordByIdToken(idToken);
            }
            catch (Exception e)
            {
                var responseFailValidToken = new
                {
                    status = new
                    {
                        success = false,
                        message = e.Message,
                        status = ((int)HttpStatusCode.NotFound),
                    },
                    data = new
                    { }
                };
                return responseFailValidToken;
            }

            if (string.IsNullOrEmpty(userRecord.Email))
            {
                return null;
            }

            var user = _unitOfWork.Users.Get(x => x.Email.Equals(userRecord.Email)).FirstOrDefault();
            if (user == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.USER.ToLower()
                });

            if (!string.IsNullOrEmpty(user.Name) && !string.IsNullOrEmpty(user.Email))
            {
                if (!(bool)user.Status)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                    {
                        ErrorMessageConstants.ACCOUNT,
                        ErrorMessageConstants.DISABLE.ToLower()
                    });
                var roleName = StatusExtension<BoekRole>.GetStatus(user.Role);
                string[] roles = { roleName };
                var newToken =
                    AccessTokenManager.GenerateJwtToken(string.IsNullOrEmpty(user.Name)
                        ? ""
                        : user.Name, roles, user.Id, _configuration);

                var responseSuccess = new BaseResponseModel<LoginViewModel>()
                {
                    Status = new StatusModel()
                    {
                        Status = 200,
                        Message = MessageConstants.MESSAGE_SUCCESS,
                        Success = true
                    },
                    Data = new LoginViewModel()
                    {
                        AccessToken = newToken,
                        Id = user.Id,
                        Phone = user.Phone,
                        Email = user.Email,
                        Name = user.Name,
                        ImageUrl = user.ImageUrl,
                        Address = user.Address,
                        Role = user.Role
                    }
                };
                return responseSuccess;
            }

            var response = new BaseResponseModel<object>()
            {
                Status = new StatusModel()
                {
                    Success = false,
                    Message = MessageConstants.MESSAGE_FAILED,
                    Status = 200
                },
                Data = new
                {
                    Phone = userRecord.PhoneNumber,
                    Email = userRecord.Email,
                    ImageUrl = userRecord.PhotoUrl
                }
            };
            return response;
        }

        public UserViewModel UpdateUser(UpdateUserRequestModel updateUser)
        {
            CheckLoginUser(updateUser.Id);
            var user = _unitOfWork.Users.Get(updateUser.Id);
            if (user == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
               {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.USER_ID
               });
            CheckUpdateUserEmail(user, updateUser.Email);
            updateUser.Address = ServiceUtils.CheckAddress(updateUser.AddressRequest).Address;
            _mapper.Map(updateUser, user);
            user.UpdatedDate = DateTime.Now;
            _unitOfWork.Users.Update(user);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
               {
                    ErrorMessageConstants.UPDATE,
                    ErrorMessageConstants.USER_INFO,
                    MessageConstants.MESSAGE_FAILED
               });
            user = _unitOfWork.Users.Get(user.Id);
            return GetRespond(user, true);
        }

        public UserViewModel UpdateUserByAdmin(UpdateUserRequestModel updateUser)
        {
            var user = _unitOfWork.Users.Get(updateUser.Id);
            if (user == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.USER_ID
                });
            CheckUpdateUserEmail(user, updateUser.Email);
            updateUser.Address = ServiceUtils.CheckAddress(updateUser.AddressRequest).Address;
            _mapper.Map(updateUser, user);
            user.UpdatedDate = DateTime.Now;
            _unitOfWork.Users.Update(user);
            _unitOfWork.Save();
            user = _unitOfWork.Users.Get(user.Id);
            if (user == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
               {
                    ErrorMessageConstants.UPDATE,
                    ErrorMessageConstants.USER_INFO,
                    MessageConstants.MESSAGE_FAILED
               });
            return GetRespond(user, true);
        }

        public CustomerUserViewModel UpdateCustomerProfile(UpdateCustomerRequestModel updateCustomer)
        {
            var _updatedUser = UpdateUser(updateCustomer.User);
            if (_updatedUser == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
               {
                    ErrorMessageConstants.UPDATE,
                    ErrorMessageConstants.USER_INFO,
                    MessageConstants.MESSAGE_FAILED
               });
            var customer = _unitOfWork.Customers.Get(updateCustomer.User.Id);
            if (customer == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.USER_ID
                });
            var _basicCustomer = _mapper.Map<Customer>(updateCustomer);
            var _basicUser = _mapper.Map<User>(updateCustomer.User);
            customer.Dob = _basicCustomer.Dob;
            customer.Gender = _basicCustomer.Gender;
            _unitOfWork.Customers.Update(customer);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
               {
                    ErrorMessageConstants.UPDATE,
                    ErrorMessageConstants.USER_INFO,
                    MessageConstants.MESSAGE_FAILED
               });
            return GetBasicCustomerRespond(_basicUser, true);
        }

        public IssuerViewModel UpdateIssuerProfile(UpdateIssuerRequestModel updateIssuer)
        {
            var _updatedUser = UpdateUser(updateIssuer.User);
            if (_updatedUser == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
               {
                    ErrorMessageConstants.UPDATE,
                    ErrorMessageConstants.USER_INFO,
                    MessageConstants.MESSAGE_FAILED
               });
            var issuer = _unitOfWork.Issuers.Get(updateIssuer.User.Id);
            if (issuer == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.USER_ID
                });
            var _basicIssuer = _mapper.Map<Issuer>(updateIssuer);
            var _basicUser = _mapper.Map<User>(updateIssuer.User);
            issuer.Description = _basicIssuer.Description;
            _unitOfWork.Issuers.Update(issuer);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
               {
                    ErrorMessageConstants.UPDATE,
                    ErrorMessageConstants.USER_INFO,
                    MessageConstants.MESSAGE_FAILED
               });
            return GetIssuerRespond(_basicUser, true);
        }
        #endregion

        #region Utils
        private List<CustomerOrganization> GetCustomerOrganizations(string email, Guid CustomerId, ref List<string> ErrorMessages, ref int status)
        {
            var list = new List<CustomerOrganization>();
            var result = _unitOfWork.OrganizationMembers.GetOrganizationIdsByEmailDomain(email);
            if (result == null)
            {
                status = (int)HttpStatusCode.NotFound;
                ErrorMessages = new List<string>()
                {
                    MessageConstants.MESSAGE_INVALID,
                    ErrorMessageConstants.USER_EMAIL
                };
                throw new Exception();
            }
            else if (result.Any())
            {
                result.ForEach(item =>
                {
                    list.Add(new CustomerOrganization()
                    {
                        CustomerId = CustomerId,
                        OrganizationId = item
                    });
                });
            }
            return list.Any() ? list : null;
        }
        private List<CustomerGroup> GetCustomerGroups(List<int?> GroupIds, Guid CustomerId, ref List<string> ErrorMessages, ref int status)
        {
            var list = new List<CustomerGroup>();
            if (GroupIds.Any())
            {
                foreach (var gis in GroupIds)
                {
                    if (_unitOfWork.Groups.Get(gis) == null)
                    {
                        status = (int)HttpStatusCode.NotFound;
                        ErrorMessages = new List<string>()
                        {
                            ErrorMessageConstants.NOT_FOUND,
                            ErrorMessageConstants.CUSTOMER_GROUP_ID.ToLower()
                        };
                        throw new Exception();
                    }
                    list.Add(new CustomerGroup()
                    {
                        CustomerId = CustomerId,
                        GroupId = gis
                    });
                }
            }
            return list.Any() ? list : null;
        }
        private string GenerateUserCode(BoekRole Role)
        {
            var UserCode = new StringBuilder();
            UserCode.Append(Role.Equals(BoekRole.Admin) ? BoekRole.Admin.ToEnumMemberAttrValue()[..1] :
                Role.Equals(BoekRole.Issuer) ? BoekRole.Issuer.ToEnumMemberAttrValue()[..1] :
                Role.Equals(BoekRole.Staff) ? BoekRole.Staff.ToEnumMemberAttrValue()[..1] :
                BoekRole.Customer.ToEnumMemberAttrValue()[..1]);
            var _random = new Random();
            UserCode.Append(_random.Next(100000000, 999999999));
            return UserCode.ToString();
        }

        private UserViewModel GetRespond(User user, bool WithAddressDetail = false)
        {
            var response = _mapper.Map<UserViewModel>(user);
            return ServiceUtils.GetResponseDetail(response, WithAddressDetail);
        }

        private CustomerUserViewModel GetBasicCustomerRespond(User user, bool WithAddressDetail = false)
        {
            var customer = _unitOfWork.Customers
                .Get(c => c.Id.Equals(user.Id))
                .Include(c => c.IdNavigation)
                .SingleOrDefault();
            var response = _mapper.Map<CustomerUserViewModel>(customer);
            response.User = ServiceUtils.GetResponseDetail(response.User, WithAddressDetail);
            return response;
        }
        private CustomerViewModel GetCustomerRespond(User user, bool WithAddressDetail = false)
        {
            var response = _unitOfWork.Customers
                .Get(c => c.Id.Equals(user.Id))
                .ProjectTo<CustomerViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefault();
            if (response != null)
            {
                response.User = ServiceUtils.GetResponseDetail(response.User, WithAddressDetail);
                response.Level = ServiceUtils.GetResponseDetail(response.Level);
            }
            return response ?? new CustomerViewModel();
        }

        private MultiUserViewModel GetMultiUserResponse(MultiUserViewModel response, bool WithAddressDetail = false)
        {
            var _userViewModel = _mapper.Map<UserViewModel>(response);
            _userViewModel = ServiceUtils.GetResponseDetail(_userViewModel, WithAddressDetail);
            _mapper.Map(_userViewModel, response);
            if (response.Customer != null)
                response.Customer.Level = ServiceUtils.GetResponseDetail(response.Customer.Level);
            return response;
        }

        private IssuerViewModel GetIssuerRespond(User user, bool WithAddressDetail = false)
        {
            var issuers = _unitOfWork.Issuers
                .Get(i => i.Id.Equals(user.Id))
                .Include(i => i.IdNavigation)
                .SingleOrDefault();
            var response = _mapper.Map<IssuerViewModel>(issuers);
            response.User = ServiceUtils.GetResponseDetail(response.User, WithAddressDetail);
            return response;
        }

        private void CheckLoginUser(Guid? Id)
        {
            var UserId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            if (!Id.Equals(UserId))
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.USER,
                    MessageConstants.MESSAGE_INVALID
                });
        }

        private void CheckUpdateUserEmail(User user, string email)
        {
            if (!user.Email.Equals(email))
                CheckDuplicatedEmail(email);
        }
        public void CheckDuplicatedEmail(string email)
        {
            if (_unitOfWork.Users.CheckDuplicatedEmail(email))
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
               {
                    ErrorMessageConstants.ACCOUNT,
                    MessageConstants.MESSAGE_EXISTED
               });
        }
        #endregion
    }
}