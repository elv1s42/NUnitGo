using System;
using System.Collections.Generic;
using System.Linq;

namespace HtmlCustomElements.CSSElements
{
    public class CssElement
    {
        public CssElement(string name)
        {
            Name = name;
            StyleFields = new List<StyleAttribute>();
        }

        public string Name;
        public List<StyleAttribute> StyleFields;

        public void AddStyleFields(List<StyleAttribute> styleFields)
        {
            foreach (var styleField in styleFields)
            {
                if (StyleFields.Any(x => x.Style.Equals(styleField.Style)))
                    StyleFields.First(x => x.Style.Equals(styleField.Style)).Value = styleField.Value;
                else
                {
                    StyleFields.Add(styleField);
                }
            }
        }

        public new string ToString()
        {
            var elementString = "";
            var nl = Environment.NewLine;
            elementString = Name + " {" + nl + StyleFields.Aggregate(elementString, 
                (current, field) => current + "    " + field.ToString() + nl) + "}" + nl;
            return elementString;
        }
    }
}
