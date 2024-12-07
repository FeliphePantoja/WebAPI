using MyWebAPIStudies.API.Converters;
using MyWebAPIStudies.API.Filters;
using MyWebAPIStudies.Application;
using MyWebAPIStudies.Infrastructure;
using MyWebAPIStudies.Infrastructure.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
	//this AddJsonOptions remove spaces in strings.
	.AddJsonOptions(opt => opt.JsonSerializerOptions.Converters.Add(new StringConverter()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add filter exception
builder.Services.AddMvc(op => op.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddApplication(builder.Configuration);// Using extension method 
builder.Services.AddInfrastructure(builder.Configuration);// Using extension method 

//Para deixar a url com o padrão minusculo ex : User para user
builder.Services.AddRouting(opt => opt.LowercaseUrls = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
MigrateDataBase();
app.Run();

void MigrateDataBase()
{
	if (builder.Configuration.GetValue<bool>("InMemoryTest"))
		return;

	var connect = builder
		.Configuration
		.GetConnectionString("Connection");

	var serviceScope = app
		.Services
		.GetRequiredService<IServiceScopeFactory>()
		.CreateScope()
		.ServiceProvider;

	DataBaseMigration.Migrate(connect!, serviceScope);
}

public partial class Program { }