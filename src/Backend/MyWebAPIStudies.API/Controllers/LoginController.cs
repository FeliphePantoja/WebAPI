using Microsoft.AspNetCore.Mvc;
using MyWebAPIStudies.Application.UseCases.Login.DoLogin;
using MyWebAPIStudies.Communication.Requests;
using MyWebAPIStudies.Communication.Responses;

namespace MyWebAPIStudies.API.Controllers
{
	public class LoginController : BaseController
	{
		[HttpPost]
		[ProducesResponseType(typeof(ResponseCreateUserJson), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> Login(
			[FromServices] IDoLoginUserCase doLoginUserCase,
			[FromBody] RequestLoginJson requestLogin)
		{
			var response = await doLoginUserCase.Execute(requestLogin);
			return Ok(response);
		}
	}
}
