using FluentValidation;

namespace NLayerArchitecture.Services.Categories.Create;

public class CreateCategoryRequestValidator:AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Kategori adı boş olamaz")
            .MaximumLength(50).WithMessage("Kategori Adı en fazla 50 karakter olabilir");
    }
}