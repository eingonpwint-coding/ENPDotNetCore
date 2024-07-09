namespace ENPDotNetCore.MvcChartApp.Models
{
    public class BarChartModel
    {
        public List<int> Series { get; set; }
        public List<string> Labels { get; set; }
        public List<string> Colors { get; set; }
    }
}
