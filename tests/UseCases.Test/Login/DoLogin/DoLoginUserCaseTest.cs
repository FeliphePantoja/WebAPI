using CommonTestUtil.Cryptography;
using CommonTestUtil.Entities;
using CommonTestUtil.Repositories;
using CommonTestUtil.Requests;
using FluentAssertions;
using MyWebAPIStudies.Application.UseCases.Login.DoLogin;
using MyWebAPIStudies.Communication.Requests;
using MyWebAPIStudies.Exceptions;
using MyWebAPIStudies.Exceptions.ExceptionsBase;

namespace UseCases.Test.Login.DoLogin
{
	public class DoLoginUserCaseTest
	{
		[Fact]
		public async Task Should_return_login_success()
		{
			(var user, var password) = UserBuilder.Build();
			var useCase = CreateUseCase(user);

			var result = await useCase.Execute(new RequestLoginJson
			{
				Email = user.Email,
				Password = password,
			});

			result.Should().NotBeNull();
			result.Name.Should().NotBeNullOrWhiteSpace().And.Be(user.Name);
		}

		[Fact]
		public async Task Should_return_login_invalid()
		{
			var request = RequestLoginJsonBuilder.Build();
			var usecase = CreateUseCase();

			Func<Task> act = async () => { await usecase.Execute(request); };

			await act.Should().ThrowAsync<InvalidLoginException>()
				.Where(e => e.Message.Equals(ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID));
		}

		private static DoLoginUserCase CreateUseCase(MyWebAPIStudies.Domain.Entities.User? user = null)
		{
			var passwordEncripter = PasswordEncripterBuilder.Build();
			var userReadOnlyRespository = new UserReadOnlyRepositoryBuilder();

			if (user is not null)
			{
				userReadOnlyRespository.GetUserByEmailAndPassword(user);
			}

			return new DoLoginUserCase(userReadOnlyRespository.Build(), passwordEncripter);
		}
	}
}
