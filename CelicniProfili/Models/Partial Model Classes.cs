using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CelicniProfili.Models {
		
	[MetadataType(typeof(I_geometrijaMetadata))]
	public partial class I_geometrija { }

	[MetadataType(typeof(I_karakteristikeMetadata))]
	public partial class I_karakteristike { }

	[MetadataType(typeof(mon_opisMetadata))]
	public partial class mon_opis { }

	[MetadataType(typeof(MonoblokMetadata))]
	public partial class Monoblok{ }

	[MetadataType(typeof(monoblok_pozicije_ojačanjaMetadata))]
	public partial class monoblok_pozicije_ojačanja { }

	[MetadataType(typeof(ojačanje_opisMetadata))]
	public partial class ojačanje_opis{ }

	[MetadataType(typeof(profilMetadata))]
	public partial class profil { }

	[MetadataType(typeof(Profil_I_karakteristikeMetadata))]
	public partial class Profil_I_karakteristike{ }

	[MetadataType(typeof(tip_profilaMetadata))]
	public partial class tip_profila { }

	[MetadataType(typeof(UsersMetadata))]
	public partial class Users { }

	[MetadataType(typeof(UserActivationMetadata))]
	public partial class UserActivation { }
}
