using FluentValidation;
using MyWebAPIStudies.Communication.Requests;
using MyWebAPIStudies.Exceptions;

namespace MyWebAPIStudies.Application.UseCases.User.Create
{
	public class CreateUserValidator : AbstractValidator<RequestCreateUserJson>
	{
		public CreateUserValidator()
		{
			RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY);
			RuleFor(user => user.Email).NotEmpty().WithMessage(ResourceMessagesException.EMAIL_EMPTY);
			RuleFor(user => user.Password.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMessagesException.PASSWORD_LENGTH);
			When(user => !string.IsNullOrEmpty(user.Email), () =>
			{
				RuleFor(user => user.Email).EmailAddress().WithMessage(ResourceMessagesException.FORMAT_EMAIL_INCORRECT);
			});
		}
	}
}
