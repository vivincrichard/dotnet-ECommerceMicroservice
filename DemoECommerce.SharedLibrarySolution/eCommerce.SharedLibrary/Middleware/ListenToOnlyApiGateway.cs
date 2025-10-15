﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.SharedLibrary.Middleware
{
    public class ListenToOnlyApiGateway(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            var signedHeader = context.Request.Headers["Api-Gateway"];


            if (signedHeader.FirstOrDefault() is null)
            {
                context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                await context.Response.WriteAsync("Sorry, Service Unavailable");
                return;
            }
            else
            {
                await next(context);
            }
        }
    }
}
