using System;

namespace StorageServices.Entities.Rebrickable
{
    public class Set
    {
        public string SetNumber { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public int ThemeId { get; set; }
        public int NumberOfParts { get; set; }
        public string SetImgUrl { get; set; }
        public string SetUrl { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
