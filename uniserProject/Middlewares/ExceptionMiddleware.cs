
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using BookProject.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;

namespace Task.Rest.Api.Middlewares
{
   
    public class ExceptionMiddleware
    {
        //private readonly RequestDelegate _next;
        //private readonly ILogger<Book> _logger;
        //public ExceptionMiddleware(RequestDelegate next, ILogger<Book> logger)
        //{
        //    _logger = logger;
        //    _next = next;
        //}
        //public async System.Threading.Tasks.Task InvokeAsync(HttpContext httpContext)
        //{
        //    try
        //    {
        //        await _next(httpContext);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Something went wrong: {ex}");
        //        await HandleExceptionAsync(httpContext, ex);
        //    }
        //}
        //private async System.Threading.Tasks.Task HandleExceptionAsync(HttpContext context, Exception exception)
        //{
        //    context.Response.ContentType = "application/json";
        //    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        //    await context.Response.WriteAsync("Her Hansi Error var zehmet olmasa database connection" +
        //        " ve ya kodu duzgun tedbiq etdiynizi yoxlayin!!");
        //}
    }
}
