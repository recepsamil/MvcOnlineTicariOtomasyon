using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;
using PagedList;
using PagedList.Mvc;
namespace MvcOnlineTicariOtomasyon.Controllers
{
	public class KategoriController : Controller
	{
		// GET: Kategori
		Context c = new Context();
		public ActionResult Index(int sayfa = 1)
		{
			var kategoriler = c.kategories.ToList().ToPagedList(sayfa, 4);
			return View(kategoriler);
		}

		[HttpGet]
		public ActionResult KategoriEkle()
		{
			return View();
		}
		[HttpPost]
		public ActionResult KategoriEkle(Kategori k)
		{
			c.kategories.Add(k);
			c.SaveChanges();
			return RedirectToAction("Index");
		}
		public ActionResult KategoriSil(int id)
		{
			var ktg = c.kategories.Find(id);
			c.kategories.Remove(ktg);
			c.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult KategoriGetir(int id)
		{
			var kategori = c.kategories.Find(id);
			return View("KategoriGetir", kategori);
		}

		public ActionResult KategoriGuncelle(Kategori k)
		{
			var ktgr = c.kategories.Find(k.KategoriId);
			ktgr.KategoriAd = k.KategoriAd;
			c.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult Liste()
		{
			Class2 cs = new Class2();
			cs.Kategoriler = new SelectList(c.kategories, "KategoriId", "KategoriAd");
			cs.Urunler = new SelectList(c.Uruns, "UrunId", "UrunAd");
			return View(cs);
		}

		public JsonResult UrunGetir(int p)
		{
			var urunlistesi = (from x in c.Uruns
									 join y in c.kategories
									 on x.Kategori.KategoriId equals y.KategoriId
									 where x.Kategori.KategoriId == p
									 select new
									 {
										 Text = x.UrunAd,
										 Value = x.UrunId.ToString()
									 }).ToList();
			return Json(urunlistesi,JsonRequestBehavior.AllowGet);
		} 



	}
}