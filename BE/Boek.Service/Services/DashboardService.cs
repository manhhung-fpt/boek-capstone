using AutoMapper;
using Boek.Core.Enums;
using Boek.Infrastructure.Requests.Dashboards;
using Boek.Infrastructure.ViewModels.Dashboards;
using Boek.Repository.Interfaces;
using Boek.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using DateTimeExtensions;
using Boek.Core.Constants;
using Boek.Infrastructure.ViewModels.Campaigns;
using Boek.Infrastructure.ViewModels.Users.Customers;
using Boek.Infrastructure.ViewModels.Orders;
using Boek.Infrastructure.ViewModels.BookProducts;
using AutoMapper.QueryableExtensions;
using Boek.Service.Utils;
using Boek.Core.Entities;
using Boek.Infrastructure.ViewModels.Users;
using Boek.Service.Commons;
using System.Net;
using Boek.Infrastructure.ViewModels.Dashboards.Revenue;
using Boek.Infrastructure.ViewModels.Users.Issuers;
using Boek.Infrastructure.ViewModels.Dashboards.Revenue.Issuer;
using Microsoft.EntityFrameworkCore;
using Boek.Core.Extensions;

namespace Boek.Service.Services
{
    public class DashboardService : IDashboardService
    {
        #region Fields and constructor
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public DashboardService(IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region System
        public DashboardViewModel<BasicCampaignViewModel> GetCreatedCampaigns(DashboardRequestModel request)
        {
            request.SetDescendingTimeLine();
            request.SetSeparateDay();
            var timeLines = ValidateTimeLine(request.TimeLine, ErrorMessageConstants.CAMPAIGN, request.IsDescendingTimeLine, request.SeparateDay, true);
            if (timeLines == null)
                return null;
            var result = new DashboardViewModel<BasicCampaignViewModel>()
            {
                models = new List<SubDashboardViewModel<BasicCampaignViewModel>>()
            };
            var _campaigns = _unitOfWork.Campaigns.Get()
            .ProjectTo<BasicCampaignViewModel>(_mapper.ConfigurationProvider)
            .ToList();
            if (_campaigns.Any())
            {
                foreach (var timeLine in timeLines)
                {
                    var subDashboard = new SubDashboardViewModel<BasicCampaignViewModel>()
                    {
                        timeLine = timeLine
                    };
                    var temp = _campaigns.Where(c =>
                    ((DateTime)c.CreatedDate).IsInsideIn((DateTime)c.CreatedDate,
                    (DateTime)timeLine.StartDate,
                    (DateTime)timeLine.EndDate)).ToList();
                    if (temp.Any())
                        subDashboard.Data = temp;
                    result.models.Add(subDashboard);
                }
            }
            if (result.models.Any())
                result.models.ForEach(m =>
                {
                    if (m.Data.Any())
                        m.Data.ForEach(d => d = ServiceUtils.GetCampaignDetail(d));
                });
            result.UpdateTotal(request.IsDescendingData, request.IsDescendingTimeLine);
            return result;
        }

        public DashboardViewModel<CustomerViewModel> GetNewCustomers(DashboardRequestModel request)
        {
            request.SetDescendingTimeLine();
            request.SetSeparateDay();
            var timeLines = ValidateTimeLine(request.TimeLine, ErrorMessageConstants.CUSTOMER, request.IsDescendingTimeLine, request.SeparateDay, true);
            if (timeLines == null)
                return null;
            var result = new DashboardViewModel<CustomerViewModel>()
            {
                models = new List<SubDashboardViewModel<CustomerViewModel>>()
            };
            var _customers = _unitOfWork.Customers.Get()
            .ProjectTo<CustomerViewModel>(_mapper.ConfigurationProvider)
            .ToList();
            if (_customers.Any())
            {
                foreach (var timeLine in timeLines)
                {
                    var subDashboard = new SubDashboardViewModel<CustomerViewModel>()
                    {
                        timeLine = timeLine
                    };
                    var temp = _customers.Where(c =>
                    ((DateTime)c.User.CreatedDate).IsInsideIn((DateTime)c.User.CreatedDate,
                    (DateTime)timeLine.StartDate,
                    (DateTime)timeLine.EndDate)).ToList();
                    if (temp.Any())
                        subDashboard.Data = temp;
                    result.models.Add(subDashboard);
                }
            }
            if (result.models.Any())
                result.models.ForEach(m =>
                {
                    if (m.Data.Any())
                        m.Data.ForEach(d => d.User = ServiceUtils.GetResponseDetail(d.User));
                });
            result.UpdateTotal(request.IsDescendingData, request.IsDescendingTimeLine);
            return result;
        }

        public RevenueListViewModel<BasicCampaignViewModel> GetCampaignsRevenue(DashboardRequestModel request)
        {
            request.SetDefaultSizeData();
            request.SetDescendingData();
            request.SetDescendingTimeLine();
            request.SetSeparateDay();
            var timeLines = ValidateTimeLine(request.TimeLine, ErrorMessageConstants.REVENUE, request.IsDescendingTimeLine, request.SeparateDay);
            if (timeLines == null)
                return null;
            var result = new RevenueListViewModel<BasicCampaignViewModel>()
            {
                Models = new List<SubRevenueListViewModel<BasicCampaignViewModel>>()
            };
            List<CampaignOrderViewModel> _campaigns = null;
            var orderValidList = new List<byte?>()
            {
                (byte)OrderStatus.Shipped,
                (byte)OrderStatus.Received
            };
            _campaigns = _unitOfWork.Campaigns.Get(c =>
                c.Orders.Any(o => orderValidList.Contains(o.Status)))
                .ProjectTo<CampaignOrderViewModel>(_mapper.ConfigurationProvider)
                .ToList();
            var IsIssuer = ServiceUtils.CheckRole(_httpContextAccessor, BoekRole.Issuer);
            if (IsIssuer)
            {
                var IssuerId = ServiceUtils.GetUserInfo(_httpContextAccessor);
                var participantValidList = new List<byte?>()
                {
                    (byte)ParticipantStatus.Accepted,
                    (byte)ParticipantStatus.Approved
                };
                _campaigns = _campaigns.Where(c =>
                c.Participants.Any(p => p.IssuerId.Equals(IssuerId) && participantValidList.Contains(p.Status)))
                .ToList();
            }
            if (_campaigns.Any())
            {
                foreach (var timeLine in timeLines)
                {
                    var temp = _campaigns.Where(c =>
                    ((DateTime)c.StartDate).IsInsideIn((DateTime)c.EndDate,
                    (DateTime)timeLine.StartDate,
                    (DateTime)timeLine.EndDate)).ToList();
                    if (temp.Any())
                    {
                        var list = GetRevenues(temp, request.SizeData, request.IsDescendingData);
                        if (list != null)
                        {
                            list.timeLine = timeLine;
                            result.Models.Add(list);
                        }
                    }
                }
                if (result.Models.Any())
                {
                    result.UpdateTotal(request.IsDescendingData, request.IsDescendingTimeLine);
                    return result;
                }
            }
            return null;
        }

        public List<SummaryViewModel> GetSummaryDashboard()
        {
            var timeLines = new List<TimeLineViewModel>()
            {
                new TimeLineViewModel()
                {
                    StartDate = DateTime.Now.Date.AddDays(-1),
                    EndDate = DateTime.Now.Date.AddTicks(-1)
                },
                new TimeLineViewModel()
                {
                    StartDate = DateTime.Now.Date,
                    EndDate = DateTime.Now.Date.AddDays(1).AddTicks(-1)
                }
            };
            Guid? IssuerId = null;
            var IsIssuer = ServiceUtils.CheckRole(_httpContextAccessor, BoekRole.Issuer);
            if (IsIssuer)
                IssuerId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            return GetSummary(timeLines, IssuerId);
        }
        #endregion

        #region Stakeholders
        public DashboardViewModel<CampaignParticipantsViewModel> GetParticipantsFromCampaign(DashboardRequestModel request)
        {
            request.SetDescendingData();
            request.SetDefaultSizeData();
            request.SetDescendingTimeLine();
            request.SetSeparateDay();
            var timeLines = ValidateTimeLine(request.TimeLine, ErrorMessageConstants.PARTICIPANT, request.IsDescendingTimeLine, request.SeparateDay);
            if (timeLines == null)
                return null;
            var result = new DashboardViewModel<CampaignParticipantsViewModel>()
            {
                models = new List<SubDashboardViewModel<CampaignParticipantsViewModel>>()
            };
            var validStatus = new List<byte?>()
            {
                (byte)ParticipantStatus.Accepted,
                (byte)ParticipantStatus.Approved
            };
            var _campaigns = _unitOfWork.Campaigns.Get(c => c.Participants.Any(p => validStatus.Contains(p.Status)))
            .ProjectTo<CampaignParticipantsViewModel>(_mapper.ConfigurationProvider)
            .ToList();
            if (_campaigns.Any())
            {
                foreach (var timeLine in timeLines)
                {
                    var subDashboard = new SubDashboardViewModel<CampaignParticipantsViewModel>()
                    {
                        timeLine = timeLine
                    };
                    var temp = _campaigns.Where(c =>
                    ((DateTime)c.CreatedDate).IsInsideIn((DateTime)c.CreatedDate,
                    (DateTime)timeLine.StartDate,
                    (DateTime)timeLine.EndDate)).ToList();
                    if (temp.Any())
                    {
                        temp = temp.GroupBy(c => c.Participants.Count())
                        .OrderByDescending(c => c.Key)
                        .Where(c => c.Any(item => item.Participants.Any(p => validStatus.Contains(p.Status))))
                        .SelectMany(c => c)
                        .ToList();
                        if (temp.Any())
                        {
                            var list = new List<CampaignParticipantsViewModel>();
                            temp.ForEach(item =>
                            {
                                item.Participants = item.Participants.Where(p => validStatus.Contains(p.Status)).ToList();
                                if (item.Participants.Any())
                                    list.Add(item);
                            });
                            if (list.Any())
                                subDashboard.Data = list.Take((int)request.SizeData).ToList();
                        }
                    }
                    result.models.Add(subDashboard);
                }
            }
            if (result.models.Any())
                result.models.ForEach(m =>
                {
                    if (m.Data.Any())
                    {
                        m.Data.ForEach(d =>
                        {
                            d = ServiceUtils.GetResponseDetail(d);
                            d.UpdateTotal();
                        });
                        var result = !request.IsDescendingTimeLine.HasValue || false.Equals(request.IsDescendingTimeLine);
                        if (request.IsDescendingData.HasValue && result)
                        {
                            if ((bool)request.IsDescendingData)
                                m.Data = m.Data.OrderByDescending(item => item.Total).ToList();
                            else
                                m.Data = m.Data.OrderBy(item => item.Total).ToList();
                        }
                    }
                });
            result.UpdateTotal(request.IsDescendingData, request.IsDescendingTimeLine);
            return result;
        }

        public DashboardViewModel<OrderViewModel> GetOrdersByAdmin(DashboardRequestModel request)
        {
            request.SetDescendingTimeLine();
            request.SetSeparateDay();
            var timeLines = ValidateTimeLine(request.TimeLine, ErrorMessageConstants.ORDER, request.IsDescendingTimeLine, request.SeparateDay, true);
            if (timeLines == null)
                return null;
            var result = new DashboardViewModel<OrderViewModel>()
            {
                models = new List<SubDashboardViewModel<OrderViewModel>>()
            };
            var _orders = _unitOfWork.Orders.Get()
            .ProjectTo<OrderViewModel>(_mapper.ConfigurationProvider)
            .ToList();
            if (_orders.Any())
            {
                foreach (var timeLine in timeLines)
                {
                    var subDashboard = new SubDashboardViewModel<OrderViewModel>()
                    {
                        timeLine = timeLine
                    };
                    var temp = _orders.Where(c =>
                    ((DateTime)c.OrderDate).IsInsideIn((DateTime)c.OrderDate,
                    (DateTime)timeLine.StartDate,
                    (DateTime)timeLine.EndDate)).ToList();
                    if (temp.Any())
                        subDashboard.Data = temp;
                    result.models.Add(subDashboard);
                }
            }
            if (result.models.Any())
                result.models.ForEach(m =>
                {
                    if (m.Data.Any())
                    {
                        m.Data.ForEach(d =>
                        {
                            d = ServiceUtils.GetResponseDetail(d);
                            d = ServiceUtils.GetTotal(d, _mapper, _unitOfWork);
                        });
                    }
                });
            result.UpdateTotal(request.IsDescendingData, request.IsDescendingTimeLine);
            return result;
        }
        public DashboardViewModel<OrderViewModel> GetOrdersByIssuer(DashboardRequestModel request)
        {
            request.SetDescendingTimeLine();
            request.SetSeparateDay();
            var timeLines = ValidateTimeLine(request.TimeLine, ErrorMessageConstants.ORDER, request.IsDescendingTimeLine, request.SeparateDay, true);
            if (timeLines == null)
                return null;
            var result = new DashboardViewModel<OrderViewModel>()
            {
                models = new List<SubDashboardViewModel<OrderViewModel>>()
            };
            var IssuerId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            var _orders = _unitOfWork.Orders.Get(o => o.OrderDetails.Any(od => od.BookProduct.IssuerId.Equals(IssuerId)))
            .ProjectTo<OrderViewModel>(_mapper.ConfigurationProvider)
            .ToList();
            if (_orders.Any())
            {
                foreach (var timeLine in timeLines)
                {
                    var subDashboard = new SubDashboardViewModel<OrderViewModel>()
                    {
                        timeLine = timeLine
                    };
                    var temp = _orders.Where(c =>
                    ((DateTime)c.OrderDate).IsInsideIn((DateTime)c.OrderDate,
                    (DateTime)timeLine.StartDate,
                    (DateTime)timeLine.EndDate)).ToList();
                    if (temp.Any())
                        subDashboard.Data = temp;
                    result.models.Add(subDashboard);
                }
            }
            if (result.models.Any())
                result.models.ForEach(m =>
                {
                    if (m.Data.Any())
                    {
                        m.Data.ForEach(d =>
                        {
                            d = ServiceUtils.GetResponseDetail(d);
                            d = ServiceUtils.GetTotal(d, _mapper, _unitOfWork);
                        });
                    }
                });
            result.UpdateTotal(request.IsDescendingData, request.IsDescendingTimeLine);
            return result;
        }

        public DashboardViewModel<BookProductViewModel> GetBookProducts(DashboardRequestModel request)
        {
            request.SetDescendingTimeLine();
            request.SetSeparateDay();
            var timeLines = ValidateTimeLine(request.TimeLine, ErrorMessageConstants.BOOK_PRODUCT, request.IsDescendingTimeLine, request.SeparateDay, true);
            if (timeLines == null)
                return null;
            var result = new DashboardViewModel<BookProductViewModel>()
            {
                models = new List<SubDashboardViewModel<BookProductViewModel>>()
            };
            var IssuerId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            var _bookProducts = _unitOfWork.BookProducts.Get(bps => bps.IssuerId.Equals(IssuerId))
            .ProjectTo<BookProductViewModel>(_mapper.ConfigurationProvider)
            .Where(bps => bps.CreatedDate.HasValue)
            .ToList();
            if (_bookProducts.Any())
            {
                foreach (var timeLine in timeLines)
                {
                    var subDashboard = new SubDashboardViewModel<BookProductViewModel>()
                    {
                        timeLine = timeLine
                    };
                    var temp = _bookProducts.Where(bp =>
                    ((DateTime)bp.CreatedDate).IsInsideIn((DateTime)bp.CreatedDate,
                    (DateTime)timeLine.StartDate,
                    (DateTime)timeLine.EndDate)).ToList();
                    if (temp.Any())
                        subDashboard.Data = temp;
                    result.models.Add(subDashboard);
                }
            }
            if (result.models.Any())
                result.models.ForEach(m =>
                {
                    if (m.Data.Any())
                        m.Data.ForEach(d => d = ServiceUtils.GetResponseDetail(d));
                });
            result.UpdateTotal(request.IsDescendingData, request.IsDescendingTimeLine);
            return result;
        }

        public DashboardRevenueViewModel<BasicCampaignViewModel, IssuerViewModel> GetIssuersRevenueFromCampaignsByAdmin(DashboardRequestModel request)
        {
            request.SetDefaultSizeData();
            request.SetDefaultSizeSubject();
            request.SetDescendingData();
            request.SetDescendingTimeLine();
            request.SetSeparateDay();
            var timeLines = ValidateTimeLine(request.TimeLine, ErrorMessageConstants.REVENUE, request.IsDescendingTimeLine, request.SeparateDay);
            if (timeLines == null)
                return null;
            var result = new DashboardRevenueViewModel<BasicCampaignViewModel, IssuerViewModel>()
            {
                Models = new List<RevenuesViewModel<BasicCampaignViewModel, IssuerViewModel>>()
            };
            List<CampaignOrderViewModel> _campaigns = null;
            var orderValidList = new List<byte?>()
            {
                (byte)OrderStatus.Shipped,
                (byte)OrderStatus.Received
            };
            _campaigns = _unitOfWork.Campaigns.Get(c =>
                c.Orders.Any(o => orderValidList.Contains(o.Status)))
                .ProjectTo<CampaignOrderViewModel>(_mapper.ConfigurationProvider)
                .ToList();
            if (_campaigns.Any())
            {
                var participantValidList = new List<byte?>()
                {
                    (byte)ParticipantStatus.Accepted,
                    (byte)ParticipantStatus.Approved
                };
                _campaigns = _campaigns.Where(c =>
                c.Participants.Any(p => participantValidList.Contains(p.Status)))
                .ToList();
                foreach (var timeLine in timeLines)
                {
                    if (result.Models.Count() != request.SizeSubject)
                    {
                        var temp = _campaigns.Where(c =>
                        ((DateTime)c.CreatedDate).IsInsideIn((DateTime)c.CreatedDate,
                        (DateTime)timeLine.StartDate,
                        (DateTime)timeLine.EndDate)).ToList();
                        if (temp.Any())
                        {
                            var list = GetRevenues(_campaigns, request.SizeData, request.SizeSubject, request.IsDescendingData);
                            if (list != null)
                            {
                                list.ForEach(item => item.timeLine = timeLine);
                                result.Models.AddRange(list);
                            }
                        }
                    }
                }
                if (result.Models.Any())
                {
                    result.UpdateTotal(request.IsDescendingData, request.IsDescendingTimeLine);
                    return result;
                }
            }
            return null;
        }

        public DashboardRevenueViewModel<BasicCampaignViewModel, IssuerViewModel> GetIssuersRevenueFromCampaign(CampaignDashboardRequestModel request)
        {
            request.SetDefaultSizeData();
            var result = new DashboardRevenueViewModel<BasicCampaignViewModel, IssuerViewModel>()
            {
                Models = new List<RevenuesViewModel<BasicCampaignViewModel, IssuerViewModel>>()
            };
            List<CampaignOrderViewModel> _campaigns = null;
            var orderValidList = new List<byte?>()
            {
                (byte)OrderStatus.Shipped,
                (byte)OrderStatus.Received
            };
            _campaigns = _unitOfWork.Campaigns.Get(c =>
                c.Orders.Any(o => orderValidList.Contains(o.Status)) &&
                c.Id.Equals(request.CampaignId))
                .ProjectTo<CampaignOrderViewModel>(_mapper.ConfigurationProvider)
                .ToList();
            var list = GetRevenues(_campaigns, request.SizeData, null, request.IsDescendingData);
            if (list != null)
            {
                result.Models.AddRange(list);
                result.UpdateTotal(request.IsDescendingData);
                return result;
            }
            return null;
        }

        #region Note
        //1. Default time line: Today (check if it is within campaign's date)
        //Revenue: today
        //Book Product: today
        //Order: today
        //2. Different time line (type - Day):
        //Revenue, Book Product, and Order: 
        //- Date: 
        //+ Start date and end date are not null: check if valid date time, valid order, and within campaign's date
        //+ Start date is not null: check if valid date time, valid order, and within campaign's date
        //+ End date is not null: check if valid date time, valid order, and within campaign's date
        //+ Start date and end date are null: campaign's date (day)
        //3. All the time:
        //- If campaign's dates are within 30 days => day
        //- If campaign's date are larger than 30 days => month
        //Revenue: campaign's date (day/month)
        //Book Product: campaign's date (day/month)
        //Order: campaign's date (day/month)
        #endregion
        public IssuerRevenuesViewModel<BasicCampaignViewModel> GetIssuersRevenueFromCampaign(IssuerCampaignDashboardRequestModel request)
        {
            request.SetDefaultAllTheTime();
            request.DashboardRequestModel.SetDefaultSizeData(5);
            request.DashboardRequestModel.SetDescendingData();
            request.DashboardRequestModel.SetDescendingTimeLine();
            var campaign = new CampaignOrderViewModel();
            var IsIssuer = ServiceUtils.CheckRole(_httpContextAccessor, BoekRole.Issuer);
            if (IsIssuer)
            {
                var IssuerId = ServiceUtils.GetUserInfo(_httpContextAccessor);
                var participantValidList = new List<byte?>()
                {
                    (byte)ParticipantStatus.Accepted,
                    (byte)ParticipantStatus.Approved
                };
                campaign = _unitOfWork.Campaigns.Get(c => c.Id.Equals(request.CampaignId) &&
                c.Participants.Any(p => p.IssuerId.Equals(IssuerId) &&
                participantValidList.Contains(p.Status)))
                .ProjectTo<CampaignOrderViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefault();
                if (campaign != null)
                {
                    campaign.Orders = campaign.Orders.Where(o =>
                    o.OrderDetails.Any(od => od.BookProduct.IssuerId.Equals(IssuerId))).ToList();
                }
            }
            else
            {
                campaign = _unitOfWork.Campaigns.Get(c => c.Id.Equals(request.CampaignId))
                .ProjectTo<CampaignOrderViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefault();
            }
            if (campaign != null)
            {
                var _campaign = _mapper.Map<Campaign>(campaign);
                var timeLines = ValidateTimeLine(request.DashboardRequestModel.TimeLine, _campaign, ErrorMessageConstants.REVENUE, false, (bool)request.IsAllTheTime);
                if (timeLines != null)
                {
                    if (timeLines.Any())
                    {
                        var result = new IssuerRevenuesViewModel<BasicCampaignViewModel>()
                        {
                            Revenues = new List<IssuerRevenueViewModel>(),
                            Orders = new DashboardViewModel<OrdersViewModel>()
                        };
                        //Book Products
                        var bookProducts = GetBestSellerFromCampaigns(timeLines, campaign, (int)request.DashboardRequestModel.SizeData);
                        if (bookProducts != null)
                            result.BestSellerBookProducts = bookProducts;

                        //Orders
                        var orders = GetOrdersFromCampaigns(timeLines, campaign, (bool)request.DashboardRequestModel.IsDescendingData);
                        if (orders != null)
                            result.Orders.models = orders;
                        //Revenues
                        var separateTimeLines = ValidateTimeLine(request.DashboardRequestModel.TimeLine, _campaign, ErrorMessageConstants.REVENUE, false, (bool)request.IsAllTheTime, true);
                        if (separateTimeLines != null)
                        {
                            if (separateTimeLines.Any())
                            {
                                var temp = GetRevenues(separateTimeLines, campaign);
                                if (temp != null)
                                    result.Revenues.AddRange(temp);
                            }
                        }
                        result.timeLine = timeLines.First();
                        result.Subject = _mapper.Map<BasicCampaignViewModel>(campaign);
                        result.UpdateTotal(request.DashboardRequestModel.IsDescendingData, request.DashboardRequestModel.IsDescendingTimeLine);
                        return result;
                    }
                }
            }
            return null;
        }

        public DashboardRevenueViewModel<IssuerViewModel, BasicCampaignViewModel> GetIssuersRevenueByAdmin(DashboardRequestModel request)
        {
            request.SetDefaultSizeData();
            request.SetDefaultSizeSubject();
            request.SetDescendingData();
            request.SetDescendingTimeLine();
            request.SetSeparateDay();
            var timeLines = ValidateTimeLine(request.TimeLine, ErrorMessageConstants.REVENUE, request.IsDescendingTimeLine, request.SeparateDay);
            if (timeLines == null)
                return null;
            var result = new DashboardRevenueViewModel<IssuerViewModel, BasicCampaignViewModel>()
            {
                Models = new List<RevenuesViewModel<IssuerViewModel, BasicCampaignViewModel>>()
            };
            List<CampaignOrderViewModel> _campaigns = null;
            var orderValidList = new List<byte?>()
            {
                (byte)OrderStatus.Shipped,
                (byte)OrderStatus.Received
            };
            _campaigns = _unitOfWork.Campaigns.Get(c =>
                c.Orders.Any(o => orderValidList.Contains(o.Status)))
                .ProjectTo<CampaignOrderViewModel>(_mapper.ConfigurationProvider)
                .ToList();
            if (_campaigns.Any())
            {
                var participantValidList = new List<byte?>()
                {
                    (byte)ParticipantStatus.Accepted,
                    (byte)ParticipantStatus.Approved
                };
                _campaigns = _campaigns.Where(c =>
                c.Participants.Any(p => participantValidList.Contains(p.Status)))
                .ToList();
                foreach (var timeLine in timeLines)
                {
                    if (result.Models.Count() != request.SizeSubject)
                    {
                        var temp = _campaigns.Where(c =>
                        ((DateTime)c.CreatedDate).IsInsideIn((DateTime)c.CreatedDate,
                        (DateTime)timeLine.StartDate,
                        (DateTime)timeLine.EndDate)).ToList();
                        if (temp.Any())
                        {
                            var list = GetRevenuesByCampaign(_campaigns, request.SizeData, request.SizeSubject, request.IsDescendingData);
                            if (list != null)
                            {
                                list.ForEach(item => item.timeLine = timeLine);
                                result.Models.AddRange(list);
                            }
                        }
                    }
                }
                if (result.Models.Any())
                {
                    result.UpdateTotal(request.IsDescendingData, request.IsDescendingTimeLine);
                    return result;
                }
            }
            return null;
        }
        #endregion

        #region Customers
        public DashboardViewModel<CustomerOrdersViewModel> GetTopSpendingCustomers(DashboardRequestModel request)
        {
            request.SetDescendingTimeLine();
            request.SetSeparateDay();
            var timeLines = ValidateTimeLine(request.TimeLine, ErrorMessageConstants.CUSTOMER_SPENDING_ORDERS, request.IsDescendingTimeLine, request.SeparateDay);
            if (timeLines == null)
                return null;
            var result = new DashboardViewModel<CustomerOrdersViewModel>()
            {
                models = new List<SubDashboardViewModel<CustomerOrdersViewModel>>()
            };
            var _customers = new List<CustomerOrdersViewModel>();
            var _guest = new List<OrderViewModel>();
            var IsIssuer = ServiceUtils.CheckRole(_httpContextAccessor, BoekRole.Issuer);
            if (IsIssuer)
            {
                var IssuerId = ServiceUtils.GetUserInfo(_httpContextAccessor);
                _customers = _unitOfWork.Customers.Get(c => c.Orders.Any(o => o.OrderDetails.Any(od => od.BookProduct.IssuerId.Equals(IssuerId))))
                .ProjectTo<CustomerOrdersViewModel>(_mapper.ConfigurationProvider)
                .ToList();

                _guest = _unitOfWork.Orders.Get(o => o.CustomerId == null &&
                o.OrderDetails.Any(od => od.BookProduct.IssuerId.Equals(IssuerId)))
                .ProjectTo<OrderViewModel>(_mapper.ConfigurationProvider)
                .ToList();
            }
            else
            {
                _customers = _unitOfWork.Customers.Get(c => c.Orders.Any())
                .ProjectTo<CustomerOrdersViewModel>(_mapper.ConfigurationProvider)
                .ToList();

                _guest = _unitOfWork.Orders.Get(o => o.CustomerId == null)
                .ProjectTo<OrderViewModel>(_mapper.ConfigurationProvider)
                .ToList();
            }
            if (_customers.Any())
            {
                foreach (var customer in _customers)
                    customer.Orders = GetTotal(customer.Orders);
            }

            if (_guest.Any())
                _guest = GetTotal(_guest);
            if (_customers.Any() || _guest.Any())
            {
                foreach (var timeLine in timeLines)
                {
                    var subDashboard = new SubDashboardViewModel<CustomerOrdersViewModel>()
                    {
                        timeLine = timeLine
                    };
                    if (_guest.Any())
                    {
                        var temp = _guest.Where(c =>
                        ((DateTime)c.OrderDate).IsInsideIn((DateTime)c.OrderDate,
                        (DateTime)timeLine.StartDate,
                        (DateTime)timeLine.EndDate)).ToList();
                        if (temp.Any())
                        {
                            var _ordersWithCustomerDetail = temp.Where(o => !string.IsNullOrEmpty(o.CustomerName) &&
                            !string.IsNullOrEmpty(o.CustomerPhone) &&
                            !string.IsNullOrEmpty(o.CustomerEmail));
                            if (_ordersWithCustomerDetail.Any())
                            {
                                var customers = new List<CustomerOrdersViewModel>();
                                foreach (var order in _ordersWithCustomerDetail)
                                {
                                    var index = customers.FindIndex(c => c.User.Name.ToLower().Equals(order.CustomerName.ToLower()) &&
                                    c.User.Phone.ToLower().Equals(order.CustomerPhone.ToLower()) &&
                                    c.User.Email.ToLower().Equals(order.CustomerEmail.ToLower()) &&
                                    c.Orders.Any(o => !o.Id.Equals(order.Id)));
                                    if (index > -1)
                                        customers[index].Orders.Add(order);
                                    else
                                    {
                                        customers.Add(new CustomerOrdersViewModel()
                                        {
                                            User = new UserViewModel()
                                            {
                                                Name = order.CustomerName,
                                                Phone = order.CustomerPhone,
                                                Email = order.CustomerEmail
                                            },
                                            Orders = new List<OrderViewModel>() { order }
                                        });
                                    }
                                }
                                var groups = customers.Select(c => new
                                {
                                    total = c.Orders.Sum(o => o.Total),
                                    customer = c
                                }).GroupBy(g => g.total).First().Select(g => g.customer);
                                subDashboard.Data.AddRange(groups);
                            }
                            var _ordersWithoutCustomerDetail = temp.Where(o => string.IsNullOrEmpty(o.CustomerName) &&
                            string.IsNullOrEmpty(o.CustomerPhone) &&
                            string.IsNullOrEmpty(o.CustomerEmail));
                            if (_ordersWithoutCustomerDetail.Any())
                            {
                                foreach (var order in _ordersWithoutCustomerDetail)
                                {
                                    var customer = new CustomerOrdersViewModel()
                                    {
                                        Orders = new List<OrderViewModel>() { order }
                                    };
                                    subDashboard.Data.Add(customer);
                                }
                            }
                        }
                    }
                    if (_customers.Any())
                    {
                        var temp = _customers.Where(c => c.Orders.Any(o =>
                        ((DateTime)o.OrderDate).IsInsideIn((DateTime)o.OrderDate,
                        (DateTime)timeLine.StartDate,
                        (DateTime)timeLine.EndDate))).ToList();
                        if (temp.Any())
                        {
                            var groups = temp.Select(c => new
                            {
                                total = c.Orders.Sum(o => o.Total),
                                customer = c
                            }).GroupBy(g => g.total).First().Select(g => g.customer);
                            subDashboard.Data.AddRange(groups);
                        }
                    }
                    result.models.Add(subDashboard);
                }
            }
            if (result.models.Any())
                result.models.ForEach(m =>
                {
                    if (m.Data.Any())
                    {
                        m.Data.ForEach(d =>
                        {
                            var ShowErrorMessage = d.User == null ? false : d.User.Role.HasValue;
                            d.User = ServiceUtils.GetResponseDetail(d.User, false, ShowErrorMessage);
                            if (d.Orders.Any())
                                d.Orders.ForEach(o => o = ServiceUtils.GetResponseDetail(o));
                            d.UpdateTotal(request.IsDescendingData, request.IsDescendingTimeLine);
                        });
                        var result = !request.IsDescendingTimeLine.HasValue || false.Equals(request.IsDescendingTimeLine);
                        if (request.IsDescendingData.HasValue && result)
                        {
                            if ((bool)request.IsDescendingData)
                                m.Data = m.Data.OrderByDescending(item => item.Total).ToList();
                            else
                                m.Data = m.Data.OrderBy(item => item.Total).ToList();
                        }
                    }
                });
            result.UpdateTotal(request.IsDescendingData, request.IsDescendingTimeLine);
            return result;
        }

        public DashboardViewModel<BookProductOrderDetailsViewModel> GetBestSellerBookProductsByAdmin(DashboardRequestModel request)
        {
            request.SetDefaultSizeData();
            request.SetDescendingTimeLine();
            request.SetSeparateDay();
            var timeLines = ValidateTimeLine(request.TimeLine, ErrorMessageConstants.BOOK_PRODUCT, request.IsDescendingTimeLine, request.SeparateDay);
            if (timeLines == null)
                return null;
            var result = new DashboardViewModel<BookProductOrderDetailsViewModel>()
            {
                models = new List<SubDashboardViewModel<BookProductOrderDetailsViewModel>>()
            };
            var IssuerId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            var _bookProducts = _unitOfWork.BookProducts.Get(bps => bps.OrderDetails.Any())
            .ProjectTo<BookProductOrderDetailsViewModel>(_mapper.ConfigurationProvider)
            .ToList();
            if (_bookProducts.Any())
            {
                foreach (var timeLine in timeLines)
                {
                    var subDashboard = new SubDashboardViewModel<BookProductOrderDetailsViewModel>()
                    {
                        timeLine = timeLine
                    };
                    var temp = _bookProducts.Where(bp =>
                    ((DateTime)bp.CreatedDate).IsInsideIn((DateTime)bp.CreatedDate,
                    (DateTime)timeLine.StartDate,
                    (DateTime)timeLine.EndDate)).Select(bps => new
                    {
                        total = bps.OrderDetails.Sum(od => od.Quantity),
                        bookProduct = bps
                    }).GroupBy(bps => bps.total).ToList();
                    if (temp.Any())
                    {
                        temp.ForEach(bps =>
                        subDashboard.Data.AddRange(bps.Select(bp => bp.bookProduct)));
                    }
                    result.models.Add(subDashboard);
                }
            }
            if (result.models.Any())
            {
                var check = !request.IsDescendingTimeLine.HasValue || false.Equals(request.IsDescendingTimeLine);
                result.models.ForEach(m =>
                {
                    if (m.Data.Any())
                    {
                        m.Data.ForEach(d =>
                        {
                            d = ServiceUtils.GetResponseDetail(d);
                            d.UpdateTotal(request.IsDescendingData, request.IsDescendingTimeLine);
                        });
                        if (request.IsDescendingData.HasValue && check)
                        {
                            if ((bool)request.IsDescendingData)
                                m.Data = m.Data.OrderByDescending(item => item.Total).ToList();
                            else
                                m.Data = m.Data.OrderBy(item => item.Total).ToList();
                        }
                        m.Data = m.Data.Take((int)request.SizeData).ToList();
                    }
                });
            }
            result.UpdateTotal(request.IsDescendingData, request.IsDescendingTimeLine);
            return result;
        }
        public DashboardViewModel<BookProductOrderDetailsViewModel> GetBestSellerBookProductsByIssuer(DashboardRequestModel request)
        {
            request.SetDefaultSizeData();
            request.SetDescendingTimeLine();
            request.SetSeparateDay();
            var timeLines = ValidateTimeLine(request.TimeLine, ErrorMessageConstants.BOOK_PRODUCT, request.IsDescendingTimeLine, request.SeparateDay);
            if (timeLines == null)
                return null;
            var result = new DashboardViewModel<BookProductOrderDetailsViewModel>()
            {
                models = new List<SubDashboardViewModel<BookProductOrderDetailsViewModel>>()
            };
            var IssuerId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            var _bookProducts = _unitOfWork.BookProducts.Get(bps => bps.IssuerId.Equals(IssuerId) && bps.OrderDetails.Any())
            .ProjectTo<BookProductOrderDetailsViewModel>(_mapper.ConfigurationProvider)
            .ToList();
            if (_bookProducts.Any())
            {
                foreach (var timeLine in timeLines)
                {
                    var subDashboard = new SubDashboardViewModel<BookProductOrderDetailsViewModel>()
                    {
                        timeLine = timeLine
                    };
                    var temp = _bookProducts.Where(bp =>
                    ((DateTime)bp.CreatedDate).IsInsideIn((DateTime)bp.CreatedDate,
                    (DateTime)timeLine.StartDate,
                    (DateTime)timeLine.EndDate)).Select(bps => new
                    {
                        total = bps.OrderDetails.Sum(od => od.Quantity),
                        bookProduct = bps
                    }).GroupBy(bps => bps.total).ToList();
                    if (temp.Any())
                    {
                        temp.Take((int)request.SizeData).ToList().ForEach(bps =>
                        subDashboard.Data.AddRange(bps.Select(bp => bp.bookProduct)));
                    }
                    result.models.Add(subDashboard);
                }
            }
            if (result.models.Any())
            {
                var check = !request.IsDescendingTimeLine.HasValue || false.Equals(request.IsDescendingTimeLine);
                result.models.ForEach(m =>
                {
                    if (m.Data.Any())
                    {
                        m.Data.ForEach(d =>
                        {
                            d = ServiceUtils.GetResponseDetail(d);
                            d.UpdateTotal(request.IsDescendingData, request.IsDescendingTimeLine);
                        });
                        if (request.IsDescendingData.HasValue && check)
                        {
                            if ((bool)request.IsDescendingData)
                                m.Data = m.Data.OrderByDescending(item => item.Total).ToList();
                            else
                                m.Data = m.Data.OrderBy(item => item.Total).ToList();
                        }
                    }
                });
            }
            result.UpdateTotal(request.IsDescendingData, request.IsDescendingTimeLine);
            return result;
        }
        #endregion

        #region Utils
        private List<TimeLineViewModel> ValidateTimeLine(List<TimeLineRequestModel> timeLines, string Subject, bool? IsDescendingTimeLine = false, bool? SeparateDay = false, bool Comparison = false)
        {
            if (timeLines == null)
                return null;
            if (timeLines.Any())
            {
                if (Comparison && timeLines.Count() < 2)
                    return null;
                if (!Comparison && timeLines.Count() >= 2)
                    return null;
                var result = new List<TimeLineViewModel>();
                foreach (var timeLine in timeLines)
                {
                    CurrentTimeLine? currentTime = null;
                    if (timeLine.IsEmptyTimeDetail())
                    {
                        currentTime = timeLine.Type.Equals((byte)TimeLineType.Day) ? CurrentTimeLine.Today :
                        timeLine.Type.Equals((byte)TimeLineType.Week) ? CurrentTimeLine.CurrentWeek :
                        timeLine.Type.Equals((byte)TimeLineType.Month) ? CurrentTimeLine.CurrentMonth :
                        timeLine.Type.Equals((byte)TimeLineType.Season) ? CurrentTimeLine.CurrentSeason : CurrentTimeLine.CurrentYear;
                    }
                    var temp = ConvertTimeLine(timeLine, currentTime, SeparateDay);
                    if (temp != null)
                        result.AddRange(temp);
                    else
                        return null;
                }
                if (result.Any())
                {
                    if ((bool)IsDescendingTimeLine)
                        result = result.OrderByDescending(item => item.StartDate).ToList();
                    else
                        result = result.OrderBy(item => item.StartDate).ToList();
                    for (int i = 0; i < result.Count(); i++)
                    {
                        result[i].Id = i;
                        result[i].SetTitle(Subject);
                    }
                    return result;
                }
            }
            return null;
        }

        private List<TimeLineViewModel> ConvertTimeLine(TimeLineRequestModel timeLine, CurrentTimeLine? currentTime = null, bool? SeparateDay = false)
        {
            var result = new List<TimeLineViewModel>();
            if (timeLine.Type.HasValue)
            {
                switch (timeLine.Type)
                {
                    case (byte)TimeLineType.Day:
                        result = ConvertDateTimeLine(timeLine, currentTime, SeparateDay);
                        break;
                    case (byte)TimeLineType.Week:
                        result = ConvertWeekTimeLine(timeLine, currentTime, SeparateDay);
                        break;
                    case (byte)TimeLineType.Month:
                        result = ConvertMonthTimeLine(timeLine, currentTime, SeparateDay);
                        break;
                    case (byte)TimeLineType.Season:
                        result = ConvertSeasonTimeLine(timeLine, currentTime, SeparateDay);
                        break;
                    case (byte)TimeLineType.Year:
                        result.Add(ConvertYearTimeLine(timeLine, currentTime));
                        break;
                    default:
                        break;
                }
            }
            return result;
        }

        private List<TimeLineViewModel> ConvertDateTimeLine(TimeLineRequestModel timeLine, CurrentTimeLine? currentTime = null, bool? SeparateDay = false)
        {
            var result = new List<TimeLineViewModel>();
            var temp = new TimeLineViewModel();
            if (currentTime.HasValue)
            {
                if (currentTime.Equals(CurrentTimeLine.Today))
                {
                    temp.StartDate = DateTime.Now.Date;
                    temp.EndDate = DateTime.Now.AddDays(1).Date;
                    result.Add(temp);
                }
            }
            else
            {
                temp = ConvertOtherTimeLine(timeLine);
                if (temp != null)
                {
                    if ((bool)SeparateDay &&
                    (((DateTime)timeLine.EndDate).Date - ((DateTime)timeLine.StartDate).Date).Days > 1)
                    {
                        DateTime? tempStartDate = timeLine.StartDate;
                        var flag = true;
                        while (flag)
                        {
                            var EndDate = ((DateTime)tempStartDate).Date.AddDays(1);
                            if ((((DateTime)timeLine.EndDate).Date - (((DateTime)tempStartDate)).Date).Days == 1)
                            {
                                result.Add(new TimeLineViewModel()
                                {
                                    Id = temp.Id,
                                    StartDate = tempStartDate,
                                    EndDate = timeLine.EndDate,
                                    Type = (byte)TimeLineType.Day,
                                    Year = ((DateTime)tempStartDate).Year,
                                    TimeLength = temp.TimeLength,
                                    SeasonType = temp.SeasonType
                                });
                                flag = false;
                            }
                            else
                            {
                                result.Add(new TimeLineViewModel()
                                {
                                    Id = temp.Id,
                                    StartDate = tempStartDate,
                                    EndDate = EndDate,
                                    Type = (byte)TimeLineType.Day,
                                    Year = ((DateTime)tempStartDate).Year,
                                    TimeLength = temp.TimeLength,
                                    SeasonType = temp.SeasonType
                                });
                                tempStartDate = EndDate;
                            }
                        }
                    }
                    else
                        result.Add(temp);
                }
            }
            return result;
        }
        private List<TimeLineViewModel> ConvertWeekTimeLine(TimeLineRequestModel timeLine, CurrentTimeLine? currentTime = null, bool? SeparateDay = false)
        {
            var result = new List<TimeLineViewModel>();
            var temp = new TimeLineViewModel() { Type = (byte)TimeLineType.Week };
            if (currentTime.HasValue)
            {
                if (currentTime.Equals(CurrentTimeLine.CurrentWeek))
                {
                    var DateTimeNow = DateTime.Now.Date;
                    temp.StartDate = DateTimeNow.LastDayOfWeek(DayOfWeek.Monday);
                    temp.EndDate = ((DateTime)temp.StartDate).AddDays(6);
                    result.Add(temp);
                }
            }
            else
            {
                temp = ConvertOtherTimeLine(timeLine);
                if (temp != null)
                {
                    if ((bool)SeparateDay &&
                    (((DateTime)timeLine.EndDate).Date - ((DateTime)timeLine.StartDate).Date).Days > 7)
                    {
                        DateTime tempStartDate = ((DateTime)timeLine.StartDate);
                        DateTime tempEndDate = ((DateTime)timeLine.StartDate).Date.LastDayOfWeek(DayOfWeek.Sunday).AddDays(7);
                        if (tempEndDate.Equals(((DateTime)timeLine.StartDate).Date))
                            tempEndDate = tempEndDate.AddDays(1).AddTicks(-1);
                        var flag = true;
                        while (flag)
                        {
                            if (((DateTime)timeLine.EndDate).Date.IsInsideIn(((DateTime)timeLine.EndDate).Date, tempStartDate, tempEndDate))
                            {
                                var lastTimeLineTemp = temp;
                                lastTimeLineTemp.StartDate = tempStartDate;
                                lastTimeLineTemp.EndDate = timeLine.EndDate;
                                result.Add(lastTimeLineTemp);
                                flag = false;
                            }
                            else
                            {
                                var timeLineTemp = temp;
                                timeLineTemp.StartDate = tempStartDate;
                                timeLineTemp.EndDate = tempEndDate;
                                result.Add(timeLineTemp);
                                tempStartDate = tempEndDate.Date.AddDays(1);
                                tempEndDate = tempStartDate.LastDayOfWeek(DayOfWeek.Sunday).AddDays(8).AddTicks(-1);
                            }
                        }
                    }
                    else
                        result.Add(temp);
                }
            }
            return result;
        }
        private List<TimeLineViewModel> ConvertMonthTimeLine(TimeLineRequestModel timeLine, CurrentTimeLine? currentTime = null, bool? SeparateDay = false)
        {
            var result = new List<TimeLineViewModel>();
            var temp = new TimeLineViewModel() { Type = (byte)TimeLineType.Month };
            if (currentTime.HasValue)
            {
                if (currentTime.Equals(CurrentTimeLine.CurrentMonth))
                {
                    var DateTimeNow = DateTime.Now.Date;
                    temp.StartDate = DateTimeNow.FirstDayOfTheMonth().Date;
                    temp.EndDate = DateTimeNow.LastDayOfTheMonth().Date.AddDays(1).AddTicks(-1);
                }
            }
            else
            {
                temp = ConvertOtherTimeLine(timeLine);
                if (temp != null)
                {
                    temp.Type = (byte)TimeLineType.Month;
                    if ((bool)SeparateDay && ((DateTime)temp.StartDate).Month != ((DateTime)temp.EndDate).Month)
                    {
                        var year = ((DateTime)temp.StartDate).Year;
                        var list = new Dictionary<int, List<DateTime?>>()
                        {
                            {((DateTime)temp.StartDate).Month, new List<DateTime?>(){temp.StartDate, SetFirstMonth((DateTime)temp.StartDate)}}
                        };
                        if ((((DateTime)temp.EndDate).Month - ((DateTime)temp.StartDate).Month) == 1)
                            list.Add(((DateTime)temp.EndDate).Month, new List<DateTime?>() { SetLastMonth((DateTime)temp.EndDate), temp.EndDate });
                        else
                        {
                            int tempStartMonth = ((DateTime)temp.StartDate).Month + 1;
                            var flag = true;
                            while (flag)
                            {
                                if (tempStartMonth < ((DateTime)temp.EndDate).Month)
                                {
                                    var startDate = new DateTime(year, tempStartMonth, 01);
                                    var endDate = startDate.LastDayOfTheMonth().AddDays(1).AddTicks(-1);
                                    list.Add(tempStartMonth, new List<DateTime?>() { startDate, endDate });
                                    tempStartMonth += 1;
                                }
                                else
                                {
                                    list.Add(((DateTime)temp.EndDate).Month, new List<DateTime?>() { SetLastMonth((DateTime)temp.EndDate), temp.EndDate });
                                    flag = false;
                                }
                            }
                        }
                        if (list.Any())
                        {
                            list.ToList().ForEach(item =>
                            {
                                result.Add(new TimeLineViewModel()
                                {
                                    Type = (byte)TimeLineType.Month,
                                    StartDate = item.Value.First(),
                                    EndDate = item.Value.Last(),
                                    TimeLength = temp.TimeLength,
                                    SeasonType = temp.SeasonType,
                                    Year = year
                                });
                            });
                        }
                    }
                    else
                        result.Add(temp);
                }
            }
            return result;
        }
        private List<TimeLineViewModel> ConvertSeasonTimeLine(TimeLineRequestModel timeLine, CurrentTimeLine? currentTime = null, bool? SeparateDay = false)
        {
            var result = new List<TimeLineViewModel>();
            var temp = new TimeLineViewModel();
            if (currentTime.HasValue)
            {
                if (currentTime.Equals(CurrentTimeLine.CurrentSeason))
                {
                    temp = GetDateTimeBySeason(null, DateTime.Now.Date);
                    result.Add(temp);
                }
            }
            else
            {
                if ((bool)SeparateDay && IsSeparateSeason(timeLine))
                {
                    var timeLineTemp = timeLine;
                    var firstSeason = GetSeasonData(((DateTime)timeLine.StartDate).Month);
                    if (firstSeason != null)
                    {
                        timeLineTemp.SeasonType = firstSeason;
                        timeLineTemp.Year = ((DateTime)timeLine.StartDate).Year;
                        temp = GetDateTimeBySeason(timeLineTemp);
                        result.Add(temp);

                        var flag = true;
                        var endDate = ((DateTime)timeLine.EndDate).Date;
                        while (flag)
                        {
                            var tempStartDate = ((DateTime)temp.EndDate).Date.AddDays(1);
                            firstSeason = GetSeasonData(tempStartDate.Month);
                            if (firstSeason != null)
                            {
                                timeLineTemp.SeasonType = firstSeason;
                                timeLineTemp.Year = tempStartDate.Year;
                                temp = GetDateTimeBySeason(timeLineTemp);
                                result.Add(temp);
                                if (endDate.IsInsideIn(endDate, (DateTime)temp.StartDate, (DateTime)temp.EndDate))
                                    flag = false;
                            }
                        }
                    }
                }
                else
                {
                    temp = GetDateTimeBySeason(timeLine);
                    result.Add(temp);
                }
            }
            return result;
        }

        private byte? GetSeasonData(int? Month)
        {
            byte? result = null;
            if (Month >= 1 && Month <= 3)
                result = (byte)SeasonTimeLine.Spring;
            if (Month >= 4 && Month <= 6)
                result = (byte)SeasonTimeLine.Summer;
            if (Month >= 7 && Month <= 9)
                result = (byte)SeasonTimeLine.Fall;
            if (Month >= 10 && Month <= 12)
                result = (byte)SeasonTimeLine.Winter;
            return result;
        }

        private bool IsSeparateSeason(TimeLineRequestModel timeLine)
        {
            var result = ConvertOtherTimeLineDetail(timeLine);
            if (result != null)
            {
                if (((DateTime)result.StartDate).Year != ((DateTime)result.EndDate).Year)
                    return true;
                if ((((DateTime)timeLine.EndDate).Month - ((DateTime)timeLine.StartDate).Month) > 2)
                    return true;
            }
            return false;
        }

        private TimeLineViewModel GetDateTimeBySeason(TimeLineRequestModel timeLine = null, DateTime? value = null)
        {
            var result = new TimeLineViewModel() { Type = (byte)TimeLineType.Season };
            if (timeLine == null)
            {
                var temp = (DateTime)value;
                if (temp.IsInsideIn(temp, TimeLineConstants.STRING_START_MONTH, TimeLineConstants.STRING_END_MONTH))
                {
                    result.StartDate = TimeLineConstants.STRING_START_MONTH;
                    result.EndDate = TimeLineConstants.STRING_END_MONTH;
                    result.SeasonType = (byte)SeasonTimeLine.Spring;
                }
                if (temp.IsInsideIn(temp, TimeLineConstants.SUMMER_START_MONTH, TimeLineConstants.SUMMER_END_MONTH))
                {
                    result.StartDate = TimeLineConstants.SUMMER_START_MONTH;
                    result.EndDate = TimeLineConstants.SUMMER_END_MONTH;
                    result.SeasonType = (byte)SeasonTimeLine.Summer;
                }
                if (temp.IsInsideIn(temp, TimeLineConstants.FALL_START_MONTH, TimeLineConstants.FALL_END_MONTH))
                {
                    result.StartDate = TimeLineConstants.FALL_START_MONTH;
                    result.EndDate = TimeLineConstants.FALL_END_MONTH;
                    result.SeasonType = (byte)SeasonTimeLine.Fall;
                }
                if (temp.IsInsideIn(temp, TimeLineConstants.WINTER_START_MONTH, TimeLineConstants.WINTER_END_MONTH))
                {
                    result.StartDate = TimeLineConstants.WINTER_START_MONTH;
                    result.EndDate = TimeLineConstants.WINTER_END_MONTH;
                    result.SeasonType = (byte)SeasonTimeLine.Winter;
                }
            }
            else
            {
                if (timeLine.SeasonType.HasValue)
                {
                    result = _mapper.Map<TimeLineViewModel>(timeLine);
                    switch (timeLine.SeasonType)
                    {
                        case (byte)SeasonTimeLine.Spring:
                            result.StartDate = TimeLineConstants.STRING_START_MONTH;
                            result.EndDate = TimeLineConstants.STRING_END_MONTH;
                            result.SeasonType = (byte)SeasonTimeLine.Spring;
                            break;
                        case (byte)SeasonTimeLine.Summer:
                            result.StartDate = TimeLineConstants.SUMMER_START_MONTH;
                            result.EndDate = TimeLineConstants.SUMMER_END_MONTH;
                            result.SeasonType = (byte)SeasonTimeLine.Summer;
                            break;
                        case (byte)SeasonTimeLine.Fall:
                            result.StartDate = TimeLineConstants.FALL_START_MONTH;
                            result.EndDate = TimeLineConstants.FALL_END_MONTH;
                            result.SeasonType = (byte)SeasonTimeLine.Fall;
                            break;
                        case (byte)SeasonTimeLine.Winter:
                            result.StartDate = TimeLineConstants.WINTER_START_MONTH;
                            result.EndDate = TimeLineConstants.WINTER_END_MONTH;
                            result.SeasonType = (byte)SeasonTimeLine.Winter;
                            break;
                    }
                    result.ChangeYear();
                }
                else
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.PreconditionRequired), new string[]{
                        MessageConstants.MESSAGE_REQUIRED,
                        ErrorMessageConstants.DASHBOARD_SEASON_TYPE
                    });
            }
            return result;
        }

        private TimeLineViewModel ConvertYearTimeLine(TimeLineRequestModel timeLine, CurrentTimeLine? currentTime = null)
        {
            var result = new TimeLineViewModel() { Type = (byte)TimeLineType.Year };
            if (currentTime.HasValue)
            {
                if (currentTime.Equals(CurrentTimeLine.CurrentYear))
                {
                    result.StartDate = TimeLineConstants.STRING_START_MONTH;
                    result.EndDate = TimeLineConstants.WINTER_END_MONTH;
                }
            }
            else
                result = ConvertOtherTimeLine(timeLine);
            return result;
        }

        private TimeLineViewModel ConvertOtherTimeLine(TimeLineRequestModel timeLine)
        {
            var result = new TimeLineViewModel();
            if (timeLine.Type.Equals((byte)TimeLineType.Year))
            {
                result = _mapper.Map<TimeLineViewModel>(timeLine);
                result.TimeLength = null;
                result.SeasonType = null;
                result.StartDate = TimeLineConstants.STRING_START_MONTH;
                result.EndDate = TimeLineConstants.WINTER_END_MONTH;
                result.ChangeYear();
            }
            else
                result = ConvertOtherTimeLineDetail(timeLine);
            return result;
        }

        private TimeLineViewModel ConvertOtherTimeLineDetail(TimeLineRequestModel timeLine)
        {
            TimeLineViewModel result = null;
            if (timeLine.IsNotEmptyStartAndEndTimeDetail())
            {
                if (timeLine.IsValidDateTime())
                {
                    if (timeLine.IsRightOrderDate())
                        result = _mapper.Map<TimeLineViewModel>(timeLine);
                    else
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                        {
                            ErrorMessageConstants.CAMPAIGN_START_DATE,
                            ErrorMessageConstants.CAMPAIGN_NOT_LATER_START_DATE,
                            ErrorMessageConstants.CAMPAIGN_END_DATE
                        });
                }
                else
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                    {
                        ErrorMessageConstants.DASHBOARD_DATE_OR_TIME_LENGTH,
                        MessageConstants.MESSAGE_INVALID
                    });
            }
            else if (timeLine.IsNotEmptyStartTimeDetail())
            {
                if (timeLine.IsValidDateTime(true) && timeLine.IsValidTimeLength())
                {
                    result = _mapper.Map<TimeLineViewModel>(timeLine);
                    result.ConvertDecimalToIntTimeLength();
                    result.EndDate = ((DateTime)result.StartDate).AddDays((((int)result.TimeLength)));
                }
                else
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                    {
                        ErrorMessageConstants.DASHBOARD_DATE_OR_TIME_LENGTH,
                        MessageConstants.MESSAGE_INVALID
                    });
            }
            else if (timeLine.IsNotEmptyEndTimeDetail())
            {
                if (timeLine.IsValidDateTime(false) && timeLine.IsValidTimeLength())
                {
                    result = _mapper.Map<TimeLineViewModel>(timeLine);
                    result.ConvertDecimalToIntTimeLength();
                    result.StartDate = ((DateTime)result.EndDate).AddDays(-((int)result.TimeLength));
                }
                else
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                    {
                        ErrorMessageConstants.DASHBOARD_DATE_OR_TIME_LENGTH,
                        MessageConstants.MESSAGE_INVALID
                    });
            }
            return result;
        }

        private List<OrderViewModel> GetTotal(List<OrderViewModel> orders)
        {
            if (orders.Any())
            {
                orders.ForEach(order =>
                {
                    if (order.OrderDetails != null)
                    {
                        if (order.OrderDetails.Any())
                            order = ServiceUtils.GetTotal(order, _mapper, _unitOfWork);
                    }
                });
            }
            return orders;
        }
        private List<OrdersViewModel> GetTotal(List<OrdersViewModel> orders)
        {
            if (orders.Any())
            {
                orders.ForEach(order =>
                {
                    if (order.OrderDetails != null)
                    {
                        if (order.OrderDetails.Any())
                        {
                            order = ServiceUtils.GetTotal(order, _mapper, _unitOfWork);
                        }
                    }
                });
            }
            return orders;
        }

        private SubRevenueListViewModel<BasicCampaignViewModel> GetRevenues(List<CampaignOrderViewModel> _campaigns, int? Size = null, bool? IsDescendingData = true)
        {
            var list = new SubRevenueListViewModel<BasicCampaignViewModel>()
            {
                Data = new List<RevenueViewModel<BasicCampaignViewModel>>()
            };
            var validStatus = new List<byte?>()
            {
                (byte)OrderStatus.Shipped,
                (byte)OrderStatus.Received
            };
            _campaigns.ForEach(c =>
            {
                var orders = c.Orders.Where(o => validStatus.Contains(o.Status)).ToList();
                if (orders.Any())
                {
                    var _orders = _mapper.Map<List<OrderViewModel>>(orders);
                    _orders = GetTotal(_orders);
                    var Revenue = _orders.Sum(o => o.Total);
                    if (Revenue > 0)
                    {
                        var campaign = _mapper.Map<BasicCampaignViewModel>(c);
                        list.Data.Add(new RevenueViewModel<BasicCampaignViewModel>()
                        {
                            Data = campaign,
                            Total = Revenue
                        });
                    }
                }
            });
            if (list.Data.Any())
            {
                list.UpdateData(Size, IsDescendingData);
                return list;
            }
            return null;
        }

        private List<RevenuesViewModel<BasicCampaignViewModel, IssuerViewModel>> GetRevenues(List<CampaignOrderViewModel> _campaigns, int? SizeData = null, int? SizeSubject = null, bool? IsDescendingData = true)
        {
            var list = new List<RevenuesViewModel<BasicCampaignViewModel, IssuerViewModel>>();
            _campaigns.ForEach(c =>
            {
                var issuers = c.Participants.Select(p => p.Issuer).Distinct().ToList();
                if (issuers.Any())
                {
                    var issuerIds = issuers.Select(p => p.Id).ToList();
                    var orderGroups = new Dictionary<IssuerViewModel, List<OrdersViewModel>>();
                    issuerIds.ForEach(i =>
                    {
                        var orders = c.Orders.Where(o => o.OrderDetails.Any(od => i.Equals(od.BookProduct.IssuerId))).ToList();
                        if (orders.Any())
                        {
                            var issuer = issuers.SingleOrDefault(item => item.Id.Equals(i));
                            if (issuer != null)
                                orderGroups.Add(issuer, orders);
                        }
                    });
                    if (!orderGroups.Equals(default(KeyValuePair<IssuerViewModel, List<OrdersViewModel>>)))
                    {
                        var issuerRevenue = new RevenuesViewModel<BasicCampaignViewModel, IssuerViewModel>()
                        {
                            Model = new SubDashboardRevenueViewModel<IssuerViewModel>()
                            {
                                Data = new List<RevenueViewModel<IssuerViewModel>>()
                            }
                        };
                        foreach (var orders in orderGroups)
                        {
                            var _orders = _mapper.Map<List<OrderViewModel>>(orders.Value);
                            _orders = GetTotal(_orders);
                            var Revenue = _orders.Sum(o => o.Total);
                            if (Revenue > 0)
                            {
                                var issuer = orders.Key;
                                issuerRevenue.Model.Data.Add(new RevenueViewModel<IssuerViewModel>()
                                {
                                    Data = issuer,
                                    Total = Revenue
                                });
                            }
                        }
                        if (issuerRevenue.Model.Data.Any())
                        {
                            var campaign = _mapper.Map<BasicCampaignViewModel>(c);
                            issuerRevenue.Subject = campaign;
                            list.Add(issuerRevenue);
                        }
                    }
                }
            });
            if (list.Any())
            {
                if (SizeSubject.HasValue && SizeSubject > 0)
                    list = list.Take((int)SizeSubject).ToList();
                if (SizeData.HasValue && SizeData > 0)
                    list.ForEach(item =>
                    item.Model.Data = item.Model.Data.Take((int)SizeData).ToList());
                if ((bool)IsDescendingData)
                {
                    list.ForEach(item =>
                    {
                        item.Model.Data =
                        item.Model.Data.OrderByDescending(issuer => issuer.Total).ToList();
                    });
                    list.OrderByDescending(item => item.Model.Data.Max(d => d.Total));
                }
                else
                {
                    list.ForEach(item =>
                    {
                        item.Model.Data =
                        item.Model.Data.OrderBy(issuer => issuer.Total).ToList();
                    });
                    list.OrderBy(item => item.Model.Data.Max(d => d.Total));
                }
                return list;
            }
            return null;
        }
        private List<RevenuesViewModel<IssuerViewModel, BasicCampaignViewModel>> GetRevenuesByCampaign(List<CampaignOrderViewModel> _campaigns, int? SizeData = null, int? SizeSubject = null, bool? IsDescendingData = true)
        {
            var list = new List<RevenuesViewModel<IssuerViewModel, BasicCampaignViewModel>>();
            var issuers = _campaigns.SelectMany(c => c.Participants).Select(p => p.Issuer).Distinct().ToList();
            if (issuers.Any())
            {
                var orderGroups = new Dictionary<IssuerViewModel, List<CampaignOrderViewModel>>();
                issuers.ForEach(i =>
                {
                    var campaigns = _campaigns.Where(c =>
                    c.Orders.Any(o =>
                    o.OrderDetails.Any(od =>
                    i.Id.Equals(od.BookProduct.IssuerId)))).ToList();
                    if (campaigns.Any())
                        orderGroups.Add(i, campaigns);
                });
                if (!orderGroups.Equals(default(KeyValuePair<IssuerViewModel, List<CampaignOrderViewModel>>)))
                {
                    orderGroups.ToList().ForEach(og =>
                    {
                        var campaignList = new SubDashboardRevenueViewModel<BasicCampaignViewModel>()
                        {
                            Data = new List<RevenueViewModel<BasicCampaignViewModel>>()
                        };
                        og.Value.ForEach(c =>
                        {
                            var orders = c.Orders;
                            if (orders.Any())
                            {
                                var _orders = _mapper.Map<List<OrderViewModel>>(orders);
                                _orders = GetTotal(_orders);
                                var Revenue = _orders.Sum(o => o.Total);
                                if (Revenue > 0)
                                {
                                    var campaign = _mapper.Map<BasicCampaignViewModel>(c);
                                    campaignList.Data.Add(new RevenueViewModel<BasicCampaignViewModel>()
                                    {
                                        Data = campaign,
                                        Total = Revenue
                                    });
                                }
                            }
                        });
                        if (campaignList.Data.Any())
                        {
                            list.Add(new RevenuesViewModel<IssuerViewModel, BasicCampaignViewModel>()
                            {
                                Subject = og.Key,
                                Model = campaignList
                            });
                        }
                    });
                }

            }
            if (list.Any())
            {
                if (SizeSubject.HasValue && SizeSubject > 0)
                    list = list.Take((int)SizeSubject).ToList();
                if (SizeData.HasValue && SizeData > 0)
                    list.ForEach(item =>
                    item.Model.Data = item.Model.Data.Take((int)SizeData).ToList());
                if ((bool)IsDescendingData)
                {
                    list.ForEach(item =>
                    {
                        item.Model.Data =
                        item.Model.Data.OrderByDescending(issuer => issuer.Total).ToList();
                    });
                    list.OrderByDescending(item => item.Model.Data.Max(d => d.Total));
                }
                else
                {
                    list.ForEach(item =>
                    {
                        item.Model.Data =
                        item.Model.Data.OrderBy(issuer => issuer.Total).ToList();
                    });
                    list.OrderBy(item => item.Model.Data.Max(d => d.Total));
                }
                return list;
            }
            return null;
        }

        #region Campaign

        #endregion
        private List<TimeLineViewModel> ValidateTimeLine(List<TimeLineRequestModel> timeLines, Campaign campaign, string Subject, bool Comparison = true, bool IsAllTheTime = false, bool SeparateDay = false)
        {
            if (timeLines == null)
                return null;
            if (timeLines.Any())
            {
                if (Comparison && timeLines.Count() < 2)
                    return null;
                if (!Comparison && timeLines.Count() >= 2)
                    return null;
                var result = new List<TimeLineViewModel>();
                foreach (var timeLine in timeLines)
                {
                    TimeLineRequestModel timeLineTemp = timeLine;
                    CurrentTimeLine? currentTime = null;
                    if (IsAllTheTime)
                        timeLineTemp = GetAllTheTimeCampaignRequestTimeLine(campaign);
                    else
                    {
                        if (timeLine.IsEmptyTimeDetail())
                        {
                            var today = DateTime.Now.Date;
                            if (today.IsInsideIn(today, (DateTime)campaign.StartDate, (DateTime)campaign.EndDate))
                            {
                                timeLineTemp = new TimeLineRequestModel()
                                {
                                    Type = (byte)TimeLineType.Day,
                                    Year = DateTime.Now.Year
                                };
                                currentTime = CurrentTimeLine.Today;
                            }
                            else
                                timeLineTemp = GetAllTheTimeCampaignRequestTimeLine(campaign);
                        }
                    }
                    if (timeLineTemp == null)
                        return null;
                    var temp = ConvertCampaignTimeLine(timeLineTemp, currentTime, SeparateDay);
                    if (temp != null)
                        result.AddRange(temp);
                    else
                        return null;
                }
                if (result.Any())
                {
                    for (int i = 0; i < result.Count(); i++)
                    {
                        result[i].Id = i;
                        result[i].SetTitle(Subject);
                    }
                    return result;
                }
            }
            return null;
        }

        private TimeLineRequestModel GetAllTheTimeCampaignRequestTimeLine(Campaign campaign)
        {
            TimeLineRequestModel timeLineTemp = null;
            int Length = ((DateTime)campaign.EndDate - (DateTime)campaign.StartDate).Days;
            if (Length > 0)
            {
                if (Length <= 30)
                {
                    timeLineTemp = new TimeLineRequestModel()
                    {
                        Type = (byte)TimeLineType.Day,
                        StartDate = campaign.StartDate,
                        EndDate = campaign.EndDate,
                        Year = ((DateTime)campaign.EndDate).Year
                    };
                }
                else
                {
                    timeLineTemp = new TimeLineRequestModel()
                    {
                        Type = (byte)TimeLineType.Month,
                        StartDate = campaign.StartDate,
                        EndDate = campaign.EndDate,
                        Year = ((DateTime)campaign.EndDate).Year
                    };
                }
            }
            return timeLineTemp;
        }

        private List<TimeLineViewModel> ConvertCampaignTimeLine(TimeLineRequestModel timeLine, CurrentTimeLine? currentTime = null, bool SeparateDay = false)
        {
            var result = new List<TimeLineViewModel>();
            if (timeLine.Type.HasValue)
            {
                switch (timeLine.Type)
                {
                    case (byte)TimeLineType.Day:
                        result = ConvertCampaignDateTimeLine(timeLine, currentTime, SeparateDay);
                        break;
                    case (byte)TimeLineType.Month:
                        result = ConvertCampaignMonthTimeLine(timeLine, SeparateDay);
                        break;
                    default:
                        break;
                }
            }
            return result;
        }

        private List<TimeLineViewModel> ConvertCampaignDateTimeLine(TimeLineRequestModel timeLine, CurrentTimeLine? currentTime = null, bool SeparateDay = false)
        {
            var result = new List<TimeLineViewModel>();
            if (currentTime.HasValue)
            {
                if (currentTime.Equals(CurrentTimeLine.Today))
                {
                    var temp = new TimeLineViewModel()
                    {
                        StartDate = DateTime.Now.Date,
                        EndDate = DateTime.Now.AddDays(1).Date,
                        Type = (byte)TimeLineType.Day
                    };
                    result.Add(temp);
                }
            }
            else
            {
                var temp = ConvertOtherTimeLine(timeLine);
                if (temp != null)
                {
                    if (SeparateDay &&
                    (((DateTime)timeLine.EndDate).Date - ((DateTime)timeLine.StartDate).Date).Days > 1)
                    {
                        DateTime? tempStartDate = timeLine.StartDate;
                        var flag = true;
                        while (flag)
                        {
                            var EndDate = ((DateTime)tempStartDate).Date.AddDays(1);
                            if ((((DateTime)timeLine.EndDate).Date - (((DateTime)tempStartDate)).Date).Days == 1)
                            {
                                result.Add(new TimeLineViewModel()
                                {
                                    Id = temp.Id,
                                    StartDate = tempStartDate,
                                    EndDate = timeLine.EndDate,
                                    Type = (byte)TimeLineType.Day,
                                    Year = ((DateTime)tempStartDate).Year,
                                    TimeLength = temp.TimeLength,
                                    SeasonType = temp.SeasonType
                                });
                                flag = false;
                            }
                            else
                            {
                                result.Add(new TimeLineViewModel()
                                {
                                    Id = temp.Id,
                                    StartDate = tempStartDate,
                                    EndDate = EndDate,
                                    Type = (byte)TimeLineType.Day,
                                    Year = ((DateTime)tempStartDate).Year,
                                    TimeLength = temp.TimeLength,
                                    SeasonType = temp.SeasonType
                                });
                                tempStartDate = EndDate;
                            }
                        }
                    }
                    else
                        result.Add(temp);
                }
            }
            return result.Any() ? result : null;
        }

        private List<TimeLineViewModel> ConvertCampaignMonthTimeLine(TimeLineRequestModel timeLine, bool SeparateDay = false)
        {
            var result = new List<TimeLineViewModel>();
            var temp = ConvertOtherTimeLine(timeLine);
            if (temp != null)
            {
                if (SeparateDay)
                {
                    if ((((DateTime)temp.StartDate).Month == ((DateTime)temp.EndDate).Month) && (((DateTime)temp.StartDate).Year == ((DateTime)temp.EndDate).Year))
                        result.Add(temp);
                    if ((((DateTime)temp.StartDate).Month != ((DateTime)temp.EndDate).Month) || (((DateTime)temp.StartDate).Year != ((DateTime)temp.EndDate).Year))
                    {
                        var year = ((DateTime)temp.StartDate).Year;
                        var list = new Dictionary<int, List<DateTime?>>()
                        {
                            {((DateTime)temp.StartDate).Month, new List<DateTime?>(){temp.StartDate, SetFirstMonth((DateTime)temp.StartDate)}}
                        };
                        if ((((DateTime)temp.EndDate).Month - ((DateTime)temp.StartDate).Month) == 1)
                            list.Add(((DateTime)temp.EndDate).Month, new List<DateTime?>() { SetLastMonth((DateTime)temp.EndDate), temp.EndDate });
                        else
                        {
                            int tempStartMonth = ((DateTime)temp.StartDate).Month + 1;
                            var flag = true;
                            while (flag)
                            {
                                if (tempStartMonth < ((DateTime)temp.EndDate).Month)
                                {
                                    int Month = tempStartMonth;
                                    var startDate = new DateTime(year, Month, 01);
                                    var endDate = startDate.LastDayOfTheMonth();
                                    list.Add(Month, new List<DateTime?>() { startDate, endDate });
                                    if (tempStartMonth == 12)
                                    {
                                        tempStartMonth = 1;
                                        year += 1;
                                    }
                                    else
                                        tempStartMonth += 1;
                                }
                                else
                                {
                                    list.Add(((DateTime)temp.EndDate).Month, new List<DateTime?>() { SetLastMonth((DateTime)temp.EndDate), temp.EndDate });
                                    flag = false;
                                }
                            }
                        }
                        if (list.Any())
                        {
                            list.ToList().ForEach(item =>
                            {
                                result.Add(new TimeLineViewModel()
                                {
                                    StartDate = item.Value.First(),
                                    EndDate = item.Value.Last(),
                                    Type = (byte)TimeLineType.Month,
                                    Id = temp.Id,
                                    SeasonType = temp.SeasonType,
                                    TimeLength = temp.SeasonType,
                                    Title = temp.Title
                                });
                            });
                        }
                    }
                }
                else
                    result.Add(temp);
            }
            return result.Any() ? result : null;
        }

        private DateTime? SetFirstMonth(DateTime startDate)
        {
            var year = startDate.Year;
            var FebruaryLastDate = (new DateTime(year, 02, 01)).LastDayOfTheMonth().Day;
            var specialDates = new List<int>() { 30, 31 };
            DateTime? endDate = null;
            if (!specialDates.Contains(startDate.Day) || !(startDate.Month.Equals(2) && startDate.Date.Equals(FebruaryLastDate)))
                endDate = (DateTime?)startDate.LastDayOfTheMonth().Date.AddDays(1).AddTicks(-1);
            else
                endDate = (DateTime?)startDate.Date.AddDays(1).AddTicks(-1);
            return endDate;
        }
        private DateTime? SetLastMonth(DateTime endDate)
        {
            var year = endDate.Year;
            DateTime? startDate = null;
            if (endDate.Day != 01)
                startDate = (DateTime?)endDate.FirstDayOfTheMonth();
            else
                startDate = (DateTime?)endDate.Date;
            return startDate;
        }

        private List<IssuerRevenueViewModel> GetRevenues(List<TimeLineViewModel> separateTimeLines, CampaignOrderViewModel campaign)
        {
            var list = new List<IssuerRevenueViewModel>();
            var validStatus = new List<byte?>()
            {
                (byte)OrderStatus.Shipped,
                (byte)OrderStatus.Received
            };
            var orders = campaign.Orders.Where(o => validStatus.Contains(o.Status)).ToList();
            if (orders.Any())
            {
                separateTimeLines.ForEach(timeLine =>
                {
                    var flag = true;
                    var _orders = orders.Where(o => validCampaignRevenueByOrder(o, (DateTime)timeLine.StartDate, (DateTime)timeLine.EndDate).Equals(true)).ToList();
                    if (_orders.Any())
                    {
                        var _TotalOrders = _mapper.Map<List<OrderViewModel>>(_orders);
                        _TotalOrders = GetTotal(_TotalOrders);
                        var Revenue = _TotalOrders.Sum(o => o.Total);
                        if (Revenue > 0)
                        {
                            flag = false;
                            list.Add(new IssuerRevenueViewModel()
                            {
                                timeLine = timeLine,
                                Revenue = Revenue
                            });
                        }
                    }
                    if (flag)
                        list.Add(new IssuerRevenueViewModel() { timeLine = timeLine });
                });
            }
            return list.Any() ? list : null;
        }

        private IssuerSubDashboardViewModel<BasicBookProductViewModel> GetBestSellerFromCampaigns(List<TimeLineViewModel> TimeLines, CampaignOrderViewModel campaign, int SizeData = 5)
        {
            var list = new IssuerSubDashboardViewModel<BasicBookProductViewModel>()
            {
                Data = new List<RevenueViewModel<BasicBookProductViewModel>>()
            };
            var orders = campaign.Orders;
            if (orders.Any())
            {
                TimeLines.ForEach(timeLine =>
                {
                    var _orders = orders.Where(o => validOrderByDateTime(o, (DateTime)timeLine.StartDate, (DateTime)timeLine.EndDate).Equals(true)).ToList();
                    if (_orders.Any())
                    {
                        var bookProducts = new List<OrderBookProductsViewModel>();
                        _orders.SelectMany(o => o.OrderDetails)
                        .Select(od => od.BookProduct).GroupBy(bp => bp.Id).Take(SizeData)
                        .ToList().ForEach(item => bookProducts.Add(item.Select(obp => obp).First()));
                        if (bookProducts.Any())
                        {
                            bookProducts.ForEach(bp =>
                            {
                                var total = _orders.SelectMany(o => o.OrderDetails)
                                .Where(od => od.BookProductId.Equals(bp.Id))
                                .Sum(od => od.Quantity);
                                if (total > 0)
                                {
                                    var basic = _mapper.Map<BasicBookProductViewModel>(bp);
                                    list.Data.Add(new RevenueViewModel<BasicBookProductViewModel>()
                                    {
                                        Data = basic,
                                        Total = total
                                    });
                                }
                            });
                        }
                    }
                });
                if (list.Data.Any())
                {

                }
            }
            return list.Data.Any() ? list : null;
        }
        private List<SubDashboardViewModel<OrdersViewModel>> GetOrdersFromCampaigns(List<TimeLineViewModel> TimeLines, CampaignOrderViewModel campaign, bool IsDescendingData = true)
        {
            var list = new List<SubDashboardViewModel<OrdersViewModel>>();
            var orders = campaign.Orders;
            if (orders.Any())
            {
                TimeLines.ForEach(timeLine =>
                {
                    var _orders = orders.Where(o => validOrderByDateTime(o, (DateTime)timeLine.StartDate, (DateTime)timeLine.EndDate).Equals(true)).ToList();
                    if (_orders.Any())
                    {
                        var groups = _orders.Select(o => o.Status).Distinct().ToList();
                        groups.ForEach(item =>
                        {
                            list.Add(new SubDashboardViewModel<OrdersViewModel>()
                            {
                                Status = item,
                                Data = _orders.Where(o => o.Status.Equals(item)).ToList(),
                                timeLine = timeLine
                            });
                        });
                    }
                });
            }
            if (list.Any())
            {
                if (IsDescendingData)
                    list = list.OrderByDescending(item => item.Data.Count()).ToList();
                else
                    list = list.OrderBy(item => item.Data.Count()).ToList();
                list.ForEach(item =>
                {
                    item.Data = GetTotal(item.Data);
                    item.Data.ForEach(o => o = ServiceUtils.GetResponseDetail(o));
                });
            }
            return list.Any() ? list : null;
        }

        private bool validCampaignRevenueByOrder(OrdersViewModel order, DateTime startDate, DateTime endDate)
        {
            var result = (CompareDateTime(order.ShippedDate, startDate, endDate)) ||
            (CompareDateTime(order.ReceivedDate, startDate, endDate));
            return result;
        }
        private bool validOrderByDateTime(OrdersViewModel order, DateTime startDate, DateTime endDate)
        {
            var result = CompareDateTime(order.OrderDate, startDate, endDate) ||
            (CompareDateTime(order.AvailableDate, startDate, endDate)) ||
            (CompareDateTime(order.ShippingDate, startDate, endDate)) ||
            (CompareDateTime(order.ShippedDate, startDate, endDate)) ||
            (CompareDateTime(order.ReceivedDate, startDate, endDate)) ||
            (CompareDateTime(order.CancelledDate, startDate, endDate));
            return result;
        }

        private bool CompareDateTime(DateTime? day, DateTime startDate, DateTime endDate)
        {
            if (day.HasValue)
                return ((DateTime)day).IsInsideIn((DateTime)day, startDate, endDate);
            return false;
        }

        private List<SummaryViewModel> GetSummary(List<TimeLineViewModel> timeLines, Guid? IssuerId)
        {
            var campaigns = new List<Campaign>();
            var yesterday = new List<Order>();
            var today = new List<Order>();
            if (IssuerId.HasValue)
            {
                var validStatus = new List<byte?>()
                {
                    (byte) ParticipantStatus.Accepted,
                    (byte) ParticipantStatus.Approved
                };
                campaigns = _unitOfWork.Campaigns.Get(c =>
                c.Participants.Any(p => p.IssuerId.Equals(IssuerId) &&
                validStatus.Contains(p.Status))
                ).ToList();
                yesterday = _unitOfWork.Orders.Get(o =>
                o.OrderDetails.Any(od => od.BookProduct.IssuerId.Equals(IssuerId)))
                .Include(o => o.OrderDetails)
                .ToList();
                today = _unitOfWork.Orders.Get(o =>
                o.OrderDetails.Any(od => od.BookProduct.IssuerId.Equals(IssuerId)))
                .Include(o => o.OrderDetails)
                .ToList();
            }
            else
            {
                campaigns = _unitOfWork.Campaigns.Get().ToList();
                yesterday = _unitOfWork.Orders.Get().Include(o => o.OrderDetails).ToList();
                today = _unitOfWork.Orders.Get().Include(o => o.OrderDetails).ToList();
            }
            yesterday = yesterday.Where(o =>
                ((DateTime)o.OrderDate).IsInsideIn((DateTime)o.OrderDate,
                (DateTime)timeLines.First().StartDate, (DateTime)timeLines.First().EndDate)).ToList();
            today = today.Where(o =>
                ((DateTime)o.OrderDate).IsInsideIn((DateTime)o.OrderDate,
                (DateTime)timeLines.Last().StartDate, (DateTime)timeLines.Last().EndDate)).ToList();
            var dateTimeNow = DateTime.Now;
            var notStartedCampaigns = campaigns.Where(c => DateTime.Compare((DateTime)c.StartDate, DateTime.Now) > 0 &&
                    DateTime.Compare((DateTime)c.EndDate, DateTime.Now) > 0).ToList();
            var startedCampaigns = campaigns.Where(c => dateTimeNow.IsInsideIn(dateTimeNow, (DateTime)c.StartDate, (DateTime)c.EndDate)).ToList();

            var todayBookProductQuantity = today.SelectMany(o => o.OrderDetails).Sum(od => od.Quantity);
            var yesterdayBookProductQuantity = yesterday.SelectMany(o => o.OrderDetails).Sum(od => od.Quantity);
            var list = new List<SummaryViewModel>()
            {
                new SummaryViewModel()
                {
                    Id = 0,
                    QuantityOfTitle = startedCampaigns.Count(),
                    Title = MessageConstants.DASHBOARD_SUMMARY_CAMPAIGN_TITLE,
                    QuantityOfSubTitle = notStartedCampaigns.Count(),
                    SubTitle = MessageConstants.DASHBOARD_SUMMARY_CAMPAIGN_SUBTITLE
                },
                new SummaryViewModel()
                {
                    Id = 1,
                    QuantityOfTitle = today.Count(),
                    Title = MessageConstants.DASHBOARD_SUMMARY_ORDER_TITLE,
                    QuantityOfSubTitle = yesterday.Count() > today.Count() ? yesterday.Count() - today.Count() : today.Count() - yesterday.Count(),
                    Status = yesterday.Count() == today.Count() ? (byte)DashboardSummary.Equal :
                    yesterday.Count() > today.Count() ? (byte)DashboardSummary.Decrease : (byte)DashboardSummary.Increase,
                    SubTitle = MessageConstants.DASHBOARD_SUMMARY_SUBTITLE
                },
                new SummaryViewModel()
                {
                    Id = 2,
                    QuantityOfTitle = todayBookProductQuantity,
                    Title = MessageConstants.DASHBOARD_SUMMARY_BOOK_PRODUCT_TITLE,
                    QuantityOfSubTitle = yesterdayBookProductQuantity > todayBookProductQuantity ? yesterdayBookProductQuantity - todayBookProductQuantity :
                    todayBookProductQuantity - yesterdayBookProductQuantity,
                    Status = yesterdayBookProductQuantity == todayBookProductQuantity ? (byte)DashboardSummary.Equal :
                    yesterdayBookProductQuantity > todayBookProductQuantity ? (byte)DashboardSummary.Decrease : (byte)DashboardSummary.Increase,
                    SubTitle = MessageConstants.DASHBOARD_SUMMARY_SUBTITLE
                }
            };
            list.ForEach(item =>
            {
                if (item.Status.HasValue)
                    item.StatusName = StatusExtension<DashboardSummary>.GetStatus(item.Status);
            });

            return list;
        }
        #endregion
    }
}