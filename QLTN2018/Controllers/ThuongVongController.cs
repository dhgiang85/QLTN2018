using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLTN2018.DAL;
using QLTN2018.Models;

namespace QLTN2018.Controllers
{
    public class ThuongVongController : Controller
    {
        private QLTNContext db = new QLTNContext();

        // GET: ThuongVong
        public async Task<ActionResult> Index()
        {
            var taiNanThuongVong = db.TaiNanThuongVongs.Include(t => t.DTTV).Include(t => t.HTBV).Include(t => t.NhomTuoi).Include(t => t.TaiNan).Include(t => t.ThuongVong);
            return View(await taiNanThuongVong.ToListAsync());
        }

        // GET: ThuongVong/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiNanThuongVong taiNanThuongVong = await db.TaiNanThuongVongs.FindAsync(id);
            if (taiNanThuongVong == null)
            {
                return HttpNotFound();
            }
            return View(taiNanThuongVong);
        }

        // GET: ThuongVong/Create
        public ActionResult Create()
        {
            ViewBag.MaDTTV = new SelectList(db.DTTVs, "MaDTTV", "TenDTTV");
            ViewBag.MaHTBV = new SelectList(db.HTBVs, "MaHTBV", "TenHTBV");
            ViewBag.MaNT = new SelectList(db.NhomTuois, "MaNT", "TenNT");
            ViewBag.TaiNanID = new SelectList(db.TaiNans, "TaiNanID", "SoBC");
            ViewBag.MaTV = new SelectList(db.ThuongVongs, "MaTV", "TyLeTV");
            return View();
        }

        // POST: ThuongVong/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TaiNanThuongVongID,TaiNanID,MaDTTV,MaTV,MaNT,MaHTBV,SoTV")] TaiNanThuongVong taiNanThuongVong)
        {
            if (ModelState.IsValid)
            {
                db.TaiNanThuongVongs.Add(taiNanThuongVong);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MaDTTV = new SelectList(db.DTTVs, "MaDTTV", "TenDTTV", taiNanThuongVong.MaDTTV);
            ViewBag.MaHTBV = new SelectList(db.HTBVs, "MaHTBV", "TenHTBV", taiNanThuongVong.MaHTBV);
            ViewBag.MaNT = new SelectList(db.NhomTuois, "MaNT", "TenNT", taiNanThuongVong.MaNT);
            ViewBag.TaiNanID = new SelectList(db.TaiNans, "TaiNanID", "SoBC", taiNanThuongVong.TaiNanID);
            ViewBag.MaTV = new SelectList(db.ThuongVongs, "MaTV", "TyLeTV", taiNanThuongVong.MaTV);
            return View(taiNanThuongVong);
        }

        // GET: ThuongVong/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiNanThuongVong taiNanThuongVong = await db.TaiNanThuongVongs.FindAsync(id);
            if (taiNanThuongVong == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDTTV = new SelectList(db.DTTVs, "MaDTTV", "TenDTTV", taiNanThuongVong.MaDTTV);
            ViewBag.MaHTBV = new SelectList(db.HTBVs, "MaHTBV", "TenHTBV", taiNanThuongVong.MaHTBV);
            ViewBag.MaNT = new SelectList(db.NhomTuois, "MaNT", "TenNT", taiNanThuongVong.MaNT);
            ViewBag.TaiNanID = new SelectList(db.TaiNans, "TaiNanID", "SoBC", taiNanThuongVong.TaiNanID);
            ViewBag.MaTV = new SelectList(db.ThuongVongs, "MaTV", "TyLeTV", taiNanThuongVong.MaTV);
            return View(taiNanThuongVong);
        }

        // POST: ThuongVong/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TaiNanThuongVongID,TaiNanID,MaDTTV,MaTV,MaNT,MaHTBV,SoTV")] TaiNanThuongVong taiNanThuongVong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taiNanThuongVong).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MaDTTV = new SelectList(db.DTTVs, "MaDTTV", "TenDTTV", taiNanThuongVong.MaDTTV);
            ViewBag.MaHTBV = new SelectList(db.HTBVs, "MaHTBV", "TenHTBV", taiNanThuongVong.MaHTBV);
            ViewBag.MaNT = new SelectList(db.NhomTuois, "MaNT", "TenNT", taiNanThuongVong.MaNT);
            ViewBag.TaiNanID = new SelectList(db.TaiNans, "TaiNanID", "SoBC", taiNanThuongVong.TaiNanID);
            ViewBag.MaTV = new SelectList(db.ThuongVongs, "MaTV", "TyLeTV", taiNanThuongVong.MaTV);
            return View(taiNanThuongVong);
        }

        // GET: ThuongVong/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiNanThuongVong taiNanThuongVong = await db.TaiNanThuongVongs.FindAsync(id);
            if (taiNanThuongVong == null)
            {
                return HttpNotFound();
            }
            return View(taiNanThuongVong);
        }

        // POST: ThuongVong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TaiNanThuongVong taiNanThuongVong = await db.TaiNanThuongVongs.FindAsync(id);
            db.TaiNanThuongVongs.Remove(taiNanThuongVong);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
