using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Writeless.Exceptions
{
    public class AppExceptionHandler
    {

        private static void ExceptionHandler(IApplicationBuilder errorApp, Action<Exception, IList<Exception>> builderErrorDetails)
        {
            //TODO: verificar se há necessidade de melhorar algo 
            errorApp.Run(async errorContext =>
            {
                errorContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorContext.Response.ContentType = "application/json";
                errorContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                var error = errorContext.Features.Get<IExceptionHandlerFeature>();
                if (error != null)
                {
                    var ex = error.Error;
                    var errorDetails = new List<Exception>();

                    builderErrorDetails(ex, errorDetails);
                    var errorJson = JsonConvert.SerializeObject(errorDetails);

                    await errorContext.Response.WriteAsync(errorJson, Encoding.UTF8);
                }
            });
        }

        //TODO: testar erro em aplicacao rodando para ver se aparece essa mensagem
        public static void InProd(IApplicationBuilder errorApp)
        {
            ExceptionHandler(errorApp, (ex, errors) => {
                errors.Add(new Exception("Erro interno."));
                InnerExceptionHandler(ex, errors);
            });
        }

        public static void InDev(IApplicationBuilder errorApp)
        {
            ExceptionHandler(errorApp, (ex, errors) => {
                InnerExceptionHandler(ex, errors);
            });
        }

        public static void InnerExceptionHandler(Exception ex, IList<Exception> errors)
        {
            errors.Add(new Exception(ex.Message, ex));

            if (ex.InnerException != null)
                InnerExceptionHandler(ex.InnerException, errors);
        }
    }
}
