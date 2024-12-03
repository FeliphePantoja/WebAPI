using Dapper;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;

namespace MyWebAPIStudies.Infrastructure.Migrations
{
	public static class DataBaseMigration
	{
		public static void Migrate(string connectionString, IServiceProvider serviceProvider)
		{
			DatabaseCreated(connectionString);
			MigrationDataBase(serviceProvider);
		}

		private static void DatabaseCreated(string connectionString)
		{
			var connectionStringBuilder = new MySqlConnectionStringBuilder(connectionString);
			var dataBaseName = connectionStringBuilder.Database;
			connectionStringBuilder.Remove("Database");

			var parameters = new DynamicParameters();
			parameters.Add("name", dataBaseName);

			using var dbConnection = new MySqlConnection(connectionStringBuilder.ConnectionString);
			var records = dbConnection.Query("SELECT * FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @name", parameters);

			if (!records.Any())
				dbConnection.Execute($"CREATE DATABASE {dataBaseName}");
		}

		private static void MigrationDataBase(IServiceProvider serviceProvider)
		{
			var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
			runner.ListMigrations();
			runner.MigrateUp();
		}
	}
}
