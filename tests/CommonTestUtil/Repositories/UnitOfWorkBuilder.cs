using Moq;
using MyWebAPIStudies.Domain.Repositories;

namespace CommonTestUtil.Repositories
{
	public class UnitOfWorkBuilder
	{
		public static IUnitOfWork Build()
		{
			var mock = new Mock<IUnitOfWork>();
			return mock.Object;
		}
	}
}
