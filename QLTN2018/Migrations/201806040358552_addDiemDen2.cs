namespace QLTN2018.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDiemDen2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DiemDen", "DateCreated", c => c.DateTime());
            AddColumn("dbo.DiemDen", "DateModified", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DiemDen", "DateModified");
            DropColumn("dbo.DiemDen", "DateCreated");
        }
    }
}
