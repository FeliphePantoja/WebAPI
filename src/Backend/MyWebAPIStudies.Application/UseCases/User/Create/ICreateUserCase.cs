using MyWebAPIStudies.Communication.Requests;
using MyWebAPIStudies.Communication.Responses;

namespace MyWebAPIStudies.Application.UseCases.User.Create
{
	public interface ICreateUserCase
	{
		Task<ResponseCreateUserJson> Execute(RequestCreateUserJson newUser);
	}
}
