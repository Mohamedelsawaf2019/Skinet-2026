export type Pagination<T> ={
    pageIndex: number;
    pageNumber: number;
    count: number;
    data: T[];
}