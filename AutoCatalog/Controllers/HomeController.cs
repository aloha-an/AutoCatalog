using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoCatalog.Models;

namespace AutoCatalog.Controllers
{
    public class HomeController : Controller
    {
        private AutoEntities db = new AutoEntities();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Users temp)
        {
           try
            {
                Users user = db.Users.Where(u => u.Email == temp.Email).Where(u => u.Password == temp.Password).First();

                if (ModelState.IsValid && user != null)
                {
                    Session["id"]= user.ID.ToString();
                    if (user.RoleID == 1)
                    {
                        Session["role"] = "admin";
                    }
                    else
                    {
                        Session["role"] = "user";
                    }
                    return RedirectToAction("AllCars", "Autos");
                }
            }
            catch(Exception ex)
            {
                ViewBag.Mes = "Логин и пароль не совпадают, либо такого пользователя нет.";
            }
            
            return View();
        }      
    }
}