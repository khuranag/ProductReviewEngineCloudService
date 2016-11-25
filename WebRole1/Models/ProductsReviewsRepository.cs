using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace WebRole1.Models
{
    /// <summary>
    /// Products Reviews Repository
    /// </summary>
    /// <seealso cref="WebRole1.Models.IProductsReviewsRepository" />
    public class ProductsReviewsRepository : IProductsReviewsRepository
    {
        /// <summary>
        /// The database context
        /// </summary>
        private StoreDBContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsReviewsRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ProductsReviewsRepository(StoreDBContext context)
        {
            this.dbContext = context;
        }

        /// <summary>
        /// Submits the review.
        /// </summary>
        /// <param name="entry">The entry.</param>
        public Result SubmitReview(ProductReviewEntry entry)
        {
            var result = StandardSubmitReviewRequestValidation(entry);

            if (result != null)
            {
                return result;
            }

            result = new Result(Status.Success);
            try
            {
                if (dbContext.Products.Where(x => x.Name == entry.ProductName).FirstOrDefault() == null)
                {
                    result.Status = Status.Failure;
                    result.ErrorReason = string.Format("Product Name: {0} doesn't exist", entry.ProductName);
                    return result;
                }

                dbContext.ProductReviews.Add(
                    new ProductReview
                    {
                        ProductName = entry.ProductName,
                        UserName = entry.UserName,
                        Score = entry.Score,
                        Comment = entry.Comment
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
        /// Gets the reviews for a product.
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        public ProductReviewsResult ReviewsPerProduct(string productName)
        {
            var result = new ProductReviewsResult()
            {
                RepositoryResult = new Result(Status.Success)
            };

            if (string.IsNullOrWhiteSpace(productName) || dbContext.Products.Where(x => x.Name == productName).FirstOrDefault() == null)
            {
                result.RepositoryResult.Status = Status.Failure;
                result.RepositoryResult.ErrorReason = string.Format("Product Name: {0} doesn't exist", productName);
                return result;
            }

            result.ReviewsResult = new AggregatedReviewsResult();

            try
            {
                result.ReviewsResult.Reviews = dbContext.ProductReviews.Where(x => x.ProductName == productName);

                if (result.ReviewsResult.Reviews != null && result.ReviewsResult.Reviews.Count() > 0)
                {
                    result.ReviewsResult.AggregatedScore = result.ReviewsResult.Reviews.Select(x => x.Score).Average();
                }
            }
            catch (Exception ex)
            {
                result.RepositoryResult.ErrorReason = ex.Message;
                result.RepositoryResult.Status = Status.InternalError;
            }

            return result;
        }

        /// <summary>
        /// Updates the review.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entry">The entry.</param>
        /// <returns></returns>
        public Result UpdateReview(int id, ProductReviewEntry entry)
        {
            var result = StandardSubmitReviewRequestValidation(entry);

            if (result != null)
            {
                return result;
            }

            result = new Result(Status.Success);

            try
            {
                var storedEntry = dbContext.ProductReviews.Where(x => x.Id == id).FirstOrDefault();
                if (storedEntry == null)
                {
                    result.Status = Status.Failure;
                    result.ErrorReason = string.Format("ID not found : {0}", id);
                    return result;
                }

                storedEntry.Comment = entry.Comment;
                storedEntry.ProductName = entry.ProductName;
                storedEntry.Score = entry.Score;
                storedEntry.UserName = entry.UserName;

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

        /// <summary>
        /// Deletes the review.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Result DeleteReview(int id)
        {
            var result = new Result(Status.Success);

            try
            {
                var storedEntry = dbContext.ProductReviews.Where(x => x.Id == id).FirstOrDefault();
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
        /// Standards the submit review request validation.
        /// </summary>
        /// <param name="value">The value.</param>
        private Result StandardSubmitReviewRequestValidation(ProductReviewEntry value)
        {
            string message = string.Empty;
            Result result = null;

            if (string.IsNullOrWhiteSpace(value.UserName))
            {
                message = "UserName can't be empty";
            }

            if (string.IsNullOrWhiteSpace(value.ProductName))
            {
                message = "ProductName can't be empty";
            }

            if (value.Score < 0 || value.Score > 5)
            {
                message = "Review score must be between 0 & 5";
            }

            if (value.Comment.Length > 1024)
            {
                message = "Comment should be less 1024 characters";
            }

            if (!string.IsNullOrWhiteSpace(message))
            {
                result = new Result(Status.Failure)
                {
                    ErrorReason = message
                };
            }

            return result;
        }
    }
}