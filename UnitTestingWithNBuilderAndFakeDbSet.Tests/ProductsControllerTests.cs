using System;
using System.Net;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using TestStack.FluentMVCTesting;
using UnitTestingWithNBuilderAndFakeDbSet.Controllers;
using UnitTestingWithNBuilderAndFakeDbSet.Models;
using UnitTestingWithNBuilderAndFakeDbSet.ViewModels;

namespace UnitTestingWithNBuilderAndFakeDbSet.Tests
{
    [TestClass]
    public class ProductsControllerTests
    {
        private IApplicationDbContext _dbContext;
        private ProductsController _controller;

        [TestInitialize]
        public void Initialize()
        {
            // Create test product data
            var products = Builder<Product>.CreateListOfSize(20)
                .All()
                    .With(p => p.IsDiscontinued = false)
                .TheFirst(5)
                    .With(p => p.IsDiscontinued = true)
                .Build();

            _dbContext = Substitute.For<IApplicationDbContext>();
            _dbContext.Products.Returns(new FakeDbSet<Product>(products));

            _controller = new ProductsController(_dbContext);
        }

        #region Index tests

        [TestMethod]
        public void IndexShouldIncludeDiscontinuedProducts()
        {
            _controller.WithCallTo(c => c.Index(true))
                .ShouldRenderDefaultView()
                .WithModel<ProductIndexViewModel>(vm => vm.Products.Count == 20);
        }

        [TestMethod]
        public void IndexShouldNotIncludeDiscontinuedProducts()
        {
            _controller.WithCallTo(c => c.Index(false))
                .ShouldRenderDefaultView()
                .WithModel<ProductIndexViewModel>(vm => vm.Products.Count == 15);
        }

        #endregion

        #region Details tests

        [TestMethod]
        public void DetailsShouldReturnBadRequest()
        {
            _controller.WithCallTo(c => c.Details(null))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void DetailsShouldReturnNotFound()
        {
            _controller.WithCallTo(c => c.Details(21))
                .ShouldGiveHttpStatus(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void DetailsShouldReturnCorrectProduct()
        {
            _controller.WithCallTo(c => c.Details(1))
                .ShouldRenderDefaultView()
                .WithModel<Product>(p => p.Id == 1);
        }

        #endregion
    }
}
