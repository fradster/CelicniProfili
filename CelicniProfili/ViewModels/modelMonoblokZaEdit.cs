using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CelicniProfili.Models;
using System.Web.Mvc;


namespace CelicniProfili.ViewModels
{
	public class modelMonoblokZaEdit
	{
		public Monoblok mblok1 { get; set; }

		public I_geometrija monoGeom1 { get; set; }

		public I_karakteristike monoKarakt1 { get; set; }

		public List<SelectListItem> mStand1 { get; set; }

		public int SelectedStandard1 { get; set; }

		public List<monoblok_pozicije_ojačanja> pozicOjač1 { get; set; }

		//private DbSet<mon_opis> opis_table1 { get; set; }
		private List<mon_opis> opis_table1;
		public double[,] monOpis1 { get; set; }


		//**************************
		//konstuktor za Id monobloka
		//**************************
		public modelMonoblokZaEdit (int Id_mono) {

			using (ČeličniProfiliEntities db = new ČeličniProfiliEntities()) {

				this.mblok1 = db.Monoblok.Find (Id_mono);
				this.monoGeom1 = db.I_geometrija.Find (Id_mono);
				this.monoKarakt1 = db.I_karakteristike.Find(Id_mono);

				DbSet<standard> Standard1 = db.standard;

				mStand1 = new List<SelectListItem>();

				foreach (var std1 in Standard1){
					this.mStand1.Add(
						new SelectListItem {
							Value = std1.Id_standard.ToString(),
							Text = std1.Naziv,
							Selected = (std1.Id_standard==1 )? true: false
						}
					);
				}

				SelectedStandard1 = Convert.ToInt32(mStand1.Where(x => x.Selected).FirstOrDefault().Value );

				this.mblok1.ID_standard = SelectedStandard1;

				this.pozicOjač1 = db.monoblok_pozicije_ojačanja.Where(x => x.ID_mono == Id_mono).ToList();

				//vadi tabelu iz mon_opis i izdvaja array koordinata
				
				
				opis_table1  = db.mon_opis.Where(x => x.ID_mono == Id_mono).ToList ();

				int br_rows = opis_table1.Count();
				this.monOpis1 = new double[br_rows, 2];

				for (int i1 = 0; i1< br_rows; i1++) {
					this.monOpis1 [i1, 0] = opis_table1.ElementAt(i1).Node_x;
					this.monOpis1 [i1, 1] = opis_table1.ElementAt(i1).Node_y;
				}
			}
		}
	}
}