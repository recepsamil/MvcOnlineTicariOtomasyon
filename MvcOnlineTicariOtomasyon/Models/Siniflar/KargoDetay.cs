﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
	public class KargoDetay
	{
		[Key]
		public int KargoDetayId { get; set; }


		[Column(TypeName = "Varchar")]
		[StringLength(300)]
		public string Aciklama { get; set; }



		[Column(TypeName = "Varchar")]
		[StringLength(10)]
		public string TakipKodu { get; set; } //1234123AB


		[Column(TypeName = "Varchar")]
		[StringLength(20)]
		public string Personel { get; set; }


		[Column(TypeName = "Varchar")]
		[StringLength(25)]
		public string Alici { get; set; }
		public DateTime Tarih { get; set; }
	}
}