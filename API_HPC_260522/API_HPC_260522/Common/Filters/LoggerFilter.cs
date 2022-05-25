using API_HPC_260522.Common.Utils;
using API_HPC_260522.Models;
using API_HPC_260522.Models.Requests;
using API_HPC_260522.Repositories;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API_HPC_260522.Common.Filters
{
    public class LoggerFilter: ActionFilterAttribute
    {
        private readonly CorrelationContext _correlationContext;
        private readonly IUnitOfWork _unitOfWork;

        public LoggerFilter(CorrelationContext correlationContext, IUnitOfWork unitOfWork) 
        {
            _correlationContext = correlationContext;
            _unitOfWork = unitOfWork;
        }


        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) 
        {
            try
            {
                var request = context.HttpContext.Request;
                var actionName = ((ControllerActionDescriptor)context.ActionDescriptor)?.ActionName;
                string requestBody = actionName.Equals("Get") || actionName.Equals("Delete") ? "No Aplica" : GetJsonRequest(context);

                var url = Microsoft.AspNetCore.Http.Extensions.UriHelper.GetDisplayUrl(request);
                _unitOfWork.LoggerRepository.AddLogger(new LoggerRequest { 
                    CorrelacionId = _correlationContext.CorrelationId,
                    EstatusCode = (int)HttpStatusCode.OK,
                    Method = actionName,
                    Url = url,
                    Request = requestBody,
                    Response = string.Empty
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                throw;
            }

            await base.OnActionExecutionAsync(context, next);
        }

        public override void OnActionExecuted(ActionExecutedContext context) 
        {
            try
            {
                var request = new LoggerRequest { CorrelacionId = _correlationContext.CorrelationId, Response = GetJsonResponse(context), EstatusCode = (int)HttpStatusCode.OK };
                if (context.Exception != null)
                {
                    request.EstatusCode = (int)HttpStatusCode.InternalServerError;
                    request.ErrorMessage = $"Message: {context.Exception.Message}, StackTrace: {context.Exception.StackTrace}";
                }
                _unitOfWork.LoggerRepository.UpdateLogger(request);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                throw;
            }
            base.OnActionExecuted(context);
        }

        #region METHODS PRIVADOS
        private HttpStatusCode GetHttpStatusCode(ActionExecutedContext context)
        {

            var result = ((Microsoft.AspNetCore.Mvc.ObjectResult)context.Result);
            return (HttpStatusCode)result?.StatusCode;
        }

        private string GetJsonResponse(ActionExecutedContext context)
        {

            var result = ((Microsoft.AspNetCore.Mvc.ObjectResult)context.Result);
            return result.ToJsonString();
        }

        private string GetJsonRequest(ActionExecutingContext context)
        {
            var keysOfArguments = context.ActionArguments.Keys.ToList();
            foreach (var key in keysOfArguments)
            {
                context.ActionArguments.TryGetValue(key, out object actionArgument);
                return actionArgument.ToJsonString();
            }
            return null;
        }



        #endregion
    }
}
