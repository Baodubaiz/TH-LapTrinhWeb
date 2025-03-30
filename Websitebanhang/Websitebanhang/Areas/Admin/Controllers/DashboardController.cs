using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Websitebanhang.Models;
namespace Websitebanhang.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }

}
