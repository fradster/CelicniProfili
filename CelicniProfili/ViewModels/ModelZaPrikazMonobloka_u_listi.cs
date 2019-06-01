using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CelicniProfili.Models;

namespace CelicniProfili.ViewModels
{
	public class ModelZaPrikazMonobloka_u_listi
	{
		public int Id { get; set; }
		public string Standard { get; set; }
		public string Naziv { get; set; }
		public double b { get; set; }
		public double h { get; set; }
		public double Wx { get; set; }

		public ModelZaPrikazMonobloka_u_listi (int Ind) {
			Id = Ind;
			using (ČeličniProfiliEntities db = new ČeličniProfiliEntities()) {

				Monoblok Mono1 = db.Monoblok.Find(Ind);

				Standard = db.standard.Where(x => x.Id_standard.Equals(Mono1.ID_standard)).First().Naziv;
				Naziv = Mono1.Naziv;
				b = db.I_geometrija.Find(Ind).b;
				h = db.I_geometrija.Find(Ind).h;
				Wx= db.I_karakteristike.Find(Ind).Wx;
			}
		}
	}
}