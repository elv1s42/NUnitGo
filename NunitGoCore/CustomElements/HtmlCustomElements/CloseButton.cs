using System.IO;
using System.Web.UI;
using NUnitGoCore.Extensions;

namespace NUnitGoCore.CustomElements.HtmlCustomElements
{
    public class CloseButton
    {
        private readonly string _buttonText = "Close";
        private readonly string _href;
        public string ButtonHtml;

        public CloseButton(string buttonText, string href)
        {
            _buttonText = buttonText.Equals("") ? _buttonText : buttonText;
            _href = href;
            ButtonHtml = GetHtml();
        }

        private string GetHtml()
        {
            var stringWriter = new StringWriter();
            using (var writer = new HtmlTextWriter(stringWriter))
            {
                writer
                    .Class("btn btn-danger")
                    .Href(_href)
                    .Type("button")
                    .A(_buttonText);
            }
            return stringWriter.ToString();
        }
    }
}
