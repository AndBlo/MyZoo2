namespace MyZoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveAnimalIdPropertyInJournal : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Journals", "AnimalId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Journals", "AnimalId", c => c.Int(nullable: false));
        }
    }
}
