
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace QLTN2018.Models
{
    public class PhuongTien
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaPT { get; set; }

        [StringLength(100)]
        public string TenPT { get; set; }
    }
}