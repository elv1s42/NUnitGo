using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace NunitGo.HtmlCustomElements.CSSElements
{
    public class StyleAttribute
    {
        public string Value;
        public string StyleString;

        public StyleAttribute(HtmlTextWriterStyle style, string value)
        {
            Value = value;
            StyleString = StyleToString(style);
        }

        public StyleAttribute(string styleString, string value)
        {
            Value = value;
            StyleString = styleString;
        }

        private readonly Dictionary<string, HtmlTextWriterStyle> _stringStyleDictionary = 
            new Dictionary<string, HtmlTextWriterStyle>
            {
                  {"background-color", HtmlTextWriterStyle.BackgroundColor},
                  {"background-image", HtmlTextWriterStyle.BackgroundImage},
                  {"border-collapse", HtmlTextWriterStyle.BorderCollapse},
                  {"border-color", HtmlTextWriterStyle.BorderColor},
                  {"border-style", HtmlTextWriterStyle.BorderStyle},
                  {"border-width", HtmlTextWriterStyle.BorderWidth},
                  {"color", HtmlTextWriterStyle.Color},
                  {"cursor", HtmlTextWriterStyle.Cursor},
                  {"direction", HtmlTextWriterStyle.Direction},
                  {"display", HtmlTextWriterStyle.Display},
                  {"filter", HtmlTextWriterStyle.Filter},
                  {"font-family", HtmlTextWriterStyle.FontFamily},
                  {"font-size", HtmlTextWriterStyle.FontSize},
                  {"font-style", HtmlTextWriterStyle.FontStyle},
                  {"font-variant", HtmlTextWriterStyle.FontVariant},
                  {"font-weight", HtmlTextWriterStyle.FontWeight},
                  {"height", HtmlTextWriterStyle.Height},
                  {"left", HtmlTextWriterStyle.Left},
                  {"list-style-image", HtmlTextWriterStyle.ListStyleImage},
                  {"list-style-type", HtmlTextWriterStyle.ListStyleType},
                  {"margin", HtmlTextWriterStyle.Margin},
                  {"margin-bottom", HtmlTextWriterStyle.MarginBottom},
                  {"margin-left", HtmlTextWriterStyle.MarginLeft},
                  {"margin-right", HtmlTextWriterStyle.MarginRight},
                  {"margin-top", HtmlTextWriterStyle.MarginTop},
                  {"overflow-x", HtmlTextWriterStyle.OverflowX},
                  {"overflow-y", HtmlTextWriterStyle.OverflowY},
                  {"overflow", HtmlTextWriterStyle.Overflow},
                  {"padding", HtmlTextWriterStyle.Padding},
                  {"padding-bottom", HtmlTextWriterStyle.PaddingBottom},
                  {"padding-left", HtmlTextWriterStyle.PaddingLeft},
                  {"padding-right", HtmlTextWriterStyle.PaddingRight},
                  {"padding-top", HtmlTextWriterStyle.PaddingTop},
                  {"position", HtmlTextWriterStyle.Position},
                  {"text-align", HtmlTextWriterStyle.TextAlign},
                  {"text-decoration", HtmlTextWriterStyle.TextDecoration},
                  {"text-overflow", HtmlTextWriterStyle.TextOverflow},
                  {"top", HtmlTextWriterStyle.Top},
                  {"visibility", HtmlTextWriterStyle.Visibility},
                  {"vertical-align", HtmlTextWriterStyle.VerticalAlign},
                  {"width", HtmlTextWriterStyle.Width},
                  {"white-space", HtmlTextWriterStyle.WhiteSpace},
                  {"z-index", HtmlTextWriterStyle.ZIndex}
            }; 

        public string StyleToString(HtmlTextWriterStyle style)
        {
            return _stringStyleDictionary.First(x => x.Value.Equals(style)).Key;
        }

        public new string ToString()
        {
            return StyleString + " : " + Value + "; ";
        }
    }
}
