import { BasicBookViewModel } from "../Books/BasicBookViewModel";

export interface GenreBooksViewModel {
    id: number;
    parentId: number;
    name: string;
    displayIndex: number;
    status: boolean;
    StatusName: string;
    books: BasicBookViewModel
}