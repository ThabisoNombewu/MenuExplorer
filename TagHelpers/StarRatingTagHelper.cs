using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MenuExplorer.TagHelpers;

// Usage: <star-rating value="4" />
[HtmlTargetElement("star-rating", TagStructure = TagStructure.WithoutEndTag)]
public class StarRatingTagHelper : TagHelper
{
    public int Value { get; set; }
    public int Max { get; set; } = 5;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var value = Math.Clamp(Value, 0, Max);

        output.TagName = "span";
        output.TagMode = TagMode.StartTagAndEndTag;
        output.Attributes.SetAttribute("class", "stars");
        output.Attributes.SetAttribute("role", "img");
        output.Attributes.SetAttribute("aria-label", $"Rated {value} out of {Max}");

        var filled = new string('★', value);
        var empty = new string('★', Max - value);
        output.Content.SetHtmlContent($"{filled}<span class=\"stars-off\">{empty}</span>");
    }
}
