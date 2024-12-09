namespace MembukuAPI.Authors;

public interface IAuthorRepository {
    IEnumerable<Author> GetAll();
    Author GetById(int id);
    Author Add(Author author);
    Author Update(Author author);
    bool Delete(int id);
}

