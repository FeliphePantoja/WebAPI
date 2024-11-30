using Microsoft.EntityFrameworkCore;
using MyWebAPIStudies.Domain.Entities;
using MyWebAPIStudies.Domain.Repositories.User;
using MyWebAPIStudies.Infrastructure.Data;

namespace MyWebAPIStudies.Infrastructure.Repositories
{
	public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository
	{
		private readonly MyDbContext _myDbContext;
		public UserRepository(MyDbContext dbContext) => _myDbContext = dbContext;

		public async Task Add(User user) => await _myDbContext.AddAsync(user);

		public async Task<bool> ExistActiveUserWithEmail(string email) =>
			await _myDbContext.Users.AnyAsync(user => user.Email.Equals(email) && user.IsActive);
	}
}
