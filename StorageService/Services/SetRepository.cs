﻿using System;
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

        public Set GetSet(int Id)
        {
            var reader = new StreamReader(SETS);

            string setsString = reader.ReadToEnd();

            reader.Close();

            var sets = JsonConvert.DeserializeObject<IEnumerable<Set>>(setsString);

            return sets.FirstOrDefault(c => c.Id == Id);

        }
    }
}
