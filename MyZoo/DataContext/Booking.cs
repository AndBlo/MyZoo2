namespace MyZoo.DataContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Booking
    {
        public int BookingId { get; set; }

        public int AnimalId { get; set; }

        public int VeterinaryId { get; set; }

        //public TimeSpan StartTime { get; set; }

        [Column(TypeName = "dateTime")]
        public DateTime DateTime { get; set; }

        public virtual Animal Animal { get; set; }

        public virtual Veterinarian Veterinarian { get; set; }
    }
}
