using System;
using System.Collections.Generic;

namespace WebRole1.Models
{
    /// <summary>
    /// Repository for Products Reviews
    /// </summary>
    public interface IProductsReviewsRepository
    {
        /// <summary>
        /// Submits the review.
        /// </summary>
        /// <param name="entry">The entry.</param>
        Result SubmitReview(ProductReviewEntry entry);

        /// <summary>
        /// Updates the review.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entry">The entry.</param>
        /// <returns></returns>
        Result UpdateReview(int id, ProductReviewEntry entry);

        /// <summary>
        /// Gets the reviews for a product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        ProductReviewsResult ReviewsPerProduct(string productName);

        /// <summary>
        /// Deletes the review.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Result DeleteReview(int id);    
    }
}
