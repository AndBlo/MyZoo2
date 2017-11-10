using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyZoo.Model
{
    public class AnimalSimple
    {
        public int AnimalId { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }

        public override string ToString()
        {
            return $"{Species} - \"{Name}\"";
        }
    }
}
