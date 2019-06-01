using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CelicniProfili.Models;
using CelicniProfili.ViewModels;

namespace CelicniProfili.Controllers
{
	public class MonobloksController : Controller
	{

		// GET: Monobloks
		//public ActionResult Index()
		//{
		//	var monoblok = db.Monoblok.Include(m => m.I_geometrija).Include(m => m.I_karakteristike).Include(m => m.standard).Include(m => m.tip_monoblok);
		//	return View(monoblok.ToList());
		//}

		// GET: Monobloks/Details/5
		//public ActionResult Details(int? id)
		//{
		//	if (id == null)
		//	{
		//		return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
		//	}
		//	Monoblok monoblok = db.Monoblok.Find(id);
		//	if (monoblok == null)
		//	{
		//		return HttpNotFound();
		//	}
		//	return View(monoblok);
		//}

		// GET: Monobloks/Create
		//public ActionResult Create()
		//{
		//	ViewBag.ID = new SelectList(db.I_geometrija, "ID", "ID");
		//	ViewBag.ID = new SelectList(db.I_karakteristike, "ID", "ID");
		//	ViewBag.ID_standard = new SelectList(db.standard, "Id_standard", "Naziv");
		//	ViewBag.ID_tip = new SelectList(db.tip_monoblok, "id_tip", "tip");
		//	return View();
		//}

		// POST: Monobloks/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public ActionResult Create([Bind(Include = "ID,ID_standard,Naziv,ID_tip")] Monoblok monoblok)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		db.Monoblok.Add(monoblok);
		//		db.SaveChanges();
		//		return RedirectToAction("Index");
		//	}

		//	ViewBag.ID = new SelectList(db.I_geometrija, "ID", "ID", monoblok.ID);
		//	ViewBag.ID = new SelectList(db.I_karakteristike, "ID", "ID", monoblok.ID);
		//	ViewBag.ID_standard = new SelectList(db.standard, "Id_standard", "Naziv", monoblok.ID_standard);
		//	ViewBag.ID_tip = new SelectList(db.tip_monoblok, "id_tip", "tip", monoblok.ID_tip);
		//	return View(monoblok);
		//}

		//******************************
		//** Editovanje Monobloka GET  ***
		//** Monobloks/Edit/5        ***
		//******************************
		public ActionResult Edit (int id= -1) {
			
			if (Session["UserId"] == null)
				return RedirectToAction("Login", "Users");

			if (id== -1) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			int level = Convert.ToInt16(Session["Level"]);
			ViewBag.User = Session["UserName"];
			ViewBag.Status = (level == 0) ? "Admin" : "User";
			ViewBag.poruka = null;

			using (ČeličniProfiliEntities db = new ČeličniProfiliEntities()) { 
				
				Monoblok mblok1 = db.Monoblok.Find(id);
				
				if (mblok1 == null) {
					return HttpNotFound();
				}

				modelMonoblokZaEdit MblokEdit1 = new modelMonoblokZaEdit(id);

				return View(MblokEdit1);
			}
		}

		// POST: Monobloks/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public ActionResult Edit([Bind(Include = "ID,ID_standard,Naziv,ID_tip")] Monoblok monoblok)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		db.Entry(monoblok).State = EntityState.Modified;
		//		db.SaveChanges();
		//		return RedirectToAction("Index");
		//	}
		//	ViewBag.ID = new SelectList(db.I_geometrija, "ID", "ID", monoblok.ID);
		//	ViewBag.ID = new SelectList(db.I_karakteristike, "ID", "ID", monoblok.ID);
		//	ViewBag.ID_standard = new SelectList(db.standard, "Id_standard", "Naziv", monoblok.ID_standard);
		//	ViewBag.ID_tip = new SelectList(db.tip_monoblok, "id_tip", "tip", monoblok.ID_tip);
		//	return View(monoblok);
		//}

		//******************************
		//**Brisanje Monobloka GET***
		//******************************
		public ActionResult DeleteMono (int? id)
		{
			if (id != null) {
				using (ČeličniProfiliEntities db = new ČeličniProfiliEntities()) {

					Monoblok monoblok1 = db.Monoblok.Find(id);

					UsersController.VisiblesInd = new int[] { 3, 4 };
					ViewBag.poruka = "Potvrdi brisanje bloka: " + monoblok1.Naziv;
					return View("Delete");
				}
			}
			else {
				return RedirectToAction("MonoblockList", "ProfiliMeni", new { poruka2 = "Nepostojeći monoblok" });
			}					
		}

		//********************************
		//**Brisanje Monobloka POST ***
		//********************************
		// POST: Monobloks/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed (int? id)
		{
			string poruka;

			if (id != null) {
				using (ČeličniProfiliEntities db = new ČeličniProfiliEntities()) {
					Monoblok monoblok1 = db.Monoblok.Find(id);

					if (monoblok1 != null) {
						UsersController.VisiblesInd = new int[] { 3, 4 };

						db.Monoblok.Remove(monoblok1);
						db.SaveChanges();
						poruka = "Izbrisan Monoblok";
					}
					else
						poruka = "Nepostojeći monoblok";
				}
			}
			else
				poruka = "Nepostojeći monoblok";

			return RedirectToAction("MonoblockList", "ProfiliMeni", new { poruka2 = poruka });
		}

		//protected override void Dispose (bool disposing) {
		//	if (disposing) {
		//		db.Dispose();
		//	}
		//	base.Dispose(disposing);
		//}
	}
}
