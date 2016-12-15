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
    public class SellersController : Controller
    {
        private ShawarmaModel db = new ShawarmaModel();

        // GET: Sellers
        public async Task<ActionResult> Index()
        {
            var seller = db.Seller.Include(s => s.SellingPoint);
            return View(await seller.ToListAsync());
        }

        // GET: Sellers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seller seller = await db.Seller.FindAsync(id);
            if (seller == null)
            {
                return HttpNotFound();
            }
            return View(seller);
        }

        // GET: Sellers/Create
        public ActionResult Create()
        {
            ViewBag.SellingPointId = new SelectList(db.SellingPoint, "SellingPointId", "Address");
            return View();
        }

        // POST: Sellers/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SellerId,SellerName,SellingPointId")] Seller seller)
        {
            if (ModelState.IsValid)
            {
                db.Seller.Add(seller);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.SellingPointId = new SelectList(db.SellingPoint, "SellingPointId", "Address", seller.SellingPointId);
            return View(seller);
        }

        // GET: Sellers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seller seller = await db.Seller.FindAsync(id);
            if (seller == null)
            {
                return HttpNotFound();
            }
            ViewBag.SellingPointId = new SelectList(db.SellingPoint, "SellingPointId", "Address", seller.SellingPointId);
            return View(seller);
        }

        // POST: Sellers/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SellerId,SellerName,SellingPointId")] Seller seller)
        {
            if (ModelState.IsValid)
            {
                db.Entry(seller).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.SellingPointId = new SelectList(db.SellingPoint, "SellingPointId", "Address", seller.SellingPointId);
            return View(seller);
        }

        // GET: Sellers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seller seller = await db.Seller.FindAsync(id);
            if (seller == null)
            {
                return HttpNotFound();
            }
            return View(seller);
        }

        // POST: Sellers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Seller seller = await db.Seller.FindAsync(id);
            db.Seller.Remove(seller);
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
