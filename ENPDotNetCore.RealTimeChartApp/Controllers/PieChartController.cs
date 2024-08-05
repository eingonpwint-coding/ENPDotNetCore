using ENPDotNetCore.RealTimeChartApp.Hubs;
using ENPDotNetCore.RealTimeChartApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ENPDotNetCore.RealTimeChartApp.Controllers;

public class PieChartController : Controller
{
    private readonly AppDbContext _context;
    private readonly IHubContext<ChartHub> _hubContext;

    public PieChartController(AppDbContext context, IHubContext<ChartHub> hubContext = null)
    {
        _context = context;
        _hubContext = hubContext;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Create()
    {
        return View();
    }

    public async Task<IActionResult> Save(TblPieChart reqModel)
    {
        await _context.TblPieCharts.AddAsync(reqModel);
        await _context.SaveChangesAsync();

        var lst = await _context.TblPieCharts.AsNoTracking().ToListAsync();

        var data = lst.Select(x => new PieChartDataModel
        {
            name = x.PieChartName,
            y = x.PieChartValue
        }).ToList();

        await _hubContext.Clients.All.SendAsync("ReceivePieChart", data);

        return RedirectToAction("Create");
    }
}

public class PieChartDataModel
{
    public string name { get; set; }
    public decimal y { get; set; }
}
