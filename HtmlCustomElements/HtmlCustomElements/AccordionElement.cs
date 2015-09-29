namespace HtmlCustomElements.HtmlCustomElements
{
    public class AccordionElement : HtmlBaseElement
    {
        public string InnerHtml;
        public string BackgroundColor;
        public double Value;

        public AccordionElement(string innerHtml, string title, string id = "")
        {
            InnerHtml = innerHtml;
            Title = title;
            Id = id;
        }
    }
}
