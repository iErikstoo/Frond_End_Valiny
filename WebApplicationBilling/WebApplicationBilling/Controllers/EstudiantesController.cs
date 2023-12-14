using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Core;
using WebApplicationBilling.Models.DTO;
using WebApplicationBilling.Repository;
using WebApplicationBilling.Repository.Interfaces;
using WebApplicationBilling.Utilities;

namespace WebApplicationBilling.Controllers
{
    public class EstudiantesController : Controller
    {
        private readonly IEstudiantesRepository _EstudianteRepository;


        public EstudiantesController(IEstudiantesRepository estudianteRepository)
        {
                this._EstudianteRepository = estudianteRepository;
        }

        [HttpGet]
        // GET: EstudiantesController
        public ActionResult Index()
        {
            return View(new EstudianteDTO() { });
        }

        
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                //Llama al repositorio
                var data = await _EstudianteRepository.GetAllAsync(UrlResources.UrlBase + UrlResources.UrlEstudiantes);
                return Json(new { data });
            }
            catch (Exception ex)
            {
                // Log the exception, handle it, or return an error message as needed
                return StatusCode(500, "Internal Server Error. Please try again later.");
            }
        }

        // GET: EstudiantesController/Details/5
        public ActionResult Details(int id) //Pendiente. Reto para el aprendiz
        {
            return View();
        }

        // GET: EstudiantesController/Create
        //Renderiza la vista
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstudiantesController/Create
        //Captura los datos y los lleva hacia el endpointpasando por el repositorio --> Nube--> DB
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EstudianteDTO estudiante)
        {
            try
            {
                await _EstudianteRepository.PostAsync(UrlResources.UrlBase + UrlResources.UrlEstudiantes, estudiante);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EstudiantesController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            var estudiante = new EstudianteDTO();

            estudiante = await _EstudianteRepository.GetByIdAsync(UrlResources.UrlBase + UrlResources.UrlEstudiantes, id.GetValueOrDefault());
            if (estudiante == null)
            {
                return NotFound();
            }
            return View(estudiante);
        }

        // POST: EstudiantesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EstudianteDTO estudiante)
        {
            if (ModelState.IsValid)
            {
                await _EstudianteRepository.UpdateAsync(UrlResources.UrlBase + UrlResources.UrlEstudiantes + estudiante.EstudianteId, estudiante);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

       
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var estudiante = await _EstudianteRepository.GetByIdAsync(UrlResources.UrlBase + UrlResources.UrlEstudiantes, id);
            if (estudiante == null)
            {
                return Json(new { success = false, message = "Estudiante no encontrado." });
            }

            var deleteResult = await _EstudianteRepository.DeleteAsync(UrlResources.UrlBase + UrlResources.UrlEstudiantes, id);
            if (deleteResult)
            {
                return Json(new { success = true, message = "Estudiante eliminado correctamente." });
            }
            else
            {
                return Json(new { success = false, message = "Error al eliminar el Estudiante." });
            }
        }


    }
}
