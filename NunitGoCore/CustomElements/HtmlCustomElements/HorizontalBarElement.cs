namespace NunitGoCore.CustomElements.HtmlCustomElements
{
    public class HorizontalBarElement
    {
        public string InnerText;
        public string TooltipText;
        public string BackgroundColor;
        public double Value;
        public string Href;

        public HorizontalBarElement(string innerText, string tooltipText, string backgroundColor, double value, string href = "")
        {
            Value = value;
            InnerText = innerText;
            TooltipText = tooltipText;
            BackgroundColor = backgroundColor;
            Href = href;
        }
    }
}
