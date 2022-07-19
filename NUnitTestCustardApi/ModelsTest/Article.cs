using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestCustardApi.ModelsTest
{
    public class Article
    {
        public string Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("textSnipet")]
        public string TextSnipet { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("author")]
        public string Author { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }
        public int SourceId { get; set; }
        [JsonProperty("isoDate")]
        public DateTime FullPublishDate { get; set; }
        public string Url { get; set; }

        private bool? _isSaved = null;
    }
}
