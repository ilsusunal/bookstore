using Bookstore.Api.Models.Books;
using FluentValidation;

namespace Bookstore.Api.Validators;

public class UpdateBookRequestValidator : AbstractValidator<UpdateBookRequest>
{
    public UpdateBookRequestValidator()
    {
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