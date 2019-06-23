using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SimpleBlog.Models.ViewModels;

namespace SimpleBlog.Infrastructure
{
    [HtmlTargetElement("ul", Attributes = "page-model")]
    public class PageLinkTagHelper : TagHelper
    {
        private IUrlHelperFactory UrlHelperFactory;

        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            UrlHelperFactory = helperFactory;
        }

        [ViewContext] [HtmlAttributeNotBound] public ViewContext ViewContext { get; set; }

        public PagingInfo PageModel { get; set; }

        public string PageAction { get; set; }
        public string PageCategoryId { get; set; }

        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; }
            = new Dictionary<string, object>();

        public bool PageClassesEnable { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }
        public string PageClassDisable { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (PageModel.TotalPages <= 1)
                return;

            IUrlHelper urlHelper = UrlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder result = new TagBuilder("ul");
            TagBuilder leftarrow = new TagBuilder("li");
            TagBuilder aleftarrow = new TagBuilder("a");
            if (PageModel.CurrentPage == 1)
            {
                leftarrow.Attributes["class"] = "disabled";
                aleftarrow.Attributes["href"] = "#!";
            }
            else
            {
                PageUrlValues["postPage"] = PageModel.CurrentPage - 1;
                PageUrlValues["categoryId"] = PageCategoryId;
                aleftarrow.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
            }

            TagBuilder ileftarrow = new TagBuilder("i");
            ileftarrow.Attributes["class"] = "material-icons";
            ileftarrow.InnerHtml.Append("chevron_left");
            aleftarrow.InnerHtml.AppendHtml(ileftarrow);
            leftarrow.InnerHtml.AppendHtml(aleftarrow);
            result.InnerHtml.AppendHtml(leftarrow);
            
            for (int i = 1; i <= PageModel.TotalPages; i++)
            {
                TagBuilder li = new TagBuilder("li");
                if (PageClassesEnable)
                {
                    li.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
                }

                TagBuilder tag = new TagBuilder("a");
                PageUrlValues["postPage"] = i;
                PageUrlValues["categoryId"] = PageCategoryId;
                tag.Attributes["href"] = i == PageModel.CurrentPage ? "#!" : urlHelper.Action(PageAction, PageUrlValues);
                tag.InnerHtml.Append(i.ToString());
                li.InnerHtml.AppendHtml(tag);
                result.InnerHtml.AppendHtml(li);
            }
            TagBuilder rightarrow = new TagBuilder("li");
            TagBuilder arightarrow = new TagBuilder("a");
            if (PageModel.CurrentPage == PageModel.TotalPages)
            {
                rightarrow.Attributes["class"] = "disabled";
                arightarrow.Attributes["href"] = "#!";
            }
            else
            {
                PageUrlValues["postPage"] = PageModel.CurrentPage + 1;
                PageUrlValues["categoryId"] = PageCategoryId;
                arightarrow.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
            }

            TagBuilder irightarrow = new TagBuilder("i");
            irightarrow.Attributes["class"] = "material-icons";
            irightarrow.InnerHtml.Append("chevron_right");
            arightarrow.InnerHtml.AppendHtml(irightarrow);
            rightarrow.InnerHtml.AppendHtml(arightarrow);
            result.InnerHtml.AppendHtml(rightarrow);

            output.Content.AppendHtml(result.InnerHtml);
        }
    }
}