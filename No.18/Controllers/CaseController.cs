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
                return RedirectToAction(nameof(Index), new { id = model.Id}); // 新增成功回到列表頁
            }
            return View(model);
        }

        // GET: /Case/Index
        public async Task<IActionResult> Index(int? id)
        {
            // 建立 ViewModel 實體
            var viewModel = new CaseIndexViewModel
            {
                // 取得所有案件列表，用於左側選單
                AllCases = await _context.Cases.ToListAsync()
            };

            // 如果網址中提供了 id (例如 /Case/Index/5)
            if (id != null)
            {
                // 從所有案件中找出對應 ID 的案件，並存入 ViewModel
                viewModel.SelectedCase = viewModel.AllCases.FirstOrDefault(c => c.Id == id);
            }

            return View(viewModel);
        }
    }
}
