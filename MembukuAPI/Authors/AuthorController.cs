using MembukuAPI.Authors.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MembukuAPI.Authors;
[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase {
    private readonly IAuthorService _authorService;

    public AuthorsController(IAuthorService authorService) {
        _authorService = authorService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<AuthorDto>> GetAuthors() {
        var authors = _authorService.GetAllAuthors();
        return Ok(authors);
    }

    [HttpGet("{id}")]
    public ActionResult<AuthorDto> GetAuthor(int id) {
        var author = _authorService.GetAuthorById(id);
        if (author == null) {
            return NotFound();
        }
        return Ok(author);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public ActionResult<AuthorDto> CreateAuthor([FromBody] CreateAuthorDto dto) {
        var author = _authorService.CreateAuthor(dto);
        return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public ActionResult<AuthorDto> UpdateAuthor(int id, [FromBody] UpdateAuthorDto dto) {
        if (id != dto.Id) {
            return BadRequest();
        }

        var updatedAuthor = _authorService.UpdateAuthor(dto);
        return Ok(updatedAuthor);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public ActionResult DeleteAuthor(int id) {
        var deleted = _authorService.DeleteAuthor(id);
        if (!deleted) {
            return NotFound();
        }
        return NoContent();
    }
}