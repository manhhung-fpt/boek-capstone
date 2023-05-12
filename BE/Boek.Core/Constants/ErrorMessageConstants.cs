using Boek.Core.Extensions;
using Boek.Core.Enums;

namespace Boek.Core.Constants
{
    public class ErrorMessageConstants
    {
        #region Commons
        public const string INSERT = "Tạo";

        public const string APPLY = "Tham dự thành";

        public const string UPDATE = "Cập nhật";

        public const string UPDATED = "Đã cập nhật";

        public const string DELETE = "Xóa";

        public const string CANCEL = "Hủy";

        public const string POSTPONE = "Hoãn";

        public const string RESTART = "Bắt đầu lại";

        public const string CONVERT = "Chuyển đổi";

        public const string CALCULATE = "Tính toán";

        public const string NOT_FOUND = "Không tìm thấy";

        public const string DISABLE = "Vô hiệu";
        #endregion

        #region Users

        public const string ACCOUNT = "Tài khoản";
        public const string GUEST = "Vô danh";
        public const string USER = "Người dùng";
        public const string USER_ID = "mã của người dùng";
        public const string USER_ACCOUNT = "tài khoản của người dùng";
        public const string USER_EMAIL = "email của người dùng";
        public const string USER_INFO = "thông tin của người dùng";
        public const string USER_ROLE = "vai trò của người dùng";
        public const string ADDRESS = "địa chỉ";
        public const string ADDRESS_OF = "địa chỉ của";
        public const string CUSTOMER_BIRTHDAY = "ngày sinh của khách hàng";
        public const string CUSTOMER_BIRTHDAY_REQUIREMENT = "Khách hàng phải từ";
        #endregion

        #region Author

        public const string AUTHOR = "tác giả";
        public const string AUTHOR_ID = "mã của tác giả";
        public const string AUTHOR_NAME = "tên của tác giả";
        public const string AUTHOR_URL = "link hình ảnh của tác giả";
        #endregion

        #region Publishers
        public const string PUBLISHER = "NXB";
        public const string PUBLISHER_ID = "mã của NXB";
        public const string PUBLISHER_NAME = "tên của NXB";
        public const string PUBLISHER_URL = "link hình ảnh của NXB";
        public const string PUBLISHER_EMAIL = "email của NXB";
        #endregion

        #region Genre

        public const string GENRE = "Thể loại sách";
        public const string GENRE_ID = "Mã của thể loại sách";
        public const string PARENT_GENRE_ID = "Mã của thể loại sách cha";
        public const string GENRE_NAME = "tên của thể loại sách";
        public const string GENRE_STATUS = "trạng thái của thể loại sách";
        #endregion

        #region Book
        public const string BOOK = "Sách";
        public const string BOOK_ID = "mã của sách";
        public const string BOOK_ISBN10 = "ISBN10 của sách";
        public const string BOOK_ISBN13 = "ISBN13 của sách";
        public const string BOOK_NAME = "tên của sách";
        public const string BOOK_STATUS = "trạng thái của sách";
        public const string BOOK_PDF_LINK = "link pdf của sách";
        public const string BOOK_AUDIO_LINK = "link audio của sách";
        public const string BOOK_PDF_EXTRA_PRICE = "giá tặng kèm pdf của sách";
        public const string BOOK_AUDIO_EXTRA_PRICE = "giá tặng kèm audio của sách";
        public static string BOOK_DISABLED_STATUS = $"{StatusExtension<BookStatus>.GetStatus((byte)BookStatus.Unreleased)}";
        public static string BOOK_ENABLED_STATUS = $"{StatusExtension<BookStatus>.GetStatus((byte)BookStatus.Releasing)}";
        public const string BOOK_DISABLE_ENABLE_BOOK_SERIES = $"trạng thái của series thành";
        public const string BOOK_DISABLE_ENABLE_ODD_BOOK_PRODUCT = "trạng thái của sách bán lẻ thành";
        public const string BOOK_DISABLE_ENABLE_SERIES_BOOK_PRODUCT = "trạng thái của sách bán series thành";
        public const string BOOK_DISABLE_ENABLE_SERIES_COMBO_BOOK_PRODUCT = "trạng thái của sách bán series hoặc combo thành";
        public const string ODD_BOOK = "Sách lẻ";
        public const string BOOK_COMBO = "Sách combo";
        public const string BOOK_SERIES = "Sách series";
        public const string BOOK_ITEM = "Sách trong";
        public const string BOOK_PRODUCT_ITEM = "các sách trong";
        public const string BOOK_PRODUCT_ITEM_INVALID_AMOUNT = "phải từ 2 cuốn trở lên";
        public const string BOOK_PRODUCT_ITEM_ID = "Mã của sách trong";
        public const string BOOK_PRODUCT_ITEM_AMOUNT = "Số lượng của sách trong";
        public const string BOOK_PRODUCT_ITEM_GENRE = "Thể loại của sách trong";
        public const string BOOK_ITEM_DISPLAY_INDEX = "vị trí sách trong";
        public const string BOOK_ITEM_AUTHOR = "tác giả sách trong";
        #endregion

        #region Book Product

        public const string BOOK_PRODUCT = "Sách bán";
        public const string BOOK_PRODUCT_ID = "mã của sách bán";
        public const string BOOK_PRODUCT_NAME = "tên của sách bán";
        public const string BOOK_PRODUCT_STATUS = "trạng thái của sách bán";
        public const string BOOK_PRODUCT_FORMAT = "định dạng của sách bán";
        public const string BOOK_PRODUCT_FORMAT_PDF = "vì thiếu thông tin pdf của sách bán";
        public const string BOOK_PRODUCT_FORMAT_AUDIO = "vì thiếu thông tin audio của sách bán";
        public const string BOOK_PRODUCT_FORMAT_INDEX = "vị trí pdf và audio của sách bán";
        public const string BOOK_PRODUCT_FORMAT_PDF_INDEX = "vị trí pdf của sách bán";
        public const string BOOK_PRODUCT_FORMAT_AUDIO_INDEX = "vị trí audio của sách bán";
        public const string BOOK_PRODUCT_SALE_QUANTITY = "số lượng của sách bán";
        public const string BOOK_PRODUCT_DISCOUNT = "giảm giá của sách bán";
        public const string BOOK_PRODUCT_TYPE = "loại của sách bán";
        public const string BOOK_PRODUCT_GENRE = "thể loại của sách bán";
        public const string BOOK_PRODUCT_COMBO_INVALID_GENRE = "Thể loại của sách bán combo phải là thể loại cha";
        public const string BOOK_PRODUCT_COMMISSION = "chiết khấu của sách bán";
        public const string BOOK_PRODUCT_COMBO_TITLE = "tên của sách bán combo";
        public const string BOOK_PRODUCT_CREATING_CAMPAIGN_ID = "mã của hội sách đang tạo";
        public const string BOOK_PRODUCT_INVALID_STATUS = "vì không phải trạng thái";
        #endregion

        #region Issuers
        public const string ISSUER = "nhà phát hành";
        public const string ACCOUNT_STATUS = "Trạng thái tài khoản của";
        #endregion

        #region Organization
        public const string ORGANIZATION = "Tổ chức";
        public const string ORGANIZATION_ID = "mã của tổ chức";
        public const string ORGANIZATION_NAME = "tên của tổ chức";
        public const string ORGANIZATION_IMAGE_URL = "link ảnh của tổ chức";
        public const string CUSTOMER_ORGANIZATION = "Tổ chức khách hàng";
        public const string CUSTOMER_ORGANIZATION_ID = "mã của tổ chức khách hàng";
        public const string ORGANIZATION_MEMBER = "thành viên của tổ chức";
        public const string ORGANIZATION_MEMBER_EMAIL_DOMAIN = "miền email của tổ chức";
        #endregion

        #region Group
        public const string GROUP = "Nhóm";
        public const string GROUP_ID = "mã của nhóm";
        public const string GROUP_NAME = "tên của nhóm";
        public const string CUSTOMER_GROUP = "Nhóm khách hàng";
        public const string CUSTOMER_GROUP_ID = "mã của nhóm khách hàng";
        #endregion

        #region Level
        public const string LEVEL = "Cấp độ";
        public const string LEVEL_ID = "mã của cấp độ";
        public const string LEVEL_NEW_ID = "mã của cấp độ mới";
        public const string LEVEL_OLD_ID = "mã của cấp độ cũ";
        public const string LEVEL_NAME = "tên của cấp độ";
        public const string LEVEL_CONDITIONAL_POINT = "mức điểm của cấp độ";
        public const string LEVEL_STATUS = "Trạng thái của cấp độ";
        #endregion

        #region Campaign
        public const string CAMPAIGN = "Hội sách";
        public const string CAMPAIGN_ID = "mã của hội sách";
        public const string CAMPAIGN_NAME = "tên của hội sách";
        public const string CAMPAIGN_DESCRIPTION = "mô tả của hội sách";
        public const string CAMPAIGN_IMAGE = "hình ảnh của hội sách";
        public const string CAMPAIGN_START_DATE = "ngày bắt đầu";
        public const string CAMPAIGN_START_DATE_NOT_TODAY = "không phải là hôm nay";
        public const string CAMPAIGN_STARTED = "vì hội sách bắt đầu";
        public const string CAMPAIGN_ENDED = "vì hội sách kết thúc";
        public const string CAMPAIGN_INVALID_NOT_STARTED = "vì hội sách không phải chưa diễn ra";
        public const string CAMPAIGN_INVALID_STATUS = "vì trạng thái hội sách";
        public const string CAMPAIGN_NOT_LATER_START_DATE = "không sau";
        public const string CAMPAIGN_END_DATE = "ngày kết thúc";
        public const string CAMPAIGN_CREATED_DATE = "ngày tạo hội sách";
        public const string CAMPAIGN_NOT_STARTED = "vì hội sách không diễn ra bây giờ";
        public const string CAMPAIGN_CANCEL = "vì hội sách đã hủy";
        public const string CAMPAIGN_DATE = "vì ngày của hội sách";
        public const string CAMPAIGN_FORMAT = "loại hội sách";
        public const string CAMPAIGN_FORMAT_OFFLINE = "vì loại hội sách trực tuyến";
        public const string CAMPAIGN_STATUS = "trạng thái hội sách";
        #endregion

        #region Campaign Commission
        public const string CAMPAIGN_COMMISSION_INVALID = "vì thiếu chiết khấu hội sách";
        public const string CAMPAIGN_COMMISSION = "Chiết khấu hội sách";
        public const string CAMPAIGN_COMMISSION_ID = "mã của chiết khấu hội sách";
        public const string CAMPAIGN_COMMISSION_GENRE = "thể loại sách được chiết khấu";
        public const string CAMPAIGN_MINIMAL_COMMISSION = "Chiết khấu tối thiểu";
        #endregion

        #region Campaign Organization
        public const string CAMPAIGN_ORGANIZATION = "Tổ chức của hội sách";
        public const string CAMPAIGN_ORGANIZATION_ID = "mã tổ chức của hội sách";
        public const string CAMPAIGN_ORGANIZATION_STATUS = "trạng thái tổ chức của hội sách";
        public const string CAMPAIGN_ORGANIZATION_SCHEDULE = "lịch trình của tổ chức hội sách";
        public const string CAMPAIGN_ORGANIZATION_SCHEDULE_DUPLICATED_DATE = "Trùng ngày bắt đầu hoặc kết thúc cùng địa điểm";
        public const string CAMPAIGN_ORGANIZATION_SCHEDULE_DATE_DURATION = "Trùng khoảng thời gian cùng địa điểm";
        public const string CAMPAIGN_ORGANIZATION_SCHEDULE_START_DATE_OR_END_DATE = "ngày bắt đầu hoặc kết thúc của tổ chức";
        public const string CAMPAIGN_ORGANIZATION_SCHEDULE_START_DATE = "ngày bắt đầu của tổ chức";
        public const string CAMPAIGN_ORGANIZATION_SCHEDULE_END_DATE = "ngày kết thúc của tổ chức";
        #endregion

        #region Campaign Group
        public const string CAMPAIGN_GROUP = "Nhóm của hội sách";
        public const string CAMPAIGN_GROUP_ID = "mã nhóm của hội sách";
        #endregion

        #region Campaign Level
        public const string CAMPAIGN_LEVEL = "Cấp độ trong hội sách";
        public const string CAMPAIGN_LEVEL_ID = "mã cấp độ của hội sách";
        #endregion

        #region Campaign Staff
        public const string CAMPAIGN_STAFF = "Nhân viên hội sách";
        public const string CAMPAIGN_STAFF_ID = "mã của nhân viên tham gia hội sách";
        public const string CAMPAIGN_STAFF_UNATTENDED_STATUS = "vì nhân viên chưa tham gia hội sách";
        public const string CAMPAIGN_STAFF_SAME_DATES = "vì trùng ngày";
        public const string CAMPAIGN_STAFF_INTERSECT_DATES = "vì trùng một khoảng thời gian";
        public const string CAMPAIGN_STAFF_INSIDE_DATES = "vì trong một khoảng thời gian";
        public const string CAMPAIGN_STAFF_WITH = "với các mã nhân viên hội sách sau: ";
        #endregion

        #region Participant
        public const string PARTICIPANT = "Tham dự viên hội sách";

        public const string PARTICIPANT_ID = "mã của tham dự viên hội sách";

        public const string PARTICIPANT_STATUS = "trạng thái";
        public const string PARTICIPANT_STATUS_OF = "trạng thái của";
        public const string UNATTENDED_PARTICIPANT = "Tham dự viên chưa tham gia hội sách";

        public const string CANCELLED_PARTICIPANT_STATUS = "vì hủy tham gia hội sách hoặc vì trạng thái của hội sách";
        #endregion

        #region Addresses
        public const string ADDRESS_CODE = "mã của";
        public const string ADDRESS_PROVINCE = "tỉnh/thành";
        public const string ADDRESS_DISTRICT = "quận/huyện";
        public const string ADDRESS_WARDS = "phường/xã";
        #endregion

        #region Order
        public const string INVALID_SCHEDULED_DATE = "Không nằm trong thời gian diễn ra sự kiện";
        public const string NOT_ENOUGH_QUANTITY = "Không đủ số lượng";
        public const string CONFLICT_BOOK_PRODUCT_WITH_AUDIO_01 = "Mâu thuẫn dữ liệu đính kèm audio 01";
        public const string CONFLICT_BOOK_PRODUCT_WITH_PDF_01 = "Mâu thuẫn dữ liệu đính kèm pdf 01";
        public const string CONFLICT_BOOK_PRODUCT_WITH_AUDIO_02 = "Mâu thuẫn dữ liệu đính kèm audio 02";
        public const string CONFLICT_BOOK_PRODUCT_WITH_PDF_02 = "Mâu thuẫn dữ liệu đính kèm pdf 02";
        public const string ERROR_POINT_CALCULATION = "Lỗi tính điểm";
        public const string ORDER = "Đơn hàng";
        public const string CUSTOMER = "khách hàng";
        public const string CUSTOMER_POINT = "điểm của khách hàng";
        public const string CUSTOMER_SPENDING_ORDERS = "chi tiêu của khách hàng";
        public const string ORDER_ID = "mã của đơn hàng";
        public const string ORDER_QR_AMOUNT = "số lượng đơn hàng";
        public const string ORDER_PAYMENT_TYPE = "loại thanh toán của đơn hàng";
        public const string ORDER_FREIGHT = "phí vận chuyển của đơn hàng";
        public const string ORDER_INVALID_INNER_OR_OUTSIDE_FREIGHT = "vì nội/ngoại thành";
        public const string ORDER_TYPE = "loại đơn hàng";
        public const string ORDER_DETAIL = "chi tiết của đơn hàng";
        public const string ORDER_OFFLINE_CAMPAIGN_WITH_AUDIO_OR_PDF = "chi tiết của đơn hàng của hội sách có kèm pdf hoặc audio";
        public const string ORDER_DETAIL_QUANTITY = "số lượng của chi tiết đơn hàng";
        public const string INTERNAL_ERROR = "Lỗi! Vui lòng liên hệ quản trị viên.";
        public const string ORDER_SAME_STATUS = "Trạng thái đơn hàng đã cập nhật";
        public const string NO_ORDER_DETAIL = "Đơn hàng không có bất kỳ sản phẩm nào";
        public const string NOT_ENOUGH_QUANTITY_WAREHOUSE = "Không đủ số lượng để bán";
        public const string NO_QUANTITY_IN_WAREHOUSE = "Không còn bất kỳ sản phẩm";
        public const string CANCELLED_ORDER = "Đơn hàng này đã bị hủy bỏ";
        public const string INVALID_ORDER_STATUS_RANGE = "Trạng thái đơn hàng ngoài phạm vi hỗ trợ";
        public const string ORDER_CANCELLED_STATUS_EXPIRED_PENDING_ORDER_MESS_DETAIL = "Hủy đơn hàng vì quá hạn thời hạn [Payment]";
        public const string ORDER_CANCELLED_STATUS_EXPIRED_PENDING_ORDER_MESS = "Hủy đơn hàng vì quá hạn thời hạn thanh toán";
        public const string ORDER_CANCELLED_STATUS_DISABLED_BOOK_PRODUCT_MESS = "Hủy đơn hàng vì sách bán bị ngừng phát hành";
        public const string ORDER_CANCELLED_STATUS_NOT_SALE_BOOK_PRODUCT_MESS = "Hủy đơn hàng vì sách bán bị ngừng bán";
        public const string ORDER_CANCELLED_STATUS_END_CAMPAIGN_MESS = "Hủy đơn hàng vì hội sách kết thúc";
        public const string ORDER_CANCELLED_STATUS_POSTPONED_CAMPAIGN_MESS = "Hủy đơn hàng vì hội sách kết thúc";
        public const string ORDER_WITH_ORDER_CODE = "với mã đơn";
        public const string ORDER_WITH_BOOK_PRODUCT_ID = "với mã sách";
        public const string ORDER_WITH_CAMPAIGN_ID = "với mã hội sách";
        public const string UPDATE_ORDER_ADDRESS = "cập nhật địa chỉ nhận sách tại ";
        public const string NOT_UPDATE_ORDER_ADDRESS = "Chưa cập nhật địa chỉ";
        public const string NO_ORDER_NOTE = "Không có ghi chú";
        public const string ORDER_PENDING_ERROR_CAMPAIGNS = "Update progressing orders' campaigns";
        public const string ORDER_PENDING_CHECK_ORDER = "Check progressing orders";
        public const string ORDER_PENDING_UPDATE_STATUS = "Update progressing orders";
        #endregion

        #region Dashboard
        public const string DASHBOARD_TIME_LENGTH = "Khoảng thời gian phải từ 1 trở lên";
        public const string DASHBOARD_SEASON_TYPE = "loại quý";
        public const string DASHBOARD_DATE = "ngày";
        public const string DASHBOARD_DATE_OR_TIME_LENGTH = "ngày hoặc khoảng thời gian";
        public const string REVENUE = "Doanh thu";
        #endregion

        #region Verification
        public const string VERIFICATION_OTP = "Otp";
        public const string VERIFICATION_INVALID_OTP = "Vui lòng gửi lại Otp";
        #endregion

        #region Notification
        public const string NOTI_IDS_OR_ROLES = "mã hoặc vai trò của người dùng";

        #region Book Products
        public const string NOTI_ERROR_CHECKING_BOOK_PRODUCT = "Notification - Check book product: ";
        public const string NOTI_ERROR_DONE_CHECKING_BOOK_PRODUCT = "Notification - Done checking book product: ";
        #endregion

        #region Participants
        public const string NOTI_ERROR_CHECKING_ADMIN_INVITATION = "Notification - Check admin invitation: ";
        public const string NOTI_ERROR_CHECKING_ISSUER_REQUEST = "Notification - Check issuer request: ";
        public const string NOTI_ERROR_PARTICIPANT_STATUS = "Notification - Update participant status: ";
        #endregion

        #region Orders
        public const string NOTI_ERROR_ISSUER_CANCELLED_ORDER = "Notification - Issuer cancelled order: ";
        public const string NOTI_ERROR_CUSTOMER_CANCELLED_ORDER = "Notification - Customer cancelled order: ";
        public const string NOTI_ERROR_NEW_ORDER = "Notification - New order: ";
        #endregion

        #endregion
    }
}
