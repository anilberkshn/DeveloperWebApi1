using System;
using System.Net;
using Newtonsoft.Json;

namespace TaskWepApi1.Model.ErrorModels
{
    [JsonObject (MemberSerialization.OptIn)]
    
    public class TaskException : SystemException
    {
        [JsonProperty]
        public HttpStatusCode StatusCode;
        [JsonProperty]
        public string ErrorMessage;

        public TaskException(HttpStatusCode statusCode, string errorMessage)
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