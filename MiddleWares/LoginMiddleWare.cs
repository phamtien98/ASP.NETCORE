using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Day4
{
    public class LoginMiddleWare
    {
        private readonly RequestDelegate _next;
        public LoginMiddleWare(RequestDelegate next)
        {
            this._next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            using var bufer = new MemoryStream();
            var request = context.Request;
            var response = context.Response;
            var stream = response.Body;
            response.Body = bufer;
            await _next(context);
            Debug.WriteLine("Request Content Type: {0} {1} {2} {3} {4}", request.Scheme, request.Host, request.Path, request.QueryString, request.Body);
            
            using (FileStream fileStream = new FileStream("Log.txt", FileMode.Append))
            {
                StreamWriter streamWriter = new StreamWriter(fileStream);
                streamWriter.WriteLine("Request Content Type: Scheme: {0}  Host: {1} Path:  {2} QueryString: {3} Body: {4}", request.Scheme, request.Host, request.Path, request.QueryString, request.Body);
                streamWriter.Flush();
            }

            bufer.Position = 0;           
            await bufer.CopyToAsync(stream);
        }
    }
}