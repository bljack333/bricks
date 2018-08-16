using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorageServices.Entities.Rebrickable
{
    public class MySet
    {
        public string ListId { get; set; }
        public int Quantity { get; set; }
        public Boolean IncludeSpares { get; set; }
        public Set Set { get; set; }
    }
}
