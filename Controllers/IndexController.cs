using Microsoft.AspNetCore.Mvc;

namespace ConsumeOMS.Controllers
{
    public class IndexController : Controller
    {
        public async Task<IActionResult> IndexPage()
        {
            return View();
        }
    }
}
