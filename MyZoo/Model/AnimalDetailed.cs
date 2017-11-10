using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyZoo.Model
{
    public class AnimalDetailed
    {
        public int AnimalId { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public float WeightInKilogram { get; set; }
        public string CountryOfOrigin { get; set; }
        public string Species { get; set; }
        public string Type { get; set; }
        public string Environment { get; set; }
        public string Mother { get; set; }
        public string Father { get; set; }
        public List<string> ChildList { get; set; }

        public override string ToString()
        {
            return $"{Species} - \"{Name}\"";
        }
    }
}
