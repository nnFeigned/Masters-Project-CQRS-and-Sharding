﻿using CQRS.Application.Categories.CommandHandlers;
using CQRS.Application.Categories.Commands;
using CQRS.Application.Products.CommandHandlers;
using CQRS.Application.Products.Commands;
using CQRS.Application.Products.Queries;
using CQRS.Application.Products.QueryHandlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Tests
{
    [TestClass]
    public class DeleteProductCommandHandlerTests : BaseTests
    {
        private readonly CreateCategoryCommandHandler _createCategoryCommandHandler = new(WriteCategoryRepository);
        private readonly CreateProductCommandHandler _createProductCommandHandler = new(WriteProductRepository);
        private readonly DeleteProductCommandHandler _deleteProductCommandHandler = new(WriteProductRepository);
        private readonly GetProductByIdQueryHandler _getProductByIdQueryHandler = new(ReadProductRepository);

        [TestMethod]
        public async Task DeleteProductShouldSucceed()
        {
            var createCategoryCommand = new CreateCategoryCommand
            {
                Name = "Test category" + Guid.NewGuid()
            };

            await _createCategoryCommandHandler.Handle(createCategoryCommand, CancellationToken.None);

            var createdCategory = ShopDbContext.Categories.FirstOrDefault(category => category.Name == createCategoryCommand.Name);

            var createProductCommand = new CreateProductCommand
            {
                CategoryId = createdCategory!.Id,
                Name = "Test product" + Guid.NewGuid(),
                Images = ["Image" + Guid.NewGuid()],
            };

            await _createProductCommandHandler.Handle(createProductCommand, CancellationToken.None);

            var createdProduct = ShopDbContext.Products.Include(product => product.Images).FirstOrDefault(product => product.Name == createProductCommand.Name);

            var deleteProductCommand = new DeleteProductCommand
            {
                Id = createdProduct!.Id
            };

            await _deleteProductCommandHandler.Handle(deleteProductCommand, CancellationToken.None);

            var deletedProduct = ShopDbContext.Products.FirstOrDefault(product => product.Id == deleteProductCommand.Id);

            Assert.IsNull(deletedProduct);
        }

        //[TestMethod]
        //public async Task DeleteProductShouldRemoveProductFromMongoDatabase()
        //{
        //    
        //}
    }
}
