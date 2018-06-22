

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTN2018.Models
{
    public class DVNBC
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaDVNBC { get; set; }

        [StringLength(100)]
        public string TenDVNBC { get; set; }
    }
}