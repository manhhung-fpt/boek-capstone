using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boek.Core.Constants;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.Books;
using Boek.Infrastructure.Requests.Books.BookSeries;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.Books;
using Boek.Infrastructure.ViewModels.Genres;
using Boek.Repository.Interfaces;
using Boek.Service.Commons;
using Boek.Service.Interfaces;
using Boek.Service.Utils;
using Boek.Core.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Boek.Infrastructure.ViewModels.Campaigns;
using Boek.Infrastructure.ViewModels.Books.Issuers;

namespace Boek.Service.Services
{
    public class BookService : IBookService
    {
        #region Fields and constructor
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public BookService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Gets
        public BookViewModel GetBookById(int id)
        {
            var book = _unitOfWork.Books.Get(id);
            if (book == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[] {
                        ErrorMessageConstants.NOT_FOUND,
                        ErrorMessageConstants.BOOK_ID
                    });
            return GetRespond(book);
        }

        public IssuerBookViewModel GetBookByIdByIssuer(int id, bool WithCampaigns = false, bool WithAllowChangingGenre = false)
        {
            var IssuerId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            var book = _unitOfWork.Books
                    .Get(b => b.Id.Equals(id) && b.IssuerId.Equals(IssuerId))
                    .ProjectTo<IssuerBookViewModel>(_mapper.ConfigurationProvider)
                    .SingleOrDefault();
            if (book == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.BOOK_ID
                });
            if (WithCampaigns)
            {
                var campaigns = _unitOfWork.Books.GetCampaigns(id);
                if (campaigns.Any())
                    book.Campaigns = _mapper.Map<List<BasicCampaignViewModel>>(campaigns);
            }
            if (WithAllowChangingGenre)
                book.AllowChangingGenre = _unitOfWork.Books.IsAllowChangingGenre(id);
            return ServiceUtils.GetBookResponseDetail(book);
        }

        public BaseResponsePagingModel<BookViewModel> GetBooks(BookRequestModel filter, PagingModel paging)
        {
            filter.GenreIds = ServiceUtils.CheckBookGenreFilter(filter.GenreIds, _unitOfWork);
            var result = _unitOfWork.Books.Get()
                .ProjectTo<BookViewModel>(_mapper.ConfigurationProvider)
                .DynamicOtherFilter(filter)
                .PagingQueryable(paging.Page, paging.Size, PageConstant.LimitPaging, PageConstant.LimitPaging);
            var list = new List<BookViewModel>();
            if (result.Item1 > 0)
                result.Item2.ToList().ForEach(x => list.Add((ServiceUtils.GetBookResponseDetail(x))));

            return new BaseResponsePagingModel<BookViewModel>()
            {
                Metadata =
                    new PagingMetadata()
                    {
                        Page = paging.Page,
                        Size = paging.Size,
                        Total = result.Item1
                    },
                Data = list
            };
        }

        public BaseResponsePagingModel<BookViewModel> GetBooksByIssuer(BookRequestModel filter, PagingModel paging)
        {
            filter.GenreIds = ServiceUtils.CheckBookGenreFilter(filter.GenreIds, _unitOfWork);
            filter.IssuerId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            var result = _unitOfWork.Books.Get()
                    .ProjectTo<BookViewModel>(_mapper.ConfigurationProvider)
                    .DynamicOtherFilter(filter)
                    .PagingQueryable(paging.Page, paging.Size, PageConstant.LimitPaging, PageConstant.LimitPaging);
            var list = result.Item2.ToList();
            if (list.Any())
                list.ForEach(b => b = ServiceUtils.GetBookResponseDetail(b));

            return new BaseResponsePagingModel<BookViewModel>()
            {
                Metadata =
                    new PagingMetadata()
                    {
                        Page = paging.Page,
                        Size = paging.Size,
                        Total = result.Item1
                    },
                Data = list
            };
        }

        public BaseResponsePagingModel<BookViewModel> GetBooksForOddOrSeriesBookProductByIssuer(SearchBookRequestModel filter, PagingModel paging)
        {
            filter.GenreIds = ServiceUtils.CheckBookGenreFilter(filter.GenreIds, _unitOfWork);
            filter.IssuerId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            var result = _unitOfWork.Books.Get(b => !b.BookProducts.Any(bp => bp.CampaignId.Equals(filter.CurrentCampaignId)))
                    .ProjectTo<BookViewModel>(_mapper.ConfigurationProvider)
                    .DynamicOtherFilter(filter)
                    .PagingQueryable(paging.Page, paging.Size, PageConstant.LimitPaging, PageConstant.LimitPaging);
            var list = result.Item2.ToList();
            if (list.Any())
                list.ForEach(b => b = ServiceUtils.GetBookResponseDetail(b));

            return new BaseResponsePagingModel<BookViewModel>()
            {
                Metadata =
                    new PagingMetadata()
                    {
                        Page = paging.Page,
                        Size = paging.Size,
                        Total = result.Item1
                    },
                Data = list
            };
        }

        public BaseResponsePagingModel<BookViewModel> GetBooksByParentGenre(int? ParentGenreId, int? GenreId, PagingModel paging)
        {
            (int, IQueryable<BookViewModel>) result = (0, null);
            var list = new List<BookViewModel>();
            if (ParentGenreId == null)
            {
                result = _unitOfWork.Books.Get()
                .ProjectTo<BookViewModel>(_mapper.ConfigurationProvider)
                .PagingQueryable(paging.Page, paging.Size, PageConstant.LimitPaging, PageConstant.LimitPaging);
            }
            var _books = GetBooksByGenres(ParentGenreId, GenreId);
            if (_books != null)
            {
                var bookIds = _books.Select(x => x.Id).ToList();
                result = _unitOfWork.Books.Get(b => bookIds.Contains(b.Id))
                .ProjectTo<BookViewModel>(_mapper.ConfigurationProvider)
                .PagingQueryable(paging.Page, paging.Size, PageConstant.LimitPaging, PageConstant.LimitPaging);
            }
            list = result.Item2.ToList();
            if (list.Any())
                list.ForEach(b => b = ServiceUtils.GetBookResponseDetail(b));
            return new BaseResponsePagingModel<BookViewModel>()
            {
                Metadata = new PagingMetadata()
                {
                    Page = paging.Page,
                    Size = paging.Size,
                    Total = result.Item1
                },
                Data = list
            };
        }
        #endregion

        #region Creates
        public BookViewModel CreateBook(CreateBookRequestModel createdBook)
        {
            var issuerId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            var Book = _mapper.Map<Book>(createdBook);
            CheckBook(Book, createdBook.Authors, (int)createdBook.GenreId);
            var authorBooks = new List<BookAuthor>();
            foreach (var author in createdBook.Authors)
            {
                authorBooks.Add(new BookAuthor() { AuthorId = author });
            }
            Book.BookAuthors = authorBooks;
            Book.Status = (byte)BookStatus.Releasing;
            Book.IssuerId = issuerId;
            Book.IsSeries = false;
            _unitOfWork.Books.Create(Book);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.INSERT,
                    ErrorMessageConstants.BOOK.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            Book = _unitOfWork.Books
                    .Get(b => b.Name.Equals(Book.Name))
                    .SingleOrDefault();
            return GetRespond(Book);
        }

        public BookViewModel CreateBookSeries(CreateBookSeriesRequestModel createBookSeries)
        {
            var issuerId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            var Book = _mapper.Map<Book>(createBookSeries);
            CheckSeriesBook(Book, createBookSeries.GenreId);
            var bookItems = Book.BookItemBooks.ToList();
            Book.BookItemBooks = null;
            Book.Status = (byte)BookStatus.Releasing;
            Book.IssuerId = issuerId;
            Book.IsSeries = true;
            _unitOfWork.Books.Create(Book);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.INSERT,
                    ErrorMessageConstants.BOOK_SERIES.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            Book = _unitOfWork.Books.Get(b => b.Name.Equals(Book.Name)).SingleOrDefault();
            bookItems.ForEach(bi => bi.ParentBookId = Book.Id);
            _unitOfWork.BookItems.AddRange(bookItems);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.INSERT,
                    ErrorMessageConstants.BOOK_ITEM.ToLower(),
                    ErrorMessageConstants.BOOK_SERIES.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            return GetRespond(Book);
        }
        #endregion

        #region Updates
        public BookViewModel UpdateBook(UpdateBookRequestModel updatedBook)
        {
            CheckBook(_mapper.Map<Book>(updatedBook), updatedBook.Authors, updatedBook.GenreId, false);

            var Book = _unitOfWork.Books.Get(updatedBook.Id);
            CheckIssuer(Book.IssuerId);
            var UpdateBook = Book;
            var OldBook = new Book()
            {
                Name = Book.Name,
                Description = Book.Description,
                ImageUrl = Book.ImageUrl,
                CoverPrice = Book.CoverPrice
            };
            _mapper.Map(updatedBook, UpdateBook);
            UpdateBook.UpdatedDate = DateTime.Now;
            UpdateBook.BookAuthors = new List<BookAuthor>();
            _unitOfWork.Books.Update(UpdateBook);
            if (!_unitOfWork.Save())
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.UPDATE,
                    ErrorMessageConstants.BOOK.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            }
            UpdateBookAuthors(updatedBook.Authors, UpdateBook.Id);
            UpdateOtherInfo(UpdateBook, OldBook);
            return GetRespond(UpdateBook);
        }

        public BookViewModel UpdateBookSeries(UpdateBookSeriesRequestModel updateBookSeries)
        {
            CheckSeriesBook(_mapper.Map<Book>(updateBookSeries), updateBookSeries.GenreId, false);
            var Book = _unitOfWork.Books.Get(updateBookSeries.Id);
            CheckIssuer(Book.IssuerId);
            var UpdateBook = Book;
            var OldBook = new Book()
            {
                Name = Book.Name,
                Description = Book.Description,
                ImageUrl = Book.ImageUrl,
                CoverPrice = Book.CoverPrice
            };
            _mapper.Map(updateBookSeries, UpdateBook);
            var bookItems = UpdateBook.BookItemBooks.ToList();
            UpdateBook.BookItemBooks = null;
            UpdateBook.UpdatedDate = DateTime.Now;
            _unitOfWork.Books.Update(UpdateBook);
            if (!_unitOfWork.Save())
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    ErrorMessageConstants.UPDATE,
                    ErrorMessageConstants.BOOK_SERIES.ToLower(),
                    MessageConstants.MESSAGE_FAILED
                });
            }
            UpdateBookItems(bookItems, updateBookSeries.Id);
            UpdateOtherInfo(UpdateBook, OldBook);
            return GetRespond(UpdateBook);
        }
        #endregion

        #region Enable or Disable Book
        public BookViewModel EnableBook(int id)
        {
            var book = _unitOfWork.Books.Get(id);
            if (book == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.BOOK_ID
                });
            CheckIssuer(book.IssuerId);
            if (book.Status.Equals((byte)BookStatus.Unreleased))
            {
                book.Status = (byte)BookStatus.Releasing;
                _unitOfWork.Books.Update(book);
                if (!_unitOfWork.Save())
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                    {
                        ErrorMessageConstants.UPDATE,
                        ErrorMessageConstants.BOOK_STATUS,
                        MessageConstants.MESSAGE_FAILED
                    });
                book = _unitOfWork.Books.Get(id);
                CheckDisabledOrEnabledBook(book);
            }
            return GetRespond(book);
        }

        public BookViewModel DisableBook(int id)
        {
            var book =
                _unitOfWork.Books.Get(b => b.Id.Equals(id)).SingleOrDefault();
            if (book == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.BOOK_ID
                });
            CheckIssuer(book.IssuerId);
            if (book.Status.Equals((byte)BookStatus.Releasing))
            {
                book.Status = (byte)BookStatus.Unreleased;
                _unitOfWork.Books.Update(book);
                if (!_unitOfWork.Save())
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                    {
                        ErrorMessageConstants.UPDATE,
                        ErrorMessageConstants.BOOK_STATUS,
                        MessageConstants.MESSAGE_FAILED
                    });
                CheckDisabledOrEnabledBook(book);
                book = _unitOfWork.Books.Get(id);
            }
            return GetRespond(book);
        }
        #endregion

        #region Utils
        private BookViewModel GetRespond(Book book, bool WithCampaigns = false)
        {
            var response = _unitOfWork.Books
                    .Get(b => b.Id == book.Id)
                    .ProjectTo<BookViewModel>(_mapper.ConfigurationProvider)
                    .SingleOrDefault();
            if (response != null) ServiceUtils.GetBookResponseDetail(response);
            return response;
        }

        private List<Book> GetBooksByGenres(int? ParentGenreId, int? GenreId = null)
        {
            var _genre = _unitOfWork.Genres.Get(g => g.Id.Equals(ParentGenreId))
                    .ProjectTo<ParentGenreViewModel>(_mapper.ConfigurationProvider)
                    .SingleOrDefault();
            //cSpell:disable
            if (_genre != null)
            {
                var _books = new List<Book>();
                var _childgenre = _genre.Genres;
                if (GenreId != null)
                    _childgenre = _childgenre.Where(cg => cg.Id.Equals(GenreId)).ToList();
                _childgenre.ForEach(g => g.Books.ForEach(b => _books.Add(b)));
                return _books;
            }
            return new List<Book>();
        }

        private void CheckBook(Book book, List<int?> authors, int? genre, bool isCreate = true)
        {
            Book OldBook = null;
            if (!isCreate)
            {
                OldBook = _unitOfWork.Books.Get(book.Id);
                if (OldBook == null)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                    {
                        ErrorMessageConstants.NOT_FOUND,
                        ErrorMessageConstants.BOOK_ID
                    });
            }
            CheckISBN(book, OldBook, isCreate);
            if (_unitOfWork.Publishers.Get(book.PublisherId) == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.PUBLISHER_ID
                });
            var _book = CheckDuplicatedBookName(book.Name);
            if (_book != null)
            {
                var IssuerId = ServiceUtils.GetUserInfo(_httpContextAccessor);
                //Duplicated existed book name
                if (isCreate && IssuerId.Equals(_book.IssuerId))
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                    {
                        MessageConstants.MESSAGE_DUPLICATED_INFO,
                        ErrorMessageConstants.BOOK_NAME
                    });

                //Duplicated other book name
                if (!isCreate && !(_book.Id.Equals(book.Id)) && IssuerId.Equals(_book.IssuerId))
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                    {
                        MessageConstants.MESSAGE_DUPLICATED_INFO,
                        ErrorMessageConstants.BOOK_NAME
                    });
            }
            CheckAuthorList(authors);
            CheckGenre(genre);
            CheckPdfAndAudio(book);
        }
        private void CheckSeriesBook(Book book, int? genre, bool isCreate = true)
        {
            Book OldBook = null;
            if (!isCreate)
            {
                OldBook = _unitOfWork.Books.Get(book.Id);
                if (OldBook == null)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                    {
                        ErrorMessageConstants.NOT_FOUND,
                        ErrorMessageConstants.BOOK_ID
                    });
                if (!(bool)OldBook.IsSeries)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                    {
                        ErrorMessageConstants.BOOK,
                        MessageConstants.MESSAGE_NOT_BELONGING,
                        ErrorMessageConstants.BOOK_SERIES.ToLower()
                    });
            }
            CheckISBN(book, OldBook, isCreate);
            var _book = CheckDuplicatedBookName(book.Name);
            if (_book != null)
            {
                //Duplicated existed book name
                if (isCreate)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                    {
                        MessageConstants.MESSAGE_DUPLICATED_INFO,
                        ErrorMessageConstants.BOOK_NAME
                    });

                //Duplicated other book name
                if (!isCreate && !(_book.Id.Equals(book.Id)))
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                    {
                        MessageConstants.MESSAGE_DUPLICATED_INFO,
                        ErrorMessageConstants.BOOK_NAME
                    });
            }
            var bookItems = book.BookItemBooks.ToList();
            CheckBookItems(bookItems);
            CheckGenre(genre, bookItems);
        }

        private void CheckAuthorList(List<int?> authors)
        {
            foreach (var author in authors)
            {
                var _temp = _unitOfWork.Authors.Get(author);
                if (_temp == null)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                    {
                        ErrorMessageConstants.NOT_FOUND,
                        ErrorMessageConstants.AUTHOR_ID
                    });
            }
        }

        private void CheckGenre(int? genre, List<BookItem> bookItems = null)
        {
            if (!_unitOfWork.Genres.ValidGenre(genre))
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.GENRE_ID.ToLower(),
                    genre.ToString()
                });
            if (bookItems != null)
            {
                foreach (var bookItem in bookItems)
                {
                    var temp = _unitOfWork.Books.Get(bookItem.BookId);
                    if (!temp.GenreId.Equals(genre))
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                        {
                            ErrorMessageConstants.BOOK,
                            bookItem.BookId.ToString(),
                            MessageConstants.MESSAGE_NOT_DUPLICATED_INFO.ToLower(),
                            ErrorMessageConstants.GENRE
                        });
                }
            }
        }

        private void CheckBookItems(List<BookItem> bookItems)
        {
            if (!bookItems.Any())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.PreconditionRequired), new string[]
                {
                    MessageConstants.MESSAGE_REQUIRED,
                    ErrorMessageConstants.BOOK_ITEM.ToLower(),
                    ErrorMessageConstants.BOOK_SERIES.ToLower()
                });
            if (bookItems.Count < 2)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                {
                    ErrorMessageConstants.BOOK_PRODUCT_ITEM.ToLower(),
                    ErrorMessageConstants.BOOK_SERIES.ToLower(),
                    ErrorMessageConstants.BOOK_PRODUCT_ITEM_INVALID_AMOUNT
                });
            var bookIds = bookItems.Select(b => b.BookId);
            var duplicates = bookIds.GroupBy(b => b).ToList();
            duplicates.ForEach(d =>
            {
                if (d.Count() > 1)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                    {
                        MessageConstants.MESSAGE_DUPLICATED_INFO,
                        ErrorMessageConstants.BOOK_ITEM.ToLower(),
                        ErrorMessageConstants.BOOK_SERIES.ToLower()
                    });
            });
            var authorIds = new List<int?>();
            foreach (var bookItem in bookItems)
            {
                var _book = _unitOfWork.Books.Get(b =>
                b.Id.Equals(bookItem.BookId))
                    .Include(b => b.BookAuthors)
                    .SingleOrDefault();
                if (_book == null)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.BOOK_ID,
                    bookItem.BookId.ToString()
                });
                if (_book.Status.Equals((byte)BookStatus.Unreleased))
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                    {
                        ErrorMessageConstants.BOOK_STATUS,
                        MessageConstants.MESSAGE_INVALID
                    });
                if ((bool)_book.IsSeries)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                    {
                        ErrorMessageConstants.BOOK,
                        MessageConstants.MESSAGE_NOT_BELONGING,
                        ErrorMessageConstants.ODD_BOOK.ToLower()
                    });
                var temps = _book.BookAuthors.Select(ba => ba.AuthorId).ToList();
                temps.ForEach(id => authorIds.Add(id));
            }
            if (authorIds.Any())
            {
                var _bookItemLength = bookItems.Count;
                var duplicatedAuthors = authorIds.GroupBy(a => a).ToList();
                var result = false;
                duplicatedAuthors.ForEach(a =>
                {
                    if (_bookItemLength.Equals(a.Count()))
                        result = true;
                });
                if (!result)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                 {
                    MessageConstants.MESSAGE_NOT_DUPLICATED_INFO,
                    ErrorMessageConstants.BOOK_ITEM_AUTHOR,
                    ErrorMessageConstants.BOOK_SERIES.ToLower()
                 });
            }
            var displayIndexes = bookItems.Select(bi => bi.DisplayIndex).ToList();
            displayIndexes.ForEach(di =>
            {
                if (!di.HasValue)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.PreconditionRequired), new string[]
                {
                    MessageConstants.MESSAGE_REQUIRED,
                    ErrorMessageConstants.BOOK_ITEM_DISPLAY_INDEX,
                    ErrorMessageConstants.BOOK_SERIES.ToLower()
                });
            });
            var IndexDuplicates = displayIndexes.GroupBy(di => di).ToList();
            IndexDuplicates.ForEach(id =>
            {
                if (id.Count() > 1)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                    {
                        MessageConstants.MESSAGE_DUPLICATED_INFO,
                        ErrorMessageConstants.BOOK_ITEM_DISPLAY_INDEX,
                        ErrorMessageConstants.BOOK_SERIES.ToLower()
                    });
            });
        }

        private void CheckPdfAndAudio(Book book)
        {
            if (!string.IsNullOrEmpty(book.PdfTrialUrl) || book.PdfExtraPrice.HasValue)
            {
                if (string.IsNullOrEmpty(book.PdfTrialUrl))
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.PreconditionRequired), new string[]
                    {
                        MessageConstants.MESSAGE_REQUIRED,
                        ErrorMessageConstants.BOOK_PDF_LINK
                    });
                if (!book.PdfExtraPrice.HasValue)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.PreconditionRequired), new string[]
                    {
                        MessageConstants.MESSAGE_REQUIRED,
                        ErrorMessageConstants.BOOK_PDF_EXTRA_PRICE
                    });
            }
            if (!string.IsNullOrEmpty(book.AudioTrialUrl) || book.AudioExtraPrice.HasValue)
            {
                if (string.IsNullOrEmpty(book.AudioTrialUrl))
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.PreconditionRequired), new string[]
                    {
                        MessageConstants.MESSAGE_REQUIRED,
                        ErrorMessageConstants.BOOK_AUDIO_LINK
                    });
                if (!book.AudioExtraPrice.HasValue)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.PreconditionRequired), new string[]
                    {
                        MessageConstants.MESSAGE_REQUIRED,
                        ErrorMessageConstants.BOOK_AUDIO_EXTRA_PRICE
                    });
            }
        }

        private void CheckISBN(Book book, Book OldBook = null, bool isCreate = true)
        {
            if (book != null)
            {
                int length = 10;
                if (!string.IsNullOrEmpty(book.Isbn10))
                {
                    CheckISBNLength(book.Isbn10, length);
                    CheckExistedISBN(book.Isbn10, length, OldBook, isCreate);
                }
                if (!string.IsNullOrEmpty(book.Isbn13))
                {
                    length = 13;
                    CheckISBNLength(book.Isbn13, 13);
                    CheckExistedISBN(book.Isbn13, length, OldBook, isCreate);
                }
            }
        }

        private void CheckISBNLength(string ISBN, int length)
        {
            int number = 0;
            foreach (var item in ISBN)
            {
                if (char.IsDigit(item))
                    number += 1;
            }
            if (length != number)
                BoekErrorMessage.ShowErrorMessage((int)HttpStatusCode.BadRequest, new string[]
                {
                    MessageConstants.MESSAGE_INVALID,
                    length.Equals(10) ? ErrorMessageConstants.BOOK_ISBN10 : ErrorMessageConstants.BOOK_ISBN13
                });
        }
        private void CheckExistedISBN(string ISBN, int length, Book OldBook = null, bool isCreate = true)
        {
            List<Book> books = null;
            if (length.Equals(10))
            {
                if (isCreate)
                    books = _unitOfWork.Books.Get(b => b.Isbn10.Equals(ISBN)).ToList();
                else
                    books = _unitOfWork.Books.Get(b => b.Isbn10.Equals(ISBN) && !b.Id.Equals(OldBook)).ToList();
            }
            else
            {
                if (isCreate)
                    books = _unitOfWork.Books.Get(b => b.Isbn13.Equals(ISBN)).ToList();
                else
                    books = _unitOfWork.Books.Get(b => b.Isbn13.Equals(ISBN) && !b.Id.Equals(OldBook)).ToList();
            }
            if (books.Any())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                    MessageConstants.MESSAGE_DUPLICATED_INFO,
                    length.Equals(10) ? ErrorMessageConstants.BOOK_ISBN10 : ErrorMessageConstants.BOOK_ISBN13
                });
        }

        private void UpdateBookAuthors(List<int?> authorBooks, int? bookId)
        {
            var _list = _unitOfWork.Books.Get(b => b.Id.Equals(bookId))
            .Include(b => b.BookAuthors)
            .SingleOrDefault();
            if (_list != null)
            {
                var _deleteBookAuthors = _list.BookAuthors.ToList();
                foreach (var author in authorBooks)
                {
                    var _author = _list.BookAuthors.SingleOrDefault(ab => ab.AuthorId.Equals(author));

                    //Create author book
                    if (_author == null)
                    {
                        var temp = new BookAuthor()
                        {
                            BookId = bookId,
                            AuthorId = author
                        };
                        _unitOfWork.BookAuthors.Create(temp);
                        _unitOfWork.Save();
                        if (_unitOfWork.BookAuthors.Get(ba => ba.BookId.Equals(temp.BookId) &&
                            ba.AuthorId.Equals(temp.AuthorId)) == null)
                        {
                            BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                            {
                                ErrorMessageConstants.INSERT,
                                ErrorMessageConstants.AUTHOR,
                                MessageConstants.MESSAGE_FAILED
                            });
                        }
                    }
                    else
                    {
                        _deleteBookAuthors.Remove(_author);
                    }
                }

                //Delete unnecessary author book
                if (_deleteBookAuthors.Any())
                {
                    foreach (var author in _deleteBookAuthors)
                    {
                        _unitOfWork.BookAuthors.Delete(author);
                        if (!_unitOfWork.Save())
                            BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                            {
                                ErrorMessageConstants.DELETE,
                                ErrorMessageConstants.AUTHOR,
                                MessageConstants.MESSAGE_FAILED
                            });
                    }
                }
            }
            else
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.BOOK_ID
                });
        }

        /*private void UpdateGenres (List<int?> genres, int? BookId)
        {
            var bookGenres = _unitOfWork.BookGenres.Get(bg => bg.BookId.Equals(BookId));
            var existedGenres = bookGenres.Where(bg => genres.Any(g => g.Equals(bg.GenreId)));
            var deletedGenres = bookGenres.Where(bg => !genres.Any(g => g.Equals(bg.GenreId)));
            var newGenreIds = existedGenres.Any() ? genres.Except(existedGenres.Select(eg => eg.GenreId)) : null;
            var newGenres = new List<BookGenre>();
            int index = bookGenres.Count() - existedGenres.Count();
            foreach(var newGenreId in newGenreIds)
            {
                newGenres.Add(new BookGenre()
                {
                    GenreId = newGenreId,
                    BookId = BookId,
                    DisplayIndex = index,
                });
                index += 1;
            }
            if (newGenres.Any() && _unitOfWork.BookGenres.AddRange(newGenres) == null)
                BoekErrorMessage.ShowErrorMessage(400, new string[]
                {
                    ErrorMessageConstants.INSERT,
                    ErrorMessageConstants.BOOKGENRE,
                    MessageConstants.MESSAGE_FAILED
                });
            if (deletedGenres.Any() && _unitOfWork.BookGenres.RemoveRange(deletedGenres) == null)
                BoekErrorMessage.ShowErrorMessage(400, new string[]
                {
                    ErrorMessageConstants.DELETE,
                    ErrorMessageConstants.BOOKGENRE,
                    MessageConstants.MESSAGE_FAILED
                });
        }*/
        private void UpdateBookItems(List<BookItem> bookItems, int? ParentBookId)
        {
            var bookitems = _unitOfWork.BookItems.Get(f =>
            f.ParentBookId.Equals(ParentBookId)).ToList();

            if (bookitems != null)
            {
                var deletedBookItems = new List<BookItem>();
                var existedBookItems = new List<BookItem>();
                bookitems.ForEach(bi =>
                {
                    if (bookItems.Any(bis => bis.BookId.Equals(bi.BookId)))
                        existedBookItems.Add(bi);
                    else
                        deletedBookItems.Add(bi);
                });
                _unitOfWork.BookItems.RemoveRange(deletedBookItems);
                var result = _unitOfWork.Save();
                if (deletedBookItems.Any() && !result)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                    {
                        ErrorMessageConstants.DELETE,
                        ErrorMessageConstants.BOOK_ITEM.ToLower(),
                        ErrorMessageConstants.BOOK_SERIES.ToLower(),
                        MessageConstants.MESSAGE_FAILED
                    });
                if (existedBookItems.Any())
                {
                    var newBookItems = new List<BookItem>();
                    bookItems.ForEach(bi =>
                    {
                        if (!existedBookItems.Any(ebi => ebi.BookId.Equals(bi.BookId)))
                        {
                            bi.ParentBookId = ParentBookId;
                            newBookItems.Add(bi);
                        }
                    });
                    _unitOfWork.BookItems.AddRange(newBookItems);
                    result = _unitOfWork.Save();
                    if (newBookItems.Any() && !result)
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                        {
                            ErrorMessageConstants.INSERT,
                            ErrorMessageConstants.BOOK_ITEM.ToLower(),
                            ErrorMessageConstants.BOOK_SERIES.ToLower(),
                            MessageConstants.MESSAGE_FAILED
                        });
                }
            }
        }

        private void UpdateOtherInfo(Book newBook, Book oldBook)
        {
            UpdateBasicBookInfo(newBook, oldBook);
            CheckDisabledOrEnabledBook(newBook);
        }

        private void UpdateBasicBookInfo(Book newBook, Book oldBook)
        {
            if (IsChange(newBook, oldBook))
            {
                var bookProducts = _unitOfWork.BookProducts.Get(bps => bps.BookId.Equals(newBook.Id) &&
                bps.Campaign.Status.Equals((byte)CampaignStatus.NotStarted)).ToList();
                if (bookProducts.Any())
                {
                    var Title = GetUpdatedDetail(newBook.Name, oldBook.Name);
                    var Description = GetUpdatedDetail(newBook.Description, oldBook.Description);
                    var ImageUrl = GetUpdatedDetail(newBook.ImageUrl, oldBook.ImageUrl);
                    var CoverPrice = GetUpdatedDetail(newBook.CoverPrice, oldBook.CoverPrice);

                    bookProducts.ForEach(bps =>
                    {
                        bps.Title = Title;
                        bps.Description = Description;
                        bps.ImageUrl = ImageUrl;
                        bps.SalePrice = ServiceUtils.GetSalePrice(CoverPrice, bps.Discount);
                    });

                    _unitOfWork.BookProducts.UpdateRange(bookProducts);
                    if (!_unitOfWork.Save())
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                        {
                            ErrorMessageConstants.UPDATE,
                            ErrorMessageConstants.BOOK_PRODUCT.ToLower(),
                            MessageConstants.MESSAGE_FAILED
                        });
                }
            }
        }

        private bool IsChange(Book newBook, Book oldBook)
        => newBook.Name.Equals(oldBook.Name) ||
        newBook.Description.Equals(oldBook.Description) ||
        newBook.ImageUrl.Equals(oldBook.ImageUrl) ||
        newBook.CoverPrice.Equals(oldBook.CoverPrice);

        private T GetUpdatedDetail<T>(T newInfo, T oldInfo)
        => newInfo.Equals(oldInfo) ? oldInfo : newInfo;

        private void CheckDisabledOrEnabledBook(Book book)
        {
            var isDisabled = book.Status.Equals((byte)BookStatus.Unreleased);
            if ((bool)book.IsSeries)
                CheckDisabledOrEnabledSeriesBook(book, isDisabled);
            else
                CheckDisabledOrEnabledOddBook(book, isDisabled);
        }

        private void CheckDisabledOrEnabledOddBook(Book book, bool isDisabled = true)
        {
            //Series books
            var ParentBookItems = _unitOfWork.BookItems
            .Get(b => b.BookId.Equals(book.Id))
            .Include(b => b.ParentBook).ToList();
            var DatetimeToday = DateTime.Now;
            //Odd book products
            var OddBookProducts = _unitOfWork.BookProducts
            .Get(b => b.BookId.Equals(book.Id))
            .Include(b => b.Campaign).ToList();
            //Parent book products
            var ParentBookProducts = _unitOfWork.BookProductItems
            .Get(b => b.BookId.Equals(book.Id))
            .Include(b => b.ParentBookProduct)
            .ThenInclude(pbp => pbp.Campaign).ToList();

            if (ParentBookItems.Any())
            {
                var result = new List<Book>();
                ParentBookItems.ForEach(pbis =>
                {
                    var isAdded = false;
                    if (isDisabled && pbis.ParentBook.Status.Equals((byte)BookStatus.Releasing))
                    {
                        isAdded = true;
                        pbis.ParentBook.Status = (byte)BookStatus.Unreleased;
                    }
                    if (!isDisabled && pbis.ParentBook.Status.Equals((byte)BookStatus.Unreleased))
                    {
                        isAdded = true;
                        pbis.ParentBook.Status = (byte)BookStatus.Releasing;
                    }
                    if (isAdded)
                        result.Add(pbis.ParentBook);
                });
                if (result.Any())
                {
                    _unitOfWork.Books.UpdateRange(result);
                    if (!_unitOfWork.Save())
                    {
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                        {
                            ErrorMessageConstants.UPDATE,
                            ErrorMessageConstants.BOOK_DISABLE_ENABLE_BOOK_SERIES,
                            isDisabled ? ErrorMessageConstants.BOOK_DISABLED_STATUS :
                            ErrorMessageConstants.BOOK_ENABLED_STATUS,
                            MessageConstants.MESSAGE_FAILED
                        });
                    }
                }
            }
            if (OddBookProducts.Any())
            {
                var result = GetDisabledOrEnabledBookProducts(OddBookProducts, DatetimeToday, isDisabled);
                if (result.Any())
                {
                    _unitOfWork.BookProducts.UpdateRange(result);
                    if (!_unitOfWork.Save())
                    {
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                        {
                            ErrorMessageConstants.UPDATE,
                            ErrorMessageConstants.BOOK_DISABLE_ENABLE_ODD_BOOK_PRODUCT,
                            isDisabled ? ErrorMessageConstants.BOOK_DISABLED_STATUS :
                            ErrorMessageConstants.BOOK_ENABLED_STATUS,
                            MessageConstants.MESSAGE_FAILED
                        });
                    }
                }
            }
            if (ParentBookProducts.Any())
            {
                var _ParentBookProducts = ParentBookProducts.Select(pbps => pbps.ParentBookProduct).ToList();
                var result = GetDisabledOrEnabledBookProducts(_ParentBookProducts, DatetimeToday, isDisabled);
                if (result.Any())
                {
                    _unitOfWork.BookProducts.UpdateRange(result);
                    if (!_unitOfWork.Save())
                    {
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                        {
                            ErrorMessageConstants.UPDATE,
                            ErrorMessageConstants.BOOK_DISABLE_ENABLE_SERIES_COMBO_BOOK_PRODUCT,
                            isDisabled ? ErrorMessageConstants.BOOK_DISABLED_STATUS :
                            ErrorMessageConstants.BOOK_ENABLED_STATUS,
                            MessageConstants.MESSAGE_FAILED
                        });
                    }
                }
            }
        }
        private void CheckDisabledOrEnabledSeriesBook(Book book, bool isDisabled = true)
        {
            var DatetimeToday = DateTime.Now;
            //Series book products
            var SeriesBookProducts = _unitOfWork.BookProducts
            .Get(b => b.BookId.Equals(book.Id))
            .Include(b => b.Campaign).ToList();
            if (SeriesBookProducts.Any())
            {
                var result = GetDisabledOrEnabledBookProducts(SeriesBookProducts, DatetimeToday, isDisabled);
                if (result.Any())
                {
                    _unitOfWork.BookProducts.UpdateRange(result);
                    if (!_unitOfWork.Save())
                    {
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                        {
                            ErrorMessageConstants.UPDATE,
                            ErrorMessageConstants.BOOK_DISABLE_ENABLE_SERIES_BOOK_PRODUCT,
                            isDisabled ? ErrorMessageConstants.BOOK_DISABLED_STATUS :
                            ErrorMessageConstants.BOOK_ENABLED_STATUS,
                            MessageConstants.MESSAGE_FAILED
                        });
                    }
                }
            }
        }

        private List<BookProduct> GetDisabledOrEnabledBookProducts(List<BookProduct> list, DateTime DatetimeToday, bool isDisabled = true)
        {
            var result = new List<BookProduct>();
            if (list.Any())
            {
                list.ForEach(item =>
                {
                    var campaign = item.Campaign;
                    if (!ServiceUtils.CheckCampaignEndDateAndStatus(ref campaign, DatetimeToday))
                    {
                        var isAdded = false;
                        //Check if book is disabled and book product is sale
                        if (isDisabled && item.Status.Equals((byte)BookProductStatus.Sale))
                        {
                            isAdded = true;
                            item.Status = (byte)BookProductStatus.Unreleased;
                        }
                        //Check if book is enabled and book product is unreleased
                        if (!isDisabled && item.Status.Equals((byte)BookProductStatus.Unreleased))
                        {
                            isAdded = true;
                            item.Status = (byte)BookProductStatus.Sale;
                        }
                        if (isAdded)
                            result.Add(item);
                    }
                });
            }
            return result;
        }

        private void CheckIssuer(Guid? IssuerId)
        {
            Guid? issuerId = ServiceUtils.GetUserInfo(_httpContextAccessor);
            if (IssuerId != issuerId)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.BadRequest), new string[]
                {
                    ErrorMessageConstants.BOOK,
                    MessageConstants.MESSAGE_NOT_BELONGING,
                    ErrorMessageConstants.ISSUER
                });
        }
        private Book CheckDuplicatedBookName(string name) =>
            _unitOfWork.Books.CheckDuplicatedBookName(name);
        #endregion
    }
}
