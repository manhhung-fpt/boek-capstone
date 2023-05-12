export interface BaseResponseModel<T> {
    status: StatusModel;
    data: T;
}
interface StatusModel {
    success: boolean;
    message: string;
    status: string;
}