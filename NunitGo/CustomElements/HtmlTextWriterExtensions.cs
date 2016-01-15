using System;
using System.Collections.Generic;
using System.Web.UI;

namespace NunitGo.CustomElements
{
    public static class HtmlTextWriterExtensions
    {
        public static void AddTag(this HtmlTextWriter writer, HtmlTextWriterTag tag,
            Dictionary<string, string> attributes, string value = "")
        {
            foreach (var attribute in attributes)
            {
                writer.AddAttribute(attribute.Key, attribute.Value);
            }
            writer.RenderBeginTag(tag);
            if (value != "")
            {
                writer.Write(value);
            }
            writer.RenderEndTag();
        }

        public static void AddTag(this HtmlTextWriter writer, HtmlTextWriterTag tag,
            Dictionary<HtmlTextWriterAttribute, string> attributes, string value = "")
        {
            foreach (var attribute in attributes)
            {
                writer.AddAttribute(attribute.Key, attribute.Value);
            }
            writer.RenderBeginTag(tag);
            if (value != "")
            {
                writer.Write(value);
            }
            writer.RenderEndTag();
        }

        public static void AddTag(this HtmlTextWriter writer, HtmlTextWriterTag tag,
            Dictionary<string, string> attributes1, 
            Dictionary<HtmlTextWriterAttribute, string> attributes2,
            string value = "")
        {
            foreach (var attribute in attributes1)
            {
                writer.AddAttribute(attribute.Key, attribute.Value);
            }
            foreach (var attribute in attributes2)
            {
                writer.AddAttribute(attribute.Key, attribute.Value);
            }
            writer.RenderBeginTag(tag);
            if (value != "")
            {
                writer.Write(value);
            }
            writer.RenderEndTag();
        }

        public static void AddTag(this HtmlTextWriter writer, HtmlTextWriterTag tag,
            string value = "")
        {
            writer.RenderBeginTag(tag);
            writer.Write(value != "" ? value : Environment.NewLine);
            writer.RenderEndTag();
        }

        public static void AddTag(this HtmlTextWriter writer, string tag, string value = "")
        {
            writer.RenderBeginTag(tag);

            writer.Write(value != "" ? value : Environment.NewLine);
            writer.RenderEndTag();
        }

        public static void OpenTreeItem(this HtmlTextWriter writer, string name, string id, string fontSize = "100%", bool isChecked = true)
        {
            writer.RenderBeginTag(HtmlTextWriterTag.Ul);
            writer.RenderBeginTag(HtmlTextWriterTag.Li);
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "checkbox");
            if (isChecked)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Checked, "checked");
            }
            writer.AddAttribute(HtmlTextWriterAttribute.Id, id);
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag(); //INPUT
            writer.AddAttribute(HtmlTextWriterAttribute.For, id);
            writer.AddStyleAttribute(HtmlTextWriterStyle.FontWeight, "bold");
            writer.AddStyleAttribute(HtmlTextWriterStyle.FontSize, fontSize);
            writer.RenderBeginTag(HtmlTextWriterTag.Label);
            writer.Write(name);
            writer.RenderEndTag(); //LABEL
        }

        public static void CloseTreeItem(this HtmlTextWriter writer)
        {
            writer.RenderEndTag();//LI
            writer.RenderEndTag();//UL
        }
    }
}
