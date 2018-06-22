namespace QLTN2018.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modified : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DiemDen", "DateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.DiemDen", "DateModified", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DiemDen", "DateModified", c => c.DateTime());
            AlterColumn("dbo.DiemDen", "DateCreated", c => c.DateTime());
        }
    }
}
