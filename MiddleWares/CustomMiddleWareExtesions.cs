using Microsoft.AspNetCore.Builder;

namespace Day4
{
    public static class CustomMiddleWareExtesions
    {
        public static IApplicationBuilder UseCustomMiddleWare(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<LoginMiddleWare>();
        }
    }
}