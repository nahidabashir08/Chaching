using Chaching.Exceptions;
using System.Net;
using System.Text.Json;
using KeyNotFoundException = Chaching.Exceptions.KeyNotFoundException;
using UnauthorizedAccessException =Chaching.Exceptions.UnauthorizedAccessException;
using NotImplementedException = Chaching.Exceptions.NotImplementedException;

namespace Chaching.Configurations
{
	public class GlobalExceptionHandlingMiddleware
	{
		private readonly RequestDelegate _next;

		public GlobalExceptionHandlingMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		private static Task HandleExceptionAsync(HttpContext context, Exception ex)
		{
			HttpStatusCode statusCode;
			var stackTrace = string.Empty;
			string message ="";
			var exceptionType = ex.GetType();

			if(exceptionType == typeof(NotFoundException)) 
			{ 
				message= ex.Message;
				statusCode = HttpStatusCode.NotFound;
				stackTrace= ex.StackTrace;
			}else if(exceptionType == typeof(BadRequestException)) 
			{ 
				message= ex.Message;
				stackTrace= ex.StackTrace;
				statusCode= HttpStatusCode.BadRequest;
			}else if(exceptionType == typeof(NotImplementedException)) 
			{ 
				message= ex.Message;
				stackTrace= ex.StackTrace;
				statusCode= HttpStatusCode.NotImplemented;
			}else if(exceptionType == typeof(KeyNotFoundException)) 
			{ 
				message= ex.Message;
				stackTrace= ex.StackTrace;
				statusCode= HttpStatusCode.NotFound;
			}else if(exceptionType == typeof(UnauthorizedAccessException)) 
			{ 
				message= ex.Message;
				stackTrace= ex.StackTrace;
				statusCode= HttpStatusCode.Unauthorized;
			}
			else
			{
				message= ex.Message;
				statusCode= HttpStatusCode.InternalServerError;
				stackTrace= ex.StackTrace;
			}

			var exceptionResult = JsonSerializer.Serialize(new { error = message, stackTrace });
			context.Response.ContentType = "application/json";
			context.Response.StatusCode=(int)statusCode;
			return context.Response.WriteAsync(exceptionResult);
		}

	}
}
