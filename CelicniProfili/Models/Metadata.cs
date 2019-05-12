using System;
using System.ComponentModel.DataAnnotations;

namespace CelicniProfili.Models {
	public class I_geometrijaMetadata {

		[Required]
		public double b { get; set; }
		[Required]
		public double h { get; set; }
		[Required]
		public double s { get; set; }
		[Required]
		public double t { get; set; }
	}

	public class I_karakteristikeMetadata {
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

	public class mon_opisMetadata
	{
		[Required]
		public int ID_mono { get; set; }
		[Required]
		public int No_node { get; set; }
		[Required]
		public double Node_x { get; set; }
		[Required]
		public double Node_y { get; set; }
		[Required]
		public int index_pr { get; set; }
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

	public class tip_profilaMetadata {
		[Required]
		public string tehnologija { get; set; }
		[Required]
		public string oznaka { get; set; }
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