using FluentMigrator;

namespace MyWebAPIStudies.Infrastructure.Migrations.Versions
{
	[Migration(1, "Create table version 1 to save the user's")]
	public class Version0001 : VersionBase
	{
		public override void Up()
		{
			CreateTable("Users")
				.WithColumn("Name").AsString(255).NotNullable()
				.WithColumn("Email").AsString(255).NotNullable()
				.WithColumn("Password").AsString(2000).NotNullable();
		}
	}
}
