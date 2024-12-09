using AutoMapper;
using MembukuAPI.Authors.Dtos;

namespace MembukuAPI.Authors;

public class AuthorService : IAuthorService {
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;

    public AuthorService(IAuthorRepository authorRepository, IMapper mapper) {
        _authorRepository = authorRepository;
        _mapper = mapper;
    }

    public IEnumerable<AuthorDto> GetAllAuthors() {
        var authors = _authorRepository.GetAll();
        return _mapper.Map<IEnumerable<AuthorDto>>(authors);
    }

    public AuthorDto GetAuthorById(int id) {
        var author = _authorRepository.GetById(id);
        return _mapper.Map<AuthorDto>(author);
    }

    public AuthorDto CreateAuthor(CreateAuthorDto dto) {
        var author = _mapper.Map<Author>(dto);
        var createdAuthor = _authorRepository.Add(author);
        return _mapper.Map<AuthorDto>(createdAuthor);
    }

    public AuthorDto UpdateAuthor(UpdateAuthorDto dto) {
        var author = _mapper.Map<Author>(dto);
        var updatedAuthor = _authorRepository.Update(author);
        return _mapper.Map<AuthorDto>(updatedAuthor);
    }

    public bool DeleteAuthor(int id) {
        return _authorRepository.Delete(id);
    }
}