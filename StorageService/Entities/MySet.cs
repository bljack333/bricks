using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorageServices.Entities
{
    public class MySet
    {
        public int Id { get; set; }
        public string SetNumber { get; set; }
        public bool IsItBuilt { get; set; }
        public int StorageAreaId { get; set; }
        public bool HaveInstructions { get; set; }
        public bool HaveBox { get; set; }
        public bool HaveStickers { get; set; }
        public bool StickersOnModel { get; set; }
        public SetState SetState { get; set; }
    }

    public enum SetState
    {
        Displayed,
        Boxed,
        PartedOut
    }
}
