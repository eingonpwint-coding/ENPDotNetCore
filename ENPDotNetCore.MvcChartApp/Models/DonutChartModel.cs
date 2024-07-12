namespace ENPDotNetCore.MvcChartApp.Models;

public class DonutChartData
{
    public string Name { get; set; }
    public double Percentage { get; set; }
}

public class DonutChartModel
{
    public List<DonutChartData> DonutChartData { get; set; }
}
