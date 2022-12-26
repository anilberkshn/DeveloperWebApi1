using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Security.Cryptography;
using DeveloperWepApi1.Model.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperWepApi1.Model.Entities
{
    [ModelMetadataType(typeof(CreateDeveloperDto))]
    public class Developer: Document
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Department { get; set; }
    }
}