using Microsoft.AspNetCore.Mvc;

namespace JNet.Vms.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Redirect("/f");
        }
    }
}
