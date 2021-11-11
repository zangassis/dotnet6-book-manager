
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDBContext>();
builder.Services.AddTransient<IBookService, BookService>();

var app = builder.Build();

var scope = app.Services.CreateScope();
var service = scope.ServiceProvider.GetService<IBookService>();


app.MapGet("v1/books", () =>
{
    var books = service.FindAll().ToList();
    return Results.Ok(books);
});

app.MapGet("v1/books/book", (Guid id) =>
{
    var book = service.FindById(id);
    return Results.Ok(book);
});

app.MapPost("v1/books", (Book book) =>
{
    var newBook = service.Save(book);

    if (string.IsNullOrEmpty(newBook.errorMessage) == false)
        return Results.BadRequest(newBook.errorMessage);

    return Results.Created("Get", new { id = book.Id });
});

app.MapPut("v1/books", (Book book) =>
{
    var updateBook = service.Update(book);

    if (string.IsNullOrEmpty(updateBook.errorMessage) == false)
        return Results.BadRequest(updateBook.errorMessage);

    return Results.Ok();
});

app.MapDelete("v1/books", (Guid id) =>
{
    var updateBook = service.Remove(id);

    if (string.IsNullOrEmpty(updateBook.errorMessage) == false)
        return Results.BadRequest(updateBook.errorMessage);

    return Results.NoContent();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
