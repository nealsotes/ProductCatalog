using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductCatalogAPI.Data.Repositories;
using ProductCatalogAPI.Models;

namespace ProductCatalogAPI.Business.Services
{
    
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int? id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Product product);
        Task <bool> ProductExistAsync(int? id);
    }




    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            
        }

        public async Task AddAsync(Product product)
        {
            await _productRepository.AddAsync(product);
        }

        public async Task DeleteAsync(Product product)
        {
            await _productRepository.DeleteAsync(product);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsnyc();
        }

        public async Task<Product?> GetByIdAsync(int? id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Product product)
        {
            await _productRepository.UpdateAsync(product);
        }
        //ProductExistAsync
        public async Task<bool> ProductExistAsync(int? id)
        {
            return await _productRepository.GetByIdAsync(id) != null;
        }

    }
}