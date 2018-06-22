
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace QLTN2018.Models
{
    public class NhomTuoi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaNT { get; set; }

        [StringLength(100)]
        public string TenNT { get; set; }
    }
}