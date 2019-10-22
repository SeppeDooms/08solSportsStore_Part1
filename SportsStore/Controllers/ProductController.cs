using Microsoft.AspNetCore.Mvc;
using SportsStore.Models.Domain;
using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Controllers {
    public class ProductController : Controller {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository) {
            _productRepository = productRepository;
        }

        public IActionResult Index() {
            IEnumerable<Product> products = _productRepository.GetAll().OrderBy(b => b.Name).ToList();
            return View(products);
        }
    }
}
