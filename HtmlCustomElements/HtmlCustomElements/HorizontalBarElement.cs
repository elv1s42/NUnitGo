namespace HtmlCustomElements.HtmlCustomElements
{
    public class HorizontalBarElement
    {
        public string InnerText;
        public string TooltipText;
        public string BackgroundColor;
        public double Value;
        public string IdToShow;

        public HorizontalBarElement(string innerText, string tooltipText, string backgroundColor, double value, string idToShow = "")
        {
            Value = value;
            InnerText = innerText;
            TooltipText = tooltipText;
            BackgroundColor = backgroundColor;
            IdToShow = idToShow;
        }
    }
}
