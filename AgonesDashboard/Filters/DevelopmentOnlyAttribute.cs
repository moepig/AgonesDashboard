using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AgonesDashboard.Filters
{
    public class DevelopmentOnlyAttribute : ActionFilterAttribute
    {
        public DevelopmentOnlyAttribute()
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var hostEnvironment = context.HttpContext.RequestServices.GetService<IHostEnvironment>();
            var logger = context.HttpContext.RequestServices.GetService<ILogger<DevelopmentOnlyAttribute>>();

            if (!hostEnvironment.IsDevelopment())
            {
                logger?.LogDebug("deny. context path: %s", context.HttpContext.Request.Path);
                context.Result = new NotFoundResult();
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
