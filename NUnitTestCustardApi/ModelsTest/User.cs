using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestCustardApi.ModelsTest
{
    public class User
    {
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
    }
}
