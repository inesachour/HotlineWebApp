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
    public class DomainesController : Controller
    {
        private readonly AppDbContext _context;

        public DomainesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Domaines/Delete/5
        public async Task<IActionResult> Delete(int? id, int idProjet)
        {
            if (id == null)
            {
                return NotFound();
            }

            var domaine = await _context.Domaines
                .FirstOrDefaultAsync(m => m.Id == id);
            if (domaine == null)
            {
                return NotFound();
            }

            _context.Domaines.Remove(domaine);
            await _context.SaveChangesAsync();
            return Redirect("~/Projets/Edit/"+idProjet); ;
        }


        private bool DomaineExists(int id)
        {
            return _context.Domaines.Any(e => e.Id == id);
        }
    }
}
