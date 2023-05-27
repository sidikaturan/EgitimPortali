using Microsoft.AspNetCore.Mvc;

namespace Uygulama.ViewComponents.Comment
{
    public class PartialAddComment : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            ViewBag.id = id;
            return View();
        }
    }
}
