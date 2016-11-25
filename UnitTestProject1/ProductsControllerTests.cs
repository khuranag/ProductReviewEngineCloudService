using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebRole1.Controllers;
using WebRole1.Models;

namespace UnitTestProject1
{
    [TestClass]
    public class ProductsControllerTests
    {
        /// <summary>
        /// Basic test fpr Post handler.
        /// </summary>
        [TestMethod]
        public void PostHandlerBasicTest()
        {
            var testEntry = new ProductEntry()
            {
                Name = "P3",
                Sku = "SKU1",
                Price = 400,
                Description = "Whatever"
            };

            var testResult = new Result(Status.Success);
            var testRepo = new Mock<IProductsRepository>();
            testRepo.Setup(m => m.AddProduct(testEntry)).Returns(testResult);

            try
            {
                new ProductsController(testRepo.Object).AddProduct(testEntry);
            }
            catch (Exception)
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
            var testEntry = new ProductEntry()
            {
                Name = "P3",
                Sku = "SKU1",
                Price = 400,
                Description = "Whatever"
            };

            var testResult = new Result(Status.Failure);
            testResult.ErrorReason = "Forced fail";
            var testRepo = new Mock<IProductsRepository>();
            testRepo.Setup(m => m.AddProduct(testEntry)).Returns(testResult);

            try
            {
                new ProductsController(testRepo.Object).AddProduct(testEntry);
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
            var testResult = new ProductsResult
            {
                Result = new Result(Status.Success),
                Products = new List<Product>()
            };

            var testRepo = new Mock<IProductsRepository>();
            testRepo.Setup(m => m.ListProducts()).Returns(testResult);

            try
            {
                var result = new ProductsController(testRepo.Object).GetProducts();
                Assert.IsNotNull(result);
            }
            catch (Exception)
            {
                Assert.Fail("Exception wasn't expected");
            }
        }

        /// <summary>
        /// error case test for Get products.
        /// </summary>
        [TestMethod]
        public void GetHandlerErrorCaseTest()
        {
            var testResult = new ProductsResult
            {
                Result = new Result(Status.Failure),
            };

            testResult.Result.ErrorReason = "Forced Fail";
            var testRepo = new Mock<IProductsRepository>();
            testRepo.Setup(m => m.ListProducts()).Returns(testResult);

            try
            {
                var result = new ProductsController(testRepo.Object).GetProducts();

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
            var testEntry = new ProductEntry()
            {
                Name = "P3",
                Sku = "SKU1",
                Price = 400,
                Description = "Whatever"
            };

            var testResult = new Result(Status.Success);
            var testRepo = new Mock<IProductsRepository>();
            testRepo.Setup(m => m.UpdateProduct(1, testEntry)).Returns(testResult);

            try
            {
                new ProductsController(testRepo.Object).UpdateProduct(1, testEntry);
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
            var testEntry = new ProductEntry()
            {
                Name = "P3",
                Sku = "SKU1",
                Price = 400,
                Description = "Whatever"
            };

            var testResult = new Result(Status.Failure);
            testResult.ErrorReason = "Forced fail";
            var testRepo = new Mock<IProductsRepository>();
            testRepo.Setup(m => m.UpdateProduct(1, testEntry)).Returns(testResult);

            try
            {
                new ProductsController(testRepo.Object).UpdateProduct(1, testEntry);
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
            var testRepo = new Mock<IProductsRepository>();
            testRepo.Setup(m => m.DeleteProduct(1)).Returns(testResult);

            try
            {
                new ProductsController(testRepo.Object).DeleteProduct(1);
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
            var testRepo = new Mock<IProductsRepository>();
            testRepo.Setup(m => m.DeleteProduct(1)).Returns(testResult);

            try
            {
                new ProductsController(testRepo.Object).DeleteProduct(1);
            }
            catch (HttpResponseException ex)
            {
                Assert.AreEqual("Forced fail", ex.Response.ReasonPhrase);
                Assert.AreEqual(HttpStatusCode.BadRequest, ex.Response.StatusCode);
            }
        }
    }
}
