namespace MyZoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Animals",
                c => new
                    {
                        AnimalId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Weight = c.Single(nullable: false),
                        Sex = c.String(nullable: false, maxLength: 6),
                        CountryId = c.Int(),
                        SpeciesId = c.Int(),
                        JournalId = c.Int(),
                    })
                .PrimaryKey(t => t.AnimalId)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .ForeignKey("dbo.Species", t => t.SpeciesId)
                .Index(t => t.CountryId)
                .Index(t => t.SpeciesId);
            
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        BookingId = c.Int(nullable: false, identity: true),
                        AnimalId = c.Int(nullable: false),
                        VeterinaryId = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BookingId)
                .ForeignKey("dbo.Animals", t => t.AnimalId, cascadeDelete: true)
                .ForeignKey("dbo.Veterinarians", t => t.VeterinaryId, cascadeDelete: true)
                .Index(t => t.AnimalId)
                .Index(t => t.VeterinaryId);
            
            CreateTable(
                "dbo.Veterinarians",
                c => new
                    {
                        VeterinaryId = c.Int(nullable: false, identity: true),
                        Namn = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.VeterinaryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.CountryId);
            
            CreateTable(
                "dbo.Families",
                c => new
                    {
                        FamilyId = c.Int(nullable: false, identity: true),
                        ChildId = c.Int(),
                        MotherId = c.Int(),
                        FatherId = c.Int(),
                    })
                .PrimaryKey(t => t.FamilyId)
                .ForeignKey("dbo.Animals", t => t.ChildId)
                .ForeignKey("dbo.Animals", t => t.FatherId)
                .ForeignKey("dbo.Animals", t => t.MotherId)
                .Index(t => t.ChildId)
                .Index(t => t.MotherId)
                .Index(t => t.FatherId);
            
            CreateTable(
                "dbo.Journals",
                c => new
                    {
                        JournalId = c.Int(nullable: false),
                        AnimalId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.JournalId)
                .ForeignKey("dbo.Animals", t => t.JournalId)
                .Index(t => t.JournalId);
            
            CreateTable(
                "dbo.JournalsDiagnoses",
                c => new
                    {
                        JournalDiagnoseId = c.Int(nullable: false, identity: true),
                        JournalId = c.Int(nullable: false),
                        DiagnoseId = c.Int(),
                    })
                .PrimaryKey(t => t.JournalDiagnoseId)
                .ForeignKey("dbo.Diagnoses", t => t.DiagnoseId)
                .ForeignKey("dbo.Journals", t => t.JournalId, cascadeDelete: true)
                .Index(t => t.JournalId)
                .Index(t => t.DiagnoseId);
            
            CreateTable(
                "dbo.Diagnoses",
                c => new
                    {
                        DiagnoseId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.DiagnoseId);
            
            CreateTable(
                "dbo.Medications",
                c => new
                    {
                        MedicationId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.MedicationId);
            
            CreateTable(
                "dbo.Species",
                c => new
                    {
                        SpeciesId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        EnvironmentId = c.Int(),
                        TypeId = c.Int(),
                    })
                .PrimaryKey(t => t.SpeciesId)
                .ForeignKey("dbo.Environments", t => t.EnvironmentId)
                .ForeignKey("dbo.Types", t => t.TypeId)
                .Index(t => t.EnvironmentId)
                .Index(t => t.TypeId);
            
            CreateTable(
                "dbo.Environments",
                c => new
                    {
                        EnvironmentId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.EnvironmentId);
            
            CreateTable(
                "dbo.Types",
                c => new
                    {
                        TypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.TypeId);
            
            CreateTable(
                "dbo.MedicationJournalsDiagnos",
                c => new
                    {
                        Medication_MedicationId = c.Int(nullable: false),
                        JournalsDiagnos_JournalDiagnoseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Medication_MedicationId, t.JournalsDiagnos_JournalDiagnoseId })
                .ForeignKey("dbo.Medications", t => t.Medication_MedicationId, cascadeDelete: true)
                .ForeignKey("dbo.JournalsDiagnoses", t => t.JournalsDiagnos_JournalDiagnoseId, cascadeDelete: true)
                .Index(t => t.Medication_MedicationId)
                .Index(t => t.JournalsDiagnos_JournalDiagnoseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Species", "TypeId", "dbo.Types");
            DropForeignKey("dbo.Species", "EnvironmentId", "dbo.Environments");
            DropForeignKey("dbo.Animals", "SpeciesId", "dbo.Species");
            DropForeignKey("dbo.Journals", "JournalId", "dbo.Animals");
            DropForeignKey("dbo.MedicationJournalsDiagnos", "JournalsDiagnos_JournalDiagnoseId", "dbo.JournalsDiagnoses");
            DropForeignKey("dbo.MedicationJournalsDiagnos", "Medication_MedicationId", "dbo.Medications");
            DropForeignKey("dbo.JournalsDiagnoses", "JournalId", "dbo.Journals");
            DropForeignKey("dbo.JournalsDiagnoses", "DiagnoseId", "dbo.Diagnoses");
            DropForeignKey("dbo.Families", "MotherId", "dbo.Animals");
            DropForeignKey("dbo.Families", "FatherId", "dbo.Animals");
            DropForeignKey("dbo.Families", "ChildId", "dbo.Animals");
            DropForeignKey("dbo.Animals", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Bookings", "VeterinaryId", "dbo.Veterinarians");
            DropForeignKey("dbo.Bookings", "AnimalId", "dbo.Animals");
            DropIndex("dbo.MedicationJournalsDiagnos", new[] { "JournalsDiagnos_JournalDiagnoseId" });
            DropIndex("dbo.MedicationJournalsDiagnos", new[] { "Medication_MedicationId" });
            DropIndex("dbo.Species", new[] { "TypeId" });
            DropIndex("dbo.Species", new[] { "EnvironmentId" });
            DropIndex("dbo.JournalsDiagnoses", new[] { "DiagnoseId" });
            DropIndex("dbo.JournalsDiagnoses", new[] { "JournalId" });
            DropIndex("dbo.Journals", new[] { "JournalId" });
            DropIndex("dbo.Families", new[] { "FatherId" });
            DropIndex("dbo.Families", new[] { "MotherId" });
            DropIndex("dbo.Families", new[] { "ChildId" });
            DropIndex("dbo.Bookings", new[] { "VeterinaryId" });
            DropIndex("dbo.Bookings", new[] { "AnimalId" });
            DropIndex("dbo.Animals", new[] { "SpeciesId" });
            DropIndex("dbo.Animals", new[] { "CountryId" });
            DropTable("dbo.MedicationJournalsDiagnos");
            DropTable("dbo.Types");
            DropTable("dbo.Environments");
            DropTable("dbo.Species");
            DropTable("dbo.Medications");
            DropTable("dbo.Diagnoses");
            DropTable("dbo.JournalsDiagnoses");
            DropTable("dbo.Journals");
            DropTable("dbo.Families");
            DropTable("dbo.Countries");
            DropTable("dbo.Veterinarians");
            DropTable("dbo.Bookings");
            DropTable("dbo.Animals");
        }
    }
}
