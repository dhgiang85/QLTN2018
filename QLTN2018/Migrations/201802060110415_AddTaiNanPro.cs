namespace QLTN2018.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTaiNanPro : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaiNan", "DonVi", c => c.String(maxLength: 20));
            AddColumn("dbo.TaiNan", "NguoiNhap", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TaiNan", "NguoiNhap");
            DropColumn("dbo.TaiNan", "DonVi");
        }
    }
}
