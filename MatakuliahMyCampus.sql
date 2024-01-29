CREATE TABLE matakuliah(
Kode_MK VARCHAR(6) NOT NULL PRIMARY KEY,
Nama_MK VARCHAR(50) NOT NULL,
Sks INT NOT NULL
);

INSERT INTO matakuliah(Kode_MK, Nama_MK, Sks)
VALUES
('S01', 'Matematika', '4'),
('S02', 'Sosiologi', '3'),
('S03', 'Biopsikologi', '3'),
('S04', 'Akuntansi Keuangan', '3'),
('S05', 'Sastra Inggris', '2'),
('S06', 'Sejarah', '4'),
('S07', 'Akuntansi Pengantar', '3'),
('S08', 'Manajemen Keuangan', '2'),
('S09', 'Algoritma dan Pemrograman', '4'),
('S010', 'Komputer Grafis', '2');

 
EXEC selectMatakuliah;



