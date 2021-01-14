CREATE VIEW SEMESTRE_INFO
AS
SELECT dbo.SEMESTRE.Id AS Id_semestre, Anio, Periodo, dbo.GRUPO.Id AS Id_grupo, Numero as Numero_grupo, Id_curso, 
	Nombre, Creditos, Carrera, Profesor, Estudiante
FROM dbo.SEMESTRE 
JOIN dbo.GRUPO ON Id_semestre = dbo.SEMESTRE.Id
JOIN dbo.CURSO ON Id_curso = dbo.CURSO.Codigo
JOIN dbo.PROFESOR_GRUPO ON dbo.PROFESOR_GRUPO.Id_grupo = dbo.GRUPO.Id 
JOIN dbo.ESTUDIANTE_GRUPO ON dbo.ESTUDIANTE_GRUPO.Id_grupo = dbo.GRUPO.Id;

select * from EVALUACION
select * from archivo_evaluacion
select * from evaluacion_integrantes
select * from evaluacion_grupo


CREATE VIEW INFO_EVALUACION
AS
SELECT dbo.EVALUACION.Id as Id_evaluacion,
	Espec.Nombre as Archivo_especificacion, Espec.Fecha_creacion as Fecha_creacion_espec, Espec.Id as Id_Espec,
	Entre.Nombre as Archivo_entregable, Entre.Fecha_creacion as Fecha_creacion_entre, --Entre.Id as Id_Entre,
	cast(Entre.Id as int) as [Id_Entre],
	--EG.Id as Id_grupo, 
	cast(EG.Id as int) as [Id_grupo],
	EG.Nota as NotaFinal, EG.Observaciones,
	Retro.Nombre as Retroalimentacion, --Retro.Id as Id_retro, 
	cast(Retro.Id as int) as [Id_retro]
FROM dbo.EVALUACION
LEFT JOIN dbo.ARCHIVO_EVALUACION AS Espec ON Id_especificacion = Espec.Id
LEFT JOIN dbo.EVALUACION_GRUPO AS EG ON Id_evaluacion = EG.Id
LEFT JOIN dbo.ARCHIVO_EVALUACION AS Entre ON Id_entregable = Entre.Id
LEFT JOIN dbo.ARCHIVO_EVALUACION AS Retro ON Id_detalle = Retro.Id


SELECT * FROM INFO_EVALUACION where id_evaluacion = 1
select * from EVALUACION_GRUPO
insert into EVALUACION_GRUPO (Id_evaluacion, Observaciones, Id_entregable, Id_detalle, Nota)
values
(1, 'Observaciones', null, null, null);

select * from ARCHIVO_EVALUACION

