using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp.Deserializers;

namespace StorageServices.Entities
{
    public class Set
    {
        [DeserializeAs(Name = "set_num")]
        public string SetNumber { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        [DeserializeAs(Name = "theme_id")]
        public int ThemeId { get; set; }
        [DeserializeAs(Name = "num_parts")]
        public int NumberOfParts { get; set; }
        [DeserializeAs(Name = "set_img_url")]
        public string SetImgUrl { get; set; }
        [DeserializeAs(Name = "last_modified_dt")]
        public DateTime LastModifiedDate { get; set; }
    }
}
