using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebRole1.Models
{
    /// <summary>
    /// Class to return final reviews with result status
    /// </summary>
    public class ProductReviewsResult
    {
        /// <summary>
        /// Gets or sets the reviews result.
        /// </summary>
        /// <value>
        /// The reviews result.
        /// </value>
        public AggregatedReviewsResult ReviewsResult { get; set; }

        /// <summary>
        /// Gets or sets the repository result.
        /// </summary>
        /// <value>
        /// The repository result.
        /// </value>
        public Result RepositoryResult { get; set; }
    }
}