using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp.Deserializers;

namespace StorageServices.Entities.Rebrickable
{
    public class Theme
    {
        public int Id { get; set; }
        [DeserializeAs(Name = "parent_id")]
        public int ParentId { get; set; }
        public string Name { get; set; }
    }
}
