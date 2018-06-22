namespace QLTN2018.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTaiNanPro1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaiNan", "DonViNhap", c => c.String(maxLength: 20));
            DropColumn("dbo.TaiNan", "DonVi");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TaiNan", "DonVi", c => c.String(maxLength: 20));
            DropColumn("dbo.TaiNan", "DonViNhap");
        }
    }
}
