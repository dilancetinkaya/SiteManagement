using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace SiteManagement.API.Filters
{
    public class ValidateFilterAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
           
                context.Result = new BadRequestObjectResult(context.ModelState.Values
                    .SelectMany(e=>e.Errors).Select(e=>e.ErrorMessage));
            }
        }
    }
}
