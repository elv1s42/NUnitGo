using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                    {"charset", "utf-8"}
                });
                writer.AddTag(HtmlTextWriterTag.Title, pageTitle);

                writer.RenderEndTag(); //HEAD

                writer.RenderBeginTag(HtmlTextWriterTag.Body);
                writer.RenderEndTag(); //BODY

                writer.AddTag("footer", "Copyright 2015 " + '\u00a9' + " NUnitGo");
                
                writer.RenderEndTag(); //HTML

            }
            _page = strWr.ToString();
        }

        public string AddToBody(string stringToAdd)
        {
            var lines = _page.SplitToLines().ToList();
            foreach (var line in lines.Where(line => line.Contains(@"</body>")))
            {
                lines.Insert(lines.IndexOf(line), stringToAdd);
                _page = String.Join(Environment.NewLine, lines);
                return _page;
            }
            return _page;
        }

        public void SavePage(string path, string name)
        {
            File.WriteAllText(path + @"\" + name + ".html", _page);
        }
    }
}
