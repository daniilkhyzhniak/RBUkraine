using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace RBUkraine.PL.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {

                var viewData = context.HttpContext.Items;
                viewData.Add("Model", context.ActionArguments.Values.FirstOrDefault());
                var viewResult = new ViewResult
                {
                    ViewData = viewData as ViewDataDictionary
                };
                context.Result = viewResult;
                return;
            }

            await next();
        }
    }
}