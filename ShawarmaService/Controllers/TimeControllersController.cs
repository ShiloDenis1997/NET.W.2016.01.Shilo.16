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
    public class TimeControllersController : Controller
    {
        private ShawarmaModel db = new ShawarmaModel();

        // GET: TimeControllers
        public async Task<ActionResult> Index()
        {
            var timeController = db.TimeController.Include(t => t.Seller);
            return View(await timeController.ToListAsync());
        }

        // GET: TimeControllers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeController timeController = await db.TimeController.FindAsync(id);
            if (timeController == null)
            {
                return HttpNotFound();
            }
            return View(timeController);
        }

        // GET: TimeControllers/Create
        public ActionResult Create()
        {
            ViewBag.SellerId = new SelectList(db.Seller, "SellerId", "SellerName");
            return View();
        }

        // POST: TimeControllers/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TimeControllerId,SellerId,WorkStart,WorkEnd")] TimeController timeController)
        {
            if (ModelState.IsValid)
            {
                db.TimeController.Add(timeController);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.SellerId = new SelectList(db.Seller, "SellerId", "SellerName", timeController.SellerId);
            return View(timeController);
        }

        // GET: TimeControllers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeController timeController = await db.TimeController.FindAsync(id);
            if (timeController == null)
            {
                return HttpNotFound();
            }
            ViewBag.SellerId = new SelectList(db.Seller, "SellerId", "SellerName", timeController.SellerId);
            return View(timeController);
        }

        // POST: TimeControllers/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TimeControllerId,SellerId,WorkStart,WorkEnd")] TimeController timeController)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timeController).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.SellerId = new SelectList(db.Seller, "SellerId", "SellerName", timeController.SellerId);
            return View(timeController);
        }

        // GET: TimeControllers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeController timeController = await db.TimeController.FindAsync(id);
            if (timeController == null)
            {
                return HttpNotFound();
            }
            return View(timeController);
        }

        // POST: TimeControllers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TimeController timeController = await db.TimeController.FindAsync(id);
            db.TimeController.Remove(timeController);
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
