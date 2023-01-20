using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductCatalogAPI.Business.Services;
using ProductCatalogAPI.Controllers;
using ProductCatalogAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogAPI.Tests
{
    public class ProductsControllerTests
    {
        [Fact]
        public async Task GetProducts_ReturnsOkObjectResult_WithExpectedProducts()
        {
            // Arrange
            var mockProductService = new Mock<IProductService>();
            var expectedProducts = new List<Product> { new Product { Id = 1, Name = "Test Product 1" }, new Product { Id = 2, Name = "Test Product 2" } };
            mockProductService.Setup(x => x.GetAllAsync()).ReturnsAsync(expectedProducts);
            var productsController = new ProductsController(mockProductService.Object);

            // Act
            var result = await productsController.GetProducts();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Product>>>(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnedProducts = Assert.IsType<List<Product>>(okObjectResult.Value);
            Assert.Equal(expectedProducts, returnedProducts);
        }
        [Fact]
        public async Task GetProduct_ReturnsOkObjectResult_WithExpectedProduct()
        {
            // Arrange
            var mockProductService = new Mock<IProductService>();
            var expectedProduct = new Product { Id = 1, Name = "Test Product" };
            mockProductService.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(expectedProduct);
            var productsController = new ProductsController(mockProductService.Object);

            // Act
            var result = await productsController.GetProduct(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Product>>(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnedProduct = Assert.IsType<Product>(okObjectResult.Value);
            Assert.Equal(expectedProduct, returnedProduct);
        }
        [Fact]
        public async Task GetProduct_ReturnsNotFoundResult_WhenProductNotFound()
        {
            // Arrange
            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((Product)null);
            var productsController = new ProductsController(mockProductService.Object);

            // Act
            var result = await productsController.GetProduct(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Product>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
        [Fact]
        public async Task PostProduct_ReturnsCreatedAtActionResult_WithExpectedProduct()
        {
            //Arrange
            var mockProductService  = new Mock<IProductService>();
            var expectedProduct = new Product { Id = 1, Name = "Test Product" };
            mockProductService.Setup(x => x.AddAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);
            mockProductService.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(expectedProduct);
            var productsController = new ProductsController(mockProductService.Object);

            var result = await productsController.PostProduct(expectedProduct);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnedProduct = Assert.IsType<Product>(createdAtActionResult.Value);
            Assert.Equal(expectedProduct, returnedProduct);
            Assert.Equal("GetProduct", createdAtActionResult.ActionName);
            Assert.Equal(1, createdAtActionResult.RouteValues["id"]);
        }
        [Fact]
        public async Task PostProduct_ReturnsBadRequestObjectResult_WhenModelStateIsInvalid()
        {
            // Arrange
            var mockProductService = new Mock<IProductService>();
            var productsController = new ProductsController(mockProductService.Object);
            productsController.ModelState.AddModelError("Name", "Required");

            // Act
            var result = await productsController.PostProduct(new Product());

            // Assert
            var actionResult = Assert.IsType<ActionResult<Product>>(result);
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var serializableError = Assert.IsType<SerializableError>(badRequestObjectResult.Value);
            Assert.Contains("Required", (serializableError["Name"] as string[]));


        }

        [Fact]
        public async Task PutProduct_ReturnsNoContentResult_WhenProductUpdated()
        {
            // Arrange
            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(x => x.ProductExistAsync(1)).ReturnsAsync(true);
            mockProductService.Setup(x => x.UpdateAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);
            var productsController = new ProductsController(mockProductService.Object);

            // Act
            var result = await productsController.PutProduct(1, new Product());

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public async Task PutProduct_ReturnsBadRequestResult_WhenIdsDoNotMatch()
        {
            // Arrange
            var mockProductService = new Mock<IProductService>();
            var productsController = new ProductsController(mockProductService.Object);

            // Act
            var result = await productsController.PutProduct(1, new Product { Id = 2 });

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async Task PutProduct_ReturnsBadRequestObjectResult_WhenModelStateIsInvalid()
        {
            // Arrange
            var mockProductService = new Mock<IProductService>();
            var productsController = new ProductsController(mockProductService.Object);
            productsController.ModelState.AddModelError("Name", "Required");

            // Act
            var result = await productsController.PutProduct(1, new Product());
            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);

            Assert.IsType<SerializableError>(badRequestObjectResult.Value);
            Assert.Equal(productsController.ModelState.IsValid, badRequestObjectResult.Value);
           

        }


        [Fact]
        public async Task PutProduct_ReturnsNotFoundResult_WhenProductNotFound()
        {
            // Arrange
            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(x => x.ProductExistAsync(1)).ReturnsAsync(false);
            var productsController = new ProductsController(mockProductService.Object);

            // Act
            var result = await productsController.PutProduct(1, new Product());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        
        public async Task DeleteProduct_ReturnsNoContentResult_WhenProductDeleted()
        {
            // Arrange
            var mockProductService = new Mock<IProductService>();
            var product = new Product { Id = 1, Name = "Test Product" };
            mockProductService.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(product);
            mockProductService.Setup(x => x.DeleteAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);
            var productsController = new ProductsController(mockProductService.Object);

            // Act
            var result = await productsController.DeleteProduct(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }


        [Fact]
        public async Task DeleteProduct_ReturnsNotFoundResult_WhenProductNotFound()
        {
            // Arrange
            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((Product)null);
            var productsController = new ProductsController(mockProductService.Object);

            // Act
            var result = await productsController.DeleteProduct(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }


}
