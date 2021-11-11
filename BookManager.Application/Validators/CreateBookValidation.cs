using FluentValidation;

public class CreateBookValidation : AbstractValidator<Book>
{
    public CreateBookValidation()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x)
            .Must(b => b is not null)
            .WithErrorCode(BookErrors.Error100);
            
        RuleFor(x => x)
           .Must(b => !string.IsNullOrEmpty(b.Title))
           .WithErrorCode(BookErrors.Error200);

        RuleFor(x => x)
           .Must(b => !string.IsNullOrEmpty(b.Author))
           .WithErrorCode(BookErrors.Error300);

        RuleFor(x => x)
            .Must(b => !string.IsNullOrEmpty(b.Description))
            .WithErrorCode(BookErrors.Error400);
    }
}