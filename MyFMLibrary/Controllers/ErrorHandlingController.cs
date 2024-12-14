using Microsoft.AspNetCore.Mvc;

namespace MyFMLibrary.Controllers
{
    public class ErrorHandlingController : Controller
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/error")]
        public IActionResult HandleError() => Problem();
    }
}
