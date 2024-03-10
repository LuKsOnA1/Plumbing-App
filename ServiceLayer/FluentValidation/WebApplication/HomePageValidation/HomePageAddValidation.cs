﻿using EntityLayer.WebApplication.ViewModels.HomePageVM;
using FluentValidation;
using ServiceLayer.Messages.WebApplication;

namespace ServiceLayer.FluentValidation.WebApplication.HomePageValidation
{
    public class HomePageAddValidation : AbstractValidator<HomePageAddVM>
    {
        public HomePageAddValidation()
        {
            RuleFor(x => x.Header)
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Header"))
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Header"))
                .MaximumLength(200).WithMessage(ValidationMessages.MaximumCharacterAllowence("Header", 200));

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Description"))
				.NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Description"))
				.MaximumLength(2000).WithMessage(ValidationMessages.MaximumCharacterAllowence("Description", 2000));

            RuleFor(x => x.VideoLink)
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("VideoLink"))
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("VideoLink"));


		}
    }
}
