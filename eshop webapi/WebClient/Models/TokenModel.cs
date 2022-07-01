using Newtonsoft.Json;
using System;

namespace WebClient.Models
{
    public class TokenModel
    {
        [JsonProperty("TokenString")]
        public string Token { get; set; }
    }
}
