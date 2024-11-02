using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class DepartmanController : Controller
    {
		// GET: Departman
		Context c = new Context();
		public ActionResult Index()
		{
			var dpr = c.Departmans.Where(x => x.Durum == true).ToList();
			return View(dpr);
		}


		[Authorize(Roles ="A")]
		[HttpGet]
		public ActionResult DepartmanEkle()
		{
			return View();
		}

		[HttpPost]
		public ActionResult DepartmanEkle(Departman d)
		{
			d.Durum = true;
			c.Departmans.Add(d);
			c.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult DepartmanSil(int id)
		{
			var departman = c.Departmans.Find(id);
			departman.Durum = false;
			c.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult DepartmanGetir(int id)
		{
			var departman = c.Departmans.Find(id);
			return View("DepartmanGetir", departman);
		}

		public ActionResult DepartmanGuncelle(Departman d)
		{
			var departman = c.Departmans.Find(d.DepartmanId);
			departman.DepartmanAd = d.DepartmanAd;
			c.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult DepartmanDetay(int id)
		{
			var degerler = c.Personels.Where(x => x.DepartmanID == id).ToList();
			var dpt = c.Departmans.Where(x => x.DepartmanId == id).Select(y => y.DepartmanAd).FirstOrDefault();
			ViewBag.d = dpt;
			return View(degerler);
		}

		public ActionResult DepartmanPersonelSatis(int id)
		{
			var degerler = c.SatisHarekets.Where(x => x.PersonelID == id).ToList();
			var per = c.Personels.Where(x => x.PersonelId == id).Select(y => y.PersonelAd + y.PersonelSoyad).FirstOrDefault();
			ViewBag.dpers = per;
			return View(degerler);

		}
	}
}