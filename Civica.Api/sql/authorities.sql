	 INSERT INTO "Authorities" ("Id", "Name", "Email", "County", "City", "District", "IsActive", "CreatedAt")
	 VALUES

-- PMB
	(gen_random_uuid(), 'Primăria Municipiului București', 'relatiipublice@pmb.ro', 'București', 'București', null, true, NOW()),
	(gen_random_uuid(), 'Poliția Locală a Municipiului București', 'office@plmb.ro', 'București', 'București', null, true, NOW()),
	(gen_random_uuid(), 'ASPA București', 'relatiicupublicul@aspa.ro', 'București', 'București', null, true, NOW()),
	(gen_random_uuid(), 'Protecția Animalelor CJ Ilfov', 'protectiaanimalelor@cjilfov.ro', 'București', 'București', null, true, NOW()),
	(gen_random_uuid(), 'Administrația Străzilor București', 'office@aspmb.ro', 'București', 'București', null, true, NOW()),

-- Autoritati Sector 1	 
     (gen_random_uuid(), 'Primăria Sector 1', 'registratura@primarias1.ro', 'București', 'București', 'Sector 1', true, NOW()),
     (gen_random_uuid(), 'DGASPC Sector 1', 'registratura@dgaspc-sectorul1.ro', 'București', 'București', 'Sector 1', true, NOW()),
	 (gen_random_uuid(), 'Poliția Locală S1', 'contact@politialocalasector1.ro', 'București', 'București', 'Sector 1', true, NOW()),
	 (gen_random_uuid(), 'Poliția Locală S1 (Protecția Animalelor)', 'politiaanimalelor@politialocalasector1.ro', 'București', 'București', 'Sector 1', true, NOW()),
	 (gen_random_uuid(), 'Protecția Animalelor S1', 'protectiaanimalelor@primarias1.ro', 'București', 'București', 'Sector 1', true, NOW()),
	 (gen_random_uuid(), 'Direcția de Utilități Publice, Salubrizare și Protecția Mediului S1', 'secretariat@mediusectorul1.ro', 'București', 'București', 'Sector 1', true, NOW()),

-- Autoritati Sector 2
	 (gen_random_uuid(), 'Primăria Sector 2', 'infopublice@ps2.ro', 'București', 'București', 'Sector 2', true, NOW()),
	 (gen_random_uuid(), 'Poliția Locală S2', 'directorgeneral.dgpl@politialocalas2.ro', 'București', 'București', 'Sector 2', true, NOW()),
	 (gen_random_uuid(), 'Asistenţă Socială si Protecţia Copilului S2', 'social@social2.ro', 'București', 'București', 'Sector 2', true, NOW()),
	 (gen_random_uuid(), 'Primar Sector 2', 'primar@ps2.ro', 'București', 'București', 'Sector 2', true, NOW()),

-- Autoritati Sector 3
	 (gen_random_uuid(), 'Primăria Sector 3', 'relatiipublice@primarie3.ro', 'București', 'București', 'Sector 3', true, NOW()),
	 (gen_random_uuid(), 'Poliția Locală S3', 'secretariat.dgpl@primarie3.ro', 'București', 'București', 'Sector 3', true, NOW()),
	 (gen_random_uuid(), 'Salubritate S3', 'comunicare@salubritate3.ro', 'București', 'București', 'Sector 3', true, NOW()),
	 (gen_random_uuid(), 'Directia Parcari', 'directiaparcari@primarie3.ro', 'București', 'București', 'Sector 3', true, NOW()),
	 (gen_random_uuid(), 'Ordine Publică și Control', 'ordinepublica.control@primarie3.ro', 'București', 'București', 'Sector 3', true, NOW()),
	 (gen_random_uuid(), 'Serviciul Circulație pe Dumurile Publice', 'circulatie@primarie3.ro', 'București', 'București', 'Sector 3', true, NOW()),
	 (gen_random_uuid(), 'Serviciul Disciplina în Construcții', 'disciplinainconstructii@primarie3.ro', 'București', 'București', 'Sector 3', true, NOW()),
	 (gen_random_uuid(), 'Serviciul Control Protecția Mediului', 'mediu@primarie3.ro', 'București', 'București', 'Sector 3', true, NOW()),

-- Autoritati Sector 4
	(gen_random_uuid(), 'Primăria Sector 4', 'contact@ps4.ro', 'București', 'București', 'Sector 4', true, NOW()),
	(gen_random_uuid(), 'Poliția Locală S4', 'sesizari@politialocala4.ro', 'București', 'București', 'Sector 4', true, NOW()),
	(gen_random_uuid(), 'DGASPC Sector 4', 'contact@dgaspc4.ro', 'București', 'București', 'Sector 4', true, NOW()),

-- Autoritati Sector 5
	 (gen_random_uuid(), 'Primăria Sector 5', 'primarie@sector5.ro', 'București', 'București', 'Sector 5', true, NOW()),
     (gen_random_uuid(), 'Serviciul de Ordine Publică și Intervenție Rapidă', 'ordinepublica@sector5.ro', 'București', 'București', 'Sector 5', true, NOW()),
     (gen_random_uuid(), 'Serviciul Politia Animalelor', 'politiaanimalelor@sector5.ro', 'București', 'București', 'Sector 5', true, NOW()),
	 (gen_random_uuid(), 'Serviciul Monitorizare Și Control Salubrizare', 'salubrizare@sector5.ro', 'București', 'București', 'Sector 5', true, NOW()),
	 (gen_random_uuid(), 'Serviciul Spații Verzi', 'spatiiverzi@sector5.ro', 'București', 'București', 'Sector 5', true, NOW()),
	 (gen_random_uuid(), 'Administrația Dezvoltare Urbană', 'dezvoltareurbana@sector5.ro', 'București', 'București', 'Sector 5', true, NOW()),
	 (gen_random_uuid(), 'Poliția Locală S5', 'politialocala@sector5.ro', 'București', 'București', 'Sector 5', true, NOW()),

-- Autoritati Sector 6
	(gen_random_uuid(), 'Primăria Sector 6', 'prim6@primarie6.ro', 'București', 'București', 'Sector 6', true, NOW()),
	(gen_random_uuid(), 'Poliţia Locală S6', 'contact@politia6.ro', 'București', 'București', 'Sector 6', true, NOW()),
	(gen_random_uuid(), 'DGASPC Sector 6', 'office@dgaspc6.com', 'București', 'București', 'Sector 6', true, NOW()),
	(gen_random_uuid(), 'ADP Sector 6', 'contact@adps6.ro', 'București', 'București', 'Sector 6', true, NOW());