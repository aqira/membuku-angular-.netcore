using MembukuAPI.Authors.Dtos;

namespace MembukuAPI.Authors;

public interface IAuthorService {
    IEnumerable<AuthorDto> GetAllAuthors();
    AuthorDto GetAuthorById(int id);
    AuthorDto CreateAuthor(CreateAuthorDto dto);
    AuthorDto UpdateAuthor(UpdateAuthorDto dto);
    bool DeleteAuthor(int id);
}