using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QLTN2018.Models
{
    public class DTTV
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaDTTV { get; set; }

        [StringLength(100)]
        public string TenDTTV { get; set; }
    }
}