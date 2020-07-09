namespace Filters.Infrastructure.Filters
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class RedirectExceptionAttribute : ExceptionFilterAttribute
    {
        private readonly Type _exceptionType;

        public RedirectExceptionAttribute()
        {
        }

        public RedirectExceptionAttribute(Type exceptionType)
        {
            this._exceptionType = exceptionType;
        }

        public override void OnException(ExceptionContext context)
        {
            if (this._exceptionType is null
                || this._exceptionType == context.Exception.GetType())
            {
                context.Result = new RedirectResult("/");
            }
        }
    }
}
