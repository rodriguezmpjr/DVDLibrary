using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace DVDLibrary.Models
{
    
    public class DVD
    {
        [JsonProperty("dvdId")]
        public int DVDId { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("releaseYear")]
        public int ReleaseYear { get; set; }
        [JsonProperty("director")]
        public string Director { get; set; }
        [JsonProperty("rating")]
        public string Rating { get; set; }
        [JsonProperty("notes")]
        public string Notes { get; set; }
    }
}