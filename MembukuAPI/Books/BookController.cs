using MembukuAPI.Books.BookDtos;
using MembukuAPI.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MembukuAPI.Books;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase {
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService) {
        _bookService = bookService;
    }

    [HttpGet]
    public ActionResult<PageResponse<BookDto>> GetBooks(
        string? name,
        string? authorName,
        int pageNumber = 1,
        int pageSize = 10
        ) {
        if (pageNumber <= 0 || pageSize <= 0) {
            return BadRequest();
        }
        var books = _bookService.GetAllBooks(pageNumber, pageSize, name, authorName);
        return Ok(books);
    }

    [HttpGet("{id}")]
    public ActionResult<BookDto> GetBook(int id) {
        var book = _bookService.GetBookById(id);
        if (book == null) {
            return NotFound();
        }
        return Ok(book);
    }

    [HttpGet("search")]
    public ActionResult<IEnumerable<BookSearchDto>> GetSearchedBooks(string q, SearchType searchType = SearchType.All) {
        var searchedBooks = _bookService.GetAllBooksSearch(q, searchType);

        return Ok(searchedBooks);
    }


    [HttpGet("{id}/cover")]
    public ActionResult<string> GetCover(int id) {
        var book = _bookService.GetBookById(id);
        if (book == null || string.IsNullOrEmpty(book.Cover)) {
            return NotFound();
        }

        var image = _bookService.GetCover(book.Cover);

        if (image.ImageStream == null || image.ContentType == null) {
            return NotFound();
        }

        return File(image.ImageStream, image.ContentType);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public ActionResult<BookDto> CreateBook([FromForm] CreateBookDto dto) {
        var book = _bookService.CreateBook(dto);
        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public ActionResult<BookDto> UpdateBook(int id, [FromForm] UpdateBookDto dto) {
        if (id != dto.Id) {
            return BadRequest();
        }

        var updatedBook = _bookService.UpdateBook(dto);
        return Ok(updatedBook);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public ActionResult DeleteBook(int id) {
        var deleted = _bookService.DeleteBook(id);
        if (!deleted) {
            return NotFound();
        }
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}/cover")]
    public ActionResult<BookDto> UpdateCover(int id, IFormFile file) {
        if (file == null || file.Length == 0) {
            return BadRequest("Invalid file.");
        }

        var book = _bookService.GetBookById(id);
        if (book == null) {
            return NotFound();
        }

        return Ok(_bookService.UpdateBookCover(id, file));
    }
}