import { AuthorViewModel } from "../Authors/AuthorViewModel";

export interface BookAuthorViewModel {
    id?: number;
    bookId?: number;
    authorId?: number;
    author: AuthorViewModel;
}