namespace QLTN2018.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QH4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TaiNan", "MaNNTNGT", "dbo.NNTNGT");
            DropIndex("dbo.TaiNan", new[] { "MaNNTNGT" });
            AlterColumn("dbo.TaiNan", "MaNNTNGT", c => c.Int());
            CreateIndex("dbo.TaiNan", "MaNNTNGT");
            AddForeignKey("dbo.TaiNan", "MaNNTNGT", "dbo.NNTNGT", "MaNNTNGT");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaiNan", "MaNNTNGT", "dbo.NNTNGT");
            DropIndex("dbo.TaiNan", new[] { "MaNNTNGT" });
            AlterColumn("dbo.TaiNan", "MaNNTNGT", c => c.Int(nullable: false));
            CreateIndex("dbo.TaiNan", "MaNNTNGT");
            AddForeignKey("dbo.TaiNan", "MaNNTNGT", "dbo.NNTNGT", "MaNNTNGT", cascadeDelete: true);
        }
    }
}
