namespace QLTN2018.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDiemDen3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DiemDen", "Lat", c => c.Decimal(nullable: false, precision: 11, scale: 7));
            AddColumn("dbo.DiemDen", "Lng", c => c.Decimal(nullable: false, precision: 11, scale: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DiemDen", "Lng");
            DropColumn("dbo.DiemDen", "Lat");
        }
    }
}
