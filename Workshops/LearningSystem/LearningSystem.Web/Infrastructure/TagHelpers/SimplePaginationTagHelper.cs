namespace LearningSystem.Web.Infrastructure.TagHelpers
{
    using Microsoft.AspNetCore.Razor.TagHelpers;

    /// <summary>
    /// Bootstrap Simple Pagination Tag Helper
    /// </summary>
    [HtmlTargetElement("pagination", Attributes = "total-pages, current-page, link-url")]
    public class SimplePaginationTagHelper : TagHelper
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        [HtmlAttributeName("link-url")]
        public string Url { get; set; }

        [HtmlAttributeName("query-params")]
        public string QueryParameters { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.PreContent.SetHtmlContent(@"<ul class=""pagination"">");

            if (this.CurrentPage <= 1)
            {
                GenerateDisabledPaginationButton(output, "Previous");
            }
            else
            {
                var prevPage = this.CurrentPage - 1;
                this.GeneratePaginationButton(output, prevPage, "Previous");
            }

            for (var i = 1; i <= this.TotalPages; i++)
            {
                if (i == this.CurrentPage)
                {
                    GenerateCurrentPageButton(output, i);
                }
                else
                {
                    this.GeneratePaginationButton(output, i, i.ToString());
                }
            }

            if (this.CurrentPage >= this.TotalPages)
            {
                GenerateDisabledPaginationButton(output, "Next");
            }
            else
            {
                var nextPage = this.CurrentPage + 1;
                this.GeneratePaginationButton(output, nextPage, "Next");
            }

            output.PostContent.SetHtmlContent("</ul>");
            output.Attributes.Clear();
        }

        private void GeneratePaginationButton(TagHelperOutput output, int page, string btnName)
        {
            output.Content.AppendHtml($@"
                <li class=""page-item"">
	                <a class=""page-link"" href=""{this.Url}?page={page}&{this.QueryParameters}"">{btnName}</a>
                </li>");
        }

        private static void GenerateDisabledPaginationButton(TagHelperOutput output, string btnName)
        {
            output.Content.AppendHtml($@"
                <li class=""page-item disabled"">
	                <span class=""page-link"">{btnName}</span>
                </li>");
        }

        private static void GenerateCurrentPageButton(TagHelperOutput output, int i)
        {
            output.Content.AppendHtml($@"
                    <li class=""page-item active"" aria-current=""page"">
                        <span class=""page-link"">
	                        {i}
	                        <span class=""sr-only"">(current)</span>
                        </span>
                    </li>");
        }
    }
}
