using Microsoft.AspNetCore.Mvc;

namespace TaskWepApi1.Controllers
{
    public class TaskController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return Ok();
        }
    }
}