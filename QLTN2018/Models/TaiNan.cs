
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTN2018.Models
{
    public class TaiNan
    {

        public int TaiNanID { get; set; }

        [StringLength(50)]
        public string SoBC { get; set; }

        [ForeignKey("DVCTN")]
        public int MaDVCTN { get; set; }
        public virtual DVCTN DVCTN { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime TGTN { get; set; }

        [StringLength(50)]
        public string DiaChi { get; set; }

        [Range(0, 100)]
        public int SoHH { get; set; }

        [ForeignKey("TuyenDuong")]
        public int MaTD { get; set; }
        public virtual TuyenDuong TuyenDuong { get; set; }

        //[ForeignKey("TinhThanhPho")]
        //public int MaTTP { get; set; }
        //public virtual TinhThanhPho TinhThanhPho { get; set; }

        [ForeignKey("QuanHuyen")]
        public int MaQH { get; set; }
        public virtual QuanHuyen QuanHuyen { get; set; }

        [ForeignKey("PhuongXa")]
        public int MaPX { get; set; }
        public virtual PhuongXa PhuongXa { get; set; }

        [ForeignKey("PhuongTien")]
        public int MaPT { get; set; }
        public virtual PhuongTien PhuongTien { get; set; }

        [ForeignKey("LoaiTaiNan")]
        public int MaLTN { get; set; }
        public virtual LoaiTaiNan LoaiTaiNan { get; set; }

        [ForeignKey("HTVC")]
        public int MaHTVC { get; set; }
        public virtual HTVC HTVC { get; set; }

        [ForeignKey("LoaiDuong")]
        public int MaLD { get; set; }
        public virtual LoaiDuong LoaiDuong { get; set; }

        [ForeignKey("NNTNGT")]
        public int? MaNNTNGT { get; set; }
        public virtual NNTNGT NNTNGT { get; set; }

        [ForeignKey("DTNNTNGT")]
        public int MaDTNNTNGT { get; set; }
        public virtual DTNNTNGT DTNNTNGT { get; set; }
        public string TomTatSoBo { get; set; }

        //[DisplayFormat(DataFormatString = "{0:F6}", ApplyFormatInEditMode = true)]
        public decimal Lat { get; set; }

        //[DisplayFormat(DataFormatString = "{0:F6}", ApplyFormatInEditMode = true)]
        public decimal Lng { get; set; }

        [StringLength(20)]
        public string DonViNhap { get; set; }

        [StringLength(100)]
        public string NguoiNhap { get; set; }
        public virtual ICollection<TaiNanThuongVong> TaiNanThuongVongs { get; set; }

    }
}