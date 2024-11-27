using Microsoft.AspNetCore.Mvc;
using MyWebAPIStudies.Application.UseCases.User.Create;
using MyWebAPIStudies.Communication.Requests;
using MyWebAPIStudies.Communication.Responses;

namespace MyWebAPIStudies.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		[HttpPost]
		[ProducesResponseType(typeof(ResponseCreateUserJson), StatusCodes.Status201Created)]
		public IActionResult Register(RequestCreateUserJson user)
		{
			var userCase = new CreateUserUseCase().Execute(user);
			return Created(string.Empty, userCase);
		}
	}
}
