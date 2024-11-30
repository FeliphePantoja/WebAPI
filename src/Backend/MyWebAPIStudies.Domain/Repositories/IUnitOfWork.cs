namespace MyWebAPIStudies.Domain.Repositories
{
	public interface IUnitOfWork
	{
		Task Commit();
	}
}
