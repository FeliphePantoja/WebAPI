using CommonTestUtil.Entities;
using CommonTestUtil.Mapper;
using CommonTestUtil.Repositories;
using FluentAssertions;
using MyWebAPIStudies.Application.UseCases.Profile;
using MyWebAPIStudies.Communication.Requests;
using MyWebAPIStudies.Domain.Entities;

namespace UseCases.Test.UserProfile
{
	public class ProfileCaseTest
	{
		private static GetUserProfileCase CreateUseCase(User user)
		{
			var mapper = MapperBuilder.Build();
			var userReadOnlyRespository = new UserReadOnlyRepositoryBuilder();
			userReadOnlyRespository.GetUserProfile(user);


			return new GetUserProfileCase(userReadOnlyRespository.Build(), mapper);
		}

		[Fact]
		public async Task Should_return_success_profile_user()
		{

			(var user, var _) = UserBuilder.Build();
			var userCase = CreateUseCase(user);
			var email = new RequestProfileJson 
			{
				Email = user.Email,
			};
			var result = await userCase.Execute(email);

			result.Should().NotBeNull();
			result.Name.Should().Be(user.Name);
		}
	}
}
