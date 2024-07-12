namespace ENPDotNetCore.MvcChartApp.Models;

public class CanvasBarChartModel
{
	public List<CanvasBarChartData> CanvasBarChartData { get; set; }
}
public class CanvasBarChartData
{
	public int NumberOfCompanies { get; set; }
	public string Country { get; set; }
}