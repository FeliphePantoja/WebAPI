using Microsoft.AspNetCore.Mvc;
using MyWebAPIStudies.Application.UseCases.User.Create;
using MyWebAPIStudies.Communication.Requests;
using MyWebAPIStudies.Communication.Responses;

namespace MyWebAPIStudies.API.Controllers
{
	public class UserController : BaseController
	{
		[HttpPost]
		[ProducesResponseType(typeof(ResponseCreateUserJson), StatusCodes.Status201Created)]
		public async Task<IActionResult> Register(
			[FromServices] ICreateUserCase createUser,//Injetando a dependencia na requisição
			[FromBody] RequestCreateUserJson user)//Vem do body
		{
			var userCase = await createUser.Execute(user);
			return Created(string.Empty, userCase);
		}
	}
}
