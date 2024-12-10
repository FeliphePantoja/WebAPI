using MyWebAPIStudies.Communication.Requests;
using MyWebAPIStudies.Communication.Responses;

namespace MyWebAPIStudies.Application.UseCases.Profile
{
	public interface IGetUserProfileCase
	{
		Task<ResponseUserProfileJson> Execute(RequestProfileJson requestProfile);
	}
}
