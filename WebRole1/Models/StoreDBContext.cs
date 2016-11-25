using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebRole1.Models
{    
    public class StoreDBContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StoreDBContext"/> class.
        /// </summary>
        public StoreDBContext() : 
            base("DefaultConnection")
        {
        }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public virtual DbSet<Product> Products { get; set; }
        
        /// <summary>
        /// Gets or sets the product reviews.
        /// </summary>
        /// <value>
        /// The product reviews.
        /// </value>
        public virtual DbSet<ProductReview> ProductReviews { get; set; }        
    }
}