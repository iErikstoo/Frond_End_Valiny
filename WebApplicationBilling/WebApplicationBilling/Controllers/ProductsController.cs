using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Core;
using WebApplicationBilling.Models.DTO;
using WebApplicationBilling.Repository.Interfaces;
using WebApplicationBilling.Utilities;

namespace WebApplicationBilling.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;


        public ProductsController(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        [HttpGet]
        // GET: ProductsController
        public ActionResult Index()
        {
            return View(new ProductDTO() { });
        }


        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                //Llama al repositorio
                var data = await _productRepository.GetAllAsync(UrlResources.UrlBase + UrlResources.UrlProducts);
                return Json(new { data });
            }
            catch (Exception ex)
            {
                // Log the exception, handle it, or return an error message as needed
                return StatusCode(500, "Internal Server Error. Please try again later.");
            }
        }

        // GET: CustomersController/Details/5
        public async Task<IActionResult> Details(int id) //Pendiente. Reto para el aprendiz
        {

            var product = await _productRepository.GetByIdAsync(UrlResources.UrlBase + UrlResources.UrlProducts, id);
            if (product == null)
            {
                return Json(new { success = false, message = "Cliente no encontrado." });
            }
            return View(product);
        }

        // GET: CustomersController/Create
        //Renderiza la vista
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomersController/Create
        //Captura los datos y los lleva hacia el endpointpasando por el repositorio --> Nube--> DB
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDTO product)
        {
            try
            {
                await _productRepository.PostAsync(UrlResources.UrlBase + UrlResources.UrlProducts, product);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomersController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            var product = new ProductDTO();

            product = await _productRepository.GetByIdAsync(UrlResources.UrlBase + UrlResources.UrlProducts, id.GetValueOrDefault());
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: CustomersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductDTO product)
        {
            if (ModelState.IsValid)
            {
                await _productRepository.UpdateAsync(UrlResources.UrlBase + UrlResources.UrlProducts + product.id, product);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetByIdAsync(UrlResources.UrlBase + UrlResources.UrlProducts, id);
            if (product == null)
            {
                return Json(new { success = false, message = "Cliente no encontrado." });
            }

            var deleteResult = await _productRepository.DeleteAsync(UrlResources.UrlBase + UrlResources.UrlProducts, id);
            if (deleteResult)
            {
                return Json(new { success = true, message = "Cliente eliminado correctamente." });
            }
            else
            {
                return Json(new { success = false, message = "Error al eliminar el cliente." });
            }
        }


    }
}