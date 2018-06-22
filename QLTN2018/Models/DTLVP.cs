using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTN2018.Models
{
    public class DTLVP
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaDTLVP { get; set; }

        [StringLength(100)]
        public string TenDTLVP { get; set; }
    }
}