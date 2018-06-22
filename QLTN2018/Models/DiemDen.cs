using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QLTN2018.Models
{
    public class DiemDen
    {
        public int ID { get; set; }

        //[DisplayFormat(DataFormatString = "{0:F6}", ApplyFormatInEditMode = true)]
        public decimal Lat { get; set; }

        //[DisplayFormat(DataFormatString = "{0:F6}", ApplyFormatInEditMode = true)]
        public decimal Lng { get; set; }

        [Required]
        public decimal Radius { get; set; }

        [StringLength(128)]
        [Required]
        public string Name { get; set; }

        [StringLength(128)]
        public string Address { get; set; }

        public string Note { get; set; }

        [StringLength(128)]
        public string DVCT { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }


    }
}