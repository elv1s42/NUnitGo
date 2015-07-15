using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using HtmlCustomElements.CSSElements;

namespace HtmlCustomElements.HtmlCustomElements
{
    public class Accordion : HtmlBaseElement
    {
        public List<AccordionElement> Elements;
        public string AccordionHtml;
        private readonly string _id;
        
        public Accordion(string id, string title, List<AccordionElement> elements)
        {
            Id = id;
            Style = GetStyleString();
            Title = title;
            Elements = elements;
            AccordionHtml = GetAccordion();
            _id = "#" + id + " ";
        }

        public string GetStyleString()
        {
            var barCssSet = new CssSet("accordion-style");
            barCssSet.AddElement(new CssElement(_id + ".accordion .tab")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.PaddingTop, "7px"),
					new StyleAttribute(HtmlTextWriterStyle.PaddingLeft, "20px"),
                    new StyleAttribute(HtmlTextWriterStyle.Color, "black"),
                    new StyleAttribute(HtmlTextWriterStyle.Display, "block"),
					new StyleAttribute(HtmlTextWriterStyle.Height, "35px"),
					new StyleAttribute(HtmlTextWriterStyle.TextDecoration, "none")
				}
            });
            barCssSet.AddElement(new CssElement(_id + ".accordion .tab-close")
            {
                StyleFields = new List<StyleAttribute>
				{
                    new StyleAttribute(HtmlTextWriterStyle.Color, "black"),
					new StyleAttribute(HtmlTextWriterStyle.Margin, "1% 2% 2% 97%"),
                    new StyleAttribute(HtmlTextWriterStyle.Display, "block"),
					new StyleAttribute(HtmlTextWriterStyle.Height, "35px"),
					new StyleAttribute(HtmlTextWriterStyle.TextDecoration, "none")
				}
            }); 
            barCssSet.AddElement(new CssElement(_id + ".accordion .accordion-tab")
            {
                StyleFields = new List<StyleAttribute>
				{
                    new StyleAttribute(HtmlTextWriterStyle.Display, "block"),
					new StyleAttribute(HtmlTextWriterStyle.Height, "35px"),
					new StyleAttribute(HtmlTextWriterStyle.TextDecoration, "none")
				}
            });
            barCssSet.AddElement(new CssElement(_id + ".accordion .tab:hover,.accordion div:target .tab")
            {
                StyleFields = new List<StyleAttribute>
				{
                    new StyleAttribute("-o-transition", "0.5s ease-out, background 0.5s ease-in"),
                    new StyleAttribute("-ms-transition", "0.5s ease-out, background 0.5s ease-in"),
                    new StyleAttribute("-moz-transition:color", "0.5s ease-out, background 0.5s ease-in"),
                    new StyleAttribute("-webkit-transition", "0.5s ease-out, background 0.5s ease-in"),
                    new StyleAttribute("transition", "0.5s ease-out, background 0.5s ease-in"),
					new StyleAttribute("background", "#CCEBE8"),
					new StyleAttribute(HtmlTextWriterStyle.PaddingTop, "10px"),
					new StyleAttribute(HtmlTextWriterStyle.PaddingLeft, "25px"),
					new StyleAttribute(HtmlTextWriterStyle.TextDecoration, "none")
				}
            });
            barCssSet.AddElement(new CssElement(_id + ".accordion .tab-close:hover,.accordion div:target .tab-close")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.Color, "red"),
					new StyleAttribute(HtmlTextWriterStyle.TextDecoration, "none")
				}
            });
            barCssSet.AddElement(new CssElement(_id + ".accordion div .content")
            {
                StyleFields = new List<StyleAttribute>
				{
                    new StyleAttribute(HtmlTextWriterStyle.Height, "70%"),
                    new StyleAttribute(HtmlTextWriterStyle.BackgroundColor, "white"),
                    new StyleAttribute(HtmlTextWriterStyle.Display, "none")
				}
            });
            barCssSet.AddElement(new CssElement(_id + ".accordion div:target .content")
            {
                StyleFields = new List<StyleAttribute>
				{
                    new StyleAttribute(HtmlTextWriterStyle.Display, "block")
				}
            });
            barCssSet.AddElement(new CssElement(_id + ".accordion > div")
            {
                StyleFields = new List<StyleAttribute>
				{
                    new StyleAttribute(HtmlTextWriterStyle.Height, "40px"),
                    new StyleAttribute(HtmlTextWriterStyle.Overflow, "hidden")
				}
            });
            barCssSet.AddElement(new CssElement(_id + ".accordion > div:target")
            {
                StyleFields = new List<StyleAttribute>
				{
                    new StyleAttribute(HtmlTextWriterStyle.Height, "300px")
				}
            });
            barCssSet.AddElement(new CssElement(_id + ".accordion .content h1")
            {
                StyleFields = new List<StyleAttribute>
				{
                    new StyleAttribute("font", "18px/32px Arial, sans-serif")
				}
            });
            barCssSet.AddElement(new CssElement(_id + ".accordion .content p")
            {
                StyleFields = new List<StyleAttribute>
				{
                    new StyleAttribute(HtmlTextWriterStyle.Margin, "10px 0"),
                    new StyleAttribute("font", "11px/16px Arial, sans-serif")
				}
            });
            barCssSet.AddElement(new CssElement(_id + ".accordion .content span")
            {
                StyleFields = new List<StyleAttribute>
				{
                    new StyleAttribute(HtmlTextWriterStyle.Color, "#4f4f4f"),
                    new StyleAttribute("font", "italic 11px/12px Georgia, Arial, sans-serif")
				}
            });
            barCssSet.AddElement(new CssElement(_id + ".accordion .content em.bullet")
            {
                StyleFields = new List<StyleAttribute>
				{
                    new StyleAttribute(HtmlTextWriterStyle.Width, "5px"),
                    new StyleAttribute(HtmlTextWriterStyle.Height, "5px"),
                    new StyleAttribute(HtmlTextWriterStyle.Margin, "0 5px"),
                    new StyleAttribute("background", "#6b8f1a"),
                    new StyleAttribute(HtmlTextWriterStyle.Display, "inline-block")
				}
            });
            barCssSet.AddElement(new CssElement(_id + ".accordion .content ul li")
            {
                StyleFields = new List<StyleAttribute>
				{
                    new StyleAttribute("list-style", "none"),
                    new StyleAttribute("float", "left")
				}
            });
            return barCssSet.ToString();
        }

        private string GetAccordion()
        {
            var stringWriter = new StringWriter();
            using (var writer = new HtmlTextWriter(stringWriter))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Id, Id);
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "accordion");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                foreach (var element in Elements)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Id, element.Id);
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "accordion-tab");
                    writer.RenderBeginTag(HtmlTextWriterTag.Div);

                    writer.AddAttribute(HtmlTextWriterAttribute.Href, "#" + element.Id);
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "tab");
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.Write(element.Title);
                    writer.RenderEndTag();

                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "content");
                    writer.RenderBeginTag(HtmlTextWriterTag.Div);
                    writer.Write(element.InnerHtml);
                    writer.RenderEndTag();

                    writer.AddAttribute(HtmlTextWriterAttribute.Href, "#");
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "tab-close");
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.Write("Close");
                    writer.RenderEndTag();

                    writer.RenderEndTag();
                }
                writer.RenderEndTag();
                writer.RenderEndTag();
            }
            return stringWriter.ToString();
        }
    }
}
