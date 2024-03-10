using EntityLayer.WebApplication.ViewModels.TeamVM;
using FluentValidation;
using ServiceLayer.Messages.WebApplication;

namespace ServiceLayer.FluentValidation.WebApplication.TeamValidation
{
    public class TeamAddValidation : AbstractValidator<TeamAddVM>
    {
        public TeamAddValidation()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Full Name"))
				.NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Full Name"))
				.MaximumLength(100).WithMessage(ValidationMessages.MaximumCharacterAllowence("Full Name", 100));

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Title"))
				.NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Title"))
				.MaximumLength(100).WithMessage(ValidationMessages.MaximumCharacterAllowence("Title", 100));

            RuleFor(x => x.Photo)
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Photo"))
				.NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Photo"));
        }
    }
}
