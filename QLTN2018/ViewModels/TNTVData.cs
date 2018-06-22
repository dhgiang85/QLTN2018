
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using QLTN2018.Models;

namespace QLTN2018.ViewModels
{
    public class TNTVData
    {
        public int TaiNanID { get; set; }
        [StringLength(50)]
        public string SoBC { get; set; }
        public int SoHH { get; set; }
        public int SoBT { get; set; }
        public int SoTV { get; set; }
        
        public int MaDVCTN { get; set; }
        [StringLength(100)]
        public string TenDVCTN { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime TGTN { get; set; }

        public string DiaChi { get; set; }
        
        public int MaTD { get; set; }
        public string TenTD { get; set; }
        public int MaPT { get; set; }
        [StringLength(100)]
        public string TenPT { get; set; }


        public int MaLTN { get; set; }
        [StringLength(100)]
        public string TenLTN { get; set; }

        [StringLength(100)]
        public string TenHTVC { get; set; }

        public int MaLD { get; set; }
        [StringLength(100)]
        public string TenLD { get; set; }


        public int MaDTNNTNGT { get; set; }
        [StringLength(100)]
        public string TenDTNNTNGT { get; set; }

        [StringLength(100)]
        public string TenNNTNGT { get; set; }

        public string TomTatSoBo { get; set; }

        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
    }
}