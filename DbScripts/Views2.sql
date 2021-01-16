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
