using Boek.Core.Extensions;
using Boek.Core.Enums;

namespace Boek.Core.Constants.Mobile
{
    public class MobileConstants
    {
        #region Campaign - Book Product - Title
        public const string TITLE = "Tiêu đề";
        public const string TITLE_DISCOUNT = "Sách giảm giá";
        public const string TITLE_COMBO = "Sách combo";
        public const string TITLE_GENRE = "Sách thể loại";
        public const string TITLE_ISSUER = "Sách của nhà phát hành";
        public const string TITLE_SAME_GENRE = "Cùng thể loại";
        public const string TITLE_SAME_ISSUER = "Cùng NPH";
        #endregion

        #region Staff - Campaign - Title
        public static string TITLE_CAMPAIGN_NOT_START = $"{StatusExtension<CampaignStatus>.GetStatus((byte)CampaignStatus.NotStarted)}";
        public static string TITLE_CAMPAIGN_STARTED = $"{StatusExtension<CampaignStatus>.GetStatus((byte)CampaignStatus.Start)}";
        public static string TITLE_CAMPAIGN_ENDED = $"{StatusExtension<CampaignStatus>.GetStatus((byte)CampaignStatus.End)}";
        public static string TITLE_CAMPAIGN_CANCELLED = $"{StatusExtension<CampaignStatus>.GetStatus((byte)CampaignStatus.Cancelled)}";
        public static string TITLE_CAMPAIGN_POSTPONED = $"{StatusExtension<CampaignStatus>.GetStatus((byte)CampaignStatus.Postponed)}";
        #endregion

        #region Customer - Campaign - Title

        public const string TITLE_CUSTOMER_CAMPAIGN = "Hội sách";
        public static string TITLE_CUSTOMER_OFFLINE_CAMPAIGN = $"{TITLE_CUSTOMER_CAMPAIGN} {StatusExtension<CampaignFormat>.GetStatus((byte)CampaignFormat.Offline)}";
        public static string TITLE_CUSTOMER_ONLINE_CAMPAIGN = $"{TITLE_CUSTOMER_CAMPAIGN} {StatusExtension<CampaignFormat>.GetStatus((byte)CampaignFormat.Online)}";
        public static string TITLE_CUSTOMER_NOT_STARTED_CAMPAIGN = $"Chưa diễn ra";
        public static string TITLE_CUSTOMER_START_CAMPAIGN = $"Diễn ra";
        public const string TITLE_CUSTOMER_CAMPAIGN_ORGANIZATION = "Tổ chức của bạn";
        public const string TITLE_CUSTOMER_CAMPAIGN_GROUP = "Nhóm bạn quan tâm";
        public const string TITLE_CUSTOMER_CAMPAIGN_LEVEL = "Hội sách cấp độ dành cho bạn";
        public const string TITLE_CUSTOMER_CAMPAIGN_ADDRESS = "Hội sách gần chỗ bạn";
        #endregion
    }
}