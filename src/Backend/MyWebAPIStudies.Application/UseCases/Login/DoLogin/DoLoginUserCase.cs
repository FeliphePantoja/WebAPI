using MyWebAPIStudies.Application.Cryptography;
using MyWebAPIStudies.Communication.Requests;
using MyWebAPIStudies.Communication.Responses;
using MyWebAPIStudies.Domain.Repositories.User;
using MyWebAPIStudies.Exceptions.ExceptionsBase;

namespace MyWebAPIStudies.Application.UseCases.Login.DoLogin
{
	public class DoLoginUserCase : IDoLoginUserCase
	{
		private readonly IUserReadOnlyRepository _repository;
		private readonly PasswordEncripter _passwordEncripter;

		public DoLoginUserCase(IUserReadOnlyRepository repository,
			PasswordEncripter passwordEncripter)
		{
			_repository = repository;
			_passwordEncripter = passwordEncripter;
		}

		public async Task<ResponseCreateUserJson> Execute(RequestLoginJson requestLoginJson)
		{
			var encriptedPassword = _passwordEncripter.Encrypt(requestLoginJson.Password);

			var user = await _repository
				.GetUserByEmailAndPassword(requestLoginJson.Email, encriptedPassword)
				?? throw new InvalidLoginException();

			return new ResponseCreateUserJson
			{
				Name = user.Name
			};
		}
	}
}
