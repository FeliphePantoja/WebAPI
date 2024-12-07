using CommonTestUtil.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyWebAPIStudies.Infrastructure.Data;

namespace WebApi.Test
{
	public class WebApplicationFactory : WebApplicationFactory<Program>
	{
		public MyWebAPIStudies.Domain.Entities.User GetUser { get; private set; } = default!;
		public string PassWord { get; private set; } = string.Empty;

		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder.UseEnvironment("Test")
				.ConfigureServices(services =>
				{
					var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<MyDbContext>));
					if (descriptor is not null)
						services.Remove(descriptor);

					var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

					services.AddDbContext<MyDbContext>(opt =>
					{
						opt.UseInMemoryDatabase("InMemoryDbForTestign");
						opt.UseInternalServiceProvider(provider);
					});


					using var scope = services.BuildServiceProvider().CreateScope();
					var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();
					dbContext.Database.EnsureCreated();
					StartDataBase(dbContext);
				});
		}

		private void StartDataBase(MyDbContext db)
		{
			(GetUser, PassWord) = UserBuilder.Build();

			db.Users.Add(GetUser);
			db.SaveChanges();
		}
	}
}
