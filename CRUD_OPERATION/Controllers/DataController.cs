using Microsoft.AspNetCore.Mvc;
using CRUD_OPERATION.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_OPERATION.Controllers
{
    public class DataController : Controller
    {
        private readonly CRUD_OPERATION.Models.AppContext _context;

        private readonly IWebHostEnvironment _env;

        public DataController(CRUD_OPERATION.Models.AppContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        //comit
        public async Task<IActionResult> Index()
        {
            var students = await _context.students.ToListAsync();

            // Check if data exists
            if (students == null || students.Count == 0) 
            {
                return View("Error"); // If no data, redirect to an error page
            }

            ViewData["result"] = students; // Store in ViewData
            return View(students);
        }


        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student std, IFormFile file)
        {

            var path = Path.Combine(_env.WebRootPath, "Uploads");
            var name = Path.GetFileName(file.FileName);
            var filepath = Path.Combine(path, name);

            using (var filestram = new FileStream(filepath, FileMode.Create))
            {
               await file.CopyToAsync(filestram);
            }

            std.Image = $@"\Uploads\{name}";

           

            _context.students.Add(std);
            _context.SaveChanges();

            return View();
        }


    }
}
