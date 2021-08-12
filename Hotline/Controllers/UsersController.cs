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
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Users
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Clients/Details/5
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            else if (user.Id != id && User.IsInRole("User"))
            {
                Redirect("denied");
            }

            return View(user);
        }


        // GET: Users/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Login,Password,Admin")] User user)
        {
            if (ModelState.IsValid)
            {
                if (LoginExists(user.Login)==false)
                {
                    if(user.Password.Length < 6)
                    {
                        TempData["error"] = "Mot de passe trés court";
                        return View(user);
                    }
                    var passwordHasher = new PasswordHasher<string>();
                    user.Password = passwordHasher.HashPassword(null, user.Password);
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Error"] = "Ce compte existe déja";
                }
            }
            return View(user);
        }


        // GET: Users/Edit/5
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            if (user.Id != id && User.IsInRole("User"))
            {
                return Redirect("denied");
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "User,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Login,Password,Admin")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (user.Id != id && User.IsInRole("User"))
                    {
                        return Redirect("denied");
                    }
                    else
                    {
                        var pwd = Request.Form["pwd"];
                        var passwordHasher = new PasswordHasher<string>();
                        if ((passwordHasher.VerifyHashedPassword(null, user.Password, pwd)) == 0)
                        {
                            TempData["Error"] = "Mot de passe incorrect.";
                            return View("Edit", user);
                        }
                        else
                        {
                            _context.Update(user);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        // GET: Users/EditPassword/5
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> EditPassword(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            if (user.Id != id && User.IsInRole("User"))
            {
                return Redirect("denied");
            }
            return View(user);
        }

        // POST: Users/EditPassword/5
        [Authorize(Roles = "User,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPassword(int id, [Bind("Id,Login,Password,Admin")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (user.Id != id && User.IsInRole("User"))
                    {
                        return Redirect("denied");
                    }
                    else
                    {
                        var newpwd = Request.Form["newpwd"];
                        var oldpwd = Request.Form["oldpwd"];
                        var passwordHasher = new PasswordHasher<string>();
                        if ((passwordHasher.VerifyHashedPassword(null, user.Password, oldpwd)) == 0)
                        {
                            TempData["Error"] = "Mot de passe incorrect.";
                            return View("EditPassword", user);
                        }
                        else
                        {
                            if (newpwd.ToString().Length < 6)
                            {
                                TempData["Error"] = "Mot de passe trés court";
                                return View(user);
                            }

                            user.Password = passwordHasher.HashPassword(null, newpwd);
                            _context.Update(user);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }


        // GET: Users/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            var rec = _context.Reclamations.Where(r => r.Responsable.Id == id);
            if (rec.Count() > 0)
            {
                TempData["error"] = "Ce User a des réclamations en cours!";
                return RedirectToAction("Index");
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
        private bool LoginExists(string login)
        {
            return _context.Users.Any(e => e.Login == login) || _context.Clients.Any(e=>e.Login == login);

        }
    }
}
