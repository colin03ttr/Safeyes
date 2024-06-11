DROP DATABASE IF EXISTS safeyes;
CREATE DATABASE safeyes;
USE safeyes;

CREATE TABLE clients (mail varchar(50) NOT NULL, nom_c varchar(20), prenom_c varchar(20), telephone varchar(10), mdp varchar(20), adresse varchar(100), carte_credit varchar(20));
CREATE TABLE vendeur (vendeur_id varchar(10) NOT NULL, nom_v varchar(20), prenom_v varchar(20), mdp_v varchar(20));
CREATE TABLE proprietaire (proprietaire_id varchar(10) NOT NULL, mdp_p varchar(10));




ALTER TABLE clients ADD CONSTRAINT PK_clients PRIMARY KEY (mail);
ALTER TABLE vendeur ADD CONSTRAINT PK_vendeur PRIMARY KEY (vendeur_id);
ALTER TABLE proprietaire ADD CONSTRAINT PK_proprietaire PRIMARY KEY (proprietaire_id);




USE `Safeyes`;
DROP procedure IF EXISTS `RecupererClient`;
DELIMITER $$
USE `Safeyes`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `RecupererClient`(IN email varchar(50))
BEGIN
SELECT  * FROM clients WHERE mail = email;
END$$
DELIMITER ;
;

DROP procedure IF EXISTS `RecupererVendeur`;
DELIMITER $$
USE `Safeyes`$$
CREATE PROCEDURE `RecupererVendeur` (in vendeurID varchar(10))
BEGIN
SELECT * FROM vendeur WHERE vendeur_id=vendeurID;
END$$
DELIMITER ;
;

DROP procedure IF EXISTS `RecupererProprietaire`;

DELIMITER $$
USE `Safeyes`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `RecupererProprietaire`(IN  proprietaireID varchar(10))
BEGIN
SELECT  * FROM proprietaire WHERE proprietaire_id = proprietaireID;
END$$
DELIMITER ;





# PEUPLEMENT




INSERT INTO Safeyes.clients VALUES ('martin.matin@gmail.com','Matin','Martin','0634879302','martinmatin', '2 rue de la vie 78000','0000 0010 0000 0000');
INSERT INTO Safeyes.clients VALUES ('tom.sawyer@gmail.com','Sawyer','Tom','0694809312','tomsawyer', '3 rue du monde 92000','0007 0000 0000 0000');
INSERT INTO Safeyes.clients VALUES ('mylene.farmer@gmail.com','Farmer','Mylene','0794279382','mylenefarmer', '6 avenue de la reine 78400','0000 0000 0000 0400');
INSERT INTO Safeyes.clients VALUES ('john.cena@gmail.com','Cena','John','0688679302','johncena', '75 rue de la chaussette 78450','0022 0000 0000 0000');
INSERT INTO Safeyes.clients VALUES ('marc.lavoine@gmail.com','Lavoine','Marc','0739349302','marclavoine', '8 rue du blé 92220','0120 0000 0900 0230');
INSERT INTO Safeyes.clients VALUES ('michael.youn@gmail.com','Youn','Michael','0634593012','michaelyoun', '7 rue du jeune 78570','0230 0900 4200 0000');
INSERT INTO Safeyes.clients VALUES ('iris.mittenaere@gmail.com','Mittenaere','Iris','0634878902','irismittenaere', '12 boulevard du dindon 92200','1230 0000 0000 0000');
INSERT INTO Safeyes.clients VALUES ('justin.bieber@gmail.com','Bieber','Justin','0677453202','justinbieber', '24 quartier Cartier 75016','0000 1234 0000 0000');
INSERT INTO Safeyes.clients VALUES ('quentin.tarantino@gmail.com','Tarantino','Quentin','0704803302','quentintarantino', '92 rue du batiment 75020','7682 0000 0000 0000');
INSERT INTO Safeyes.clients VALUES ('yannick.noah@gmail.com','Noah','Yannick','0732342102','yannicknoah', '123 rue du soleil 75014','0000 0000 0000 4820');

INSERT INTO Safeyes.proprietaire VALUES ('Proprio','pierrebf');

INSERT INTO Safeyes.vendeur VALUES ('null', 'null', 'null', 'null'); #vendeur assigné par défauts aux commandes
INSERT INTO Safeyes.vendeur VALUES ('H.P','Potter','Harry','harrypotter');
INSERT INTO Safeyes.vendeur VALUES ('B.D', 'Dylan', 'Bob','bobdylan');
INSERT INTO Safeyes.vendeur VALUES ('A.B', 'Bent', 'Amel','amelbent');
INSERT INTO Safeyes.vendeur VALUES ('J.G', 'Garcia', 'José','josegarcia');



