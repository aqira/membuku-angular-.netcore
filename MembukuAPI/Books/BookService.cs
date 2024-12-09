using AutoMapper;
using MembukuAPI.Books.BookDtos;
using MembukuAPI.Images;
using MembukuAPI.Images.Dtos;
using MembukuAPI.Reviews;
using MembukuAPI.Shared.Dtos;

namespace MembukuAPI.Books;

public class BookService : IBookService {
    private readonly IBookRepository _bookRepository;
    private readonly IReviewRepository _reviewRepository;
    private readonly IImageService _imageService;
    private readonly IMapper _mapper;
    private readonly string _imagePartialPath = "Assets/Books";

    public BookService(IBookRepository bookRepository, IMapper mapper, IImageService imageService, IReviewRepository reviewRepository) {
        _bookRepository = bookRepository;
        _mapper = mapper;
        _imageService = imageService;
        _reviewRepository = reviewRepository;
    }

    public IEnumerable<BookDto> GetAllBooks() {
        var books = _bookRepository.GetAll();
        return _mapper.Map<IEnumerable<BookDto>>(books);
    }

    public PageResponse<BookDto> GetAllBooks(int pageNumber, int pageSize, string? name, string? authorName) {
        var books = _bookRepository.GetAll(pageNumber, pageSize, name, authorName);
        var booksCount = _bookRepository.Count(name, authorName);

        return new PageResponse<BookDto> {
            Data = books.Select(b => _mapper.Map<BookDto>(b)),
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalRecords = booksCount
        };
    }

    public BookDto GetBookById(int id) {
        var book = _bookRepository.GetById(id);
        return _mapper.Map<BookDto>(book);
    }

    public BookDto CreateBook(CreateBookDto dto) {
        var book = _mapper.Map<Book>(dto);
        book.Cover = _imageService.CreateImage(_imagePartialPath, dto.Cover).FileName;
        var createdBook = _bookRepository.Add(book);
        return _mapper.Map<BookDto>(createdBook);
    }

    public BookDto UpdateBook(UpdateBookDto dto) {
        var book = _bookRepository.GetById(dto.Id);
        if (book == null) {
            return null;
        }

        var oldCover = book.Cover;
        if (dto.Cover != null) {
            //delete old picture (if existed)
            if (!string.IsNullOrEmpty(oldCover)) {
                _imageService.DeleteImage(_imagePartialPath, oldCover);
            }
            //add new picture
            var image = _imageService.CreateImage(_imagePartialPath, dto.Cover);
            //update the new cover
            book.Cover = image.FileName;
        }
        book.Name = dto.Name;
        book.ReleaseDate = dto.ReleaseDate;
        book.AuthorId = dto.AuthorId;

        var updatedBook = _bookRepository.Update(book);
        return _mapper.Map<BookDto>(updatedBook);
    }

    public bool DeleteBook(int id) {
        var book = _bookRepository.GetById(id);

        if (book == null) {
            return false;
        }

        if (!string.IsNullOrEmpty(book.Cover)) {
            _imageService.DeleteImage(_imagePartialPath, book.Cover);
        }

        return _bookRepository.Delete(id);
    }

    public ImageDto GetCover(string cover) {
        return _imageService.GetImageStream(_imagePartialPath, cover);
    }

    public BookDto UpdateBookCover(int id, IFormFile file) {
        var book = _bookRepository.GetById(id);
        if (book == null || file == null || file.Length == 0) {
            return null;
        }
        var oldCover = book.Cover;

        var image = _imageService.CreateImage(_imagePartialPath, file);
        book.Cover = image.FileName;

        if (!string.IsNullOrEmpty(oldCover)) {
            _imageService.DeleteImage(_imagePartialPath, oldCover);
        }
        var updatedBook = _bookRepository.Update(book);

        return _mapper.Map<BookDto>(updatedBook);
    }

    public IEnumerable<BookSearchDto> GetAllBooksSearch(string q, SearchType searchType) {
        var books = _bookRepository.GetAll(q, searchType);
        return books.Select((b) => new BookSearchDto {
            Id = b.Id,
            Name = b.Name,
            ReleaseDate = b.ReleaseDate,
            AvgRating = _reviewRepository.AverageRatingByBookId(b.Id),
            RatingsCount = _reviewRepository.CountRatingByBookId(b.Id),
            Author = new BookAuthorDto {
                Id = b.Author!.Id,
                Name = b.Author!.Name
            }
        });
    }
}