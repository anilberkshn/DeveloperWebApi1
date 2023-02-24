using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DeveloperWepApi1.Model.RequestModels
{
    public class CreateDeveloperDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public string Department { get; set; }
    }
}