using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebRole1.Models;

namespace UnitTestProject1
{
    [TestClass]
    public class ProductReviewRepositoryTests
    {
        /// <summary>
        /// Basic test for Submit reviews.
        /// </summary>
        [TestMethod]
        public void SubmitReviewTestBasic()
        {
            var testDbContext = RepositoryTestCommon.SetupSingleEntryDbContext();            
            var testEntry = new ProductReviewEntry()
            {
                ProductName = "P1",
                UserName = "U1",
                Score = 4,
                Comment = "Whatever"
            };

            var result = new ProductsReviewsRepository(testDbContext.Object).SubmitReview(testEntry);

            Assert.IsNotNull(result);
            Assert.AreEqual(Status.Success, result.Status);
            testDbContext.Verify(m => m.SaveChanges(), Times.Once);            
        }

        /// <summary>
        /// Invalid Product Name test for Submit reviews.
        /// </summary>
        [TestMethod]
        public void SubmitReviewTestInvalidProductName()
        {
            var testDbContext = RepositoryTestCommon.SetupSingleEntryDbContext();
            var testEntry = new ProductReviewEntry()
            {
                ProductName = "P2",
                UserName = "U1",
                Score = 3,
                Comment = "Whatever"
            };

            var result = new ProductsReviewsRepository(testDbContext.Object).SubmitReview(testEntry);

            Assert.IsNotNull(result);
            Assert.AreEqual(Status.Failure, result.Status);
            Assert.AreEqual("Product Name: P2 doesn't exist", result.ErrorReason);     
            testDbContext.Verify(m => m.SaveChanges(), Times.Never);
        }

        /// <summary>
        /// Backend expetion test for Submit reviews.
        /// </summary>
        [TestMethod]
        public void SubmitReviewTestException()
        {
            var testProducts = RepositoryTestCommon.SetupTestProducts(new List<Product>());
            var testReviews = new Mock<DbSet<ProductReview>>();
            var testDbContext = RepositoryTestCommon.SetupDbContext(null, testReviews.Object);
            var testEntry = new ProductReviewEntry()
            {
                ProductName = "P1",
                UserName = "U1",
                Score = 3,
                Comment = "Whatever"
            };

            var result = new ProductsReviewsRepository(testDbContext.Object).SubmitReview(testEntry);

            Assert.IsNotNull(result);
            Assert.AreEqual(Status.InternalError, result.Status);
            testDbContext.Verify(m => m.SaveChanges(), Times.Never);
        }

        /// <summary>
        /// Basic test for Update reviews.
        /// </summary>
        [TestMethod]
        public void UpdateReviewTestBasic()
        {
            var testDbContext = RepositoryTestCommon.SetupSingleEntryDbContext();
            var testEntry = new ProductReviewEntry()
            {
                ProductName = "P1",
                UserName = "U1",
                Score = 4,
                Comment = "Whatever"
            };
            var result = new ProductsReviewsRepository(testDbContext.Object).UpdateReview(1, testEntry);

            Assert.IsNotNull(result);
            Assert.AreEqual(Status.Success, result.Status);
            Assert.AreEqual(4, testDbContext.Object.ProductReviews.FirstOrDefault().Score);
            testDbContext.Verify(m => m.SaveChanges(), Times.Once);
        }


        /// <summary>
        /// Invalid ID test for Update reviews.
        /// </summary>
        [TestMethod]
        public void UpdateReviewTestInvalidId()
        {
            var testDbContext = RepositoryTestCommon.SetupSingleEntryDbContext();
            var testEntry = new ProductReviewEntry()
            {
                ProductName = "P1",
                UserName = "U1",
                Score = 4,
                Comment = "Whatever"
            };
            var result = new ProductsReviewsRepository(testDbContext.Object).UpdateReview(0, testEntry);

            Assert.IsNotNull(result);
            Assert.AreEqual(Status.Failure, result.Status);
            Assert.AreEqual("ID not found : 0", result.ErrorReason);
            Assert.AreEqual(3, testDbContext.Object.ProductReviews.FirstOrDefault().Score);
            testDbContext.Verify(m => m.SaveChanges(), Times.Never);
        }

        /// <summary>
        /// Backend expetion test for Submit reviews.
        /// </summary>
        [TestMethod]
        public void UpdateReviewTestException()
        {
            var testProducts = RepositoryTestCommon.SetupTestProducts(new List<Product>());
            var testReviews = new Mock<DbSet<ProductReview>>();
            var testDbContext = RepositoryTestCommon.SetupDbContext(null, testReviews.Object);
            var testEntry = new ProductReviewEntry()
            {
                ProductName = "P1",
                UserName = "U1",
                Score = 3,
                Comment = "Whatever"
            };

            var result = new ProductsReviewsRepository(testDbContext.Object).UpdateReview(1, testEntry);

            Assert.IsNotNull(result);
            Assert.AreEqual(Status.InternalError, result.Status);
            testDbContext.Verify(m => m.SaveChanges(), Times.Never);
        }

        /// <summary>
        /// Basic test for retrieving reviews.
        /// </summary>
        [TestMethod]
        public void ReviewsPerProductTestBasic()
        {
            var testDbContext = RepositoryTestCommon.SetupMultipleEntryDbContext();
            
            var result = new ProductsReviewsRepository(testDbContext.Object).ReviewsPerProduct("P1");
       
            Assert.IsNotNull(result);
            Assert.AreEqual(Status.Success, result.RepositoryResult.Status);
            Assert.AreEqual(2, result.ReviewsResult.Reviews.Count());
            Assert.AreEqual(3.5, result.ReviewsResult.AggregatedScore);
        }

        /// <summary>
        /// Invalid Product NAme test for retrieving reviews.
        /// </summary>
        [TestMethod]
        public void ReviewsPerProductTestInvalidProductName()
        {
            var testDbContext = RepositoryTestCommon.SetupMultipleEntryDbContext();

            var result = new ProductsReviewsRepository(testDbContext.Object).ReviewsPerProduct("P10");

            Assert.IsNotNull(result);
            Assert.AreEqual(Status.Failure, result.RepositoryResult.Status);            
            Assert.AreEqual("Product Name: P10 doesn't exist", result.RepositoryResult.ErrorReason);
            Assert.IsNull(result.ReviewsResult);
        }

        /// <summary>
        /// Basic test for Delete reviews.
        /// </summary>
        [TestMethod]
        public void DeleteReviewTestBasic()
        {
            var testDbContext = RepositoryTestCommon.SetupSingleEntryDbContext();            
            var result = new ProductsReviewsRepository(testDbContext.Object).DeleteReview(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(Status.Success, result.Status);
            testDbContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        /// <summary>
        /// Invalid ID test for Delete reviews.
        /// </summary>
        [TestMethod]
        public void DeleteReviewTestInvalidId()
        {
            var testDbContext = RepositoryTestCommon.SetupSingleEntryDbContext();
            var result = new ProductsReviewsRepository(testDbContext.Object).DeleteReview(0);

            Assert.IsNotNull(result);
            Assert.AreEqual(Status.Failure, result.Status);
            Assert.AreEqual("ID: 0 doesn't exist", result.ErrorReason);
            testDbContext.Verify(m => m.SaveChanges(), Times.Never);
        }        
    }
}
