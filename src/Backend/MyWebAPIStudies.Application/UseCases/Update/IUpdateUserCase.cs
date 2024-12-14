using MyWebAPIStudies.Communication.Requests;

namespace MyWebAPIStudies.Application.UseCases.Update
{
	public interface IUpdateUserCase
	{
		Task Execute(RequestUpdateUserJson request);
	}
}
