using Microsoft.AspNetCore.Mvc;
using MyWebAPIStudies.Application.UseCases.Profile;
using MyWebAPIStudies.Application.UseCases.Update;
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

		[HttpGet]
		[ProducesResponseType(typeof(ResponseCreateUserJson),StatusCodes.Status200OK)]
		public async Task<IActionResult> GetUserProfile(
			[FromServices] IGetUserProfileCase useCase, 
			[FromBody] RequestProfileJson requestProfile)
		{
			var result = await useCase.Execute(requestProfile);
			return Ok(result);
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(typeof(ResponseErrorJson),StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Update(
			[FromServices] IUpdateUserCase userCase,
			[FromBody] RequestUpdateUserJson request)
		{
			await userCase.Execute(request);
			return NoContent();
		}
	}
}
