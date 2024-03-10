using EntityLayer.WebApplication.ViewModels.CategoryVM;
using FluentValidation;
using ServiceLayer.Messages.WebApplication;

namespace ServiceLayer.FluentValidation.WebApplication.CategoryValidation
{
    public class CategoryAddValidation : AbstractValidator<CategoryAddVM>
    {
        public CategoryAddValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Name"))
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Name"))
                .MaximumLength(50).WithMessage(ValidationMessages.MaximumCharacterAllowence("Name", 50));
        }
    }
}
