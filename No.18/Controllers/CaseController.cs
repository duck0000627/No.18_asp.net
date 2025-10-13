using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using No._18.Models;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Create(CaseModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Cases.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // 新增成功回到列表頁
            }
            return View(model);
        }

        // GET: /Case/Index
        public async Task<IActionResult> Index()
        {
            var caseList = await _context.Cases.ToListAsync();

            return View(caseList);
        }
    }
}
