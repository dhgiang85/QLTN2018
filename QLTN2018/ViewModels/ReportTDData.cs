using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLTN2018.ViewModels
{
    public class ReportTDData
    {
        public int MaTD { get; set; }
        public string TenTD { get; set; }
        public int SoVu { get; set; }
        
        public int SoHH { get; set; }
        
        public int SoBT { get; set; }
        
        public int SoTV { get; set; }

    }
    public class ReportTDDataComp
    {
        public int MaTD { get; set; }
        public string TenTD { get; set; }
        public int SoVuCur { get; set; }
        public int SoVuComp { get; set; }

        public int SoHHCur { get; set; }
        public int SoHHComp { get; set; }

        public int SoBTCur { get; set; }
        public int SoBTComp { get; set; }

        public int SoTVCur { get; set; }
        public int SoTVComp { get; set; }
    }
}