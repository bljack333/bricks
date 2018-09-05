using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StorageServices.Entities;

namespace StorageServices.Services
{
    public class PartsRepository : IPartsRepository
    {
        private const string PARTS = "Data/parts.json";

        public IEnumerable<MyPart> GetMyParts()
        {
            var reader = new StreamReader(PARTS);

            string partsString = reader.ReadToEnd();

            reader.Close();

            var parts = JsonConvert.DeserializeObject<IEnumerable<MyPart>>(partsString);

            return parts;

        }

        public MyPart GetMyPart(string partNumber)
        {
            return GetMyParts().FirstOrDefault(p => p.PartNumber == partNumber);
        }
    }
}
