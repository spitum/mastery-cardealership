using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace GuildCars.UI.Models
{
    public class CaptchaResponse
    {
        [JsonProperty("Success")]
        public bool Success { get; set; }

        [JsonProperty("ErrorCodes")]
        public List<string> ErrorCodes { get; set; }


    }
}