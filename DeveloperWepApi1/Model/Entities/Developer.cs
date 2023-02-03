using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Security.Cryptography;
using System.Text.Json.Serialization;
using DeveloperWepApi1.Model.RequestModels;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization.Attributes;


namespace DeveloperWepApi1.Model.Entities
{
    // [ModelMetadataType(typeof(CreateDeveloperDto))]
    public class Developer: Document
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Department { get; set; }
       
        public string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
        
    }
}