using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Board.Core;

namespace Board.Controllers
{
    public class RoleController : Controller
    {
        [Authorize(Roles = $"{Constants.Roles.Administrator}")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
