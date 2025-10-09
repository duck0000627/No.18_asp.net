using Microsoft.AspNetCore.Mvc;
using No._18.Models;

namespace No._18.Controllers
{
    public class CaseController : Controller
    {
        private readonly AppDbContext _context;

        public CaseController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Case/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Case/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CaseModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Cases.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index"); // 新增成功回到列表頁
            }
            return View(model);
        }

        // GET: /Case/Index
        public IActionResult Index()
        {
            return View();
        }
    }
}
