# CelicniProfili
Projektni zadatak je formiranje baze čeličnih profila korišćenih u građevinskoj industriji, pregled geometrijskih i mehaničkih karakteristika preseka. Pored toga, program treba da omogući logovanje na postojeće korisničke naloge, formiranje novih naloga, kao i administraciju naloga uz ovlaštenja admina.

Scenario upotrebe je sledeći: korisnik se loguje na svoj nalog, nakon čega je upućen na korisnički panel gde može da odabere da menja svoje podatke (šifru, email) ili da pristupi bazi čeličnih profila. Ako ima nivo administratora, može da pristupi i listi svih korisnika, gde može da menja njihove podatke ili da ih briše. Novi korisnici mogu da otvore nov nalog, uz aktivaciju naloga preko e-mail poruke koju će sistem poslati.

Baza podataka: Backup baze se nalazi u CelicniProfili/AppData/Baza folderu. Tu je takođe i sql script formiranje.sql preko koga je formirana baza (nije predviđen da se startuje u "1-nom cugu"). Baza se sastoji iz sledećih tabela:

	- Users : Tabela korsnika sa njihovim podacima (user name, password, e-mail, level (User ili admin))	
	- UserActivation : tabela koja čuva privremene podatke korisnika koji su otvorili nalog ali ga nisu aktivirali. Sadrži ID korisnika i generisani GUID kod.
	- profil : profili definisani preko kombinacije nosećeg monobloka i ojačanja, sadri podatke o geometrijksim koordinatama eventualnog novog naknadnog ojačanja.
	- tip profila : za zadati monoblok definiše tehnologiju valjanja i oznaku grupe profila
	- standard : za zadati monoblok definiše naziv standarda i šifru koju ima po tom standardu
	- I_geometrija : geometrijske karakteristike monobloka
	- I-karakteristike : mehaničke karakteristike monobloka
	- monoblok_pozicije_ojačanja : definiše geometrijske tačke na monobloku gde ima smisla ubaciti ojačavajuće limove
	- [ojačanje_opis] : dimenzije ojačavajućeg lima
	- [Profil_I-karakteristike] : mehaničke karakteristike profila

Korišćene tehnlogije i bibioteke:
	
	- .NET ASP MVC
	- SQL
	- Entity framework
	- JS
	- SVG