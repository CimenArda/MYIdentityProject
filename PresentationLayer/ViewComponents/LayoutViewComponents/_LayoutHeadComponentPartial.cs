using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.ViewComponents.LayoutViewComponents
{
    public class _LayoutHeadComponentPartial :ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }

    }
}
