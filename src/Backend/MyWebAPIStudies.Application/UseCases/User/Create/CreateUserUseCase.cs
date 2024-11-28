using MyWebAPIStudies.Application.AutoMappers;
using MyWebAPIStudies.Communication.Requests;
using MyWebAPIStudies.Communication.Responses;
using MyWebAPIStudies.Exceptions.ExceptionsBase;

namespace MyWebAPIStudies.Application.UseCases.User.Create
{
	public class CreateUserUseCase
	{

		public ResponseCreateUserJson Execute(RequestCreateUserJson newUser)
		{
			Validate(newUser);

			var autoMapper = new AutoMapper.MapperConfiguration(
				opt => opt.AddProfile(new AutoMapping())
				).CreateMapper();

			var user = autoMapper.Map<Domain.Entities.User>(newUser);

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
