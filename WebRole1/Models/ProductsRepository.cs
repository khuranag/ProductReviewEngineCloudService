using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebRole1.Models
{
    /// <summary>
    /// Products Repository
    /// </summary>
    /// <seealso cref="WebRole1.Models.IProductsRepository" />
    public class ProductsRepository : IProductsRepository
    {
        /// <summary>
        /// The database context
        /// </summary>
        private StoreDBContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsReviewsRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ProductsRepository(StoreDBContext context)
        {
            this.dbContext = context;
        }

        /// <summary>
        /// Adds the product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Result AddProduct(ProductEntry product)
        {
            var result = new Result(Status.Success);
            try
            {
                dbContext.Products.Add(
                    new Product
                    {
                        Name = product.Name,
                        Sku = product.Sku,
                        Price = product.Price,
                        Description = product.Description
                    });
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                result.ErrorReason = ex.Message;
                result.Status = Status.InternalError;
            }

            return result;
        }

        /// <summary>
        /// Deletes the product.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Result DeleteProduct(int id)
        {
            var result = new Result(Status.Success);

            try
            {
                var storedEntry = dbContext.Products.Where(x => x.Id == id).FirstOrDefault();
                if (storedEntry == null)
                {
                    result.Status = Status.Failure;
                    result.ErrorReason = string.Format("ID: {0} doesn't exist", id);
                    return result;
                }

                dbContext.Entry(storedEntry).State = System.Data.Entity.EntityState.Deleted;
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                result.ErrorReason = ex.Message;
                result.Status = Status.InternalError;
            }

            return result;
        }

        /// <summary>
        /// Gets the products.
        /// </summary>
        /// <returns>Products result</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public ProductsResult ListProducts()
        {
            var result = new ProductsResult();
            result.Result = new Result(Status.Success);
            try
            {
                result.Products = dbContext.Products;
            }
            catch (Exception ex)
            {
                result.Result.ErrorReason = ex.Message;
                result.Result.Status = Status.InternalError;
            }

            return result;
        }

        /// <summary>
        /// Updates the product.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entry">The entry.</param>
        /// <returns>result</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Result UpdateProduct(int id, ProductEntry entry)
        {
            var result = new Result(Status.Success);

            try
            {
                var storedEntry = dbContext.Products.Where(x => x.Id == id).FirstOrDefault();
                if (storedEntry == null)
                {
                    result.Status = Status.Failure;
                    result.ErrorReason = string.Format("ID not found : {0}", id);
                    return result;
                }

                storedEntry.Name = entry.Name;
                storedEntry.Price = entry.Price;
                storedEntry.Sku = entry.Sku;
                storedEntry.Description = entry.Description;

                dbContext.Entry(storedEntry).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                result.ErrorReason = ex.Message;
                result.Status = Status.InternalError;
            }

            return result;
        }
    }
}