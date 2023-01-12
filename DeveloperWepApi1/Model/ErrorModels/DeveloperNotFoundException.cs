   using System;
using Newtonsoft.Json;

namespace DeveloperWepApi1.Model.ErrorModels
{
    [JsonObject (MemberSerialization.OptIn)]
    public class DeveloperNotFoundException : SystemException
    {   
        [JsonProperty]
        public int StatusCode = 404;
        [JsonProperty]
        public string ErrorMessage;

        public DeveloperNotFoundException(string errorMessage)
        { 
            ErrorMessage = errorMessage;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}