namespace MyZoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIgnoreJournalPropertyInAnimal1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Animals", "JournalId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Animals", "JournalId", c => c.Int());
        }
    }
}
