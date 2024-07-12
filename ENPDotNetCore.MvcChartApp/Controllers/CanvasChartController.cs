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
            return View();
        }

        public IActionResult BarChart()
        {
            return View();
        }
    }
}
