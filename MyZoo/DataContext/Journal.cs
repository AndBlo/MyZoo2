namespace MyZoo.DataContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Journal
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Journal()
        {
            JournalsDiagnoses = new HashSet<JournalsDiagnos>();
        }
        [Key]
        public int AnimalId { get; set; }

        //[ForeignKey("Animal")]
        //public int AnimalId { get; set; }

        public virtual Animal Animal { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JournalsDiagnos> JournalsDiagnoses { get; set; }
    }
}
