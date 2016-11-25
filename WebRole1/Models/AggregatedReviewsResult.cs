using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebRole1.Models
{
    /// <summary>
    /// Reviews Result Container
    /// </summary>
    public class AggregatedReviewsResult
    {
        /// <summary>
        /// Gets or sets the aggregated score.
        /// </summary>
        /// <value>
        /// The aggregated score.
        /// </value>
        public double AggregatedScore { get; set; }

        /// <summary>
        /// The reviews
        /// </summary>
        public IEnumerable<ProductReview> Reviews { get; set; }
    }
}