namespace MyZoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeWeightFromDecimalToFloat : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Animals", "Weight", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Animals", "Weight", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
