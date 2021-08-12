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
using System.Collections.ObjectModel;

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
        [HttpGet("Reclamations")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Index(int? pageNumber,string sortOrder)
        {
            ViewData["StatutSortParamDesc"] = String.IsNullOrEmpty(sortOrder) ? "statut_desc" : "";
            ViewData["StatutSortParamAsc"] = String.IsNullOrEmpty(sortOrder) ? "statut_asc" : "";

            ViewData["ProjetSortParamAsc"] = String.IsNullOrEmpty(sortOrder) ? "projet_asc" : "";
            ViewData["ProjetSortParamDesc"] = String.IsNullOrEmpty(sortOrder) ? "projet_desc" : "";


            var reclamations = _context.Reclamations.Include(r => r.Projet).Include(r => r.Domaine)
                .Where(r => r.Client.Login == User.Identity.Name && r.Statut != "Cloturée");

            switch (sortOrder)
            {
                case "statut_desc":
                    reclamations = reclamations.OrderByDescending(r => r.Statut);
                    break;
                case "statut_asc":
                    reclamations = reclamations.OrderBy(r => r.Statut);
                    break;
                case "projet_desc":
                    reclamations = reclamations.OrderByDescending(r => r.Projet.Nom);
                    break;
                case "projet_asc":
                    reclamations = reclamations.OrderBy(r => r.Projet.Nom);
                    break;
                default:
                    reclamations = reclamations.OrderBy(r => r.DateSoumission);
                    break;
            }
            int pageSize = 8;
            return View(await PaginatedList<Reclamation>.CreateAsync(reclamations.AsNoTracking(), pageNumber ?? 1, pageSize));

        }

        // GET: Reclamations/Details/5
        [HttpGet("Reclamations/Details/{id}")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reclamation = await _context.Reclamations.Include(c=>c.Projet).Include(c=>c.Domaine)
                .FirstOrDefaultAsync(m => m.Numero == id);
            if (reclamation == null)
            {
                return NotFound();
            }

            return View(reclamation);
        }

        // GET: Reclamations/Create
        [HttpGet("Reclamations/Create")]
        [Authorize(Roles = "Client")]
        public IActionResult Create(int? projetId)
        {
            var projets = _context.Projets.Where(p => p.Client.Login == User.Identity.Name).OrderBy(p=>p.Nom);
            if (projetId.HasValue)
            {
                var domaines = _context.Domaines.Where(d => d.Projet.Id == projetId).OrderBy(d=>d.Nom);
                ViewBag.DomainesList = domaines;

            }
            ViewBag.ProjetsList = projets;
            return View();
        }


        public JsonResult GetDomaines(int id)
        {
            var domaines = _context.Domaines.Include(d => d.Projet).Where(d => d.Projet.Id == id).OrderBy(d=>d.Nom);
            return Json(new SelectList(domaines, "Id", "Nom"));
        }


        // POST: Reclamations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Client")]
        [HttpPost("Reclamations/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Numero,Client,Projet,Domaine,Description,DateSoumission,Statut,DateAffectation,Responsable,DateResolution,Solution")] Reclamation reclamation)
        {

            if (ModelState.IsValid)
            {
                var localTimeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Central Africa Standard Time");
                reclamation.DateSoumission = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, localTimeZone);

                reclamation.Statut = "Soumise";
                var login = User.FindFirstValue(ClaimTypes.Name); // will give the user's userId
                var user = _context.Clients.Where(u => u.Login == login).FirstOrDefault();
                reclamation.Client = user;
                var idProjet = Int32.Parse(Request.Form["Projet"]);
                reclamation.Projet = _context.Projets.Find(idProjet);
                var idDomaine = Int32.Parse(Request.Form["Domaine"]);
                reclamation.Domaine = _context.Domaines.Find(idDomaine);
                _context.Add(reclamation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Create",reclamation);
        }

        // GET: ReclamationsClient/Delete/5
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reclamation = await _context.Reclamations.Include(r=>r.Projet).Include(r=>r.Domaine)
                .FirstOrDefaultAsync(m => m.Numero == id);
            if (reclamation == null)
            {
                return NotFound();
            }

            return View(reclamation);
        }

        // POST: ReclamationsClient/Delete/5
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
