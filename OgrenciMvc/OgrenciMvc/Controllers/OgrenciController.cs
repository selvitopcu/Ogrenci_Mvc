using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using OgrenciMvc.Models.EntityFramework;

namespace OgrenciMvc.Controllers
{
    public class OgrenciController : Controller
    {
        // GET: Ogrenci
        DbMvcOkulEntities1 db = new DbMvcOkulEntities1();
        public ActionResult Index()
        {
            var ogrnciler = db.TBLOGRENCI.ToList();
            return View(ogrnciler);
        }
        [HttpGet]
        public ActionResult YeniOgrenci()
        {
            List<SelectListItem> degerler = (from i in db.TBLKULUPLER.ToList()
                                             select new SelectListItem                                 {
                                                 Text = i.KULUPAD,                             Value = i.KULUPID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }
        [HttpPost]
        public ActionResult YeniOgrenci(TBLOGRENCI p)
        {
            var klp=db.TBLKULUPLER.Where(m=>m.KULUPID==p.TBLKULUPLER.KULUPID).FirstOrDefault();
            p.TBLKULUPLER = klp;
            db.TBLOGRENCI.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            var ogrenci = db.TBLOGRENCI.Find(id);
            db.TBLOGRENCI.Remove(ogrenci);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult OgrenciGetir(int id)
        {
            var ogrenci = db.TBLOGRENCI.Find(id);
            List<SelectListItem> degerler = (from i in db.TBLKULUPLER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KULUPAD,
                                                 Value = i.KULUPID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View("OgrenciGetir", ogrenci);
        }
        public ActionResult Guncelle(TBLOGRENCI p)
        {
            var ogr = db.TBLOGRENCI.Find(p.OGRENCIID);
            ogr.OGRAD = p.OGRAD;
            ogr.OGRSOYAD = p.OGRSOYAD;
            ogr.OGRFOTO = p.OGRFOTO;
            ogr.OGRCINSIYET = p.OGRCINSIYET;
            ogr.OGRKULUP=p.OGRKULUP;
            db.SaveChanges();
            return RedirectToAction("Index","Ogrenci");
        }
    }
}