

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTN2018.Models
{
    public class LoaiDuong
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaLD { get; set; }

        [StringLength(100)]
        public string TenLD { get; set; }
    }
}