namespace HtmlCustomElements.HtmlCustomElements
{
    public class HorizontalBarElement
    {
        public string InnerText;
        public string TooltipText;
        public string BackgroundColor;
        public double Value;

        public HorizontalBarElement(string innerText, string tooltipText, string backgroundColor, double value)
        {
            Value = value;
            InnerText = innerText;
            TooltipText = tooltipText;
            BackgroundColor = backgroundColor;
        }
    }
}
