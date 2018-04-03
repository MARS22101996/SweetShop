using SweetShop.BLL.Interfaces;
using SweetShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SweetShop.BLL.Dto;
using SweetShop.BLL.Infrastructure.Exceptions;
using SweetShop.BLL.Services;
using SweetShop.DAL.Entities;

namespace SweetShop.Tests.Services
{
    [TestClass]
    public class ProductServiceTest : TestBase
    {
        private readonly IProductService _sut;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

       public ProductServiceTest()
       {
          var mapper = GetMapper();
          _unitOfWorkMock = new Mock<IUnitOfWork>();
          var basketServiceMock = new Mock<IBasketService>();
          _sut = new ProductService(_unitOfWorkMock.Object, mapper, basketServiceMock.Object);
       }

        [TestMethod]
        public void Get_ReturnsCorrectProduct_WhenProductExists()
        {
            var product = new Product {Id = 1};

            _unitOfWorkMock
                .Setup(unitOfWork => unitOfWork.Products.GetProduct(product.Id))
                .Returns(product);

            var result = _sut.Get(product.Id);

            Assert.AreEqual(product.Id, result.Id);
        }

        [TestMethod]
        public void Get_ThrowsEntityNotFoundException_WhenProductIsNotExisted()
        {
            const int productId = 1;
            _unitOfWorkMock
                .Setup(unitOfWork => unitOfWork.Products.GetProduct(productId))
                .Returns((Product) null);

            Assert.ThrowsException<EntityNotFoundException>(() => _sut.Get(productId));
        }

        [TestMethod]
        public void GetAll_ReturnsCorrectProduct_WhenProductsAreExisted()
        {
            var products = new List<Product> {new Product {Id = 1}};

            _unitOfWorkMock
                .Setup(unitOfWork => unitOfWork.Products.GetAllProducts()).Returns(products);

            var result = _sut.GetAll();

            Assert.AreEqual(products.Count, result.Count());
        }

        [TestMethod]
        public void GetAll_ReturnsEmptyList_WhenProductsAreNotExisted()
        {
            var products = new List<Product>();

            _unitOfWorkMock
                .Setup(unitOfWork => unitOfWork.Products.GetAllProducts()).Returns(products);

            var result = _sut.GetAll();

            Assert.AreEqual(products.Count, result.Count());
        }

        [TestMethod]
        public void GetFilteredByCompany_ReturnsFilteredProducts_WhenProductsAreExisted()
        {
            const int companyId = 1;

            var products = new List<Product> {new Product {Id = 1}};

            _unitOfWorkMock
                .Setup(unitOfWork => unitOfWork.Products.Get(It.IsAny<Expression<Func<Product, bool>>>()))
                .Returns(products);

            var result = _sut.GetFilteredByCompany(companyId);

            Assert.AreEqual(products.Count, result.Count());
        }

        [TestMethod]
        public void GetFilteredByCompany_ReturnsEmptyList_WhenProductsAreNotExisted()
        {
            const int companyId = 1;

            var products = new List<Product>();

            _unitOfWorkMock
                .Setup(unitOfWork => unitOfWork.Products.Get(It.IsAny<Expression<Func<Product, bool>>>()))
                .Returns(new List<Product>());

            var result = _sut.GetFilteredByCompany(companyId);

            Assert.AreEqual(products.Count, result.Count());
        }

        [TestMethod]
        public void Create_CallsCreateFromDal_WhenProductIsValid()
        {
            var model = new ProductDto {Id = 1, CompanyId = 1};

            _unitOfWorkMock.Setup(unitOfWork => unitOfWork.Products.Create(It.IsAny<Product>()));

            _unitOfWorkMock
                .Setup(unitOfWork => unitOfWork.Companies.Get(model.CompanyId))
                .Returns(new Company {Id = 1});

            _sut.Create(model);

            _unitOfWorkMock.Verify(unitOfWork => unitOfWork.Products.Create(It.IsAny<Product>()), Times.Once);
        }

        [TestMethod]
        public void Create_ThrowsEntityNotFoundException_WhenCompanyIsNotExisted()
        {
            var model = new ProductDto {Id = 1, CompanyId = 1};

            _unitOfWorkMock.Setup(unitOfWork => unitOfWork.Products.Create(It.IsAny<Product>()));

            _unitOfWorkMock
                .Setup(unitOfWork => unitOfWork.Companies.Get(model.CompanyId))
                .Returns((Company) null);

            Assert.ThrowsException<EntityNotFoundException>(() => _sut.Create(model));
        }

        [TestMethod]
        public void Create_ThrowsArgumentNullException_WhenProductIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _sut.Create(null));
        }

        [TestMethod]
        public void Update_CallsUpdateFromDal_WhenProductIsValid()
        {
            var model = new ProductDto {Id = 1, CompanyId = 1};

            _unitOfWorkMock.Setup(unitOfWork => unitOfWork.Products.Update(It.IsAny<Product>()));

            _sut.Update(model);

            _unitOfWorkMock.Verify(unitOfWork => unitOfWork.Products.Update(It.IsAny<Product>()), Times.Once);
        }

        [TestMethod]
        public void Update_ThrowsArgumentNullException_WhenProductIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _sut.Update(null));
        }

        [TestMethod]
        public void Delete_CallsDeleteMethod_WhenProductIsExisted()
        {
            var product = new Product {Id = 1};

            _unitOfWorkMock
                .Setup(unitOfWork => unitOfWork.Products.Get(It.IsAny<int>()))
                .Returns(product);

            _unitOfWorkMock
                .Setup(x => x.Products.Delete(It.IsAny<int>()));

            _sut.Delete(product.Id);

            _unitOfWorkMock.Verify(unitOfWork => unitOfWork.Products.Delete(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void DeleteAsync_ThrowsEntityNotFoundException_WhenProductIsNotExisted()
        {
            const int productId = 1;

            _unitOfWorkMock
                .Setup(unitOfWork => unitOfWork.Products.Get(It.IsAny<int>()))
                .Returns((Product) null);

            Assert.ThrowsException<EntityNotFoundException>(() => _sut.Delete(productId));
        }
    }
}
