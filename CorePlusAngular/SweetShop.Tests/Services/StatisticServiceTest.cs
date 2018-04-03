using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SweetShop.BLL.Interfaces;
using SweetShop.BLL.Services;
using SweetShop.DAL.Entities;
using SweetShop.DAL.Interfaces;

namespace SweetShop.Tests.Services
{
   [TestClass]
   public class StatisticServiceTest : TestBase
   {
      private readonly IStatisticService _sut;
      private readonly Mock<IUnitOfWork> _unitOfWorkMock;

      public StatisticServiceTest()
      {
         _unitOfWorkMock = new Mock<IUnitOfWork>();
         _sut = new StatisticService(_unitOfWorkMock.Object);
      }

      [TestMethod]
      public void GetStatisticByProducts_ReturnsCorrectStatistic_WhenProductsAreExisted()
      {
         const string name = "Chocolate";
         const int expectedCount = 1;
         const int expectedLikes = 3;

         var products = new List<Product>
         {
            new Product {Id = 1, Name = name, Likes = 1},
            new Product {Id = 2, Name = name, Likes = 2}
         };

         _unitOfWorkMock
         .Setup(unitOfWork => unitOfWork.Products.GetAll()).Returns(products);

         var result = _sut.GetStatisticByProducts().ToList();

         Assert.AreEqual(expectedCount, result.Count);
         Assert.AreEqual(name, result.First().Name);
         Assert.AreEqual(expectedLikes, result.First().Likes);
      }

      [TestMethod]
      public void GetStatisticByProducts_ReturnsEmptyList_WhenProductsAreNotExisted()
      {
         const int expectedCount = 0;

         _unitOfWorkMock
         .Setup(unitOfWork => unitOfWork.Products.GetAll()).Returns(new List<Product>());

         var result = _sut.GetStatisticByProducts().ToList();

         Assert.AreEqual(expectedCount, result.Count);
      }

      [TestMethod]
      public void GetStatisticByCompany_ReturnsEmptyList_WhenProductsAreNotExisted()
      {
         const int expectedCount = 0;

         _unitOfWorkMock
         .Setup(unitOfWork => unitOfWork.Products.GetAll()).Returns(new List<Product>());

         _unitOfWorkMock
         .Setup(unitOfWork => unitOfWork.Companies.GetAll()).Returns(new List<Company>());

         var result = _sut.GetStatisticByCompany().ToList();

         Assert.AreEqual(expectedCount, result.Count);
      }

      [TestMethod]
      public void GetStatisticByCompany_ReturnsEmptyList_WhenCompaniesAreNotExisted()
      {
         const int expectedCount = 0;

         _unitOfWorkMock
         .Setup(unitOfWork => unitOfWork.Products.GetAll()).Returns(new List<Product>());

         _unitOfWorkMock
         .Setup(unitOfWork => unitOfWork.Companies.GetAll()).Returns(new List<Company>());

         var result = _sut.GetStatisticByCompany().ToList();

         Assert.AreEqual(expectedCount, result.Count);
      }

      [TestMethod]
      public void GetStatisticByCompany_ReturnsEmptyList_WhenCompaniesAndProductsAreNotExisted()
      {
         const int expectedCount = 0;

         _unitOfWorkMock
         .Setup(unitOfWork => unitOfWork.Products.GetAll()).Returns(new List<Product>());

         _unitOfWorkMock
         .Setup(unitOfWork => unitOfWork.Companies.GetAll()).Returns(new List<Company>());

         var result = _sut.GetStatisticByCompany().ToList();

         Assert.AreEqual(expectedCount, result.Count);
      }

      [TestMethod]
      public void GetStatisticByCompany_ReturnsCorrectStatistic_WhenProductsAndCompaniesAreExisted()
      {
         const string name = "Roshen";
         const int expectedCount = 1;
         const int expectedLikes = 3;

         var products = new List<Product>
         {
            new Product {Id = 1, Likes = 1, CompanyId = 1},
            new Product {Id = 2, Likes = 2, CompanyId = 1}
         };

         var companies = new List<Company>
         {
            new Company {Id = 1, Name = name},
            new Company {Id = 2, Name = name}
         };

         _unitOfWorkMock
         .Setup(unitOfWork => unitOfWork.Products.GetAll()).Returns(products);

         _unitOfWorkMock
         .Setup(unitOfWork => unitOfWork.Companies.GetAll()).Returns(companies);

         var result = _sut.GetStatisticByCompany().ToList();

         Assert.AreEqual(expectedCount, result.Count);
         Assert.AreEqual(name, result.First().Name);
         Assert.AreEqual(expectedLikes, result.First().Likes);
      }
   }
}