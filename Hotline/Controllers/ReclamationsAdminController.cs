using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hotline.Data;
using Hotline.Models;
using Microsoft.AspNetCore.Authorization;

namespace Hotline.Controllers
{
    public class ReclamationsAdminController : Controller
    {
        private readonly AppDbContext _context;

        public ReclamationsAdminController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ReclamationsAdmin
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(int? pageNumber,string sortOrder,bool cloturé,bool résolue)
        {
            ViewData["ClientSortParamDesc"] = String.IsNullOrEmpty(sortOrder) ? "client_desc" : "";
            ViewData["ClientSortParamAsc"] = String.IsNullOrEmpty(sortOrder) ? "client_asc" : "";

            ViewData["DateSortParamDesc"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["DateSortParamAsc"] = sortOrder == "Date" ? "date_asc" : "Date";

            var reclamations = _context.Reclamations.Include(c => c.Client).Where(r=> r.Numero != 0);
            if (cloturé == true)
            {
                reclamations = reclamations.Where(r => r.Statut == "Cloturée");
                ViewData["recs"] = "Cloturée";
            }
            else if (résolue == true)
            {
                reclamations = reclamations.Where(r => r.Statut == "Résolue");
                ViewData["recs"] = "Résolue";
            }
            else
            {
                reclamations = reclamations.Where(r => r.Statut != "Résolue" && r.Statut != "Cloturée");
                ViewData["recs"] = "Autre";
            }
            switch (sortOrder)
            {
                case "client_asc":
                    reclamations = reclamations.OrderBy(r => r.Client.Login);
                    break;
                case "client_desc":
                    reclamations = reclamations.OrderByDescending(r => r.Client.Login);
                    break;
                case "date_desc":
                    reclamations = reclamations.OrderByDescending(r => r.Statut);
                    break;
                case "date_asc":
                    reclamations = reclamations.OrderBy(r => r.Statut);
                    break;
                default:
                    reclamations = reclamations.OrderBy(r => r.DateSoumission);
                    break;
            }
            int pageSize = 8;
            return View(await PaginatedList<Reclamation>.CreateAsync(reclamations.AsNoTracking(), pageNumber ?? 1, pageSize));
        }


        // GET: ReclamationsAdmin/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reclamation = await _context.Reclamations.Include(r => r.Domaine).Include(r => r.Projet).Include(r=>r.Client)
                .FirstOrDefaultAsync(m => m.Numero == id);
            if (reclamation == null)
            {
                return NotFound();
            }

            return View(reclamation);
        }

        // GET: ReclamationsAdmin/Edit/5
        [Authorize(Roles = "Admin")]
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
            if (reclamation.Statut != "Soumise")
            {
                return RedirectToAction("Index");
            }
            var users = _context.Users.OrderBy(u=>u.Login);
            ViewBag.UsersList = users;
            return View(reclamation);
        }

        // POST: ReclamationsAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Numero,Client,Projet,Domaine,Description,DateSoumission,Statut,DateAffectation,Responsable,DateResolution,Solution")] Reclamation reclamation,int idResponsalbe)
        {
            if (id != reclamation.Numero)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (reclamation.Statut != "Soumise")
                    {
                        TempData["Error"] = "Vous ne pouvez pas modifier cette réclamation";
                        return RedirectToAction("Edit",reclamation);
                    }
                    var idResponsable = Int32.Parse(Request.Form["Responsable"]);
                    reclamation.Responsable = _context.Users.Find(idResponsable);
                    reclamation.Statut = "Afféctée";
                    var localTimeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Central Africa Standard Time");
                    reclamation.DateAffectation = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, localTimeZone);

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

        // GET: ReclamationsAdmin/Delete/5
        [Authorize(Roles = "Admin")]
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

        // POST: ReclamationsAdmin/Delete/5
        [Authorize(Roles = "Admin")]
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
