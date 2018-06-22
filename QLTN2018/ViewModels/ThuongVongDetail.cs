

using System.ComponentModel.DataAnnotations;

namespace QLTN2018.ViewModels
{
    public class ThuongVongDetail
    {
        [StringLength(100)]
        public string TenDTTV { get; set; }

        [StringLength(100)]
        public string TenNT { get; set; }

        [StringLength(100)]
        public string TenHTBV { get; set; }

        [StringLength(100)]
        public string TyLeTV { get; set; }

        public int SoTV { get; set; }

        public bool Nam { get; set; }
    }
}