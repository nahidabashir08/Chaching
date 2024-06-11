using System.Runtime.CompilerServices;

namespace Chaching.Configurations
{
	public static class ApplicationBuilderExtensions
	{
		public static IApplicationBuilder AppGlobalErrorHandler(this IApplicationBuilder applicationBuilder)
			=> applicationBuilder.UseMiddleware<GlobalExceptionHandlingMiddleware>();
	}
}
