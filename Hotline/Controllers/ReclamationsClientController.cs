using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hotline.Data;
using Hotline.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Hotline.Controllers
{
    public class ReclamationsClientController : Controller
    {
        private readonly AppDbContext _context;

        public ReclamationsClientController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Reclamations
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Index(int? pageNumber)
        {
            var reclamations = _context.Reclamations.Where(r => r.Client.Login == User.Identity.Name);
            int pageSize = 8;
            return View(await PaginatedList<Reclamation>.CreateAsync(reclamations.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Reclamations/Details/5
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reclamation = await _context.Reclamations
                .FirstOrDefaultAsync(m => m.Numero == id);
            if (reclamation == null)
            {
                return NotFound();
            }

            return View(reclamation);
        }

        // GET: Reclamations/Create
        [Authorize(Roles = "Client")]
        public IActionResult Create(int? projetId)
        {
            var projets = _context.Projets.Where(p => p.Client.Login == User.Identity.Name);
            //var domaines = (from d in _context.Domaines
            //select d).ToList(); //addwhere Client ==Client
            //var domaines = _context.Domaines; //FIX THIIIIIIIIIIIS domaine for every Projet chosen ?
            if (projetId.HasValue)
            {
                var domaines = _context.Domaines.Where(d => d.Projet.Id == projetId);
                ViewBag.DomainesList = domaines;

            }

            ViewBag.ProjetsList = projets;
            return View();
        }

        // POST: Reclamations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Client")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Numero,Description,DateSoumission,Statut,DateAffectation,DateResolution,Solution")] Reclamation reclamation)
        {

            if (ModelState.IsValid)
            {
                reclamation.DateSoumission = DateTime.Now;
                reclamation.Statut = "Soumise";
                var login = User.FindFirstValue(ClaimTypes.Name); // will give the user's userId
                var user = _context.Clients.Where(u => u.Login == login).FirstOrDefault(); //UNICITY VERIFICATION AND FIX THIS
                reclamation.Client = user;
                //PROJET + DOMAINE

                _context.Add(reclamation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reclamation);
        }


        // GET: Reclamations/Edit/5
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reclamation = await _context.Reclamations.FindAsync(id);
            if (reclamation == null)
            {
                return NotFound();
            }
            return View(reclamation);
        }

        // POST: Reclamations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Client")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Numero,Description,DateSoumission,Statut,DateAffectation,DateResolution,Solution")] Reclamation reclamation)
        {
            if (id != reclamation.Numero)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reclamation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReclamationExists(reclamation.Numero))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(reclamation);
        }

        // GET: Reclamations/Delete/5
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reclamation = await _context.Reclamations
                .FirstOrDefaultAsync(m => m.Numero == id);
            if (reclamation == null)
            {
                return NotFound();
            }

            return View(reclamation);
        }

        // POST: Reclamations/Delete/5
        [Authorize(Roles = "Client")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reclamation = await _context.Reclamations.FindAsync(id);
            _context.Reclamations.Remove(reclamation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReclamationExists(int id)
        {
            return _context.Reclamations.Any(e => e.Numero == id);
        }
    }
}
