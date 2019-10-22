using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportsStore.Models.Domain;
using SportsStore.Models.ProductViewModels;
using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Controllers {
    public class ProductController : Controller {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository) {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index() {
            IEnumerable<Product> products = _productRepository.GetAll().OrderBy(b => b.Name).ToList();
            return View(products);
        }

        public IActionResult Edit(int id) {
            Product product = _productRepository.GetById(id);
            if (product == null)
                return NotFound();
            ViewData["Categories"] = GetCategoriesSelectList();
            return View(new EditViewModel(product));
        }

        [HttpPost]
        public IActionResult Edit(int id, EditViewModel editViewModel) {
            Product product = _productRepository.GetById(id);
            product.EditProduct(editViewModel.Name, editViewModel.Description, editViewModel.Price, editViewModel.InStock, _categoryRepository.GetById(editViewModel.CategoryId));
            _productRepository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private SelectList GetCategoriesSelectList(int selected = 0) {
            return new SelectList(_categoryRepository.GetAll().OrderBy(g => g.Name).ToList(),
                nameof(Category.CategoryId), nameof(Category.Name), selected);
        }

    }
}
