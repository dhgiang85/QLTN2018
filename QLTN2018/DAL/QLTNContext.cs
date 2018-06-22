using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using QLTN2018.Models;

namespace QLTN2018.DAL
{
    public class QLTNContext : DbContext
    {
        public QLTNContext() : base("QLTNContext")
        {

        }
        public DbSet<HTVC> HTVCs { get; set; }
        public DbSet<HTBV> HTBVs { get; set; }
        public DbSet<DTLVP> DTLVPs { get; set; }    //not use
        public DbSet<DTNNTNGT> DTNNTNGTs { get; set; }
        public DbSet<DVCTN> DVCTNs { get; set; }
        public DbSet<DVNBC> DVNBCs { get; set; } //not use
        public DbSet<DVBC> DVBCs { get; set; }  //not use
        public DbSet<NNTNGT> NNTNGTs { get; set; }
        public DbSet<LoaiDuong> LoaiDuongs { get; set; }
        public DbSet<PhuongTien> PhuongTiens { get; set; }
        public DbSet<PTKATKT> PTKATKTs { get; set; }    //not use

        public DbSet<ThuongVong> ThuongVongs { get; set; }
        public DbSet<TuyenDuong> TuyenDuongs { get; set; }
        public DbSet<LoaiTaiNan> LoaiTaiNans { get; set; }
        public DbSet<NhomTuoi> NhomTuois { get; set; }
        public DbSet<DTTV> DTTVs { get; set; }
        public DbSet<TinhThanhPho> TinhThanhPhos { get; set; }
        public DbSet<QuanHuyen> QuanHuyens { get; set; }
        public DbSet<PhuongXa> PhuongXas { get; set; }
        public DbSet<TaiNan> TaiNans { get; set; }
        public DbSet<TaiNanThuongVong> TaiNanThuongVongs { get; set; }

        public DbSet<DiemDen> DiemDens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<TaiNan>().Property(x => x.Lat).HasPrecision(11, 7);
            modelBuilder.Entity<TaiNan>().Property(x => x.Lng).HasPrecision(11, 7);
            modelBuilder.Entity<DiemDen>().Property(x => x.Lat).HasPrecision(11, 7);
            modelBuilder.Entity<DiemDen>().Property(x => x.Lng).HasPrecision(11, 7);
            modelBuilder.Entity<DiemDen>().Property(x => x.Radius).HasPrecision(18, 7);
            modelBuilder.Entity<TaiNan>()
                .HasRequired(c => c.QuanHuyen)
                .WithMany(d => d.TaiNans)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiNan>()
                .HasRequired(c => c.DTNNTNGT)
                .WithMany(d => d.TaiNans)
                .WillCascadeOnDelete(false);
        }
    }
}