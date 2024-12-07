using MyWebAPIStudies.Communication.Requests;
using MyWebAPIStudies.Communication.Responses;

namespace MyWebAPIStudies.Application.UseCases.Login.DoLogin
{
	public interface IDoLoginUserCase
	{
		Task<ResponseCreateUserJson> Execute(RequestLoginJson requestLoginJson);
	}
}
