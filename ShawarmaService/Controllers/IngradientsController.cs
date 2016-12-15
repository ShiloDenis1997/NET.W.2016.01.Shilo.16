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
    public class IngradientsController : Controller
    {
        private ShawarmaModel db = new ShawarmaModel();

        // GET: Ingradients
        public async Task<ActionResult> Index()
        {
            var ingradient = db.Ingradient.Include(i => i.IngradientCategory);
            return View(await ingradient.ToListAsync());
        }

        // GET: Ingradients/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingradient ingradient = await db.Ingradient.FindAsync(id);
            if (ingradient == null)
            {
                return HttpNotFound();
            }
            return View(ingradient);
        }

        // GET: Ingradients/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.IngradientCategory, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Ingradients/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IngradientId,IngradientName,TotalWeight,CategoryId")] Ingradient ingradient)
        {
            if (ModelState.IsValid)
            {
                db.Ingradient.Add(ingradient);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.IngradientCategory, "CategoryId", "CategoryName", ingradient.CategoryId);
            return View(ingradient);
        }

        // GET: Ingradients/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingradient ingradient = await db.Ingradient.FindAsync(id);
            if (ingradient == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.IngradientCategory, "CategoryId", "CategoryName", ingradient.CategoryId);
            return View(ingradient);
        }

        // POST: Ingradients/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IngradientId,IngradientName,TotalWeight,CategoryId")] Ingradient ingradient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ingradient).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.IngradientCategory, "CategoryId", "CategoryName", ingradient.CategoryId);
            return View(ingradient);
        }

        // GET: Ingradients/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingradient ingradient = await db.Ingradient.FindAsync(id);
            if (ingradient == null)
            {
                return HttpNotFound();
            }
            return View(ingradient);
        }

        // POST: Ingradients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Ingradient ingradient = await db.Ingradient.FindAsync(id);
            db.Ingradient.Remove(ingradient);
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
