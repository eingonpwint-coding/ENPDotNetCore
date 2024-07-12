using ENPDotNetCore.MvcChartApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ENPDotNetCore.MvcChartApp.Controllers
{
    public class CanvasChartController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

		public IActionResult SplineAreaChart()
		{
			var model = new SplineAreaChartModel
			{
				SplineAreaChartData = new List<SplineAreaChartData>
				{
					new SplineAreaChartData{ Year = new DateTime(2000, 1, 1), Revenue = 3289000 },
					new SplineAreaChartData{Year = new DateTime(2001, 1, 1), Revenue = 3830000 },
					new SplineAreaChartData{Year = new DateTime(2002, 1, 1), Revenue = 2009000},
					new SplineAreaChartData {Year = new DateTime(2003, 1, 1), Revenue = 2840000},
					new SplineAreaChartData {Year = new DateTime(2004, 1, 1), Revenue = 2396000 },
					new SplineAreaChartData {Year = new DateTime(2005, 1, 1), Revenue = 1613000},
					new SplineAreaChartData { Year = new DateTime(2006, 1, 1), Revenue = 2821000},
					new SplineAreaChartData { Year = new DateTime(2007, 1, 1), Revenue = 2000000},
					new SplineAreaChartData {Year = new DateTime(2008, 1, 1), Revenue = 1397000},
					new SplineAreaChartData { Year = new DateTime(2009, 1, 1), Revenue = 2506000},
					new SplineAreaChartData {Year = new DateTime(2010, 1, 1), Revenue = 2798000},
					new SplineAreaChartData {Year = new DateTime(2011, 1, 1), Revenue = 3386000},
					new SplineAreaChartData {Year = new DateTime(2012, 1, 1), Revenue = 6704000},
					new SplineAreaChartData {Year = new DateTime(2013, 1, 1), Revenue = 6026000},
					new SplineAreaChartData { Year = new DateTime(2014, 1, 1), Revenue = 2394000},
					new SplineAreaChartData {Year = new DateTime(2015, 1, 1), Revenue = 1872000},
					new SplineAreaChartData {Year = new DateTime(2016, 1, 1), Revenue = 2140000}
				}
			};
			return View(model);
		}

        public IActionResult CanvasBarChart()
        {
			var model = new CanvasBarChartModel
			{
				CanvasBarChartData = new List<CanvasBarChartData>
				{
					new CanvasBarChartData {NumberOfCompanies = 3, Country = "Sweden"},
					new CanvasBarChartData {NumberOfCompanies = 7, Country = "Taiwan"},
					new CanvasBarChartData {NumberOfCompanies = 5, Country = "Russia" },
					new CanvasBarChartData {NumberOfCompanies = 9, Country = "Spain" },
					new CanvasBarChartData {NumberOfCompanies = 7, Country = "Brazil"},
					new CanvasBarChartData {NumberOfCompanies = 7, Country = "India"},
					new CanvasBarChartData {NumberOfCompanies = 9, Country = "Italy" },
					new CanvasBarChartData {NumberOfCompanies = 8, Country = "Australia"},
					new CanvasBarChartData {NumberOfCompanies = 11, Country = "Canada"},
					new CanvasBarChartData {NumberOfCompanies = 15, Country = "South Korea"},
					new CanvasBarChartData {NumberOfCompanies = 12, Country = "Netherlands"},
					new CanvasBarChartData {NumberOfCompanies = 15, Country = "Switzerland"},
					new CanvasBarChartData {NumberOfCompanies = 25, Country = "Britain"},
					new CanvasBarChartData { NumberOfCompanies = 28, Country = "Germany"},
					new CanvasBarChartData { NumberOfCompanies = 29, Country = "France"},
					new CanvasBarChartData{NumberOfCompanies = 52, Country = "Japan"},
					new CanvasBarChartData { NumberOfCompanies = 103, Country = "China"},
					new CanvasBarChartData{NumberOfCompanies = 134, Country = "US" }
				}
			};
            return View(model);
        }
    }
}
