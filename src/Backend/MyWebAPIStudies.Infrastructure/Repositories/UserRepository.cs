using Microsoft.EntityFrameworkCore;
using MyWebAPIStudies.Domain.Entities;
using MyWebAPIStudies.Domain.Repositories.UpdateUser;
using MyWebAPIStudies.Domain.Repositories.User;
using MyWebAPIStudies.Infrastructure.Data;

namespace MyWebAPIStudies.Infrastructure.Repositories
{
	public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository, IUserUpdateOnlyRepository
	{
		private readonly MyDbContext _myDbContext;
		public UserRepository(MyDbContext dbContext) => _myDbContext = dbContext;

		public async Task Add(User user) => await _myDbContext.AddAsync(user);

		public async Task<bool> ExistActiveUserWithEmail(string email) =>
			await _myDbContext.Users.AnyAsync(user => user.Email.Equals(email) && user.IsActive);

		public async Task<User?> GetUserByEmailAndPassword(string email, string password)
		{
			return await _myDbContext
				.Users
				.AsNoTracking()// NoTracking informamos para nosso repositorio que esse retono não será atualizado (melhora de peformace).
				.FirstOrDefaultAsync(user => user.Email.Equals(email) && password.Equals(password) && user.IsActive);
		}

		public async Task<User?> GetUserProfile(string email)
		{
			return await _myDbContext
				.Users
				.AsNoTracking()
				.SingleOrDefaultAsync(user => user.Email.Equals(email));
		}

		public async Task<User> GetById(long id)
		{
			return await _myDbContext
				.Users
				.FirstAsync(user => user.Id == id);
		}

		public void Update(User user)
		{
			_myDbContext.Users.Update(user);
		}
	}
}
