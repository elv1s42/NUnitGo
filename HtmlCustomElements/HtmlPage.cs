using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.UI;

namespace HtmlCustomElements
{
    public class HtmlPage
    {
        private string _page;

        public HtmlPage(string pageTitle)
        {
            var strWr = new StringWriter();
            using (var writer = new HtmlTextWriter(strWr))
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Html);

                writer.RenderBeginTag(HtmlTextWriterTag.Head);

                writer.AddTag(HtmlTextWriterTag.Meta, new Dictionary<string, string>
                {
                    {"http-equiv", "Content-Type"},
                    {"content", @"text/html"},
                    {"charset", "windows-1251"}
                });
                writer.AddTag(HtmlTextWriterTag.Title, pageTitle);

                writer.RenderEndTag(); //HEAD

                writer.RenderBeginTag(HtmlTextWriterTag.Body);
                writer.RenderEndTag(); //BODY
                
                writer.RenderEndTag(); //HTML
            }
            _page = strWr.ToString();
        }

        public void AddToBody(string stringToAdd)
        {
            var strWr = new StringWriter(new StringBuilder(_page));
            using (var writer = new HtmlTextWriter(strWr))
            {

            }
        }

        public void SavePage(string path, string name)
        {
            File.WriteAllText(path + @"\" + name + ".html", _page);
        }
    }
}
