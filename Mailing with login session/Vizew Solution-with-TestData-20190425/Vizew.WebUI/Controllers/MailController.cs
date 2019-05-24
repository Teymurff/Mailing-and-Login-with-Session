using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Vizew.WebUI.Models;
using Vizew.WebUI.Models.Common;
using Vizew.WebUI.Models.Entity;

namespace Vizew.WebUI.Controllers
{
    public class MailController : Controller
    {
        // GET: Mail
        public ActionResult Index()
        {
            if (Session[SessionKey.User] == null)
            {
              
               return RedirectToAction("Login");

            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(EmailModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new NullReferenceException();
            }

            //Extension.SendMail(model.ToMails,model.Subject, model.Body);
            model.ToMails.SendMail(model.Subject, model.Body);

            return View();
        }


        [HttpPost]
        public ActionResult Subscribe(string email)
        {

            if (Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
               


                using (var db = new SmtpDbContext())
                {
                    if (db.Subscribers.Any(s=>s.Email==email))
                    {
                        ViewBag.ErrorMessage = "Artiq qeydiyyatdan kecmishsiniz";
                        return View("Index");
                    }

                    string key = Guid.NewGuid().ToString().Replace("-", "");


                    string replyLink = string.Concat(ConfigurationManager.AppSettings.Get("siteDomainName"),
                        "/Mail/CheckSubscribe?key=", key);

                    //ConfigurationManager.AppSettings.Get("siteDomainName");
                    //Mail/CheckSubscribe
                    //?key=

                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine($"<h1>Hello,{email}!{Environment.NewLine}<h1/>");
                    builder.AppendLine($"Please check this link!{Environment.NewLine}<a href='{replyLink}'>{replyLink}</a>");

                    if (email.SendMail("New Subscriber", builder.ToString()))
                    {

                        db.Subscribers.Add(new Models.Entity.Subscriber
                        {
                            Email = email,
                            Key = key,
                            CreateDate = DateTime.Now


                        });
                    
                    }

                    builder.Clear();
                }


               
              
            }
            else
            {
                ViewBag.ErrorMessage = "Email Duz deyil";

            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult CheckSubscribe(string key)
        {

          
            return View();
        }

        public ActionResult Login()
        {


            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            if (user.Email=="test@mail.az" && user.Password=="12345")
            {
                Session[SessionKey.User] = user;
                return RedirectToAction("Index");
            }

            return View();
        }


        //public ActionResult Logout()
        //{

        //    Session.Abandon();

        //    return RedirectToAction("Login");
        //}


    }
}