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
    public class OrderDetailsController : Controller
    {
        private ShawarmaModel db = new ShawarmaModel();

        // GET: OrderDetails
        public async Task<ActionResult> Index()
        {
            var orderDetails = db.OrderDetails.Include(o => o.OrderHeader).Include(o => o.Shawarma);
            return View(await orderDetails.ToListAsync());
        }

        // GET: OrderDetails/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetails orderDetails = await db.OrderDetails.FindAsync(id);
            if (orderDetails == null)
            {
                return HttpNotFound();
            }
            return View(orderDetails);
        }

        // GET: OrderDetails/Create
        public ActionResult Create()
        {
            ViewBag.OrderHeaderId = new SelectList(db.OrderHeader, "OrderHeaderId", "OrderHeaderId");
            ViewBag.ShawarmaId = new SelectList(db.Shawarma, "ShawarmaId", "ShawarmaName");
            return View();
        }

        // POST: OrderDetails/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "OrderHeaderId,ShawarmaId,Quantity")] OrderDetails orderDetails)
        {
            if (ModelState.IsValid)
            {
                db.OrderDetails.Add(orderDetails);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.OrderHeaderId = new SelectList(db.OrderHeader, "OrderHeaderId", "OrderHeaderId", orderDetails.OrderHeaderId);
            ViewBag.ShawarmaId = new SelectList(db.Shawarma, "ShawarmaId", "ShawarmaName", orderDetails.ShawarmaId);
            return View(orderDetails);
        }

        // GET: OrderDetails/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetails orderDetails = await db.OrderDetails.FindAsync(id);
            if (orderDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderHeaderId = new SelectList(db.OrderHeader, "OrderHeaderId", "OrderHeaderId", orderDetails.OrderHeaderId);
            ViewBag.ShawarmaId = new SelectList(db.Shawarma, "ShawarmaId", "ShawarmaName", orderDetails.ShawarmaId);
            return View(orderDetails);
        }

        // POST: OrderDetails/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "OrderHeaderId,ShawarmaId,Quantity")] OrderDetails orderDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderDetails).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.OrderHeaderId = new SelectList(db.OrderHeader, "OrderHeaderId", "OrderHeaderId", orderDetails.OrderHeaderId);
            ViewBag.ShawarmaId = new SelectList(db.Shawarma, "ShawarmaId", "ShawarmaName", orderDetails.ShawarmaId);
            return View(orderDetails);
        }

        // GET: OrderDetails/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetails orderDetails = await db.OrderDetails.FindAsync(id);
            if (orderDetails == null)
            {
                return HttpNotFound();
            }
            return View(orderDetails);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            OrderDetails orderDetails = await db.OrderDetails.FindAsync(id);
            db.OrderDetails.Remove(orderDetails);
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
