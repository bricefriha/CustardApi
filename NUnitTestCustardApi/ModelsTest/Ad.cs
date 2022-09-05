using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestCustardApi.ModelsTest
{
    public class Ad
    {
        [JsonProperty("exhibitorAdID")]
        public string ExhibitorAdID { get; set; }
        [JsonProperty("exhibitorID")]
        public string ExhibitorID { get; set; }
        [JsonProperty("exhibitorAdImage")]
        public string ExhibitorAdImage { get; set; }
        [JsonProperty("page")]
        public string Page { get; set; }
        [JsonProperty("conferenceID")]
        public string ConferenceID { get; set; }
        [JsonProperty("position")]
        public string Position { get; set; }
        [JsonProperty("createdDate")]
        public string CreatedDate { get; set; }
        [JsonProperty("createdBy")]
        public string CreatedBy { get; set; }
        [JsonProperty("lastUpdatedDate")]
        public string LastUpdatedDate { get; set; }
        [JsonProperty("lastUpdatedBy")]
        public string LastUpdatedBy { get; set; }
        [JsonProperty("deletedDate")]
        public string DeletedDate { get; set; }
        [JsonProperty("deletedBy")]
        public string DeletedBy { get; set; }
        [JsonProperty("deleted")]
        public bool Deleted { get; set; }
    }
}
