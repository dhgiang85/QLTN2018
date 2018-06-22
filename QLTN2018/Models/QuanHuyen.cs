

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTN2018.Models
{
    public class QuanHuyen
    {
        [Column(Order =0)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaQH { get; set; }

        [Column(Order = 1)]
        [ForeignKey("TinhThanhPho")]
        public int MaTTP { get; set; }
        public virtual TinhThanhPho TinhThanhPho { get; set; }

        [Column(Order = 2)]
        [StringLength(100)]
        public string TenQH { get; set; }
        
        public ICollection<PhuongXa> PhuongXas { get; set; }
        public ICollection<TaiNan> TaiNans { get; set; }
    }
}