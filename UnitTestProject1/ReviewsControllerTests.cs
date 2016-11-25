using System;
using System.Net;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebRole1.Controllers;
using WebRole1.Models;

namespace UnitTestProject1
{
    [TestClass]
    public class ReviewsControllerTests
    {
        /// <summary>
        /// Basic test fpr Post handler.
        /// </summary>
        [TestMethod]
        public void PostHandlerBasicTest()
        {
            var testEntry = new ProductReviewEntry()
            {
                ProductName = "P1",
                UserName = "U1",
                Score = 3,
                Comment = "Whatever"
            };

            var testResult = new Result(Status.Success);
            var testRepo = new Mock<IProductsReviewsRepository>();
            testRepo.Setup(m => m.SubmitReview(testEntry)).Returns(testResult);

            try
            {
                new ReviewsController(testRepo.Object).PostProductReviews(testEntry);
            }
            catch(Exception)
            {
                Assert.Fail("Exception wasn't expected");    
            }
        }

        /// <summary>
        /// Error case test for Post handler.
        /// </summary>
        [TestMethod]
        public void PostHandlerErrorCaseTest()
        {
            var testEntry = new ProductReviewEntry()
            {
                ProductName = "P1",
                UserName = "U1",
                Score = 3,
                Comment = "Whatever"
            };

            var testResult = new Result(Status.Failure);
            testResult.ErrorReason = "Forced fail";
            var testRepo = new Mock<IProductsReviewsRepository>();
            testRepo.Setup(m => m.SubmitReview(testEntry)).Returns(testResult);

            try
            {
                new ReviewsController(testRepo.Object).PostProductReviews(testEntry);
            }
            catch (HttpResponseException ex)
            {
                Assert.AreEqual("Forced fail", ex.Response.ReasonPhrase);
                Assert.AreEqual(HttpStatusCode.BadRequest, ex.Response.StatusCode);
            }
        }
        
        /// <summary>
        /// Base case for Get handler.
        /// </summary>
        [TestMethod]
        public void GetHandlerBasicTest()
        {
            var testEntry = "P1";
            var testResult = new ProductReviewsResult
            {
                RepositoryResult = new Result(Status.Success),
                ReviewsResult = new AggregatedReviewsResult
                {
                    AggregatedScore = 2.5
                }
            };        
                
            var testRepo = new Mock<IProductsReviewsRepository>();
            testRepo.Setup(m => m.ReviewsPerProduct(testEntry)).Returns(testResult);

            try
            {
                var result = new ReviewsController(testRepo.Object).GetReviewsPerProduct(testEntry);
                Assert.IsNotNull(result);
                Assert.AreEqual(2.5, result.AggregatedScore);
            }
            catch (Exception)
            {
                Assert.Fail("Exception wasn't expected");
            }
        }

        /// <summary>
        /// error case test for Get reviews.
        /// </summary>
        [TestMethod]
        public void GetHandlerErrorCaseTest()
        {
            var testEntry = "P1";
            var testResult = new ProductReviewsResult
            {
                RepositoryResult = new Result(Status.Failure),
            };

            testResult.RepositoryResult.ErrorReason = "Forced Fail";
            var testRepo = new Mock<IProductsReviewsRepository>();
            testRepo.Setup(m => m.ReviewsPerProduct(testEntry)).Returns(testResult);

            try
            {
                var result = new ReviewsController(testRepo.Object).GetReviewsPerProduct(testEntry);
                
            }
            catch (HttpResponseException ex)
            {
                Assert.AreEqual("Forced Fail", ex.Response.ReasonPhrase);
                Assert.AreEqual(HttpStatusCode.BadRequest, ex.Response.StatusCode);
            }
        }

        /// <summary>
        /// Basic test for Put handler.
        /// </summary>
        [TestMethod]
        public void PutHandlerBasicTest()
        {
            var testEntry = new ProductReviewEntry()
            {
                ProductName = "P1",
                UserName = "U1",
                Score = 3,
                Comment = "Whatever"
            };

            var testResult = new Result(Status.Success);
            var testRepo = new Mock<IProductsReviewsRepository>();
            testRepo.Setup(m => m.UpdateReview(1, testEntry)).Returns(testResult);

            try
            {
                new ReviewsController(testRepo.Object).UpdateProductReview(1, testEntry);
            }
            catch (Exception)
            {
                Assert.Fail("Exception wasn't expected");
            }
        }

        /// <summary>
        /// Error case test for Put handler.
        /// </summary>
        [TestMethod]
        public void PutHandlerErrorCaseTest()
        {
            var testEntry = new ProductReviewEntry()
            {
                ProductName = "P1",
                UserName = "U1",
                Score = 3,
                Comment = "Whatever"
            };

            var testResult = new Result(Status.Failure);
            testResult.ErrorReason = "Forced fail";
            var testRepo = new Mock<IProductsReviewsRepository>();
            testRepo.Setup(m => m.UpdateReview(1, testEntry)).Returns(testResult);

            try
            {
                new ReviewsController(testRepo.Object).UpdateProductReview(1, testEntry);
            }
            catch (HttpResponseException ex)
            {
                Assert.AreEqual("Forced fail", ex.Response.ReasonPhrase);
                Assert.AreEqual(HttpStatusCode.BadRequest, ex.Response.StatusCode);
            }
        }

        /// <summary>
        /// Basic test for Delete handler.
        /// </summary>
        [TestMethod]
        public void DeleteHandlerBasicTest()
        {
            var testResult = new Result(Status.Success);
            var testRepo = new Mock<IProductsReviewsRepository>();
            testRepo.Setup(m => m.DeleteReview(1)).Returns(testResult);

            try
            {
                new ReviewsController(testRepo.Object).DeleteProductReview(1);
            }
            catch (Exception)
            {
                Assert.Fail("Exception wasn't expected");
            }
        }

        /// <summary>
        /// Error case test for Delete handler.
        /// </summary>
        [TestMethod]
        public void DeleteHandlerErrorCaseTest()
        {
            var testResult = new Result(Status.Failure);
            testResult.ErrorReason = "Forced fail";
            var testRepo = new Mock<IProductsReviewsRepository>();
            testRepo.Setup(m => m.DeleteReview(1)).Returns(testResult);

            try
            {
                new ReviewsController(testRepo.Object).DeleteProductReview(1);
            }
            catch (HttpResponseException ex)
            {
                Assert.AreEqual("Forced fail", ex.Response.ReasonPhrase);
                Assert.AreEqual(HttpStatusCode.BadRequest, ex.Response.StatusCode);
            }
        }
    }
}
