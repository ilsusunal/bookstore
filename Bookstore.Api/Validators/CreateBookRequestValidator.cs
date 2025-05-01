using Bookstore.Api.Models.Books;
using FluentValidation;

namespace Bookstore.Api.Validators;

public class CreateBookRequestValidator : AbstractValidator<CreateBookRequest>
{
    public CreateBookRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.ISBN)
            .NotEmpty()
            .Length(10, 13);

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.PageCount)
            .GreaterThan(0);

        RuleFor(x => x.PublishedDate)
            .LessThanOrEqualTo(DateTime.Today);

        RuleFor(x => x.AuthorId)
            .GreaterThan(0);

        RuleFor(x => x.GenreId)
            .GreaterThan(0);
    }
}