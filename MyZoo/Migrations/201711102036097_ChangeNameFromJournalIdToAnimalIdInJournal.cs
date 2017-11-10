namespace MyZoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeNameFromJournalIdToAnimalIdInJournal : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.JournalsDiagnoses", "JournalId", "dbo.Journals");
            DropIndex("dbo.JournalsDiagnoses", new[] { "JournalId" });
            RenameColumn(table: "dbo.Journals", name: "JournalId", newName: "AnimalId");
            RenameIndex(table: "dbo.Journals", name: "IX_JournalId", newName: "IX_AnimalId");
            AddColumn("dbo.JournalsDiagnoses", "Journal_AnimalId", c => c.Int());
            CreateIndex("dbo.JournalsDiagnoses", "Journal_AnimalId");
            AddForeignKey("dbo.JournalsDiagnoses", "Journal_AnimalId", "dbo.Journals", "AnimalId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JournalsDiagnoses", "Journal_AnimalId", "dbo.Journals");
            DropIndex("dbo.JournalsDiagnoses", new[] { "Journal_AnimalId" });
            DropColumn("dbo.JournalsDiagnoses", "Journal_AnimalId");
            RenameIndex(table: "dbo.Journals", name: "IX_AnimalId", newName: "IX_JournalId");
            RenameColumn(table: "dbo.Journals", name: "AnimalId", newName: "JournalId");
            CreateIndex("dbo.JournalsDiagnoses", "JournalId");
            AddForeignKey("dbo.JournalsDiagnoses", "JournalId", "dbo.Journals", "JournalId", cascadeDelete: true);
        }
    }
}
