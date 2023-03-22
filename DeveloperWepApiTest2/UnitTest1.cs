using System;
using DeveloperWepApi1.DeveloperRepository;
using DeveloperWepApi1.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace DeveloperWepApiTest2
{
    [TestFixture]
    public class Tests
    {
        private Mock<DeveloperRepository> _mock;
        
        
        [TestInitialize]
        public void StartMoq()
        {
            _mock = new Mock<DeveloperRepository>();
        }
        
        
        
        
        [TestMethod]
        public void Test1()
        {
            //Arrange
            var developer = new DeveloperService(_mock.Object);
            //Act
            var dev = developer.GetById(Guid.Parse("f9a08115-776f-49f4-b267-36f7ce0d126a"));
            //Assert
            Assert.NotNull(dev);
        }
    }
}