using Microsoft.EntityFrameworkCore;
using MyWebAPIStudies.Domain.Entities;

namespace MyWebAPIStudies.Infrastructure.Data
{
	public class MyDbContext(DbContextOptions options) : DbContext(options)
	{
		public DbSet<User> Users { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//Using configuring this project.
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyDbContext).Assembly);
		}
	}
}
