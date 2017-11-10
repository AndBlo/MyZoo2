using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyZoo.Model
{
    class AnimalBooking
    {
        public int BookingId { get; set; }
        public int AnimalId { get; set; }
        public int VeterinaryId { get; set; }
        public string AnimalName { get; set; }
        public string VeterinaryName { get; set; }
        public DateTime DateTime { get; set; }

        public override string ToString()
        {
            return $"{VeterinaryName} - Datum: {DateTime.ToString("f")}";
        }
    }
}
