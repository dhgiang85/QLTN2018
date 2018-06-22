using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTN2018.Models
{
    public class TaiNanThuongVong
    {
        public TaiNanThuongVong()
        {
            MaDTTV = MaTV = MaNT = MaHTBV = SoTV = 1;
            Nam = false;
        }
        
        [Key]
        public int TaiNanThuongVongID { get; set; }


        [ForeignKey("TaiNan")]
        public int TaiNanID { get; set; }
        public virtual TaiNan TaiNan { get; set; }

        [ForeignKey("DTTV")]
        public int MaDTTV { get; set; }
        public virtual DTTV DTTV { get; set; }

        [ForeignKey("ThuongVong")]
        public int MaTV { get; set; }
        public virtual ThuongVong ThuongVong { get; set; }

        [ForeignKey("NhomTuoi")]
        public int MaNT { get; set; }
        public virtual NhomTuoi NhomTuoi { get; set; }

        [ForeignKey("HTBV")]
        public int MaHTBV { get; set; }
        public virtual HTBV HTBV { get; set; }

        [Range(1, 100)]
        public int SoTV { get; set; }

        public bool Nam { get; set; }

        

    }
}