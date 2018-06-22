namespace QLTN2018.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QH2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DTLVP",
                c => new
                    {
                        MaDTLVP = c.Int(nullable: false),
                        TenDTLVP = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.MaDTLVP);
            
            CreateTable(
                "dbo.DTNNTNGT",
                c => new
                    {
                        MaDTNNTNGT = c.Int(nullable: false),
                        TenDTNNTNGT = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.MaDTNNTNGT);
            
            CreateTable(
                "dbo.NNTNGT",
                c => new
                    {
                        MaNNTNGT = c.Int(nullable: false),
                        MaDTNNTNGT = c.Int(nullable: false),
                        TenNNTNGT = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.MaNNTNGT)
                .ForeignKey("dbo.DTNNTNGT", t => t.MaDTNNTNGT, cascadeDelete: true)
                .Index(t => t.MaDTNNTNGT);
            
            CreateTable(
                "dbo.DTTV",
                c => new
                    {
                        MaDTTV = c.Int(nullable: false),
                        TenDTTV = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.MaDTTV);
            
            CreateTable(
                "dbo.DVBC",
                c => new
                    {
                        MaDVBC = c.Int(nullable: false),
                        TenDVBC = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.MaDVBC);
            
            CreateTable(
                "dbo.DVCTN",
                c => new
                    {
                        MaDVCTN = c.Int(nullable: false),
                        TenDVCTN = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.MaDVCTN);
            
            CreateTable(
                "dbo.DVNBC",
                c => new
                    {
                        MaDVNBC = c.Int(nullable: false),
                        TenDVNBC = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.MaDVNBC);
            
            CreateTable(
                "dbo.HTBV",
                c => new
                    {
                        MaHTBV = c.Int(nullable: false),
                        TenHTBV = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.MaHTBV);
            
            CreateTable(
                "dbo.HTVC",
                c => new
                    {
                        MaHTVC = c.Int(nullable: false),
                        TenHTVC = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.MaHTVC);
            
            CreateTable(
                "dbo.LoaiDuong",
                c => new
                    {
                        MaLD = c.Int(nullable: false),
                        TenLD = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.MaLD);
            
            CreateTable(
                "dbo.LoaiTaiNan",
                c => new
                    {
                        MaLTN = c.Int(nullable: false),
                        TenLTN = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.MaLTN);
            
            CreateTable(
                "dbo.NhomTuoi",
                c => new
                    {
                        MaNT = c.Int(nullable: false),
                        TenNT = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.MaNT);
            
            CreateTable(
                "dbo.PTKATKT",
                c => new
                    {
                        MaPTKATKT = c.Int(nullable: false),
                        TenPTKATKT = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.MaPTKATKT);
            
            CreateTable(
                "dbo.PhuongTien",
                c => new
                    {
                        MaPT = c.Int(nullable: false),
                        TenPT = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.MaPT);
            
            CreateTable(
                "dbo.PhuongXa",
                c => new
                    {
                        MaPX = c.Int(nullable: false),
                        MaQH = c.Int(nullable: false),
                        TenPX = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.MaPX)
                .ForeignKey("dbo.QuanHuyen", t => t.MaQH, cascadeDelete: true)
                .Index(t => t.MaQH);
            
            CreateTable(
                "dbo.QuanHuyen",
                c => new
                    {
                        MaQH = c.Int(nullable: false),
                        MaTTP = c.Int(nullable: false),
                        TenQH = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.MaQH)
                .ForeignKey("dbo.TinhThanhPho", t => t.MaTTP, cascadeDelete: true)
                .Index(t => t.MaTTP);
            
            CreateTable(
                "dbo.TaiNan",
                c => new
                    {
                        TaiNanID = c.Int(nullable: false, identity: true),
                        SoBC = c.String(maxLength: 50),
                        MaDVCTN = c.Int(nullable: false),
                        TGTN = c.DateTime(nullable: false),
                        DiaChi = c.String(maxLength: 50),
                        SoHH = c.Int(nullable: false),
                        MaTD = c.Int(nullable: false),
                        MaQH = c.Int(nullable: false),
                        MaPX = c.Int(nullable: false),
                        MaPT = c.Int(nullable: false),
                        MaLTN = c.Int(nullable: false),
                        MaHTVC = c.Int(nullable: false),
                        MaLD = c.Int(nullable: false),
                        MaNNTNGT = c.Int(nullable: false),
                        TomTatSoBo = c.String(),
                        Lat = c.Decimal(nullable: false, precision: 11, scale: 6),
                        Lng = c.Decimal(nullable: false, precision: 11, scale: 6),
                    })
                .PrimaryKey(t => t.TaiNanID)
                .ForeignKey("dbo.DVCTN", t => t.MaDVCTN, cascadeDelete: true)
                .ForeignKey("dbo.HTVC", t => t.MaHTVC, cascadeDelete: true)
                .ForeignKey("dbo.LoaiDuong", t => t.MaLD, cascadeDelete: true)
                .ForeignKey("dbo.LoaiTaiNan", t => t.MaLTN, cascadeDelete: true)
                .ForeignKey("dbo.NNTNGT", t => t.MaNNTNGT, cascadeDelete: true)
                .ForeignKey("dbo.PhuongTien", t => t.MaPT, cascadeDelete: true)
                .ForeignKey("dbo.PhuongXa", t => t.MaPX, cascadeDelete: true)
                .ForeignKey("dbo.QuanHuyen", t => t.MaQH)
                .ForeignKey("dbo.TuyenDuong", t => t.MaTD, cascadeDelete: true)
                .Index(t => t.MaDVCTN)
                .Index(t => t.MaTD)
                .Index(t => t.MaQH)
                .Index(t => t.MaPX)
                .Index(t => t.MaPT)
                .Index(t => t.MaLTN)
                .Index(t => t.MaHTVC)
                .Index(t => t.MaLD)
                .Index(t => t.MaNNTNGT);
            
            CreateTable(
                "dbo.TaiNanThuongVong",
                c => new
                    {
                        TaiNanThuongVongID = c.Int(nullable: false, identity: true),
                        TaiNanID = c.Int(nullable: false),
                        MaDTTV = c.Int(nullable: false),
                        MaTV = c.Int(nullable: false),
                        MaNT = c.Int(nullable: false),
                        MaHTBV = c.Int(nullable: false),
                        SoTV = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TaiNanThuongVongID)
                .ForeignKey("dbo.DTTV", t => t.MaDTTV, cascadeDelete: true)
                .ForeignKey("dbo.HTBV", t => t.MaHTBV, cascadeDelete: true)
                .ForeignKey("dbo.NhomTuoi", t => t.MaNT, cascadeDelete: true)
                .ForeignKey("dbo.TaiNan", t => t.TaiNanID, cascadeDelete: true)
                .ForeignKey("dbo.ThuongVong", t => t.MaTV, cascadeDelete: true)
                .Index(t => t.TaiNanID)
                .Index(t => t.MaDTTV)
                .Index(t => t.MaTV)
                .Index(t => t.MaNT)
                .Index(t => t.MaHTBV);
            
            CreateTable(
                "dbo.ThuongVong",
                c => new
                    {
                        MaTV = c.Int(nullable: false),
                        TyLeTV = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.MaTV);
            
            CreateTable(
                "dbo.TuyenDuong",
                c => new
                    {
                        MaTD = c.Int(nullable: false),
                        TenTD = c.String(maxLength: 100),
                        TrongDiem = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MaTD);
            
            CreateTable(
                "dbo.TinhThanhPho",
                c => new
                    {
                        MaTTP = c.Int(nullable: false),
                        TenTTP = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.MaTTP);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PhuongXa", "MaQH", "dbo.QuanHuyen");
            DropForeignKey("dbo.QuanHuyen", "MaTTP", "dbo.TinhThanhPho");
            DropForeignKey("dbo.TaiNan", "MaTD", "dbo.TuyenDuong");
            DropForeignKey("dbo.TaiNanThuongVong", "MaTV", "dbo.ThuongVong");
            DropForeignKey("dbo.TaiNanThuongVong", "TaiNanID", "dbo.TaiNan");
            DropForeignKey("dbo.TaiNanThuongVong", "MaNT", "dbo.NhomTuoi");
            DropForeignKey("dbo.TaiNanThuongVong", "MaHTBV", "dbo.HTBV");
            DropForeignKey("dbo.TaiNanThuongVong", "MaDTTV", "dbo.DTTV");
            DropForeignKey("dbo.TaiNan", "MaQH", "dbo.QuanHuyen");
            DropForeignKey("dbo.TaiNan", "MaPX", "dbo.PhuongXa");
            DropForeignKey("dbo.TaiNan", "MaPT", "dbo.PhuongTien");
            DropForeignKey("dbo.TaiNan", "MaNNTNGT", "dbo.NNTNGT");
            DropForeignKey("dbo.TaiNan", "MaLTN", "dbo.LoaiTaiNan");
            DropForeignKey("dbo.TaiNan", "MaLD", "dbo.LoaiDuong");
            DropForeignKey("dbo.TaiNan", "MaHTVC", "dbo.HTVC");
            DropForeignKey("dbo.TaiNan", "MaDVCTN", "dbo.DVCTN");
            DropForeignKey("dbo.NNTNGT", "MaDTNNTNGT", "dbo.DTNNTNGT");
            DropIndex("dbo.TaiNanThuongVong", new[] { "MaHTBV" });
            DropIndex("dbo.TaiNanThuongVong", new[] { "MaNT" });
            DropIndex("dbo.TaiNanThuongVong", new[] { "MaTV" });
            DropIndex("dbo.TaiNanThuongVong", new[] { "MaDTTV" });
            DropIndex("dbo.TaiNanThuongVong", new[] { "TaiNanID" });
            DropIndex("dbo.TaiNan", new[] { "MaNNTNGT" });
            DropIndex("dbo.TaiNan", new[] { "MaLD" });
            DropIndex("dbo.TaiNan", new[] { "MaHTVC" });
            DropIndex("dbo.TaiNan", new[] { "MaLTN" });
            DropIndex("dbo.TaiNan", new[] { "MaPT" });
            DropIndex("dbo.TaiNan", new[] { "MaPX" });
            DropIndex("dbo.TaiNan", new[] { "MaQH" });
            DropIndex("dbo.TaiNan", new[] { "MaTD" });
            DropIndex("dbo.TaiNan", new[] { "MaDVCTN" });
            DropIndex("dbo.QuanHuyen", new[] { "MaTTP" });
            DropIndex("dbo.PhuongXa", new[] { "MaQH" });
            DropIndex("dbo.NNTNGT", new[] { "MaDTNNTNGT" });
            DropTable("dbo.TinhThanhPho");
            DropTable("dbo.TuyenDuong");
            DropTable("dbo.ThuongVong");
            DropTable("dbo.TaiNanThuongVong");
            DropTable("dbo.TaiNan");
            DropTable("dbo.QuanHuyen");
            DropTable("dbo.PhuongXa");
            DropTable("dbo.PhuongTien");
            DropTable("dbo.PTKATKT");
            DropTable("dbo.NhomTuoi");
            DropTable("dbo.LoaiTaiNan");
            DropTable("dbo.LoaiDuong");
            DropTable("dbo.HTVC");
            DropTable("dbo.HTBV");
            DropTable("dbo.DVNBC");
            DropTable("dbo.DVCTN");
            DropTable("dbo.DVBC");
            DropTable("dbo.DTTV");
            DropTable("dbo.NNTNGT");
            DropTable("dbo.DTNNTNGT");
            DropTable("dbo.DTLVP");
        }
    }
}
