using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebRole1.Models;

namespace WebRole1.Controllers
{
    /// <summary>
    /// Reviews Controller
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />    
    [RoutePrefix("products/reviews")]
    [Authorize]
    public class ReviewsController : ApiController
    {
        /// <summary>
        /// The reviewes repo
        /// </summary>
        private IProductsReviewsRepository reviewesRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReviewsController"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public ReviewsController(IProductsReviewsRepository repository)
        {
            this.reviewesRepo = repository;
        }

        /// <summary>
        /// Lists the reviews for a product
        /// </summary>
        /// <param name="productname">The productname.</param>
        /// <returns>
        /// Aggregated result
        /// </returns>
        [Route("{productname}")]
        [HttpGet]
        public AggregatedReviewsResult GetReviewsPerProduct(string productname)
        {
            var result = this.reviewesRepo.ReviewsPerProduct(productname);
            this.ExamineRepositoryResult(result.RepositoryResult);
            return result.ReviewsResult;
        }

        /// <summary>
        /// Submit a product review
        /// </summary>
        /// <param name="value">The value.</param>        
        [Route("")]
        [HttpPost]
        public void PostProductReviews(ProductReviewEntry value)
        {
            this.ExamineRepositoryResult(this.reviewesRepo.SubmitReview(value));
        }

        /// <summary>
        /// Update a product review
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        [Route("{id:int}")]        
        [HttpPut]
        public void UpdateProductReview(int id, ProductReviewEntry value)
        {
            this.ExamineRepositoryResult(this.reviewesRepo.UpdateReview(id, value));            
        }

        /// <summary>
        /// Delete a  product review
        /// </summary>
        /// <param name="id">The identifier.</param>
        [Route("{id:int}")]
        [HttpDelete]
        public void DeleteProductReview(int id)
        {
            this.ExamineRepositoryResult(this.reviewesRepo.DeleteReview(id));            
        }
    }
}
