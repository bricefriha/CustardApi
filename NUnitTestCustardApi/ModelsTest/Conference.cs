using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestCustardApi.ModelsTest
{
    public class Conference
    {
        [JsonProperty("conferenceID")]
        public string ConferenceID { get; set; }
        [JsonProperty("conferenceTitle")]
        public string ConferenceTitle { get; set; }
        [JsonProperty("shortTitle")]
        public string ShortTitle { get; set; }
        [JsonProperty("timeZoneID")]
        public string TimeZoneID { get; set; }
        [JsonProperty("startDate")]
        public string StartDate { get; set; }
        [JsonProperty("endDate")]
        public string EndDate { get; set; }
    }
}
