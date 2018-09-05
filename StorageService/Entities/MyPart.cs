using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorageServices.Entities
{
    public class MyPart
    {
        public string PartNumber { get; set; }
        public IEnumerable<int> ContainerIds { get; set; }
    }
}
