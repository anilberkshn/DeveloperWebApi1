using System;
using System.Data.Common;
using System.Security.Cryptography;

namespace DeveloperWepApi1.Model.Entities
{
    public class Developer: Document
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Department { get; set; }
    }
}