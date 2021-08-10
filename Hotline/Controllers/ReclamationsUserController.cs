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
using Hotline.Services;

namespace Hotline.Controllers
{
    public class ReclamationsUserController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMailingService _mailingService;
        public ReclamationsUserController(AppDbContext context, IMailingService mailingService)
        {
            _context = context;
            _mailingService = mailingService;
        }

        // GET: Reclamations
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Index(int? pageNumber)
        {
            var reclamations = _context.Reclamations.Include(c => c.Client).Where(r => r.Responsable.Login == User.Identity.Name);
            int pageSize = 8;
            return View(await PaginatedList<Reclamation>.CreateAsync(reclamations.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Reclamations/Details/5
        [Authorize(Roles = "User,Admin")]
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

        // GET: Reclamations/Edit/5
        [Authorize(Roles = "User,Admin")]
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
        [Authorize(Roles = "User,Admin")]
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
                    reclamation.DateResolution = DateTime.Now;
                    reclamation.Statut = "Résolue";
                    _context.Update(reclamation);
                    var reclamations = _context.Reclamations.Include(c => c.Client).Where(r => r.Numero == reclamation.Numero);
                    var client= reclamations.AsNoTracking().FirstOrDefault().Client;
                    await _mailingService.SendEmail(client.Email, "Réclamation résolue", "La réclamation numéro "+reclamation.Numero+" a été résolue. Veuillez consulter notre site web");
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
        [Authorize(Roles = "User,Admin")]
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
        [Authorize(Roles = "User,Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reclamation = await _context.Reclamations.FindAsync(id);
            _context.Reclamations.Remove(reclamation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Reclamations/Cloturer/5
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Cloturer(int? id)
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
            reclamation.Statut = "Cloturée";
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        private bool ReclamationExists(int id)
        {
            return _context.Reclamations.Any(e => e.Numero == id);
        }
    }
}
