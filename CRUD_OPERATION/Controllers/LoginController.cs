using CRUD_OPERATION.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace CRUD_OPERATION.Controllers
{
    public class LoginController : Controller
    {
        private readonly CRUD_OPERATION.Models.AppContext _context;

       

        public LoginController(CRUD_OPERATION.Models.AppContext context)
        {
            _context = context;
          
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee emp)
        {
            var _login =  _context.employees.Where(x => x.Email == emp.Email && x.Password == emp.Password).FirstOrDefault();

            if (_login == null)
            {
                return View("Create"); 
            }
           
            HttpContext.Session.SetInt32("key", _login.Id);

            return RedirectToAction("Profile");
        }

        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public async Task<IActionResult> Profile()
        {
            var _data = HttpContext.Session.GetInt32("key");

            if (_data == null)
            {
                return RedirectToAction("Create"); // Redirect to login if session is null
            }

            var _dtt = _context.employees.Where(x => x.Id == _data).FirstOrDefault();
            return View(_dtt);
        }



        //public async Task<IActionResult> Profile()
        //{
        //   var _data = HttpContext.Session.GetInt32("key");

        //    var _dtt = _context.employees.Where(x => x.Id == _data).FirstOrDefault();
        //    //ViewBag.n = _data;

        //    //if (ViewBag.n == null)
        //    //{
        //    //    return RedirectToAction("Create");
        //    //}
        //    return View(_dtt);
        //}

        //public async Task<IActionResult> Logout()
        //{
        //    HttpContext.Session.Clear(); // Clear session
        //    HttpContext.Response.Cookies.Delete(".AspNetCore.Session"); // Delete session cookie

        //    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        //    Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate";
        //    Response.Headers["Pragma"] = "no-cache";
        //    Response.Headers["Expires"] = "0";

        //    return RedirectToAction("Create");
        //}
        public async Task<IActionResult> Logout()
        {
            // Clear the session data
            HttpContext.Session.Clear();

            // Sign out the user from authentication
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Prevent the browser from caching the page
            Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate"; // Prevent caching
            Response.Headers["Pragma"] = "no-cache"; // For HTTP 1.0
            Response.Headers["Expires"] = "0"; // Expire immediately

            // Redirect to the Login page
            return RedirectToAction("Create");
        }


    }
}
