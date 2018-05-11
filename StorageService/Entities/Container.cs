using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorageServices.Entities
{
    public class Container
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int StorageAreaId { get; set; }
        public ContainerTypeEnum ContainerType { get; set; }

        public List<ContainerDivisionSlot> Slots { get; set; }
    }

    public enum ContainerTypeEnum
    {
        StorageBox,
        Drawers,
        Bulk,
        ZipBag
    }

    public class ContainerDivisionSlot
    {
        public int Id { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public ContainerDivision Division { get; set; }
    }
}
