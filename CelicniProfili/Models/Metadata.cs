using System;
using System.ComponentModel.DataAnnotations;

namespace CelicniProfili.Models {
	public class I_geometrijaMetadata {

		[Required]
		[Display(Name = "b [mm]")]
		public double b { get; set; }

		[Required]
		[Display(Name = "h [mm]")]
		public double h { get; set; }

		[Required]
		[Display(Name = "s [mm]")]
		public double s { get; set; }

		[Display(Name = "t [mm]")]
		[Required]
		public double t { get; set; }
	}

	public class I_karakteristikeMetadata {
		[Required]
		[Display(Name = "A [cm²]")]
		public double A { get; set; }

		[Required]
		[Display(Name = "G [kg/m]")]
		public double G { get; set; }

		[Required]
		[Display(Name = "Ix [cm^4]")]
		public double Ix { get; set; }

		[Required]
		[Display(Name = "Wx [cm³]")]
		public double Wx { get; set; }

		[Required]
		[Display(Name = "ix_jez [cm]")]
		public double ix_jez { get; set; }

		[Required]
		[Display(Name = "Iy [cm^4]")]
		public double Iy { get; set; }

		[Required]
		[Display(Name = "Wy [cm³]")]
		public double Wy { get; set; }

		[Required]
		[Display(Name = "iy_jez [cm]")]
		public double iy_jez { get; set; }

		[Required]
		[Display(Name = "Sx [cm³]")]
		public double Sx { get; set; }

		[Required]
		[Display(Name = "s_x [cm]")]
		public double s_x { get; set; }

		[Required]
		[Display(Name = "I_tor [cm^4]")]
		public double I_tor { get; set; }
	}


	public class MonoblokMetadata
	{
		[Required]
		public int ID_standard { get; set; }
		[Required]
		public int ID_tip { get; set; }
		[Required]
		public string Naziv { get; set; }
	}

	public class monoblok_pozicije_ojačanjaMetadata {
		[Required]
		public int ID_mono { get; set; }
		[Required]
		public double X_pos { get; set; }
		[Required]
		public double Y_pos { get; set; }
		[Required]
		public int br_segment { get; set; }
		[Required]
		public string pravac { get; set; }
		[Required]
		public int index_pr { get; set; }
	}

	public class ojačanje_opisMetadata {
		[Required]
		public int Id_ojač { get; set; }
		[Required]
		public double b { get; set; }
		[Required]
		public double h { get; set; }
		[Required]
		public double pol_x { get; set; }
		[Required]
		public double pol_y { get; set; }
	}

	public class profilMetadata {
		[Required]
		public int ID_monoblok { get; set; }
		[Required]
		public int ID_ojačanje { get; set; }
		[Required]
		public Nullable<double> pos_x { get; set; }
		[Required]
		public Nullable<double> pos_y { get; set; }
	}

	public class Profil_I_karakteristikeMetadata {
		[Required]
		public int ID { get; set; }
		[Required]
		public double A { get; set; }
		[Required]
		public double G { get; set; }
		[Required]
		public double Ix { get; set; }
		[Required]
		public double Wx { get; set; }
		[Required]
		public double ix_jez { get; set; }
		[Required]
		public double Iy { get; set; }
		[Required]
		public double Wy { get; set; }
		[Required]
		public double iy_jez { get; set; }
		[Required]
		public double Sx { get; set; }
		[Required]
		public double s_x { get; set; }
		[Required]
		public double I_tor { get; set; }
	}

	public class mon_opisMetadata {
		public int ID_mono { get; set; }
		public int No_node { get; set; }
		public double Node_x { get; set; }
		public double Node_y { get; set; }
		public int index_pr { get; set; }
	}

	public class tip_monoblokMetadata {
		[Required]
		public int id_tip { get; set; }

		[Required]
		public int tip { get; set; }

		[Required]
		public int id_tehn { get; set; }
	}

	public class tehnologija_monoblokMetadata {
		[Required]
		public int ID_tehn { get; set; }

		[Required]
		public int tehnologija { get; set; }
	}

	public class UsersMetadata {

		[Required (ErrorMessage = "Ime user-a je obavezno")]
		public string Name { get; set; }

		[DataType(DataType.Password)]
		[Required (ErrorMessage = "Nedostaje password")]
		[StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
		public string pass { get; set; }

		[Required]
		public short level { get; set; }

		[EmailAddress(ErrorMessage = "Pogrešan format e-mail adrese")]
		[Required (ErrorMessage = "Email je obavezan")]
		public string email { get; set; }
	}


	public class UserActivationMetadata {

		[Required]
		public int UserId { get; set; }

		public System.Guid ActivationCode { get; set; }
	}


	public class Admin_SMTP_parameteresMetaData {

		[Required(ErrorMessage = "Ime user-a je obavezno")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "Nedostaje password")]
		public string password { get; set; }

		[Required(ErrorMessage = "Mora Host name")]
		[RegularExpression(@"^(([a-zA-Z0-9]|[a-zA-Z0-9][a-zA-Z0-9\-]*[a-zA-Z0-9])\.)*([A-Za-z0-9]|[A-Za-z0-9][A-Za-z0-9\-]*[A-Za-z0-9])$", ErrorMessage = "Nije u dobrom formatu")]
		public string host { get; set; }
	}
}