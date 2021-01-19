GO
CREATE VIEW INFO_EVALUACION
AS
SELECT 
	-- Info evaluación
	evaluacion.Nombre,
	evaluacion.Notas_publicadas,
	evaluacion.Fecha_entrega,
	evaluacion.Peso_nota,
	evaluacion.Grupal,
	evaluacion.Rubro,
	evaluacion.Numero,
	evaluacion.Curso,
	evaluacion.Anio,
	evaluacion.Periodo,
	-- Archivo especificación
	especificacion.Nombre as Especificacion,
	especificacion.Carpeta as Carpeta_especificacion,
	especificacion.Tipo_carpeta as Tipo_carpeta_especificacion,
	-- Archivo entregable
	cast(entregable.Nombre as varchar(100)) as Entregable,
	cast(entregable.Carpeta as varchar(100)) as Carpeta_entregable,
	cast(entregable.Tipo_carpeta as varchar(20)) as Tipo_carpeta_entregable,
	cast(entregable.Fecha_creacion as datetime) as Fecha_entregable,
	-- Archivo Detalle
	cast(detalle.Nombre as varchar(100)) as Detalle,
	cast(detalle.Carpeta as varchar(100)) as Carpeta_detalle,
	cast(detalle.Tipo_carpeta as varchar(20)) as Tipo_carpeta_detalle,
	-- Evaluación grupo
	grupo.Id as Id_evaluacion_grupo,
	cast(grupo.Nota as decimal(5,2)) as Nota,
	cast(grupo.Observaciones as text) as Observaciones
FROM dbo.EVALUACION as evaluacion
LEFT JOIN dbo.EVALUACION_GRUPO as grupo ON
	grupo.Evaluacion = evaluacion.Nombre AND
	grupo.Rubro = evaluacion.Rubro AND
	grupo.Numero = evaluacion.Numero AND
	grupo.Curso = evaluacion.Curso AND
	grupo.Anio = evaluacion.Anio AND
	grupo.Periodo = evaluacion.Periodo
LEFT JOIN dbo.ARCHIVO as especificacion ON
	especificacion.Nombre = evaluacion.Especificacion AND
	especificacion.Carpeta = evaluacion.Carpeta_especificacion AND
	especificacion.Tipo_carpeta = evaluacion.Tipo_carpeta_especificacion AND
	especificacion.Numero = evaluacion.Numero AND 
	especificacion.Curso = evaluacion.Curso AND 
	especificacion.Anio = evaluacion.Anio AND 
	especificacion.Periodo = evaluacion.Periodo
LEFT JOIN dbo.ARCHIVO as entregable ON
	entregable.Nombre = grupo.Entregable AND
	entregable.Carpeta = grupo.Carpeta_entregable AND
	entregable.Tipo_carpeta = grupo.Tipo_carpeta_entregable AND
	entregable.Numero = evaluacion.Numero AND
	entregable.Curso = evaluacion.Curso AND
	entregable.Anio = evaluacion.Anio AND
	entregable.Periodo = evaluacion.Periodo
LEFT JOIN dbo.ARCHIVO as detalle ON
	detalle.Nombre = grupo.Detalle AND
	detalle.Carpeta = grupo.Carpeta_detalle AND
	detalle.Tipo_carpeta = grupo.Tipo_carpeta_detalle AND
	detalle.Numero = evaluacion.Numero AND
	detalle.Curso = evaluacion.Curso AND
	detalle.Anio = evaluacion.Anio AND
	detalle.Periodo = evaluacion.Periodo;

GO
CREATE VIEW CURSO_GRUPO
AS
SELECT
	curso.Codigo,
	curso.Nombre as Nombre_curso,
	curso.Creditos,
	curso.Carrera,
	grupo.Numero as Numero_grupo,
	grupo.Anio as Anio_semestre,
	grupo.Periodo as Periodo_semestre
FROM dbo.GRUPO as grupo
LEFT JOIN dbo.CURSO as curso ON
	curso.Codigo = grupo.Curso;

--SELECT * FROM dbo.CURSO_GRUPO;

GO
CREATE VIEW INFO_EVALUAR_ENTREGABLES
AS
SELECT
	Estudiante,
	Entregable, 
	Carpeta_entregable, 
	Tipo_carpeta_entregable, 
	Detalle, 
	Carpeta_detalle, 
	Tipo_carpeta_detalle, 
	Observaciones, 
	Nota,
	IE.Nombre as Nombre_evaluacion,
	IE.Rubro,
	IE.Numero,
	IE.Curso,
	IE.Anio,
	IE.Periodo,
	IE.Id_evaluacion_grupo,
	PG.Profesor
from INFO_EVALUACION as IE
LEFT JOIN dbo.EVALUACION_INTEGRANTES as EI ON EI.Id_grupo = Id_evaluacion_grupo
JOIN dbo.RUBRO as R ON R.Nombre = IE.Rubro AND  R.Numero = IE.Numero AND R.Curso = IE.Curso AND R.Anio = IE.Anio AND R.Periodo = IE.Periodo
JOIN dbo.GRUPO as G ON G.Numero = IE.Numero AND G.Curso = IE.Curso AND G.Anio = IE.Anio AND G.Periodo = IE.Periodo
JOIN dbo.PROFESOR_GRUPO as PG ON PG.Numero = IE.Numero AND PG.Curso = IE.Curso AND PG.Anio = IE.Anio AND PG.Periodo = IE.Periodo