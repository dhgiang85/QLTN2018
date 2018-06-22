
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTN2018.Models
{
    public class DTNNTNGT
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int MaDTNNTNGT { get; set; }

      
        [StringLength(100)]
        public string TenDTNNTNGT { get; set; }

      
        public ICollection<NNTNGT> NNTNGTs { get; set; }
        public ICollection<TaiNan> TaiNans { get; set; }
    }
}