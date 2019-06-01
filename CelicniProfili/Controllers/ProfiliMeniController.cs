using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CelicniProfili.Models;
using CelicniProfili.ViewModels;

namespace CelicniProfili.Controllers
{

	public class ProfiliMeniController : Controller
	{

		//*********************
		// GET: Meni za profile
		//*********************
		public ActionResult Meni () {
			//ako je ulogovan
			if (Session["UserId"] != null) {
				int level = Convert.ToInt16(Session["Level"]);
				ViewBag.User = Session["UserName"];
				ViewBag.Status = (level == 0) ? "Admin" : "User";

				UsersController.VisiblesInd = new int[] { 3, 4 };
				ViewBag.Poruka = String.Empty;

				return View();
			}
			else
				return RedirectToAction("Login");
		}

		//***********************
		//***MonoblockList GET
		//**********************
		public ActionResult MonoblockList (string poruka2="") {
			if (Session["UserId"] != null) {
				int level = Convert.ToInt16(Session["Level"]);
				ViewBag.User = Session["UserName"];
				ViewBag.Status = (level == 0) ? "Admin" : "User";
				ViewBag.Poruka = poruka2;

				UsersController.VisiblesInd = new int[] { 3, 4 };

				List<SelectListItem> tehnList1 = LoadTehn_list();
				ViewData["Tehnologija"] = tehnList1;

				return View();
			}
			else
				return RedirectToAction("Login");
		}


		//***********************
		//***MonoblockList Post
		//**********************
		[HttpPost]
		public ActionResult MonoblockList (MonoblokViewModel MonBLViewModel1) {
			if (Session["UserId"] != null) {
				UsersController.VisiblesInd = new int[] { 3, 4 };
				return View();
			}
			else
				return RedirectToAction("Login");
		}


		//******************************
		//puni list tehnologija_monoblok
		//*******************************
		private List<SelectListItem> LoadTehn_list () {
			List<SelectListItem> TehnList1 = new List<SelectListItem>();

			using (ČeličniProfiliEntities db = new ČeličniProfiliEntities()) {

				DbSet<tehnologija_monoblok> tehnTab = db.tehnologija_monoblok;

				foreach (var tehnItem1 in tehnTab) {
					TehnList1.Add(new SelectListItem {
						Text = tehnItem1.tehnologija,
						Value = tehnItem1.ID_tehn.ToString()
					});
				}
			}
			return TehnList1;
		}

		//************************************
		//puni listu tip_monoblok i vraća JSON
		//*************************************
		public JsonResult GetTips (string tehn_id) {
			List<SelectListItem> TipList1 = new List<SelectListItem>();
			TipList1.Add(new SelectListItem { Text = "Select", Value = "0" });

			if (tehn_id != String.Empty) {

				using (ČeličniProfiliEntities db = new ČeličniProfiliEntities()) {

					DbSet<tip_monoblok> tipTab = db.tip_monoblok;

					foreach (var tipItem1 in tipTab) {
						if (tipItem1.id_tehn == Convert.ToInt32(tehn_id)) {
							TipList1.Add(new SelectListItem {
								Text = tipItem1.tip,
								Value = tipItem1.id_tip.ToString(),
							});
						}
					}
				}
			}
			return Json(new SelectList(TipList1, "Value", "Text"));
		}

		//************************************
		//puni listu monoblokova i vraća JSON
		//*************************************
		public JsonResult GetMonoblocks (string tip_id) {

			List<ModelZaPrikazMonobloka_u_listi> MonoLst1 = new List<ModelZaPrikazMonobloka_u_listi> ();

			using (ČeličniProfiliEntities db = new ČeličniProfiliEntities()) {
				int ID_tip = Convert.ToInt32(tip_id);

				DbSet<Monoblok> MonoTabela = db.Monoblok;

				var selectedMono = MonoTabela.Where(x => x.ID_tip.Equals(ID_tip));

				foreach (var monoBl1 in selectedMono) {
					MonoLst1.Add(new ModelZaPrikazMonobloka_u_listi(monoBl1.ID));
				}
			}

			var JSonO = Json(MonoLst1);
			return JSonO;
		}


	}
}