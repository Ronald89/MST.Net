using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MST.Net.Models;
using PagedList;

namespace MST.Net.Controllers
{
    public class KodeposController : Controller
    {
        private mst_netEntities db = new mst_netEntities();

        // GET: Kodepos
        public ActionResult Index(string Sorting_Order, string Search_Prov, string Search_Kab, int? Page_No)
        {
            Sorting_Order = String.IsNullOrEmpty(Sorting_Order) ? "" : Sorting_Order;
            ViewBag.CurrentSortOrder = Sorting_Order;
            ViewBag.FilterProv = Search_Prov;
            ViewBag.FilterKab = Search_Kab;

            var kodepos = from kode in db.Kodepos select kode;

            if (Search_Prov != null)
            {
                //Page_No = 1;
                kodepos = from kode in db.Kodepos select kode;
                {
                    kodepos = kodepos.Where(kode => kode.provinsi.ToUpper().Contains(Search_Prov.ToUpper()));
                }
                
            }

            if (!String.IsNullOrEmpty(Search_Kab))
            {
                //Page_No = 1;
                kodepos = from kode in db.Kodepos select kode;
                {
                    kodepos = kodepos.Where(kode => kode.provinsi.ToUpper().Contains(Search_Prov.ToUpper())
                    && kode.kabupaten.ToUpper().Contains(Search_Kab.ToUpper()));
                }
                
            }

            switch (Sorting_Order)
            {
                case "Kodepos":
                    kodepos = kodepos.OrderByDescending(kp => kp.kode);
                    break;
                case "Kelurahan":
                    kodepos = kodepos.OrderBy(kp => kp.kelurahan);
                    break;
                case "Kelurahan_Desc":
                    kodepos = kodepos.OrderByDescending(kp => kp.kelurahan);
                    break;
                case "Kecamatan":
                    kodepos = kodepos.OrderBy(kp => kp.kecamatan);
                    break;
                case "Kecamatan_Desc":
                    kodepos = kodepos.OrderByDescending(kp => kp.kecamatan);
                    break;
                case "Kabupaten":
                    kodepos = kodepos.OrderBy(kp => kp.kabupaten);
                    break;
                case "Kabupaten_Desc":
                    kodepos = kodepos.OrderByDescending(kp => kp.kabupaten);
                    break;
                case "Provinsi":
                    kodepos = kodepos.OrderBy(kp => kp.provinsi);
                    break;
                case "Provinsi_Desc":
                    kodepos = kodepos.OrderByDescending(kp => kp.provinsi);
                    break;
                default:
                    kodepos = kodepos.OrderBy(kp => kp.kode);
                    break;
            }

            int Size_Of_Page = 4;
            int No_Of_Page = (Page_No ?? 1);

            return View(kodepos.ToPagedList(No_Of_Page, Size_Of_Page));
        }

        // GET: Kodepos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kodepos kodepos = db.Kodepos.Find(id);
            if (kodepos == null)
            {
                return HttpNotFound();
            }
            return View(kodepos);
        }

        // GET: Kodepos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kodepos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,kode,kelurahan,kecamatan,type_kab,kabupaten,provinsi")] Kodepos kodepos)
        {
            ViewBag.StatusMessages = "";

            if (ModelState.IsValid)
            {
                var existKodepos = db.Kodepos.Where(n => n.kode == kodepos.kode && n.kelurahan == kodepos.kelurahan && n.kecamatan == kodepos.kecamatan && n.kabupaten == kodepos.kabupaten).FirstOrDefault();

                if (existKodepos == null)
                {
                    db.Kodepos.Add(kodepos);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.StatusMessages = "Data already exists!";
                    return View(kodepos);
                }

            }

            return View(kodepos);
        }

        // GET: Kodepos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kodepos kodepos = db.Kodepos.Find(id);

            ViewBag.KabSelect = "";
            ViewBag.KotaSelect = "";

            if ((bool)kodepos.type_kab)
            {
                ViewBag.KabSelect = " selected";
            }
            else
            {
                ViewBag.KotaSelect = " selected";
            }

            if (kodepos == null)
            {
                return HttpNotFound();
            }
            return View(kodepos);
        }

        // POST: Kodepos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,kode,kelurahan,kecamatan,type_kab,kabupaten,provinsi")] Kodepos kodepos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kodepos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kodepos);
        }

        // GET: Kodepos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kodepos kodepos = db.Kodepos.Find(id);
            if (kodepos == null)
            {
                return HttpNotFound();
            }
            return View(kodepos);
        }

        // POST: Kodepos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kodepos kodepos = db.Kodepos.Find(id);
            db.Kodepos.Remove(kodepos);
            db.SaveChanges();
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
