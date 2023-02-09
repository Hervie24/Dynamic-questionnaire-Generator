using Questionnaire_test.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Questionnaire_test.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(Models.User user)
        {
            if (IsValid(user.UserName, user.Password))
            {
                FormsAuthentication.SetAuthCookie(user.UserName, false);
                return RedirectToAction("Index", "Question");
            }
            else
            {
                ModelState.AddModelError("", "Login details are wrong.");
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Models.User user)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    using (var db = new SampleDataBaseEntities())
                    {
                       
                        var nUser = db.Users.Create();
                        nUser.UserName = user.UserName;
                        nUser.Password = user.Password;
                        nUser.FirstName = user.FirstName;
                        nUser.LastName = user.LastName;
                        nUser.CreatedDate = DateTime.Now;
                        nUser.ModifiedDate = DateTime.Now;                   
                        db.Users.Add(nUser);
                        db.SaveChanges();
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Data is not correct");
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }


        private bool IsValid(string userName, string password)
        {
            bool IsValid = false;

            using (var db = new SampleDataBaseEntities())
            {
                var user = db.Users.FirstOrDefault(u => u.UserName == userName);
                if (user != null)
                {
                    if (user.Password ==password)
                    {
                        IsValid = true;
                    }
                }
            }
            return IsValid;
        }


    }
}