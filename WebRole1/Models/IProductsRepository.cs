using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRole1.Models
{
    /// <summary>
    /// Interface for Products Repository
    /// </summary>
    public interface IProductsRepository
    {
        /// <summary>
        /// Adds the product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        Result AddProduct(ProductEntry product);

        /// <summary>
        /// Gets the products.
        /// </summary>
        /// <returns></returns>
        ProductsResult ListProducts();

        /// <summary>
        /// Updates the product.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entry">The entry.</param>
        /// <returns></returns>
        Result UpdateProduct(int id, ProductEntry entry);

        /// <summary>
        /// Deletes the product.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Result DeleteProduct(int id);
    }
}
