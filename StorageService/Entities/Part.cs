using System.Collections.Generic;

namespace StorageServices.Entities
{
    public class Part
    {
        public string PartNumber { get; set; }
        public string Name { get; set; }
        public int PartCategoryId { get; set; }
        public int YearFrom { get; set; }
        public int YearTo { get; set; }
        public string PartUrl { get; set; }
        public string PartImageUrl { get; set; }
        public IEnumerable<string> Prints { get; set; }
        public IEnumerable<string> Molds { get; set; }
        public IEnumerable<string> Alternates { get; set; }
        public IEnumerable<ExternalId> ExternalIds { get; set; }
    }

    public class ExternalId
    {
        public string Name { get; set; }
        public IEnumerable<string> Ids { get; set; }
    }
}
