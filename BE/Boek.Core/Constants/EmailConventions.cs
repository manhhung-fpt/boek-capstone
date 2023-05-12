using Boek.Core.Enums;
using Boek.Core.Extensions;
using Microsoft.Extensions.Configuration;

namespace Boek.Core.Constants
{
    public class EmailConventions
    {
        #region Constants

        #region Body2_2
        public const string EMAIL_IMAGE = "[Image]";
        public const string EMAIL_TITLE = "[Title]";
        public const string EMAIL_QUANTITY = "[Quantity]";
        public const string EMAIL_SALE_PRICE = "[SalePrice]";
        public const string EMAIL_WITH_PDF = "[WithPdf]";
        public const string EMAIL_WITH_AUDIO = "[WithAudio]";
        public const string EMAIL_PDF_QUANTITY = "[PdfQuantity]";
        public const string EMAIL_AUDIO_QUANTITY = "[AudioQuantity]";
        public const string EMAIL_PDF_PRICE = "[PdfPrice]";
        public const string EMAIL_AUDIO_PRICE = "[AudioPrice]";
        public const string EMAIL_BOOK_FORMAT = "[Format]";
        #endregion

        #region Body2_3 and Note
        public const string EMAIL_CODE = "[Code]";
        public const string EMAIL_CUSTOMER_NAME = "[CustomerName]";
        public const string EMAIL_ORDER_STATUS = "[OrderStatus]";
        public const string EMAIL_TYPE = "[Type]";
        public const string EMAIL_ADDRESS = "[Address]";
        public const string EMAIL_ISSUER_NAME = "[Issuer.Name]";
        public const string EMAIL_NOTE = "[Note]";
        public const string EMAIL_PAYMENT_NAME = "[PaymentName]";
        public const string EMAIL_CAMPAIGN_NAME = "[CampaignName]";
        public const string EMAIL_SUB_TOTAL = "[SubTotal]";
        public const string EMAIL_FREIGHT = "[Freight]";
        public const string EMAIL_DISCOUNT = "[DiscountTotal]";
        public const string EMAIL_TOTAL = "[Total]";
        public const string EMAIL_INNER_FREIGHT = "[InnerFreight]";
        public const string EMAIL_OUTSIDE_FREIGHT = "[OutsideFreight]";
        #endregion

        #region Configuration
        public const string EMAIL_CONFIG_VN = "VN";
        public const string EMAIL_CONFIG_EN = "EN";
        public const string EMAIL_CONFIG_COLON = ":";
        public const string EMAIL_CONFIG_BODY_1 = "Body1";
        public const string EMAIL_CONFIG_BODY_2 = "Body2";
        public const string EMAIL_CONFIG_NOTE = "Note";
        public const string EMAIL_CONFIG_PICK_UP = "PickUp";
        public const string EMAIL_CONFIG_SHIPPING = "Shipping";

        #region VN
        public const string EMAIL_CONFIG_VN_TITLE = $"{EMAIL_CONFIG_VN}{EMAIL_CONFIG_COLON}Title";
        public const string EMAIL_CONFIG_VN_GREETING = $"{EMAIL_CONFIG_VN}{EMAIL_CONFIG_COLON}Sender";
        #region Body 1
        public const string EMAIL_CONFIG_VN_BODY_1_NEW_ORDER = $"{EMAIL_CONFIG_VN}{EMAIL_CONFIG_COLON}{EMAIL_CONFIG_BODY_1}{EMAIL_CONFIG_COLON}NewOrder";
        public const string EMAIL_CONFIG_VN_BODY_1_AVAILABLE = $"{EMAIL_CONFIG_VN}{EMAIL_CONFIG_COLON}{EMAIL_CONFIG_BODY_1}{EMAIL_CONFIG_COLON}Available";
        public const string EMAIL_CONFIG_VN_BODY_1_SHIPPING = $"{EMAIL_CONFIG_VN}{EMAIL_CONFIG_COLON}{EMAIL_CONFIG_BODY_1}{EMAIL_CONFIG_COLON}Shipping";
        public const string EMAIL_CONFIG_VN_BODY_1_DELIVERED = $"{EMAIL_CONFIG_VN}{EMAIL_CONFIG_COLON}{EMAIL_CONFIG_BODY_1}{EMAIL_CONFIG_COLON}Shipped";
        public const string EMAIL_CONFIG_VN_BODY_1_RECEIVED = $"{EMAIL_CONFIG_VN}{EMAIL_CONFIG_COLON}{EMAIL_CONFIG_BODY_1}{EMAIL_CONFIG_COLON}Received";
        public const string EMAIL_CONFIG_VN_BODY_1_BOEK_CANCELLATION = $"{EMAIL_CONFIG_VN}{EMAIL_CONFIG_COLON}{EMAIL_CONFIG_BODY_1}{EMAIL_CONFIG_COLON}BoekCancellation";
        public const string EMAIL_CONFIG_VN_BODY_1_CUSTOMER_CANCELLATION = $"{EMAIL_CONFIG_VN}{EMAIL_CONFIG_COLON}{EMAIL_CONFIG_BODY_1}{EMAIL_CONFIG_COLON}CustomerCancellation";
        public const string EMAIL_CONFIG_VN_BODY_1_ISSUER_CANCELLATION = $"{EMAIL_CONFIG_VN}{EMAIL_CONFIG_COLON}{EMAIL_CONFIG_BODY_1}{EMAIL_CONFIG_COLON}IssuerCancellation";
        #endregion
        public const string EMAIL_CONFIG_VN_BODY_2_1 = $"{EMAIL_CONFIG_VN}{EMAIL_CONFIG_COLON}{EMAIL_CONFIG_BODY_2}{EMAIL_CONFIG_COLON}Body2_1";
        public const string EMAIL_CONFIG_VN_BODY_2_2 = $"{EMAIL_CONFIG_VN}{EMAIL_CONFIG_COLON}{EMAIL_CONFIG_BODY_2}{EMAIL_CONFIG_COLON}Body2_2";
        public const string EMAIL_CONFIG_VN_BODY_2_3 = $"{EMAIL_CONFIG_VN}{EMAIL_CONFIG_COLON}{EMAIL_CONFIG_BODY_2}{EMAIL_CONFIG_COLON}Body2_3";
        public const string EMAIL_CONFIG_VN_SHIPPING_NOTE = $"{EMAIL_CONFIG_VN}{EMAIL_CONFIG_COLON}{EMAIL_CONFIG_NOTE}{EMAIL_CONFIG_COLON}{EMAIL_CONFIG_SHIPPING}";
        public const string EMAIL_CONFIG_VN_PICK_UP_NOTE = $"{EMAIL_CONFIG_VN}{EMAIL_CONFIG_COLON}{EMAIL_CONFIG_NOTE}{EMAIL_CONFIG_COLON}{EMAIL_CONFIG_PICK_UP}";
        public const string EMAIL_CONFIG_VN_CONCLUSION = $"{EMAIL_CONFIG_VN}{EMAIL_CONFIG_COLON}Conclusion";
        #endregion

        #region EN
        public const string EMAIL_CONFIG_EN_TITLE_1 = $"{EMAIL_CONFIG_EN}{EMAIL_CONFIG_COLON}Title1";
        public const string EMAIL_CONFIG_EN_TITLE_2 = $"{EMAIL_CONFIG_EN}{EMAIL_CONFIG_COLON}Title2";
        public const string EMAIL_CONFIG_EN_TITLE_3 = $"{EMAIL_CONFIG_EN}{EMAIL_CONFIG_COLON}Title3";
        public const string EMAIL_CONFIG_EN_SUB_TITLE = $"{EMAIL_CONFIG_EN}{EMAIL_CONFIG_COLON}SubTitle";
        public const string EMAIL_CONFIG_EN_GREETING = $"{EMAIL_CONFIG_EN}{EMAIL_CONFIG_COLON}Sender";
        #region Body 1
        public const string EMAIL_CONFIG_EN_BODY_1_NEW_ORDER = $"{EMAIL_CONFIG_EN}{EMAIL_CONFIG_COLON}{EMAIL_CONFIG_BODY_1}{EMAIL_CONFIG_COLON}NewOrder";
        public const string EMAIL_CONFIG_EN_BODY_1_AVAILABLE = $"{EMAIL_CONFIG_EN}{EMAIL_CONFIG_COLON}{EMAIL_CONFIG_BODY_1}{EMAIL_CONFIG_COLON}Available";
        public const string EMAIL_CONFIG_EN_BODY_1_SHIPPING = $"{EMAIL_CONFIG_EN}{EMAIL_CONFIG_COLON}{EMAIL_CONFIG_BODY_1}{EMAIL_CONFIG_COLON}Shipping";
        public const string EMAIL_CONFIG_EN_BODY_1_DELIVERED = $"{EMAIL_CONFIG_EN}{EMAIL_CONFIG_COLON}{EMAIL_CONFIG_BODY_1}{EMAIL_CONFIG_COLON}Shipped";
        public const string EMAIL_CONFIG_EN_BODY_1_RECEIVED = $"{EMAIL_CONFIG_EN}{EMAIL_CONFIG_COLON}{EMAIL_CONFIG_BODY_1}{EMAIL_CONFIG_COLON}Received";
        public const string EMAIL_CONFIG_EN_BODY_1_BOEK_CANCELLATION = $"{EMAIL_CONFIG_EN}{EMAIL_CONFIG_COLON}{EMAIL_CONFIG_BODY_1}{EMAIL_CONFIG_COLON}BoekCancellation";
        public const string EMAIL_CONFIG_EN_BODY_1_CUSTOMER_CANCELLATION = $"{EMAIL_CONFIG_EN}{EMAIL_CONFIG_COLON}{EMAIL_CONFIG_BODY_1}{EMAIL_CONFIG_COLON}CustomerCancellation";
        public const string EMAIL_CONFIG_EN_BODY_1_ISSUER_CANCELLATION = $"{EMAIL_CONFIG_EN}{EMAIL_CONFIG_COLON}{EMAIL_CONFIG_BODY_1}{EMAIL_CONFIG_COLON}IssuerCancellation";
        #endregion
        public const string EMAIL_CONFIG_EN_BODY_2_1 = $"{EMAIL_CONFIG_EN}{EMAIL_CONFIG_COLON}{EMAIL_CONFIG_BODY_2}{EMAIL_CONFIG_COLON}Body2_1";
        public const string EMAIL_CONFIG_EN_BODY_2_2 = $"{EMAIL_CONFIG_EN}{EMAIL_CONFIG_COLON}{EMAIL_CONFIG_BODY_2}{EMAIL_CONFIG_COLON}Body2_2";
        public const string EMAIL_CONFIG_EN_BODY_2_3 = $"{EMAIL_CONFIG_EN}{EMAIL_CONFIG_COLON}{EMAIL_CONFIG_BODY_2}{EMAIL_CONFIG_COLON}Body2_3";
        public const string EMAIL_CONFIG_EN_SHIPPING_NOTE = $"{EMAIL_CONFIG_EN}{EMAIL_CONFIG_COLON}{EMAIL_CONFIG_NOTE}{EMAIL_CONFIG_COLON}{EMAIL_CONFIG_SHIPPING}";
        public const string EMAIL_CONFIG_EN_PICK_UP_NOTE = $"{EMAIL_CONFIG_EN}{EMAIL_CONFIG_COLON}{EMAIL_CONFIG_NOTE}{EMAIL_CONFIG_COLON}{EMAIL_CONFIG_PICK_UP}";
        public const string EMAIL_CONFIG_EN_CONCLUSION = $"{EMAIL_CONFIG_EN}{EMAIL_CONFIG_COLON}Conclusion";
        #endregion

        #endregion

        #endregion
    }

    public class Email
    {
        public string Title { get; set; }
        public string Greeting { get; set; }
        public string Body1 { get; set; }
        public string Body2_1 { get; set; }
        public string Body2_2 { get; set; }
        public string Body2_3 { get; set; }
        public string Note { get; set; }
        public string Conclusion { get; set; }
        public int Language { get; set; }
        public bool IsValid()
        => !string.IsNullOrEmpty(Greeting) && !string.IsNullOrEmpty(Body1) &&
        !string.IsNullOrEmpty(Body2_1) && !string.IsNullOrEmpty(Body2_2) &&
        !string.IsNullOrEmpty(Body2_3) && !string.IsNullOrEmpty(Note) &&
        !string.IsNullOrEmpty(Conclusion);

        public void SetEmptyData()
        {
            Title = string.Empty;
            Greeting = string.Empty;
            Body1 = string.Empty;
            Body2_1 = string.Empty;
            Body2_2 = string.Empty;
            Body2_3 = string.Empty;
            Note = string.Empty;
            Conclusion = string.Empty;
        }

        public void ReplaceEmailSample(Dictionary<List<string>, Dictionary<string, string>> pairs)
        {
            Title = SetDataByReplacingDetail(pairs, nameof(Title), Title);
            Greeting = SetDataByReplacingDetail(pairs, nameof(Greeting), Greeting);
            Body1 = SetDataByReplacingDetail(pairs, nameof(Body1), Body1);
            Body2_1 = SetDataByReplacingDetail(pairs, nameof(Body2_1), Body2_1);
            Body2_2 = SetDataByReplacingDetail(pairs, nameof(Body2_2), Body2_2);
            Body2_3 = SetDataByReplacingDetail(pairs, nameof(Body2_3), Body2_3);
            Note = SetDataByReplacingDetail(pairs, nameof(Note), Note);
            Conclusion = SetDataByReplacingDetail(pairs, nameof(Conclusion), Conclusion);
        }

        private string SetDataByReplacingDetail(Dictionary<List<string>, Dictionary<string, string>> pairs, string name, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var temp = pairs.Where(item => item.Key.Contains(name)).ToList();
                if (temp.Any())
                {
                    temp.ForEach(item =>
                    {
                        var detail = item.Value.First();
                        value = value.Replace(detail.Key, detail.Value);
                    });
                }
            }
            return value;
        }
    }

    public class VietnameseEmail
    {
        public VietnameseEmail(OrderStatus status, OrderType type, IConfiguration configuration, BoekRole role = BoekRole.Customer, bool IsBoek = false)
        {
            Email = new Email();
            Email.Title = configuration.GetSection(EmailConventions.EMAIL_CONFIG_VN_TITLE).Value + $" {EmailConventions.EMAIL_CODE}";
            Email.Greeting = configuration.GetSection(EmailConventions.EMAIL_CONFIG_VN_GREETING).Value;
            Email.Body2_1 = configuration.GetSection(EmailConventions.EMAIL_CONFIG_VN_BODY_2_1).Value;
            Email.Body2_2 = configuration.GetSection(EmailConventions.EMAIL_CONFIG_VN_BODY_2_2).Value;
            Email.Body2_3 = configuration.GetSection(EmailConventions.EMAIL_CONFIG_VN_BODY_2_3).Value;
            Email.Note = type.Equals(OrderType.PickUp) ? configuration.GetSection(EmailConventions.EMAIL_CONFIG_VN_PICK_UP_NOTE).Value :
            configuration.GetSection(EmailConventions.EMAIL_CONFIG_VN_SHIPPING_NOTE).Value;
            Email.Conclusion = configuration.GetSection(EmailConventions.EMAIL_CONFIG_VN_CONCLUSION).Value;
            Email.Language = MessageConstants.LANGUAGE_VN;
            SetBody1ByOrderStatus(status, configuration, role, IsBoek);
            SetTitleStatus(status);
        }
        public Email Email { get; set; }

        public void SetBody1ByOrderStatus(OrderStatus status, IConfiguration configuration, BoekRole role = BoekRole.Customer, bool IsBoek = false)
        {
            switch (status)
            {
                case OrderStatus.Processing:
                    Email.Body1 = configuration.GetSection(EmailConventions.EMAIL_CONFIG_VN_BODY_1_NEW_ORDER).Value;
                    break;
                case OrderStatus.PickUpAvailable:
                    Email.Body1 = configuration.GetSection(EmailConventions.EMAIL_CONFIG_VN_BODY_1_AVAILABLE).Value;
                    break;
                case OrderStatus.Shipping:
                    Email.Body1 = configuration.GetSection(EmailConventions.EMAIL_CONFIG_VN_BODY_1_SHIPPING).Value;
                    break;
                case OrderStatus.Shipped:
                    Email.Body1 = configuration.GetSection(EmailConventions.EMAIL_CONFIG_VN_BODY_1_DELIVERED).Value;
                    break;
                case OrderStatus.Received:
                    Email.Body1 = configuration.GetSection(EmailConventions.EMAIL_CONFIG_VN_BODY_1_RECEIVED).Value;
                    break;
                case OrderStatus.Cancelled:
                    if (IsBoek)
                        Email.Body1 = configuration.GetSection(EmailConventions.EMAIL_CONFIG_VN_BODY_1_BOEK_CANCELLATION).Value;
                    else
                    {
                        if (role.Equals(BoekRole.Customer))
                            Email.Body1 = configuration.GetSection(EmailConventions.EMAIL_CONFIG_VN_BODY_1_CUSTOMER_CANCELLATION).Value;
                        else if (role.Equals(BoekRole.Issuer))
                            Email.Body1 = configuration.GetSection(EmailConventions.EMAIL_CONFIG_VN_BODY_1_ISSUER_CANCELLATION).Value;
                    }
                    break;
            }
        }

        public void SetTitleStatus(OrderStatus status)
        {
            if (!string.IsNullOrEmpty(Email.Title))
            {
                var StatusName = StatusExtension<OrderStatus>.GetStatus((byte)status);
                if (!string.IsNullOrEmpty(StatusName))
                    Email.Title += $" {StatusName.ToLower()} - ";
            }
        }
    }

    public class EnglishEmail
    {
        public EnglishEmail(OrderStatus status, OrderType type, IConfiguration configuration, BoekRole role = BoekRole.Customer, bool IsBoek = false)
        {
            Email = new Email();
            Email.Greeting = configuration.GetSection(EmailConventions.EMAIL_CONFIG_EN_GREETING).Value;
            Email.Body2_1 = configuration.GetSection(EmailConventions.EMAIL_CONFIG_EN_BODY_2_1).Value;
            Email.Body2_2 = configuration.GetSection(EmailConventions.EMAIL_CONFIG_EN_BODY_2_2).Value;
            Email.Body2_3 = configuration.GetSection(EmailConventions.EMAIL_CONFIG_EN_BODY_2_3).Value;
            Email.Note = type.Equals(OrderType.PickUp) ? configuration.GetSection(EmailConventions.EMAIL_CONFIG_EN_PICK_UP_NOTE).Value :
            configuration.GetSection(EmailConventions.EMAIL_CONFIG_EN_SHIPPING_NOTE).Value;
            Email.Conclusion = configuration.GetSection(EmailConventions.EMAIL_CONFIG_EN_CONCLUSION).Value;
            Email.Language = MessageConstants.LANGUAGE_EN;
            SetTitleAndBody1ByOrderStatus(status, configuration, role, IsBoek);
        }
        public Email Email { get; set; }

        public void SetTitleAndBody1ByOrderStatus(OrderStatus status, IConfiguration configuration, BoekRole role = BoekRole.Customer, bool IsBoek = false)
        {
            switch (status)
            {
                case OrderStatus.Processing:
                    Email.Title = configuration.GetSection(EmailConventions.EMAIL_CONFIG_EN_TITLE_1).Value;
                    Email.Body1 = configuration.GetSection(EmailConventions.EMAIL_CONFIG_EN_BODY_1_NEW_ORDER).Value;
                    break;
                case OrderStatus.PickUpAvailable:
                    Email.Title = configuration.GetSection(EmailConventions.EMAIL_CONFIG_EN_TITLE_2).Value;
                    Email.Body1 = configuration.GetSection(EmailConventions.EMAIL_CONFIG_EN_BODY_1_AVAILABLE).Value;
                    break;
                case OrderStatus.Shipping:
                    Email.Title = configuration.GetSection(EmailConventions.EMAIL_CONFIG_EN_TITLE_2).Value;
                    Email.Body1 = configuration.GetSection(EmailConventions.EMAIL_CONFIG_EN_BODY_1_SHIPPING).Value;
                    break;
                case OrderStatus.Shipped:
                    Email.Title = configuration.GetSection(EmailConventions.EMAIL_CONFIG_EN_TITLE_3).Value;
                    Email.Body1 = configuration.GetSection(EmailConventions.EMAIL_CONFIG_EN_BODY_1_DELIVERED).Value;
                    break;
                case OrderStatus.Received:
                    Email.Title = configuration.GetSection(EmailConventions.EMAIL_CONFIG_EN_TITLE_3).Value;
                    Email.Body1 = configuration.GetSection(EmailConventions.EMAIL_CONFIG_EN_BODY_1_RECEIVED).Value;
                    break;
                case OrderStatus.Cancelled:
                    Email.Title = configuration.GetSection(EmailConventions.EMAIL_CONFIG_EN_TITLE_3).Value;
                    if (IsBoek)
                        Email.Body1 = configuration.GetSection(EmailConventions.EMAIL_CONFIG_EN_BODY_1_BOEK_CANCELLATION).Value;
                    else
                    {
                        if (role.Equals(BoekRole.Customer))
                            Email.Body1 = configuration.GetSection(EmailConventions.EMAIL_CONFIG_EN_BODY_1_CUSTOMER_CANCELLATION).Value;
                        else if (role.Equals(BoekRole.Issuer))
                            Email.Body1 = configuration.GetSection(EmailConventions.EMAIL_CONFIG_EN_BODY_1_ISSUER_CANCELLATION).Value;
                    }
                    break;
            }
            if (!status.Equals(OrderStatus.Processing))
                SetTitleStatus(status);

        }

        public void SetTitleStatus(OrderStatus status)
        {
            if (!string.IsNullOrEmpty(Email.Title))
            {
                var StatusName = StatusExtension<OrderStatus>.GetStatus((byte)status, false, MessageConstants.LANGUAGE_EN);
                if (!string.IsNullOrEmpty(StatusName))
                    Email.Title += $" {StatusName.ToLower()}";
            }
        }
    }

    public class EmailDetail
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string SubTitle { get; set; }
        public string Body { get; set; }
        public bool IsValid { get; set; }
    }
}