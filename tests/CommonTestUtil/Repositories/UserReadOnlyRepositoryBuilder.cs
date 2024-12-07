using Moq;
using MyWebAPIStudies.Domain.Entities;
using MyWebAPIStudies.Domain.Repositories.User;

namespace CommonTestUtil.Repositories
{
	public class UserReadOnlyRepositoryBuilder
	{
		private readonly Mock<IUserReadOnlyRepository> _repository;
		public UserReadOnlyRepositoryBuilder() => _repository = new Mock<IUserReadOnlyRepository>();

		public IUserReadOnlyRepository Build()
		{
			return _repository.Object;
		}

		public void ExistActiveUserWithEmail(string email)
		{
			_repository.Setup(repo => repo.ExistActiveUserWithEmail(email)).ReturnsAsync(true);
		}

		public void GetUserByEmailAndPassword(User user)
		{
			_repository.Setup(repo => repo.GetUserByEmailAndPassword(user.Email, user.Password)).ReturnsAsync(user);
		}

	}
}
