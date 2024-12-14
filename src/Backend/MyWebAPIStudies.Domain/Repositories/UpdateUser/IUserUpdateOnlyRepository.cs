namespace MyWebAPIStudies.Domain.Repositories.UpdateUser
{
	public interface IUserUpdateOnlyRepository
	{
		Task<Entities.User> GetById(long id);
		void Update(Entities.User user);
	}
}
