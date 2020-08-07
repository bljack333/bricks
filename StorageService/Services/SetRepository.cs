using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StorageServices.Entities;

namespace StorageServices.Services
{
    public class SetRepository : ISetRepository
    {
        private const string SETS = "Data/sets.json";

        public MySet GetSet(int Id)
        {
            var reader = new StreamReader(SETS);

            string setsString = reader.ReadToEnd();

            reader.Close();

            var sets = JsonConvert.DeserializeObject<IEnumerable<MySet>>(setsString);

            return sets.FirstOrDefault(c => c.Id == Id);

        }

        public IEnumerable<MySet> GetMySets()
        {
            var reader = new StreamReader(SETS);

            string setsString = reader.ReadToEnd();

            reader.Close();

            if (String.IsNullOrWhiteSpace(setsString))
            { return new List<MySet>();}

            var sets = JsonConvert.DeserializeObject<IEnumerable<MySet>>(setsString);

            return sets;
        }
        public MySet GetSetBySetNumber(string setNumber)
        {
            var reader = new StreamReader(SETS);

            string setsString = reader.ReadToEnd();

            reader.Close();

            var sets = JsonConvert.DeserializeObject<IEnumerable<MySet>>(setsString);

            return sets.FirstOrDefault(c => c.SetNumber == setNumber);

        }

    }
}
