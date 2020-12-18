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

select * from dbo.RUBRO
WHERE Id_grupo = 1

select * from dbo.NOTICIA
select * from dbo.GRUPO


SELECT dbo.SEMESTRE.Id AS Id_semestre, Anio, Periodo, dbo.GRUPO.Id AS Id_grupo, Numero as Numero_grupo, Id_curso, 
	Nombre, Creditos, Carrera, Profesor, Estudiante
FROM dbo.SEMESTRE 
JOIN dbo.GRUPO ON Id_semestre = dbo.SEMESTRE.Id
JOIN dbo.CURSO ON Id_curso = dbo.CURSO.Codigo
JOIN dbo.PROFESOR_GRUPO ON dbo.PROFESOR_GRUPO.Id_grupo = dbo.GRUPO.Id 
JOIN dbo.ESTUDIANTE_GRUPO ON dbo.ESTUDIANTE_GRUPO.Id_grupo = dbo.GRUPO.Id;