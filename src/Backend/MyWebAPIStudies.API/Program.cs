using MyWebAPIStudies.API.Filters;
using MyWebAPIStudies.Application;
using MyWebAPIStudies.Domain.Repositories.User;
using MyWebAPIStudies.Infrastructure;
using MyWebAPIStudies.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add filter exception
builder.Services.AddMvc(op => op.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddApplication(builder.Configuration);// Using extension method 
builder.Services.AddInfrastructure(builder.Configuration);// Using extension method 

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

app.Run();
