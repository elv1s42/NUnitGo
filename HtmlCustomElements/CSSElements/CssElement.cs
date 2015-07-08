using System.Collections.Generic;
using System.Linq;

namespace HtmlCustomElements.CSSElements
{
    public class CssElement
    {
        public string Name;
        public List<StyleAttribute> StyleFields;

        public void AddStyleFields(List<StyleAttribute> styleFields)
        {
            foreach (var styleField in styleFields)
            {
                if (StyleFields.Any(x => x.Name.Equals(styleField.Name)))
                    StyleFields.First(x => x.Name.Equals(styleField.Name)).Style = styleField.Style;
                else
                {
                    StyleFields.Add(styleField);
                }
            }
        }
    }
}
