using FluentValidation;
using MyWebAPIStudies.Communication.Requests;

namespace MyWebAPIStudies.Application.UseCases.User.Create
{
	internal class CreateUserValidator : AbstractValidator<RequestCreateUserJson>
	{
		public CreateUserValidator()
		{
			RuleFor(user => user.Name).NotEmpty();
			RuleFor(user => user.Email).NotEmpty();
			RuleFor(user => user.Email).EmailAddress();
			RuleFor(user => user.Password.Length).GreaterThanOrEqualTo(6);
		}
	}
}
