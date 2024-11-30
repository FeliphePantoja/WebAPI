using Microsoft.Extensions.DependencyInjection;
using MyWebAPIStudies.Application.AutoMappers;
using MyWebAPIStudies.Application.Cryptography;
using MyWebAPIStudies.Application.UseCases.User.Create;
using Microsoft.Extensions.Configuration;

namespace MyWebAPIStudies.Application
{
	public static class DependecyInjectionExtension
	{
		public static void AddApplication(this IServiceCollection service, IConfiguration config)
		{
			AddAutoMapper(service);
			AddUserCases(service);
			AddPassWordEncrypter(service, config);
		}

		private static void AddAutoMapper(IServiceCollection service)
		{
			service.AddScoped(opt =>
			new AutoMapper.MapperConfiguration(options => options.AddProfile(
				new AutoMapping())).CreateMapper());
		}

		private static void AddUserCases(IServiceCollection service)
		{
			service.AddScoped<ICreateUserCase, CreateUserUseCase>();
		}

		private static void AddPassWordEncrypter(IServiceCollection service, IConfiguration config)
		{
			//I'm using library for line Microsoft.Extensions.Configuration.Binder.
			var additionalKey = config.GetValue<string>("Settings:Password:AdditionalKey");
			service.AddScoped(opt => new PasswordEncripter(additionalKey!));
		}
	}
}
