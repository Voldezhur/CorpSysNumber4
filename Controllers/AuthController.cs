using System;
using System.Collections;
using System.Data.Common;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Practice4.Models;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace Practice4.Controllers;

public class AuthController : Controller
{
    ApplicationContext db;
    public AuthController(ApplicationContext context)
    {
        db = context;
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(User user)
    {
        db.users.Add(user);
        await db.SaveChangesAsync();

        HttpContext.Response.Cookies.Append("authed_user", user.username, new CookieOptions {Expires = DateTime.Now.AddDays(1)});

        return RedirectToAction("Main", "Authed");
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(User user)
    {
        if (db.users.Where(u => u.username == user.username &&  u.password == user.password).Count() > 0) {
            HttpContext.Response.Cookies.Append("authed_user", user.username, new CookieOptions {Expires = DateTime.Now.AddDays(1)});
            return RedirectToAction("Main", "Authed");
        }

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
