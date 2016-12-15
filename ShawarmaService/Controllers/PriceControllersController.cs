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
    public class PriceControllersController : Controller
    {
        private ShawarmaModel db = new ShawarmaModel();

        // GET: PriceControllers
        public async Task<ActionResult> Index()
        {
            var priceController = db.PriceController.Include(p => p.SellingPoint).Include(p => p.Shawarma);
            return View(await priceController.ToListAsync());
        }

        // GET: PriceControllers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PriceController priceController = await db.PriceController.FindAsync(id);
            if (priceController == null)
            {
                return HttpNotFound();
            }
            return View(priceController);
        }

        // GET: PriceControllers/Create
        public ActionResult Create()
        {
            ViewBag.SellingPointId = new SelectList(db.SellingPoint, "SellingPointId", "Address");
            ViewBag.ShawarmaId = new SelectList(db.Shawarma, "ShawarmaId", "ShawarmaName");
            return View();
        }

        // POST: PriceControllers/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PriceControllerId,ShawarmaId,Price,SellingPointId,Comment")] PriceController priceController)
        {
            if (ModelState.IsValid)
            {
                db.PriceController.Add(priceController);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.SellingPointId = new SelectList(db.SellingPoint, "SellingPointId", "Address", priceController.SellingPointId);
            ViewBag.ShawarmaId = new SelectList(db.Shawarma, "ShawarmaId", "ShawarmaName", priceController.ShawarmaId);
            return View(priceController);
        }

        // GET: PriceControllers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PriceController priceController = await db.PriceController.FindAsync(id);
            if (priceController == null)
            {
                return HttpNotFound();
            }
            ViewBag.SellingPointId = new SelectList(db.SellingPoint, "SellingPointId", "Address", priceController.SellingPointId);
            ViewBag.ShawarmaId = new SelectList(db.Shawarma, "ShawarmaId", "ShawarmaName", priceController.ShawarmaId);
            return View(priceController);
        }

        // POST: PriceControllers/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PriceControllerId,ShawarmaId,Price,SellingPointId,Comment")] PriceController priceController)
        {
            if (ModelState.IsValid)
            {
                db.Entry(priceController).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.SellingPointId = new SelectList(db.SellingPoint, "SellingPointId", "Address", priceController.SellingPointId);
            ViewBag.ShawarmaId = new SelectList(db.Shawarma, "ShawarmaId", "ShawarmaName", priceController.ShawarmaId);
            return View(priceController);
        }

        // GET: PriceControllers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PriceController priceController = await db.PriceController.FindAsync(id);
            if (priceController == null)
            {
                return HttpNotFound();
            }
            return View(priceController);
        }

        // POST: PriceControllers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PriceController priceController = await db.PriceController.FindAsync(id);
            db.PriceController.Remove(priceController);
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
