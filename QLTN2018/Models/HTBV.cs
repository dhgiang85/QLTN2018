
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace QLTN2018.Models
{
    public class HTBV
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaHTBV { get; set; }

        [StringLength(100)]
        public string TenHTBV { get; set; }

    }
}