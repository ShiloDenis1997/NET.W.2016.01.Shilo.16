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
    public class OrderHeadersController : Controller
    {
        private ShawarmaModel db = new ShawarmaModel();

        // GET: OrderHeaders
        public async Task<ActionResult> Index()
        {
            var orderHeader = db.OrderHeader.Include(o => o.Seller);
            return View(await orderHeader.ToListAsync());
        }

        // GET: OrderHeaders/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderHeader orderHeader = await db.OrderHeader.FindAsync(id);
            if (orderHeader == null)
            {
                return HttpNotFound();
            }
            return View(orderHeader);
        }

        // GET: OrderHeaders/Create
        public ActionResult Create()
        {
            ViewBag.SellerId = new SelectList(db.Seller, "SellerId", "SellerName");
            return View();
        }

        // POST: OrderHeaders/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "OrderHeaderId,OrderDate,SellerId")] OrderHeader orderHeader)
        {
            if (ModelState.IsValid)
            {
                db.OrderHeader.Add(orderHeader);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.SellerId = new SelectList(db.Seller, "SellerId", "SellerName", orderHeader.SellerId);
            return View(orderHeader);
        }

        // GET: OrderHeaders/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderHeader orderHeader = await db.OrderHeader.FindAsync(id);
            if (orderHeader == null)
            {
                return HttpNotFound();
            }
            ViewBag.SellerId = new SelectList(db.Seller, "SellerId", "SellerName", orderHeader.SellerId);
            return View(orderHeader);
        }

        // POST: OrderHeaders/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "OrderHeaderId,OrderDate,SellerId")] OrderHeader orderHeader)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderHeader).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.SellerId = new SelectList(db.Seller, "SellerId", "SellerName", orderHeader.SellerId);
            return View(orderHeader);
        }

        // GET: OrderHeaders/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderHeader orderHeader = await db.OrderHeader.FindAsync(id);
            if (orderHeader == null)
            {
                return HttpNotFound();
            }
            return View(orderHeader);
        }

        // POST: OrderHeaders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            OrderHeader orderHeader = await db.OrderHeader.FindAsync(id);
            db.OrderHeader.Remove(orderHeader);
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
