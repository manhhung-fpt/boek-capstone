export interface BaseResponsePagingModel<T> {
    metadata: PagingMetadata;
    data: T[];
}
export interface PagingMetadata {
    page: number;
    size: number;
    total: number;
}