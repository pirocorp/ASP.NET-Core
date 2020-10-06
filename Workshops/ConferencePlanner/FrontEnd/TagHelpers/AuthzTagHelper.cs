namespace FrontEnd.TagHelpers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;

    [HtmlTargetElement("*", Attributes = "authz")]
    [HtmlTargetElement("*", Attributes = "authz-policy")]
    public class AuthzTagHelper : TagHelper
    {
        private readonly IAuthorizationService authzService;

        public AuthzTagHelper(IAuthorizationService authzService)
        {
            this.authzService = authzService;
        }

        [HtmlAttributeName("authz")]
        public bool RequiresAuthentication { get; set; }

        [HtmlAttributeName("authz-policy")]
        public string RequiredPolicy { get; set; }

        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var requiresAuth = this.RequiresAuthentication || !string.IsNullOrEmpty(this.RequiredPolicy);
            var showOutput = false;

            if (context.AllAttributes["authz"] != null && !requiresAuth && !this.ViewContext.HttpContext.User.Identity.IsAuthenticated)
            {
                // authz="false" & user isn't authenticated
                showOutput = true;
            }
            else if (!string.IsNullOrEmpty(this.RequiredPolicy))
            {
                // authz-policy="foo" & user is authorized for policy "foo"
                var authorized = false;
                var cachedResult = this.ViewContext.ViewData["AuthPolicy." + this.RequiredPolicy];
                if (cachedResult != null)
                {
                    authorized = (bool)cachedResult;
                }
                else
                {
                    var authResult = await this.authzService.AuthorizeAsync(this.ViewContext.HttpContext.User, this.RequiredPolicy);
                    authorized = authResult.Succeeded;
                    this.ViewContext.ViewData["AuthPolicy." + this.RequiredPolicy] = authorized;
                }

                showOutput = authorized;
            }
            else if (requiresAuth && this.ViewContext.HttpContext.User.Identity.IsAuthenticated)
            {
                // authz="true" & user is authenticated
                showOutput = true;
            }

            if (!showOutput)
            {
                output.SuppressOutput();
            }
        }
    }
}
