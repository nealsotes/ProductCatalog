using ProductCatalogAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogAPI.Tests
{
    public class ProductTest : IDisposable
    {
        Product testProduct;
        public ProductTest()
        {
            testProduct = new Product
            {
                Id = 1,
                Name = "Test Product",
                Description = "Test Description",
                Price = 10.00,
                ImageUrl = "https://test.com"
            };

        }
        public void Dispose()
        {
            testProduct = null;
        }
        
        [Fact]
        public void CanChangeName()
        {
            //act
            testProduct.Name = "Test Name";
            //assert
            Assert.Equal("Test Name", testProduct.Name);
        }
        [Fact]
        public void CanChangeDescription()
        {
            testProduct.Description = "Test Description";
            Assert.Equal("Test Description", testProduct.Description);
        }
        [Fact]
        public void CanChangePrice()
        {
            testProduct.Price = 10.00;
            Assert.Equal(10.00, testProduct.Price);
        }
    }
}
