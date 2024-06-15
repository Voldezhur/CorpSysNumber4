using System.Data.Common;
using System.Diagnostics;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Practice4.Models;

namespace Practice4.Controllers;

public class AuthedController : Controller
{
    ApplicationContext db;
    public AuthedController(ApplicationContext context)
    {
        db = context;
    }

    public IActionResult UserList()
    {
        ViewBag.db = db;
        return View();
    }

    public IActionResult Main()
    {
        string username = "";
        if (Request.Cookies["authed_user"] != null)
        {
            username = Request.Cookies["authed_user"];
        }

        var user = db.users.Where(x => x.username == username).ToList()[0];
        ViewBag.user = user;
        var messages = db.message.Where(x => x.recipient_id == user.id).ToList();
        ViewBag.messages = messages;
        return View();
    }

    [HttpPut]
    public IActionResult Main(int id)
    {
        return View();
    }

    public IActionResult SendMessage()
    {
        string username = "";
        if (Request.Cookies["authed_user"] != null)
        {
            username = Request.Cookies["authed_user"];
        }

        var user = db.users.Where(x => x.username == username).ToList()[0];
        ViewBag.user = user;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage(Message message)
    {
        var sender_id = db.users.Where(x => x.username == message.sender_name).ToList()[0].id;
        var recipient_id = db.users.Where(x => x.username == message.recipient_name).ToList()[0].id;

        Message newMessage = new Message{sender_id = sender_id, recipient_id = recipient_id, sender_name = message.sender_name, recipient_name = message.recipient_name, text = message.text,  date_sent = DateTime.Now};

        db.message.Add(newMessage);
        await db.SaveChangesAsync();

        return RedirectToAction("Main", "Authed");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
