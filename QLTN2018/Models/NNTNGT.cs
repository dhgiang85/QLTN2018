using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTN2018.Models
{
    public class NNTNGT
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 0)]
        //[Column(TypeName = "VARCHAR")]
        //[StringLength(15)]
        public int MaNNTNGT { get; set; }
        
        [Column(Order = 1)]
        [ForeignKey("DTNNTNGT")]
        public int MaDTNNTNGT { get; set; }
        public virtual DTNNTNGT DTNNTNGT { get; set; }

        [Column(Order = 2)]
        [StringLength(100)]
        public string TenNNTNGT { get; set; }
    }
}