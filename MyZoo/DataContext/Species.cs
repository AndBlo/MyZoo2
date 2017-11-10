namespace MyZoo.DataContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Species
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Species()
        {
            Animals = new HashSet<Animal>();
        }

        public int SpeciesId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int? EnvironmentId { get; set; }

        public int? TypeId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Animal> Animals { get; set; }

        public virtual Environment Environment { get; set; }

        public virtual Type Type { get; set; }
    }
}
