using System;
using System.Collections.Generic;
using System.Web.UI;

namespace NUnitGoCore.Extensions
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

        public static HtmlTextWriter Tag(this HtmlTextWriter writer, HtmlTextWriterTag tag,
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
            return writer;
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

        public static HtmlTextWriter WriteString(this HtmlTextWriter writer, string value = "")
        {
            writer.Write(value != "" ? value : Environment.NewLine);
            return writer;
        }

        public static HtmlTextWriter OpenTag(this HtmlTextWriter writer, HtmlTextWriterTag tag)
        {
            writer.RenderBeginTag(tag);
            return writer;
        }

        public static HtmlTextWriter OpenTag(this HtmlTextWriter writer, string tag)
        {
            writer.RenderBeginTag(tag);
            return writer;
        }

        public static HtmlTextWriter Css(this HtmlTextWriter writer, HtmlTextWriterStyle styleAttr, string value)
        {
            writer.AddStyleAttribute(styleAttr, value);
            return writer;
        }

        public static HtmlTextWriter CssShadow(this HtmlTextWriter writer, string value)
        {
            return writer
                .WithAttr("box-shadow", value)
                .WithAttr("-moz-box-shadow", value)
                .WithAttr("-webkit-box-shadow", value);
        }

        public static HtmlTextWriter Css(this HtmlTextWriter writer, string styleAttr, string value)
        {
            writer.AddStyleAttribute(styleAttr, value);
            return writer;
        }

        public static HtmlTextWriter WithAttr(this HtmlTextWriter writer, HtmlTextWriterAttribute attr, string value)
        {
            writer.AddAttribute(attr, value);
            return writer;
        }

        public static HtmlTextWriter WithAttr(this HtmlTextWriter writer, string attr, string value)
        {
            writer.AddAttribute(attr, value);
            return writer;
        }

        public static HtmlTextWriter Text(this HtmlTextWriter writer, string value)
        {
            writer.Write(value);
            return writer;
        }

        public static HtmlTextWriter Tag(this HtmlTextWriter writer, HtmlTextWriterTag tag, string value)
        {
            return writer.OpenTag(tag).Text(value).CloseTag();
        }

        public static HtmlTextWriter Tag(this HtmlTextWriter writer, string tag, string value)
        {
            return writer.OpenTag(tag).Text(value).CloseTag();
        }

        public static HtmlTextWriter Tag(this HtmlTextWriter writer, string tag)
        {
            return writer.OpenTag(tag).CloseTag();
        }

        public static HtmlTextWriter CloseTag(this HtmlTextWriter writer)
        {
            writer.RenderEndTag();
            return writer;
        }

        public static HtmlTextWriter Tag(this HtmlTextWriter writer, HtmlTextWriterTag tag, Action someAction)
        {
            writer.OpenTag(tag);
            someAction.Invoke();
            return writer.CloseTag();
        }

        public static HtmlTextWriter Tag(this HtmlTextWriter writer, HtmlTextWriterTag tag, Action<HtmlTextWriter> someAction)
        {
            writer.OpenTag(tag);
            someAction.Invoke(writer);
            return writer.CloseTag();
        }
        
        public static HtmlTextWriter Tag(this HtmlTextWriter writer, HtmlTextWriterTag tag, params Action[] someActions)
        {
            writer.OpenTag(tag);
            foreach (var action in someActions)
            {
                action.Invoke();
            }
            return writer.CloseTag();
        }

        public static HtmlTextWriter Tag(this HtmlTextWriter writer, HtmlTextWriterTag tag,
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
            return writer;
        }

        public static HtmlTextWriter DoAction(this HtmlTextWriter writer, Action someAction)
        {
            someAction.Invoke();
            return writer;
        }

        public static HtmlTextWriter If(this HtmlTextWriter writer, bool condition, Action someAction)
        {
            return condition ? writer.DoAction(someAction) : writer;
        }

        public static HtmlTextWriter TagIf(this HtmlTextWriter writer, bool condition, HtmlTextWriterTag tag)
        {
            return condition ? writer.Tag(tag) : writer;
        }

        public static HtmlTextWriter TagIf(this HtmlTextWriter writer, bool condition, HtmlTextWriterTag tag, string value)
        {
            return condition ? writer.Tag(tag, value) : writer;
        }

        public static HtmlTextWriter TagIf(this HtmlTextWriter writer, bool condition, HtmlTextWriterTag tag, Action someAction)
        {
            return condition ? writer.Tag(tag, someAction) : writer;
        }

        public static HtmlTextWriter Stylesheets(this HtmlTextWriter writer, List<string> pathsToCss)
        {
            foreach (var path in pathsToCss)
            {
                writer.Tag(HtmlTextWriterTag.Link, new Dictionary<HtmlTextWriterAttribute, string>
                {
                    {HtmlTextWriterAttribute.Rel, @"stylesheet"},
                    {HtmlTextWriterAttribute.Type, @"text/css"},
                    {HtmlTextWriterAttribute.Href, path}
                });
            }
            return writer;
        }

        public static HtmlTextWriter Scripts(this HtmlTextWriter writer, List<string> pathsToScripts)
        {
            foreach (var path in pathsToScripts)
            {
                writer.WithAttr(HtmlTextWriterAttribute.Src, path)
                    .Tag(HtmlTextWriterTag.Script);
            }
            return writer;
        }
    }
}
