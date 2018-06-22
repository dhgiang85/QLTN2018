namespace QLTN2018.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ass1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TaiNan", "Lat", c => c.Decimal(nullable: false, precision: 11, scale: 7));
            AlterColumn("dbo.TaiNan", "Lng", c => c.Decimal(nullable: false, precision: 11, scale: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TaiNan", "Lng", c => c.Decimal(nullable: false, precision: 11, scale: 6));
            AlterColumn("dbo.TaiNan", "Lat", c => c.Decimal(nullable: false, precision: 11, scale: 6));
        }
    }
}
