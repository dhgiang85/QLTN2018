

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTN2018.Models
{
    public class DVCTN
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaDVCTN { get; set; }

        [StringLength(100)]
        public string TenDVCTN { get; set; }
    }
}