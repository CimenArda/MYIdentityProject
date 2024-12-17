using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.ViewComponents.EmailViewComponents
{
    public class _EmailMiniMenuComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }

    }
}
