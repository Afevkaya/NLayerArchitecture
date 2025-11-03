using FluentValidation;

namespace NLayerArchitecture.Services.Products.Create;

public class CreateProductRequestValidator: AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ürün ismi zorunludur")
            .Length(2, 100).WithMessage("Ürün ismi 2 ile 100 karakter arasında olmalıdır");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Ürün fiyatı 0 dan büyük olmalıdır");

        RuleFor(x => x.Stock)
            .InclusiveBetween(1, 100).WithMessage("Ürün adedi 1 ile 100 arasında olmalıdır");
    }
}