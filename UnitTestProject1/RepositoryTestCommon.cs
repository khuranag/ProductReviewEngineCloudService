using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using WebRole1.Models;

namespace UnitTestProject1
{
    /// <summary>
    /// Repository Test base
    /// </summary>
    public class RepositoryTestCommon
    {
        /// <summary>
        /// Sets up the single entry database context.
        /// </summary>
        /// <returns></returns>
        public static Mock<StoreDBContext> SetupSingleEntryDbContext()
        {
            var testProducts = SetupTestProducts(new List<Product>
            {
                new Product {
                    Id = 1,
                    Name ="P1",
                    Description = "Something",
                    Price = 100000,
                    Sku = "A1B2C3"
                }
            });

            var testReviews = SetupTestReviews(new List<ProductReview>
            {
                new ProductReview
                {
                    Id = 1,
                    ProductName = "P1",
                    UserName = "U1",
                    Score = 3,
                    Comment = "Whatever"
                }
            });

            return SetupDbContext(testProducts.Object, testReviews.Object);
        }

        /// <summary>
        /// Setups the multiple entry database context.
        /// </summary>
        /// <returns></returns>
        public static Mock<StoreDBContext> SetupMultipleEntryDbContext()
        {
            var testProducts = SetupTestProducts(new List<Product>
            {
                new Product {
                    Name ="P1",
                    Description = "Something",
                    Price = 100000,
                    Sku = "A1B2C3"
                },
                new Product {
                    Name ="P2",
                    Description = "Something 2",
                    Price = 200000,
                    Sku = "A2B4C6"
                }
            });
            var testReviews = SetupTestReviews(new List<ProductReview>
            {
                new ProductReview
                {
                    Id = 1,
                    ProductName = "P1",
                    UserName = "U1",
                    Score = 3,
                    Comment = "Whatever"
                },
                new ProductReview
                {
                    Id = 2,
                    ProductName = "P1",
                    UserName = "U1",
                    Score = 4,
                    Comment = "Whatever"
                }
            });

            return SetupDbContext(testProducts.Object, testReviews.Object);
        }

        /// <summary>
        /// Sets up test products.
        /// </summary>
        /// <param name="products">The products.</param>
        /// <returns></returns>
        public static Mock<DbSet<Product>> SetupTestProducts(List<Product> products)
        {
            var testProducts = new Mock<DbSet<Product>>();
            var productsData = products.AsQueryable();
            testProducts.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(productsData.Provider);
            testProducts.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(productsData.Expression);
            testProducts.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(productsData.ElementType);
            testProducts.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(() => productsData.GetEnumerator());
            return testProducts;
        }
        
        /// <summary>
        /// Setups the database context.
        /// </summary>
        /// <param name="products">The products.</param>
        /// <param name="reviews">The reviews.</param>
        /// <returns></returns>
        public static Mock<StoreDBContext> SetupDbContext(DbSet<Product> products, DbSet<ProductReview> reviews)
        {
            var testDbContext = new Mock<StoreDBContext>();
            testDbContext.Setup(m => m.Products).Returns(products);
            testDbContext.Setup(m => m.ProductReviews).Returns(reviews);
            return testDbContext;
        }

        /// <summary>
        /// Sets up test reviews.
        /// </summary>
        /// <param name="reviews">The reviews.</param>
        /// <returns></returns>
        public static Mock<DbSet<ProductReview>> SetupTestReviews(List<ProductReview> reviews)
        {
            var testReviews = new Mock<DbSet<ProductReview>>();
            var reviewsData = reviews.AsQueryable();
            testReviews.As<IQueryable<ProductReview>>().Setup(m => m.Provider).Returns(reviewsData.Provider);
            testReviews.As<IQueryable<ProductReview>>().Setup(m => m.Expression).Returns(reviewsData.Expression);
            testReviews.As<IQueryable<ProductReview>>().Setup(m => m.ElementType).Returns(reviewsData.ElementType);
            testReviews.As<IQueryable<ProductReview>>().Setup(m => m.GetEnumerator()).Returns(() => reviewsData.GetEnumerator());
            return testReviews;
        }
    }
}
