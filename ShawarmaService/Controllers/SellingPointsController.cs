using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ORM;

namespace ShawarmaService.Controllers
{
    public class SellingPointsController : Controller
    {
        private ShawarmaModel db = new ShawarmaModel();

        // GET: SellingPoints
        public async Task<ActionResult> Index()
        {
            var sellingPoint = db.SellingPoint.Include(s => s.SellingPointCategory);
            return View(await sellingPoint.ToListAsync());
        }

        // GET: SellingPoints/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SellingPoint sellingPoint = await db.SellingPoint.FindAsync(id);
            if (sellingPoint == null)
            {
                return HttpNotFound();
            }
            return View(sellingPoint);
        }

        // GET: SellingPoints/Create
        public ActionResult Create()
        {
            ViewBag.SellingPointCategoryId = new SelectList(db.SellingPointCategory, "SellingPointCategoryId", "SellingPointCategoryName");
            return View();
        }

        // POST: SellingPoints/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SellingPointId,Address,SellingPointCategoryId,ShawarmaTitle")] SellingPoint sellingPoint)
        {
            if (ModelState.IsValid)
            {
                db.SellingPoint.Add(sellingPoint);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.SellingPointCategoryId = new SelectList(db.SellingPointCategory, "SellingPointCategoryId", "SellingPointCategoryName", sellingPoint.SellingPointCategoryId);
            return View(sellingPoint);
        }

        // GET: SellingPoints/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SellingPoint sellingPoint = await db.SellingPoint.FindAsync(id);
            if (sellingPoint == null)
            {
                return HttpNotFound();
            }
            ViewBag.SellingPointCategoryId = new SelectList(db.SellingPointCategory, "SellingPointCategoryId", "SellingPointCategoryName", sellingPoint.SellingPointCategoryId);
            return View(sellingPoint);
        }

        // POST: SellingPoints/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SellingPointId,Address,SellingPointCategoryId,ShawarmaTitle")] SellingPoint sellingPoint)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sellingPoint).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.SellingPointCategoryId = new SelectList(db.SellingPointCategory, "SellingPointCategoryId", "SellingPointCategoryName", sellingPoint.SellingPointCategoryId);
            return View(sellingPoint);
        }

        // GET: SellingPoints/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SellingPoint sellingPoint = await db.SellingPoint.FindAsync(id);
            if (sellingPoint == null)
            {
                return HttpNotFound();
            }
            return View(sellingPoint);
        }

        // POST: SellingPoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SellingPoint sellingPoint = await db.SellingPoint.FindAsync(id);
            db.SellingPoint.Remove(sellingPoint);
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
