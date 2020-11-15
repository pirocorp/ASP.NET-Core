namespace LearningSystem.Web.Infrastructure.Filters
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// This action filter validates the model state,
    /// if the action accepts model argument containing in it's name model
    /// and controller is derived from Controller 
    /// </summary>
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                return;
            }

            var controller = context.Controller as Controller;

            if (controller is null)
            {
                return;
            }

            var model = context.ActionArguments
                .FirstOrDefault(a => a.Key.ToLower().Contains("model")).Value;

            if (model is null)
            {
                return;
            }

            context.Result = controller.View(model);
        }
    }
}
