using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StorageServices.Entities;

namespace StorageServices.Domain
{
    public class MyPart
    {
        // Rebrickable Part
        public string PartNumber { get; set; }
        public string Name { get; set; }
        public int PartCategoryId { get; set; }
        public string PartUrl { get; set; }
        public string PartImageUrl { get; set; }
        public int Quantity { get; set; }
        public PartExternalId ExternalId { get; set; }
        public IEnumerable<Color> Colors { get; set; }
        public IEnumerable<Container> Containers { get; set; }
    }

    public class PartList
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PartExternalId
    {

    }

    public class Color
    {
        public int Quantity { get; set; }
        public string Name { get; set; }
        public string RGB { get; set; }
        public bool IsTransparent { get; set; }
    }

    public class ColorExternalId
    {

    }
}
