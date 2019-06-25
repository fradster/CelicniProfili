ALTER TABLE profil ADD FOREIGN KEY (ID_monoblok) REFERENCES Monoblok (ID)

ALTER TABLE [dbo].[tip monoblok] ADD FOREIGN KEY (id_tehn) REFERENCES  [tehnologija monoblok] (id_tehn)

ALTER TABLE Monoblok ADD FOREIGN KEY (Id_standard) REFERENCES standard (Id_standard)

ALTER TABLE Monoblok ADD FOREIGN KEY (ID) REFERENCES [I-geometrija] (ID)

ALTER TABLE Monoblok ADD FOREIGN KEY (ID) REFERENCES [I-karakteristike] (ID)

ALTER TABLE profil ADD Foreign KEY (ID) REFERENCES [Profil_I-karakteristike] (ID);