using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hotline.Data;
using Hotline.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Hotline.Controllers
{
    public class ClientsController : Controller
    {
        private readonly AppDbContext _context;

        public ClientsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Clients
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(int? pageNumber)
        {
            ViewBag.ProjetsList = _context.Projets.Include(p=>p.Client).OrderBy(p=>p.Nom);
            int pageSize = 8;
            return View(await PaginatedList<Client>.CreateAsync(_context.Clients.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Clients/Details/5
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }
            else if (client.Id != id)
            {
                Redirect("denied");
            }

            return View(client);
        }

        // GET: Clients/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Login,Password,Email")] Client client)
        {
            if (ModelState.IsValid)
            {
                if (LoginExists(client.Login) == false)
                {
                    if (client.Password.Length < 6)
                    {
                        TempData["Error"] = "Mot de passe trés court";
                        return RedirectToAction("Create");
                    }
                    var passwordHasher = new PasswordHasher<string>();
                    client.Password = passwordHasher.HashPassword(null, client.Password);
                    _context.Add(client);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Error"] = "Ce compte existe déja";
                }
            }
            return RedirectToAction("Create");
        }

        // GET: Clients/Edit/5
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            if (client.Id != id)
            {
                return Redirect("denied");
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Client")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Login,Password,Email")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if ( client.Id != id)
                    {
                        return Redirect("denied");
                    }
                    else
                    {
                        var pwd = Request.Form["pwd"];
                        var passwordHasher = new PasswordHasher<string>();
                        if ((passwordHasher.VerifyHashedPassword(null,client.Password,pwd)) == 0)
                        {
                            TempData["Error"] = "Mot de passe incorrect.";
                            return View("Edit",client);
                        }
                        else
                        {
                            _context.Update(client);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index","Home");

            }
            return View(client);
        }

        // GET: Clients/EditPassword/5
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> EditPassword(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            if (client.Id != id)
            {
                return Redirect("denied");
            }
            return View(client);
        }

        // POST: Clients/EditPassword/5
        [Authorize(Roles = "Client")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPassword(int id, [Bind("Id,Login,Password,Email")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (client.Id != id)
                    {
                        return Redirect("denied");
                    }
                    else
                    {
                        var newpwd = Request.Form["newpwd"];
                        var oldpwd = Request.Form["oldpwd"];
                        var passwordHasher = new PasswordHasher<string>();
                        if ((passwordHasher.VerifyHashedPassword(null, client.Password, oldpwd)) == 0)
                        {
                            TempData["Error"] = "Mot de passe incorrect.";
                            return View("EditPassword", client);
                        }
                        else
                        {
                            if (newpwd.ToString().Length < 6)
                            {
                                TempData["Error"] = "Mot de passe trés court";
                                return View(client);
                            }

                            client.Password = passwordHasher.HashPassword(null,newpwd);
                            _context.Update(client);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            return View(client);
        }

        // GET: Clients/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }

        private bool LoginExists(string login)
        {
            return _context.Users.Any(e => e.Login == login) || _context.Clients.Any(e => e.Login == login);

        }
    }
}
