public interface IBookService
{
    IEnumerable<Book> FindAll();
    Book FindById(Guid id);
    ResponseBookResult Save(Book newBook);
    ResponseBookResult Update(Book book);
    ResponseBookResult Remove(Guid id);
}

