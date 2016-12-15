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
    public class SellingPointCategoriesController : Controller
    {
        private ShawarmaModel db = new ShawarmaModel();

        // GET: SellingPointCategories
        public async Task<ActionResult> Index()
        {
            return View(await db.SellingPointCategory.ToListAsync());
        }

        // GET: SellingPointCategories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SellingPointCategory sellingPointCategory = await db.SellingPointCategory.FindAsync(id);
            if (sellingPointCategory == null)
            {
                return HttpNotFound();
            }
            return View(sellingPointCategory);
        }

        // GET: SellingPointCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SellingPointCategories/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SellingPointCategoryId,SellingPointCategoryName")] SellingPointCategory sellingPointCategory)
        {
            if (ModelState.IsValid)
            {
                db.SellingPointCategory.Add(sellingPointCategory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(sellingPointCategory);
        }

        // GET: SellingPointCategories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SellingPointCategory sellingPointCategory = await db.SellingPointCategory.FindAsync(id);
            if (sellingPointCategory == null)
            {
                return HttpNotFound();
            }
            return View(sellingPointCategory);
        }

        // POST: SellingPointCategories/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SellingPointCategoryId,SellingPointCategoryName")] SellingPointCategory sellingPointCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sellingPointCategory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(sellingPointCategory);
        }

        // GET: SellingPointCategories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SellingPointCategory sellingPointCategory = await db.SellingPointCategory.FindAsync(id);
            if (sellingPointCategory == null)
            {
                return HttpNotFound();
            }
            return View(sellingPointCategory);
        }

        // POST: SellingPointCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SellingPointCategory sellingPointCategory = await db.SellingPointCategory.FindAsync(id);
            db.SellingPointCategory.Remove(sellingPointCategory);
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
