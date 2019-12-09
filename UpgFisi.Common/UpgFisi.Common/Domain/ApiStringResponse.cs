using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace UpgFisi.Common.Domain
{
    public class ApiStringResponse
    {
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        public ApiStringResponse(string message)
        {
            Message = message;
        }
    }
}
