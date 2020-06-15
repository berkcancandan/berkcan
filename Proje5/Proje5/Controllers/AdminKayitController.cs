using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proje5.Models;

namespace Proje5.Controllers
{
    public class AdminKayitController : FiltreController
    {

        KitapDB db = new KitapDB();

        // GET: AdminKayit
        public ActionResult Index()
        {
            string adminadi = Session["adminname"].ToString();
            var admin = db.Admins.Where(i => i.Aad == adminadi).SingleOrDefault();
            ViewBag.admin = adminadi;
            return View();
        }

        // GET: AdminKayit/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }



     

        // GET: AdminKayit/Create
        public ActionResult Create()
        {
            if(Session["username"]!=null)
            {
                HttpNotFound();// kullanıcı admin create giremesin
            }
            return View();
        }

        // POST: AdminKayit/Create
        [HttpPost]
        public ActionResult Create(Admin model)
        {
            try
            {
                var varmi = db.Misafirs.Where(i => i.Mid == model.Aid).SingleOrDefault(); //farklı kullanıcı adı ekle
                if (varmi != null)  //kayıtlı zaten bu kullanıcı 
                {
                    return View();
                }
            
                db.Admins.Add(model);
                db.SaveChanges();
                Session["adminname"] = model.Aid;
                return RedirectToAction("Index", "AdminKayit");
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminKayit/Edit/5
        public ActionResult Edit(int id)
        {
            var kisi = db.Misafirs.Where(i => i.Mkayitno == id).SingleOrDefault();

            return View(kisi);
        }
        public ActionResult Listele()
        {

            var liste = db.Misafirs;
            return View(liste);
        }
        [HttpPost]
        public ActionResult Listele(string misafirid)
        {

            var liste = db.Misafirs;
            return View(liste);
        }


        // POST: AdminKayit/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Admin model)
        {
            try
            {
                var kisi = db.Admins.Where(i => i.Akayitno == id).SingleOrDefault();
                kisi.Aad = model.Aad;
                kisi.Aid = model.Aid;
                kisi.Asifre = model.Asifre;
                kisi.Asoyad = model.Asoyad;
                db.SaveChanges();
               

                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: AdminKayit/Delete/5
      

        // POST: AdminKayit/Delete/5
       
        public ActionResult Delete(int id)
        {
            try
            {
                var misafir = db.Misafirs.Where(i => i.Mkayitno == id).SingleOrDefault();
                var misafiradi = misafir.Mad;
                var Kitapvarmi = db.Kitaplars.Where(i => i.Kitapno == misafir.Mkitapno).SingleOrDefault(); // kitap listesinden kitabı bulur
                var Kitapkaydi = db.Kayitlars.Where(i => i.Bkitapno == misafir.Mkitapno).SingleOrDefault(); // kayıt listesinden kitab

                if (Kitapvarmi != null)
                {
                    Kitapvarmi.Kdurum = "Free";
                    Kitapvarmi.Kkayitli = null;
                    db.Kayitlars.Remove(Kitapkaydi);
                }
                db.Misafirs.Remove(misafir);
                db.SaveChanges();   
                return RedirectToAction("Listele");
            }
            catch
            {
                return RedirectToAction("Listele");
            }
        }
    
    }
}
