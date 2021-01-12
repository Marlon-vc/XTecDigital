CREATE VIEW SEMESTRE_INFO
AS
SELECT dbo.SEMESTRE.Id AS Id_semestre, Anio, Periodo, dbo.GRUPO.Id AS Id_grupo, Numero as Numero_grupo, Id_curso, 
	Nombre, Creditos, Carrera, Profesor, Estudiante
FROM dbo.SEMESTRE 
JOIN dbo.GRUPO ON Id_semestre = dbo.SEMESTRE.Id
JOIN dbo.CURSO ON Id_curso = dbo.CURSO.Codigo
JOIN dbo.PROFESOR_GRUPO ON dbo.PROFESOR_GRUPO.Id_grupo = dbo.GRUPO.Id 
JOIN dbo.ESTUDIANTE_GRUPO ON dbo.ESTUDIANTE_GRUPO.Id_grupo = dbo.GRUPO.Id;

select * from curso

SELECT dbo.NOTICIA.Id as Id_noticia, Titulo, Mensaje, Autor, Fecha_publicacion, Numero as Numero_grupo, Nombre as Nombre_curso, dbo.GRUPO.Id as Id_grupo
FROM dbo.NOTICIA
JOIN dbo.GRUPO ON dbo.GRUPO.Id = dbo.NOTICIA.Id_grupo
JOIN dbo.CURSO ON dbo.CURSO.Codigo = Id_curso