using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestCustard.ModelsTest
{
    public class Todolist
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("user")]
        public string User { get; set; }
    }
}
