namespace ENPDotNetCore.MvcChartApp.Models;

public class GridConfigurationChartModel
{
    public List<string> Labels { get; set; }

    public List<DataSet> DataSets { get; set; }

    
}
public class DataSet
{
    public string Label { get; set; }

    public List<int> Data { get; set; }

    public bool Fill { get; set; }

    public string BorderColor { get; set; }

    public string BackgroundColor { get; set; }

}
