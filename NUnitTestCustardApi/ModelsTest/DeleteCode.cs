using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestCustardApi.ModelsTest
{
    public class DeleteCode
    {
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
