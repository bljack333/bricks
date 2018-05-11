using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorageServices.Entities
{
    [Serializable]
    public class StorageArea
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
