using System;

using System.Collections.Generic;

using System.Data.Entity;
using System.EnterpriseServices;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
using QLTN2018.DAL;
using QLTN2018.Models;

using QLTN2018.ViewModels;

namespace QLTN2018.Controllers
{
    public class TaiNanController : Controller
    {
        private QLTNContext db = new QLTNContext();

        // GET: TaiNan
        public async Task<ActionResult> Index()
        {
            var taiNans = (from tn in db.TaiNans
                        join tntv in db.TaiNanThuongVongs on tn.TaiNanID equals tntv.TaiNanID into ps
                        from tntv in ps.DefaultIfEmpty()            //get informat when tntv is null.
                        group new { tn, tntv } by new { tn.TaiNanID } into grp
                        let _tn = grp.FirstOrDefault().tn
                        let _SoBT = (int?)grp.Where(x => x.tntv.ThuongVong.MaTV > 1).Sum(c => c.tntv.SoTV) ?? 0
                        let _SoTV = (int?)grp.Where(x => x.tntv.ThuongVong.MaTV == 1).Sum(c => c.tntv.SoTV) ?? 0
                        select new TNTVData()
                        {
                            TaiNanID = _tn.TaiNanID,
                            SoBC = _tn.SoBC,
                            TenDVCTN = _tn.DVCTN.TenDVCTN,
                            TGTN = _tn.TGTN,
                            DiaChi =
                                _tn.DiaChi + " " + _tn.TuyenDuong.TenTD + ", " + _tn.PhuongXa.TenPX + ", " +
                                _tn.QuanHuyen.TenQH,
                            TenPT = _tn.PhuongTien.TenPT,
                            TenLTN = _tn.LoaiTaiNan.TenLTN,
                            TenHTVC = _tn.HTVC.TenHTVC,
                            TenLD = _tn.LoaiDuong.TenLD,
                            TenDTNNTNGT = _tn.DTNNTNGT.TenDTNNTNGT,
                            TenNNTNGT = _tn.NNTNGT.TenNNTNGT,
                            TomTatSoBo = _tn.TomTatSoBo,
                            Lat = _tn.Lat,
                            Lng = _tn.Lng,
                            SoHH = _tn.SoHH,
                            SoBT = _SoBT,
                            SoTV = _SoTV,
                        }).OrderByDescending(x => x.TGTN);

            return View(await taiNans.ToListAsync());
        }

        // GET: TaiNan/Details/5
       

        // GET: TaiNan/Create
        [ValidateInput(false)]
        public ActionResult Create()
        {
            var taiNan = new TaiNan();
            taiNan.TGTN = DateTime.Now;
            taiNan.Lat = 10.7643952M;
            taiNan.Lng = 106.6908047M;
            taiNan.TaiNanThuongVongs = new List<TaiNanThuongVong>();
            ViewBag.MaDVCTN = new SelectList(db.DVCTNs, "MaDVCTN", "TenDVCTN");
            ViewBag.MaHTVC = new SelectList(db.HTVCs, "MaHTVC", "TenHTVC");
            ViewBag.MaLD = new SelectList(db.LoaiDuongs, "MaLD", "TenLD");
            ViewBag.MaLTN = new SelectList(db.LoaiTaiNans, "MaLTN", "TenLTN");
            ViewBag.MaNNTNGT = new SelectList(NNList(1), "MaNNTNGT", "TenNNTNGT");
            ViewBag.MaDTNNTNGT = new SelectList(db.DTNNTNGTs, "MaDTNNTNGT", "TenDTNNTNGT");
            ViewBag.MaPT = new SelectList(db.PhuongTiens, "MaPT", "TenPT");
            ViewBag.MaPX = new SelectList(PXList(1), "MaPX", "TenPX");
            ViewBag.MaTD = new SelectList(db.TuyenDuongs, "MaTD", "TenTD");
            ViewBag.MaQH = new SelectList(db.QuanHuyens, "MaQH", "TenQH");
            return View(taiNan);
        }

        // POST: TaiNan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(
                Include =
                    "TaiNanID,MaDVCTN,TGTN,DiaChi,SoHH,MaTD,MaQH,MaPX,MaPT,MaLTN,MaHTVC,MaLD,MaDTNNTNGT,MaNNTNGT,TomTatSoBo,Lat,Lng,TaiNanThuongVongs"
                )] TaiNan taiNan)
        {
            //[Bind(Include = "TaiNanID,SoBC,MaDVCTN,TGTN,DiaChi,SoHH,MaTD,MaQH,MaPX,MaPT,MaLTN,MaHTVC,MaLD,MaDTNNTNGT,MaNNTNGT,TomTatSoBo,Lat,Lng")] TaiNan taiNan
            if (ModelState.IsValid)
            {
                var g = Guid.NewGuid();
               
                taiNan.SoBC = "MCST"+ taiNan.TGTN.ToString("yyMMdd")+"_"+g.ToString().Split('-')[1];
                taiNan.DonViNhap = "MCST";
                db.TaiNans.Add(taiNan);
                await db.SaveChangesAsync();
                return RedirectToAction("DSTN");
            }

            ViewBag.MaDVCTN = new SelectList(db.DVCTNs, "MaDVCTN", "TenDVCTN", taiNan.MaDVCTN);
            ViewBag.MaHTVC = new SelectList(db.HTVCs, "MaHTVC", "TenHTVC", taiNan.MaHTVC);
            ViewBag.MaLD = new SelectList(db.LoaiDuongs, "MaLD", "TenLD", taiNan.MaLD);
            ViewBag.MaLTN = new SelectList(db.LoaiTaiNans, "MaLTN", "TenLTN", taiNan.MaLTN);
            ViewBag.MaNNTNGT = new SelectList(NNList(taiNan.MaDTNNTNGT), "MaNNTNGT", "TenNNTNGT", taiNan.MaNNTNGT);
            ViewBag.MaDTNNTNGT = new SelectList(db.DTNNTNGTs, "MaDTNNTNGT", "TenDTNNTNGT", taiNan.MaDTNNTNGT);
            ViewBag.MaPT = new SelectList(db.PhuongTiens, "MaPT", "TenPT", taiNan.MaPT);
            ViewBag.MaPX = new SelectList(PXList(taiNan.MaQH), "MaPX", "TenPX", taiNan.MaPX);
            ViewBag.MaTD = new SelectList(db.TuyenDuongs, "MaTD", "TenTD", taiNan.MaTD);
            ViewBag.MaQH = new SelectList(db.QuanHuyens, "MaQH", "TenQH", taiNan.MaQH);

            ViewBag.ListDTTV = db.DTTVs.ToList();
            ViewBag.ListNT = db.NhomTuois.ToList();
            ViewBag.ListTV = db.ThuongVongs.ToList();
            ViewBag.ListHTBV = db.HTBVs.ToList();
            return View(taiNan);
        }

        // GET: TaiNan/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiNan taiNan = await db.TaiNans.FindAsync(id);
  
            if (taiNan == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDVCTN = new SelectList(db.DVCTNs, "MaDVCTN", "TenDVCTN", taiNan.MaDVCTN);
            ViewBag.MaHTVC = new SelectList(db.HTVCs, "MaHTVC", "TenHTVC", taiNan.MaHTVC);
            ViewBag.MaLD = new SelectList(db.LoaiDuongs, "MaLD", "TenLD", taiNan.MaLD);
            ViewBag.MaLTN = new SelectList(db.LoaiTaiNans, "MaLTN", "TenLTN", taiNan.MaLTN);
            ViewBag.MaNNTNGT = new SelectList(NNList(taiNan.MaDTNNTNGT), "MaNNTNGT", "TenNNTNGT", taiNan.MaNNTNGT);
            ViewBag.MaDTNNTNGT = new SelectList(db.DTNNTNGTs, "MaDTNNTNGT", "TenDTNNTNGT", taiNan.MaDTNNTNGT);
            ViewBag.MaPT = new SelectList(db.PhuongTiens, "MaPT", "TenPT", taiNan.MaPT);
            ViewBag.MaPX = new SelectList(PXList(taiNan.MaQH), "MaPX", "TenPX", taiNan.MaPX);
            ViewBag.MaTD = new SelectList(db.TuyenDuongs, "MaTD", "TenTD", taiNan.MaTD);
            ViewBag.MaQH = new SelectList(db.QuanHuyens, "MaQH", "TenQH", taiNan.MaQH);

            ViewBag.ListDTTV = db.DTTVs.ToList();
            ViewBag.ListNT = db.NhomTuois.ToList();
            ViewBag.ListTV = db.ThuongVongs.ToList();
            ViewBag.ListHTBV = db.HTBVs.ToList();
            return View(taiNan);
        }

        // POST: TaiNan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TaiNan taiNan)
        {
            if (ModelState.IsValid)
            {
                foreach (var tntv in taiNan.TaiNanThuongVongs)
                {
                    if (tntv.TaiNanID != 0)
                    {
                        db.Entry(tntv).State = EntityState.Modified;
                    }
                    else
                    {
                        tntv.TaiNanID = taiNan.TaiNanID;
                        db.TaiNanThuongVongs.Add(tntv);
                    }
                }
                db.Entry(taiNan).State = EntityState.Modified;
                var ListTNTVID = new HashSet<int>(taiNan.TaiNanThuongVongs.Select(x=>x.TaiNanThuongVongID));
                foreach (var tntv in db.TaiNanThuongVongs.Where(x=>x.TaiNanID==taiNan.TaiNanID))
                {
                    if (!ListTNTVID.Contains(tntv.TaiNanThuongVongID))
                    {
                        db.TaiNanThuongVongs.Remove(tntv);
                    }
                }

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MaDVCTN = new SelectList(db.DVCTNs, "MaDVCTN", "TenDVCTN", taiNan.MaDVCTN);
            ViewBag.MaHTVC = new SelectList(db.HTVCs, "MaHTVC", "TenHTVC", taiNan.MaHTVC);
            ViewBag.MaLD = new SelectList(db.LoaiDuongs, "MaLD", "TenLD", taiNan.MaLD);
            ViewBag.MaLTN = new SelectList(db.LoaiTaiNans, "MaLTN", "TenLTN", taiNan.MaLTN);
            ViewBag.MaNNTNGT = new SelectList(NNList(taiNan.MaDTNNTNGT), "MaNNTNGT", "TenNNTNGT", taiNan.MaNNTNGT);
            ViewBag.MaDTNNTNGT = new SelectList(db.DTNNTNGTs, "MaDTNNTNGT", "TenDTNNTNGT", taiNan.MaDTNNTNGT);
            ViewBag.MaPT = new SelectList(db.PhuongTiens, "MaPT", "TenPT", taiNan.MaPT);
            ViewBag.MaPX = new SelectList(PXList(taiNan.MaQH), "MaPX", "TenPX", taiNan.MaPX);
            ViewBag.MaTD = new SelectList(db.TuyenDuongs, "MaTD", "TenTD", taiNan.MaTD);
            ViewBag.MaQH = new SelectList(db.QuanHuyens, "MaQH", "TenQH", taiNan.MaQH);

            ViewBag.ListDTTV = db.DTTVs.ToList();
            ViewBag.ListNT = db.NhomTuois.ToList();
            ViewBag.ListTV = db.ThuongVongs.ToList();
            ViewBag.ListHTBV = db.HTBVs.ToList();
            return View(taiNan);
        }

        
        public ActionResult DeleteConfirmed(int tnID)
        {
            try
            {
                var status = false;
                var rslt = db.TaiNans.Single(x => x.TaiNanID==tnID);
                if (rslt != null)
                {
                    db.TaiNans.Remove(rslt);
                    db.SaveChanges();
                    status = true;
                    return new JsonResult { Data = new { status = status } };
                }
                else
                {
                    return HttpNotFound();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public ActionResult TvEntryRow()
        {
            var tntv = new TaiNanThuongVong();
            ViewBag.ListDTTV = db.DTTVs.ToList();
            ViewBag.ListNT = db.NhomTuois.ToList();
            ViewBag.ListTV = db.ThuongVongs.ToList();
            ViewBag.ListHTBV = db.HTBVs.ToList();
            return PartialView("_ThuongVongChiTiet", tntv);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public JsonResult GetPX(string QHID)
        {

            int id = Convert.ToInt32(QHID);
            return Json(new SelectList(PXList(id), "MaPX", "TenPX"), JsonRequestBehavior.AllowGet);
        }

        public List<PhuongXa> PXList(int QHID)
        {
            var pxList = new List<PhuongXa>();
            pxList = db.PhuongXas.Where(s => s.QuanHuyen.MaQH == QHID).ToList();
            return pxList;
        }


        public JsonResult GetNN(string DTNNID)
        {
            int id = Convert.ToInt32(DTNNID);
            return Json(new SelectList(NNList(id), "MaNNTNGT", "TenNNTNGT"), JsonRequestBehavior.AllowGet);
        }

        public List<NNTNGT> NNList(int DTNNID)
        {
            var nnList = new List<NNTNGT>();
            nnList = db.NNTNGTs.Where(s => s.DTNNTNGT.MaDTNNTNGT == DTNNID).ToList();
            return nnList;
        }

        public ActionResult TVDetails(int? tnID)
        {
            try
            {
                List<TaiNanThuongVong> tntvs = db.TaiNanThuongVongs.Where(x => x.TaiNanID == tnID).ToList();
                var rslt = new List<ThuongVongDetail>();
                foreach (var  tntv in tntvs)
                {
                    rslt.Add(new ThuongVongDetail()
                    {
                        TenDTTV = tntv.DTTV.TenDTTV,
                        TenNT = tntv.NhomTuoi.TenNT,
                        TenHTBV = tntv.HTBV.TenHTBV,
                        SoTV = tntv.SoTV,
                        TyLeTV = tntv.ThuongVong.TyLeTV,
                        Nam = tntv.Nam
                    });
                }

                return Json(rslt, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public ActionResult GetDetails(int? tnID)
        {
            try
            {
                //var rslt = from tntv in db.TaiNanThuongVongs
                //    where tntv.TaiNanID == tnID
                //    group tntv by tntv.TaiNanID into grp
                //    join tn in db.TaiNans on grp.FirstOrDefault().TaiNanID equals tn.TaiNanID
                //    select new TNTVData()
                //    {
                //        TaiNanID = grp.FirstOrDefault().TaiNanID,
                //        SoBC = tn.SoBC,
                //        TenDVCTN = tn.DVCTN.TenDVCTN,
                //        TGTN = tn.TGTN,
                //        DiaChi = tn.DiaChi + " " + tn.TuyenDuong.TenTD + ", " + tn.PhuongXa.TenPX + ", " + tn.QuanHuyen.TenQH,
                //        TenPT = tn.PhuongTien.TenPT,
                //        TenLTN = tn.LoaiTaiNan.TenLTN,
                //        TenHTVC = tn.HTVC.TenHTVC,
                //        TenLD = tn.LoaiDuong.TenLD,
                //        TenDTNNTNGT = tn.DTNNTNGT.TenDTNNTNGT,
                //        TenNNTNGT = tn.NNTNGT.TenNNTNGT,
                //        TomTatSoBo = tn.TomTatSoBo,
                //        Lat = tn.Lat.ToString(),
                //        Lng = tn.Lng.ToString(),
                //        SoHH = tn.SoHH,
                //        SoBT = (int?)grp.Where(x => x.ThuongVong.MaTV > 1).Sum(c => c.SoTV) ?? 0,
                //        SoTV = (int?)grp.Where(x => x.ThuongVong.MaTV == 1).Sum(c => c.SoTV) ?? 0,
                //    };
                var rslt = (from tn in db.TaiNans
                    join tntv in db.TaiNanThuongVongs on tn.TaiNanID equals tntv.TaiNanID into ps
                    from tntv in ps.DefaultIfEmpty()            //get informat when tntv is null.
                    where tn.TaiNanID == tnID                   //get one enity
                    group new {tn, tntv} by new {tn.TaiNanID} into grp
                    let _tn = grp.FirstOrDefault().tn
                    let _SoBT = (int?) grp.Where(x => x.tntv.ThuongVong.MaTV > 1).Sum(c => c.tntv.SoTV) ?? 0
                    let _SoTV = (int?) grp.Where(x => x.tntv.ThuongVong.MaTV == 1).Sum(c => c.tntv.SoTV) ?? 0
                    select new TNTVData()
                    {
                        TaiNanID = _tn.TaiNanID,
                        SoBC = _tn.SoBC,
                        TenDVCTN = _tn.DVCTN.TenDVCTN,
                        TGTN = _tn.TGTN,
                        DiaChi =
                            _tn.DiaChi + " " + _tn.TuyenDuong.TenTD + ", " + _tn.PhuongXa.TenPX + ", " +
                            _tn.QuanHuyen.TenQH,
                        TenPT = _tn.PhuongTien.TenPT,
                        TenLTN = _tn.LoaiTaiNan.TenLTN,
                        TenHTVC = _tn.HTVC.TenHTVC,
                        TenLD = _tn.LoaiDuong.TenLD,
                        TenDTNNTNGT = _tn.DTNNTNGT.TenDTNNTNGT,
                        TenNNTNGT = _tn.NNTNGT.TenNNTNGT,
                        TomTatSoBo = _tn.TomTatSoBo,
                        Lat = _tn.Lat,
                        Lng = _tn.Lng,
                        SoHH = _tn.SoHH,
                        SoBT = _SoBT,
                        SoTV = _SoTV,
                    }).FirstOrDefault();
                return Json(rslt, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public ActionResult Map()
        {
            DateTime DateStart;
            DateTime DateEnd;

            DateTime date = DateTime.Now;
            DateStart = new DateTime(date.Year, date.Month, 1);
            DateEnd = date;


            ViewBag.dateStart = string.Format("{0:dd-MM-yyyy HH:mm}", DateStart);
            ViewBag.dateEnd = string.Format("{0:dd-MM-yyyy HH:mm}", DateEnd);


            ViewBag.MaDVCTN = new SelectList(db.DVCTNs, "MaDVCTN", "TenDVCTN");
            ViewBag.MaLD = new SelectList(db.LoaiDuongs, "MaLD", "TenLD");
            ViewBag.MaLTN = new SelectList(db.LoaiTaiNans, "MaLTN", "TenLTN");
            ViewBag.MaPT = new SelectList(db.PhuongTiens, "MaPT", "TenPT");
            ViewBag.MaDTNNTNGT = new SelectList(db.DTNNTNGTs, "MaDTNNTNGT", "TenDTNNTNGT");
            return View();
        }
        public ActionResult MapSearch(string dateStart, string dateEnd, string diaChi, string soBaoCao, int? MaDVCTN, int[] MaLTN, int[] MaPT, int[] MaLD, int[] MaDTNNTNGT, int Page, string UpdateZone)
        {
            try
            {
                DateTime DateStart;
                DateTime DateEnd;

                DateStart = DateTime.ParseExact(dateStart, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
                DateEnd = DateTime.ParseExact(dateEnd, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);

                var rslt = (from tn in db.TaiNans
                      
                            join tntv in db.TaiNanThuongVongs on tn.TaiNanID equals tntv.TaiNanID into ps
                            from tntv in ps.DefaultIfEmpty()
                                //get informat when tntv is null.
                            where tn.TGTN >= DateStart && tn.TGTN <= DateEnd
                            group new { tn, tntv } by new { tn.TaiNanID }
                    into grp
                            let _tn = grp.FirstOrDefault().tn
                            let _SoBT = (int?)grp.Where(x => x.tntv.ThuongVong.MaTV > 1).Sum(c => c.tntv.SoTV) ?? 0
                            let _SoTV = (int?)grp.Where(x => x.tntv.ThuongVong.MaTV == 1).Sum(c => c.tntv.SoTV) ?? 0
                            select new TNTVData()
                            {
                                TaiNanID = _tn.TaiNanID,
                                SoBC = _tn.SoBC,
                                MaDVCTN = _tn.MaDVCTN,
                                TenDVCTN = _tn.DVCTN.TenDVCTN,
                                TGTN = _tn.TGTN,
                                DiaChi =
                                    _tn.DiaChi + " " + _tn.TuyenDuong.TenTD + ", " + _tn.PhuongXa.TenPX + ", " +
                                    _tn.QuanHuyen.TenQH,
                                MaPT = _tn.MaPT,
                                TenPT = _tn.PhuongTien.TenPT,
                                MaLTN = _tn.MaLTN,
                                TenLTN = _tn.LoaiTaiNan.TenLTN,
                                TenHTVC = _tn.HTVC.TenHTVC,
                                MaLD = _tn.LoaiDuong.MaLD,
                                TenLD = _tn.LoaiDuong.TenLD,
                                MaDTNNTNGT = _tn.DTNNTNGT.MaDTNNTNGT,
                                TenDTNNTNGT = _tn.DTNNTNGT.TenDTNNTNGT,
                                TenNNTNGT = _tn.NNTNGT.TenNNTNGT,
                                TomTatSoBo = _tn.TomTatSoBo,
                                Lat = _tn.Lat,
                                Lng = _tn.Lng,
                                SoHH = _tn.SoHH,
                                SoBT = _SoBT,
                                SoTV = _SoTV,
                            });

                
                

                if (MaLTN != null)
                {
                    var ListLTN = new HashSet<int>(MaLTN);
                    rslt = rslt.Where(x => ListLTN.Contains(x.MaLTN));

                }
                if (MaPT != null)
                {
                    var ListPT = new HashSet<int>(MaPT);
                    rslt = rslt.Where(x => ListPT.Contains(x.MaPT));

                }
                if (MaDVCTN != null)
                {
                    rslt = rslt.Where(x => x.MaDVCTN == MaDVCTN);

                }
                if (MaLD != null)
                {
                    var ListLD = new HashSet<int>(MaLD);
                    rslt = rslt.Where(x => ListLD.Contains(x.MaLD));

                }
                if (MaDTNNTNGT != null)
                {
                    var ListDTNNTNGT = new HashSet<int>(MaDTNNTNGT);
                    rslt = rslt.Where(x => ListDTNNTNGT.Contains(x.MaDTNNTNGT));

                }
                if (!String.IsNullOrEmpty(diaChi))
                {
                    rslt = rslt.Where(x => x.DiaChi.ToUpper().Contains(diaChi.ToUpper()));

                }
                if (!String.IsNullOrEmpty(soBaoCao))
                {
                    rslt = rslt.Where(x => x.SoBC.ToUpper().Contains(soBaoCao.ToUpper()));
                }

                var all_items_query = rslt;
                var count_query = rslt;

                //var ListLTN = new HashSet<int>(MaLTN ?? new[] {1, 2, 3, 4, 5});
                //var ListPT = new HashSet<int>(MaPT ?? new[] {1, 2, 3, 4, 5, 6, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21});
                //var ListLD = new HashSet<int>(MaLD ?? new[] {1, 2, 3, 4, 5, 6, 8});
                //var ListDTNNTNGT = new HashSet<int>(MaDTNNTNGT ?? new[] { 1, 2, 3, 4, 5, 6, 8, 9, 10, 11, 12, 13 });

                //all_items_query = all_items_query.Where(x => ListLTN.Contains(x.MaLTN)
                //                 && ListPT.Contains(x.MaPT)
                //                 && ListLD.Contains(x.MaLD) 
                //                 && ListDTNNTNGT.Contains(x.MaDTNNTNGT)
                //                 && (x.MaDVCTN == MaDVCTN || MaDVCTN == null)
                //                 && (x.DiaChi.ToUpper().Contains(diaChi.ToUpper()) || diaChi == "")
                //                 && (x.SoBC.ToUpper().Contains(soBaoCao.ToUpper()) || soBaoCao == "")
                //      );
                //count_query = count_query.Where(x => ListLTN.Contains(x.MaLTN)
                //            && ListPT.Contains(x.MaPT) 
                //            && ListLD.Contains(x.MaLD)
                //            && ListDTNNTNGT.Contains(x.MaDTNNTNGT)
                //            && (x.MaDVCTN == MaDVCTN || MaDVCTN == null)
                //            && (x.DiaChi.ToUpper().Contains(diaChi.ToUpper()) || diaChi == "")
                //            && (x.SoBC.ToUpper().Contains(soBaoCao.ToUpper()) || soBaoCao == "")
                //);


                int cur_page = Page;
                bool previous_btn = true;
                bool next_btn = true;
                bool first_btn = true;
                bool last_btn = true;
                int per_page = 20; // default for mapsearch.js.
                int start = (Page-1) * per_page;

                var page_items_query = all_items_query.OrderByDescending(x => x.TGTN)
                                          .Skip(start)
                                          .Take(per_page);
                

                var pag_navigation = "";

                var count = count_query.Count();

                decimal nop_ceil = Decimal.Divide(count, per_page);
                int no_of_paginations = Convert.ToInt32(Math.Ceiling(nop_ceil));

                var start_loop = 1;
                var end_loop = no_of_paginations;
               

                if (cur_page >= 7)
                {
                    start_loop = cur_page - 3;
                    if (no_of_paginations > cur_page + 3)
                    {
                        end_loop = cur_page + 3;
                    }
                    else if (cur_page <= no_of_paginations && cur_page > no_of_paginations - 6)
                    {
                        start_loop = no_of_paginations - 6;
                        end_loop = no_of_paginations;
                    }
                }
                else
                {
                    if (no_of_paginations > 7)
                    {
                        end_loop = 7;
                    }
                }

                pag_navigation += "<ul>";

                if (first_btn && cur_page > 1)
                {
                    pag_navigation += "<li p='1' class='active'>First</li>";
                }
                else if (first_btn)
                {
                    pag_navigation += "<li p='1' class='inactive'>First</li>";
                }

                if (previous_btn && cur_page > 1)
                {
                    var pre = cur_page - 1;
                    pag_navigation += "<li p='" + pre + "' class='active'>Previous</li>";
                }
                else if (previous_btn)
                {
                    pag_navigation += "<li class='inactive'>Previous</li>";
                }

                if (start_loop > 1)
                {
                    pag_navigation += "<li class='disabled'>...</li>";
                }
                for (int i = start_loop; i <= end_loop; i++)
                {

                    if (cur_page == i)
                        pag_navigation += "<li p='" + i + "' class = 'selected' >" + i + "</li>";
                    else
                        pag_navigation += "<li p='" + i + "' class='active'>" + i + "</li>";
                }
                if (end_loop < no_of_paginations)
                {
                    pag_navigation += "<li class='disabled'>...</li>";
                }

                if (next_btn && cur_page < no_of_paginations)
                {
                    var nex = cur_page + 1;
                    pag_navigation += "<li p='" + nex + "' class='active'>Next</li>";
                }
                else if (next_btn)
                {
                    pag_navigation += "<li class='inactive'>Next</li>";
                }

                if (last_btn && cur_page < no_of_paginations)
                {
                    pag_navigation += "<li p='" + no_of_paginations + "' class='active'>Last</li>";
                }
                else if (last_btn)
                {
                    pag_navigation += "<li p='" + no_of_paginations + "' class='inactive'>Last</li>";
                }

                pag_navigation = pag_navigation + "</ul>";
                var pageitems = page_items_query.ToList();
           
                if (UpdateZone == "get-all-items")
                {
                    var allitems = all_items_query.OrderByDescending(x => x.TGTN).ToList();
                    return Json(new {allitems, pageitems, updateZone = "All", pag_navigation},
                        JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new {pageitems, updateZone = "Page", pag_navigation },
                        JsonRequestBehavior.AllowGet);
                }
                
            }
            catch (Exception e)
            {
                return null;
            }
        }

        
        public async Task<ActionResult> Report()
        {
            ViewBag.MaDVCTN = new SelectList(db.DVCTNs, "MaDVCTN", "TenDVCTN");
            ViewBag.MaLD = new SelectList(db.LoaiDuongs, "MaLD", "TenLD");
            ViewBag.MaLTN = new SelectList(db.LoaiTaiNans, "MaLTN", "TenLTN");
            ViewBag.MaPT = new SelectList(db.PhuongTiens, "MaPT", "TenPT");
            ViewBag.MaDTNNTNGT = new SelectList(db.DTNNTNGTs, "MaDTNNTNGT", "TenDTNNTNGT");
            return View();
        }
        public ActionResult ReportSearch(int monthStart, int monthEnd,int year ,string diaChi, string soBaoCao, int? MaDVCTN, int[] MaLTN, int[] MaPT, int[] MaLD, int[] MaDTNNTNGT, bool[] SearchOptions)
        {
            try
            {


                var rslt = (from tn in db.TaiNans
                            join tntv in db.TaiNanThuongVongs on tn.TaiNanID equals tntv.TaiNanID into ps
                            from tntv in ps.DefaultIfEmpty()
                                //get informat when tntv is null.
                            where
                                 tn.TGTN.Month >= monthStart && tn.TGTN.Month <= monthEnd &&
                                 (tn.TGTN.Year == year || tn.TGTN.Year == (year - 1))
                            group new { tn, tntv } by new { tn.TaiNanID }
                            into grp
                            let _tn = grp.FirstOrDefault().tn
                            let _SoBT = (int?)grp.Where(x => x.tntv.ThuongVong.MaTV > 1).Sum(c => c.tntv.SoTV) ?? 0
                            let _SoTV = (int?)grp.Where(x => x.tntv.ThuongVong.MaTV == 1).Sum(c => c.tntv.SoTV) ?? 0
                            select new TNTVData()
                            {
                                TaiNanID = _tn.TaiNanID,
                                SoBC = _tn.SoBC,
                                MaDVCTN = _tn.MaDVCTN,
                                TenDVCTN = _tn.DVCTN.TenDVCTN,
                                TGTN = _tn.TGTN,
                                MaTD = _tn.MaTD,
                                TenTD = _tn.TuyenDuong.TenTD,
                                DiaChi =
                                    _tn.DiaChi + " " + _tn.TuyenDuong.TenTD + ", " + _tn.PhuongXa.TenPX + ", " +
                                    _tn.QuanHuyen.TenQH,
                                MaPT = _tn.MaPT,
                                TenPT = _tn.PhuongTien.TenPT,
                                MaLTN = _tn.MaLTN,
                                TenLTN = _tn.LoaiTaiNan.TenLTN,
                                TenHTVC = _tn.HTVC.TenHTVC,
                                MaLD = _tn.LoaiDuong.MaLD,
                                TenLD = _tn.LoaiDuong.TenLD,
                                MaDTNNTNGT = _tn.DTNNTNGT.MaDTNNTNGT,
                                TenDTNNTNGT = _tn.DTNNTNGT.TenDTNNTNGT,
                                TenNNTNGT = _tn.NNTNGT.TenNNTNGT,
                                TomTatSoBo = _tn.TomTatSoBo,
                                Lat = _tn.Lat,
                                Lng = _tn.Lng,
                                SoHH = _tn.SoHH,
                                SoBT = _SoBT,
                                SoTV = _SoTV,
                            }).ToList();
               
                if (MaLTN != null)
                {
                    var ListLTN = new HashSet<int>(MaLTN);
                    rslt = rslt.Where(x => ListLTN.Contains(x.MaLTN)).ToList();
               
                }
                if (MaPT != null)
                {
                    var ListPT = new HashSet<int>(MaPT);
                    rslt = rslt.Where(x => ListPT.Contains(x.MaPT)).ToList();
                  
                }
                if (MaDVCTN != null)
                {
                    rslt = rslt.Where(x => x.MaDVCTN == MaDVCTN).ToList();
                 
                }
                if (MaLD != null)
                {
                    var ListLD = new HashSet<int>(MaLD);
                    rslt = rslt.Where(x => ListLD.Contains(x.MaLD)).ToList();
                 
                }
                if (MaDTNNTNGT != null)
                {
                    var ListDTNNTNGT = new HashSet<int>(MaDTNNTNGT);
                    rslt = rslt.Where(x => ListDTNNTNGT.Contains(x.MaDTNNTNGT)).ToList();
                
                }
                if (!String.IsNullOrEmpty(diaChi))
                {
                    rslt = rslt.Where(x => x.DiaChi.ToUpper().Contains(diaChi.ToUpper())).ToList();

                }
                if (!String.IsNullOrEmpty(soBaoCao))
                {
                    rslt = rslt.Where(x => x.SoBC.ToUpper().Contains(soBaoCao.ToUpper())).ToList();
                }

                var soTVData = new List<int>();
                var soTVData_y = new List<int>();
                var soTVDataM  = new List<int>();
                var soTVDataM_y = new List<int>();

                var soTVLabel = new List<string>();
                //var soTVLabel = new List<string>(new string[] { "Số hư hỏng", "Số bị thương", "Số tử vong" });

                var soTVLabelMonth = new List<string>();

                var iSoHH = SearchOptions[0] ? 1 : 0;
                var iSoBT = SearchOptions[1] ? 1 : 0;
                var iSoTV = SearchOptions[2] ? 1 : 0;

                if (SearchOptions[0])
                {
                    soTVData.Add(rslt.Where(x => x.TGTN.Year == year).Sum(x => x.SoHH));
                    soTVData_y.Add(rslt.Where(x => x.TGTN.Year == (year - 1)).Sum(x => x.SoHH));
                    soTVLabel.Add("Số hư hỏng");
                }
                if (SearchOptions[1])
                {
                    soTVData.Add(rslt.Where(x => x.TGTN.Year == year).Sum(x => x.SoBT));
                    soTVData_y.Add(rslt.Where(x => x.TGTN.Year == (year - 1)).Sum(x => x.SoBT));
                    soTVLabel.Add("Số bị thương");
                }
                if (SearchOptions[2])
                {
                    soTVData.Add(rslt.Where(x => x.TGTN.Year == year).Sum(x => x.SoTV));
                    soTVData_y.Add(rslt.Where(x => x.TGTN.Year == (year - 1)).Sum(x => x.SoTV));
                    soTVLabel.Add("Số tử vong");
                }

                //soTVData.Add(rslt.Where(x => x.TGTN.Year == year).Sum(x => x.SoHH));
                //soTVData.Add(rslt.Where(x => x.TGTN.Year == year).Sum(x => x.SoBT));
                //soTVData.Add(rslt.Where(x => x.TGTN.Year == year).Sum(x => x.SoTV));

                //soTVData_y.Add(rslt.Where(x => x.TGTN.Year == (year - 1)).Sum(x => x.SoHH));
                //soTVData_y.Add(rslt.Where(x => x.TGTN.Year == (year - 1)).Sum(x => x.SoBT));
                //soTVData_y.Add(rslt.Where(x => x.TGTN.Year == (year - 1)).Sum(x => x.SoTV));

                var soVuDataM = new List<int>();
                var soVuDataM_y = new List<int>();

                for (int i = monthStart; i <= monthEnd; i++)
                {
                    soTVDataM.Add(
                        rslt.Where(x => x.TGTN.Month == i && x.TGTN.Year == year)
                            .Sum(x => iSoHH*x.SoHH + iSoBT*x.SoBT + iSoTV*x.SoTV));
                    soTVDataM_y.Add(
                        rslt.Where(x => x.TGTN.Month == i && x.TGTN.Year == (year - 1))
                            .Sum(x => iSoHH*x.SoHH + iSoBT*x.SoBT + iSoTV*x.SoTV));

                    soVuDataM.Add(rslt.Count(x => (x.SoTV > 0 || !SearchOptions[4]) && (x.SoTV > 0 || x.SoBT > 0 || !SearchOptions[3])
                                    && x.TGTN.Year == year && x.TGTN.Month == i));

                    soVuDataM_y.Add(rslt.Count(x => (x.SoTV > 0 || !SearchOptions[4]) && (x.SoTV > 0 || x.SoBT > 0 || !SearchOptions[3])
                                    && x.TGTN.Year == (year - 1) && x.TGTN.Month == i));

                    soTVLabelMonth.Add("tháng " + i);
                }
                var soVuData = new List<int>();
                soVuData.Add(rslt.Count(x => (x.SoTV > 0 || !SearchOptions[4]) && (x.SoTV > 0 || x.SoBT > 0 || !SearchOptions[3])
                                    && x.TGTN.Year == (year-1)));

                soVuData.Add(rslt.Count(x => (x.SoTV > 0 || !SearchOptions[4]) && (x.SoTV > 0 || x.SoBT > 0 || !SearchOptions[3])
                                    && x.TGTN.Year == year));

                var rsltTDcur = from tncur in rslt.Where(x => x.TGTN.Year == year)
                    group tncur by tncur.MaTD
                    into grp
                    select new ReportTDData()
                    {
                        MaTD = grp.Key,
                        TenTD = grp.FirstOrDefault().TenTD,
                        SoVu = grp.Count(),
                        SoBT = grp.Sum(x => x.SoBT),
                        SoHH = grp.Sum(x => x.SoHH),
                        SoTV = grp.Sum(x => x.SoTV),
                    };
                var rsltTDcomp = (from tncur in rslt.Where(x => x.TGTN.Year == (year - 1))
                    group tncur by tncur.MaTD
                    into grp
                    select new ReportTDData()
                    {
                        MaTD = grp.Key,
                        TenTD = grp.FirstOrDefault().TenTD,
                        SoVu = grp.Count(),
                        SoBT = grp.Sum(x => x.SoBT),
                        SoHH = grp.Sum(x => x.SoHH),
                        SoTV = grp.Sum(x => x.SoTV),
                    });
     
                List<ReportTDDataComp> rsltTD_lf = (from curYear in rsltTDcur
                    join compYear in rsltTDcomp
                        on curYear.MaTD equals compYear.MaTD into ps
                    from compYear in
                        ps.DefaultIfEmpty(new ReportTDData()
                        {
                            MaTD = curYear.MaTD,
                            SoVu = 0,
                            SoBT = 0,
                            SoTV = 0,
                            SoHH = 0
                        })
                    select new ReportTDDataComp()
                    {
                        MaTD = curYear.MaTD,
                        TenTD = curYear.TenTD,
                        SoVuCur = curYear.SoVu,
                        SoVuComp = compYear.SoVu,
                        SoHHCur = curYear.SoHH,
                        SoHHComp = compYear.SoHH,
                        SoBTCur = curYear.SoBT,
                        SoBTComp = compYear.SoBT,
                        SoTVCur = curYear.SoTV,
                        SoTVComp = compYear.SoTV,
                    }).ToList();

                List<ReportTDDataComp> rsltTD_rt = (from compYear in rsltTDcomp
                    join curYear in rsltTDcur
                        on compYear.MaTD equals curYear.MaTD into ps
                    from curYear in
                        ps.DefaultIfEmpty(new ReportTDData()
                        {
                            MaTD = compYear.MaTD,
                            SoVu = 0,
                            SoBT = 0,
                            SoTV = 0,
                            SoHH = 0
                        })
                    select new ReportTDDataComp()
                    {
                        MaTD = compYear.MaTD,
                        TenTD = compYear.TenTD,
                        SoVuCur = curYear.SoVu,
                        SoVuComp = compYear.SoVu,
                        SoHHCur = curYear.SoHH,
                        SoHHComp = compYear.SoHH,
                        SoBTCur = curYear.SoBT,
                        SoBTComp = compYear.SoBT,
                        SoTVCur = curYear.SoTV,
                        SoTVComp = compYear.SoTV,
                    }).ToList();
                
                var TDData = rsltTD_lf.Union(rsltTD_rt).DistinctBy(x=>x.MaTD).ToList();


                return Json(new
                {
                    TVData = soTVData,
                    TVLabel = soTVLabel,
                    TVDataY = soTVData_y,
                    label = year.ToString(),
                    labelY = (year - 1).ToString(),
                    soTVDataM,
                    soTVDataM_y,
                    soVuData,
                    soVuDataM,
                    soVuDataM_y,
                    TDData,
                    soTVLabelMonth
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return null;
            }
        }


        [HttpGet]
        public ActionResult DSTN()
        {
            /* No logic required here, let's just render the view */
            return View();
        }

        [HttpPost]
        public string DSTN(FormCollection collection)
        {
            /* Setup default variables that we are going to populate later */
            var pag_content = "";
            var pag_navigation = "";

            /* Define all posted data coming from the view. */
            int page = Convert.ToInt32(collection["data[page]"]); /* Page we are currently at */
            string sort = collection["data[sort]"] == "ASC" ? "asc" : "desc"; /* Order of our sort (DESC or ASC) */
            string name = collection["data[name]"]; /* Name of the column name we want to sort */
            int max = Convert.ToInt32(collection["data[max]"]); /* Number of items to display per page */
            string search = collection["data[search]"]; /* Keyword provided on our search box */

            int cur_page = page;
            page -= 1;
            int per_page = max > 1 ? max : 10;

            bool previous_btn = true;
            bool next_btn = true;
            bool first_btn = true;
            bool last_btn = true;
            int start = page * per_page;
            var thsort = "";
            /* Let's build the query using available data that we received form the front-end via ajax */
            var rslt = (from tn in db.TaiNans
                    join tntv in db.TaiNanThuongVongs on tn.TaiNanID equals tntv.TaiNanID into ps
                    from tntv in ps.DefaultIfEmpty()
                    //get informat when tntv is null.
                    group new {tn, tntv} by new {tn.TaiNanID}
                    into grp
                    let _tn = grp.FirstOrDefault().tn
                    let _SoBT = (int?) grp.Where(x => x.tntv.ThuongVong.MaTV > 1).Sum(c => c.tntv.SoTV) ?? 0
                    let _SoTV = (int?) grp.Where(x => x.tntv.ThuongVong.MaTV == 1).Sum(c => c.tntv.SoTV) ?? 0
                    select new TNTVData()
                    {
                        TaiNanID = _tn.TaiNanID,
                        SoBC = _tn.SoBC,
                        MaDVCTN = _tn.MaDVCTN,
                        TenDVCTN = _tn.DVCTN.TenDVCTN,
                        TGTN = _tn.TGTN,
                        DiaChi =
                            _tn.DiaChi + " " + _tn.TuyenDuong.TenTD + ", " + _tn.PhuongXa.TenPX + ", " +
                            _tn.QuanHuyen.TenQH,
                        MaPT = _tn.MaPT,
                        TenPT = _tn.PhuongTien.TenPT,
                        MaLTN = _tn.MaLTN,
                        TenLTN = _tn.LoaiTaiNan.TenLTN,
                        TenHTVC = _tn.HTVC.TenHTVC,
                        MaLD = _tn.LoaiDuong.MaLD,
                        TenLD = _tn.LoaiDuong.TenLD,
                        MaDTNNTNGT = _tn.DTNNTNGT.MaDTNNTNGT,
                        TenDTNNTNGT = _tn.DTNNTNGT.TenDTNNTNGT,
                        TenNNTNGT = _tn.NNTNGT.TenNNTNGT,
                        TomTatSoBo = _tn.TomTatSoBo,
                        Lat = _tn.Lat,
                        Lng = _tn.Lng,
                        SoHH = _tn.SoHH,
                        SoBT = _SoBT,
                        SoTV = _SoTV,
                    });

            
            
            switch (name)
            {
                case "TGTN":
                    thsort += "<th>Số BC</span></th><th class='active'>Thời gian<span class='fa'></th><th>Loại</th><th>Đơn vị</th><th>Địa chỉ</th>";
                    break;
                case "MaDVCTN":
                    thsort += "<th>Số BC</th><th>Thời gian<span class='fa'</span></th><th>Loại</th><th class='active'>Đơn vị<span class='fa'></span></th><th>Địa chỉ</th>";
                    break;
                case "MaLTN":
                    thsort += "<th>Số BC</th><th>Thời gian<span class='fa'></span></th><th class='active'>Loại<span class='fa'></span></th><th>Đơn vị</th><th>Địa chỉ</th>";
                    break;
            }

            var all_items_query = rslt;
            /* Get total items in our database */
            var count_query = rslt; /* Get total products count. */

            /* If there is a search keyword, we search through the database for possible matches*/
            if (search != "")
            {
                /* The "Contains" method matches records using the LIKE %keyword% format */
               all_items_query = all_items_query.Where(x =>
                    x.SoBC.ToUpper().Contains(search.ToUpper()) ||
                    x.TomTatSoBo.ToUpper().Contains(search.ToUpper()) ||
                    x.DiaChi.ToUpper().Contains(search.ToUpper())
                );
                count_query = count_query.Where(x =>
                    x.SoBC.ToUpper().Contains(search.ToUpper()) ||
                    x.TomTatSoBo.ToUpper().Contains(search.ToUpper()) ||
                    x.DiaChi.ToUpper().Contains(search.ToUpper())
                );

            }
            all_items_query = all_items_query.OrderByField(name, sort == "asc")
                                     .Skip(start)
                                     .Take(per_page); /* Get only the products to display. */
            /* We now fetch the data from our database */
            var all_items = all_items_query.ToList();
            int count = count_query.Count();


            if (count > 0)
            {
                pag_content +=
                    "<div class='table-responsive'><table class='table table-striped table-bordered table-condensed table-hover table-post-list'>" +
                    "<tr><th></th>"+ thsort +
                    "<th>Nguyên nhân</th><th>Phương tiện</th><th>HH</th><th>BT</th><th>TV</th></tr>";
                /* Loop through each item to create views */
                foreach (var item in all_items)
                {
                    pag_content += "<tr id='Row_" + item.TaiNanID + "' >" +
                                        "<td class='width-5 text-center' style='vertical-align: middle;' >" +
                                                "<div class='btn-group btn-group-sm' role='group' aria-label='...'>"+
                                                    "<div class='btn-group ' role='group' aria-label='Hiển thị chi tiết'>"+
                                                        "<a onclick = 'RowDetails(" + item.TaiNanID + ")' class='parents js-view-parents' data-href='formation_json_parents' data-toggle='tooltip' data-placement='top' alt='Hiển thị chi tiết' title='Hiển thị chi tiết' >" +
                                                            "<span class='fa fa-plus-circle fa-lg' aria-hidden='true' style='margin: 4px;cursor: pointer;'  id='sRow_" + item.TaiNanID + "'></span>" +
                                                        "</a>"+
                                                        "<a class='details-open'></a>"+
                                                    "</div>"+
                                                   "<div class='btn-group' role='group' aria-label='chỉnh sửa' id='trdelete_"+item.TaiNanID+"'>"+
                                                       "<a onclick = 'Details("+item.TaiNanID+ ")' alt='Thông tin thêm' data-toggle='tooltip' data-id=" + item.TaiNanID + " data-placement='right' title='Chỉnh sửa'>" +
                                                            "<span class='fa fa-map-marker fa-lg' aria-hidden='true' style='margin: 4px;cursor: pointer;'></span>"+
                                                        "</a>"+
                                                   "</div>"+
                                               "</div>"+
                                        "</td>"+
                                        "<td>"+item.SoBC+"</td>"+
                                        "<td class='width-10'>"+item.TGTN.ToString("dd/MM/yy HH:mm") +"</td>"+
                                        "<td class='width-12'>"+item.TenLTN + "</td>"+
                                        "<td class='width-7'>"+item.TenDVCTN+"</td>"+
                                        "<td>"+item.DiaChi+"</td>"+
                                        "<td>"+item.TenDTNNTNGT+"</td>"+
                                        "<td>"+item.TenPT+"</td>"+
                                        "<td>"+item.SoHH+"</td>"+
                                        "<td>"+item.SoBT+"</td>"+
                                        "<td>"+item.SoTV+"</td>"+
                                    "</tr>";
                }
                pag_content += "</table></div>";
            }
            else
            {
                /* Show a message if no items were found */
                pag_content += "<p class='p-d bg-danger'>No items found</p>";
            }

            //pag_content = pag_content + "<br class = 'clear' />";

            /* Bellow is the navigation logic and view */
            decimal nop_ceil = Decimal.Divide(count, per_page);
            int no_of_paginations = Convert.ToInt32(Math.Ceiling(nop_ceil));

            var start_loop = 1;
            var end_loop = no_of_paginations;

            if (cur_page >= 7)
            {
                start_loop = cur_page - 3;
                if (no_of_paginations > cur_page + 3)
                {
                    end_loop = cur_page + 3;
                }
                else if (cur_page <= no_of_paginations && cur_page > no_of_paginations - 6)
                {
                    start_loop = no_of_paginations - 6;
                    end_loop = no_of_paginations;
                }
            }
            else
            {
                if (no_of_paginations > 7)
                {
                    end_loop = 7;
                }
            }

            pag_navigation += "<ul>";

            if (first_btn && cur_page > 1)
            {
                pag_navigation += "<li p='1' class='active'>First</li>";
            }
            else if (first_btn)
            {
                pag_navigation += "<li p='1' class='inactive'>First</li>";
            }

            if (previous_btn && cur_page > 1)
            {
                var pre = cur_page - 1;
                pag_navigation += "<li p='" + pre + "' class='active'>Previous</li>";
            }
            else if (previous_btn)
            {
                pag_navigation += "<li class='inactive'>Previous</li>";
            }
            if (start_loop > 1)
            {
                pag_navigation += "<li class='disabled'>...</li>";
            }
            for (int i = start_loop; i <= end_loop; i++)
            {

                if (cur_page == i)
                    pag_navigation += "<li p='" + i + "' class = 'selected' >" + i + "</li>";
                else
                    pag_navigation += "<li p='" + i + "' class='active'>" + i + "</li>";
            }
            if (end_loop < no_of_paginations)
            {
                pag_navigation += "<li class='disabled'>...</li>";
            }
            if (next_btn && cur_page < no_of_paginations)
            {
                var nex = cur_page + 1;
                pag_navigation += "<li p='" + nex + "' class='active'>Next</li>";
            }
            else if (next_btn)
            {
                pag_navigation += "<li class='inactive'>Next</li>";
            }

            if (last_btn && cur_page < no_of_paginations)
            {
                pag_navigation += "<li p='" + no_of_paginations + "' class='active'>Last</li>";
            }
            else if (last_btn)
            {
                pag_navigation += "<li p='" + no_of_paginations + "' class='inactive'>Last</li>";
            }

            pag_navigation = pag_navigation + "</ul>";

            /* Lets put our variables in a dictionary */
            var response = new Dictionary<string, string>
            {
                {"content", pag_content},
                {"navigation", pag_navigation}
            };

            /* Then we return the Dictionary in json format to our front-end */
            string json = new JavaScriptSerializer().Serialize(response);
            return json;
        }
        public ActionResult MapDiemDen()
        {
            DateTime DateStart;
            DateTime DateEnd;

            DateTime date = DateTime.Now;
            DateStart = new DateTime(date.Year, date.Month, 1);
            DateEnd = date;


            ViewBag.dateStart = string.Format("{0:dd-MM-yyyy HH:mm}", DateStart);
            ViewBag.dateEnd = string.Format("{0:dd-MM-yyyy HH:mm}", DateEnd);

            return View();
        }
        public ActionResult MapDiemDenSearch(string dateStart, string dateEnd, int Page, string UpdateZone)
        {
            try
            {
                DateTime DateStart;
                DateTime DateEnd;

                DateStart = DateTime.ParseExact(dateStart, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
                DateEnd = DateTime.ParseExact(dateEnd, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);

                var rslt = (from tn in db.TaiNans

                            join tntv in db.TaiNanThuongVongs on tn.TaiNanID equals tntv.TaiNanID into ps
                            from tntv in ps.DefaultIfEmpty()
                                //get informat when tntv is null.
                            where (tn.TGTN >= DateStart && tn.TGTN <= DateEnd ) 
                            group new { tn, tntv } by new { tn.TaiNanID }
                    into grp
                            let _tn = grp.FirstOrDefault().tn
                            let _SoBT = (int?)grp.Where(x => x.tntv.ThuongVong.MaTV > 1).Sum(c => c.tntv.SoTV) ?? 0
                            let _SoTV = (int?)grp.Where(x => x.tntv.ThuongVong.MaTV == 1).Sum(c => c.tntv.SoTV) ?? 0
                            select new TNTVData()
                            {
                                TaiNanID = _tn.TaiNanID,
                                SoBC = _tn.SoBC,
                                MaDVCTN = _tn.MaDVCTN,
                                TenDVCTN = _tn.DVCTN.TenDVCTN,
                                TGTN = _tn.TGTN,
                                DiaChi =
                                    _tn.DiaChi + " " + _tn.TuyenDuong.TenTD + ", " + _tn.PhuongXa.TenPX + ", " +
                                    _tn.QuanHuyen.TenQH,
                                MaPT = _tn.MaPT,
                                TenPT = _tn.PhuongTien.TenPT,
                                MaLTN = _tn.MaLTN,
                                TenLTN = _tn.LoaiTaiNan.TenLTN,
                                TenHTVC = _tn.HTVC.TenHTVC,
                                MaLD = _tn.LoaiDuong.MaLD,
                                TenLD = _tn.LoaiDuong.TenLD,
                                MaDTNNTNGT = _tn.DTNNTNGT.MaDTNNTNGT,
                                TenDTNNTNGT = _tn.DTNNTNGT.TenDTNNTNGT,
                                TenNNTNGT = _tn.NNTNGT.TenNNTNGT,
                                TomTatSoBo = _tn.TomTatSoBo,
                                Lat = _tn.Lat,
                                Lng = _tn.Lng,
                                SoHH = _tn.SoHH,
                                SoBT = _SoBT,
                                SoTV = _SoTV,
                            });
                

                var all_items_query = rslt;
                var count_query = rslt;

                int cur_page = Page;
                bool previous_btn = true;
                bool next_btn = true;
                bool first_btn = true;
                bool last_btn = true;
                int per_page = 20; // default for mapsearch.js.
                int start = (Page - 1) * per_page;

                var page_items_query = all_items_query.OrderByDescending(x => x.TGTN)
                                          .Skip(start)
                                          .Take(per_page);


                var pag_navigation = "";

                var count = count_query.Count();

                decimal nop_ceil = Decimal.Divide(count, per_page);
                int no_of_paginations = Convert.ToInt32(Math.Ceiling(nop_ceil));

                var start_loop = 1;
                var end_loop = no_of_paginations;


                if (cur_page >= 7)
                {
                    start_loop = cur_page - 3;
                    if (no_of_paginations > cur_page + 3)
                    {
                        end_loop = cur_page + 3;
                    }
                    else if (cur_page <= no_of_paginations && cur_page > no_of_paginations - 6)
                    {
                        start_loop = no_of_paginations - 6;
                        end_loop = no_of_paginations;
                    }
                }
                else
                {
                    if (no_of_paginations > 7)
                    {
                        end_loop = 7;
                    }
                }

                pag_navigation += "<ul>";

                if (first_btn && cur_page > 1)
                {
                    pag_navigation += "<li p='1' class='active'>First</li>";
                }
                else if (first_btn)
                {
                    pag_navigation += "<li p='1' class='inactive'>First</li>";
                }

                if (previous_btn && cur_page > 1)
                {
                    var pre = cur_page - 1;
                    pag_navigation += "<li p='" + pre + "' class='active'>Previous</li>";
                }
                else if (previous_btn)
                {
                    pag_navigation += "<li class='inactive'>Previous</li>";
                }

                if (start_loop > 1)
                {
                    pag_navigation += "<li class='disabled'>...</li>";
                }
                for (int i = start_loop; i <= end_loop; i++)
                {

                    if (cur_page == i)
                        pag_navigation += "<li p='" + i + "' class = 'selected' >" + i + "</li>";
                    else
                        pag_navigation += "<li p='" + i + "' class='active'>" + i + "</li>";
                }
                if (end_loop < no_of_paginations)
                {
                    pag_navigation += "<li class='disabled'>...</li>";
                }

                if (next_btn && cur_page < no_of_paginations)
                {
                    var nex = cur_page + 1;
                    pag_navigation += "<li p='" + nex + "' class='active'>Next</li>";
                }
                else if (next_btn)
                {
                    pag_navigation += "<li class='inactive'>Next</li>";
                }

                if (last_btn && cur_page < no_of_paginations)
                {
                    pag_navigation += "<li p='" + no_of_paginations + "' class='active'>Last</li>";
                }
                else if (last_btn)
                {
                    pag_navigation += "<li p='" + no_of_paginations + "' class='inactive'>Last</li>";
                }

                pag_navigation = pag_navigation + "</ul>";
                var pageitems = page_items_query.ToList();

                if (UpdateZone == "get-all-items")
                {
                    var allitems = all_items_query.OrderByDescending(x => x.TGTN).ToList();
                    return Json(new { allitems, pageitems, updateZone = "All", pag_navigation },
                        JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { pageitems, updateZone = "Page", pag_navigation },
                        JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception e)
            {
                return null;
            }
        }
       
    }
}
