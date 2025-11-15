using FluentValidation;

namespace NLayerArchitecture.Services.Products.Update;

public class UpdateProductRequestValidator: AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(p=>p.Id)
            .NotEmpty()
            .WithMessage("Id zorunludur");
        RuleFor(p=>p.Name)
            .NotEmpty()
            .WithMessage("Ürün ismi zorunludur")
            .Length(2,100)
            .WithMessage("Ürün ismi 2 ile 100 karakter arasında olmalıdır");
        RuleFor(p=>p.Price)
            .GreaterThan(0)
            .WithMessage("Ürün fiyatı 0 dan büyük olmalıdır");
        RuleFor(p=>p.Stock)
            .InclusiveBetween(1,100)
            .WithMessage("Ürün adedi 1 ile 100 arasında olmalıdır");
        RuleFor(p=>p.CategoryId).NotEmpty().WithMessage("Kategori Id zorunludur");
    }
}