using Boek.Core.Enums;
using Boek.Core.Extensions;

namespace Boek.Core.Constants
{
    public class MessageConstants
    {
        public const string BOEK = "Boek";
        public const string MESSAGE_SUCCESS = "thành công";
        public const string MESSAGE_FAILED = "thất bại";
        public const string MESSAGE_INVALID = "không hợp lệ";
        public const string MESSAGE_TO_BE = "phải là";
        public const string MESSAGE_UNAUTHENTICATION = "chưa đăng nhập";
        public const string MESSAGE_LINKED_INFO = "vì liên kết với thông tin khác";
        public const string MESSAGE_DUPLICATED_INFO = "Trùng";
        public const string MESSAGE_NOT_DUPLICATED_INFO = "Không trùng";
        public const string MESSAGE_REQUIRED = "Phải có";
        public const string MESSAGE_NOT_BELONGING = "không thuộc về";
        public const string MESSAGE_EXISTED = "đã tồn tại";
        public const string MESSAGE_INVITED_PARTICIPANT = "được mời tham gia";
        public const string MESSAGE_REQUEST_PARTICIPANT = "yêu cầu tham gia";

        #region Boek constants

        #region Auth Configuration
        public const string AUTH_CONFIG_TOKEN = "Token";
        public const string AUTH_CONFIG_SECRET_KEY = "SecretKey";
        public const string AUTH_CONFIG_POLICY_NAME = "BookFairAuthorization";
        public const string AUTH_CONFIG_POLICY_CLAIM_ROLE = "Role";
        #endregion

        #region Swagger Configuration
        public const string SWAGGER_CONFIG_SECURITY_NAME = "Bearer";
        public const string SWAGGER_CONFIG_SECURITY_SCHEME_DESC = @"JWT Authorization header using the Bearer scheme.
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer iJIUzI1NiIsInR5cCI6IkpXVCGlzI9d9zc2'";
        public const string SWAGGER_CONFIG_SECURITY_SCHEME_NAME = "Authorization";
        public const string SWAGGER_CONFIG_SECURITY_SCHEME = "oauth2";
        #endregion

        #region Program
        public const string PROGRAM_CORS_ORIGINS = "AllowedOrigins";
        public const string PROGRAM_FILE = "FileLogging";
        public const string PROGRAM_PATH_KEY_2 = "Keys";
        public const string PROGRAM_PATH_KEY_3 = "firebase.json";

        #endregion

        #region Connection string
        public const string BOEK_JSON_FILE = "appsettings.json";
        public const string BOEK_FULL_CONNECTION_STRING = "ConnectionStrings:BoekCapstone";
        public const string BOEK_CONNECTION_STRING = "BoekCapstone";
        #endregion

        #region Cloudinary
        public const string BOEK_CLOUDINARY_NAME = "Cloudinary:CloudName";
        public const string BOEK_CLOUDINARY_API_KEY = "Cloudinary:ApiKey";
        public const string BOEK_CLOUDINARY_API_SECRET = "Cloudinary:ApiSecret";
        #endregion

        #region Boek Email
        public const string BOEK_EMAIL = "BoekEmail";
        public const string BOEK_EMAIL_HOST = "Host";
        public const string BOEK_EMAIL_USER_NAME = "UserName";
        public const string BOEK_EMAIL_PASSWORD = "Password";
        #endregion

        #region Session
        public const string SESSION_OTP_VERIFICATION = "OTP_Email_Session";
        public const string COOKIE_NAME_OTP_VERIFICATION = "OTP_Email_Cookie";
        public const string REDIS_OTP_VERIFICATION = "OTP_Email_Redis";
        public const string REDIS = "Redis";
        #endregion

        #region Swagger operation

        #region Admin
        public const string SWAGGER_OPERATION_ADMIN_AUTHOR = "Admin - Authors";
        public const string SWAGGER_OPERATION_ADMIN_BOOK_PRODUCT = "Admin - Book Products";
        public const string SWAGGER_OPERATION_ADMIN_CAMPAIGN = "Admin - Campaigns";
        public const string SWAGGER_OPERATION_ADMIN_GENRE = "Admin - Genres";
        public const string SWAGGER_OPERATION_ADMIN_GROUP = "Admin - Groups";
        public const string SWAGGER_OPERATION_ADMIN_LEVEL = "Admin - Levels";
        public const string SWAGGER_OPERATION_ADMIN_ORDER = "Admin - Orders";
        public const string SWAGGER_OPERATION_ADMIN_ORGANIZATION = "Admin - Organizations";
        public const string SWAGGER_OPERATION_ADMIN_PARTICIPANT = "Admin - Participants";
        public const string SWAGGER_OPERATION_ADMIN_PUBLISHER = "Admin - Publishers";
        public const string SWAGGER_OPERATION_ADMIN_USER = "Admin - Users";
        public const string SWAGGER_OPERATION_ADMIN_DASHBOARD_COMPARISON = "Admin - Dashboards - Comparison";
        public const string SWAGGER_OPERATION_ADMIN_DASHBOARD_DATE_RANGE = "Admin - Dashboards - Date Range";
        public const string SWAGGER_OPERATION_ADMIN_DASHBOARD_SUMMARY = "Admin - Dashboards - Summary";
        #endregion

        #region Issuer
        public const string SWAGGER_OPERATION_ISSUER_AUTHOR = "Issuer - Authors";
        public const string SWAGGER_OPERATION_ISSUER_BOOK_PRODUCT = "Issuer - Books";
        public const string SWAGGER_OPERATION_ISSUER_CAMPAIGN = "Issuer - Campaigns";
        public const string SWAGGER_OPERATION_ISSUER_ORDER = "Issuer - Orders";
        public const string SWAGGER_OPERATION_ISSUER_PARTICIPANT = "Issuer - Participants";
        public const string SWAGGER_OPERATION_ISSUER_DASHBOARD_COMPARISON = "Issuer - Dashboards - Comparison";
        public const string SWAGGER_OPERATION_ISSUER_DASHBOARD_DATE_RANGE = "Issuer - Dashboards - Date Range";
        public const string SWAGGER_OPERATION_ISSUER_DASHBOARD_SUMMARY = "Issuer - Dashboards - Summary";
        #endregion

        #region Staff
        public const string SWAGGER_OPERATION_STAFF_CAMPAIGN = "Staff - Campaigns";
        public const string SWAGGER_OPERATION_STAFF_ORDER = "Staff - Orders";
        #endregion

        #region Mobile
        public const string SWAGGER_OPERATION_CUSTOMER_BOOK_PRODUCT = "Customer - Book Products";
        public const string SWAGGER_OPERATION_CUSTOMER_CAMPAIGN = "Customer - Campaigns";
        #endregion

        #endregion

        #region ZaloPay
        public const string APP_ID = "app_id";
        public const string APP_USER = "app_user";
        public const string APP_TIME = "app_time";
        public const string AMOUNT = "amount";
        public const string APP_TRANS_ID = "app_trans_id";
        public const string EMBED_DATA = "embed_data";
        public const string ITEM = "item";
        public const string DESCRIPTION = "description";
        public const string BANK_CODE = "bank_code";
        public const string MAC = "mac";
        public const string CALLBACK_URL = "callback_url";
        public const string REDIRECT_URL = "redirecturl";
        #endregion

        #region Notification

        #region Configuration
        public const string NOTI_URL = "/notificationHub";
        public const string NOTI_ACCESS_TOKEN = "access_token";
        public const string NOTI_MESS_URL = "ReceiveMess";
        #endregion

        #region Participant
        public const string NOTI_PARTICIPANT_ADMIN_APPROVAL_MESS = "Quản trị viên chấp nhận yêu cầu của bạn";
        public const string NOTI_PARTICIPANT_ADMIN_REJECTION_MESS = "Quản trị viên từ chối yêu cầu của bạn";
        public const string NOTI_PARTICIPANT_ADMIN_CANCELLATION_MESS = "Quản trị viên hủy lời mời";
        public const string NOTI_PARTICIPANT_ISSUER_APPROVAL_MESS = "chấp nhận lời mời";
        public const string NOTI_PARTICIPANT_ISSUER_REJECTION_MESS = "từ chối lời mời";
        #endregion

        #region Order
        public const string NOTI_NEW_SHIPPING_ORDER_MESS = "Có đơn hàng giao mới";
        public const string NOTI_NEW_PICK_UP_ORDER_MESS = "Có đơn hàng tại quầy mới";
        #endregion

        #endregion

        #region Telegram
        public const string TELEGRAM_CONFIG_API_TOKEN = "Telegram:ApiToken";
        public const string TELEGRAM_CONFIG_CHANNEL_ID = "Telegram:ChannelId";
        public const string TELEGRAM_CONFIG_BASE_URL = "https://api.telegram.org/bot{0}/sendMessage?chat_id={1}&text={2}";
        #endregion

        #region Middle ware
        public const string MIDDLE_WARE_CONFIG_IP = "CF-CONNECTING-IP";
        public const string MIDDLE_WARE_CONFIG_USER_AGENT = "User-Agent";
        public const string MIDDLE_WARE_CONFIG_LOGGER = "LoggingService";
        public const string MIDDLE_WARE_CONFIG_JSON_CONTENT_TYPE = "application/json";
        public const string MIDDLE_WARE_CONFIG_OTHER_CONTENT_TYPE = "Not a JSON request";
        #endregion

        #region Languages
        public const int LANGUAGE_VN = 0;
        public const int LANGUAGE_EN = 1;
        #endregion

        #region Email
        public const string WITH_VN = "Có";
        public const string WITH_EN = "Yes";
        public const string WITHOUT_VN = "Không";
        public const string WITHOUT_EN = "No";
        #endregion

        #region Dashboard
        public const string DASHBOARD_SUMMARY_ORDER_TITLE = "đơn hàng";
        public static string DASHBOARD_SUMMARY_CAMPAIGN_TITLE = $"{StatusExtension<CampaignStatus>.GetStatus((byte)CampaignStatus.Start).ToLower()}";
        public const string DASHBOARD_SUMMARY_BOOK_PRODUCT_TITLE = "sách được mua";
        public const string DASHBOARD_SUMMARY_SUBTITLE = "so với hôm qua";
        public static string DASHBOARD_SUMMARY_CAMPAIGN_SUBTITLE = $"{StatusExtension<CampaignStatus>.GetStatus((byte)CampaignStatus.NotStarted).ToLower()}";
        public const string DASHBOARD_SUMMARY_CAMPAIGN_NO = $"Chưa";
        public const string DASHBOARD_SUMMARY_INCREASE = "Tăng";
        public const string DASHBOARD_SUMMARY_DECREASE = "Giảm";
        public const string DASHBOARD_SUMMARY_SAME = "Bằng số lượng";
        #endregion
        #endregion

    }
}
