using Microsoft.AspNetCore.Mvc;

namespace ProjMVCAirportData.Controllers
{
    public class Airport : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
