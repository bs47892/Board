using Board.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Board.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Context _context;

        public HomeController(ILogger<HomeController> logger, Context context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        // POST: Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User newUser)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(user => user.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email", "Email is already in use!");
                    return View(nameof(Index));
                }

                PasswordHasher<User> hasher = new PasswordHasher<User>();
                newUser.Password = hasher.HashPassword(newUser, newUser.Password);

                _context.Add(newUser);
                await _context.SaveChangesAsync();

                TempData["Message"] = "You are registred successfuly";

                return RedirectToAction(nameof(Index));

            }
            return View(nameof(Index));
        }

        // POST: Login
        [HttpPost("Login")]
        public IActionResult Login(LoginUser userSubmission)
        {
            //if (ModelState.IsValid)
            //{
                var userInDb = _context.Users.FirstOrDefault(user => user.Email == userSubmission.UserEmail);

                if (userInDb == null)
                {
                    ModelState.AddModelError("UserEmail", "Invalid Email/Password");
                    return View(nameof(Index));
                }

                var hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.UserPassword);

                if (result == 0)
                {
                    ModelState.AddModelError("UserPassword", "Invalid Email/Password");
                    return View(nameof(Index));
                }

                HttpContext.Session.SetInt32("UserId", userInDb.UserId);

                return RedirectToAction(nameof(Index), "Workspaces");
           // }
            return View(nameof(Index));
        }

        // GET: Logout
        [HttpGet("Logout")]
        public RedirectToActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}






