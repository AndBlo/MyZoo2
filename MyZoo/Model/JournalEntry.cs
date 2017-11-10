using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyZoo.Model
{
    class JournalEntry
    {
        public int JournalId { get; set; }
        public int AnimalId { get; set; }
        public string AnimalName { get; set; }
        public int DiagnoseId { get; set; }
        public string DiagnoseName { get; set; }
        public string DiagnoseDescription { get; set; }
        public List<string> Medications { get; set; }

    }
}
