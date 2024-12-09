using MembukuAPI.HighlightedBooks.HighlightedBookDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MembukuAPI.HighlightedBooks;

[Route("api/[controller]")]
[ApiController]
public class HighlightedBooksController : ControllerBase {
    private readonly IHighlightedBookService _highlightedBookService;

    public HighlightedBooksController(IHighlightedBookService highlightedBookService) {
        _highlightedBookService = highlightedBookService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<HighlightedBookDto>> GetHighlightedBooks() {
        var highlightedBooks = _highlightedBookService.GetAllHighlightedBooks();
        return Ok(highlightedBooks);
    }

    [HttpGet("{bookId}")]
    public ActionResult<HighlightedBookDto> GetHighlightedBook(int bookId) {
        var highlightedBook = _highlightedBookService.GetHighlightedBookById(bookId);
        if (highlightedBook == null) {
            return NotFound();
        }
        return Ok(highlightedBook);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public ActionResult<HighlightedBookDto> CreateHighlightedBook([FromBody] CreateHighlightedBookDto dto) {
        var highlightedBook = _highlightedBookService.CreateHighlightedBook(dto);
        return CreatedAtAction(nameof(GetHighlightedBook), new { bookId = highlightedBook.BookId }, highlightedBook);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{bookId}")]
    public ActionResult<HighlightedBookDto> UpdateHighlightedBook(int bookId, [FromBody] UpdateHighlightedBookDto dto) {
        if (bookId != dto.BookId) {
            return BadRequest();
        }

        var updatedHighlightedBook = _highlightedBookService.UpdateHighlightedBook(dto);
        return Ok(updatedHighlightedBook);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{bookId}")]
    public ActionResult DeleteHighlightedBook(int bookId) {
        var deleted = _highlightedBookService.DeleteHighlightedBook(bookId);
        if (!deleted) {
            return NotFound();
        }
        return NoContent();
    }
}