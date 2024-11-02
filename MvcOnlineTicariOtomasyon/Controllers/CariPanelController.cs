using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
	public class CariPanelController : Controller
	{
		// GET: CariPanel
		Context c = new Context();
		[Authorize]
		public ActionResult Index()
		{
			var mail = (string)Session["CariMail"];
			var degerler = c.mesajlars.Where(x => x.Alici == mail).ToList();
			ViewBag.m = mail;
			var mailid = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariID).FirstOrDefault();
			ViewBag.mid = mailid;
			var toplamsatis = c.SatisHarekets.Where(x => x.CariId == mailid).Count();
			ViewBag.tst = toplamsatis;
			var toplamtutar = c.SatisHarekets.Where(x => x.CariId == mailid).Sum(y => y.ToplamTutar);
			ViewBag.toplamtutar = toplamtutar;
			var toplamurunsayisi = c.SatisHarekets.Where(x => x.CariId == mailid).Sum(y => y.Adet);
			ViewBag.toplamurunsayisi = toplamurunsayisi;
			var adsoyad = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
			ViewBag.adsoyad = adsoyad;
			var Mail = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariMail).FirstOrDefault();
			ViewBag.Mail = Mail;
			var sehir = c.Carilers.Where(x => x.CariSehir == mail).Select(y => y.CariSehir).FirstOrDefault();
			ViewBag.sehir = sehir;
			return View(degerler);
		}

		public ActionResult Siparislerim()
		{
			var mail = (string)Session["CariMail"];
			var id = c.Carilers.Where(x => x.CariMail == mail.ToString()).Select(y => y.CariID).FirstOrDefault();
			var degerler = c.SatisHarekets.Where(x => x.CariId == id).ToList();
			return View(degerler);
		}

		public ActionResult GelenMesajlar()
		{
			var mail = (string)Session["CariMail"];
			var mesajlar = c.mesajlars.Where(x => x.Alici == mail).OrderByDescending(x => x.MesajId).ToList();
			var gelensayisi = c.mesajlars.Count(x => x.Alici == mail).ToString();
			ViewBag.d1 = gelensayisi;
			var gidensayisi = c.mesajlars.Count(x => x.Gonderici == mail).ToString();
			ViewBag.d2 = gidensayisi;
			return View(mesajlar);
		}

		public ActionResult GidenMesajlar()
		{
			var mail = (string)Session["CariMail"];
			var mesajlar = c.mesajlars.Where(x => x.Gonderici == mail).ToList();
			var gelensayisi = c.mesajlars.Count(x => x.Alici == mail).ToString();
			ViewBag.d1 = gelensayisi;
			var gidensayisi = c.mesajlars.Count(x => x.Gonderici == mail).ToString();
			ViewBag.d2 = gidensayisi;
			return View(mesajlar);
		}

		public ActionResult MesajDetay(int id)
		{
			var mesajlar = c.mesajlars.Where(x => x.MesajId == id).ToList();
			var mail = (string)Session["CariMail"];
			var gelensayisi = c.mesajlars.Count(x => x.Alici == mail).ToString();
			ViewBag.d1 = gelensayisi;
			var gidensayisi = c.mesajlars.Count(x => x.Gonderici == mail).ToString();
			ViewBag.d2 = gidensayisi;
			return View(mesajlar);
		}

		[HttpGet]
		public ActionResult YeniMesaj()
		{
			var mail = (string)Session["CariMail"];
			var gelensayisi = c.mesajlars.Count(x => x.Alici == mail).ToString();
			ViewBag.d1 = gelensayisi;
			var gidensayisi = c.mesajlars.Count(x => x.Gonderici == mail).ToString();
			ViewBag.d2 = gidensayisi;
			return View();
		}

		[HttpPost]
		public ActionResult YeniMesaj(mesajlar m)
		{
			var mail = (string)Session["CariMail"];
			m.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
			m.Gonderici = mail;
			c.mesajlars.Add(m);
			c.SaveChanges();
			return View();
		}

		public ActionResult KargoTakip(string p)
		{

			var kargo = from x in c.KargoDetays select x;

			kargo = kargo.Where(y => y.TakipKodu.Contains(p));
			return View(kargo.ToList());
		}

		public ActionResult CariKargoTakip(string id)
		{
			var degerler = c.KargoTakips.Where(x => x.TakipKodu == id).ToList();
			return View(degerler);
		}

		public ActionResult LogOut()
		{
			FormsAuthentication.SignOut();
			Session.Abandon();
			return RedirectToAction("Index", "Login");
		}

		public PartialViewResult Partial1()
		{
			var mail = (string)Session["CariMail"];
			var id = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariID).FirstOrDefault();
			var caribul = c.Carilers.Find(id);
			return PartialView("Partial1", caribul);

		}
		public PartialViewResult Partial2()
		{
			var veriler = c.mesajlars.Where(x => x.Gonderici == "admin").ToList();
			return PartialView(veriler);
		}

		public ActionResult CariBilgiGuncelle(Cariler cr) 
		{
			var cari = c.Carilers.Find(cr.CariID);
			cari.CariAd = cr.CariAd;
			cari.CariSoyad = cr.CariSoyad;
			cari.CariMail = cr.CariMail;
			cari.CariSehir = cr.CariSehir;
			cari.CariSifre = cr.CariSifre;
			cr.Durum = true;
			c.SaveChanges();
			return RedirectToAction("Index");
		}



	}
}