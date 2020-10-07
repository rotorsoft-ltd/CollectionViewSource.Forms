using Newtonsoft.Json;
using System;

namespace SampleApp.Models
{
    [JsonObject]
    public class PersonModel
    {
        [JsonProperty]
        public string Avatar { get; set; }

        [JsonProperty]
        public string FirstName { get; set; }

        [JsonProperty]
        public string LastName { get; set; }

        [JsonProperty]
        public string Gender { get; set; }

        [JsonProperty]
        public DateTime Dob { get; set; }

        [JsonProperty]
        public string Country { get; set; }
    }
}
