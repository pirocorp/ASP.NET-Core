namespace Filters.Infrastructure.Filters
{
    using Microsoft.AspNetCore.Mvc.Filters;

    public class AddHeaderAttribute : ResultFilterAttribute
    {
        private readonly string _name;
        private readonly string _value;

        public AddHeaderAttribute(string name, string value)
        {
            this._name = name;
            this._value = value;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var responseHeaders = context.HttpContext.Response.Headers;

            if (!responseHeaders.ContainsKey(this._name))
            {
                responseHeaders.Add(this._name, this._value);
            }
        }
    }
}
