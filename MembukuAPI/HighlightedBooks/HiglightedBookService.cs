using AutoMapper;
using MembukuAPI.HighlightedBooks.HighlightedBookDtos;

namespace MembukuAPI.HighlightedBooks;

public class HighlightedBookService : IHighlightedBookService {
    private readonly IHighlightedBookRepository _highlightedBookRepository;
    private readonly IMapper _mapper;

    public HighlightedBookService(IHighlightedBookRepository highlightedBookRepository, IMapper mapper) {
        _highlightedBookRepository = highlightedBookRepository;
        _mapper = mapper;
    }

    public IEnumerable<HighlightedBookDto> GetAllHighlightedBooks() {
        var highlightedBooksDto = _highlightedBookRepository.GetAll().Select(hb => new HighlightedBookDto {
            BookId = hb.BookId,
            Name = hb.Book.Name,
            AuthorName = hb.Book.Author.Name,
            Cover = hb.Book.Cover,
            ReleaseDate = hb.Book.ReleaseDate,
            OrderNumber = hb.OrderNumber
        });
        return highlightedBooksDto;
    }

    public HighlightedBookDto GetHighlightedBookById(int bookId) {
        var highlightedBook = _highlightedBookRepository.GetById(bookId);
        return _mapper.Map<HighlightedBookDto>(highlightedBook);
    }

    public HighlightedBookDto CreateHighlightedBook(CreateHighlightedBookDto dto) {
        var highlightedBook = _mapper.Map<HighlightedBook>(dto);
        var createdHighlightedBook = _highlightedBookRepository.Add(highlightedBook);
        return _mapper.Map<HighlightedBookDto>(createdHighlightedBook);
    }

    public HighlightedBookDto UpdateHighlightedBook(UpdateHighlightedBookDto dto) {
        var highlightedBook = _mapper.Map<HighlightedBook>(dto);
        var updatedHighlightedBook = _highlightedBookRepository.Update(highlightedBook);
        return _mapper.Map<HighlightedBookDto>(updatedHighlightedBook);
    }

    public bool DeleteHighlightedBook(int bookId) {
        return _highlightedBookRepository.Delete(bookId);
    }
}