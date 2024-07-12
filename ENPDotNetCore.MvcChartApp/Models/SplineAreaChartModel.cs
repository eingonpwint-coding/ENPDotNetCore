namespace ENPDotNetCore.MvcChartApp.Models
{
    public class SplineAreaChartModel
    {
        public List<SplineAreaChartData> SplineAreaChartData {  get; set; }
    }
	public class SplineAreaChartData
	{
		public DateTime Year { get; set; }
		public double Revenue { get; set; }
	}
}
