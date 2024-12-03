using CommonTestUtil.Cryptography;
using CommonTestUtil.Mapper;
using CommonTestUtil.Repositories;
using CommonTestUtil.Requests;
using FluentAssertions;
using MyWebAPIStudies.Application.UseCases.User.Create;
using MyWebAPIStudies.Exceptions;
using MyWebAPIStudies.Exceptions.ExceptionsBase;

namespace UseCases.Test.CreateUser
{
	public class CreateUserCaseTest
	{
		private CreateUserUseCase UsersCases(string? validEmail = null)
		{
			var mapper = MapperBuilder.Build();
			var password = PasswordEncripterBuilder.Build();
			var writeRepository = UserWriteOnlyRepositoryBuilder.Build();
			var readRepository = new UserReadOnlyRepositoryBuilder();
			var unitOfWork = UnitOfWorkBuilder.Build();

			if (!string.IsNullOrWhiteSpace(validEmail))
				readRepository.ExistActiveUserWithEmail(validEmail);

			return new CreateUserUseCase(
				writeRepository,
				readRepository.Build(),
				unitOfWork,
				password,
				mapper);
		}

		[Fact]
		public async Task Should_return_success_create_user()
		{
			var request = CreateUserValidatorBuilder.Build();
			var userCase = UsersCases();

			var result = await userCase.Execute(request);

			result.Should().NotBeNull();
			result.Name.Should().Be(request.Name);
		}

		[Fact]
		public async Task Should_return_fail_Email_create_user()
		{
			var request = CreateUserValidatorBuilder.Build();
			var userCase = UsersCases(request.Email);

			Func<Task> act = async () => await userCase.Execute(request);

			(await act.Should().ThrowAsync<ErrorOnValidationException>())
				.Where(
				ex => ex.ErrorMessages.Count == 1 &&
				ex.ErrorMessages.Contains(ResourceMessagesException.EMAIL_EXISTE));
		}

		[Fact]
		public async Task Should_return_fail_Name_create_user()
		{
			var request = CreateUserValidatorBuilder.Build();
			request.Name =string.Empty;
			var userCase = UsersCases();

			Func<Task> act = async () => await userCase.Execute(request);

			(await act.Should().ThrowAsync<ErrorOnValidationException>())
				.Where(
				ex => ex.ErrorMessages.Count == 1 &&
				ex.ErrorMessages.Contains(ResourceMessagesException.NAME_EMPTY));
		}

	}
}
