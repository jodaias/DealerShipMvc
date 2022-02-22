using KissLog;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DealerShipMvc.Extensions
{
    public class AuditFilter : IActionFilter
    {
        private readonly IKLogger _iKLogger;

        public AuditFilter(IKLogger iKLogger)
        {
            _iKLogger = iKLogger;
        }

        public void OnActionExecuted(ActionExecutedContext context){}

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var message = context.HttpContext.User.Identity.Name + "Accessed"
                    + context.HttpContext.Request.HttpContext.Request.GetDisplayUrl();

                _iKLogger.Info(message);
            }
        }
    }
}
