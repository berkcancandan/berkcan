using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proje5.Models;

namespace Proje5.Controllers
{
    public class HomeController : Controller
    {
        KitapDB db = new KitapDB();
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Agiris()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Agiris(Admin model)
        {
            try
            {
                var varmi = db.Admins.Where(i => i.Aid == model.Aid).SingleOrDefault();
                if (varmi == null)
                {
                    return View();
                    //id yok
                }
                if (varmi.Asifre == model.Asifre)
                {


                    Session["adminname"] = varmi.Aid;

                    return RedirectToAction("Index", "AdminKayit");

                }
                else
                {
                    return View();
                    //şifre yanlış

                }


            }
            catch
            {

            }

            return View();
        }
        public ActionResult Logout()
        {
            Session["username"] = null;
            Session["adminname"] = null;
            return RedirectToAction("Index", "Home");
            //çıkıncaaa nereye gelsin
        }

        public ActionResult Mgiris()
        {

            return View();

        }
        [HttpPost]
        public ActionResult Mgiris(Misafir model)
        {
            try
            {
                var varmi = db.Misafirs.Where(i => i.Mid == model.Mid).SingleOrDefault();
                if (varmi == null)
                {
                    return View();
                    //id yok
                }
                if (varmi.Msifre == model.Msifre)
                {


                    Session["username"] = varmi.Mid;

                    return RedirectToAction("Index", "MisafirKayit");

                }
                else
                {
                    return View();
                    //şifre yanlış

                }


            }
            catch
            {

            }

            return View();
        }


        // GET: MisafirKayit/Create
        public ActionResult Create()
        {
            return View();


        }
        [HttpPost]
        public ActionResult Create(Misafir model)
        {
            try
            {
                var varmi = db.Misafirs.Where(i => i.Mid == model.Mid).SingleOrDefault(); //farklı kullanıcı adı ekle

                if (varmi != null)  //kayıtlı zaten bu kullanıcı 
                {
                    ViewBag.uyari = "Bu Adla Kullanıcı Kayıtlıdır";
                    return View();

                }
                 if(model.Msifre<0)  { ViewBag.uyari = "Lütfen farklı bir şifre seçiniz"; return View();   } //şifre kontrol

                db.Misafirs.Add(model);
                db.SaveChanges();

                if (Session["adminname"] == null) { Session["username"] = model.Mid; return RedirectToAction("Profil", "MisafirKayit"); }
               
                return RedirectToAction("Listele", "AdminKayit");
            

                //  return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}