using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductCatalogAPI.Data.Repositories;
using ProductCatalogAPI.Models;

namespace ProductCatalogAPI.Business.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;  
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _productRepository.GetAllAsnyc();
        }
        public async Task<Product?> GetProductByIdAsync(int? id)
        {
            return await _productRepository.GetByIdAsync(id);
        }
        public async Task AddProductAsync(Product product)
        {
            await _productRepository.AddAsync(product);
        }
        public async Task UpdateProductAsync(Product product)
        {
            await _productRepository.UpdateAsync(product);
        }
        public async Task DeleteProductAsync(Product product)
        {
            await _productRepository.DeleteAsync(product);
        }
        //productExists
        public async Task<bool> ProductExistsAsync(int? id)
        {
            return await _productRepository.GetByIdAsync(id) != null;
        }
       
    }
}