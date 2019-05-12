using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CelicniProfili.Models;

namespace CelicniProfili.Controllers{

	public class ProfiliMeniController : Controller {

		//*********************
		// GET: Meni za profile
		//*********************
		public ActionResult Meni () {
			UsersController.VisiblesInd = new int[] { 3, 4, 5 };
			ViewBag.Poruka = String.Empty;

			return View();
		}

		//***********************
		//***MonoblockList GET
		//**********************
		//public ActionResult MonoblockList () {

		//	using (ČeličniProfiliEntities db = new ČeličniProfiliEntities()) {
				

		//	}
		//}

	}
}
