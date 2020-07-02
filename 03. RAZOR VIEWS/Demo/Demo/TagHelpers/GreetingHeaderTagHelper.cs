namespace Demo.TagHelpers
{
    using Microsoft.AspNetCore.Razor.TagHelpers;

    [HtmlTargetElement("h1", Attributes = "GreetingString")]
    [HtmlTargetElement("h2", Attributes = "GreetingString")]
    [HtmlTargetElement("h3", Attributes = "GreetingString")]
    [HtmlTargetElement("h4", Attributes = "GreetingString")]
    [HtmlTargetElement("h5", Attributes = "GreetingString")]
    [HtmlTargetElement("h6", Attributes = "GreetingString")]
    public class GreetingHeaderTagHelper : TagHelper
    {
        public string GreetingString { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.Add("name", "Piro");
            output.Content.SetContent(this.GreetingString);
            base.Process(context, output);
        }
    }
}
