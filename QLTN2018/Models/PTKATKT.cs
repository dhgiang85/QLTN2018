

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTN2018.Models
{
    public class PTKATKT
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaPTKATKT { get; set; }

        [StringLength(100)]
        public string TenPTKATKT { get; set; }
    }
}