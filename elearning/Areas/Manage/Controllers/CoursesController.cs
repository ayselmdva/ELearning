using elearning.DAL;
using elearning.Models;
using elearning.Utilites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace elearning.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class CoursesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public CoursesController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Courses.Include(x=>x.Teacher).ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Teachers = await _context.Teachers.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Course course)
        {
            ViewBag.Teachers = await _context.Teachers.ToListAsync();
            if (!ModelState.IsValid) { return View(); }
            if(course == null) { ModelState.AddModelError("", "Error"); return View(); }
            if (!course.ImageFile.CheckFileType("image")) { ModelState.AddModelError("File", "Error"); return View(); }
            if (course.ImageFile.CheckFileSize(20000)) { ModelState.AddModelError("File", "Error"); return View(); }
            string filename = await course.ImageFile.SaveFileAsync(_environment.WebRootPath, "faces");
            course.Image = filename;
            await _context.AddAsync(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Teachers = await _context.Teachers.ToListAsync();
            Course? course=await _context.Courses.FirstOrDefaultAsync(x=>x.Id==id);
            if (course == null) { NotFound(); return View(); }
            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Course course)
        {
            ViewBag.Teachers = await _context.Teachers.ToListAsync();
            Course? exists = await _context.Courses.FirstOrDefaultAsync(x => x.Id == course.Id);
            if (exists == null) { NotFound(); return View(); }
            if (course == null) { NotFound(); return View(); }
            exists.Teacher = course.Teacher;
            exists.Price=course.Price;
            exists.Name=course.Name;
            exists.TeacherId=course.TeacherId;
            if(exists.ImageFile != null)
            {
                if (!course.ImageFile.CheckFileType("image")) { ModelState.AddModelError("File", "Error"); return View(); }
                if (!course.ImageFile.CheckFileSize(2000)) { ModelState.AddModelError("File", "Error"); return View(); }
                string fileName = await course.ImageFile.SaveFileAsync(_environment.WebRootPath, "faces");
                exists.Image=fileName;
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            Course? course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);
            if (course == null) { NotFound(); return View(); }
            course.ImageFile.DeleteFile(_environment.WebRootPath, "faces", course.Image);
            _context.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
