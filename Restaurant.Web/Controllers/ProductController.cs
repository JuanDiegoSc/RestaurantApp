using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Restaurant.Web.Models;
using Restaurant.Web.Service;
using Restaurant.Web.Service.IService;
using System.Collections.Generic;

namespace Restaurant.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _ProductService;
        public ProductController(IProductService productService)
        {
            _ProductService = productService;
        }
        public async Task<IActionResult?> ProductIndex()
        {
            List<ProductDto>? list = new();
            ResponseDto? response = await _ProductService.GetAllProductsAsync();
            
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response.Message;
            }
            return View(list);
        }
        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _ProductService.CreateProductAsync(model);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Product created successfully";
                    return RedirectToAction(nameof(ProductIndex));
                }
                else
                {
                    TempData["error"] = response.Message;
                }
            }
            return View(model);
        }

        public async Task<IActionResult> ProductDelete(int productId)
        {
           ResponseDto? response = await _ProductService.GetProductByIdAsync(productId);

             if (response != null && response.IsSuccess)
             {
                ProductDto? model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                 return View(model);
             }

                return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ProductDelete(ProductDto productDto)
        {
                ResponseDto? response = await _ProductService.DeleteProductAsync(productDto.ProductId);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Product deleted successfully";
                    return RedirectToAction(nameof(ProductIndex));
                }
                else
                {
                TempData["error"] = response.Message;
                }

            return View(productDto);
        }

        public async Task<IActionResult> ProductEdit(int productId)
        {
            ResponseDto? response = await _ProductService.GetProductByIdAsync(productId);

            if (response != null && response.IsSuccess)
            {
                ProductDto? model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ProductEdit(ProductDto productDto)
        {
            ResponseDto? response = await _ProductService.UpdateProductAsync(productDto);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Product updated successfully";
                return RedirectToAction(nameof(ProductIndex));
            }
            else
            {
                TempData["error"] = response.Message;
            }

            return View(productDto);
        }
    }
}
