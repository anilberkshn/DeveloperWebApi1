using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeveloperWepApi1.DeveloperRepository;
using DeveloperWepApi1.Model.Entities;
using DeveloperWepApi1.Model.ErrorModels;
using DeveloperWepApi1.Model.RequestModels;
using DeveloperWepApi1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace DeveloperWepApiTest2
{
    [TestClass]
    public class DeveloperServiceUnitTest3
    {
        [Test]
        public void Get_By_Id()
        {
            //arrange
            var mockRepository = new Mock<IDeveloperRepository>();
            var developer = new Developer
            {
                Id = Guid.Parse("f9a08115-776f-49f4-b267-36f7ce0d126a"),
                Name = "Furkan",
            };
            mockRepository.Setup(x => x.GetById(developer.Id)).Returns(new Developer()
            {
                Id = developer.Id,
                Name = developer.Name
            });
            var developerService = new DeveloperService(mockRepository.Object);

            //act
            var result = developerService.GetById(developer.Id);

            //assert
            Assert.AreEqual(developer.Name, result.Name, "Name alanı doğru değil");
            Assert.AreEqual(developer.Id, result.Id);
            
        }

        [Test] 
        public async Task Get_All_Developer()
        {
            //arrange
            var mockRepository = new Mock<IDeveloperRepository>();
            IEnumerable<Developer> developerList = new List<Developer>
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
            mockRepository.Setup(x => x.GetAllAsync(It.IsAny<GetAllDto>())).Returns(Task.FromResult(developerList));
            var developerService = new DeveloperService(mockRepository.Object);

            //act
            var result1 = await developerService.GetAllAsync(new GetAllDto(2, 5));
            //var result3 = await developerService.GetAllAsync(new GetAllDto(0, 150));

            //assert
            Assert.AreEqual(result1.Count(), 3);
            Assert.ThrowsException<ValidationErrorException>(() => developerService.GetAllAsync(new GetAllDto(-1, 5)));
            Assert.ThrowsException<ValidationErrorException>(() => developerService.GetAllAsync(new GetAllDto(0, 150)));
            Assert.ThrowsException<ValidationErrorException>(() => developerService.GetAllAsync(new GetAllDto(0, -1)));

        }
        [Test]
        public void Delete_Developer_Test()
        {
            //arrange
            var mockRepository = new Mock<IDeveloperRepository>();
            var developer = new Developer() 
            {
                Id = Guid.Parse("f9a08115-776f-49f4-b267-36f7ce0d126a"),
                Name = "Furkan",
            };
            
            mockRepository.Setup(x =>x.Delete(developer.Id))
                .Returns(developer.Id);

            var developerService = new DeveloperService(mockRepository.Object);
            
            // act
            var result = developerService.Delete(developer.Id);
           
            //Assert
            Assert.IsInstanceOfType<Guid>(result);
        }
        
        [Test]
        public async Task Insert_Developer_Test()
        {
            //arrange
            var mockRepository = new Mock<IDeveloperRepository>();
            var developer = new Developer() 
            {
                Id = Guid.Parse("f9a08115-776f-49f4-b267-36f7ce0d126a"),
                Name = "Furkan",
            };
            mockRepository.Setup(x => x.InsertDeveloperAsync(developer)).Returns(Task.FromResult<Guid>(developer.Id));

            var developerService = new DeveloperService(mockRepository.Object);
            //act 
            var result = await developerService.InsertDeveloperAsync(developer);
            
            //Assert
            
            Assert.AreEqual(result,developer.Id);
        }
        
        [Test]
        public void SoftDelete_Developer_Test()
        {
            //arrange
            var mockRepository = new Mock<IDeveloperRepository>();
           
            var developer = new Developer() 
            {
                Id = Guid.Parse("f9a08115-776f-49f4-b267-36f7ce0d126a"),
                Name = "Furkan",
                IsDeleted = true,
            };
            mockRepository.Setup(x => x.GetById(developer.Id)).Returns(developer);
            
            var softDeleteDto = new SoftDeleteDto()
            {
                DeletedTime = DateTime.Parse("2023-03-16T10:28:26.839Z"),
                IsDeleted = true
            };
            mockRepository.Setup(x => x.SoftDelete(developer.Id, softDeleteDto));
                         
            var developerService = new DeveloperService(mockRepository.Object);
            
            // act
            developerService.SoftDelete(developer.Id,softDeleteDto);
            
            //Assert
            Assert.AreEqual(softDeleteDto.IsDeleted,developer.IsDeleted);
        }
        
        [Test]
        public void Update_Developer_Test()
        {
            //arrange
            var mockRepository = new Mock<IDeveloperRepository>();
            var developer = new Developer() 
            {
                Id = Guid.Parse("f9a08115-776f-49f4-b267-36f7ce0d126a"),
                Name = "Furkan",
                Surname = "Aydın",
                Department = "backend developer",
                CreatedTime = DateTime.Parse("2023-03-16T10:28:26.839Z"),
                UpdatedTime = DateTime.Parse("2023-03-16T10:28:26.839Z"),
                DeleteTime = DateTime.Parse("0001-01-01T00:00:00Z"),
                IsDeleted = false
            };

            mockRepository.Setup(x => x.GetById(developer.Id)).Returns(developer);
            var updateDto = new UpdateDeveloperDto()
            {
                Name = "Furkan",
                Surname = "Aydın",
                Department = "backend developer",
                UpdatedTime = DateTime.Parse("2023-03-16T10:28:26.839Z"),
                IsDeleted = false
            };
            
            mockRepository.Setup(x =>
                x.UpdateDeveloper(developer.Id,updateDto));// .Returns(developer);

            var developerService = new DeveloperService(mockRepository.Object);
            
            // act
            developerService.UpdateDeveloper(developer.Id,updateDto);
            
            //Assert
            Assert.AreEqual(updateDto.Name,developer.Name);
            Assert.AreEqual(updateDto.Surname,developer.Surname);
            Assert.AreEqual(updateDto.Department,developer.Department);
        }
    }
    
}