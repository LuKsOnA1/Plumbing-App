using EntityLayer.WebApplication.ViewModels.TestimonalVM;
using FluentValidation;
using ServiceLayer.Messages.WebApplication;

namespace ServiceLayer.FluentValidation.WebApplication.TestimonalValidation
{
    public class TestimonalUpdateValidation : AbstractValidator<TestimonalUpdateVM>
    {
        public TestimonalUpdateValidation()
        {
			RuleFor(x => x.FullName)
				.NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Full Name"))
				.NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Full Name"))
				.MaximumLength(100).WithMessage(ValidationMessages.MaximumCharacterAllowence("Full Name", 100));

			RuleFor(x => x.Title)
				.NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Title"))
				.NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Title"))
				.MaximumLength(100).WithMessage(ValidationMessages.MaximumCharacterAllowence("Title", 100));

			RuleFor(x => x.Comment)
				.NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Comment"))
				.NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Comment"))
				.MaximumLength(2000).WithMessage(ValidationMessages.MaximumCharacterAllowence("Comment", 2000));

		}
    }
}
