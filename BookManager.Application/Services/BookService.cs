using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

public class BookService : IBookService
{
    private readonly AppDBContext _context;

    public BookService(AppDBContext context)
    {
        _context = context;
    }

    public IEnumerable<Book> FindAll() => _context.Books.ToList();

    public Book FindById(Guid id) =>
        _context.Books.Where(x => x.Id == id).SingleOrDefault();

    public ResponseBookResult Remove(Guid id)
    {
        var existing = _context.Books.SingleOrDefault(x => x.Id == id);

        if (existing is null)
            return new ResponseBookResult(Guid.Empty, $"error: {BookErrors.Error500} Id: {id.ToString().ToUpper()}");

        _context.Books.Remove(existing);
        _context.SaveChanges();

        return new ResponseBookResult(id, string.Empty);
    }

    public ResponseBookResult Save(Book newBook)
    {
        var validation = Validate(newBook);

        if (validation.IsValid == false)
            return new ResponseBookResult(Guid.Empty, $"error: {validation.Errors[0].ErrorCode}");

        var id = Guid.NewGuid();

        newBook.Id = id;

        _context.Books.Add(newBook);
        _context.SaveChanges();

        return new ResponseBookResult(id, string.Empty);
    }

    public ResponseBookResult Update(Book book)
    {
        var isValid = _context.Books.Where(x => x.Id == book.Id);

        if (isValid.Any() == false)
            return new ResponseBookResult(Guid.Empty, $"error: {BookErrors.Error500} Id: {book.Id.ToString().ToUpper()}");

        _context.Entry(book).State = EntityState.Modified;
        _context.SaveChanges();

        return new ResponseBookResult(book.Id, string.Empty);
    }

    public ValidationResult Validate(Book book) =>
        new CreateBookValidation().Validate(book);
}