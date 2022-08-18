using Exchange.Api.Common.Models;
using Exchange.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Exchange.Api.Common.Filter
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly IHostEnvironment env;
        private readonly ILogger<GlobalExceptionFilter> logger;

        public GlobalExceptionFilter(IHostEnvironment env, ILogger<GlobalExceptionFilter> logger)
        {
            this.env = env;
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            logger.LogError("An exception occurred,",
                            context.Exception,
                            context.Exception.Message);

            if (context.Exception.GetType() == typeof(ApplicationBusinessException))
            {
                var json = new ErrorResponse
                {
                    GeneralMessages = new[] { context.Exception.Message }
                };

                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                var json = new ErrorResponse
                {
                    GeneralMessages = new[] { "An error occurred. Try it again." }
                };

                if (env.IsDevelopment())
                {
                    json.DeveloperMessage = context.Exception.ToString();
                }

                context.Result = new InternalServerErrorObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            context.ExceptionHandled = true;

        }
    }
}
