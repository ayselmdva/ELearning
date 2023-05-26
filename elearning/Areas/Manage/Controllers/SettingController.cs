using elearning.DAL;
using elearning.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace elearning.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;

        public SettingController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Settings.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Settings? settings = await _context.Settings.FirstOrDefaultAsync(x => x.Id == id);
            if (settings == null) { NotFound(); return View(); }
            return View(settings);
        }
        
        public async Task<IActionResult> Edit(Settings settings)
        {
            Settings? exists = await _context.Settings.FirstOrDefaultAsync(x => x.Id == settings.Id);
            if (exists == null) { NotFound(); return View(); }
            exists.Value=settings.Value;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
