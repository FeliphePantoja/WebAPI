using MyWebAPIStudies.Application.AutoMappers;
using MyWebAPIStudies.Application.Cryptography;
using MyWebAPIStudies.Communication.Requests;
using MyWebAPIStudies.Communication.Responses;
using MyWebAPIStudies.Exceptions.ExceptionsBase;

namespace MyWebAPIStudies.Application.UseCases.User.Create
{
	public class CreateUserUseCase
	{

		public ResponseCreateUserJson Execute(RequestCreateUserJson newUser)
		{
			var createPasswordCrypt = new PasswordEncripter();
			var autoMapper = new AutoMapper.MapperConfiguration(
				opt => opt.AddProfile(new AutoMapping())
				).CreateMapper();

			Validate(newUser);

			var user = autoMapper.Map<Domain.Entities.User>(newUser);
			user.Password = createPasswordCrypt.Encrypt(newUser.Password);
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
