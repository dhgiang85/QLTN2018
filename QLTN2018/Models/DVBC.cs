
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace QLTN2018.Models
{
    public class DVBC
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaDVBC { get; set; }

        [StringLength(100)]
        public string TenDVBC { get; set; }
    }
}