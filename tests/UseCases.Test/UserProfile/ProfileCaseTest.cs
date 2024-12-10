using CommonTestUtil.Repositories;
using CommonTestUtil.Requests;
using MyWebAPIStudies.Application.UseCases.Login.DoLogin;
using MyWebAPIStudies.Application.UseCases.Profile;

namespace UseCases.Test.UserProfile
{
	public class ProfileCaseTest
	{
		private static GetUserProfileCase CreateUseCase()
		{
			var request = CreateUserValidatorBuilder.Build();
			var userReadOnlyRespository = new UserReadOnlyRepositoryBuilder();

			userReadOnlyRespository.GetUserProfile(request.Email);


			return new GetUserProfileCase(userReadOnlyRespository.Build());
		}
	}
}
