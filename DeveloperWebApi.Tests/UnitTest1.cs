using System;
using DeveloperWepApi1.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Xunit;
using NUnit;
using Assert = Xunit.Assert;

namespace DeveloperWebApi.Tests
{
    [TestClass]
    public class Tests
    {
        private Mock<DeveloperService> _mock;

        [TestInitialize]
        public void StartMoq()
        {
            
        }
        [TestMethod]
        public void Test1()
        {
            //Arrange
            var developer = new DeveloperService();
            //Act
            var dev = developer.GetById(Guid.Parse("f9a08115-776f-49f4-b267-36f7ce0d126a"));
            //Assert
            Assert.NotNull(dev);
        }
    }
}
