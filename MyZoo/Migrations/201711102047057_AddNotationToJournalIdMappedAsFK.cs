namespace MyZoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotationToJournalIdMappedAsFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.JournalsDiagnoses", "Journal_AnimalId", "dbo.Journals");
            DropIndex("dbo.JournalsDiagnoses", new[] { "Journal_AnimalId" });
            DropColumn("dbo.JournalsDiagnoses", "JournalId");
            RenameColumn(table: "dbo.JournalsDiagnoses", name: "Journal_AnimalId", newName: "JournalId");
            AlterColumn("dbo.JournalsDiagnoses", "JournalId", c => c.Int(nullable: false));
            CreateIndex("dbo.JournalsDiagnoses", "JournalId");
            AddForeignKey("dbo.JournalsDiagnoses", "JournalId", "dbo.Journals", "AnimalId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JournalsDiagnoses", "JournalId", "dbo.Journals");
            DropIndex("dbo.JournalsDiagnoses", new[] { "JournalId" });
            AlterColumn("dbo.JournalsDiagnoses", "JournalId", c => c.Int());
            RenameColumn(table: "dbo.JournalsDiagnoses", name: "JournalId", newName: "Journal_AnimalId");
            AddColumn("dbo.JournalsDiagnoses", "JournalId", c => c.Int(nullable: false));
            CreateIndex("dbo.JournalsDiagnoses", "Journal_AnimalId");
            AddForeignKey("dbo.JournalsDiagnoses", "Journal_AnimalId", "dbo.Journals", "AnimalId");
        }
    }
}
