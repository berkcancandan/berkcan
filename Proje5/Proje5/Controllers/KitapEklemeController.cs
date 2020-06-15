using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proje5.Models;


namespace Proje5.Controllers
{
    public class KitapEklemeController : FiltreController
    {
        KitapDB db = new KitapDB();


        // GET: KitapAlma
        public ActionResult Index()
        {

            return View();
        }

        // GET: KitapAlma/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

       
     
   
        
        public ActionResult Kiralama(int Kitapno)
        {
            Kayitlar model = new Kayitlar();
            string misafirid = Session["username"].ToString();
                var misafir = db.Misafirs.Where(i => i.Mid == misafirid).SingleOrDefault();
                var Mkayitlikitap = db.Kayitlars.Where(i => i.Bkayitno == misafir.Mkayitno).SingleOrDefault(); // misaafirde kitap var mı

                if (misafir.Mkitapno!=null)
                {
                    return View(); // zaten kayıtlı kitabı var
                }
                var Kitapvarmi=db.Kitaplars.Where(i => i.Kitapno == Kitapno).SingleOrDefault();

                if (Mkayitlikitap == null && Kitapvarmi.Kkayitli == null)
                {
                    model.Bkayitno = misafir.Mkayitno; //sessiondan kayit no çekme
                     model.Bkitapno = Kitapno;
                     Kitapvarmi.Kkayitli = misafir.Mkayitno;
                    misafir.Mkitapno = Kitapvarmi.Kitapno;
                    Kitapvarmi.Kdurum = "Taken";
                    db.Kayitlars.Add(model);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                } // Kayıt var mı 
                else if (Kitapvarmi == null)
                {
                    return View(); // kayıt edilmek istenen kitap yoksa

                }
                return View();
        }
        
        public ActionResult Kitapiade(int Mkitapno)
        {
            var Kitapvarmi = db.Kitaplars.Where(i => i.Kitapno == Mkitapno).SingleOrDefault();
            return View(Kitapvarmi);


        }
        [HttpPost]
        public ActionResult Kitapiade()
        {
            string misafirid = Session["username"].ToString();
            var misafir = db.Misafirs.Where(i => i.Mid == misafirid).SingleOrDefault();  // kullanıcı

            var Kitapvarmi = db.Kitaplars.Where(i => i.Kitapno == misafir.Mkitapno).SingleOrDefault(); // kitap listesinden kitabı bulur
            var Kitapkaydi = db.Kayitlars.Where(i => i.Bkitapno == misafir.Mkitapno).SingleOrDefault(); // kayıt listesinden kitab

            misafir.Mkitapno = null;
            Kitapvarmi.Kdurum = "Free";
            Kitapvarmi.Kkayitli = null;
            db.Kayitlars.Remove(Kitapkaydi);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");


        }
        public ActionResult Create()
        {
            if (Session["username"] != null)
            {
                HttpNotFound();// kullanıcı admin create giremesin
            }

            return View();
           

        }

        public ActionResult KitapListesi(String AramaYap = null)
        {

            var kitaplar =from a in db.Kitaplars select a;
            if(!string.IsNullOrEmpty(AramaYap))
            {
                kitaplar = kitaplar.Where(i => i.Kadi.Contains(AramaYap));
                if(kitaplar==null)  kitaplar = kitaplar.Where(i => i.Ketiketleri.Contains(AramaYap));
                if (kitaplar == null) kitaplar = kitaplar.Where(i => i.Kyazar.Contains(AramaYap));

            }
            return View(kitaplar);
        }
        // POST: KitapAlma/Create
        [HttpPost]
        public ActionResult Create(Kitaplar model)
        {
            
           
            try
            {
               var varmi = db.Kitaplars.Where(i => i.Kadi == model.Kadi).SingleOrDefault(); //farklı kitap ekle
                                                                                              // TODO: Add insert logic here
              if (varmi != null)            {  return View();     }
                model.Kdurum = "Free";
                db.Kitaplars.Add(model);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            catch
            {
            
                return View();
            }
        }

        // GET: KitapAlma/Edit/5
        public ActionResult Edit(int id)
        {
            var kitap = db.Kitaplars.Where(i => i.Kitapno == id).SingleOrDefault();

            return View(kitap);
        }

        // POST: KitapAlma/Edit/5
        [HttpPost]
        public ActionResult Edit(Kitaplar model)
        {
            try
            {
                var kitap = db.Kitaplars.Where(i => i.Kitapno == model.Kitapno).SingleOrDefault();
                kitap.Kadi = model.Kadi;
                kitap.Kyazar = model.Kyazar;
                kitap.Kdurum= model.Kdurum;
                kitap.Ketiketleri = model.Ketiketleri;
                kitap.Kkayitli = model.Kkayitli;
                db.SaveChanges();
                TempData["msg"] = "<script>alert('Değiştirme Başarılı');</script>";
                
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: KitapAlma/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {

                var kitap = db.Kitaplars.Where(i => i.Kitapno == id).SingleOrDefault();
               

                var Kitapkaydi = db.Kayitlars.Where(i => i.Bkitapno == kitap.Kitapno).SingleOrDefault(); // kayıt listesinden kitab
                if (Kitapkaydi != null)
                {
                    var Kitapvarmi = db.Misafirs.Where(i => i.Mkayitno == Kitapkaydi.Bkayitno).SingleOrDefault(); // kitap kayıtlı ise kullanıcı bulma
                    Kitapvarmi.Mkitapno= null;

                    db.Kayitlars.Remove(Kitapkaydi);
                }

             


                db.Kitaplars.Remove(kitap);
                db.SaveChanges();
               

                return RedirectToAction("KitapListesi");
            }
            catch
            {
                return RedirectToAction("KitapListesi");
            }
        }

       
      
    }
}
