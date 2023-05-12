namespace Boek.Infrastructure.Responds.Orders
{
    public class ZaloPayOrderResponseModel
    {
        public byte? return_code { get; set; }
        public string return_message { get; set; }
        public int? sub_return_code { get; set; }
        public string sub_return_message { get; set; }
        public string zp_trans_token { get; set; }
        public string app_trans_id { get; set; }
        public string order_url { get; set; }
        public string order_token { get; set; }
    }

    public class ZaloPayQueryCreatedOrderResponseModel
    {
        public byte? return_code { get; set; }
        public string return_message { get; set; }
        public byte? sub_return_code { get; set; }
        public string sub_return_message { get; set; }
        public bool? is_processing { get; set; }
        public decimal? amount { get; set; }
        public ulong? zp_trans_id { get; set; }
        public ulong? server_time { get; set; }
        public decimal? discount_amount { get; set; }

    }

    public class ZaloPayCallBackResponseModel
    {
        public int? return_code { get; set; }
        public string return_message { get; set; }
    }
}
