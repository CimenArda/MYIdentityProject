using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class MessageLayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
