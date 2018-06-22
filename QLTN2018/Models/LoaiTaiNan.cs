

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTN2018.Models
{
    public class LoaiTaiNan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaLTN { get; set; }

        [StringLength(100)]
        public string TenLTN { get; set; }
    }
}