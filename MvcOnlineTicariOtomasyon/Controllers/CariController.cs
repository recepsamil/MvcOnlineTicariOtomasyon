using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
	public class CariController : Controller
	{
		// GET: Cari
		Context c = new Context();
		public ActionResult Index()
		{
			var cariler = c.Carilers.Where(x => x.Durum == true).ToList();
			return View(cariler);
		}

		[HttpGet]
		public ActionResult YeniCari() 
		{
			return View();
		}
		[HttpPost]
		public ActionResult YeniCari(Cariler p)
		{
			
			p.Durum = true;
			c.Carilers.Add(p);
			
			c.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult CariSil(int id)
		{
			var cariler = c.Carilers.Find(id);
			cariler.Durum = false;
			c.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult CariGetir(int id)
		{
			var cari = c.Carilers.Find(id);
			return View("CariGetir", cari);
		}

		public ActionResult CariGuncelle(Cariler p)
		{
			if(!ModelState.IsValid)
			{
				return View("CariGetir");
			}
			var cari = c.Carilers.Find(p.CariID);
			cari.CariAd = p.CariAd;
			cari.CariSoyad = p.CariSoyad;
			cari.CariSehir = p.CariSehir;
			cari.CariMail = p.CariMail;
			c.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult MusteriSatis(int id)
		{ 
			var degerler = c.SatisHarekets.Where(x=> x.CariId == id).ToList();
			var cr = c.Carilers.Where(x => x.CariID == id).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
			ViewBag.cari = cr;
			return View(degerler);
		}

	}
}