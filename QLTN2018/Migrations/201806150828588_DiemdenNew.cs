namespace QLTN2018.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DiemdenNew : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DiemDen", "Address", c => c.String(maxLength: 128));
            AddColumn("dbo.DiemDen", "Note", c => c.String());
            AddColumn("dbo.DiemDen", "DVCT", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DiemDen", "DVCT");
            DropColumn("dbo.DiemDen", "Note");
            DropColumn("dbo.DiemDen", "Address");
        }
    }
}
