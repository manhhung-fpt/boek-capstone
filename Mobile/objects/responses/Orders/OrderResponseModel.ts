export interface ZaloPayOrderResponseModel {
    return_code: number;
    return_message: string;
    sub_return_code: number;
    sub_return_message: string;
    zp_trans_token: string;
    app_trans_id: string;
    order_url: string;
    order_token: string;
  }
  