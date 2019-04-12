using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UniversitySchedule.Core.Enums;
using UniversitySchedule.Core.ExceptionTypes;
using UniversitySchedule.Core.Models;

namespace UniversitySchedule.API.ErrorHandling
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private const string CONTENT_TYPE = "application/json";

        public ErrorHandlingMiddleware(RequestDelegate next)
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

        public Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            return WriteExceptionAsync(context, exception, code);
        }

        private Task WriteExceptionAsync(HttpContext context, Exception exception, HttpStatusCode code)
        {
            var response = context.Response;
            response.ContentType = CONTENT_TYPE;

            object obj;
            if (exception is LogicException logicException)
            {
                response.StatusCode = (int)StatusCode.INTERNAL_ERROR;
                var errMessage = logicException.MessageType.HasValue ? logicException.MessageType.ToString() : logicException.Message;

                obj = new Response
                {
                    ErrorMessage = errMessage,
                    Status = ResponseStatus.Error,
                    ErrorCode = logicException.MessageType
                };
            }
            else
            {
                if (exception is DbUpdateException updateException)
                {
                    response.StatusCode = (int)StatusCode.INTERNAL_ERROR;
                    obj = new Response
                    {
                        Status = ResponseStatus.Error,
                        ErrorMessage = updateException.InnerException.Message
                    };
                }
                else
                {
                    response.StatusCode = (int)code;
                    obj = new Response
                    {
                        ErrorMessage = exception.Message,
                        Status = ResponseStatus.Error
                    };
                }
            }

            return response.WriteAsync(JsonConvert.SerializeObject(obj, Formatting.Indented,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
        }
    }
}
