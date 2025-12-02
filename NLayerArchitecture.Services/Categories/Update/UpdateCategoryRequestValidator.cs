using FluentValidation;

namespace NLayerArchitecture.Services.Categories.Update;

public class UpdateCategoryRequestValidator: AbstractValidator<UpdateCategoryRequest>
{
    public UpdateCategoryRequestValidator()
    {
        RuleFor(c=>c.Name).NotEmpty().WithMessage("Kategori Adı zorunludur")
            .MaximumLength(50).WithMessage("Kategori Adı en fazla 50 karakter olabilir");
    }
}