using System.Collections.Generic;
using System.Linq;

namespace NunitGoCore.CustomElements.CSSElements
{
    public class CssSet
    {
        public string Name;
        private readonly HashSet<CssElement> _elements;

        public CssSet(string name = "")
        {
            Name = name;
            _elements = new HashSet<CssElement>();
        }

        public HashSet<CssElement> GetElements()
        {
            return _elements;
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

        public void AddElements(List<CssElement> elementList)
        {
            foreach (var element in elementList)
            {
                AddElement(element);
            }
        }

        public void AddSet(CssSet setToAdd)
        {
            foreach (var element in setToAdd.GetElements())
            {
                _elements.Add(element);
            }
        }

        public new string ToString()
        {
            return _elements.Aggregate("", (current, element) => current + element.ToString());
        }
    }
}
