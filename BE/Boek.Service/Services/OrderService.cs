using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boek.Core.Constants;
using Boek.Core.Entities;
using Boek.Core.Enums;
using Boek.Infrastructure.Requests.BookProducts;
using Boek.Infrastructure.Requests.CampaignStaffs;
using Boek.Infrastructure.Requests.OrderDetails;
using Boek.Infrastructure.Requests.Orders;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.Responds.Orders;
using Boek.Infrastructure.ViewModels.Orders;
using Boek.Repository.Interfaces;
using Boek.Service.Commons;
using Boek.Service.Functions.ZaloPayHelper;
using Boek.Service.Functions.ZaloPayHelper.Crypto;
using Boek.Service.Interfaces;
using Boek.Service.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq.Dynamic.Core;
using System.Net;
using Boek.Infrastructure.Requests.Orders.Guest;
using Boek.Infrastructure.Requests.Notifications;
using Boek.Core.Extensions;
using Boek.Infrastructure.Requests.Orders.Update;
using Boek.Infrastructure.Requests.Verifications;
using System.Globalization;
using Boek.Infrastructure.ViewModels.Campaigns;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Boek.Infrastructure.Requests.Orders.ZaloPay;
using System.Security.Claims;
using Boek.Infrastructure.ViewModels.Orders.Calculation;
using Boek.Infrastructure.Requests.Orders.Calculation;
using Boek.Infrastructure.ViewModels.OrderDetails.Calculation;
using Newtonsoft.Json.Linq;
using System.Data;
using DateTimeExtensions;

namespace Boek.Service.Services
{
    public class OrderService : IOrderService
    {
        #region Field(s) and constructor
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IVerificationService _verificationService;
        private readonly ILogger<OrderService> _logger;

        private readonly INotificationService _notificationService;
        private readonly IConfiguration _configuration;

        #region ZaloPay Configuration
        private readonly string app_id = "2553";
        private readonly string key1 = "PcY4iZIKFCIdgZvA6ueMcMHHUbRLYjPL";
        private readonly string key2 = "eG4r0GcoNtRGbO8";
        private readonly string create_order_url = "https://sb-openapi.zalopay.vn/v2/create";
        private readonly string redirect_url = "https://server.boek.live/api/zalopay/return-page";
        private readonly string call_back_url = "https://server.boek.live/api/orders/zalopay/call-back";
        //private readonly string call_back_url = "http://localhost:5282/api/orders/zalopay";
        private readonly string query_order_url = "https://sb-openapi.zalopay.vn/v2/query";
        private readonly string refund_url = "https://sb-openapi.zalopay.vn/v2/refund";
        private readonly string query_refund_url = "https://sb-openapi.zalopay.vn/v2/query_refund";
        #endregion

        public OrderService(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor,
        ILogger<OrderService> logger,
        INotificationService notificationService,
        IVerificationService verificationService)
        {
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
            this._httpContextAccessor = httpContextAccessor;
            _notificationService = notificationService;
            _verificationService = verificationService;
            _configuration = new ConfigurationBuilder()
            .AddJsonFile("email.json", true, true)
            .Build();
            this._logger = logger;
        }
        #endregion

        #region Get

        #region Admin
        public async Task<BaseResponsePagingModel<OrderViewModel>> GetOrders(OrderRequestModel filter, PagingModel paging)
        {
            var query = GetOrderViewModelTotals();
            query = query.DynamicOtherFilter(filter);
            var list = new List<OrderViewModel>();
            var count = 0;

            if (query.Any())
            {
                var result = query.PagingQueryable(paging.Page, paging.Size, PageConstant.LimitPaging, PageConstant.LimitPaging);
                if (result.Item1 > 0)
                {
                    count = result.Item1;
                    result.Item2.ToList().ForEach(o => list.Add(ServiceUtils.GetResponseDetail(o)));
                }
            }

            return new BaseResponsePagingModel<OrderViewModel>()
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

        public OrderViewModel GetOrder(Guid id)
        {
            var order = _unitOfWork.Orders.Get(o => o.Id.Equals(id))
            .SingleOrDefault();
            if (order == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.ORDER_ID
                });
            return GetResponse(id);
        }
        #endregion

        #region Issuer
        public async Task<BaseResponsePagingModel<OrderViewModel>> GetOrdersByIssuer(OrderRequestModel filter, PagingModel paging)
        {
            var _filter = _mapper.Map<OrderFilterRequestModel>(filter);
            var IssuerId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            _filter.OrderDetails = new OrderDetailRequestModel()
            {
                BookProduct = new BookProductRequestModel()
                {
                    IssuerId = IssuerId
                }
            };
            var query = GetOrderViewModelTotals();
            query = query.DynamicOtherFilter(_filter);
            var list = new List<OrderViewModel>();
            var count = 0;

            if (query.Any())
            {
                var result = query.PagingQueryable(paging.Page, paging.Size, PageConstant.LimitPaging, PageConstant.LimitPaging);
                if (result.Item1 > 0)
                {
                    count = result.Item1;
                    result.Item2.ToList().ForEach(o => list.Add(ServiceUtils.GetResponseDetail(o)));
                }
            }

            return new BaseResponsePagingModel<OrderViewModel>()
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

        public OrderViewModel GetOrderByIssuer(Guid id)
        {
            var IssuerId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            var result = _unitOfWork.Orders.Get(o => o.Id.Equals(id) &&
            o.OrderDetails.Any(od => od.BookProduct.IssuerId.Equals(IssuerId)))
            .SingleOrDefault();
            if (result == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.ORDER_ID
                });
            return GetResponse(id);
        }

        #endregion

        #region Staff
        public async Task<BaseResponsePagingModel<OrderViewModel>> GetOrdersByStaff(OrderRequestModel filter, PagingModel paging, bool? byStaff = false)
        {
            var _filter = _mapper.Map<OrderFilterRequestModel>(filter);
            var StaffId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            var query = GetOrderViewModelTotals(StaffId);
            if (byStaff.HasValue)
            {
                if ((bool)byStaff)
                {
                    _filter.CampaignStaff = new CampaignStaffRequestModel()
                    {
                        StaffId = StaffId,
                    };
                    query = query.Where(c => c.CampaignStaff != null).AsQueryable();
                }
            }
            query = query.DynamicOtherFilter(_filter);
            var list = new List<OrderViewModel>();
            var count = 0;

            if (query.Any())
            {
                var result = query.PagingQueryable(paging.Page, paging.Size, PageConstant.LimitPaging, PageConstant.LimitPaging);
                if (result.Item1 > 0)
                {
                    count = result.Item1;
                    result.Item2.ToList().ForEach(o => list.Add(ServiceUtils.GetResponseDetail(o)));
                }
            }

            return new BaseResponsePagingModel<OrderViewModel>()
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

        public OrderViewModel GetOrderByStaff(Guid id)
        {
            var result = _unitOfWork.Orders.Get(o => o.Id.Equals(id) &&
            o.Type.Equals((byte)OrderType.PickUp))
            .SingleOrDefault();
            if (result == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.ORDER_ID
                });
            return GetResponse(id);
        }

        #endregion

        #region Customer
        public async Task<BaseResponsePagingModel<OrderViewModel>> GetOrdersByCustomer(OrderRequestModel filter, PagingModel paging)
        {
            var _filter = _mapper.Map<OrderFilterRequestModel>(filter);
            _filter.CustomerId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            var query = GetOrderViewModelTotals();
            query = query.Where(c => c.CustomerId.HasValue).AsQueryable();
            query = query.DynamicOtherFilter(_filter);
            var list = new List<OrderViewModel>();
            var count = 0;

            if (query.Any())
            {
                var result = query.PagingQueryable(paging.Page, paging.Size, PageConstant.LimitPaging, PageConstant.LimitPaging);
                if (result.Item1 > 0)
                {
                    count = result.Item1;
                    result.Item2.ToList().ForEach(o => list.Add(ServiceUtils.GetResponseDetail(o)));
                }
            }

            return new BaseResponsePagingModel<OrderViewModel>()
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
        public OrderViewModel GetOrderByCustomer(Guid id)
        {
            var customerId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            var result = _unitOfWork.Orders.Get(q => q.Id.Equals(id) &&
            q.CustomerId.Equals(customerId))
            .SingleOrDefault();
            if (result == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.ORDER_ID
                });
            return GetResponse(id);
        }
        #endregion
        public async Task<List<OrderViewModel>> GetOrderFromQRCode(QROrdersRequestModel qROrders)
        {
            if (qROrders.OrderIds == null || !qROrders.OrderIds.Any())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.PreconditionRequired), new string[]
                {
                    MessageConstants.MESSAGE_REQUIRED,
                    ErrorMessageConstants.ORDER_ID
                });
            var orders = _unitOfWork.Orders.Get(o => o.CustomerId.Equals(qROrders.CustomerId)
            && o.CampaignId.Equals(qROrders.CampaignId) &&
            qROrders.OrderIds.Contains(o.Id))
            .ProjectTo<OrderViewModel>(_mapper.ConfigurationProvider).ToList();
            if (!orders.Any() || orders.Count() != qROrders.OrderIds.Count())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                {
                    ErrorMessageConstants.ORDER_QR_AMOUNT,
                    MessageConstants.MESSAGE_INVALID
                });
            orders.ForEach(o =>
            {
                o = ServiceUtils.GetResponseDetail(o);
                o = ServiceUtils.GetTotal(o, _mapper, _unitOfWork);
            });
            return orders;
        }

        public List<string> GetOrderCampaignAddresses(Guid id)
        {
            var order = _unitOfWork.Orders.Get(o => o.Id.Equals(id))
            .SingleOrDefault();
            if (order == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.ORDER_ID
                });
            var campaign = _unitOfWork.Campaigns.Get(c => c.Id.Equals(order.CampaignId))
            .ProjectTo<CampaignViewModel>(_mapper.ConfigurationProvider)
            .SingleOrDefault();
            if (campaign == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.CAMPAIGN_ID,
                });
            var result = new List<string>();
            if (!string.IsNullOrEmpty(campaign.Address))
                result.Add(campaign.Address);
            if (campaign.CampaignOrganizations.Any())
            {
                var temp = campaign.CampaignOrganizations
                .SelectMany(cos => cos.Schedules)
                .Where(schedule => (DateTime.Now).IsInsideIn((DateTime.Now), (DateTime)schedule.StartDate, (DateTime)schedule.EndDate) == true)
                .GroupBy(schedule => schedule.Address)
                .Where(address => !string.IsNullOrEmpty(address.Key) && !result.Contains(address.Key))
                .Select(address => address.Key);
                if (temp.Any())
                    result.AddRange(temp);
            }
            return result.Any() ? result : null;
        }

        #endregion

        #region Calculation
        public OrderCalculationViewModel GetShippingOrderCalculation(ShippingOrderCalculationRequestModel request)
        {
            var order = _mapper.Map<Order>(request);
            order.Address = ServiceUtils.CheckAddress(request.AddressRequest).Address;
            var result = GetOrderCalculation(order, OrderType.Shipping);
            if (result == null)
                BoekErrorMessage.ShowErrorMessage((int)HttpStatusCode.Conflict, new string[]
                {
                    ErrorMessageConstants.CALCULATE,
                    ErrorMessageConstants.ORDER,
                    MessageConstants.MESSAGE_FAILED
                });
            return result;
        }
        public OrderCalculationViewModel GetPickUpOrderCalculation(PickUpOrderCalculationRequestModel request)
        {
            var order = _mapper.Map<Order>(request);
            order.Freight = 0;
            var result = GetOrderCalculation(order, OrderType.PickUp);
            if (result == null)
                BoekErrorMessage.ShowErrorMessage((int)HttpStatusCode.Conflict, new string[]
                {
                    ErrorMessageConstants.CALCULATE,
                    ErrorMessageConstants.ORDER,
                    MessageConstants.MESSAGE_FAILED
                });
            return result;
        }
        public OrderCalculationViewModel GetCartCalculation(PickUpOrderCalculationRequestModel request)
        {
            var order = _mapper.Map<Order>(request);
            order.Freight = 0;
            var result = GetOrderCalculation(order, OrderType.PickUp, false);
            if (result == null)
                BoekErrorMessage.ShowErrorMessage((int)HttpStatusCode.Conflict, new string[]
                {
                    ErrorMessageConstants.CALCULATE,
                    ErrorMessageConstants.ORDER,
                    MessageConstants.MESSAGE_FAILED
                });
            return result;
        }
        #endregion

        #region Create
        public async Task AddRangeAsyncCustom(IEnumerable<OrderCreateModel> entity)
        {
            _unitOfWork.Orders.AddRange(_mapper.Map<IEnumerable<Order>>(entity));
            if (!_unitOfWork.Save())
            {
                throw new ApplicationException();
            }
        }

        public async Task<Order> AddOrderAsyncCustom(OrderCreateModel createOrder)
        {
            return await _unitOfWork.Orders.AddAsyncCustom(_mapper.Map<Order>(createOrder));
        }

        #region Phuong

        #region Customer
        public List<OrderViewModel> CreateShippingOrderByCustomer(CreateShippingOrderRequestModel createShippingOrder)
        {
            var order = _mapper.Map<Order>(createShippingOrder);
            order.Address = ServiceUtils.CheckAddress(createShippingOrder.AddressRequest).Address;
            var validPayments = new List<byte>() { (byte)OrderPayment.Cash };
            var orders = CheckOrder(order, OrderType.Shipping, validPayments, true);
            if (orders == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.INSERT,
                    ErrorMessageConstants.ORDER.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            _unitOfWork.Orders.AddRange(orders);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.INSERT,
                    ErrorMessageConstants.ORDER.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            var orderIds = orders.Select(o => o.Id).ToList();
            var result = _unitOfWork.Orders.Get(o => orderIds.Contains(o.Id))
            .ProjectTo<OrderViewModel>(_mapper.ConfigurationProvider).ToList();
            result.ForEach(o =>
            {
                o = ServiceUtils.GetResponseDetail(o);
                o = ServiceUtils.GetTotal(o, _mapper, _unitOfWork);
            });
            return result;
        }

        public List<OrderViewModel> CreatePickUpOrderByCustomer(CreateCustomerPickUpOrderRequestModel createPickUpOrder)
        {
            var order = _mapper.Map<Order>(createPickUpOrder);
            order.Freight = 0;
            var validPayments = new List<byte>() { (byte)OrderPayment.Cash };
            var orders = CheckOrder(order, OrderType.PickUp, validPayments, true);
            if (orders == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.INSERT,
                    ErrorMessageConstants.ORDER.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            _unitOfWork.Orders.AddRange(orders);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.INSERT,
                    ErrorMessageConstants.ORDER.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            var orderIds = orders.Select(o => o.Id).ToList();
            var result = _unitOfWork.Orders.Get(o => orderIds.Contains(o.Id))
            .ProjectTo<OrderViewModel>(_mapper.ConfigurationProvider).ToList();
            result.ForEach(o =>
            {
                o = ServiceUtils.GetResponseDetail(o);
                o = ServiceUtils.GetTotal(o, _mapper, _unitOfWork);
            });
            return result;
        }
        #endregion

        #region Staff
        public List<OrderViewModel> CreatePickUpOrderByStaff(CreateStaffPickUpOrderRequestModel createPickUpOrder)
        {
            var order = _mapper.Map<Order>(createPickUpOrder);
            order.Freight = 0;
            var validPayments = new List<byte>() { (byte)OrderPayment.Cash };
            var orders = CheckOrder(order, OrderType.PickUp, validPayments, true);
            if (orders == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.INSERT,
                    ErrorMessageConstants.ORDER.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            var orderDetails = new List<OrderDetail>();
            orders.ForEach(o => orderDetails.AddRange(o.OrderDetails));
            _unitOfWork.Orders.AddRange(orders);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.INSERT,
                    ErrorMessageConstants.ORDER.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            UpdateSaleQuantityBookProduct(orderDetails);
            var orderIds = orders.Select(o => o.Id).ToList();
            var result = _unitOfWork.Orders.Get(o => orderIds.Contains(o.Id))
            .ProjectTo<OrderViewModel>(_mapper.ConfigurationProvider).ToList();
            UpdateCustomerPoint(_mapper.Map<List<Order>>(result));
            result.ForEach(o =>
            {
                o = ServiceUtils.GetResponseDetail(o);
                o = ServiceUtils.GetTotal(o, _mapper, _unitOfWork);
            });
            return result;
        }
        #endregion

        #region Guest
        public List<OrderViewModel> CreateShippingOrderByGuest(CreateGuestShippingOrderRequestModel createShippingOrder)
        {
            var order = _mapper.Map<Order>(createShippingOrder);
            order.Address = ServiceUtils.CheckAddress(createShippingOrder.AddressRequest).Address;
            var validPayments = new List<byte>() { (byte)OrderPayment.Cash };
            var orders = CheckGuestOrder(order, OrderType.Shipping, validPayments, true);
            if (orders == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.INSERT,
                    ErrorMessageConstants.ORDER.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            _unitOfWork.Orders.AddRange(orders);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.INSERT,
                    ErrorMessageConstants.ORDER.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            var orderIds = orders.Select(o => o.Id).ToList();
            var result = _unitOfWork.Orders.Get(o => orderIds.Contains(o.Id))
            .ProjectTo<OrderViewModel>(_mapper.ConfigurationProvider).ToList();
            result.ForEach(o =>
            {
                o = ServiceUtils.GetResponseDetail(o);
                o = ServiceUtils.GetTotal(o, _mapper, _unitOfWork);
            });
            return result;
        }

        public List<OrderViewModel> CreatePickUpOrderByGuest(CreateGuestPickUpOrderRequestModel createPickUpOrder)
        {
            var order = _mapper.Map<Order>(createPickUpOrder);
            order.Freight = 0;
            var validPayments = new List<byte>() { (byte)OrderPayment.Cash };
            var orders = CheckGuestOrder(order, OrderType.PickUp, validPayments, true);
            if (orders == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.INSERT,
                    ErrorMessageConstants.ORDER.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            _unitOfWork.Orders.AddRange(orders);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.INSERT,
                    ErrorMessageConstants.ORDER.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            var orderIds = orders.Select(o => o.Id).ToList();
            var result = _unitOfWork.Orders.Get(o => orderIds.Contains(o.Id))
            .ProjectTo<OrderViewModel>(_mapper.ConfigurationProvider).ToList();
            result.ForEach(o =>
            {
                o = ServiceUtils.GetResponseDetail(o);
                o = ServiceUtils.GetTotal(o, _mapper, _unitOfWork);
            });
            return result;
        }
        #endregion

        #endregion
        #endregion

        #region Update
        public async Task UpdateRangeCustom(IEnumerable<OrderCreateModel> createOrder)
        {
            _unitOfWork.Orders.UpdateRange(_mapper.Map<IEnumerable<Order>>(createOrder));
            if (!_unitOfWork.Save())
            {
                throw new ApplicationException();
            }
        }

        public List<OrderViewModel> UpdateProgressingStatusOrder(List<OrderUpdateModel> orderUpdateModels)
        {
            var orderIds = orderUpdateModels.Select(o => o.Id).ToList();
            var _orders = _unitOfWork.Orders.Get(o => orderIds.Contains(o.Id))
            .Include(o => o.OrderDetails)
            .ThenInclude(orderDetail => orderDetail.BookProduct)
            .ThenInclude(bookProduct => bookProduct.BookProductItems)
            .Include(o => o.OrderDetails)
            .ThenInclude(orderDetail => orderDetail.BookProduct)
            .ThenInclude(bookProduct => bookProduct.Book)
            .Include(o => o.Campaign).ToList();
            if (_orders.Any())
            {
                var ShowErrorMessage = false;
                var errorMessages = CheckPendingOrderCampaigns(_orders.Select(o => o.CampaignId).ToList());
                if (errorMessages != null)
                    SendErrorLog(errorMessages, ErrorMessageConstants.ORDER_PENDING_ERROR_CAMPAIGNS);
                else
                {
                    errorMessages = new List<string>();
                    var validStatus = new List<byte?>() { (byte)OrderStatus.Processing };
                    var validType = new List<byte?>()
                    {
                        (byte) OrderType.Shipping,
                        (byte) OrderType.PickUp,
                    };
                    var _status = OrderStatus.Processing;

                    _orders.ForEach(o =>
                    {
                        var note = o.Note;
                        var updateOrder = orderUpdateModels.SingleOrDefault(item => item.Id.Equals(o.Id));
                        if (updateOrder != null)
                        {
                            var _updateOrder = _mapper.Map<Order>(updateOrder);
                            o.Note = ServiceUtils.GetUpdateNote(_updateOrder.Note, note, MessageConstants.BOEK);
                        }
                        var temp = CheckUpdateStatus(o, validStatus, validType, _status, true, false, ShowErrorMessage);
                        if (temp != null)
                            errorMessages.AddRange(temp);
                    });
                    if (errorMessages.Any())
                        SendErrorLog(errorMessages, ErrorMessageConstants.ORDER_PENDING_CHECK_ORDER);
                    else
                    {
                        _orders.ForEach(o =>
                        {
                            var temp = UpdateOrderByStatus(o, _status, false, false, ShowErrorMessage);
                            if (temp != null)
                                errorMessages.AddRange(temp);
                        });
                        if (errorMessages.Any())
                            SendErrorLog(errorMessages, ErrorMessageConstants.ORDER_PENDING_UPDATE_STATUS);
                        else
                            return GetResponses(orderIds);
                    }
                }
            }
            return null;
        }

        public OrderViewModel UpdateAddressPickUpOrder(UpdateAvailableOrderRequestModel updateAvailableOrder)
        {
            var _order = _unitOfWork.Orders.Get(o => o.Id.Equals(updateAvailableOrder.Id)).Include(o => o.OrderDetails)
                .ThenInclude(orderDetail => orderDetail.BookProduct)
                .ThenInclude(bookProduct => bookProduct.BookProductItems)
                .Include(o => o.OrderDetails)
                .ThenInclude(orderDetail => orderDetail.BookProduct)
                .ThenInclude(bookProduct => bookProduct.Book)
                .Include(o => o.Campaign)
                .SingleOrDefault();
            var note = _order.Note;
            var _updateOrder = _mapper.Map<Order>(updateAvailableOrder);
            if (string.IsNullOrEmpty(_updateOrder.Note))
                _updateOrder.Note = ErrorMessageConstants.UPDATE_ORDER_ADDRESS + _updateOrder.Address.Trim();
            _order.Note = ServiceUtils.GetUpdateNote(_updateOrder.Note, note, _httpContextAccessor, ClaimTypes.Name);
            _order.Address = _updateOrder.Address.Trim();
            var validStatus = new List<byte?>()
            {
                (byte) OrderStatus.PickUpAvailable,
            };
            var validType = new List<byte?>()
            {
                (byte) OrderType.PickUp,
            };

            var _status = OrderStatus.PickUpAvailable;
            CheckUpdateStatus(_order, validStatus, validType, _status, true, true, true, false);
            CheckIssuerId(_order.OrderDetails.First().BookProduct.IssuerId);
            UpdateOrderByStatus(_order, _status, false, true);
            return GetResponse(updateAvailableOrder.Id);
        }

        public OrderViewModel UpdatePickUpAvailableStatusOrder(UpdateAvailableOrderRequestModel updateAvailableOrder)
        {
            var _order = _unitOfWork.Orders.Get(o => o.Id.Equals(updateAvailableOrder.Id)).Include(o => o.OrderDetails)
                .ThenInclude(orderDetail => orderDetail.BookProduct)
                .ThenInclude(bookProduct => bookProduct.BookProductItems)
                .Include(o => o.OrderDetails)
                .ThenInclude(orderDetail => orderDetail.BookProduct)
                .ThenInclude(bookProduct => bookProduct.Book)
                .Include(o => o.Campaign)
                .SingleOrDefault();
            var note = _order.Note;
            var _updateOrder = _mapper.Map<Order>(updateAvailableOrder);
            _order.Note = ServiceUtils.GetUpdateNote(_updateOrder.Note, note, _httpContextAccessor, ClaimTypes.Name);
            _order.Address = _updateOrder.Address;
            var validStatus = new List<byte?>()
            {
                (byte) OrderStatus.Processing
            };
            var validType = new List<byte?>()
            {
                (byte) OrderType.PickUp,
            };

            var _status = OrderStatus.PickUpAvailable;
            CheckUpdateStatus(_order, validStatus, validType, _status, true);
            CheckIssuerId(_order.OrderDetails.First().BookProduct.IssuerId);
            UpdateOrderByStatus(_order, _status);
            return GetResponse(updateAvailableOrder.Id);
        }

        public OrderViewModel UpdateShippingStatusOrder(OrderUpdateModel updateOrder)
        {
            var _order = _unitOfWork.Orders.Get(o => o.Id.Equals(updateOrder.Id)).Include(o => o.OrderDetails)
                .ThenInclude(orderDetail => orderDetail.BookProduct)
                .ThenInclude(bookProduct => bookProduct.BookProductItems)
                .Include(o => o.OrderDetails)
                .ThenInclude(orderDetail => orderDetail.BookProduct)
                .ThenInclude(bookProduct => bookProduct.Book)
                .Include(o => o.Campaign)
                .SingleOrDefault();
            var note = _order.Note;
            var _updateOrder = _mapper.Map<Order>(updateOrder);
            _order.Note = ServiceUtils.GetUpdateNote(_updateOrder.Note, note, _httpContextAccessor, ClaimTypes.Name);
            var validStatus = new List<byte?>()
            {
                (byte) OrderStatus.Processing
            };
            var validType = new List<byte?>()
            {
                (byte) OrderType.Shipping,
            };

            var _status = OrderStatus.Shipping;
            CheckUpdateStatus(_order, validStatus, validType, _status, true);
            CheckIssuerId(_order.OrderDetails.First().BookProduct.IssuerId);
            UpdateOrderByStatus(_order, _status);
            UpdateSaleQuantityBookProduct(_order.OrderDetails.ToList());
            return GetResponse(updateOrder.Id);
        }

        public OrderViewModel UpdateShippedStatusOrder(OrderUpdateModel updateOrder)
        {
            var _order = _unitOfWork.Orders.Get(o => o.Id.Equals(updateOrder.Id))
                .Include(o => o.OrderDetails)
                .ThenInclude(orderDetail => orderDetail.BookProduct)
                .ThenInclude(bookProduct => bookProduct.BookProductItems)
                .Include(o => o.OrderDetails)
                .ThenInclude(orderDetail => orderDetail.BookProduct)
                .ThenInclude(bookProduct => bookProduct.Book)
                .Include(o => o.Campaign)
                .SingleOrDefault();
            var note = _order.Note;
            var _updateOrder = _mapper.Map<Order>(updateOrder);
            _order.Note = ServiceUtils.GetUpdateNote(_updateOrder.Note, note, _httpContextAccessor, ClaimTypes.Name);
            var validStatus = new List<byte?>()
            {
                (byte) OrderStatus.Shipping
            };
            var validType = new List<byte?>()
            {
                (byte) OrderType.Shipping,
            };

            var _status = OrderStatus.Shipped;
            CheckUpdateStatus(_order, validStatus, validType, _status, true, false);
            CheckIssuerId(_order.OrderDetails.First().BookProduct.IssuerId);
            UpdateOrderByStatus(_order, _status);
            UpdateCustomerPoint(new List<Order>() { _order });
            return GetResponse(updateOrder.Id);
        }

        public OrderViewModel UpdateReceivedStatusOrder(OrderUpdateModel updateOrder)
        {
            var _order = _unitOfWork.Orders.Get(o => o.Id.Equals(updateOrder.Id))
                .Include(o => o.OrderDetails)
                .ThenInclude(orderDetail => orderDetail.BookProduct)
                .ThenInclude(bookProduct => bookProduct.BookProductItems)
                .Include(o => o.Campaign)
                .ThenInclude(campaign => campaign.CampaignStaffs)
                .SingleOrDefault();
            var validStatus = new List<byte?>()
            {
                (byte) OrderStatus.PickUpAvailable
            };
            var validType = new List<byte?>()
            {
                (byte) OrderType.PickUp,
            };

            var _status = OrderStatus.Received;
            CheckUpdateStatus(_order, validStatus, validType, _status, true);
            var note = _order.Note;
            var _staffId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            var _campaignStaff = _order.Campaign.CampaignStaffs.SingleOrDefault(cs => cs.StaffId.Equals(_staffId)
            && cs.Status.Equals((byte)CampaignStaffStatus.Attended));
            if (_campaignStaff == null)
            {
                BoekErrorMessage.ShowErrorMessage((int)HttpStatusCode.BadRequest, new string[]
                {
                    MessageConstants.MESSAGE_INVALID,
                    ErrorMessageConstants.CAMPAIGN_STAFF_UNATTENDED_STATUS
                });
            }
            var _updateOrder = _mapper.Map<Order>(updateOrder);
            _order.Note = ServiceUtils.GetUpdateNote(_updateOrder.Note, note, _httpContextAccessor, ClaimTypes.Role);
            _order.CampaignStaffId = _campaignStaff.Id;
            _order.CampaignStaff = _campaignStaff;
            UpdateOrderByStatus(_order, _status);
            UpdateCustomerPoint(new List<Order>() { _order });
            UpdateSaleQuantityBookProduct(_order.OrderDetails.ToList());
            return GetResponse(updateOrder.Id);
        }

        public OrderViewModel UpdateCancelStatusOrderByIssuer(OrderUpdateModel updateOrder)
        {
            var _order = _unitOfWork.Orders.Get(o => o.Id.Equals(updateOrder.Id))
                .Include(o => o.OrderDetails)
                .ThenInclude(orderDetail => orderDetail.BookProduct)
                .ThenInclude(bookProduct => bookProduct.BookProductItems)
                .Include(o => o.Campaign)
                .SingleOrDefault();
            var validStatus = new List<byte?>()
            {
                (byte)OrderStatus.Processing
            };
            var _status = OrderStatus.Cancelled;
            CheckCancelStatus(_order, validStatus, _status);
            CheckIssuerId(_order.OrderDetails.First().BookProduct.IssuerId);
            var note = _order.Note;
            var _updateOrder = _mapper.Map<Order>(updateOrder);
            _order.Note = ServiceUtils.GetUpdateNote(_updateOrder.Note, note, _httpContextAccessor, ClaimTypes.Name);
            UpdateOrderByStatus(_order, _status);
            return GetResponse(updateOrder.Id);
        }

        public OrderViewModel UpdateCancelStatusOrderByCustomer(OrderUpdateModel updateOrder)
        {
            var _order = _unitOfWork.Orders.Get(q => q.Id.Equals(updateOrder.Id))
                .Include(o => o.Campaign)
                .SingleOrDefault();
            var validStatus = new List<byte?>()
            {
                (byte)OrderStatus.Processing
            };
            var _status = OrderStatus.Cancelled;
            CheckCancelStatus(_order, validStatus, _status);
            CheckCustomer(_order.CustomerId);
            var note = _order.Note;
            var _updateOrder = _mapper.Map<Order>(updateOrder);
            _order.Note = ServiceUtils.GetUpdateNote(_updateOrder.Note, note, _httpContextAccessor, ClaimTypes.Name);
            UpdateOrderByStatus(_order, _status);
            return GetResponse(updateOrder.Id);
        }
        #endregion

        #region ZaloPay
        public List<Order> ValidateZaloPayOrder(CreateZaloPayOrderRequestModel createZaloPayOrder)
        {
            //Check valid role
            var validRoles = new Dictionary<byte?, string>()
            {
                //{(byte)BoekRole.Staff, StatusExtension<BoekRole>.GetStatus((byte)BoekRole.Staff)},
                {(byte)BoekRole.Customer, StatusExtension<BoekRole>.GetStatus((byte)BoekRole.Customer)},
                {(byte)BoekRole.Guest, StatusExtension<BoekRole>.GetStatus((byte)BoekRole.Guest)}
            };
            var role = CheckZaloPayRoles(validRoles);
            List<Order> orders = null;
            switch (role.Key)
            {
                // #region Staff
                // case (byte)BoekRole.Staff:
                //     //Check valid order type
                //     var validList = new List<byte?>() { (byte)OrderType.PickUp };
                //     CheckValidOrderType(createZaloPayOrder.Type, validList);
                //     orders = GetStaffZaloPayOrders(createZaloPayOrder);
                //     break;
                // #endregion

                #region Customer
                case (byte)BoekRole.Customer:
                    //Check valid order type
                    var validList = new List<byte?>()
                    {
                        (byte)OrderType.PickUp,
                        (byte)OrderType.Shipping
                    };
                    CheckValidOrderType(createZaloPayOrder.Type, validList);
                    orders = GetCustomerZaloPayOrders(createZaloPayOrder);
                    break;
                #endregion

                #region Guest
                case (byte)BoekRole.Guest:
                    //Check valid order type
                    validList = new List<byte?>()
                    {
                        (byte)OrderType.PickUp,
                        (byte)OrderType.Shipping
                    };
                    CheckValidOrderType(createZaloPayOrder.Type, validList);
                    orders = GetGuestZaloPayOrders(createZaloPayOrder);
                    break;
                    #endregion
            }
            return orders;
        }

        public ZaloPayCreateOrder CreatePendingOrders(List<Order> orders, CreateZaloPayOrderRequestModel createZaloPayOrder)
        {
            var flag = false;
            try
            {
                if (orders == null)
                {
                    flag = true;
                    throw new Exception();
                }
                if (!orders.Any())
                {
                    flag = true;
                    throw new Exception();
                }
                orders.ForEach(o => o.Status = (byte)OrderStatus.Pending);
                _unitOfWork.Orders.AddRange(orders);
                if (!_unitOfWork.Save())
                {
                    flag = true;
                    throw new Exception();
                }
                var temp = orders.Select(o => o.Id).ToList();
                UpdateOrderDateOfPendingOrders(temp, ref flag);
                if (temp.Any())
                {
                    var orderIds = new List<Guid?>();
                    temp.ForEach(item => orderIds.Add(item));
                    var _orders = GetResponses(orderIds);
                    if (_orders.Any())
                    {
                        var amount = (decimal)_orders.Sum(o => o.Total);
                        var orderDetails = _orders.SelectMany(o => o.OrderDetails);
                        var items = _mapper.Map<IEnumerable<ZaloPayItemParams>>(orderDetails);
                        var response = new ZaloPayCreateOrder()
                        {
                            User = _orders.First().CustomerName,
                            Amount = amount,
                            Description = createZaloPayOrder.Description,
                            RedirectUrl = createZaloPayOrder.RedirectUrl,
                            Items = items,
                            OrderIds = temp
                        };
                        return response;
                    }
                    else
                    {
                        flag = true;
                        throw new Exception();
                    }
                }
                else
                {
                    flag = true;
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                if (flag)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                    {
                        ErrorMessageConstants.INSERT,
                        ErrorMessageConstants.ORDER.ToLower(),
                        MessageConstants.MESSAGE_FAILED
                    });
                else
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.InternalServerError), new string[] { ex.Message });
            }
            return null;
        }

        private void UpdateOrderDateOfPendingOrders(List<Guid> orderIds, ref bool flag)
        {
            var dateTimeNow = DateTime.Now;
            var orders = _unitOfWork.Orders.Get(q => orderIds.Contains(q.Id)).ToList();
            if (orders.Any())
            {
                orders.ForEach(o => o.OrderDate = dateTimeNow);
                _unitOfWork.Orders.UpdateRange(orders);
                if (!_unitOfWork.Save())
                {
                    flag = true;
                    throw new Exception();
                }
            }
        }

        public ZaloPayOrderResponseModel CreateZaloPayOrder(ZaloPayCreateOrder createModel)
        {
            //Execute calling ZaloPay Server
            Random rnd = new Random();
            var param = new Dictionary<string, string>();
            var embedData = new EmbedDataQrequestCreateModel
            {
                redirecturl = createModel.RedirectUrl,
                OrderIds = createModel.OrderIds
            };
            var app_trans_id = rnd.Next(1000000);
            param.Add(MessageConstants.APP_ID, app_id);
            param.Add(MessageConstants.APP_USER, createModel.User);
            param.Add(MessageConstants.APP_TIME, Functions.ZaloPayHelper.Utils.GetTimeStamp().ToString());
            param.Add(MessageConstants.AMOUNT, createModel.Amount.ToString());
            var appTransId = DateTime.Now.ToString("yyMMdd") + "_" + app_trans_id;
            param.Add(MessageConstants.APP_TRANS_ID, appTransId);
            param.Add(MessageConstants.EMBED_DATA, JsonConvert.SerializeObject(embedData));
            param.Add(MessageConstants.DESCRIPTION, createModel.Description + app_trans_id);
            param.Add(MessageConstants.BANK_CODE, "zalopayapp");
            if (createModel.Items.Count() > 0)
            {
                param.Add(MessageConstants.ITEM, JsonConvert.SerializeObject(createModel.Items));
            }
            else
            {
                param.Add(MessageConstants.ITEM, "[]");
            }
            param.Add(MessageConstants.CALLBACK_URL, call_back_url);
            var data = app_id + "|" + param[MessageConstants.APP_TRANS_ID] + "|" + param[MessageConstants.APP_USER] + "|" + param[MessageConstants.AMOUNT] + "|"
                + param[MessageConstants.APP_TIME] + "|" + param[MessageConstants.EMBED_DATA] + "|" + param[MessageConstants.ITEM];
            param.Add(MessageConstants.MAC, HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, key1, data));
            var result = HttpHelper.PostFormAsync(create_order_url, param);

            //Response FE
            var responseJson = JsonConvert.SerializeObject(result.Result);
            var responseRecord = JsonConvert.DeserializeObject<ZaloPayOrderResponseModel>(responseJson);
            responseRecord.app_trans_id = appTransId;
            return responseRecord;
        }

        public ZaloPayQueryCreatedOrderResponseModel QueryCreatedZaloPayOrder(ZaloPayOrderQueryModel createModel)
        {
            var param = new Dictionary<string, string>();
            param.Add(MessageConstants.APP_ID, app_id);
            param.Add(MessageConstants.APP_TRANS_ID, createModel.AppTransId);
            var data = app_id + "|" + createModel.AppTransId + "|" + key1;

            param.Add(MessageConstants.MAC, HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, key1, data));

            var result = HttpHelper.PostFormAsync(query_order_url, param);
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var responseJson = JsonConvert.SerializeObject(result.Result);
            var responseRecord = JsonConvert.DeserializeObject<ZaloPayQueryCreatedOrderResponseModel>(responseJson, settings);
            return responseRecord;
        }

        public ZaloPayCallPayViewModel CallBackZaloPayOrder(dynamic cbdata)
        {
            _logger.LogInformation("============= CALL BACK ZALOPAY ORDER =============");
            var result = new ZaloPayCallPayViewModel();
            var resultJson = new ZaloPayCallBackResponseModel();
            try
            {

                /*var rawDynamicData = JObject.Parse(cbdata);
                var dataStr = Convert.ToString(rawDynamicData["data"]);
                _logger.LogInformation($"dataStr: {dataStr}");
                var reqMac = Convert.ToString(rawDynamicData["mac"]);
                _logger.LogInformation($"reqMac: {reqMac}");*/

                var rawDynamicDataToString = Convert.ToString(cbdata);
                FirstHierarchyZaloPayCallBackRequestModel rawDynamicDataObject = JsonConvert.DeserializeObject<FirstHierarchyZaloPayCallBackRequestModel>(rawDynamicDataToString);
                var dataStrJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(rawDynamicDataObject.data);
                var embedDataStr = JsonConvert.DeserializeObject<EmbedDataZaloPayRequestModel>((string)dataStrJson["embed_data"]);

                var mac = HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, key2, rawDynamicDataObject.data);

                // Kiểm tra callback hợp lệ (đến từ ZaloPay server)
                if (!rawDynamicDataObject.mac.Equals(mac))
                {
                    // Callback không hợp lệ
                    resultJson.return_code = -1;
                    resultJson.return_message = "mac not equal";
                    _logger.LogInformation("[Call back] Mac is not equal");
                }
                else
                {
                    // Thanh toán thành công
                    resultJson.return_code = 1;
                    resultJson.return_message = "success";
                    _logger.LogInformation("[Call back] Mac is equal and order is stored");

                    /*var dataStrJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(dataStr);
                    var embedStrJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(dataStrJson["embed_data"]);
                    //var dataStrJsonDeserializeClass = JsonConvert.DeserializeObject<EmbedDataRequestModel>(dataStrJson["embed_data"]);
                    var orderIds = JsonConvert.DeserializeObject<List<string>>(embedStrJson["OrderIds"]);
                    string countTemp = orderIds.Count.ToString();
                    _logger.LogInformation(countTemp);
                    List<string> list = orderIds as List<string>;
                     */
                }
                List<string> list = embedDataStr.OrderIds as List<string>;
                if (list != null)
                {
                    if (list.Any())
                    {
                        var orderUpdateModels = new List<OrderUpdateModel>();
                        list.ForEach(item =>
                        {
                            Guid orderId = new Guid();
                            Guid.TryParse(item, out orderId);
                            if (orderId != Guid.Empty)
                                orderUpdateModels.Add(new OrderUpdateModel() { Id = orderId });
                        });

                        if (orderUpdateModels.Any())
                        {
                            result = new ZaloPayCallPayViewModel()
                            {
                                zaloPayCallBackResponseModel = resultJson,
                                orderUpdateModels = orderUpdateModels
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultJson.return_code = 0; // ZaloPay server sẽ callback lại (tối đa 3 lần)
                resultJson.return_message = ex.Message;
                result.zaloPayCallBackResponseModel = resultJson;
                return result;
            }
            return result;
        }
        #endregion

        #region Notification
        public void SendNewZaloPayOrderNotification(List<OrderViewModel> orders)
        {
            if (orders.Any())
            {
                var notifications = new List<NotificationRequestModel>();
                var ShippingOrders = orders.Where(o => o.Type.Equals((byte)OrderType.Shipping)).ToList();
                var PickUpOrders = orders.Where(o => o.Type.Equals((byte)OrderType.PickUp)).ToList();
                GetShippingOrdersOfNotification(ShippingOrders, ref notifications);
                GetPickUpOrdersOfNotification(PickUpOrders, ref notifications);
                if (notifications.Any())
                    _notificationService.PushNewOrderNotification(notifications);
            }
        }

        public void SendNewShippingOrderNotification(List<OrderViewModel> orders)
        {
            if (orders.Any())
            {
                var notifications = new List<NotificationRequestModel>();
                GetShippingOrdersOfNotification(orders, ref notifications);
                if (notifications.Any())
                    _notificationService.PushNewOrderNotification(notifications);
            }
        }

        public void SendNewPickUpOrderNotification(List<OrderViewModel> orders)
        {
            if (orders.Any())
            {
                var notifications = new List<NotificationRequestModel>();
                GetPickUpOrdersOfNotification(orders, ref notifications);
                if (notifications.Any())
                    _notificationService.PushNewOrderNotification(notifications);
            }
        }

        public void SendCancelledOrderByIssuerNotification(OrderViewModel order)
        {
            if (order != null)
            {
                var validStatus = (byte)OrderStatus.Cancelled;
                if (order.Status.Equals(validStatus) && order.CustomerId.HasValue)
                {
                    if (order.OrderDetails != null)
                    {
                        if (order.OrderDetails.Any())
                        {
                            var issuer = order.OrderDetails.First().BookProduct.Issuer.User.Name;
                            var message = StatusExtension<OrderStatus>.GetStatus(validStatus);
                            var notification = new NotificationRequestModel()
                            {
                                UserIds = new List<Guid?>() { order.CustomerId },
                                UserRoles = new List<byte>() { (byte)BoekRole.Admin },
                                Status = order.Status,
                                StatusName = order.StatusName,
                                Message = $"{issuer} {message.ToLower()} {order.Id}"
                            };
                            _notificationService.PushCancelledOrderByIssuerNotification(notification);
                        }
                    }
                }
            }
        }

        public void SendCancelledOrderByCustomerNotification(OrderViewModel order)
        {
            if (order != null)
            {
                var validStatus = (byte)OrderStatus.Cancelled;
                if (order.Status.Equals(validStatus) && order.CustomerId.HasValue)
                {
                    if (order.OrderDetails != null)
                    {
                        if (order.OrderDetails.Any())
                        {
                            var customer = order.CustomerName;
                            var issuer = order.OrderDetails.First().BookProduct.IssuerId;
                            var message = StatusExtension<OrderStatus>.GetStatus(validStatus);
                            var notification = new NotificationRequestModel()
                            {
                                UserIds = new List<Guid?>() { issuer },
                                UserRoles = new List<byte>() { (byte)BoekRole.Admin },
                                Status = order.Status,
                                StatusName = order.StatusName,
                                Message = $"{customer} {message.ToLower()} {order.Id}"
                            };
                            _notificationService.PushCancelledOrderByCustomerNotification(notification);
                        }
                    }
                }
            }
        }
        #endregion

        #region Email
        public void SendNewZaloPayOrderEmail(Dictionary<OrderViewModel, string> orders)
        {
            var emailRequestModels = GetBoekOrderEmails(orders);
            SendBoekOrderEmails(emailRequestModels);
        }
        public void SendNewShippingOrderEmail(Dictionary<OrderViewModel, string> orders)
        {
            var emailRequestModels = GetBoekOrderEmails(orders);
            SendBoekOrderEmails(emailRequestModels);
        }

        public void SendNewPickUpOrderEmail(Dictionary<OrderViewModel, string> orders)
        {
            var emailRequestModels = GetBoekOrderEmails(orders);
            SendBoekOrderEmails(emailRequestModels);
        }

        public void SendAvailableOrderEmail(OrderViewModel order, string Note)
        {
            var status = StatusExtension<OrderStatus>.GetEnumStatus(order.Status);
            var role = BoekRole.Issuer;
            var type = StatusExtension<OrderType>.GetEnumStatus(order.Type);
            var email = GetEmail(status, type, order, Note, role);
            if (email.IsValid)
            {
                var request = _mapper.Map<EmailRequestModel>(email);
                _verificationService.SendEmail(request, false);
            }
        }

        public void SendUpdateAddressPickUpOrderEmail(OrderViewModel order, string Note)
        {
            var status = StatusExtension<OrderStatus>.GetEnumStatus(order.Status);
            var role = BoekRole.Issuer;
            var type = StatusExtension<OrderType>.GetEnumStatus(order.Type);
            var email = GetEmail(status, type, order, Note, role);
            if (email.IsValid)
            {
                var request = _mapper.Map<EmailRequestModel>(email);
                _verificationService.SendEmail(request, false);
            }
        }

        public void SendShippingOrderEmail(OrderViewModel order, string Note)
        {
            if (order.Customer == null)
            {
                var status = StatusExtension<OrderStatus>.GetEnumStatus(order.Status);
                var role = BoekRole.Issuer;
                var type = StatusExtension<OrderType>.GetEnumStatus(order.Type);
                var email = GetEmail(status, type, order, Note, role);
                if (email.IsValid)
                {
                    var request = _mapper.Map<EmailRequestModel>(email);
                    _verificationService.SendEmail(request, false);
                }
            }
        }

        public void SendReceivedOrderEmail(OrderViewModel order, string Note)
        {
            var status = StatusExtension<OrderStatus>.GetEnumStatus(order.Status);
            var role = BoekRole.Staff;
            var type = StatusExtension<OrderType>.GetEnumStatus(order.Type);
            var email = GetEmail(status, type, order, Note, role);
            if (email.IsValid)
            {
                var request = _mapper.Map<EmailRequestModel>(email);
                _verificationService.SendEmail(request, false);
            }
        }

        public void SendShippedOrderEmail(OrderViewModel order, string Note)
        {
            var status = StatusExtension<OrderStatus>.GetEnumStatus(order.Status);
            var role = BoekRole.Issuer;
            var type = StatusExtension<OrderType>.GetEnumStatus(order.Type);
            var email = GetEmail(status, type, order, Note, role);
            if (email.IsValid)
            {
                var request = _mapper.Map<EmailRequestModel>(email);
                _verificationService.SendEmail(request, false);
            }
        }

        public void SendCustomerCancelledOrderEmail(OrderViewModel order, string Note)
        {
            var status = StatusExtension<OrderStatus>.GetEnumStatus(order.Status);
            var role = BoekRole.Customer;
            var type = StatusExtension<OrderType>.GetEnumStatus(order.Type);
            var email = GetEmail(status, type, order, Note, role);
            if (email.IsValid)
            {
                var request = _mapper.Map<EmailRequestModel>(email);
                _verificationService.SendEmail(request, false);
            }
        }

        public void SendIssuerCancelledOrderEmail(OrderViewModel order, string Note)
        {
            var status = StatusExtension<OrderStatus>.GetEnumStatus(order.Status);
            var role = BoekRole.Issuer;
            var type = StatusExtension<OrderType>.GetEnumStatus(order.Type);
            var email = GetEmail(status, type, order, Note, role);
            if (email.IsValid)
            {
                var request = _mapper.Map<EmailRequestModel>(email);
                _verificationService.SendEmail(request, false);
            }
        }

        public void SendBoekCancelledOrderEmail(List<Guid> orderIds)
        {
            var orders = _unitOfWork.Orders.Get(o => orderIds.Contains(o.Id))
            .ProjectTo<OrderViewModel>(_mapper.ConfigurationProvider)
            .ToList();
            if (orders != null)
            {
                if (orders.Any())
                {
                    orders.ForEach(o =>
                    {
                        o = ServiceUtils.GetResponseDetail(o);
                        o = ServiceUtils.GetTotal(o, _mapper, _unitOfWork);
                    });
                }
            }
            var pairs = ConvertOrdersWithNote(orders);
            var emailRequestModels = GetBoekOrderEmails(pairs);
            SendBoekOrderEmails(emailRequestModels);
        }
        #endregion

        #region Utils

        private List<OrderViewModel> GetResponses(List<Guid?> ids)
        {
            var _orders = _unitOfWork.Orders.Get(q => ids.Contains(q.Id))
                .ProjectTo<OrderViewModel>(_mapper.ConfigurationProvider).ToList();
            if (_orders.Any())
            {
                _orders.ForEach(_order =>
                {
                    _order = ServiceUtils.GetResponseDetail(_order);
                    _order = ServiceUtils.GetTotal(_order, _mapper, _unitOfWork);
                });
            }
            return _orders.Any() ? _orders : null;
        }

        private OrderViewModel GetResponse(Guid? id)
        {
            var _order = _unitOfWork.Orders.Get(q => q.Id.Equals(id))
                .ProjectTo<OrderViewModel>(_mapper.ConfigurationProvider).SingleOrDefault();
            if (_order != null)
            {
                _order = ServiceUtils.GetResponseDetail(_order);
                _order = ServiceUtils.GetTotal(_order, _mapper, _unitOfWork);
            }
            return _order;
        }

        private IQueryable<OrderViewModel> GetOrderViewModelTotals(Guid? StaffId)
        {
            var result = _unitOfWork.Orders.Get(o =>
            o.Type.Equals((byte)OrderType.PickUp) &&
            o.Campaign.CampaignStaffs.Any(cs =>
            cs.StaffId.Equals(StaffId) &&
            cs.Status.Equals((byte)CampaignStaffStatus.Attended)))
            .ProjectTo<OrderViewModel>(_mapper.ConfigurationProvider);
            var list = result.ToList();
            list.ForEach(o => o = ServiceUtils.GetTotal(o, _mapper, _unitOfWork));
            return list.AsQueryable();
        }
        private IQueryable<OrderViewModel> GetOrderViewModelTotals()
        {
            var result = _unitOfWork.Orders.Get()
                .ProjectTo<OrderViewModel>(_mapper.ConfigurationProvider);
            var list = result.ToList();
            list.ForEach(o => o = ServiceUtils.GetTotal(o, _mapper, _unitOfWork));
            return list.AsQueryable();
        }

        private List<string> CheckCampaignDateTime(Campaign campaign, bool ShowErrorMessage = true)
        {
            var error = new List<string>();
            DateTime dateTimeNow = DateTime.Now;
            if (!(campaign.StartDate <= dateTimeNow && campaign.EndDate >= dateTimeNow))
            {
                error.Add(ErrorMessageConstants.INVALID_SCHEDULED_DATE);
                if (ShowErrorMessage)
                    BoekErrorMessage.ShowErrorMessage((int)HttpStatusCode.BadRequest, error.ToArray());
            }
            return error.Any() ? error : null;
        }

        private Order GetOrderDetailsAndBookProduct(Order order, IEnumerable<OrderDetail> orderDetails, List<BookProduct> bookProducts)
        {
            order.OrderDetails = orderDetails.ToList();
            if (bookProducts.Any())
            {
                bookProducts.ForEach(q =>
                {
                    order.OrderDetails.Where(t => t.BookProductId.Equals(q.Id)).Select(x => new BookProduct
                    {
                        Id = q.Id,
                        BookId = q.BookId,
                        CampaignId = q.CampaignId,
                        BookProductItems = q.BookProductItems
                    }).ToList();
                });
            }
            return order;
        }

        private double CheckCustomerPointFromOrders(Order order)
        {
            var total = _unitOfWork.Orders.GetTotal(order);
            return total > 0 ? (total / 1000.0) : 0;
        }

        private List<Order> CheckOrder(Order order, OrderType type, List<byte> validPayments, bool IsCreate = false)
        {
            var campaign = CheckCampaignOrder(order.CampaignId, type, IsCreate);
            if (type.Equals(OrderType.Shipping))
                return CheckShippingOrder(order, campaign, validPayments, IsCreate);
            if (type.Equals(OrderType.PickUp))
                return CheckPickUpOrder(order, campaign, validPayments, IsCreate);
            return null;
        }

        private List<Order> CheckGuestOrder(Order order, OrderType type, List<byte> validPayments, bool IsCreate = false)
        {
            var campaign = CheckCampaignOrder(order.CampaignId, type, IsCreate);
            if (type.Equals(OrderType.Shipping))
                return CheckGuestShippingOrder(order, campaign, validPayments, IsCreate);
            if (type.Equals(OrderType.PickUp))
                return CheckGuestPickUpOrder(order, campaign, validPayments, IsCreate);
            return null;
        }

        private Campaign CheckCampaignOrder(int? CampaignId, OrderType type, bool IsCreate = false, bool CheckCampaignType = true)
        {
            var _campaign = _unitOfWork.Campaigns.Get(c => c.Id.Equals(CampaignId))
            .Include(c => c.CampaignOrganizations)
            .ThenInclude(CampaignOrganization => CampaignOrganization.Schedules)
            .Include(c => c.Participants)
            .SingleOrDefault();
            if (_campaign == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.CAMPAIGN_ID,
                });
            var dateTimeNow = DateTime.Now;
            if (!dateTimeNow.IsInsideIn(dateTimeNow, (DateTime)_campaign.StartDate, (DateTime)_campaign.EndDate))
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    IsCreate ? ErrorMessageConstants.INSERT :
                    ErrorMessageConstants.UPDATE,
                    MessageConstants.MESSAGE_FAILED,
                    ErrorMessageConstants.CAMPAIGN_NOT_STARTED,
                });
            if (CheckCampaignType)
            {
                if (type.Equals(OrderType.PickUp) &&
            _campaign.Format.Equals((byte)CampaignFormat.Online))
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                    {
                    ErrorMessageConstants.CAMPAIGN_FORMAT,
                    MessageConstants.MESSAGE_INVALID
                    });
            }
            return _campaign;
        }

        private List<Order> CheckShippingOrder(Order order, Campaign campaign, List<byte> validPayments, bool IsCreate = false)
        {
            var result = new List<Order>();
            var UserId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            var customer = CheckCustomer(UserId, IsCreate);
            CheckPayment(order.Payment, validPayments);
            var BookProductsByIssuer = new Dictionary<Guid?, List<Guid?>>();
            CheckOrderDetails(order.OrderDetails.ToList(), campaign, ref BookProductsByIssuer);
            result = GetShippingOrder(order, customer, BookProductsByIssuer, IsCreate);
            if (result != null)
            {
                if (result.Any())
                {
                    var NumberOfOrders = result.Count();
                    CheckFreight(order.Freight, OrderType.Shipping, order.Address, campaign, NumberOfOrders);
                    if (NumberOfOrders >= 2)
                    {
                        var Freight = order.Freight / NumberOfOrders;
                        result.ForEach(item => item.Freight = Freight);
                    }
                }
            }
            return result;
        }

        private List<Order> CheckPickUpOrder(Order order, Campaign campaign, List<byte> validPayments, bool IsCreate = false)
        {
            var result = new List<Order>();
            var UserId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            var IsCustomer = ServiceUtils.CheckRole(_httpContextAccessor, BoekRole.Customer);
            var customer = new User();
            var campaignStaff = new CampaignStaff();
            if (IsCustomer)
                customer = CheckCustomer(UserId, IsCreate);
            if (!IsCustomer)
            {
                campaignStaff = CheckStaff(UserId, campaign.Id, IsCreate);
                CheckPickUpAddress(order.Address, campaign);
                if (order.CustomerId.HasValue)
                    customer = CheckCustomer(order.CustomerId, IsCreate);
                else
                    customer = null;
            }
            CheckPayment(order.Payment, validPayments);
            CheckFreight(order.Freight, OrderType.PickUp);
            var BookProductsByIssuer = new Dictionary<Guid?, List<Guid?>>();
            CheckOrderDetails(order.OrderDetails.ToList(), campaign, ref BookProductsByIssuer);
            result = GetPickUpOrder(order, customer, campaignStaff, BookProductsByIssuer, IsCreate, IsCustomer);
            return result;
        }

        private List<Order> CheckGuestShippingOrder(Order order, Campaign campaign, List<byte> validPayments, bool IsCreate = false)
        {
            var result = new List<Order>();
            CheckPayment(order.Payment, validPayments);
            var BookProductsByIssuer = new Dictionary<Guid?, List<Guid?>>();
            CheckOrderDetails(order.OrderDetails.ToList(), campaign, ref BookProductsByIssuer);
            result = GetGuestShippingOrder(order, BookProductsByIssuer, IsCreate);
            if (result != null)
            {
                if (result.Any())
                {
                    var NumberOfOrders = result.Count();
                    CheckFreight(order.Freight, OrderType.Shipping, order.Address, campaign, NumberOfOrders);
                    if (NumberOfOrders >= 2)
                    {
                        var Freight = order.Freight / NumberOfOrders;
                        result.ForEach(item => item.Freight = Freight);
                    }
                }
            }
            return result;
        }

        private List<Order> CheckGuestPickUpOrder(Order order, Campaign campaign, List<byte> validPayments, bool IsCreate = false)
        {
            var result = new List<Order>();
            CheckPayment(order.Payment, validPayments);
            CheckFreight(order.Freight, OrderType.PickUp);
            var BookProductsByIssuer = new Dictionary<Guid?, List<Guid?>>();
            CheckOrderDetails(order.OrderDetails.ToList(), campaign, ref BookProductsByIssuer);
            result = GetGuestPickUpOrder(order, BookProductsByIssuer, IsCreate);
            return result;
        }

        //TO-DO: Shipping
        //Pick-up - Customer
        //Pick-up - if customer id is not null
        private User CheckCustomer(Guid? UserId, bool IsCreate = false)
        {
            if (!IsCreate)
            {
                var _UserId = ServiceUtils.GetUserInfo(_httpContextAccessor);
                if (!_UserId.Equals(UserId))
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                    {
                        ErrorMessageConstants.ORDER,
                        MessageConstants.MESSAGE_NOT_BELONGING,
                        ErrorMessageConstants.CUSTOMER
                    });
            }
            var customer = _unitOfWork.Users.Get(UserId);
            if (customer == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.USER_ID
                });
            if (!(bool)customer.Status)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                {
                    ErrorMessageConstants.ACCOUNT_STATUS,
                    ErrorMessageConstants.CUSTOMER,
                    MessageConstants.MESSAGE_INVALID,
                });
            return customer;
        }
        //TO-DO: Pick-up Staff - Create, Update
        private CampaignStaff CheckStaff(Guid? StaffId, int CampaignId, bool IsCreate = false)
        {
            if (!IsCreate)
            {
                var _UserId = ServiceUtils.GetUserInfo(_httpContextAccessor);
                if (!_UserId.Equals(StaffId))
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                    {
                        ErrorMessageConstants.ORDER,
                        MessageConstants.MESSAGE_NOT_BELONGING,
                        ErrorMessageConstants.CAMPAIGN_STAFF
                    });
            }
            var campaignStaff = _unitOfWork.CampaignStaffs.Get(cs => cs.StaffId.Equals(StaffId) &&
            cs.CampaignId.Equals(CampaignId)).SingleOrDefault();
            if (campaignStaff == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.CAMPAIGN_STAFF_ID
                });
            if (!campaignStaff.Status.Equals((byte)CampaignStaffStatus.Attended))
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    IsCreate ? ErrorMessageConstants.INSERT :
                    ErrorMessageConstants.UPDATE,
                    ErrorMessageConstants.ORDER,
                    MessageConstants.MESSAGE_FAILED,
                    ErrorMessageConstants.CAMPAIGN_STAFF_UNATTENDED_STATUS
                });
            return campaignStaff;
        }

        //TO-DO: Shipping
        //Pick-up Customer
        //Pick-up Staff
        public void CheckPayment(byte Payment, List<byte> validPayments)
        {
            var payments = new List<byte>()
            {
                (byte) OrderPayment.Unpaid,
                (byte) OrderPayment.ZaloPay,
                (byte) OrderPayment.Cash
            };
            if (!payments.Contains(Payment) || !validPayments.Contains(Payment))
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                {
                    ErrorMessageConstants.ORDER_PAYMENT_TYPE,
                    MessageConstants.MESSAGE_INVALID,
                });
        }

        //TO-DO: Shipping
        //Pick-up
        public void CheckFreight(decimal? Freight, OrderType type, string AddressOrder = null, Campaign campaign = null, int? NumberOfOrders = null)
        {
            if (!Freight.HasValue)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.PreconditionRequired), new string[]
                {
                    MessageConstants.MESSAGE_REQUIRED,
                    ErrorMessageConstants.ORDER_FREIGHT
                });
            if (type.Equals(OrderType.PickUp) && Freight != 0)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                {
                    ErrorMessageConstants.ORDER_FREIGHT,
                    MessageConstants.MESSAGE_INVALID
                });
            if (type.Equals(OrderType.Shipping))
            {
                if (NumberOfOrders.HasValue)
                    Freight /= NumberOfOrders;
                var validFreights = new List<decimal?>()
                {
                    (decimal)OrderFreight.Inner,
                    (decimal)OrderFreight.Outside
                };
                if (!validFreights.Contains(Freight))
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                    {
                        ErrorMessageConstants.ORDER_FREIGHT,
                        MessageConstants.MESSAGE_INVALID
                    });
                if (campaign.Format.Equals((byte)CampaignFormat.Online) && !Freight.Equals((decimal)OrderFreight.Inner))
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                    {
                        ErrorMessageConstants.ORDER_FREIGHT,
                        MessageConstants.MESSAGE_TO_BE,
                        ((decimal)OrderFreight.Inner).ToString()
                    });
                if (campaign.Format.Equals((byte)CampaignFormat.Offline))
                {
                    if (string.IsNullOrEmpty(campaign.Address))
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.PreconditionRequired), new string[]
                        {
                            MessageConstants.MESSAGE_REQUIRED,
                            ErrorMessageConstants.ADDRESS_OF,
                            ErrorMessageConstants.CAMPAIGN.ToLower()
                        });
                    if ((bool)campaign.IsRecurring)
                    {
                        var schedules = new List<Schedule>()
                        {
                            new Schedule()  {Address = campaign.Address}
                        };
                        campaign.CampaignOrganizations.Select(cos => cos.Schedules).ToList().ForEach(s => schedules.AddRange(s));
                        var provinces = _unitOfWork.Schedules.GetProvincesFromSchedules(schedules);
                        if (provinces == null)
                            BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                            {
                                ErrorMessageConstants.NOT_FOUND,
                                ErrorMessageConstants.ADDRESS_PROVINCE,
                                ErrorMessageConstants.CAMPAIGN_ORGANIZATION_SCHEDULE
                            });
                        var IsInner = provinces.Any(p => AddressOrder.ToLower().Trim().Contains(p.NameWithType.ToLower().Trim()));
                        if (!IsInner && Freight.Equals((decimal)OrderFreight.Inner) ||
                        IsInner && Freight.Equals((decimal)OrderFreight.Outside))
                            BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                            {
                                ErrorMessageConstants.ORDER_FREIGHT,
                                MessageConstants.MESSAGE_INVALID,
                                ErrorMessageConstants.ORDER_INVALID_INNER_OR_OUTSIDE_FREIGHT
                            });
                    }
                    else
                    {
                        var address = campaign.Address.Split(",").Last().ToLower().Trim();
                        var province = ProvincesList.PROVINCES.SingleOrDefault(p => address.Equals(p.NameWithType.ToLower().Trim()));
                        if (province == null)
                            BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                            {
                                ErrorMessageConstants.NOT_FOUND,
                                ErrorMessageConstants.ADDRESS_PROVINCE,
                                ErrorMessageConstants.CAMPAIGN.ToLower()
                            });
                        if (!AddressOrder.ToLower().Trim().Contains(address) && Freight.Equals((decimal)OrderFreight.Inner) ||
                        AddressOrder.ToLower().Trim().Contains(address) && Freight.Equals((decimal)OrderFreight.Outside))
                            BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                            {
                                ErrorMessageConstants.ORDER_FREIGHT,
                                MessageConstants.MESSAGE_INVALID,
                                ErrorMessageConstants.ORDER_INVALID_INNER_OR_OUTSIDE_FREIGHT
                            });
                    }
                }
            }
        }

        //TO-DO: All
        public List<OrderDetail> CheckOrderDetails(List<OrderDetail> orderDetails, Campaign campaign, ref Dictionary<Guid?, List<Guid?>> BookProductsByIssuer)
        {
            if (orderDetails == null || !orderDetails.Any())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.PreconditionRequired), new string[]
                {
                    MessageConstants.MESSAGE_REQUIRED,
                    ErrorMessageConstants.ORDER_DETAIL
                });
            if (campaign.Format.Equals((byte)CampaignFormat.Offline))
            {
                if (orderDetails.Any(od => od.WithAudio == true || od.WithPdf == true))
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                    {
                        ErrorMessageConstants.ORDER_OFFLINE_CAMPAIGN_WITH_AUDIO_OR_PDF
                    });
            }
            var issuerIds = campaign.Participants.Where(p =>
            p.Status.Equals((byte)ParticipantStatus.Accepted) ||
            p.Status.Equals((byte)ParticipantStatus.Approved))
            .Select(p => p.IssuerId).ToList();
            if (!issuerIds.Any())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                {
                    ErrorMessageConstants.PARTICIPANT,
                    MessageConstants.MESSAGE_INVALID
                });
            CheckDuplicatedBookProduct(orderDetails);
            var issuers = new Dictionary<Guid?, List<Guid?>>();
            orderDetails.ForEach(ods =>
            {
                var bookProduct = _unitOfWork.BookProducts.Get(bp =>
                bp.Id.Equals(ods.BookProductId))
                .Include(bp => bp.BookProductItems)
                .Include(bp => bp.Book)
                .SingleOrDefault();
                if (bookProduct == null)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                    {
                        ErrorMessageConstants.NOT_FOUND,
                        ErrorMessageConstants.BOOK_PRODUCT_ID
                    });
                if (!issuerIds.Contains(bookProduct.IssuerId))
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                    {
                        ErrorMessageConstants.UNATTENDED_PARTICIPANT,
                        bookProduct.IssuerId.ToString()
                    });
                if (!bookProduct.Status.Equals((byte)BookProductStatus.Sale))
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                    {
                        ErrorMessageConstants.BOOK_PRODUCT_STATUS,
                        bookProduct.Id.ToString(),
                        MessageConstants.MESSAGE_INVALID
                    });
                CheckSaleQuantity(ods.Quantity, bookProduct.SaleQuantity, bookProduct.Title);
                CheckBookProductByType(bookProduct, ods.WithPdf, ods.WithAudio);
                ods.Price = bookProduct.SalePrice;
                ods.Discount = bookProduct.Discount;
                if (!issuers.Keys.Contains(bookProduct.IssuerId))
                    issuers.Add(bookProduct.IssuerId, new List<Guid?>() { bookProduct.Id });
                else
                {
                    if (!issuers[bookProduct.IssuerId].Contains(bookProduct.Id))
                        issuers[bookProduct.IssuerId].Add(bookProduct.Id);
                }
            });
            if (issuers.Any())
                BookProductsByIssuer = issuers;
            return orderDetails;
        }

        public void CheckDuplicatedBookProduct(List<OrderDetail> orderDetails)
        {
            var bookProducts = orderDetails.GroupBy(od => od.BookProductId)
            .Where(od => od.Count() > 1)
            .Select(od => od.Key.ToString());
            if (bookProducts.Any())
            {
                var errorMessages = new List<string>()
                {
                    MessageConstants.MESSAGE_DUPLICATED_INFO,
                    ErrorMessageConstants.BOOK_PRODUCT_ID,
                };
                errorMessages.AddRange(bookProducts);
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), errorMessages.ToArray());
            }
        }

        public void CheckSaleQuantity(int? Quantity, int SaleQuantity, string Title = null)
        {
            if (!Quantity.HasValue)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.PreconditionRequired), new string[]
                {
                    MessageConstants.MESSAGE_REQUIRED,
                    ErrorMessageConstants.ORDER_DETAIL_QUANTITY,
                    Title ?? ""
                });
            if (SaleQuantity <= 0)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                {
                    Title ?? "",
                    ErrorMessageConstants.NOT_ENOUGH_QUANTITY_WAREHOUSE.ToLower()
                });
            if (Quantity <= 0 || Quantity > SaleQuantity)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                {
                    ErrorMessageConstants.ORDER_DETAIL_QUANTITY,
                    Title ?? "",
                    MessageConstants.MESSAGE_INVALID
                });

        }

        public void CheckBookProductByType(BookProduct bookProduct, bool? WithPdf, bool? WithAudio)
        {
            if (bookProduct.Type.Equals((byte)BookType.Odd))
            {
                if (!bookProduct.PdfExtraPrice.HasValue && (bool)WithPdf)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                    {
                        ErrorMessageConstants.CONFLICT_BOOK_PRODUCT_WITH_PDF_01
                    });
                if (!bookProduct.AudioExtraPrice.HasValue && (bool)WithAudio)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                    {
                        ErrorMessageConstants.CONFLICT_BOOK_PRODUCT_WITH_AUDIO_01
                    });
            }
            else
            {
                if ((bool)WithAudio)
                {
                    bookProduct.BookProductItems.ToList()
                        .ForEach(itm =>
                        {
                            if (!itm.AudioExtraPrice.HasValue)
                                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                                {
                                    bookProduct.Id.ToString(),
                                    ErrorMessageConstants.CONFLICT_BOOK_PRODUCT_WITH_PDF_02
                                });
                        });
                }
                if ((bool)WithPdf)
                {
                    bookProduct.BookProductItems.ToList()
                        .ForEach(itm =>
                        {
                            if (!itm.PdfExtraPrice.HasValue)
                                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                                {
                                    bookProduct.Id.ToString(),
                                    ErrorMessageConstants.CONFLICT_BOOK_PRODUCT_WITH_AUDIO_02
                                });
                        });
                }
            }
        }

        public void CheckPickUpAddress(string Address, Campaign campaign)
        {
            if (string.IsNullOrEmpty(campaign.Address))
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.PreconditionRequired), new string[]
                {
                    MessageConstants.MESSAGE_REQUIRED,
                    ErrorMessageConstants.ADDRESS_OF,
                    ErrorMessageConstants.CAMPAIGN.ToLower()
                });
            if (string.IsNullOrEmpty(Address))
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.PreconditionRequired), new string[]
                {
                    MessageConstants.MESSAGE_REQUIRED,
                    ErrorMessageConstants.ADDRESS_OF,
                    ErrorMessageConstants.ORDER.ToLower()
                });
            Address = Address.ToLower().Trim();
            var addresses = new List<string>() { campaign.Address };
            if ((bool)campaign.IsRecurring)
            {
                campaign.CampaignOrganizations.Select(cos => cos.Schedules).ToList().ForEach(s =>
                {
                    var temp = s.Where(schedule => !string.IsNullOrEmpty(schedule.Address)).Select(s => s.Address);
                    if (temp.Any())
                        addresses.AddRange(temp);
                });
            }
            var result = addresses.Any(a => a.ToLower().Trim().Contains(Address));
            if (!result)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                {
                        ErrorMessageConstants.ADDRESS_OF,
                        ErrorMessageConstants.ORDER.ToLower(),
                        MessageConstants.MESSAGE_INVALID
                });
        }

        public List<Order> GetShippingOrder(Order order, User customer, Dictionary<Guid?, List<Guid?>> BookProductsByIssuer, bool IsCreate = false)
        {
            var list = new List<Order>();
            if (IsCreate)
            {
                order.CustomerId = customer.Id;
                order.CustomerName = customer.Name;
                order.CustomerPhone = customer.Phone;
                order.CustomerEmail = customer.Email;
                order.Type = (byte)OrderType.Shipping;
                order.Status = (byte)OrderStatus.Processing;
                order.OrderDate = DateTime.Now;
                if (BookProductsByIssuer.Count() >= 2)
                {
                    foreach (var item in BookProductsByIssuer)
                        list.Add(ConvertOrder(order, item));
                    var temp = _mapper.Map<BasicOrderRequestModel>(order);
                    list.ForEach(item => _mapper.Map(temp, item));
                }
                else
                {
                    order.Id = Guid.NewGuid();
                    order.Code = GenerateCode();
                    list.Add(order);
                }
            }
            return list;
        }
        public List<Order> GetPickUpOrder(Order order, User customer, CampaignStaff campaignStaff, Dictionary<Guid?, List<Guid?>> BookProductsByIssuer, bool IsCreate = false, bool IsCustomer = true)
        {
            var list = new List<Order>();
            if (IsCreate)
            {
                order = GetBasicOrderInfoByRole(order, customer, campaignStaff, IsCreate, IsCustomer);
                order.OrderDate = DateTime.Now;
                if (BookProductsByIssuer.Count() >= 2)
                {
                    foreach (var item in BookProductsByIssuer)
                        list.Add(ConvertOrder(order, item));
                    var temp = _mapper.Map<BasicOrderRequestModel>(order);
                    list.ForEach(item => _mapper.Map(temp, item));
                }
                else
                {
                    order.Id = Guid.NewGuid();
                    order.Code = GenerateCode();
                    list.Add(order);
                }
            }
            return list;
        }

        private Order GetBasicOrderInfoByRole(Order order, User customer, CampaignStaff campaignStaff, bool IsCreate = false, bool IsCustomer = true)
        {
            if (IsCreate)
            {
                if (IsCustomer)
                {
                    order.CustomerId = customer.Id;
                    order.CustomerName = customer.Name;
                    order.CustomerPhone = customer.Phone;
                    order.CustomerEmail = customer.Email;
                    order.Status = (byte)OrderStatus.Processing;
                }
                else
                {
                    if (customer != null)
                    {
                        order.CustomerId = customer.Id;
                        order.CustomerName = customer.Name;
                        order.CustomerPhone = customer.Phone;
                        order.CustomerEmail = customer.Email;
                    }
                    order.CampaignStaffId = campaignStaff.Id;
                    order.ReceivedDate = DateTime.Now;
                    order.Status = (byte)OrderStatus.Received;
                }
                order.Type = (byte)OrderType.PickUp;
            }
            return order;
        }

        public List<Order> GetGuestShippingOrder(Order order, Dictionary<Guid?, List<Guid?>> BookProductsByIssuer, bool IsCreate = false)
        {
            var list = new List<Order>();
            if (IsCreate)
            {
                order.Type = (byte)OrderType.Shipping;
                order.Status = (byte)OrderStatus.Processing;
                order.OrderDate = DateTime.Now;
                if (BookProductsByIssuer.Count() >= 2)
                {
                    foreach (var item in BookProductsByIssuer)
                        list.Add(ConvertOrder(order, item));
                    var temp = _mapper.Map<BasicOrderRequestModel>(order);
                    list.ForEach(item => _mapper.Map(temp, item));
                }
                else
                {
                    order.Id = Guid.NewGuid();
                    order.Code = GenerateCode();
                    list.Add(order);
                }
            }
            return list;
        }

        private Order ConvertOrder(Order order, KeyValuePair<Guid?, List<Guid?>> item)
        {
            var temp = new Order();
            temp.Id = Guid.NewGuid();
            temp.Code = GenerateCode();
            var BookProductIds = item.Value;
            temp.OrderDetails = order.OrderDetails.Where(od => BookProductIds.Contains(od.BookProductId)).ToList();
            return temp;
        }

        public List<Order> GetGuestPickUpOrder(Order order, Dictionary<Guid?, List<Guid?>> BookProductsByIssuer, bool IsCreate = false)
        {
            var list = new List<Order>();
            if (IsCreate)
            {
                order.Status = (byte)OrderStatus.Processing;
                order.Type = (byte)OrderType.PickUp;
                order.OrderDate = DateTime.Now;
                if (BookProductsByIssuer.Count() >= 2)
                {
                    foreach (var item in BookProductsByIssuer)
                        list.Add(ConvertOrder(order, item));
                    var temp = _mapper.Map<BasicOrderRequestModel>(order);
                    list.ForEach(item => _mapper.Map(temp, item));
                }
                else
                {
                    order.Id = Guid.NewGuid();
                    order.Code = GenerateCode();
                    list.Add(order);
                }
            }
            return list;
        }

        private void UpdateSaleQuantityBookProduct(List<OrderDetail> orderDetails, bool IsIncrease = false)
        {
            if (orderDetails.Any())
            {
                var bookProductIds = orderDetails.Select(od => od.BookProductId);
                var bookProducts = _unitOfWork.BookProducts.Get(bps => bookProductIds.Contains(bps.Id)).ToList();
                if (bookProducts.Any())
                {
                    bookProducts.ForEach(bps =>
                    {
                        var temp = orderDetails.SingleOrDefault(od => od.BookProductId.Equals(bps.Id));
                        if (temp != null)
                            bps = UpdateSaleQuantity(bps, temp.Quantity, IsIncrease);
                    });
                    _unitOfWork.BookProducts.UpdateRange(bookProducts);
                    if (!_unitOfWork.Save())
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                            ErrorMessageConstants.UPDATE,
                            ErrorMessageConstants.BOOK_PRODUCT_SALE_QUANTITY.ToLower(),
                            MessageConstants.MESSAGE_FAILED
                        });
                }
            }
        }

        private BookProduct UpdateSaleQuantity(BookProduct bookProduct, int Quantity, bool IsIncrease = false)
        {
            bookProduct.SaleQuantity = IsIncrease ? bookProduct.SaleQuantity + Quantity :
            bookProduct.SaleQuantity - Quantity;
            return bookProduct;
        }

        private void UpdateCustomerPoint(List<Order> orders)
        {
            orders = orders.Where(o => o.CustomerId.HasValue).ToList();
            if (orders.Any())
            {
                var customerOrders = orders.GroupBy(o => o.CustomerId).ToList();
                if (customerOrders.Any())
                {
                    var customerIds = customerOrders.Select(cos => cos.Key).ToList();
                    var customers = _unitOfWork.Customers.Get(c => customerIds.Contains(c.Id)).ToList();
                    customerOrders.ForEach(cos =>
                    {
                        double point = 0;
                        cos.Select(cos => cos).ToList().ForEach(o => point += CheckCustomerPointFromOrders(o));
                        if (point == 0)
                            BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.InternalServerError), new string[] { ErrorMessageConstants.ERROR_POINT_CALCULATION });
                        else
                        {
                            var index = customers.FindIndex(c => c.Id.Equals(cos.Key));
                            if (index > -1)
                                customers[index].Point += (int)point;
                            else
                                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                                {
                                    ErrorMessageConstants.NOT_FOUND,
                                    ErrorMessageConstants.CUSTOMER
                                });
                        }
                    });
                    _unitOfWork.Customers.UpdateRange(customers);
                    if (!_unitOfWork.Save())
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                        {
                            ErrorMessageConstants.UPDATE,
                            ErrorMessageConstants.CUSTOMER_POINT.ToLower(),
                            MessageConstants.MESSAGE_FAILED
                        });
                }
            }
        }

        private List<string> CheckUpdateStatus(Order _order, List<byte?> validStatusList, List<byte?> validTypeList, OrderStatus status, bool checkCancelled = false, bool CheckBookProductQuantity = true, bool ShowErrorMessage = true, bool CheckSameStatus = true)
        {
            var errorMessages = new List<string>();
            var orderIdMess = $" - Order Id: {_order.Id}";
            if (_order == null)
            {
                var temp = new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.ORDER.ToLower()
                };
                if (ShowErrorMessage)
                    BoekErrorMessage.ShowErrorMessage((int)HttpStatusCode.NotFound, temp);
                else
                    errorMessages.AddRange(temp);
            }

            var error = CheckValidOrderType(_order.Type, validTypeList, ShowErrorMessage);
            if (error != null & ShowErrorMessage)
            {
                error.ForEach(e => e += orderIdMess);
                errorMessages.AddRange(error);
            }

            var _campaign = _unitOfWork.Campaigns.Get(_order.CampaignId);
            if (_campaign == null)
            {
                var temp = new string[] { ErrorMessageConstants.NOT_FOUND + ErrorMessageConstants.CAMPAIGN.ToLower() + orderIdMess };
                if (ShowErrorMessage)
                    BoekErrorMessage.ShowErrorMessage((int)HttpStatusCode.NotFound, temp);
                else
                    errorMessages.AddRange(temp);
            }
            error = CheckCampaignDateTime(_campaign, ShowErrorMessage);
            if (error != null & ShowErrorMessage)
            {
                error.ForEach(e => e += orderIdMess);
                errorMessages.AddRange(error);
            }
            var _orderDetail = _order.OrderDetails.ToList();
            if (!_orderDetail.Any())
            {
                var temp = new string[] { ErrorMessageConstants.NO_ORDER_DETAIL + orderIdMess };
                if (ShowErrorMessage)
                    BoekErrorMessage.ShowErrorMessage((int)HttpStatusCode.NotFound, temp);
                else
                    errorMessages.AddRange(temp);
            }
            error = CheckOrderDetailsQuantity(_orderDetail, CheckBookProductQuantity, ShowErrorMessage);
            if (error != null & ShowErrorMessage)
            {
                error.ForEach(e => e += orderIdMess);
                errorMessages.AddRange(error);
            }
            if (CheckSameStatus)
            {
                if (_order.Status.Equals((byte)status))
                {
                    var temp = new string[] { ErrorMessageConstants.ORDER_SAME_STATUS + orderIdMess };
                    if (ShowErrorMessage)
                        BoekErrorMessage.ShowErrorMessage((int)HttpStatusCode.BadRequest, temp);
                    else
                        errorMessages.AddRange(temp);
                }
            }
            if (checkCancelled)
            {
                error = CheckCancelledUpdateOrder(_order, ShowErrorMessage);
                if (error != null & ShowErrorMessage)
                {
                    error.ForEach(e => e += orderIdMess);
                    errorMessages.AddRange(error);
                }
            }
            error = CheckValidStatus(_order, validStatusList, ShowErrorMessage);
            if (error != null & ShowErrorMessage)
            {
                error.ForEach(e => e += orderIdMess);
                errorMessages.AddRange(error);
            }
            return errorMessages.Any() ? errorMessages : null;
        }

        private List<string> CheckOrderDetailsQuantity(List<OrderDetail> orderDetails, bool CheckBookProductQuantity = true, bool ShowErrorMessage = true)
        {
            var error = new List<string>();
            orderDetails.ForEach(od =>
            {
                if (od.BookProduct.SaleQuantity <= 0)
                {
                    if (ShowErrorMessage)
                        BoekErrorMessage.ShowErrorMessage((int)HttpStatusCode.Conflict, new string[] { ErrorMessageConstants.NO_QUANTITY_IN_WAREHOUSE, $"- Order Id: {od.OrderId}" });
                    else
                        error.Add(ErrorMessageConstants.NO_QUANTITY_IN_WAREHOUSE + $"- {ErrorMessageConstants.ORDER_ID}: {od.OrderId}");
                }
                if (CheckBookProductQuantity)
                    if (od.BookProduct.SaleQuantity < od.Quantity)
                    {
                        if (ShowErrorMessage)
                            BoekErrorMessage.ShowErrorMessage((int)HttpStatusCode.Conflict, new string[] { ErrorMessageConstants.NOT_ENOUGH_QUANTITY_WAREHOUSE, $"- Order Id: {od.OrderId}" });
                        else
                            error.Add(ErrorMessageConstants.NOT_ENOUGH_QUANTITY_WAREHOUSE + $"- {ErrorMessageConstants.ORDER_ID}: {od.OrderId}");
                    }
            });
            return error.Any() ? error : null;
        }

        private List<string> CheckCancelledUpdateOrder(Order _order, bool ShowErrorMessage = true)
        {
            var error = new List<string>();
            if (_order.Status.Equals((byte)OrderStatus.Cancelled))
            {
                if (ShowErrorMessage)
                    BoekErrorMessage.ShowErrorMessage((int)HttpStatusCode.BadRequest, new string[] { ErrorMessageConstants.CANCELLED_ORDER, $"- Order Id: {_order.Id}" });
                else
                    error.Add(ErrorMessageConstants.CANCELLED_ORDER + $"- Order Id: {_order.Id}");
            }
            return error.Any() ? error : null;
        }

        private List<string> CheckValidStatus(Order _order, List<byte?> validList, bool ShowErrorMessage = true)
        {
            var error = new List<string>();
            if (!validList.Contains(_order.Status))
            {
                if (ShowErrorMessage)
                    BoekErrorMessage.ShowErrorMessage((int)HttpStatusCode.BadRequest, new string[] { ErrorMessageConstants.INVALID_ORDER_STATUS_RANGE, $"- Order Id: {_order.Id}" });
                else
                    error.Add(ErrorMessageConstants.INVALID_ORDER_STATUS_RANGE + $"- Order Id: {_order.Id}");
            }
            return error.Any() ? error : null;
        }

        private void CheckCustomer(Guid? CustomerId)
        {
            var userId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            if (!CustomerId.Equals(userId))
            {
                BoekErrorMessage.ShowErrorMessage((int)HttpStatusCode.NotFound, new string[]
                {
                    ErrorMessageConstants.USER.ToLower(),
                    MessageConstants.MESSAGE_INVALID
                });
            }
        }
        private void CheckIssuerId(Guid? IssuerId)
        {
            var userId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            if (!IssuerId.Equals(userId))
            {
                BoekErrorMessage.ShowErrorMessage((int)HttpStatusCode.BadRequest, new string[]
                {
                    ErrorMessageConstants.ORDER,
                    MessageConstants.MESSAGE_NOT_BELONGING,
                    ErrorMessageConstants.ISSUER
                });
            }
        }

        private List<string> UpdateOrderByStatus(Order _order, OrderStatus status, bool IsCreated = false, bool UpdateAddressOrder = false, bool ShowErrorMessage = true)
        {
            var error = new List<string>();
            if (!UpdateAddressOrder)
            {
                if (status == OrderStatus.PickUpAvailable)
                {
                    _order.AvailableDate = DateTime.Now;
                }
                else if (status == OrderStatus.Shipping)
                {
                    _order.ShippingDate = DateTime.Now;
                }
                else if (status == OrderStatus.Shipped)
                {
                    _order.ShippedDate = DateTime.Now;
                }
                else if (status == OrderStatus.Received)
                {
                    _order.ReceivedDate = DateTime.Now;
                }
                else if (status == OrderStatus.Cancelled)
                {
                    _order.CancelledDate = DateTime.Now;
                }
            }

            _order.Status = (byte)status;
            _unitOfWork.Orders.Update(_order);
            if (!_unitOfWork.Save())
            {
                if (ShowErrorMessage)
                    BoekErrorMessage.ShowErrorMessage((int)HttpStatusCode.Conflict, new string[]
                    {
                        IsCreated ? ErrorMessageConstants.INSERT : ErrorMessageConstants.UPDATE,
                        MessageConstants.MESSAGE_FAILED
                    });
                else
                    error.Add(IsCreated ? ErrorMessageConstants.INSERT : ErrorMessageConstants.UPDATE + " " + MessageConstants.MESSAGE_FAILED + $"- Id:{_order.Id}");
            }
            return error.Any() ? error : null;
        }

        private void CheckCancelStatus(Order _order, List<byte?> validList, OrderStatus status)
        {
            if (_order == null)
            {
                BoekErrorMessage.ShowErrorMessage((int)HttpStatusCode.NotFound, new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.ORDER.ToLower()
                });
            }

            if (_order.Status.Equals((byte)status))
                BoekErrorMessage.ShowErrorMessage((int)HttpStatusCode.BadRequest, new string[]
                {
                    ErrorMessageConstants.ORDER_SAME_STATUS
                });
            var _campaign = _unitOfWork.Campaigns.Get(_order.CampaignId);
            CheckCampaignDateTime(_order.Campaign);
            CheckValidStatus(_order, validList);
        }

        private string GenerateCode()
        {
            string Code = null;
            var flag = true;
            while (flag)
            {
                var _random = new Random();
                var temp = $"#{_random.NextInt64(1000000000000, 9999999999999)}";
                flag = _unitOfWork.Orders.Get(o => o.Code.Contains(temp)).Any();
                if (!flag)
                    Code = temp;
            }
            return Code;
        }

        private List<string> CheckValidOrderType(byte? type, List<byte?> validList, bool ShowErrorMessage = true)
        {
            if (!validList.Contains(type))
            {
                var error = new List<string>() { $"{ErrorMessageConstants.ORDER_TYPE} {MessageConstants.MESSAGE_INVALID}" };
                if (ShowErrorMessage)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), error.ToArray());
                else
                    return error;
            }
            return null;
        }
        #region Email

        private List<EmailRequestModel> GetBoekOrderEmails(Dictionary<OrderViewModel, string> orders)
        {
            var result = new List<EmailRequestModel>();
            if (orders != null)
            {
                if (orders.Any())
                {
                    orders.ToList().ForEach(o =>
                    {
                        var key = o.Key;
                        var value = o.Value;
                        var status = StatusExtension<OrderStatus>.GetEnumStatus(key.Status);
                        var role = BoekRole.Admin;
                        var type = StatusExtension<OrderType>.GetEnumStatus(key.Type);
                        var email = GetEmail(status, type, key, value, role, true);
                        if (email.IsValid)
                        {
                            var request = _mapper.Map<EmailRequestModel>(email);
                            result.Add(request);
                        }
                    });
                }
            }
            return result;
        }

        private void SendBoekOrderEmails(List<EmailRequestModel> emailRequestModels)
        {
            if (emailRequestModels != null)
            {
                if (emailRequestModels.Any())
                    emailRequestModels.ForEach(request => _verificationService.SendEmail(request, false));
            }
        }

        private EmailDetail GetEmail(OrderStatus status, OrderType type, OrderViewModel order, string Note, BoekRole role = BoekRole.Customer, bool IsBoek = false)
        {
            EmailDetail emailDetail = new EmailDetail();
            if (CheckOrderEmail(order))
            {
                emailDetail = new EmailDetail();
                var vietnameseEmail = new VietnameseEmail(status, type, _configuration, role, IsBoek);
                var englishEmail = new EnglishEmail(status, type, _configuration, role, IsBoek);

                GetOrderDetailInEmailBody(ref vietnameseEmail, ref englishEmail, order, Note);

                if (!string.IsNullOrEmpty(vietnameseEmail.Email.Title) &&
                !string.IsNullOrEmpty(englishEmail.Email.Title))
                    emailDetail.Subject = vietnameseEmail.Email.Title + englishEmail.Email.Title;
                if (vietnameseEmail.Email.IsValid() && englishEmail.Email.IsValid())
                    emailDetail.Body = GetBody(vietnameseEmail.Email) + GetBody(englishEmail.Email);
                if (!string.IsNullOrEmpty(emailDetail.Subject) && !string.IsNullOrEmpty(emailDetail.Body))
                {
                    emailDetail.SubTitle = _configuration.GetSection(EmailConventions.EMAIL_CONFIG_EN_SUB_TITLE).Value;
                    emailDetail.Body = emailDetail.SubTitle + emailDetail.Body;
                    emailDetail.To = order.CustomerEmail;
                    emailDetail.IsValid = true;
                }
                else
                {
                    emailDetail.Subject = string.Empty;
                    emailDetail.Body = string.Empty;
                    emailDetail.IsValid = false;
                }
            }
            else
                emailDetail.IsValid = false;
            return emailDetail;
        }

        private bool CheckOrderEmail(OrderViewModel order)
        {
            var customerInfo = !string.IsNullOrEmpty(order.CustomerName) && !string.IsNullOrEmpty(order.CustomerEmail);
            var orderInfo = false;
            if (order.OrderDetails.Any() && order.Total.HasValue)
                orderInfo = order.Total > 0;
            return customerInfo && orderInfo;
        }

        private void GetOrderDetailInEmailBody(ref VietnameseEmail vietnameseEmail, ref EnglishEmail englishEmail, OrderViewModel order, string Note)
        {
            if (!order.OrderDetails.Any())
            {
                vietnameseEmail.Email.SetEmptyData();
                englishEmail.Email.SetEmptyData();
            }
            else
            {
                //Vietnamese
                var temp = SetEmailDetail(order, Note, vietnameseEmail.Email);
                if (temp != null)
                    vietnameseEmail.Email = temp;
                else
                    vietnameseEmail.Email.SetEmptyData();

                //English
                temp = SetEmailDetail(order, Note, englishEmail.Email);
                if (temp != null)
                    englishEmail.Email = temp;
                else
                    englishEmail.Email.SetEmptyData();
            }
        }

        private Email SetEmailDetail(OrderViewModel order, string Note, Email email)
        {
            var currencyFormat = "C";
            var currencyFormatProvider = new CultureInfo("vi-VN");
            var flag = true;

            //Sub total
            order = ServiceUtils.GetTotal(order, _mapper, _unitOfWork);
            flag = order.SubTotal > 0;

            if (flag)
            {
                GetBody2_2(order, currencyFormat, currencyFormatProvider, ref email, ref flag);
                if (flag)
                    ConvertOtherDataIntoEmail(order, Note, currencyFormat, currencyFormatProvider, ref email);
            }
            return flag ? email : null;
        }

        private void GetBody2_2(OrderViewModel order, string currencyFormat, CultureInfo currencyFormatProvider, ref Email email, ref bool flag)
        {
            string Body2_2 = null;
            //Get body2_2
            foreach (var item in order.OrderDetails)
            {
                var image = $"\"{item.BookProduct.ImageUrl}\"";
                var title = item.BookProduct.Title;
                var Quantity = item.Quantity;
                decimal SalePrice = item.BookProduct.SalePrice;
                flag = !string.IsNullOrEmpty(image) && !string.IsNullOrEmpty(title) &&
                Quantity > 0 && SalePrice > 0;
                if (flag)
                {
                    var sample = email.Body2_2;
                    var format = StatusExtension<BookFormat>.GetStatus(item.BookProduct.Format, false, email.Language);
                    var WithPdf = (bool)item.WithPdf ? (email.Language.Equals(MessageConstants.LANGUAGE_VN) ? MessageConstants.WITH_VN : MessageConstants.WITH_EN) :
                    (email.Language.Equals(MessageConstants.LANGUAGE_VN) ? MessageConstants.WITHOUT_VN : MessageConstants.WITHOUT_EN);
                    var WithAudio = (bool)item.WithAudio ? (email.Language.Equals(MessageConstants.LANGUAGE_VN) ? MessageConstants.WITH_VN : MessageConstants.WITH_EN) :
                    (email.Language.Equals(MessageConstants.LANGUAGE_VN) ? MessageConstants.WITHOUT_VN : MessageConstants.WITHOUT_EN);
                    double PdfPrice = 0;
                    double AudioPrice = 0;
                    var PdfQuantity = 0;
                    var AudioQuantity = 0;
                    if ((bool)item.WithPdf && item.BookProduct.PdfExtraPrice.HasValue)
                    {
                        PdfPrice = (double)item.BookProduct.PdfExtraPrice;
                        PdfQuantity = Quantity;
                    }
                    if ((bool)item.WithAudio && item.BookProduct.AudioExtraPrice.HasValue)
                    {
                        AudioPrice = (double)item.BookProduct.AudioExtraPrice;
                        AudioQuantity = Quantity;
                    }
                    var pairs = new Dictionary<string, string>()
                    {
                        {EmailConventions.EMAIL_IMAGE, image},
                        {EmailConventions.EMAIL_TITLE, title},
                        {EmailConventions.EMAIL_QUANTITY, Quantity.ToString()},
                        {EmailConventions.EMAIL_SALE_PRICE, ((double)SalePrice).ToString(currencyFormat, currencyFormatProvider)},
                        {EmailConventions.EMAIL_WITH_PDF, WithPdf.ToString()},
                        {EmailConventions.EMAIL_WITH_AUDIO, WithAudio.ToString()},
                        {EmailConventions.EMAIL_PDF_QUANTITY, PdfQuantity.ToString()},
                        {EmailConventions.EMAIL_AUDIO_QUANTITY, AudioQuantity.ToString()},
                        {EmailConventions.EMAIL_PDF_PRICE, PdfPrice.ToString(currencyFormat, currencyFormatProvider)},
                        {EmailConventions.EMAIL_AUDIO_PRICE, AudioPrice.ToString(currencyFormat, currencyFormatProvider)},
                        {EmailConventions.EMAIL_BOOK_FORMAT, format},
                    };
                    if (string.IsNullOrEmpty(Body2_2))
                        Body2_2 = ReplaceEmailSample(pairs, sample);
                    else
                        Body2_2 += ReplaceEmailSample(pairs, sample);
                }
                else
                    break;
            }
            if (flag)
                email.Body2_2 = Body2_2;
        }

        private void ConvertOtherDataIntoEmail(OrderViewModel order, string Note, string currencyFormat, CultureInfo currencyFormatProvider, ref Email email)
        {
            var issuer = order.OrderDetails.First().BookProduct.Issuer.User.Name;
            string note = ErrorMessageConstants.NO_ORDER_NOTE;
            if (!string.IsNullOrEmpty(Note))
                note = Note;
            var payment = StatusExtension<OrderPayment>.GetStatus(order.Payment, false, email.Language);
            var type = StatusExtension<OrderType>.GetStatus(order.Type, false, email.Language);

            var pairs = new Dictionary<List<string>, Dictionary<string, string>>()
            {
                {
                    new List<string>()
                    {
                        nameof(email.Title),
                        nameof(email.Body1),
                        nameof(email.Body2_1),
                    }, new Dictionary<string, string>() {{EmailConventions.EMAIL_CODE, order.Code}}
                },
                {
                    new List<string>() {nameof(email.Greeting)},
                    new Dictionary<string, string>()  {{EmailConventions.EMAIL_CUSTOMER_NAME, order.CustomerName}}
                },
                {
                    new List<string>() {nameof(email.Body1)},
                    new Dictionary<string, string>()  {{EmailConventions.EMAIL_ORDER_STATUS, order.StatusName}}
                },
                {
                    new List<string>() {nameof(email.Body2_1)},
                    new Dictionary<string, string>()  {{EmailConventions.EMAIL_TYPE, type}}
                },
                {
                    new List<string>()
                    {
                        nameof(email.Body1),
                        nameof(email.Body2_1),
                    }, new Dictionary<string, string>() {{EmailConventions.EMAIL_ADDRESS, string.IsNullOrEmpty(order.Address) ? ErrorMessageConstants.NOT_UPDATE_ORDER_ADDRESS : order.Address}}
                },
                {
                    new List<string>()
                    {
                        nameof(email.Body1),
                        nameof(email.Body2_1),
                    }, new Dictionary<string, string>() {{EmailConventions.EMAIL_ISSUER_NAME, issuer}}
                },
                {
                    new List<string>() {nameof(email.Body1)},
                    new Dictionary<string, string>()  {{EmailConventions.EMAIL_NOTE, note}}
                },
                {
                    new List<string>() {nameof(email.Body2_1)},
                    new Dictionary<string, string>()  {{EmailConventions.EMAIL_PAYMENT_NAME, payment}}
                },
                {
                    new List<string>() {nameof(email.Body2_1)},
                    new Dictionary<string, string>()  {{EmailConventions.EMAIL_CAMPAIGN_NAME, order.Campaign.Name}}
                },
                {
                    new List<string>() {nameof(email.Body2_3)},
                    new Dictionary<string, string>()  {{EmailConventions.EMAIL_SUB_TOTAL, ((double)order.SubTotal).ToString(currencyFormat, currencyFormatProvider)}}
                },
                {
                    new List<string>() {nameof(email.Body2_3)},
                    new Dictionary<string, string>()  {{EmailConventions.EMAIL_FREIGHT, order.Freight.HasValue ? ((double)order.Freight).ToString(currencyFormat, currencyFormatProvider) : "0"}}
                },
                {
                    new List<string>() {nameof(email.Body2_3)},
                    new Dictionary<string, string>()  {{EmailConventions.EMAIL_DISCOUNT, ((double)order.DiscountTotal).ToString(currencyFormat, currencyFormatProvider)}}
                },
                {
                    new List<string>() {nameof(email.Body2_3)},
                    new Dictionary<string, string>()  {{EmailConventions.EMAIL_TOTAL, ((double)order.Total).ToString(currencyFormat, currencyFormatProvider)}}
                },
                {
                    new List<string>() {nameof(email.Note)},
                    new Dictionary<string, string>()  {{EmailConventions.EMAIL_INNER_FREIGHT, ((double)(decimal)OrderFreight.Inner).ToString(currencyFormat, currencyFormatProvider)}}
                },
                {
                    new List<string>() {nameof(email.Note)},
                    new Dictionary<string, string>()  {{EmailConventions.EMAIL_OUTSIDE_FREIGHT, ((double)(decimal)OrderFreight.Outside).ToString(currencyFormat, currencyFormatProvider)}}
                }
            };
            email.ReplaceEmailSample(pairs);

        }
        private string ReplaceEmailSample(Dictionary<string, string> pairs, string sample)
        {
            foreach (var item in pairs)
                sample = sample.Replace(item.Key, item.Value);
            return sample;
        }

        private string GetBody(Email email)
        => email.Greeting + email.Body1 + email.Body2_1 +
        email.Body2_2 + email.Body2_3 + email.Note + email.Conclusion;

        #endregion

        #region ZaloPay
        private KeyValuePair<byte?, string> CheckZaloPayRoles(Dictionary<byte?, string> validRoles)
        {
            var role = ServiceUtils.GetUserInfoValue(_httpContextAccessor, ClaimTypes.Role);
            if (string.IsNullOrEmpty(role))
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.USER_ROLE,
                });
            var result = validRoles.SingleOrDefault(vr => vr.Value.ToLower().Trim().Equals(role.ToLower().Trim()));
            if (result.Equals(default(KeyValuePair<byte?, string>)))
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.USER_ROLE,
                    MessageConstants.MESSAGE_INVALID
                });
            return result;
        }

        private List<Order> GetStaffZaloPayOrders(CreateZaloPayOrderRequestModel createZaloPayOrder)
        {
            var StaffOrder = _mapper.Map<CreateStaffPickUpOrderRequestModel>(createZaloPayOrder);
            var order = _mapper.Map<Order>(StaffOrder);
            order.Freight = 0;
            var validPayments = new List<byte>() { (byte)OrderPayment.ZaloPay };
            var orders = CheckOrder(order, OrderType.PickUp, validPayments, true);
            if (orders == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.INSERT,
                    ErrorMessageConstants.ORDER.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            return orders;
        }
        private List<Order> GetCustomerZaloPayOrders(CreateZaloPayOrderRequestModel createZaloPayOrder)
        {
            List<Order> orders = null;
            Order order = null;
            OrderType type = OrderType.PickUp;
            switch (createZaloPayOrder.Type)
            {
                #region Pick Up
                case (byte)OrderType.PickUp:
                    var PickUpOrder = _mapper.Map<CreateCustomerPickUpOrderRequestModel>(createZaloPayOrder);
                    order = _mapper.Map<Order>(PickUpOrder);
                    order.Freight = 0;
                    break;
                #endregion
                #region Shipping
                case (byte)OrderType.Shipping:
                    var ShippingOrder = _mapper.Map<CreateShippingOrderRequestModel>(createZaloPayOrder);
                    order = _mapper.Map<Order>(ShippingOrder);
                    order.Address = ServiceUtils.CheckAddress(ShippingOrder.AddressRequest).Address;
                    type = OrderType.Shipping;
                    break;
                    #endregion

            }
            if (order != null)
            {
                var validPayments = new List<byte>() { (byte)OrderPayment.ZaloPay };
                orders = CheckOrder(order, type, validPayments, true);
                if (orders == null)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                    {
                        ErrorMessageConstants.INSERT,
                        ErrorMessageConstants.ORDER.ToLower(),
                        MessageConstants.MESSAGE_FAILED
                    });
            }
            return orders;
        }

        private List<Order> GetGuestZaloPayOrders(CreateZaloPayOrderRequestModel createZaloPayOrder)
        {
            List<Order> orders = null;
            Order order = null;
            OrderType type = OrderType.PickUp;

            switch (createZaloPayOrder.Type)
            {
                #region Pick Up
                case (byte)OrderType.PickUp:
                    var PickUpOrder = _mapper.Map<CreateGuestPickUpOrderRequestModel>(createZaloPayOrder);
                    order = _mapper.Map<Order>(PickUpOrder);
                    order.Freight = 0;
                    break;
                #endregion
                #region Shipping
                case (byte)OrderType.Shipping:
                    var ShippingOrder = _mapper.Map<CreateGuestShippingOrderRequestModel>(createZaloPayOrder);
                    order = _mapper.Map<Order>(ShippingOrder);
                    order.Address = ServiceUtils.CheckAddress(ShippingOrder.AddressRequest).Address;
                    type = OrderType.Shipping;
                    break;
                    #endregion

            }
            if (order != null)
            {
                var validPayments = new List<byte>() { (byte)OrderPayment.ZaloPay };
                orders = CheckGuestOrder(order, type, validPayments, true);
                if (orders == null)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                    {
                        ErrorMessageConstants.INSERT,
                        ErrorMessageConstants.ORDER.ToLower(),
                        MessageConstants.MESSAGE_FAILED
                    });
            }
            return orders;
        }

        private List<string> CheckPendingOrderCampaigns(List<int?> campaignIds, bool ShowErrorMessage = false)
        {
            var errorMessages = new List<string>();
            var campaigns = _unitOfWork.Campaigns.Get(c => campaignIds.Contains(c.Id)).ToList();
            if (!campaigns.Any())
            {
                var error = $"{ErrorMessageConstants.INVALID_SCHEDULED_DATE} {ErrorMessageConstants.CAMPAIGN.ToLower()}";
                if (ShowErrorMessage)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[] { error });
                else
                    errorMessages.Add(error);
            }
            campaigns = campaigns.Where(c => DateTime.Compare((DateTime)c.StartDate, DateTime.Now) <= 0 &&
                    DateTime.Compare((DateTime)c.EndDate, DateTime.Now) > 0).ToList();
            if (!campaigns.Any())
            {
                var error = new string[] { ErrorMessageConstants.INVALID_SCHEDULED_DATE };
                if (ShowErrorMessage)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), error);
                else
                    errorMessages.AddRange(error);
            }
            return errorMessages.Any() ? errorMessages : null;
        }

        private void SendErrorLog(List<string> errorMessages, string Title)
        {
            if (errorMessages != null)
            {
                if (errorMessages.Any())
                {
                    string message = "";
                    errorMessages.ForEach(error =>
                    {
                        if (string.IsNullOrEmpty(message))
                            message = $"====={Title}===== || {error}";
                        else
                            message += $" | {error}";
                    });
                    _logger.LogInformation(message);
                }
            }
        }
        #endregion

        #region Notification
        private void GetShippingOrdersOfNotification(List<OrderViewModel> orders, ref List<NotificationRequestModel> notifications)
        {
            if (orders.Any())
            {
                var validType = (byte)OrderType.Shipping;
                var validStatus = (byte)OrderStatus.Processing;
                foreach (var item in orders)
                {
                    if (item.Type.Equals(validType) && item.Status.Equals(validStatus))
                    {
                        if (item.OrderDetails != null)
                        {
                            if (item.OrderDetails.Any())
                            {
                                var issuerId = item.OrderDetails.First().BookProduct.IssuerId;
                                var notification = new NotificationRequestModel()
                                {
                                    UserIds = new List<Guid?>() { issuerId },
                                    UserRoles = new List<byte>() { (byte)BoekRole.Admin },
                                    Status = item.Status,
                                    StatusName = item.StatusName,
                                    Message = MessageConstants.NOTI_NEW_SHIPPING_ORDER_MESS + $"[{item.Id}]"
                                };
                                notifications.Add(notification);
                            }
                        }
                    }
                }
            }
        }

        private void GetPickUpOrdersOfNotification(List<OrderViewModel> orders, ref List<NotificationRequestModel> notifications)
        {
            if (orders.Any())
            {
                var validType = (byte)OrderType.PickUp;
                var validStatus = (byte)OrderStatus.Processing;
                var campaignId = orders.First().CampaignId;
                var staff = _unitOfWork.CampaignStaffs.Get(cs => cs.CampaignId.Equals(campaignId) &&
                cs.Status.Equals((byte)CampaignStaffStatus.Attended)).ToList();
                var staffIds = new List<Guid?>();
                if (staff.Any())
                    staffIds = staff.Select(s => s.StaffId).ToList();
                foreach (var item in orders)
                {
                    if (item.Type.Equals(validType) && item.Status.Equals(validStatus))
                    {
                        if (item.OrderDetails != null)
                        {
                            if (item.OrderDetails.Any())
                            {
                                var issuerId = item.OrderDetails.First().BookProduct.IssuerId;
                                var notification = new NotificationRequestModel()
                                {
                                    UserIds = new List<Guid?>() { issuerId },
                                    UserRoles = new List<byte>() { (byte)BoekRole.Admin },
                                    Status = item.Status,
                                    StatusName = item.StatusName,
                                    Message = MessageConstants.NOTI_NEW_PICK_UP_ORDER_MESS + $"[{item.Id}]"
                                };
                                if (staffIds.Any())
                                    notification.UserIds.AddRange(staffIds);
                                notifications.Add(notification);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Calculation
        public OrderCalculationViewModel GetOrderCalculation(Order order, OrderType type, bool CheckCampaignType = true)
        {
            var campaign = CheckCampaignOrder(order.CampaignId, type, true, CheckCampaignType);
            if (type.Equals(OrderType.Shipping))
                order.Freight = GetShippingFreightCalculation(order.Address, campaign);
            var orderDetails = _mapper.Map<List<OrderDetail>>(order.OrderDetails);
            var _orderDetail = GetOrderDetailCalculation(orderDetails, campaign);
            if (_orderDetail.Item1 > 0)
            {
                var freightName = StatusExtension<OrderFreight>.GetStatus(order.Freight);
                if (type.Equals(OrderType.Shipping))
                    order.Freight *= _orderDetail.Item1;
                order.OrderDetails = _orderDetail.Item2;
                order.Type = (byte)type;
                var orderViewModel = _mapper.Map<OrderViewModel>(order);
                orderViewModel = ServiceUtils.GetTotal(orderViewModel, _mapper, _unitOfWork);
                var result = _mapper.Map<OrderCalculationViewModel>(orderViewModel);
                result.PlusPoint = result.Total > 0 ? (decimal?)((double)result.Total / 1000.0) : 0;
                result.FreightName = freightName;
                return result;
            }
            else
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                {
                    ErrorMessageConstants.ORDER_DETAIL,
                    MessageConstants.MESSAGE_INVALID
                });
            return null;
        }

        private decimal? GetShippingFreightCalculation(string AddressOrder = null, Campaign campaign = null)
        {
            if (campaign.Format.Equals((byte)CampaignFormat.Online))
                return (decimal)OrderFreight.Inner;
            if (campaign.Format.Equals((byte)CampaignFormat.Offline))
            {
                if (string.IsNullOrEmpty(campaign.Address))
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.PreconditionRequired), new string[]
                    {
                            MessageConstants.MESSAGE_REQUIRED,
                            ErrorMessageConstants.ADDRESS_OF,
                            ErrorMessageConstants.CAMPAIGN.ToLower()
                    });
                if ((bool)campaign.IsRecurring)
                {
                    var schedules = new List<Schedule>()
                    {
                        new Schedule()  {Address = campaign.Address}
                    };
                    campaign.CampaignOrganizations.Select(cos => cos.Schedules).ToList().ForEach(s => schedules.AddRange(s));
                    var provinces = _unitOfWork.Schedules.GetProvincesFromSchedules(schedules);
                    if (provinces == null)
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                        {
                                ErrorMessageConstants.NOT_FOUND,
                                ErrorMessageConstants.ADDRESS_PROVINCE,
                                ErrorMessageConstants.CAMPAIGN_ORGANIZATION_SCHEDULE
                        });
                    var IsInner = provinces.Any(p => AddressOrder.ToLower().Trim().Contains(p.NameWithType.ToLower().Trim()));
                    if (!IsInner)
                        return (decimal)OrderFreight.Outside;
                    else
                        return (decimal)OrderFreight.Inner;
                }
                else
                {
                    var address = campaign.Address.Split(",").Last().ToLower().Trim();
                    var province = ProvincesList.PROVINCES.SingleOrDefault(p => address.Equals(p.NameWithType.ToLower().Trim()));
                    if (province == null)
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                        {
                                ErrorMessageConstants.NOT_FOUND,
                                ErrorMessageConstants.ADDRESS_PROVINCE,
                                ErrorMessageConstants.CAMPAIGN.ToLower()
                        });
                    if (!AddressOrder.ToLower().Trim().Contains(address))
                        return (decimal)OrderFreight.Outside;
                    else
                        return (decimal)OrderFreight.Inner;
                }
            }
            return null;
        }

        private (int, List<OrderDetail>) GetOrderDetailCalculation(List<OrderDetail> orderDetails, Campaign campaign)
        {
            if (orderDetails == null || !orderDetails.Any())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.PreconditionRequired), new string[]
                {
                    MessageConstants.MESSAGE_REQUIRED,
                    ErrorMessageConstants.ORDER_DETAIL
                });
            if (campaign.Format.Equals((byte)CampaignFormat.Offline))
            {
                if (orderDetails.Any(od => od.WithAudio == true || od.WithPdf == true))
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                    {
                        ErrorMessageConstants.ORDER_OFFLINE_CAMPAIGN_WITH_AUDIO_OR_PDF
                    });
            }
            var issuerIds = campaign.Participants.Where(p =>
            p.Status.Equals((byte)ParticipantStatus.Accepted) ||
            p.Status.Equals((byte)ParticipantStatus.Approved))
            .Select(p => p.IssuerId).ToList();
            if (!issuerIds.Any())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                {
                    ErrorMessageConstants.PARTICIPANT,
                    MessageConstants.MESSAGE_INVALID
                });
            CheckDuplicatedBookProduct(orderDetails);
            var issuers = new Dictionary<Guid?, List<Guid?>>();
            orderDetails.ForEach(ods =>
            {
                var bookProduct = _unitOfWork.BookProducts.Get(bp =>
                bp.Id.Equals(ods.BookProductId))
                .Include(bp => bp.BookProductItems)
                .Include(bp => bp.Book)
                .SingleOrDefault();
                if (bookProduct == null)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                    {
                        ErrorMessageConstants.NOT_FOUND,
                        ErrorMessageConstants.BOOK_PRODUCT_ID
                    });
                if (!issuerIds.Contains(bookProduct.IssuerId))
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                    {
                        ErrorMessageConstants.UNATTENDED_PARTICIPANT,
                        bookProduct.IssuerId.ToString()
                    });
                if (!bookProduct.Status.Equals((byte)BookProductStatus.Sale))
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                    {
                        ErrorMessageConstants.BOOK_PRODUCT_STATUS,
                        bookProduct.Id.ToString(),
                        MessageConstants.MESSAGE_INVALID
                    });
                CheckSaleQuantity(ods.Quantity, bookProduct.SaleQuantity, bookProduct.Title);
                CheckBookProductByType(bookProduct, ods.WithPdf, ods.WithAudio);
                ods.Price = bookProduct.SalePrice;
                ods.Discount = bookProduct.Discount;
                ods.BookProduct = bookProduct;
                if (!issuers.Keys.Contains(bookProduct.IssuerId))
                    issuers.Add(bookProduct.IssuerId, new List<Guid?>() { bookProduct.Id });
                else
                {
                    if (!issuers[bookProduct.IssuerId].Contains(bookProduct.Id))
                        issuers[bookProduct.IssuerId].Add(bookProduct.Id);
                }
            });
            if (issuers.Any())
            {
                (int, List<OrderDetail>) result = (issuers.Count(), orderDetails);
                return result;
            }
            return (0, null);
        }
        #endregion

        #region Models
        public Dictionary<OrderViewModel, string> ConvertOrdersWithNote(List<OrderViewModel> orders)
        {
            var result = new Dictionary<OrderViewModel, string>();
            orders.ForEach(o =>
            {
                var Note = string.IsNullOrEmpty(o.Note) ? null : o.Note.Split(";").Last();
                result.Add(o, Note);
            });
            return result;
        }
        #endregion

        #endregion
    }
}