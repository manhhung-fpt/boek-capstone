using System.Runtime.Serialization;

namespace Boek.Core.Enums
{
    public enum BoekRole
    {
        [EnumMember(Value = "Admin")]
        Admin = 1,
        [EnumMember(Value = "Issuer")]
        Issuer = 2,
        [EnumMember(Value = "Staff")]
        Staff = 3,
        [EnumMember(Value = "Customer")]
        Customer = 4,
        [EnumMember(Value = "Guest")]
        Guest = 5
    }

    public enum BookFormat
    {
        [EnumMember(Value = "Sách giấy;Printed book")]
        PrintBook = 1,
        [EnumMember(Value = "Pdf;Pdf")]
        Pdf = 2,
        [EnumMember(Value = "Audio;Audio")]
        Audio = 3,
    }

    public enum BookType
    {
        [EnumMember(Value = "Sách lẻ")]
        Odd = 1,
        [EnumMember(Value = "Sách combo")]
        Combo = 2,
        [EnumMember(Value = "Sách series")]
        Series = 3,
    }

    public enum BoekStatus
    {
        [EnumMember(Value = "Hoạt động")]
        Active = 1,
        [EnumMember(Value = "Vô hiệu hóa")]
        Inactive = 2
    }

    public enum BookStatus
    {
        [EnumMember(Value = "Phát hành")]
        Releasing = 1,
        [EnumMember(Value = "Ngừng phát hành")]
        Unreleased = 2
    }

    public enum BookProductStatus
    {
        [EnumMember(Value = "Chờ duyệt")]
        Pending = 1,
        [EnumMember(Value = "Từ chối")]
        Rejected = 2,
        [EnumMember(Value = "Đang bán")]
        Sale = 3,
        [EnumMember(Value = "Ngừng bán")]
        NotSale = 4,
        [EnumMember(Value = "Ngừng bán do hội sách kết thúc")]
        NotSaleDueEndDate = 5,
        [EnumMember(Value = "Ngừng bán do hội sách bị hủy")]
        NotSaleDueCancelledCampaign = 6,
        [EnumMember(Value = "Ngừng bán do hội sách tạm hoãn")]
        NotSaleDuePostponedCampaign = 7,
        [EnumMember(Value = "Hết hàng")]
        OutOfStock = 8,
        [EnumMember(Value = "Ngừng phát hành")]
        Unreleased = 9
    }

    public enum CampaignStatus
    {
        //Chưa bắt đầu
        [EnumMember(Value = "Hội sách sắp diễn ra")]
        NotStarted = 1,
        //Bắt đầu hội sách
        [EnumMember(Value = "Hội sách diễn ra")]
        Start = 2,
        //Kết thúc hội sách
        [EnumMember(Value = "Hội sách kết thúc")]
        End = 3,
        //Hủy hội sách
        [EnumMember(Value = "Hội sách bị hủy")]
        Cancelled = 4,
        //Hủy hội sách
        [EnumMember(Value = "Hội sách tạm dừng")]
        Postponed = 5
    }

    public enum OrderStatus
    {
        //Đang xử lý
        [EnumMember(Value = "Đang chờ;Pending")]
        Pending = 1,
        //Đang xử lý
        [EnumMember(Value = "Đang xử lý;Progressing")]
        Processing = 2,
        //Chờ nhận hàng - tại địa điểm campaign
        [EnumMember(Value = "Chờ nhận hàng;Available")]
        PickUpAvailable = 3,
        //Đang vận chuyển - đến địa chỉ của khách
        [EnumMember(Value = "Đang vận chuyển;Shipping")]
        Shipping = 4,
        //Đã giao - đến địa chỉ của khách
        [EnumMember(Value = "Đã giao;Shipped")]
        Shipped = 5,
        //Đã nhận - tại địa điểm campaign
        [EnumMember(Value = "Đã nhận;Received")]
        Received = 6,
        //Hủy đơn hàng
        [EnumMember(Value = "Hủy đơn hàng;Cancelled")]
        Cancelled = 7
    }

    public enum OrderType
    {
        [EnumMember(Value = "Giao hàng;Shipping")]
        Shipping = 1,
        [EnumMember(Value = "Nhận tại quầy;Pick-up")]
        PickUp = 2
    }

    public enum ParticipantStatus
    {
        //issuer đợi admin duyệt
        [EnumMember(Value = "Chờ duyệt")]
        PendingRequest = 1,
        [EnumMember(Value = "Chờ lời mời")]
        //admin đợi issuer accept/reject
        PendingInvitation = 2,
        [EnumMember(Value = "Chấp nhận duyệt")]
        //admin chấp nhận yêu cầu từ issuer
        Approved = 3,
        [EnumMember(Value = "Từ chối duyệt")]
        //admin từ chối yêu cầu từ issuer
        RejectedRequest = 4,
        [EnumMember(Value = "Chấp nhận lời mời")]
        //issuer chấp nhận lời mời
        Accepted = 5,
        [EnumMember(Value = "Từ chối lời mời")]
        //issuer từ chối lời mời
        RejectedInvitation = 6,
        [EnumMember(Value = "Hủy lời mời")]
        //admin hủy lời mời
        Cancelled = 7,
        [EnumMember(Value = "Hủy vì hội sách bắt đầu")]
        //hủy lời mời vì hội sách bắt đầu
        CancelledDueStartDate = 8,
        [EnumMember(Value = "Hủy vì hội sách kết thúc")]
        //hủy lời mời vì hội sách bắt đầu
        CancelledDueEndDate = 9,
        [EnumMember(Value = "Hủy vì hội sách hủy")]
        //hủy lời mời vì hội sách bắt đầu
        CancelledDueCancelledCampaign = 10
    }

    public enum CampaignFormat
    {
        [EnumMember(Value = "Trực tiếp")]
        Offline = 1,
        [EnumMember(Value = "Trực tuyến")]
        Online = 2
    }

    public enum CampaignStaffStatus
    {
        [EnumMember(Value = "Tham gia")]
        Attended = 1,
        [EnumMember(Value = "Không tham gia")]
        Unattended = 2
    }

    public enum OrderPayment
    {
        [EnumMember(Value = "Chưa thanh toán;Unpaid")]
        Unpaid = 1,
        [EnumMember(Value = "Thanh toán ZaloPay;ZaloPay")]
        ZaloPay = 2,
        [EnumMember(Value = "Thanh toán tiền mặt;Cash")]
        Cash = 3
    }

    public enum OrderFreight
    {
        [EnumMember(Value = "Không có phí;Unpaid")]
        Unpaid = 0,
        [EnumMember(Value = "Nội thành;Inner freight")]
        Inner = 15000,
        [EnumMember(Value = "Ngoại thành;Outside freight")]
        Outside = 30000
    }

    public enum TimeLineType
    {
        [EnumMember(Value = "Ngày")]
        Day = 1,
        [EnumMember(Value = "Tuần")]
        Week = 2,
        [EnumMember(Value = "Tháng")]
        Month = 3,
        [EnumMember(Value = "Quý")]
        Season = 4,
        [EnumMember(Value = "Năm")]
        Year = 5
    }

    public enum CurrentTimeLine
    {
        [EnumMember(Value = "Hôm nay")]
        Today = 1,
        [EnumMember(Value = "Tuần này")]
        CurrentWeek = 2,
        [EnumMember(Value = "Tháng này")]
        CurrentMonth = 3,
        [EnumMember(Value = "Quý này")]
        CurrentSeason = 4,
        [EnumMember(Value = "Năm nay")]
        CurrentYear = 5
    }
    public enum SeasonTimeLine
    {
        [EnumMember(Value = "Xuân")]
        Spring = 1,
        [EnumMember(Value = "Hạ")]
        Summer = 2,
        [EnumMember(Value = "Thu")]
        Fall = 3,
        [EnumMember(Value = "Đông")]
        Winter = 4
    }
    public enum DashboardSummary
    {
        [EnumMember(Value = "Tăng")]
        Increase = 1,
        [EnumMember(Value = "Giảm")]
        Decrease = 2,
        [EnumMember(Value = "Bằng")]
        Equal = 3
    }

    public enum NotificationType
    {
        [EnumMember(Value = "Kiểm sách bán")]
        CheckingBookProduct = 1,
        [EnumMember(Value = "Đã duyệt sách bán")]
        DoneCheckingBookProduct = 2,
        [EnumMember(Value = "Gửi lời mời")]
        ParticipantInvitation = 3,
        [EnumMember(Value = "Gửi yêu cầu")]
        ParticipantRequest = 4,
        [EnumMember(Value = "Trạng thái tham dự viên")]
        ParticipantStatus = 5,
        [EnumMember(Value = "Đơn hàng mới")]
        NewOrder = 6,
        [EnumMember(Value = "Đơn hàng bị hủy")]
        CancelledOrder = 7
    }
}
