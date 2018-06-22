

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTN2018.Models
{
    public class TuyenDuong
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaTD { get; set; }

        [StringLength(100)]
        public string TenTD { get; set; }

        public bool TrongDiem { get; set; }
    }
}