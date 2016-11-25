using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebRole1.Models;

namespace WebRole1.Controllers
{
    /// <summary>
    /// Products Controller
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [RoutePrefix("store/products")]
    public class ProductsController : ApiController
    {
        /// <summary>
        /// The reviewes repo
        /// </summary>
        private IProductsRepository productsRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReviewsController"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public ProductsController(IProductsRepository repository)
        {
            this.productsRepo = repository;
        }

        /// <summary>
        /// Get the list of products in the store
        /// </summary>
        /// <returns>List of Products</returns>
        [Route("")]
        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            var result = this.productsRepo.ListProducts();
            this.ExamineRepositoryResult(result.Result);
            return result.Products;
        }

        /// <summary>
        /// Add a product
        /// </summary>
        /// <param name="value">The value.</param>
        [Route("")]
        [HttpPost]
        public void AddProduct(ProductEntry value)
        {
            this.ExamineRepositoryResult(this.productsRepo.AddProduct(value));
        }

        /// <summary>
        /// Update a product.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        [Route("{id:int}")]
        [HttpPut]
        public void UpdateProduct(int id, ProductEntry value)
        {
            this.ExamineRepositoryResult(this.productsRepo.UpdateProduct(id, value));
        }

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="id">The identifier.</param>
        [Route("{id:int}")]
        [HttpDelete]
        public void DeleteProduct(int id)
        {
            this.ExamineRepositoryResult(this.productsRepo.DeleteProduct(id));
        }
    }
}
