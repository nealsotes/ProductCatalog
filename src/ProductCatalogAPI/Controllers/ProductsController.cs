using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogAPI.Business.Services;
using ProductCatalogAPI.Models;

namespace ProductCatalogAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {

        private readonly IProductService _productService;
        public ProductsController(IProductService productservice)
        {
            _productService = productservice;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {

            var products = await _productService.GetAllAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {


            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {

                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
               
                return BadRequest(ModelState);
            }
            var sanitizedProduct = new Product
            {
                Name = HtmlEncoder.Default.Encode(product.Name),
                Description = HtmlEncoder.Default.Encode(product.Description),
                Price = product.Price,
                ImageUrl = HtmlEncoder.Default.Encode(product.ImageUrl)
            };
            await _productService.AddAsync(sanitizedProduct);
            return CreatedAtAction(nameof(GetProduct), new { id = sanitizedProduct.Id }, sanitizedProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
         
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var sanitizedProduct = new Product
            {
                Id = product.Id,
                Name = HtmlEncoder.Default.Encode(product.Name),
                Description = HtmlEncoder.Default.Encode(product.Description),
                Price = product.Price,
                ImageUrl = HtmlEncoder.Default.Encode(product.ImageUrl)

            };
            try
            {
                //check if the product exist
                if (!await _productService.ProductExistAsync(id))
                {
                    return NotFound();
                }
                await _productService.UpdateAsync(sanitizedProduct);
            }
            catch (Exception)
            {
                throw;
            }
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productService.DeleteAsync(product);
            return NoContent();
        }
      
    }
}