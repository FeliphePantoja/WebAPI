using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyWebAPIStudies.Domain.Repositories;
using MyWebAPIStudies.Domain.Repositories.User;
using MyWebAPIStudies.Infrastructure.Data;
using MyWebAPIStudies.Infrastructure.Repositories;
using System.Reflection;

namespace MyWebAPIStudies.Infrastructure
{
	public static class DependecyInjectionExtension
	{
		public static void AddInfrastructure(this IServiceCollection service, IConfiguration config)
		{
			AddDbContext(service, config);
			AddFluentMigrator(service, config);
			AddRepositories(service);
		}

		private static void AddDbContext(IServiceCollection services, IConfiguration config)
		{
			//This line is important because I am using pomelo ;(
			var version = new MySqlServerVersion(new Version(8, 0, 34));

			//I am using DataBase in docker
			services.AddDbContext<MyDbContext>(db =>
			{
				db.UseMySql(config.GetConnectionString("Connection"), version);
			});
		}

		private static void AddRepositories(IServiceCollection services)
		{
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
			services.AddScoped<IUserReadOnlyRepository, UserRepository>();
		}

		private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("Connection");

			services.AddFluentMigratorCore().ConfigureRunner(opt =>{
				opt
				.AddMySql5()
				.WithGlobalConnectionString(connectionString)
				.ScanIn(Assembly.Load("MyWebAPIStudies.Infrastructure")).For.All();
			});
		}
	}
}
