using Microsoft.AspNetCore.Mvc;

namespace No._18.Controllers
{
    public class WorkController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
