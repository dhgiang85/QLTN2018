namespace QLTN2018.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modified2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DiemDen", "Radius", c => c.Decimal(nullable: false, precision: 18, scale: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DiemDen", "Radius", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
