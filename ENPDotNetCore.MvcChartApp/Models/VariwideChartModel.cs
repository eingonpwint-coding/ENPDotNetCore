namespace ENPDotNetCore.MvcChartApp.Models
{
    public class VariwideChartData
    {
        public string Country { get; set; }
        public double LaborCost { get; set; }
        public int GDP { get; set; }
    }
    public class VariwideChartModel
    {
        public List<VariwideChartData> VariwideChartData { get; set; }
    }
}
