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
    public class ShawarmasController : Controller
    {
        private ShawarmaModel db = new ShawarmaModel();

        // GET: Shawarmas
        public async Task<ActionResult> Index()
        {
            return View(await db.Shawarma.ToListAsync());
        }

        // GET: Shawarmas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shawarma shawarma = await db.Shawarma.FindAsync(id);
            if (shawarma == null)
            {
                return HttpNotFound();
            }
            return View(shawarma);
        }

        // GET: Shawarmas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Shawarmas/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ShawarmaId,ShawarmaName,CookingTime")] Shawarma shawarma)
        {
            if (ModelState.IsValid)
            {
                db.Shawarma.Add(shawarma);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(shawarma);
        }

        // GET: Shawarmas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shawarma shawarma = await db.Shawarma.FindAsync(id);
            if (shawarma == null)
            {
                return HttpNotFound();
            }
            return View(shawarma);
        }

        // POST: Shawarmas/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ShawarmaId,ShawarmaName,CookingTime")] Shawarma shawarma)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shawarma).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(shawarma);
        }

        // GET: Shawarmas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shawarma shawarma = await db.Shawarma.FindAsync(id);
            if (shawarma == null)
            {
                return HttpNotFound();
            }
            return View(shawarma);
        }

        // POST: Shawarmas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Shawarma shawarma = await db.Shawarma.FindAsync(id);
            db.Shawarma.Remove(shawarma);
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
