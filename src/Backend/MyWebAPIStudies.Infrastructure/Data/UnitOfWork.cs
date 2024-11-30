using MyWebAPIStudies.Domain.Repositories;

namespace MyWebAPIStudies.Infrastructure.Data
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly MyDbContext _myDbContext;
		public UnitOfWork(MyDbContext dbContext) => _myDbContext = dbContext;
		public async Task Commit() => await _myDbContext.SaveChangesAsync();
	}
}
