using Bogus;
using MyWebAPIStudies.Communication.Requests;

namespace CommonTestUtil.Requests
{
	public class CreateUserValidatorBuilder
	{
		public static RequestCreateUserJson Build(int passwordSize = 10)
		{
			return new Faker<RequestCreateUserJson>()
				.RuleFor(user => user.Name, (f) => f.Person.FirstName)
				.RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Name))
				.RuleFor(user => user.Password, (f) => f.Internet.Password(passwordSize));
		}
	}
}
