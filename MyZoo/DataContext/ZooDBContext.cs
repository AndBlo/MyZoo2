namespace MyZoo.DataContext
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ZooDataBaseContext : DbContext
    {
        public ZooDataBaseContext()
            : base("name=ZooDataBaseContext")
        {
        }

        public virtual DbSet<Animal> Animals { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Diagnosis> Diagnoses { get; set; }
        public virtual DbSet<Environment> Environments { get; set; }
        public virtual DbSet<Family> Families { get; set; }
        public virtual DbSet<Journal> Journals { get; set; }
        public virtual DbSet<JournalsDiagnos> JournalsDiagnoses { get; set; }
        public virtual DbSet<Medication> Medications { get; set; }
        public virtual DbSet<Species> Species { get; set; }
        public virtual DbSet<Type> Types { get; set; }
        public virtual DbSet<Veterinarian> Veterinarians { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>()
                .HasMany(e => e.FamiliesChildren)
                .WithOptional(e => e.AnimalChild)
                .HasForeignKey(e => e.ChildId);

            modelBuilder.Entity<Animal>()
                .HasMany(e => e.FamiliesFathers)
                .WithOptional(e => e.AnimalMother)
                .HasForeignKey(e => e.FatherId);

            modelBuilder.Entity<Animal>()
                .HasMany(e => e.FamiliesMothers)
                .WithOptional(e => e.AnimalFather)
                .HasForeignKey(e => e.MotherId);
        }
    }
}
