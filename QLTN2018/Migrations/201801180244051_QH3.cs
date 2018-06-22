namespace QLTN2018.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QH3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaiNan", "MaDTNNTNGT", c => c.Int(nullable: false));
            CreateIndex("dbo.TaiNan", "MaDTNNTNGT");
            AddForeignKey("dbo.TaiNan", "MaDTNNTNGT", "dbo.DTNNTNGT", "MaDTNNTNGT");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaiNan", "MaDTNNTNGT", "dbo.DTNNTNGT");
            DropIndex("dbo.TaiNan", new[] { "MaDTNNTNGT" });
            DropColumn("dbo.TaiNan", "MaDTNNTNGT");
        }
    }
}
