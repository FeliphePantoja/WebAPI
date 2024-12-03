using Moq;
using MyWebAPIStudies.Domain.Repositories.User;

namespace CommonTestUtil.Repositories
{
	public class UserWriteOnlyRepositoryBuilder
	{
		public static IUserWriteOnlyRepository Build()
		{
			var mock = new Mock<IUserWriteOnlyRepository>();
			return mock.Object;
		}

	}
}
