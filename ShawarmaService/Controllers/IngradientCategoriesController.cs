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
    public class IngradientCategoriesController : Controller
    {
        private ShawarmaModel db = new ShawarmaModel();

        // GET: IngradientCategories
        public async Task<ActionResult> Index()
        {
            return View(await db.IngradientCategory.ToListAsync());
        }

        // GET: IngradientCategories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IngradientCategory ingradientCategory = await db.IngradientCategory.FindAsync(id);
            if (ingradientCategory == null)
            {
                return HttpNotFound();
            }
            return View(ingradientCategory);
        }

        // GET: IngradientCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IngradientCategories/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CategoryId,CategoryName")] IngradientCategory ingradientCategory)
        {
            if (ModelState.IsValid)
            {
                db.IngradientCategory.Add(ingradientCategory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(ingradientCategory);
        }

        // GET: IngradientCategories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IngradientCategory ingradientCategory = await db.IngradientCategory.FindAsync(id);
            if (ingradientCategory == null)
            {
                return HttpNotFound();
            }
            return View(ingradientCategory);
        }

        // POST: IngradientCategories/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CategoryId,CategoryName")] IngradientCategory ingradientCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ingradientCategory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(ingradientCategory);
        }

        // GET: IngradientCategories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IngradientCategory ingradientCategory = await db.IngradientCategory.FindAsync(id);
            if (ingradientCategory == null)
            {
                return HttpNotFound();
            }
            return View(ingradientCategory);
        }

        // POST: IngradientCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            IngradientCategory ingradientCategory = await db.IngradientCategory.FindAsync(id);
            db.IngradientCategory.Remove(ingradientCategory);
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
