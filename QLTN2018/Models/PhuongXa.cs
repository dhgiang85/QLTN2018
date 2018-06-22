

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTN2018.Models
{
    public class PhuongXa
    {
        [Column(Order = 0)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaPX { get; set; }

        [Column(Order = 1)]
        [ForeignKey("QuanHuyen")]
        public int MaQH { get; set; }
        public virtual QuanHuyen QuanHuyen { get; set; }

        [Column(Order = 2)]
        [StringLength(100)]
        public string TenPX { get; set; }
    }
}