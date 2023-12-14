using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using NuGet.Packaging.Core;
using WebApplicationBilling.Models.DTO;
using WebApplicationBilling.Repository;
using WebApplicationBilling.Repository.Interfaces;
using WebApplicationBilling.Utilities;

namespace WebApplicationBilling.Controllers
{
    public class RegistrosController : Controller
    {
        private readonly IRegistrosRepository _RegistroRepository;


        public RegistrosController(IRegistrosRepository registroRepository)
        {
            this._RegistroRepository = registroRepository;
        }

        [HttpGet]
        // GET: RegistrosController
        public ActionResult Index()
        {
            return View(new RegistroDTO() { });
        }


        public async Task<IActionResult> GetAllRegistros()
        {
            try
            {
                //Llama al repositorio
                var data = await _RegistroRepository.GetAllAsync(UrlResources.UrlBase + UrlResources.UrlRegistros);
                return Json(new { data });
            }
            catch (Exception ex)
            {
                // Log the exception, handle it, or return an error message as needed
                return StatusCode(500, "Internal Server Error. Please try again later.");
            }
        }

        // GET: RegistrosController/Details/5
        public ActionResult Details(int id) //Pendiente. Reto para el aprendiz
        {
            return View();
        }

        // GET: RegistrosController/Create
        //Renderiza la vista
        public ActionResult Create()
        {
            return View();
        }

        // POST:RegistrosController/Create
        //Captura los datos y los lleva hacia el endpointpasando por el repositorio --> Nube--> DB
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegistroDTO registro)
        {
            try
            {
                await _RegistroRepository.PostAsync(UrlResources.UrlBase + registro.RegistroId, registro);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RegistrosController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            var registro = new RegistroDTO();

            registro = await _RegistroRepository.GetByIdAsync(UrlResources.UrlBase + UrlResources.UrlRegistros, id.GetValueOrDefault());
            if (registro == null)
            {
                return NotFound();
            }
            return View(registro);
        }

        // POST: RegistrosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RegistroDTO registro)
        {
            if (ModelState.IsValid)
            {
                await _RegistroRepository.UpdateAsync(UrlResources.UrlBase + UrlResources.UrlRegistros + registro.RegistroId, registro);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var registro = await _RegistroRepository.GetByIdAsync(UrlResources.UrlBase + UrlResources.UrlRegistros, id);
            if (registro == null)
            {
                return Json(new { success = false, message = "Registro no encontrado." });
            }

            var deleteResult = await _RegistroRepository.DeleteAsync(UrlResources.UrlBase + UrlResources.UrlRegistros, id);
            if (deleteResult)
            {
                return Json(new { success = true, message = "Registro eliminado correctamente." });
            }
            else
            {
                return Json(new { success = false, message = "Error al eliminar el Registro." });
            }
        }


    }
}
