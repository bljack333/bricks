using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorageServices.Entities
{
    public class ContainerDivision
    {
        public int Id { get; set; }
        public ContainerDivisionTypeEnum ContainerDivisionType { get; set; }
        public List<string> Parts { get; set; }
        public string Other { get; set; }
    }

    public enum ContainerDivisionTypeEnum
    {
        Drawer,
        Slot
    }
}
