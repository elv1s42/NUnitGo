using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HtmlCustomElements.CSSElements
{
    public class CssSet
    {
        public string Name;
        private readonly HashSet<CssElement> _elements;

        public CssSet()
        {
            _elements = new HashSet<CssElement>();
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
        public new string ToString()
        {
            return _elements.Aggregate("", (current, element) => current + element.ToString());
        }
    }
}
