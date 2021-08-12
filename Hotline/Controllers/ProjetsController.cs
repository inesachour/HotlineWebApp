using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hotline.Data;
using Hotline.Models;

namespace Hotline.Controllers
{
    public class ProjetsController : Controller
    {
        private readonly AppDbContext _context;

        public ProjetsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Projets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projet = await _context.Projets.Include(p=>p.Domaines)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projet == null)
            {
                return NotFound();
            }

            return View(projet);
        }

        // GET: Projets/Create
        public IActionResult Create()
        {
            var clients = _context.Clients.OrderBy(c=>c.Login);
            ViewBag.ClientsList = clients;

            return View();
        }

        // POST: Projets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Client,Domaines")] Projet projet)
        {
            if (ModelState.IsValid)
            {

                projet.Client = _context.Clients.Find(Int32.Parse(HttpContext.Request.Form["client"]));
                //await _context.SaveChangesAsync();

                var i = 1;
                while (! string.IsNullOrEmpty(HttpContext.Request.Form["domaine" + i]))
                {
                    Domaine d = new Domaine();
                    d.Nom = HttpContext.Request.Form["domaine"+i];
                    d.Projet = projet;
                    _context.Domaines.Add(d);
                    projet.Domaines.Add(d);
                    i++;
                }
                _context.Add(projet);

                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Clients");
            }
            return RedirectToAction("Index","Clients");
        }

        // GET: Projets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projet = await _context.Projets.FindAsync(id);
            ViewBag.DomainesList = _context.Domaines.Where(d => d.Projet.Id == id).OrderBy(d=>d.Nom);
            if (projet == null)
            {
                return NotFound();
            }
            return View(projet);
        }

        // POST: Projets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Client,Domaines")] Projet projet)
        {
            if (id != projet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                     var i = 1;
                while (! string.IsNullOrEmpty(HttpContext.Request.Form["domaine" + i]))
                {
                    Domaine d = new Domaine();
                    d.Nom = HttpContext.Request.Form["domaine"+i];
                    d.Projet = projet;
                    _context.Domaines.Add(d);
                    projet.Domaines.Add(d);
                    i++;
                }
                    _context.Update(projet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjetExists(projet.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index","Clients");
            }
            return View(projet);
        }

        // GET: Projets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projet = await _context.Projets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projet == null)
            {
                return NotFound();
            }

            return View(projet);
        }

        // POST: Projets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projet = await _context.Projets.FindAsync(id);
            var dom = _context.Domaines.Where(d => d.Projet.Id == id);
            if (dom.Count() > 0)
            {
                TempData["error"] = "Veuillez supprimer les domaines de ce projet d'abord!";
                return RedirectToAction("Index","Clients");
            }
            _context.Projets.Remove(projet);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Clients");
        }

        private bool ProjetExists(int id)
        {
            return _context.Projets.Any(e => e.Id == id);
        }
    }
}
