namespace MyZoo.DataContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Animal
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Animal()
        {
            Bookings = new HashSet<Booking>();
            FamiliesChildren = new HashSet<Family>();
            FamiliesFathers = new HashSet<Family>();
            FamiliesMothers = new HashSet<Family>();
            Journals = new HashSet<Journal>();
        }

        public int AnimalId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public float Weight { get; set; }

        [Required]
        [StringLength(6)]
        public string Sex { get; set; }

        public int? CountryId { get; set; }

        public int? SpeciesId { get; set; }

        public virtual Country Country { get; set; }

        public virtual Species Species { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking> Bookings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Family> FamiliesChildren { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Family> FamiliesFathers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Family> FamiliesMothers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Journal> Journals { get; set; }

        public override string ToString()
        {
            return $"{Species.Name} - \"{Name}\"";
        }
    }
}
