using QLTN2018.DAL;
using QLTN2018.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace QLTN2018.Controllers
{
    public class DiemDenController : Controller
    {
        private QLTNContext db = new QLTNContext();


        public async Task<ActionResult> Index()
        {
            var listDiemDen = await db.DiemDens.ToListAsync();
            return View(listDiemDen);
        }
        [ValidateInput(false)]
        public ActionResult Create()
        {
            var diemDen = new DiemDen();
            diemDen.DateCreated = DateTime.Now;
            diemDen.Lat = 10.7643952M;
            diemDen.Lng = 106.6908047M;
            diemDen.Radius = 50.0M;
            return View(diemDen);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(
            [Bind(Include = "ID, Name, Note, DVCT, Address, Lat, Lng, Radius, DateCreated")]DiemDen diemDen)
        {
            if (ModelState.IsValid)
            {

                diemDen.DateModified = DateTime.Now;
                db.DiemDens.Add(diemDen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DiemDen dd)
        {
            if (ModelState.IsValid)
            {
                dd.DateModified = DateTime.Now;
                db.Entry(dd).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
      
            return View(dd);
        }


        public ActionResult GetListDiemDen()
        {
            try
            {
 
                var rslt = db.DiemDens.Select(x => new
                {
                    Lat = x.Lat,
                    Lng = x.Lng,
                    Radius = x.Radius
                }).ToList();
                return  Json(new {Data =  rslt },JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiemDen dd = await db.DiemDens.FindAsync(id);

            if (dd == null)
            {
                return HttpNotFound();
            }
          
            return View(dd);
        }


        public ActionResult GetDetails(int? ddID)
        {

            var rslt = db.DiemDens.Where(x=>x.ID == ddID).FirstOrDefault();
            return Json(rslt, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteConfirmed(int ddID)
        {
            try
            {
                var status = false;
                var rslt = db.DiemDens.Single(x => x.ID == ddID);
                if (rslt != null)
                {
                    db.DiemDens.Remove(rslt);
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
    }
}