using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proje5.Models;

namespace Proje5.Controllers
{
    public class MisafirKayitController : FiltreController
    {

        KitapDB db = new KitapDB();

        // GET: MisafirKayit
        public ActionResult Index()
        {
            string misafiradi = Session["username"].ToString();
            var misafir=db.Misafirs.Where(i => i.Mid ==misafiradi).SingleOrDefault();
            ViewBag.misafir = misafiradi;

            return View();
        }


      

      

        public ActionResult Profil()
        {
            string kisim = Session["username"].ToString();
            var kisi = db.Misafirs.Where(i => i.Mid == kisim).SingleOrDefault();

            return View(kisi);
        }
        // GET: MisafirKayit/Details/5
        public ActionResult Details(int id)
        {
            var kisi = db.Misafirs.Where(i => i.Mkayitno == id);
            return View();
        }



        // GET: MisafirKayit/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["username"] == null && Session["adminname"] != null) //admin isen
            {
                var kisi = db.Misafirs.Where(i => i.Mkayitno == id).SingleOrDefault();
                return View(kisi);

            }

         

          
            else if (Session["username"] != null && Session["adminname"] == null) // misaafirsinsindir.
            {
                string kisim = Session["username"].ToString();
                var kisim2 = db.Misafirs.Where(i => i.Mid == kisim).SingleOrDefault(); // yapan kişi
                if (kisim2.Mkayitno == id)
                { var kisi = db.Misafirs.Where(i => i.Mkayitno == id).SingleOrDefault(); return View(kisi); } 

                return HttpNotFound(); // farklı biri denerse
            }

            return HttpNotFound();

        }
      
        public ActionResult Kitapiade(int id)
        {

            return RedirectToAction("Kitapiade", "KitapEkleme",new {@Mkitapno=id });
        }
        // POST: AdminKayit/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Misafir model)
        {
            try
            {
                var kisi = db.Misafirs.Where(i => i.Mkayitno == id).SingleOrDefault();
                kisi.Mad = model.Mad;
                kisi.Mid = model.Mid;
                kisi.Msifre = model.Msifre;
                kisi.Msoyad = model.Msoyad;
                db.SaveChanges();


                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    
       
    }
}
