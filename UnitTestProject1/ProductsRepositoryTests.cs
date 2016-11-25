using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebRole1.Models;

namespace UnitTestProject1
{
    [TestClass]
    public class ProductsRepositoryTests
    {
        /// <summary>
        /// Add product basic test
        /// </summary>
        [TestMethod]
        public void AddProductTestBasic()
        {
            var testDbContext = RepositoryTestCommon.SetupSingleEntryDbContext();
            var testEntry = new ProductEntry()
            {
                Name = "P3",
                Sku = "SKU1",
                Price = 400,
                Description = "Whatever"
            };

            var result = new ProductsRepository(testDbContext.Object).AddProduct(testEntry);

            Assert.IsNotNull(result);
            Assert.AreEqual(Status.Success, result.Status);
            testDbContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        /// <summary>
        /// Add product exception test
        /// </summary>
        [TestMethod]
        public void AddProductTestException()
        {
            var testProducts = RepositoryTestCommon.SetupTestProducts(new List<Product>());
            var testDbContext = RepositoryTestCommon.SetupDbContext(null, null);
            var testEntry = new ProductEntry()
            {
                Name = "P3",
                Sku = "SKU1",
                Price = 400,
                Description = "Whatever"
            };

            var result = new ProductsRepository(testDbContext.Object).AddProduct(testEntry);

            Assert.IsNotNull(result);
            Assert.AreEqual(Status.InternalError, result.Status);
            testDbContext.Verify(m => m.SaveChanges(), Times.Never);
        }

        /// <summary>
        /// Basic test for Delete product.
        /// </summary>
        [TestMethod]
        public void DeleteProductTestBasic()
        {
            var testDbContext = RepositoryTestCommon.SetupSingleEntryDbContext();
            var result = new ProductsRepository(testDbContext.Object).DeleteProduct(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(Status.Success, result.Status);
            testDbContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        /// <summary>
        /// Invalid ID test for Delete product
        /// </summary>
        [TestMethod]
        public void DeleteProductTestInvalidId()
        {
            var testDbContext = RepositoryTestCommon.SetupSingleEntryDbContext();
            var result = new ProductsRepository(testDbContext.Object).DeleteProduct(0);

            Assert.IsNotNull(result);
            Assert.AreEqual(Status.Failure, result.Status);
            Assert.AreEqual("ID: 0 doesn't exist", result.ErrorReason);
            testDbContext.Verify(m => m.SaveChanges(), Times.Never);
        }

        /// <summary>
        /// Basic test for retrieving products.
        /// </summary>
        [TestMethod]
        public void GetProductsTestBasic()
        {
            var testDbContext = RepositoryTestCommon.SetupMultipleEntryDbContext();

            var result = new ProductsRepository(testDbContext.Object).ListProducts();

            Assert.IsNotNull(result);
            Assert.AreEqual(Status.Success, result.Result.Status);
            Assert.AreEqual(2, result.Products.Count());            
        }

        /// <summary>
        /// Updates the product test basic.
        /// </summary>
        [TestMethod]
        public void UpdateProductTestBasic()
        {
            var testDbContext = RepositoryTestCommon.SetupSingleEntryDbContext();
            var testEntry = new ProductEntry()
            {
                Name = "P1",
                Sku = "SKU1",
                Price = 400,
                Description = "Whatever"
            };
            var result = new ProductsRepository(testDbContext.Object).UpdateProduct(1, testEntry);

            Assert.IsNotNull(result);
            Assert.AreEqual(Status.Success, result.Status);
            Assert.AreEqual(400, testDbContext.Object.Products.FirstOrDefault().Price);
            testDbContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void UpdateReviewTestInvalidId()
        {
            var testDbContext = RepositoryTestCommon.SetupSingleEntryDbContext();
            var testEntry = new ProductEntry()
            {
                Name = "P1",
                Sku = "SKU1",
                Price = 400,
                Description = "Whatever"
            };
            var result = new ProductsRepository(testDbContext.Object).UpdateProduct(0, testEntry);

            Assert.IsNotNull(result);
            Assert.AreEqual(Status.Failure, result.Status);
            Assert.AreEqual("ID not found : 0", result.ErrorReason);
            Assert.AreEqual(100000, testDbContext.Object.Products.FirstOrDefault().Price);
            testDbContext.Verify(m => m.SaveChanges(), Times.Never);
        }
    }
}
