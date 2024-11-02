﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
	public class mesajlar
	{
		[Key]
		public int MesajId { get; set; }

		[Column(TypeName = "Varchar")]
		[StringLength(50)]
		public string Gonderici { get; set; }

		[Column(TypeName = "Varchar")]
		[StringLength(50)]
		public string Alici { get; set; }

		[Column(TypeName = "Varchar")]
		[StringLength(50)]
		public string Konu { get; set; }

		[Column(TypeName = "Varchar")]
		[StringLength(2000)]
		public string Icerik { get; set; }

		[Column(TypeName = "Smalldatetime")]
		public DateTime Tarih { get; set; }
	}
}