using Hotline.Data;
using Hotline.Models;
using Hotline.Services;
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
        private readonly IMailingService _mailingService;

        public AuthenticationController( AppDbContext context, IMailingService mailingService)
        {
            _context = context;
            _mailingService = mailingService;

        }

        [HttpGet("denied")]
        public IActionResult Denied()
        {
            return RedirectToAction("Index","Home");
        }

        [HttpGet("login")]
        public IActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            if(_context.Users.Count() == 0)
            {
                var passwordHasher = new PasswordHasher<string>();
                User u = new User { Login = "MAVISION", Admin = true, Password = passwordHasher.HashPassword(null, "MAVISION") };
                _context.Add(u);
                _context.SaveChanges();
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string login, string password, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            
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


        [HttpGet("forgetPassword")]
        public IActionResult forgetPassword()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            return View();
        }

        [HttpPost("forgetPassword")]
        public async Task<IActionResult> forgetPassword(string login, string email)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }

            var clients = from c in _context.Clients
                          select c;
            var client = clients.Where(c => c.Login == login).FirstOrDefault();
            if (client != null)
            {
                if (email != client.Email)
                {
                    TempData["Error"] = "Email incorrect";
                    return View();
                }
                else
                {
                    Random r = new Random();
                    var pwd = r.Next(10000,99999).ToString();
                    var passwordHasher = new PasswordHasher<string>();
                    client.Password = passwordHasher.HashPassword(null,pwd);
                    _context.SaveChanges();
                    await _mailingService.SendEmail(client.Email, "Récupération mot de passe", "Votre nouveau mot de passe est : "+pwd);
                    TempData["success"] = "Le mail de recupération de mot de passe a été envoyé avec succès";
                    return RedirectToAction("login");
                }
            }

            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
