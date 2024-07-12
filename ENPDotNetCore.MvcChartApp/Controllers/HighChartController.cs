using ENPDotNetCore.MvcChartApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ENPDotNetCore.MvcChartApp.Controllers
{
    public class HighChartController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DonutChart()
        {
            var model = new DonutChartModel
            {
                DonutChartData = new List<DonutChartData>
                {
                    new DonutChartData
                    {
                        Name = "EV", Percentage = 23.9
                    },
                    new DonutChartData
                    {
                        Name = "Hybrids", Percentage = 12.6
                    },
                    new DonutChartData
                    {
                        Name = "Diesel", Percentage = 37.0
                    },
                    new DonutChartData
                    {
                        Name = "Petrol", Percentage = 26.4
                    }
                }
            };
            return View(model);
        }

        public IActionResult VariwideChart()
        {
            var model = new VariwideChartModel
            {
                VariwideChartData = new List<VariwideChartData>
                {
                    new VariwideChartData {Country = "Norway", LaborCost = 50.2, GDP = 335504},
                    new VariwideChartData {Country = "Denmark", LaborCost = 42.0, GDP = 277339},
                    new VariwideChartData {Country = "Belgium", LaborCost = 39.2, GDP = 421611},
                    new VariwideChartData {Country = "Sweden", LaborCost = 38.0, GDP = 462057},
                    new VariwideChartData {Country = "France", LaborCost = 35.6, GDP = 2228857},
                    new VariwideChartData {Country = "Netherlands", LaborCost = 34.3, GDP = 702641},
                    new VariwideChartData {Country = "Finland", LaborCost = 33.2, GDP = 215615},
                    new VariwideChartData {Country = "Germany", LaborCost = 33.0, GDP = 3144050},
                    new VariwideChartData {Country = "Austria", LaborCost = 32.7, GDP = 349344},
                    new VariwideChartData {Country = "Ireland", LaborCost = 30.4, GDP = 275567},
                    new VariwideChartData {Country = "Italy", LaborCost = 27.8, GDP = 1672438},
                    new VariwideChartData { Country = "United Kingdom", LaborCost = 26.7, GDP = 2366911},
                    new VariwideChartData {Country = "Spain", LaborCost = 21.3, GDP = 1113851},
                    new VariwideChartData {Country = "Greece", LaborCost = 14.2, GDP = 175887},
                    new VariwideChartData {Country = "Portugal", LaborCost = 13.7, GDP = 184933},
                    new VariwideChartData {Country = "Czech Republic", LaborCost = 10.2, GDP = 176564 },
                    new VariwideChartData {Country = "Poland", LaborCost = 8.6, GDP = 424269},
                    new VariwideChartData {Country = "Romania", LaborCost = 5.5, GDP = 169578}

                }
            };
            return View(model);
        }
    }
}
