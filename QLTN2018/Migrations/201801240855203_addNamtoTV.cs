namespace QLTN2018.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNamtoTV : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaiNanThuongVong", "Nam", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TaiNanThuongVong", "Nam");
        }
    }
}
