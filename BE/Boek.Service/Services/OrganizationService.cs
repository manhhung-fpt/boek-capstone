using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boek.Core.Constants;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.Organizations;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.Campaigns;
using Boek.Infrastructure.ViewModels.OrganizationMembers;
using Boek.Infrastructure.ViewModels.Organizations;
using Boek.Infrastructure.ViewModels.Users.Customers;
using Boek.Repository.Interfaces;
using Boek.Service.Commons;
using Boek.Service.Interfaces;
using Boek.Service.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Boek.Service.Services
{
    public class OrganizationService : IOrganizationService
    {
        #region Field(s) and constructor
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrganizationService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Gets
        public OrganizationViewModel GetOrganizationById(int id)
        {
            var _organization = _unitOfWork.Organizations.Get(o => o.Id.Equals(id))
            .ProjectTo<OrganizationViewModel>(_mapper.ConfigurationProvider)
            .SingleOrDefault();
            if (_organization == null)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.ORGANIZATION_ID
                });
            }
            var list = new List<OrganizationViewModel>() { _organization };
            GetMemberViewModels(ref list, new OrganizationDetailRequestModel() { WithMembers = true });
            _organization = list.First();
            return _organization;
        }

        public BaseResponsePagingModel<OrganizationViewModel> GetOrganizations(OrganizationRequestModel filter, PagingModel paging, OrganizationDetailRequestModel organizationDetail)
        {
            var _filter = _mapper.Map<OrganizationViewModel>(filter);
            var result = _unitOfWork.Organizations.Get()
                    .ProjectTo<OrganizationViewModel>(_mapper.ConfigurationProvider)
                    .DynamicFilter(_filter)
                    .PagingQueryable(paging.Page, paging.Size, PageConstant.LimitPaging, PageConstant.LimitPaging);
            var list = result.Item2.ToList();
            GetUserViewModels(ref list, organizationDetail);
            GetCampaignViewModels(ref list, organizationDetail);
            GetMemberViewModels(ref list, organizationDetail);
            return new BaseResponsePagingModel<OrganizationViewModel>()
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
        public OrganizationViewModel CreateOrganization(CreateOrganizationRequestModel createOrganization)
        {
            var _organization = _mapper.Map<Organization>(createOrganization);
            if (CheckDuplicatedOrganizationName(_organization.Name) != null)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[] {
                        MessageConstants.MESSAGE_DUPLICATED_INFO,
                        ErrorMessageConstants.ORGANIZATION_NAME
                    });
            }
            _organization.OrganizationMembers = CheckOrganizationMembers(_organization.OrganizationMembers.ToList());
            _unitOfWork.Organizations.Create(_organization);
            if (!_unitOfWork.Save())
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[] {
                        ErrorMessageConstants.INSERT,
                        ErrorMessageConstants.ORGANIZATION.ToLower(),
                        MessageConstants.MESSAGE_FAILED
                    });
            }
            _organization = _unitOfWork.Organizations
                    .Get(o => o.Name.Equals(_organization.Name))
                    .Include(o => o.OrganizationMembers)
                    .SingleOrDefault();
            UpdateCustomerOrganizationByOrganizationMember(_organization.OrganizationMembers.ToList(), true);
            return GetResponse(_organization.Id);
        }

        public BasicOrganizationViewModel DeleteOrganization(int id)
        {
            var _organization =
                _unitOfWork.Organizations.Get(o => o.Id.Equals(id))
                    .Include(o => o.CampaignOrganizations)
                    .Include(o => o.CustomerOrganizations)
                    .Include(o => o.OrganizationMembers)
                    .SingleOrDefault();
            if (_organization == null)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.ORGANIZATION_ID
                });
            }
            if (_organization.CampaignOrganizations.Any() ||
                _organization.CustomerOrganizations.Any())
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.DELETE,
                    ErrorMessageConstants.ORGANIZATION.ToLower(),
                    MessageConstants.MESSAGE_FAILED,
                    MessageConstants.MESSAGE_LINKED_INFO
                });
            }
            var _organizationMembers = _organization.OrganizationMembers.ToList();
            _unitOfWork.OrganizationMembers.RemoveRange(_organizationMembers);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.DELETE,
                    ErrorMessageConstants.ORGANIZATION_MEMBER.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            _unitOfWork.Organizations.Delete(_organization);
            if (!_unitOfWork.Save())
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.DELETE,
                    ErrorMessageConstants.ORGANIZATION.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            }
            return _mapper.Map<BasicOrganizationViewModel>(_organization);
        }

        public OrganizationViewModel UpdateOrganization(UpdateOrganizationRequestModel updateOrganization)
        {
            var _organization = _unitOfWork.Organizations.Get(updateOrganization.Id);
            if (_organization == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.ORGANIZATION_ID
                });
            var _name = CheckDuplicatedOrganizationName(updateOrganization.Name);
            if (_name != null && !_name.Id.Equals(updateOrganization.Id))
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    MessageConstants.MESSAGE_DUPLICATED_INFO,
                    ErrorMessageConstants.ORGANIZATION_NAME
                });
            var _organizationMembers = _mapper.Map<List<OrganizationMember>>(updateOrganization.OrganizationMembers);
            _organizationMembers.ForEach(om => om.OrganizationId = updateOrganization.Id);
            _organizationMembers = CheckOrganizationMembers(_organizationMembers, false);
            var basic = _mapper.Map<BasicOrganizationViewModel>(updateOrganization);
            _mapper.Map(basic, _organization);
            _unitOfWork.Organizations.Update(_organization);
            if (!_unitOfWork.Save())
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.UPDATE,
                    ErrorMessageConstants.ORGANIZATION.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            }
            UpdateOrganizationMembers(_organizationMembers);
            _organization = _unitOfWork.Organizations.Get(updateOrganization.Id);
            return GetResponse(_organization.Id);
        }
        #endregion

        #region Utils
        private OrganizationViewModel GetResponse(int id)
        {
            var response = _unitOfWork.Organizations.Get(o => o.Id.Equals(id))
                    .Include(o => o.OrganizationMembers)
                    .ProjectTo<OrganizationViewModel>(_mapper.ConfigurationProvider)
                    .SingleOrDefault();
            if (response != null)
            {
                response.Members = ServiceUtils.GetResponsesDetails(response.Members);
                return response;
            }
            return null;
        }
        private void GetUserViewModels(ref List<OrganizationViewModel> list, OrganizationDetailRequestModel organizationDetail)
        {
            if (organizationDetail.WithCustomers.HasValue)
            {
                if ((bool)organizationDetail.WithCustomers)
                {
                    list.ForEach(o =>
                    {
                        var customers = _mapper.Map<List<CustomerUserViewModel>>(_unitOfWork.CustomerOrganizations.GetCustomers(o.Id));
                        customers.ForEach(c => c.User = ServiceUtils.GetResponseDetail(c.User));
                        o.Customers = customers;
                    });
                }
                else
                    list.ForEach(o => o.Customers = null);
            }
            else
                list.ForEach(o => o.Customers = null);
        }
        private void GetCampaignViewModels(ref List<OrganizationViewModel> list, OrganizationDetailRequestModel organizationDetail)
        {
            if (organizationDetail.WithCampaigns.HasValue)
            {
                if ((bool)organizationDetail.WithCampaigns)
                {
                    list.ForEach(o =>
                    {
                        var campaigns =
                            _mapper.Map<List<BasicCampaignViewModel>>(_unitOfWork
                                    .CampaignOrganizations
                                    .GetCampaigns(o.Id));
                        campaigns
                            .ForEach(c => c = ServiceUtils.GetCampaignDetail(c));
                        o.Campaigns = campaigns;
                    });
                }
                else
                    list.ForEach(o => o.Campaigns = null);
            }
            else
                list.ForEach(o => o.Campaigns = null);
        }
        private void GetMemberViewModels(ref List<OrganizationViewModel> list, OrganizationDetailRequestModel organizationDetail)
        {
            if (organizationDetail.WithMembers.HasValue)
            {
                if ((bool)organizationDetail.WithMembers)
                {
                    list.ForEach(o =>
                    {
                        var members = _unitOfWork.OrganizationMembers.GetOrganizationMembersByOrganizationId(o.Id);
                        if (members != null)
                        {
                            if (members.Any())
                            {
                                var _members = _mapper.Map<List<OrganizationMemberViewModel>>(members);
                                _members = ServiceUtils.GetResponsesDetails(_members);
                                o.Members = _members;
                            }
                            else
                                o.Members = null;
                        }
                        else
                            o.Members = null;
                    });
                }
                else
                    list.ForEach(o => o.Members = null);
            }
            else
                list.ForEach(o => o.Members = null);
        }

        public Organization CheckDuplicatedOrganizationName(string name) =>
            _unitOfWork.Organizations.CheckDuplicatedOrganizationName(name);

        private List<OrganizationMember> CheckOrganizationMembers(List<OrganizationMember> organizationMembers, bool IsCreate = true)
        {
            if (organizationMembers != null)
            {
                if (organizationMembers.Any())
                {
                    if (IsCreate)
                    {
                        if (organizationMembers.Any(om => string.IsNullOrEmpty(om.EmailDomain)))
                            BoekErrorMessage.ShowErrorMessage((int)HttpStatusCode.BadRequest, new string[]
                            {
                                MessageConstants.MESSAGE_REQUIRED,
                                ErrorMessageConstants.ORGANIZATION_MEMBER_EMAIL_DOMAIN
                            });
                        var values = organizationMembers.GroupBy(om => om.EmailDomain)
                        .Where(o => o.Count() > 1)
                        .Select(o => o.Key).ToList();
                        if (values.Any())
                        {
                            var message = "";
                            values.ToList().ForEach(v =>
                            {
                                if (string.IsNullOrEmpty(message))
                                    message = $"{v};";
                                else
                                    message += $"{v};";
                            });
                            BoekErrorMessage.ShowErrorMessage((int)HttpStatusCode.BadRequest, new string[]
                            {
                                MessageConstants.MESSAGE_DUPLICATED_INFO,
                                ErrorMessageConstants.ORGANIZATION_MEMBER_EMAIL_DOMAIN,
                                message
                            });
                        }
                        organizationMembers.ForEach(om => om.Status = true);
                    }
                    else
                    {
                        if (organizationMembers.Any(om => !om.OrganizationId.HasValue))
                            BoekErrorMessage.ShowErrorMessage((int)HttpStatusCode.BadRequest, new string[]
                            {
                                MessageConstants.MESSAGE_REQUIRED,
                                ErrorMessageConstants.ORGANIZATION_ID
                            });
                        if (organizationMembers.Any(om => string.IsNullOrEmpty(om.EmailDomain)))
                            BoekErrorMessage.ShowErrorMessage((int)HttpStatusCode.BadRequest, new string[]
                            {
                                MessageConstants.MESSAGE_REQUIRED,
                                ErrorMessageConstants.ORGANIZATION_MEMBER_EMAIL_DOMAIN
                            });

                        organizationMembers.GroupBy(om => om.OrganizationId).ToList().ForEach(om =>
                        {
                            var values = om.Select(o => o)
                            .GroupBy(o => o.EmailDomain)
                            .Where(o => o.Count() > 1)
                            .Select(o => o.Key).ToList();

                            if (values.Any())
                            {
                                var message = "";
                                values.ToList().ForEach(v =>
                                {
                                    if (string.IsNullOrEmpty(message))
                                        message = $"{v};";
                                    else
                                        message += $"{v};";
                                });
                                BoekErrorMessage.ShowErrorMessage((int)HttpStatusCode.BadRequest, new string[]
                                {
                                    MessageConstants.MESSAGE_DUPLICATED_INFO,
                                    ErrorMessageConstants.ORGANIZATION_MEMBER_EMAIL_DOMAIN,
                                    om.Key.ToString(),
                                    message
                                });
                            }
                        });
                    }
                }
                else
                    BoekErrorMessage.ShowErrorMessage((int)HttpStatusCode.BadRequest, new string[]
                    {
                        MessageConstants.MESSAGE_REQUIRED,
                        ErrorMessageConstants.ORGANIZATION_MEMBER
                    });
            }
            else
                BoekErrorMessage.ShowErrorMessage((int)HttpStatusCode.BadRequest, new string[]
                {
                    MessageConstants.MESSAGE_REQUIRED,
                    ErrorMessageConstants.ORGANIZATION_MEMBER
                });
            return organizationMembers;
        }

        private void UpdateOrganizationMembers(List<OrganizationMember> organizationMembers)
        {
            if (organizationMembers != null)
            {
                if (organizationMembers.Any())
                {
                    var organizationIds = organizationMembers.Where(om => om.OrganizationId.HasValue).Select(om => om.OrganizationId).ToList();
                    var list = _unitOfWork.OrganizationMembers.Get(om => organizationIds.Contains(om.OrganizationId)).ToList();
                    if (list.Any())
                    {
                        var DeleteOrganizationMembers = new List<OrganizationMember>();
                        var NewOrganizationMembers = new List<OrganizationMember>();
                        var UpdateOrganizationMembers = new List<OrganizationMember>();
                        organizationMembers.ForEach(oms =>
                        {
                            var item = list.SingleOrDefault(om => om.EmailDomain.Equals(oms.EmailDomain));
                            if (item != null)
                                UpdateOrganizationMembers.Add(item);
                            else
                            {
                                NewOrganizationMembers.Add(new OrganizationMember()
                                {
                                    OrganizationId = oms.OrganizationId,
                                    EmailDomain = oms.EmailDomain,
                                    Status = true
                                });
                            }
                        });
                        if (UpdateOrganizationMembers.Any())
                        {
                            var existedEmailDomains = UpdateOrganizationMembers.Select(nom => nom.EmailDomain);
                            list.ForEach(item =>
                            {
                                if (!existedEmailDomains.Contains(item.EmailDomain))
                                {
                                    item.Status = false;
                                    DeleteOrganizationMembers.Add(item);
                                }
                            });
                            var newUpdate = new List<OrganizationMember>();
                            UpdateOrganizationMembers.ForEach(uom =>
                            {
                                var temp = organizationMembers.SingleOrDefault(om => uom.EmailDomain.Equals(om.EmailDomain));
                                if (temp != null)
                                {
                                    if (!((bool)temp.Status).Equals((bool)uom.Status))
                                    {
                                        var item = list.SingleOrDefault(om => om.EmailDomain.Equals(uom.EmailDomain));
                                        item.Status = temp.Status;
                                        newUpdate.Add(item);
                                    }
                                }
                            });
                            if (newUpdate.Any())
                            {
                                UpdateCustomerOrganizationByOrganizationMember(newUpdate);
                                _unitOfWork.OrganizationMembers.UpdateRange(newUpdate);
                                if (!_unitOfWork.Save())
                                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                                    {
                                        ErrorMessageConstants.UPDATE,
                                        ErrorMessageConstants.ORGANIZATION_MEMBER.ToLower(),
                                        MessageConstants.MESSAGE_FAILED
                                    });
                            }
                            if (DeleteOrganizationMembers.Any())
                            {
                                UpdateCustomerOrganizationByOrganizationMember(DeleteOrganizationMembers);
                                _unitOfWork.OrganizationMembers.UpdateRange(DeleteOrganizationMembers);
                                if (!_unitOfWork.Save())
                                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                                    {
                                        ErrorMessageConstants.DELETE,
                                        ErrorMessageConstants.ORGANIZATION_MEMBER.ToLower(),
                                        MessageConstants.MESSAGE_FAILED
                                    });
                            }
                        }

                        if (NewOrganizationMembers.Any())
                        {
                            _unitOfWork.OrganizationMembers.UpdateRange(NewOrganizationMembers);
                            if (!_unitOfWork.Save())
                                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                                {
                                    ErrorMessageConstants.UPDATE,
                                    ErrorMessageConstants.ORGANIZATION_MEMBER.ToLower(),
                                    MessageConstants.MESSAGE_FAILED
                                });
                            var organizationId = NewOrganizationMembers.First().OrganizationId;
                            var EmailDomains = NewOrganizationMembers.Select(nom => nom.EmailDomain.ToLower().Trim());
                            var temp = _unitOfWork.OrganizationMembers.Get(om => om.OrganizationId.Equals(organizationId) &&
                            EmailDomains.Contains(om.EmailDomain)).ToList();
                            UpdateCustomerOrganizationByOrganizationMember(temp);
                        }
                    }
                }
            }
        }

        private void UpdateCustomerOrganizationByOrganizationMember(List<OrganizationMember> organizationMembers, bool IsCreate = false)
        {
            if (organizationMembers.Any())
            {
                var organizationId = organizationMembers.Where(om => om.OrganizationId.HasValue).First().OrganizationId;
                var validOrganizations = organizationMembers.Where(om => om.Status.Equals(true)).ToList();
                var invalidOrganizations = organizationMembers.Where(om => om.Status.Equals(false)).ToList();
                if (validOrganizations.Any())
                {

                    var list = new List<OrganizationMember>();
                    if (IsCreate)
                    {
                        var keys = validOrganizations.Select(o => o.OrganizationId).Distinct().ToList();
                        list = _unitOfWork.OrganizationMembers.Get(om => keys.Contains(om.OrganizationId)).ToList();
                    }
                    else
                    {
                        var keys = validOrganizations.Select(o => o.Id).Distinct().ToList();
                        list = _unitOfWork.OrganizationMembers.Get(om => keys.Contains(om.Id)).ToList();
                    }
                    if (list.Any())
                    {
                        var domains = list.Select(item => item.EmailDomain.ToLower().Trim()).ToList();
                        var customers = new List<CustomerOrganization>();
                        domains.ForEach(d =>
                        {
                            var temp = _unitOfWork.Customers.Get(c => c.IdNavigation.Email.ToLower().Trim().Substring((c.IdNavigation.Email.IndexOf("@") + 1)).Equals(d)
                            && !c.CustomerOrganizations.Any(co => co.OrganizationId.Equals(organizationId)));
                            if (temp.Any())
                            {
                                temp.ToList().ForEach(u =>
                                {
                                    if (!customers.Any(c => c.CustomerId.Equals(u.Id)))
                                    {
                                        customers.Add(new CustomerOrganization()
                                        {
                                            CustomerId = u.Id,
                                            OrganizationId = organizationId
                                        });
                                    }
                                });
                            }
                        });
                        if (customers.Any())
                        {
                            _unitOfWork.CustomerOrganizations.AddRange(customers);
                            if (!_unitOfWork.Save())
                                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                                {
                                    ErrorMessageConstants.INSERT,
                                    ErrorMessageConstants.CUSTOMER_ORGANIZATION.ToLower(),
                                    MessageConstants.MESSAGE_FAILED
                                });
                        }
                    }
                }

                if (invalidOrganizations.Any())
                {

                    var keys = invalidOrganizations.Select(o => o.Id).ToList();
                    var list = _unitOfWork.OrganizationMembers.Get(om => keys.Contains(om.Id)).ToList();
                    if (list.Any())
                    {
                        var domains = list.Select(item => item.EmailDomain.ToLower().Trim()).ToList();
                        var customers = new List<CustomerOrganization>();
                        domains.ForEach(d =>
                        {
                            var temp = _unitOfWork.Customers.Get(c => c.IdNavigation.Email.ToLower().Trim().Substring((c.IdNavigation.Email.IndexOf("@") + 1)).Equals(d)
                            && c.CustomerOrganizations.Any(co => co.OrganizationId.Equals(organizationId)))
                            .Include(c => c.CustomerOrganizations).ToList();
                            if (temp.Any())
                            {
                                var customerOrganizations = temp.SelectMany(u => u.CustomerOrganizations)
                                .Where(u => u.OrganizationId.Equals(organizationId))
                                .ToList();
                                if (customerOrganizations.Any())
                                {
                                    customerOrganizations.ForEach(cos =>
                                    {
                                        if (!customers.Any(c => c.CustomerId.Equals(cos.CustomerId)))
                                            customers.Add(cos);
                                    });
                                }
                            }
                        });
                        if (customers.Any())
                        {
                            _unitOfWork.CustomerOrganizations.RemoveRange(customers);
                            if (!_unitOfWork.Save())
                                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                                {
                                    ErrorMessageConstants.DELETE,
                                    ErrorMessageConstants.CUSTOMER_ORGANIZATION.ToLower(),
                                    MessageConstants.MESSAGE_FAILED
                                });
                        }
                    }
                }
            }
        }
        #endregion
    }
}
