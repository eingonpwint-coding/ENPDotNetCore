using ENPDotNetCore.MvcChartApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ENPDotNetCore.MvcChartApp.Controllers
{
    public class ApexChartController : Controller
    {
        private readonly ILogger<ApexChartController> _logger;

        public ApexChartController(ILogger<ApexChartController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SimplePieChart()
        {
            _logger.LogInformation("Simple Pie chart");
            PieChartModel model = new PieChartModel();
            model.Series = new List<int> { 44, 55, 13, 43, 22};
            model.Labels = new List<string> { "Team A", "Team B", "Team C", "Team D", "Team E" };
            return View(model);
        }

        public IActionResult SimpleBarChart()
        {
            BarChartModel model = new BarChartModel();
            model.Series = new List<int> { 400, 430, 448, 470, 540, 580, 690, 1100, 1200, 1380 };
            model.Labels = new List<string> { "South Korea", "Canada", "United Kingdom", "Netherlands", "Italy", "France", "Japan", "United States", "China", "India" };
            model.Colors = new List<string> { "#33b2df", "#546E7A", "#d4526e", "#13d8aa", "#A5978B", "#2b908f", "#f9a3a4", "#90ee7e", "#f48024", "#69d2e7" };
            return View(model);
        }

        public IActionResult RadialBarChart()
        {
            var model = new RadialBarChartModel
            {
                Series = new List<int> { 76, 67, 61, 90 },
                Labels = new List<string> { "Vimeo", "Messenger", "Facebook", "LinkedIn" },
                Colors = new List<string> { "#1ab7ea", "#0084ff", "#39539E", "#0077B5" }
            };

            return View(model);
        }

    }
}
