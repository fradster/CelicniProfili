using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CelicniProfili.Models;
using System.Data.Entity;

namespace CelicniProfili.ViewModels {
	
	//klasa za ViewModel prikaza liste Monoblokova
	public class MonoblokViewModel
	{
		//lista za tehnologija_monoblok tabelu
		public List<SelectListItem> tehnItems{ get; set; }
		
		public int tehnSelected { get; set; }

		//lista za tip_monoblok tabelu
		public List<SelectListItem> tipItems { get; set; }

		public int tipSelected { get; set; }

		//lista monoblokova
		public List<ModelZaPrikazMonobloka_u_listi> monoblokItems { get; set; }


		//*****************************
		//***konstruktor bez argumenata
		//*****************************
		public MonoblokViewModel (int ID_tehn) {
			tehnSelected = ID_tehn;

			using (ČeličniProfiliEntities db = new ČeličniProfiliEntities()) {
				//adresiram tebelu
				DbSet<tehnologija_monoblok> tehnTab = db.tehnologija_monoblok;

				//popuni tehnItems listu
				foreach (var tehnItem1 in tehnTab) {
					tehnItems.Add(new SelectListItem {
						Text = tehnItem1.tehnologija,
						Value = tehnItem1.ID_tehn.ToString(),
						Selected = (tehnItem1.ID_tehn == tehnSelected) ? true : false
					});
				}

				//popuni tipItems listu ako je lista tehnologije selectovana
				if (tehnSelected > 0) {

					//adresiram tebelu
					DbSet<tip_monoblok> tipTab = db.tip_monoblok;

					//popuni listu samo sa tipovima koji odgovaraju selektovanoj tehnologiji
					IQueryable<tip_monoblok> CurentTip = tipTab.Where(x => x.id_tehn.Equals(tehnSelected));

					//popuni tipItems listu
					foreach (var tipItem1 in CurentTip) {
						tipItems.Add(new SelectListItem {
							Text = tipItem1.tip,
							Value = tipItem1.id_tip.ToString(),
							Selected = (tipItem1.id_tip == tipSelected) ? true : false
						});
					}

					//popuni listu monoblokova ako je selektovano nešto iz tipa
					if (tipSelected > 0) {
						List<ModelZaPrikazMonobloka_u_listi> monoList = new List<ModelZaPrikazMonobloka_u_listi>();

						//izdvaja sve monoblokove koji su zadatog tipa
						IQueryable<Monoblok> monBlSelected = db.Monoblok.Where(x => x.ID_tip.Equals(tipSelected));

						foreach (var mono1 in monBlSelected) {
							int ID = mono1.ID;
							monoList.Add(new ModelZaPrikazMonobloka_u_listi(ID));
						}

						monoblokItems = monoList;
					}
				}
			}
		}
	}
}