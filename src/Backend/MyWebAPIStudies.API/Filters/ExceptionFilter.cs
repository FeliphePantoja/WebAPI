using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyWebAPIStudies.Communication.Responses;
using MyWebAPIStudies.Exceptions;
using MyWebAPIStudies.Exceptions.ExceptionsBase;
using System.Net;

namespace MyWebAPIStudies.API.Filters
{
	public class ExceptionFilter : IExceptionFilter
	{
		public void OnException(ExceptionContext context)
		{
			if (context.Exception is MyRecipeException)
				HandleException(context);
			else
				ThrowUnknowException(context);
		}

		private void HandleException(ExceptionContext context)
		{
			if (context.Exception is InvalidLoginException)
			{
				context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
				context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(context.Exception.Message));
			}
			else if (context.Exception is ErrorOnValidationException)
			{
				var exception = context.Exception as ErrorOnValidationException;
				context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
				context.Result = new BadRequestObjectResult(new ResponseErrorJson(exception!.ErrorMessages));
			}
		}

		private void ThrowUnknowException(ExceptionContext context)
		{
			context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
			context.Result = new ObjectResult(new ResponseErrorJson(ResourceMessagesException.UNKNOWN_ERROR));
		}
	}
}
