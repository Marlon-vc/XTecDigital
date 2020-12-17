SELECT * FROM dbo.SEMESTRE;

INSERT INTO dbo.CURSO (Codigo, Nombre, Creditos, Carrera, Habilitado) VALUES
    ('CE1101', 'Introducción a la programación', 3, 'Computadores', 1),
    ('CE1102', 'Taller de programación', 3, 'Computadores', 1),
    ('MA1102', 'Cálculo diferencial e integral', 4, 'Matemática', 0),
    ('QU1102', 'Laboratorio de química básica I', 2, 'Química', 1),
    ('QU1106', 'Química básica I', 3, 'Química', 0),
    ('CE1103', 'Algoritmos y estructuras de datos I', 3, 'Computadores', 1),
    ('FI1101', 'Física general I', 3, 'Física', 1),
    ('FI1202', 'Laboratorio de física general I', 2, 'Física', 1);

SELECT * FROM dbo.CURSO;

SELECT * FROM dbo.SEMESTRE
SELECT * FROM dbo.GRUPO
SELECT * FROM dbo.RUBRO
SELECT * FROM dbo.CARPETA
JOIN dbo.GRUPO ON dbo.SEMESTRE.Id = dbo.GRUPO.Id_semestre
JOIN dbo.RUBRO ON dbo.GRUPO.Id = dbo.RUBRO.Id_grupo
JOIN dbo.CARPETA ON dbo.GRUPO.Id = dbo.CARPETA.Id_grupo

INSERT INTO [dbo].[#tblImport]
SELECT Carnet, Nombre, Apellido1, Apellido2, IdCurso, NombreCurso, Ano, Semestre, Grupo, IdProfesor, NombreProfesor, ApellidoProfesor, ApellidoProfesor2
FROM OPENROWSET('
	Microsoft.ACE.OLEDB.12.0','Excel12.0;HDR=YES;Database=C:\Users\pvill\Desktop\Proyecto_II_-_XTECDigital_Carga_Semestre_Final.xlsx', 'SELECT * FROM [Data$]
')