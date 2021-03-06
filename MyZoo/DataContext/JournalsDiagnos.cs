namespace MyZoo.DataContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("JournalsDiagnoses")]
    public partial class JournalsDiagnos
    {
        public JournalsDiagnos()
        {
            Medications = new HashSet<Medication>();
        }

        [Key]
        public int JournalDiagnoseId { get; set; }

        [ForeignKey("Journal")]
        public int JournalId { get; set; }

        public int? DiagnoseId { get; set; }

        //public int? MedicationId { get; set; }

        public virtual Diagnosis Diagnosis { get; set; }

        public virtual Journal Journal { get; set; }

        //public virtual Medication Medication { get; set; }
        public virtual ICollection<Medication> Medications { get; set; }
    }
}
