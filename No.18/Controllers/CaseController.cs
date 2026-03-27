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
                // 新案件預設為 Received 狀態
                model.Status = CaseStatus.Received;
                _context.Cases.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = model.Id}); // 新增成功回到列表頁
            }
            return View(model);
        }

        // GET: /Case/Index
        public async Task<IActionResult> Index(int? id, string searchString)
        {
            var casesQuery = _context.Cases.AsQueryable();

            // 3. 如果 searchString 有值，就加入 Where 篩選條件
            if (!string.IsNullOrEmpty(searchString))
            {
                casesQuery = casesQuery.Where(c =>
                    c.CaseNumber.Contains(searchString) ||
                    c.CompanyName.Contains(searchString) ||
                    c.ResponsiblePerson.Contains(searchString)
                );
            }

            // 建立 ViewModel 實體
            var viewModel = new CaseIndexViewModel
            {
                // 5. 執行最終的查詢 (ToList)，將篩選後的結果放入 AllCases
                AllCases = await casesQuery.OrderBy(c => c.Id).ToListAsync(),

                // 6. 將搜尋關鍵字存入 ViewModel，以便回傳給 View 顯示
                SearchString = searchString
            };

            // 如果網址中提供了 id (例如 /Case/Index/5)
            if (id != null)
            {
                // 從所有案件中找出對應 ID 的案件，並存入 ViewModel
                viewModel.SelectedCase = viewModel.AllCases.FirstOrDefault(c => c.Id == id);
            }
            else if (viewModel.AllCases.Any() && id == null)
            {
                // 如果是搜尋後第一次載入，自動選中第一筆
                viewModel.SelectedCase = viewModel.AllCases.FirstOrDefault();
            }

            return View(viewModel);
        }

        // POST: /Case/UpdateStatus
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int caseId, CaseStatus newStatus)
        {
            var caseToUpdate = await _context.Cases.FindAsync(caseId);
            if (caseToUpdate != null)
            {
                caseToUpdate.Status = newStatus;
                await _context.SaveChangesAsync();
            }
            // 更新完成後，重新導向回 Index 頁面，並顯示剛剛更新的那個案件
            return RedirectToAction(nameof(Index), new { id = caseId });
        }

        // GET: /Case/Edit/5 (載入修改畫面)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var caseModel = await _context.Cases.FindAsync(id);
            if (caseModel == null) return NotFound();

            return View(caseModel);
        }

        // POST: /Case/Edit/5 (接收並儲存修改後的資料)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CaseModel model)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Cases.Any(e => e.Id == model.Id))
                        return NotFound();
                    else
                        throw;
                }
                // 修改成功後，回到首頁並查看該案件
                return RedirectToAction(nameof(Index), new { id = model.Id });
            }
            return View(model);
        }

        // POST: /Case/Delete/5 (處理刪除請求)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var caseModel = await _context.Cases.FindAsync(id);
            if (caseModel != null)
            {
                _context.Cases.Remove(caseModel);
                await _context.SaveChangesAsync();
            }
            // 刪除成功後，回到首頁
            return RedirectToAction(nameof(Index));
        }
    }
}
