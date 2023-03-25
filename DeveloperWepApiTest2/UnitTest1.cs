using System;
using DeveloperWepApi1.DeveloperRepository;
using DeveloperWepApi1.Model.Entities;
using DeveloperWepApi1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace DeveloperWepApiTest2
{
    [TestFixture]
    public class DeveloperServiceUnitTest
    {
        private Mock<DeveloperService> _mockDeveloperService;
        private Mock<DeveloperRepository> _mockDeveloperRepository;
  
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
            //arrange
            // _mockDevelop
            //
            //
            //act
            //assert
        }
    }
}