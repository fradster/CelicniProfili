using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CelicniProfili.Models;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;

namespace CelicniProfili.Controllers{

	public class UsersController : Controller {

		//*********************************************
		//**lista svih akcija unutar kontrolera home i njihovih naziva
		//*********************************************
		public static List<string> ActionList = new List<string> { "Login", "SignUp", "SignOut", "About", "UserPanel", };
		public static List<string> ActionNames = new List<string> { "Login", "Sign Up", "Sign Out", "About", "Korisnički Panel" };
		//List<string> ControllerList = new List<string> { "Home" };
		public static int[] VisiblesInd;// lista vidljivih akcija (broji od 1)


		//***********
		//**Login GET
		//***********
		public ActionResult Login () {
			//lista redosleda prikaza akcija u meniju
			VisiblesInd = new int[] { 2, 4 };
			ViewBag.Poruka = String.Empty;
			ViewBag.Action = "Login";
			return View();
		}


		//********************************
		//**Login sa user argumentom, POST
		//*********************************
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Login (Users user1) {

			string poruka1;

			//proveri dal je Model binder ima grešaka
			if (ModelState.IsValid) {
				using (ČeličniProfiliEntities db = new ČeličniProfiliEntities()) {
					user1.Name.Trim();
					user1.pass.Trim();

					//nađe usera sa odgovarajućim name i pass-om
					var obj1 = db.Users.Where(x => (x.Name.Equals(user1.Name)) && (x.pass.Equals(user1.pass))).FirstOrDefault();

					//ako postoji user
					if (obj1 != null) {

						bool act1 = db.UserActivation.Any(p => p.UserId.Equals (obj1.id));
						//ako nema pending aktivacije, uloguj usera
						if (!act1) {
							//podaci za tekuću sesiju
							Session["UserId"] = obj1.id;//int
							Session["UserName"] = obj1.Name;
							Session["Level"] = obj1.level;//0: admin, 1: user

							return RedirectToAction("UserPanel");
						}
						else {
							poruka1 = "Nalog nije aktiviran";
							VisiblesInd = new int[] { 2, 4 };
							ViewBag.Action = "Login";
							ViewBag.poruka = poruka1;
							return View();
						}
					}
					else {
						poruka1 = "Ne valja User ili Password. Probaj ponovo ili SignUp";

						VisiblesInd = new int[] { 2, 4 };
						ViewBag.Action = "Login";
						ViewBag.poruka = poruka1;
						return View();
					}
				}
			}
			else {
				poruka1 = "Model bind error";
				VisiblesInd = new int[] { 2, 4 };
				ViewBag.Action = "Login";
				ViewBag.poruka = poruka1;
				return View();
			}
		}


		//**************
		//**SignUp GET
		//**************
		public ActionResult SignUp () {
			VisiblesInd = new int[] { 1, 4 };
			ViewBag.Poruka = String.Empty;
			ViewBag.Action = "SignUp";
			return View("Login");
		}


		//**********************************
		//**SignUp sa user argumentom, POST
		//**********************************
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult SignUp (Users user1) {

			string poruka1;

			if (this.ModelState.IsValid) {

				user1.Name.Trim();
				user1.pass.Trim();

				//da li ima praznina u nazivima
				if (!CheckSpaces(user1, out poruka1).Any(x => x.Equals(true))) {
					using (ČeličniProfiliEntities db = new ČeličniProfiliEntities()) {
						//da li taj user name već postoji
						bool NameCondition = db.Users.Any(x => (x.Name.Equals(user1.Name)));
						bool MailCondition = db.Users.Any(x => (x.email.Equals(user1.email)));

						//ako ga nema
						if (!(NameCondition || MailCondition)) {
							//dodaj usera
							db.Users.Add(user1);
							db.SaveChanges();

							//pošalji mail
							SendActivationEmail(user1);
							return RedirectToAction("SignUpstep2");
						}
						else {
							poruka1 = (NameCondition) ? "Već postoji user sa istim imenom" : "E-mail adresa je već registrovana";
							ViewBag.Action = "SignUp";
						}
					}
				}
			}
			else { poruka1 = "Model bind error"; }

			ViewBag.poruka = poruka1;
			VisiblesInd = new int[] { 1, 4 };
			ViewBag.Action = "SignUp";

			return View("Login");
		}


		public ActionResult SignUpstep2 () {
			VisiblesInd = new int[] { 2, 4 };
			return View();
		}


		//************************************
		//***Prikazuje form za reset passworda
		//************************************
		public ActionResult ResetPass () {
			VisiblesInd = new int[] { 1, 2, 4 };
			ViewBag.Action = "ResetPass";
			@ViewBag.poruka = "Upiši svoj User Name i biće ti poslat mail sa novom lozinkom.";
			return View("Login");
		}


		//********************************
		//***Šalje mail za reset passworda
		//********************************
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ResetPass (Users user1) {
			string poruka1;

			if (this.ModelState.IsValid) {

				user1.Name.Trim();

				if (!CheckSpaces(user1, out poruka1).Any(x => x.Equals(true))) {
					using (ČeličniProfiliEntities db = new ČeličniProfiliEntities()) {

						var dbUser = db.Users.Where(x => (x.Name.Equals(user1.Name))).FirstOrDefault();

						//da li postoji user
						if (dbUser != null) {

							bool ActivatedCondition = db.UserActivation.Any(x => x.UserId.Equals(dbUser.id));

							//ako je aktiviran
							if (!ActivatedCondition) {

								//generiši novi password
								string newpass = System.Web.Security.Membership.GeneratePassword(9, 3);
								dbUser.pass = newpass; db.SaveChanges();

								//pošalji mail sa novim passwordom
								int port = 587;
								string host = "smtp.gmail.com";
								string username = "draff557@gmail.com";
								string password = "merdita_01";
								string mailFrom = "noreply@mail.com";
								string mailTo = dbUser.email;
								string mailTitle = "Reset password";
								string mailMessage = "Pozdrav " + dbUser.Name + ",";
								mailMessage += "<br /><br />Tvoj novi password je: ";
								mailMessage += "<br />" + newpass + "<br />";
								mailMessage += "<a href='" + string.Format("{0}://{1}/Users/Login/", Request.Url.Scheme, Request.Url.Authority) + "'>Login</a>";

								var message1 = new MimeMessage();
								message1.From.Add(new MailboxAddress(mailFrom));
								message1.To.Add(new MailboxAddress(mailTo));
								message1.Subject = mailTitle;
								message1.Body = new TextPart("html") { Text = mailMessage };

								using (var client = new SmtpClient()) {
									client.Connect(host, port, SecureSocketOptions.StartTls);
									client.Authenticate(username, password);

									client.Send(message1);
									client.Disconnect(true);
								}

								VisiblesInd = new int[] { 1, 2, 4 };
								return View();
							}
							//ako nije aktiviran
							else
								poruka1 = "User nije aktiviran";
						}
						//ako nema usera
						else
							poruka1 = "Ne postoji taj user.";
					}
				}
			}
			else
				poruka1 = "Model bind error";

			ViewBag.poruka = poruka1;
			VisiblesInd = new int[] { 1, 2, 4 };
			ViewBag.Action = "ResetPass";

			return View("Login");
		}


		//***********************************
		//** šalje mail za aktivaciju naloga
		//***********************************
		private void SendActivationEmail (Users user1) {

			//novi unique identifier
			Guid activationCode1 = Guid.NewGuid();
			ČeličniProfiliEntities db = new ČeličniProfiliEntities();

			//ubaci podatke novog usera u UserActivation Tableu
			db.UserActivation.Add(new UserActivation {
				UserId = user1.id,
				ActivationCode = activationCode1
			});
			db.SaveChanges();

			int port = 587;
			string host = "smtp.gmail.com";
			string username = "draff557@gmail.com";
			string password = "merdita_01";
			string mailFrom = "noreply@mail.com";
			string mailTo = user1.email;
			string mailTitle = "Aktivacija Naloga";
			string mailMessage = "Pozdrav " + user1.Name + ",";
			mailMessage += "<br /><br />Klikni na donji link za aktivaciju naloga";
			mailMessage += "<br /><a href = '" + string.Format("{0}://{1}/Users/Activation/{2}", Request.Url.Scheme, Request.Url.Authority, activationCode1) + "'>Aktiviraj.</a>";

			var message1 = new MimeMessage();
			message1.From.Add(new MailboxAddress(mailFrom));
			message1.To.Add(new MailboxAddress(mailTo));
			message1.Subject = mailTitle;
			message1.Body = new TextPart("html") { Text = mailMessage };

			using (var client = new SmtpClient()) {
				client.Connect(host, port, SecureSocketOptions.StartTls);
				client.Authenticate(username, password);

				client.Send(message1);
				client.Disconnect(true);
			}
		}

		public ActionResult Activation () {

			string poruka1=String.Empty;
			string Guidobj1 = RouteData.Values["id"].ToString ();

			Guid activationCode1 = new Guid();

			if (Guid.TryParse(Guidobj1, out activationCode1) && Guidobj1 != null ) {

				ČeličniProfiliEntities db = new ČeličniProfiliEntities();

				UserActivation Activation1 = db.UserActivation.Where(p => p.ActivationCode.Equals (activationCode1)).FirstOrDefault();

				if (Activation1 != null) {
					db.UserActivation.Remove(Activation1);
					db.SaveChanges();

					Users user1= db.Users.Where(p => p.id == Activation1.UserId).FirstOrDefault();

					Session["UserId"] = user1.id;//int
					Session["UserName"] = user1.Name;
					Session["Level"] = user1.level;//0: admin, 1: user

					ViewBag.poruka = "Uspešna Aktivacija";
					ViewBag.panelFlag = true;
					VisiblesInd= new int[] { 3, 4, 5 };
					return View();
				}
			}
			VisiblesInd = new int[] { 1, 4};
			ViewBag.panelFlag = false;
			ViewBag.poruka = "Neuspešna Aktivacija";

			return View();
		}


		//******************
		//**Korisnički panel
		//******************
		public ActionResult UserPanel (string poruka2="") {

			//ako je user ulogovan
			if (Session["UserId"] != null) {
				int level = Convert.ToInt16(Session["Level"]);

				ViewBag.User = Session["UserName"];
				ViewBag.Status = (level == 0) ? "Admin" : "User";
				ViewBag.poruka = (poruka2 == "") ? null : poruka2;
				Session["Action"] = "UserPanel";

				VisiblesInd = new int[] { 3, 4 };

				return View();
			}
			else {
				return RedirectToAction("Login");
			}
		}


		//******************
		//**Nalog usera GET***
		//******************
		public ActionResult UserData (int? id) {

			if (id != null) {

				using (ČeličniProfiliEntities db = new ČeličniProfiliEntities()) {

					Users user1 = db.Users.Find(id);

					if (user1 != null) {

						VisiblesInd = new int[] { 3, 4 };
						ViewBag.Poruka = String.Empty;
						ViewBag.Action = Session["Action"].ToString();

						return View(user1);
					}
				}
			}
				return RedirectToAction("Login");
		}


		//******************
		//**Nalog usera POST**
		//******************
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult UserData ([Bind(Include = "id,Name,pass,level,email")] Users user1) {

			if (Session["UserId"] != null) {
				string poruka1;

				if (ModelState.IsValid) {

					user1.pass.Trim();
					user1.email.Trim();

					using (ČeličniProfiliEntities db = new ČeličniProfiliEntities()) {

						//da li taj email već postoji, a da nije isti user
						bool MailCondition = db.Users.Any(x => (x.email.Equals(user1.email) && !x.id.Equals(user1.id)));

						//Ako ga nema, uradi update
						if (!MailCondition) {

							db.Entry(user1).State = EntityState.Modified;
							db.SaveChanges();
							poruka1 = "Uspešna promena podataka";
							string akcija = Session["Action"].ToString();

							return RedirectToAction(akcija, "Users", new { poruka2= poruka1 });
						}
						else
							ViewBag.poruka  = "Mail adresa je zauzeta";
					}
				}
				else
					ViewBag.poruka = "Model bind error";

				return View();
			}
			else
				return RedirectToAction("Login");
		}


		//******************************************
		//**Proveri ispravnost Username i Password-a
		//******************************************
		bool[] CheckSpaces (Users user1, out string poruka1) {

			bool[] SpcCheck = new bool[3];


			SpcCheck[0] = user1.Name.Any(x => Char.IsWhiteSpace(x));
			SpcCheck[1] = user1.pass.Any(x => Char.IsWhiteSpace(x));
			SpcCheck[2] = (user1.pass.Length < 6);

			if (SpcCheck[0])
				poruka1 = "Ne sme biti razmaka u imenu user-a.";
			else if (SpcCheck[1])
				poruka1 = "Ne sme biti razmaka u password-u.";
			else if (SpcCheck[2])
				poruka1 = "Dužina passworda mora biti veća od 5 karaktera";
			else
				poruka1 = String.Empty;

			return SpcCheck;
		}

		//**********************************
		//**Izloguje se
		//**********************************
		public ActionResult SignOut () {

			Session["UserId"] = null;

			VisiblesInd = new int[] { 2, 4 };
			ViewBag.Poruka = String.Empty;
			ViewBag.Action = "Login";
			return View("Login");
		}

		//**************************************
		//**Lista korisnika (samo za admina) GET
		//**************************************
		public ActionResult UserList(string poruka2="") {

			bool userCond = (Session["UserId"] != null);
			bool levelCond = (Convert.ToInt16(Session["Level"]) == 0);

			//ako je ulogovan i ako je admin
			if (userCond && levelCond) {
				using (ČeličniProfiliEntities db = new ČeličniProfiliEntities()) {
					ViewBag.poruka = (poruka2 == "") ? null : poruka2;
					Session["Action"] = "UserList";
					int level = Convert.ToInt16(Session["Level"]);

					ViewBag.Status = (level == 0) ? "Admin" : "User";
					ViewBag.User = Session["UserName"];

					return View(db.Users.ToList());
				}
			}
			else {
				if (!userCond)
					return RedirectToAction("Login");
				else
					return RedirectToAction("UserPanel");
			}
		}

		//******************************
		//**Brisanje Naloga usera GET***
		//******************************
		public ActionResult DeleteUser (int? id) {

		if (id != null) {

				//ako hoće sebe da izbriše
				if (id != Convert.ToInt16( Session["UserId"])){

					using (ČeličniProfiliEntities db = new ČeličniProfiliEntities()) {

						Users user1 = db.Users.Find(id);

						if (user1 != null) {

							VisiblesInd = new int[] { 3, 4 };

							ViewBag.poruka= "Portvrdi brisanje usera: " + user1.Name;
							return View(user1);
						}
						else {
							return RedirectToAction("UserList", "Users", new { poruka2 = "Nepostojeći User" });
						}
					}
				}
				else {
					return RedirectToAction("UserList", "Users", new { poruka2 = "Ne možeš sebe izbrisati" });
				}
			}
			return RedirectToAction("Login");
		}


		//******************************
		//**Brisanje Naloga usera POST***
		//******************************
		[HttpPost, ActionName("DeleteUser")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteUserConfirmed (int? id) {

			if (id != null) {
				//ako hoće sebe da izbriše
				string poruka;

				if (id != Convert.ToInt16( Session["UserId"])){

					using (ČeličniProfiliEntities db = new ČeličniProfiliEntities()) {

						Users user1 = db.Users.Find(id);

						if (user1 != null) {

							VisiblesInd = new int[] { 3, 4 };
							ViewBag.Poruka = String.Empty;

							Users users = db.Users.Find(id);
							db.Users.Remove(users);
							db.SaveChanges();

							poruka= "Izbrisan User";

						}
						else
							poruka= "Nepostojeći User";
					}
				}
				else {
					poruka= "Ne možeš sebe izbrisati";
				}
				return RedirectToAction("UserList","Users", new {poruka2 = poruka});
			}
				return RedirectToAction("Login");
		}


		/*
		// GET: Users
		public ActionResult Index() {
			return View(db.Users.ToList());
		}

		// GET: Users/Details/5
		public ActionResult Details(int? id) {
			if (id == null)
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

			Users users = db.Users.Find(id);

			if (users == null)
				return HttpNotFound();

			return View(users);
		}

		// GET: Users/Create
		public ActionResult Create() {
			return View();
		}

		// POST: Users/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "id,Name,pass,level,email")] Users users) {

			if (ModelState.IsValid){
				db.Users.Add(users);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(users);
		}

		// GET: Users/Edit/5
		public ActionResult Edit(int? id) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Users users = db.Users.Find(id);
			if (users == null) {
				return HttpNotFound();
			}
			return View(users);
		}

		// POST: Users/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "id,Name,pass,level,email")] Users users) {
			if (ModelState.IsValid) {
				db.Entry(users).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(users);
		}

		// GET: Users/Delete/5
		public ActionResult Delete(int? id) {

			if (id == null)
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

			Users users = db.Users.Find(id);

			if (users == null)
				return HttpNotFound();

			return View(users);
		}

		// POST: Users/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id) {
			Users users = db.Users.Find(id);
			db.Users.Remove(users);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing) {
			if (disposing)
				db.Dispose();

			base.Dispose(disposing);
		}*/
	}
}
