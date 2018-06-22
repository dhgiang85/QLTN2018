

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTN2018.Models
{
    public class TinhThanhPho
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaTTP { get; set; }
        
        [StringLength(100)]
        public string TenTTP { get; set; }

        public ICollection<QuanHuyen> QuanHuyens { get; set; }
    }
}