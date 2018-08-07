using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using RestSharp.Deserializers;

namespace StorageServices.Entities
{
    public class Part
    {
        [DeserializeAs(Name="part_num")]
        public string PartNumber { get; set; }
        public string Name { get; set; }
        [DeserializeAs(Name = "part_cat_id")]
        public int PartCategoryId { get; set; }
        [DeserializeAs(Name = "year_from")]
        public int YearFrom { get; set; }
        [DeserializeAs(Name = "year_to")]
        public int YearTo { get; set; }
        [DeserializeAs(Name = "part_url")]
        public string PartUrl { get; set; }
        [DeserializeAs(Name = "part_img_url")]
        public string PartImageUrl { get; set; }
        public IEnumerable<string> Prints { get; set; }
        public IEnumerable<string> Molds { get; set; }
        public IEnumerable<string> Alternates { get; set; }
        [DeserializeAs(Name = "external_ids")]
        public IEnumerable<ExternalId> ExternalIds { get; set; }
    }

    public class ExternalId
    {
        public string Name { get; set; }
        public IEnumerable<string> Ids { get; set; }
    }
}
