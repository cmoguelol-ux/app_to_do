// Hecho por Carlos Arturo Moguel Olvera

using app_to_do.Services;
using Microsoft.AspNetCore.Mvc;

namespace app_to_do.Controllers
{
    public class DogController : Controller
    {
        private readonly DogWcfClientService _dogService;

        public DogController(DogWcfClientService dogService)
        {
            _dogService = dogService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _dogService.ObtenerPerritoDelDia();

            return View(model);
        }
    }
}