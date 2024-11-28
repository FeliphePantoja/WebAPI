using MyWebAPIStudies.Communication.Requests;
using MyWebAPIStudies.Communication.Responses;
using MyWebAPIStudies.Exceptions.ExceptionsBase;
using System.Net.Http.Headers;

namespace MyWebAPIStudies.Application.UseCases.User.Create
{
	public class CreateUserUseCase
	{

		public ResponseCreateUserJson Execute(RequestCreateUserJson newUser)
		{
			Validate(newUser);

			return new ResponseCreateUserJson
			{
				Name = newUser.Name,
			};
		}

		private void Validate(RequestCreateUserJson newUser)
		{
			var result = new CreateUserValidator().Validate(newUser);
			if (!result.IsValid)
			{
				var errorMSGs = result.Errors.Select(x => x.ErrorMessage).ToList();

				throw new ErrorOnValidationException(errorMSGs);
			}
		}
	}
}
