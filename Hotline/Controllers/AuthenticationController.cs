using Hotline.Data;
using Hotline.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hotline.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly AppDbContext _context;

        public AuthenticationController( AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("denied")]
        public IActionResult Denied()
        {
            return View();
        }

        [HttpGet("login")]
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string login, string password, string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;

            var clients = from c in _context.Clients
                        select c;
            var client = clients.Where(c => c.Login == login).FirstOrDefault();
            if (client != null)
            {
                var passwordHasher = new PasswordHasher<string>();
                if ((passwordHasher.VerifyHashedPassword(null, client.Password, password)) == 0)
                {
                    TempData["Error"] = "Mot de passe incorrect.";
                }
                else
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, login));
                    claims.Add(new Claim(ClaimTypes.Role, "Client"));
                    claims.Add(new Claim("Id", client.Id.ToString()));
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect("/");
                    }
                    return Redirect(returnUrl);
                }
            }
            else
            {
                var users = from u in _context.Users
                            select u;
                var user = users.Where(u => u.Login == login).FirstOrDefault();
                if (user == null)
                {
                    TempData["Error"] = "Ce compte n'exite pas.";
                }
                else
                {
                    var passwordHasher = new PasswordHasher<string>();
                    if ((passwordHasher.VerifyHashedPassword(null, user.Password, password)) == 0)
                    {
                        TempData["Error"] = "Mot de passe incorrect.";
                    }
                    else
                    {
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, login));
                        if (user.Admin == true) claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                        else claims.Add(new Claim(ClaimTypes.Role, "User"));
                        claims.Add(new Claim("Id", user.Id.ToString()));
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                        await HttpContext.SignInAsync(claimsPrincipal);
                        if (string.IsNullOrEmpty(returnUrl))
                        {
                            return Redirect("/");
                        }
                        return Redirect(returnUrl);
                    }
                }
            }
            
            return View();
        }


        [Authorize(Roles ="Client")]
        [HttpGet("account")]
        public async Task<IActionResult> AccountClient(int? id)
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
            else if(client.Id != id)
            {
                return Redirect("denied");
            }
            return View(client);
        }

        [Authorize]
        [HttpGet("account/modify")]
        public IActionResult Modify()
        {
            return View();
        }

        [Authorize]
        [HttpPost("account/modify")]
        public async Task<IActionResult> Modify2()
        {
            return View();
        }


        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/"); //REDIRECT TO HOME ACTION  ?? or maybe try view ~/Views/Guestbook/Index.cshtml
        }
    }
}
