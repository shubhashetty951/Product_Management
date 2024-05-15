using Microsoft.AspNetCore.Mvc;
using Simple_Project.Models; 

namespace Simple_Project
{
    public class ProductsController : Controller
    {
        private readonly DapperRepository _repository;

        public ProductsController(DapperRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var products = _repository.GetProducts();
            return View(products); 
        }

        // Create a new product
        public IActionResult Create()
        {
            return View(); 
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid) 
            {
                _repository.AddProduct(product);
                return RedirectToAction("Index"); 
            }

            return View(product); 
        }

        // Edit an existing product
        public IActionResult Edit(int id)
        {
            var product = _repository.GetProduct(id);
            if (product == null)
            {
                return NotFound(); 
            }
            return View(product); 
        }

        [HttpPost]
        public IActionResult Edit(int id, Product product)
        {
            if (ModelState.IsValid) 
            {
                if (id != product.Id) 
                {
                    return BadRequest(); 
                }
                _repository.UpdateProduct(product);
                return RedirectToAction(nameof(Index)); 
            }
            return View(product); 
        }

        
        public IActionResult Delete(int id)
        {
            var product = _repository.GetProduct(id);
            if (product == null)
            {
                return NotFound(); 
            }
            return View(product); 
        }

        
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _repository.GetProduct(id);
            if (product == null)
            {
                return NotFound(); 
            }

            _repository.DeleteProduct(id);
            return RedirectToAction(nameof(Index)); 
        }
    }
}
