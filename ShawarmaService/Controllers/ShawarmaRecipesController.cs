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
    public class ShawarmaRecipesController : Controller
    {
        private ShawarmaModel db = new ShawarmaModel();

        // GET: ShawarmaRecipes
        public async Task<ActionResult> Index()
        {
            var shawarmaRecipe = db.ShawarmaRecipe.Include(s => s.Ingradient).Include(s => s.Shawarma);
            return View(await shawarmaRecipe.ToListAsync());
        }

        // GET: ShawarmaRecipes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShawarmaRecipe shawarmaRecipe = await db.ShawarmaRecipe.FindAsync(id);
            if (shawarmaRecipe == null)
            {
                return HttpNotFound();
            }
            return View(shawarmaRecipe);
        }

        // GET: ShawarmaRecipes/Create
        public ActionResult Create()
        {
            ViewBag.IngradientId = new SelectList(db.Ingradient, "IngradientId", "IngradientName");
            ViewBag.ShawarmaId = new SelectList(db.Shawarma, "ShawarmaId", "ShawarmaName");
            return View();
        }

        // POST: ShawarmaRecipes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ShawarmaRecipeId,ShawarmaId,IngradientId,Weight")] ShawarmaRecipe shawarmaRecipe)
        {
            if (ModelState.IsValid)
            {
                db.ShawarmaRecipe.Add(shawarmaRecipe);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IngradientId = new SelectList(db.Ingradient, "IngradientId", "IngradientName", shawarmaRecipe.IngradientId);
            ViewBag.ShawarmaId = new SelectList(db.Shawarma, "ShawarmaId", "ShawarmaName", shawarmaRecipe.ShawarmaId);
            return View(shawarmaRecipe);
        }

        // GET: ShawarmaRecipes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShawarmaRecipe shawarmaRecipe = await db.ShawarmaRecipe.FindAsync(id);
            if (shawarmaRecipe == null)
            {
                return HttpNotFound();
            }
            ViewBag.IngradientId = new SelectList(db.Ingradient, "IngradientId", "IngradientName", shawarmaRecipe.IngradientId);
            ViewBag.ShawarmaId = new SelectList(db.Shawarma, "ShawarmaId", "ShawarmaName", shawarmaRecipe.ShawarmaId);
            return View(shawarmaRecipe);
        }

        // POST: ShawarmaRecipes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ShawarmaRecipeId,ShawarmaId,IngradientId,Weight")] ShawarmaRecipe shawarmaRecipe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shawarmaRecipe).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IngradientId = new SelectList(db.Ingradient, "IngradientId", "IngradientName", shawarmaRecipe.IngradientId);
            ViewBag.ShawarmaId = new SelectList(db.Shawarma, "ShawarmaId", "ShawarmaName", shawarmaRecipe.ShawarmaId);
            return View(shawarmaRecipe);
        }

        // GET: ShawarmaRecipes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShawarmaRecipe shawarmaRecipe = await db.ShawarmaRecipe.FindAsync(id);
            if (shawarmaRecipe == null)
            {
                return HttpNotFound();
            }
            return View(shawarmaRecipe);
        }

        // POST: ShawarmaRecipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ShawarmaRecipe shawarmaRecipe = await db.ShawarmaRecipe.FindAsync(id);
            db.ShawarmaRecipe.Remove(shawarmaRecipe);
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
