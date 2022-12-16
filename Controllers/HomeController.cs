using Microsoft.AspNetCore.Mvc;
using ProyectoPortafolio.Models;
using ProyectoPortafolio.Servicios;
using System.Diagnostics;

namespace ProyectoPortafolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IservicioEmail servicioEmail;

        public HomeController(ILogger<HomeController> logger, IservicioEmail servicioEmail )
           
        {
            _logger = logger;
            this.servicioEmail = servicioEmail;
        }

        public IActionResult Index()
        {
            //Bloque de informacion
            var p = new Persona()
            {
                Nombre = "Jose Emerson Alvarado Gómez",
                Edad = 30,
                Profesion = "Lic. en Computacion",
                Lugaruni = "Unab"
            };
            return View(p);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Contacto()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Contacto(Contacto contacto)
        {
            await servicioEmail.Enviar(contacto);
            return RedirectToAction("Gracias");
        }



        public IActionResult Gracias()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}