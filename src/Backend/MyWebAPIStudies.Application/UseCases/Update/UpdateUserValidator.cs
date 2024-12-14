using FluentValidation;
using MyWebAPIStudies.Communication.Requests;
using MyWebAPIStudies.Exceptions;

namespace MyWebAPIStudies.Application.UseCases.Update
{
	internal class UpdateUserValidator : AbstractValidator<RequestUpdateUserJson>
	{
		public UpdateUserValidator()
		{
			RuleFor(request => request.Name).NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY);
			RuleFor(request => request.Email).NotEmpty().WithMessage(ResourceMessagesException.EMAIL_EMPTY);

			When(request => !string.IsNullOrWhiteSpace(request.Email), () =>
			{
				RuleFor(request=> request.Email).EmailAddress().WithMessage(ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID)
			});
		}
	}
}
