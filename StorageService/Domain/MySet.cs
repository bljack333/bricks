using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorageServices.Domain
{
    public class MySet
    {
        public int Id { get; set; }
        public bool IsItBuilt { get; set; }
        public int StorageAreaId { get; set; }
        public bool HaveInstructions { get; set; }
        public bool HaveBox { get; set; }
        public bool HaveStickers { get; set; }
        public bool StickersOnModel { get; set; }
        public SetState SetState { get; set; }
        public string ListId { get; set; }
        public int Quantity { get; set; }
        public Boolean IncludeSpares { get; set; }
        public string SetNumber { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public int ThemeId { get; set; }
        public int NumberOfParts { get; set; }
        public string SetImgUrl { get; set; }
        public string SetUrl { get; set; }
        public DateTime LastModifiedDate { get; set; }

    }

    public enum SetState
    {
        Displayed,
        Boxed,
        PartedOut,
        Unclassified
    }

}
