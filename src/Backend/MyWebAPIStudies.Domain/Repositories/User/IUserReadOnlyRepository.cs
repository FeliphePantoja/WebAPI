namespace MyWebAPIStudies.Domain.Repositories.User
{
	public interface IUserReadOnlyRepository
	{
		Task<bool> ExistActiveUserWithEmail(string email);
		Task<Entities.User?> GetUserByEmailAndPassword(string email, string password);
		Task<Entities.User?> GetUserProfile(string email);
	}
}
