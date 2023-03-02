using System;
using System.Net;
using Newtonsoft.Json;

namespace DeveloperWepApi1.Model.ErrorModels
{
    [JsonObject (MemberSerialization.OptIn)]
    public class ValidationErrorException : SystemException
    {
        [JsonProperty]
        public HttpStatusCode StatusCode;
        [JsonProperty]
        public string ErrorMessage;

        public ValidationErrorException(HttpStatusCode statusCode, string errorMessage)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}