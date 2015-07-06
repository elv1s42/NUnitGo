using System.IO;
using System.Web.UI;

namespace HtmlCustomElements.HtmlCustomElements
{
    public class HorizontalBar : HtmlBaseElement
    {
        public HorizontalBar(string id, string style, string title)
        {
            Id = id;
            Style = style;
            Title = title;
        }

        static string GetDivElements()
        {
            // Initialize StringWriter instance.
            var stringWriter = new StringWriter();

            // Put HtmlTextWriter in using block because it needs to call Dispose.
            using (var writer = new HtmlTextWriter(stringWriter))
            {
                string[] words = { "Sam", "Dot", "Perls" };
                // Loop over some strings.
                foreach (var word in words)
                {
                    // Some strings for the attributes.
                    const string classValue = "ClassName";
                    const string urlValue = "http://www.dotnetperls.com/";
                    const string imageValue = "image.jpg";

                    // The important part:
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, classValue);
                    writer.RenderBeginTag(HtmlTextWriterTag.Div); // Begin #1

                    writer.AddAttribute(HtmlTextWriterAttribute.Href, urlValue);
                    writer.RenderBeginTag(HtmlTextWriterTag.A); // Begin #2

                    writer.AddAttribute(HtmlTextWriterAttribute.Src, imageValue);
                    writer.AddAttribute(HtmlTextWriterAttribute.Width, "60");
                    writer.AddAttribute(HtmlTextWriterAttribute.Height, "60");
                    writer.AddAttribute(HtmlTextWriterAttribute.Alt, "");

                    writer.RenderBeginTag(HtmlTextWriterTag.Img); // Begin #3
                    writer.RenderEndTag(); // End #3

                    writer.Write(word);

                    writer.RenderEndTag(); // End #2
                    writer.RenderEndTag(); // End #1
                }
            }
            // Return the result.
            return stringWriter.ToString();
        }

    }
}
