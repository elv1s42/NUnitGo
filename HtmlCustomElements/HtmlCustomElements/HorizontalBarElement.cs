namespace HtmlCustomElements.HtmlCustomElements
{
    public class HorizontalBarElement
    {
        public string InnerText;
        public string TooltipText;
        public string BackgroundColor;
        public int Value;

        public HorizontalBarElement(string innerText, string tooltipText, string backgroundColor, int value)
        {
            Value = value;
            InnerText = innerText;
            TooltipText = tooltipText;
            BackgroundColor = backgroundColor;
        }
    }
}
