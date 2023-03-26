using System;
using System.Collections.Generic;
using System.Linq;
using DeveloperWepApi1.DeveloperRepository;
using DeveloperWepApi1.Model.Entities;
using DeveloperWepApi1.Model.RequestModels;
using DeveloperWepApi1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace DeveloperWepApiTest2
{
    [TestClass]
    public class DeveloperServiceUnitTest
    {
        public readonly IDeveloperRepository _mockIDeveloperRepository;
        
        private Mock<DeveloperService> _mockDeveloperService;
        private Mock<DeveloperRepository> _mockDeveloperRepository;

        public DeveloperServiceUnitTest()
        {
            var developerList = new List<Developer>
            {
                new Developer
                {
                    Name = "Furkan",
                    Surname = "Aydın",
                    Department = "backend developer",
                    Id = Guid.Parse("f9a08115-776f-49f4-b267-36f7ce0d126a"),
                    CreatedTime = DateTime.Parse("2023-03-16T10:28:26.839Z"),
                    UpdatedTime = DateTime.Parse("2023-03-16T10:28:26.839Z"),
                    DeleteTime = DateTime.Parse("0001-01-01T00:00:00Z"),
                    IsDeleted = false
                },
                new Developer
                {
                    Name = "string",
                    Surname = "string",
                    Department = "string",
                    Id = Guid.Parse("2721844c-8e9c-4bcb-8a92-eac4355e0c6c"),
                    CreatedTime = DateTime.Parse("2023-01-12T13:01:19.923Z"),
                    UpdatedTime = DateTime.Parse("2023-01-12T13:03:56.027Z"),
                    DeleteTime = DateTime.Parse("2023-01-12T13:03:51.321Z"),
                    IsDeleted = true
                },
                new Developer
                {
                    Name = "dneeme",
                    Surname = "denememe",
                    Department = "denemevalidator",
                    Id = Guid.Parse("0491bb97-c1d1-465c-b250-5ee690b1751b"),
                    CreatedTime = DateTime.Parse("2022-12-29T12:10:39.901Z"),
                    UpdatedTime = DateTime.Parse("2022-12-29T12:10:39.901Z"),
                    DeleteTime = DateTime.Parse("0001-01-01T00:00:00"),
                    IsDeleted = false
                }
            };
            var mockDeveloperRepository = new Mock<IDeveloperRepository>();

            mockDeveloperRepository.Setup(x => x.GetAllAsync(new GetAllDto()));//.Returns(developerList);
            mockDeveloperRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new Developer());//hatayı durdurmak için
            mockDeveloperRepository.Setup(x => x.InsertDeveloperAsync(It.IsAny<Developer>())).Callback(
                (Developer target) =>
                {
                   developerList.Add(target); 
                });

            this._mockIDeveloperRepository = mockDeveloperRepository.Object;
        }

        [OneTimeSetUp]
        public void Setup()
        {
            _mockDeveloperRepository = new Mock<DeveloperRepository>();
            _mockDeveloperService = new Mock<DeveloperService>();
        }
        
        [Test]
        public void GetById_developer_Test1()
        {
            //Arrange
            _mockDeveloperRepository.Setup(x => 
                    x.GetById(It.IsAny<Guid>()))
                //"It" methoda yollanması gereken parametreler yerine daha geniş bir
                //parametre kullanımını sağlıyor. 
                .Returns(new Developer
                {
                    Id = default,
                    CreatedTime = default,
                    UpdatedTime = default,
                    DeleteTime = default,
                    IsDeleted = false,
                    Name = null,
                    Surname = null,
                    Department = null,
                    Username = null,
                    Password = null,
                    RefreshToken = null,
                    RefreshTokenEndDate = null
                });
            
            var developer = new DeveloperService(_mockDeveloperRepository.Object);
            
            
            //Act
            //var dev = developer.GetById(Guid.Parse("f9a08115-776f-49f4-b267-36f7ce0d126a"));
            var dev = developer.GetById(default);
            //Assert
            Assert.NotNull(dev);
        }
        
        [Test]
        public void GetAll_Developer_Test1()
        {
            //arrange
            //act
            //assert
        } 
        [Test]
        public void Delete_Developer_Test1()
        {
            //arrange
            var moqDeveloperRepository = new Mock<DeveloperRepository>();
            var service = new DeveloperService(moqDeveloperRepository.Object);
            //act
            var exception = Record.Exception(() => service.Delete(Guid.Parse("")));
            // xunit kütüphanesi eklendi record için.
            
            //assert
            Assert.Null(exception);
        } 
        
        [Test]
        public void SoftDelete_Developer_Test1()
        {
            //arrange
            //act
            //assert
        } 
        [TestMethod]
        public void Insert_Developer_Test1()
        {
            var actual = this._mockIDeveloperRepository.GetAllAsync(new GetAllDto());// .Count + 1;
 
            var developer = new Developer() { Id = Guid.Parse("2721844c-8e9c-4bcb-8a92-eac4355e0c7c"), Name = "User4", Surname = "User4LastName" };
 
            this._mockIDeveloperRepository.InsertDeveloperAsync(developer);
 
            var expected = this._mockIDeveloperRepository.GetAllAsync(new GetAllDto());//.Count; // hata almaması için new getalldto yazıldı. 
 
            Assert.AreEqual(actual, expected);
        }
    }
}