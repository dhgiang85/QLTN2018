namespace QLTN2018.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDiemDen1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DiemDen",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Radius = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DiemDen");
        }
    }
}
