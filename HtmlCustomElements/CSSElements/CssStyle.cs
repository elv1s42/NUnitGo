using System.Collections.Generic;
using System.Linq;

namespace HtmlCustomElements.CSSElements
{
    public class CssStyle
    {
        public string Name;
        private readonly HashSet<CssElement> _elements;

        public CssStyle(HashSet<CssElement> elements)
        {
            _elements = elements;
        }

        public void AddElement(CssElement element)
        {
            if (_elements.Any(x => x.Name.Equals(element.Name)))
            {
                _elements.First(x => x.Name.Equals(element.Name))
                    .AddStyleFields(element.StyleFields);

            }
            else
            {
                _elements.Add(element);
            }
        }

        //TODO: CssStyle.ToString

    }
}
