using ENPDotNetCore.MvcChartApp.Models;
using Microsoft.AspNetCore.Mvc;
using static ENPDotNetCore.MvcChartApp.Models.GridConfigurationChartModel;

namespace ENPDotNetCore.MvcChartApp.Controllers
{
    public class ChartJsController : Controller
    {
        public IActionResult ExampleChart()
        {
            return View();
        }

        public IActionResult InterpolationLineChart()
        { 
            return View();
        }

        public IActionResult GridConfigurationChart() 
        {
            var model = new GridConfigurationChartModel
            {
                Labels = new List<string> { "January", "February", "March", "April", "May", "June", "July" },
                DataSets = new List<DataSet>
                {
                    new DataSet 
                    {
                        Label = "Dataset 1",
                        Data = new List<int> { 10, 30, 39, 20, 25, 34, -10 },
                        Fill = false,
                        BorderColor = "rgb(255, 99, 132)",
                        BackgroundColor = "rgba(255, 99, 132, 0.5)"
                    },
                    new DataSet
                    {
                        Label = "Dataset 2",
                        Data = new List<int> { 18, 33, 22, 19, 11, -39, 30 },
                        Fill = false,
                        BorderColor = "rgb(54, 162, 235)",
                        BackgroundColor = "rgba(54, 162, 235, 0.5)"
                    }
                }
            };
            return View(model);
        }
    }
}
