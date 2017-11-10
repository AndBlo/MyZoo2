namespace MyZoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDateAndTimeToSingleDateTimeInBookings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "DateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Bookings", "StartTime");
            DropColumn("dbo.Bookings", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bookings", "Date", c => c.DateTime(nullable: false, storeType: "date"));
            AddColumn("dbo.Bookings", "StartTime", c => c.Time(nullable: false, precision: 7));
            DropColumn("dbo.Bookings", "DateTime");
        }
    }
}
