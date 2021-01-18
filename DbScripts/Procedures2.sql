-- Obtener todos los cursos
GO
CREATE PROCEDURE dbo.sp_get_courses
AS
SELECT Codigo, Nombre, Creditos, Carrera, Habilitado
FROM dbo.CURSO;

-- Obtener los cursos activos
GO
CREATE PROCEDURE dbo.sp_get_active_courses 
AS
SELECT Codigo, Nombre, Creditos, Carrera, Habilitado
FROM dbo.CURSO
WHERE Habilitado = 1;

-- Obtener un curso por código
GO
CREATE PROCEDURE dbo.sp_get_course
    @Code VARCHAR(10)
AS
SELECT Codigo, Nombre, Creditos, Carrera, Habilitado
FROM dbo.CURSO
WHERE Codigo = @Code;

-- Crear un curso
GO
CREATE PROCEDURE dbo.sp_create_course
    @Codigo VARCHAR(10),
    @Nombre VARCHAR(100),
    @Creditos INT,
    @Carrera VARCHAR(100),
    @Habilitado BIT
AS
INSERT INTO dbo.CURSO (Codigo, Nombre, Creditos, Carrera, Habilitado)
VALUES (@Codigo, @Nombre, @Creditos, @Carrera, @Habilitado);

-- Eliminar un curso
GO
CREATE PROCEDURE dbo.sp_delete_course
    @Code VARCHAR(10)
AS
DELETE FROM dbo.CURSO
WHERE Codigo = @Code;

-- Actualizar un curso
GO
CREATE PROCEDURE dbo.sp_update_course
	@Codigo VARCHAR(10),
	@Nombre VARCHAR(100),
	@Carrera VARCHAR(100),
	@Creditos INT,
	@Habilitado BIT
AS
UPDATE dbo.CURSO
SET Nombre = @Nombre, Creditos = @Creditos, Carrera = @Carrera, Habilitado = @Habilitado
WHERE Codigo = @Codigo;

-- ### INICIALIZACIÓN SEMESTRE ### --

-- Crear semestre
GO
CREATE PROCEDURE dbo.sp_create_semester
	@Anio INT,
	@Periodo CHAR(1)
AS
INSERT INTO dbo.SEMESTRE (Anio, Periodo)
VALUES (@Anio, @Periodo);

GO
CREATE PROCEDURE dbo.sp_get_semestre
    @Anio INT,
    @Periodo CHAR(1)
AS
SELECT Anio, Periodo
FROM dbo.SEMESTRE
WHERE Anio = @Anio AND Periodo = @Periodo;

-- Crear tabla temporal de semestre
GO
CREATE PROCEDURE dbo.sp_create_temporal_table
AS
CREATE TABLE [#EXCEL_TABLE] (
	[Carnet] varchar(50),
	[IdCurso] varchar(10),
	[NombreCurso] varchar(100),
	[Anio] int,
	[Periodo] char(1),
	[Grupo] int,
	[IdProfesor] varchar(50)
);

GO
CREATE PROCEDURE dbo.sp_delete_temporal_table
AS
IF OBJECT_ID('tempdb.dbo.#EXCEL_TABLE', 'U') IS NOT NULL
  DROP TABLE #EXCEL_TABLE; 

GO
CREATE PROCEDURE dbo.sp_insert_temporal_table
	@Carnet varchar(50),
	@IdCurso varchar(10),
	@NombreCurso varchar(100),
	@Anio int,
	@Periodo char(1),
	@Grupo int,
	@IdProfesor varchar(50)
AS
INSERT INTO #EXCEL_TABLE (Carnet, IdCurso, NombreCurso, Anio, Periodo, Grupo, IdProfesor)
VALUES (@Carnet, @IdCurso, @NombreCurso, @Anio, @Periodo, @Grupo, @IdProfesor);

--insertar desde tabla temporal
GO
CREATE PROCEDURE dbo.sp_initialize_semester
AS
INSERT INTO dbo.SEMESTRE (Anio, Periodo)
SELECT DISTINCT Anio, Periodo FROM #EXCEL_TABLE;

CREATE PROCEDURE dbo.sp_semester_excel
	@Carnet varchar(50),
	@IdCurso varchar(10),
	@NombreCurso varchar(100),
	@Anio int,
	@Periodo char(1),
	@Grupo int,
	@IdProfesor varchar(50)
AS
BEGIN
CREATE TABLE [#EXCEL_TABLE] (
	[Carnet] varchar(50),
	[IdCurso] varchar(10),
	[NombreCurso] varchar(100),
	[Anio] int,
	[Periodo] char(1),
	[Grupo] int,
	[IdProfesor] varchar(50)
)

INSERT INTO #EXCEL_TABLE (Carnet, IdCurso, NombreCurso, Anio, Periodo, Grupo, IdProfesor)
VALUES (@Carnet, @IdCurso, @NombreCurso, @Anio, @Periodo, @Grupo, @IdProfesor);



END;



-- Crear grupo
GO
CREATE PROCEDURE dbo.sp_create_grupo
	@Numero INT,
	@Curso VARCHAR(10),
	@Anio INT,
	@Periodo CHAR(1)
AS
INSERT INTO dbo.GRUPO (Numero, Curso, Anio, Periodo)
VALUES (@Numero, @Curso, @Anio, @Periodo);

-- Crear grupo_estudiante
GO
CREATE PROCEDURE dbo.sp_create_grupo_estudiante
	@Numero INT,
    @Curso VARCHAR(10),
    @Anio INT,
    @Periodo CHAR(1),
	@carnet VARCHAR(50)
AS
INSERT INTO dbo.ESTUDIANTE_GRUPO (Estudiante, Numero, Curso, Anio, Periodo) 
VALUES (@carnet, @Numero, @Curso, @Anio, @Periodo);

-- Crear grupo_profesor
GO
CREATE PROCEDURE dbo.sp_create_grupo_profesor
	@Numero INT,
    @Curso VARCHAR(10),
    @Anio INT,
    @Periodo CHAR(1),
	@cedula VARCHAR(50)
AS
INSERT INTO dbo.PROFESOR_GRUPO (Profesor, Numero, Curso, Anio, Periodo)
VALUES (@cedula, @Numero, @Curso, @Anio, @Periodo);

-- Crear rubros iniciarles de grupo
GO
CREATE PROCEDURE dbo.sp_create_initial_rubro
	@Numero INT,
    @Curso VARCHAR(10),
    @Anio INT,
    @Periodo CHAR(1)
AS
INSERT INTO dbo.RUBRO (Nombre, Porcentaje, Numero, Curso, Anio, Periodo)
VALUES
    ('Quices', 30.0, @Numero, @Curso, @Anio, @Periodo),
    ('Examenes', 30.0, @Numero, @Curso, @Anio, @Periodo),
	('Proyectos', 40.0, @Numero, @Curso, @Anio, @Periodo);

-- ### RUBROS ### --

-- Crear un rubro
GO
CREATE PROCEDURE dbo.sp_create_rubro
	@Nombre VARCHAR(50),
	@Porcentaje DECIMAL(5,2),
	@Numero INT,
    @Curso VARCHAR(10),
    @Anio INT,
    @Periodo CHAR(1)
AS
INSERT INTO dbo.RUBRO (Nombre, Porcentaje, Numero, Curso, Anio, Periodo)
VALUES (@Nombre, @Porcentaje, @Numero, @Curso, @Anio, @Periodo);

-- Obtener un rubro por atributos
GO
CREATE PROCEDURE dbo.sp_get_rubro
	@Nombre VARCHAR(50),
	@Numero INT,
    @Curso VARCHAR(10),
    @Anio INT,
    @Periodo CHAR(1)
AS
SELECT Nombre, Porcentaje, Numero, Curso, Anio, Periodo 
FROM dbo.RUBRO
WHERE Nombre = @Nombre AND Numero = @Numero AND Curso = @Curso AND Anio = @Anio AND Periodo = @Periodo;

-- Obtener los rubros de un grupo 
GO
CREATE PROCEDURE dbo.sp_get_rubros_grupo 
	@Numero INT,
    @Curso VARCHAR(10),
    @Anio INT,
    @Periodo CHAR(1)
AS
SELECT Nombre, Porcentaje, Numero, Curso, Anio, Periodo 
FROM dbo.RUBRO
WHERE Numero = @Numero AND Curso = @Curso AND Anio = @Anio AND Periodo = @Periodo;

-- Actualizar un rubro
GO 
CREATE PROCEDURE dbo.sp_update_rubro
	@Nombre VARCHAR(50),
    @NuevoNombre VARCHAR(50),
	@Porcentaje DECIMAL(5,2),
    @Numero INT,
    @Curso VARCHAR(10),
    @Anio INT,
    @Periodo CHAR(1)
AS
UPDATE dbo.RUBRO
SET Nombre = @NuevoNombre, Porcentaje = @Porcentaje
WHERE Nombre = @Nombre AND Numero = @Numero AND Curso = @Curso AND Anio = @Anio AND Periodo = @Periodo;

-- Eliminar un rubro
GO 
CREATE PROCEDURE dbo.sp_delete_rubro
	@Nombre VARCHAR(50),
    @Numero INT,
    @Curso VARCHAR(10),
    @Anio INT,
    @Periodo CHAR(1)
AS
DELETE FROM dbo.Rubro
WHERE Nombre = @Nombre AND Numero = @Numero AND Curso = @Curso AND Anio = @Anio AND Periodo = @Periodo;

-- Obtener todas las carpetas de un grupo
GO
CREATE PROCEDURE dbo.sp_get_group_folders
	@Numero INT,
    @Curso VARCHAR(10),
    @Anio INT,
    @Periodo CHAR(1)
AS
SELECT Nombre, Solo_lectura, Tipo, Numero, Curso, Anio, Periodo
FROM dbo.CARPETA
WHERE Tipo = 'NORMAL' AND Numero = @Numero AND Curso = @Curso AND Anio = @Anio AND Periodo = @Periodo; --Tipo 0 -> documentos normales

-- Obtener todas las carpetas especiales de un grupo
GO
CREATE PROCEDURE dbo.sp_get_all_group_folders
    @Numero INT,
    @Curso VARCHAR(10),
    @Anio INT,
    @Periodo CHAR(1)
AS
SELECT Nombre, Solo_lectura, Tipo, Numero, Curso, Anio, Periodo
FROM dbo.CARPETA
WHERE Tipo <> 'NORMAL' AND Numero = @Numero AND Curso = @Curso AND Anio = @Anio AND Periodo = @Periodo;

-- Obtener todos los archivos de un grupo
GO
CREATE PROCEDURE dbo.sp_get_group_files
    @Numero INT,
    @Curso VARCHAR(10),
    @Anio INT,
    @Periodo CHAR(1)
AS
SELECT Nombre, Fecha_creacion, Tamanio, Carpeta, Tipo_carpeta, Numero, Curso, Anio, Periodo
FROM dbo.ARCHIVO
WHERE Tipo_carpeta = 'RAIZ' AND Numero = @Numero AND Curso = @Curso AND Anio = @Anio AND Periodo = @Periodo;

-- Crear carpetas iniciales para un grupo
GO
CREATE PROCEDURE dbo.sp_create_initial_folders
	@Numero INT,
    @Curso VARCHAR(10),
    @Anio INT,
    @Periodo CHAR(1)
AS
INSERT INTO dbo.CARPETA (Nombre, Solo_lectura, Tipo, Numero, Curso, Anio, Periodo)
VALUES
	('Documentos', 1, 'RAIZ', @Numero, @Curso, @Anio, @Periodo),
    ('Especificaciones', 1, 'ESPECIFICACIONES', @Numero, @Curso, @Anio, @Periodo),
    ('Entregables', 1, 'ENTREGABLES', @Numero, @Curso, @Anio, @Periodo),
    ('Detalles', 1, 'DETALLES', @Numero, @Curso, @Anio, @Periodo),
	('Presentaciones', 1, 'NORMAL', @Numero, @Curso, @Anio, @Periodo),
	('Quices', 1, 'NORMAL', @Numero, @Curso, @Anio, @Periodo),
	('Exámenes', 1, 'NORMAL', @Numero, @Curso, @Anio, @Periodo),
	('Proyectos', 1, 'NORMAL', @Numero, @Curso, @Anio, @Periodo);

-- Obtener una carpeta por atributos
GO
CREATE PROCEDURE dbo.sp_get_folder
	@Nombre VARCHAR(100),
    @Tipo VARCHAR(20),
    @Numero INT,
    @Curso VARCHAR(10),
    @Anio INT,
    @Periodo CHAR(1)
AS
SELECT Nombre, Solo_lectura, Tipo, Numero, Curso, Anio, Periodo
FROM dbo.CARPETA
WHERE 
    Nombre = @Nombre AND Tipo = @Tipo AND Numero = @Numero AND Curso = @Curso AND Anio = @Anio AND Periodo = @Periodo;

GO
CREATE PROCEDURE dbo.sp_get_type_folder
    @Tipo VARCHAR(20),
    @Numero INT,
    @Curso VARCHAR(10),
    @Anio INT,
    @Periodo CHAR(1)
AS
SELECT Nombre, Solo_lectura, Tipo, Numero, Curso, Anio, Periodo
FROM dbo.CARPETA
WHERE Tipo = @Tipo AND Numero = @Numero AND Curso = @Curso AND Anio = @Anio AND Periodo = @Periodo;

GO
CREATE PROCEDURE dbo.sp_get_root_folder
	@Numero INT,
    @Curso VARCHAR(10),
    @Anio INT,
    @Periodo CHAR(1)
AS
SELECT Nombre, Solo_lectura, Tipo, Numero, Curso, Anio, Periodo
FROM dbo.CARPETA
WHERE Tipo = 'RAIZ' AND Numero = @Numero AND Curso = @Curso AND Anio = @Anio AND Periodo = @Periodo;

-- Actualizar una carpeta
GO
CREATE PROCEDURE dbo.sp_update_folder
	@Nombre VARCHAR(100),
    @NuevoNombre VARCHAR(100),
	@Tipo VARCHAR(20),
    @Numero INT,
    @Curso VARCHAR(10),
    @Anio INT,
    @Periodo CHAR(1)
AS
UPDATE dbo.CARPETA
SET Nombre = @NuevoNombre
WHERE Tipo = @Tipo AND Solo_lectura = 0 AND  Nombre = @Nombre AND Numero = @Numero AND Curso = @Curso AND Anio = @Anio AND Periodo = @Periodo;

-- Crear una carpeta
GO
CREATE PROCEDURE dbo.sp_create_folder
	@Nombre VARCHAR(100),
	@SoloLectura BIT,
	@Tipo VARCHAR(20),
    @Numero INT,
    @Curso VARCHAR(10),
    @Anio INT,
    @Periodo CHAR(1)
AS
INSERT INTO dbo.CARPETA (Nombre, Solo_lectura, Tipo, Numero, Curso, Anio, Periodo) 
VALUES (@Nombre, @SoloLectura, @Tipo, @Numero, @Curso, @Anio, @Periodo);

-- Eliminar una carpeta
GO
CREATE PROCEDURE dbo.sp_delete_folder
	@Nombre VARCHAR(100),
    @Tipo VARCHAR(20),
	@Numero INT,
    @Curso VARCHAR(10),
    @Anio INT,
    @Periodo CHAR(1)
AS
DELETE FROM dbo.CARPETA
WHERE Tipo = 'NORMAL' AND Solo_lectura = 0 AND Nombre = @Nombre AND Numero = @Numero AND Curso = @Curso AND Anio = @Anio AND Periodo = @Periodo;

-- Obtener los archivos de una carpeta
GO
CREATE PROCEDURE dbo.sp_get_files
	@Nombre VARCHAR(100),
    @Tipo VARCHAR(20),
	@Numero INT,
    @Curso VARCHAR(10),
    @Anio INT,
    @Periodo CHAR(1)
AS
SELECT Nombre, Fecha_creacion, Tamanio, Carpeta, Tipo_carpeta, Numero, Curso, Anio, Periodo
FROM dbo.ARCHIVO
WHERE Carpeta = @Nombre AND Tipo_carpeta = @Tipo AND Numero = @Numero AND Curso = @Curso AND Anio = @Anio AND Periodo = @Periodo;

-- Obtener un archivo por atributos
GO
CREATE PROCEDURE dbo.sp_get_file
	@Nombre VARCHAR(100),
    @Carpeta VARCHAR(100),
	@Tipo_carpeta VARCHAR(20),
    @Numero INT,
    @Curso VARCHAR(10),
    @Anio INT,
    @Periodo CHAR(1)
AS
SELECT Nombre, Fecha_creacion, Tamanio, Carpeta, Tipo_carpeta, Numero, Curso, Anio, Periodo
FROM dbo.ARCHIVO
WHERE Nombre = @Nombre AND Carpeta = @Carpeta AND Tipo_carpeta = @Tipo_carpeta AND Numero = @Numero AND Curso = @Curso AND Anio = @Anio AND Periodo = @Periodo;

-- Crear un archivo
GO
CREATE PROCEDURE dbo.sp_create_file
	@Nombre VARCHAR(50),
	@FechaCreacion DATETIME,
	@Tamanio INT,
    @Carpeta VARCHAR(100),
    @Tipo_carpeta VARCHAR(20),
    @Numero INT,
    @Curso VARCHAR(10),
    @Anio INT,
    @Periodo CHAR(1)
AS
INSERT INTO dbo.ARCHIVO (Nombre, Fecha_creacion, Tamanio, Carpeta, Tipo_carpeta, Numero, Curso, Anio, Periodo)
VALUES (@Nombre, @FechaCreacion, @Tamanio, @Carpeta, @Tipo_carpeta, @Numero, @Curso, @Anio, @Periodo);

-- Actualizar un archivo
GO
CREATE PROCEDURE dbo.sp_update_file
	@Nombre VARCHAR(50),
    @NombreNuevo VARCHAR(50),
    @FechaCreacion DATETIME,
	@Tamanio INT,
	@Carpeta VARCHAR(100),
    @Tipo_carpeta VARCHAR(20),
    @Numero INT,
    @Curso VARCHAR(10),
    @Anio INT,
    @Periodo CHAR(1)
AS
UPDATE dbo.ARCHIVO
SET Nombre = @NombreNuevo
WHERE Nombre = @Nombre AND  Carpeta = @Carpeta AND Tipo_carpeta = @Tipo_carpeta AND Numero = @Numero AND Curso = @Curso AND Anio = @Anio AND Periodo = @Periodo;

-- Eliminar un archivo
GO
CREATE PROCEDURE dbo.sp_delete_file
	@Nombre VARCHAR(50),
    @Carpeta VARCHAR(100),
    @Tipo_carpeta VARCHAR(20),
    @Numero INT,
    @Curso VARCHAR(10),
    @Anio INT,
    @Periodo CHAR(1)
AS
DELETE FROM dbo.ARCHIVO
WHERE Nombre = @Nombre AND Carpeta = @Carpeta AND Tipo_carpeta = @Tipo_carpeta AND Numero = @Numero AND Curso = @Curso AND Anio = @Anio AND Periodo = @Periodo;

-- ### PROCEDIMIENTOS DE NOTICIAS ### --

-- Obtener todas las noticias de un grupo
GO
CREATE PROCEDURE dbo.sp_get_noticias_grupo
	@Numero INT,
    @Curso VARCHAR(10),
    @Anio INT,
    @Periodo CHAR(1)
AS
SELECT Titulo, Mensaje, Autor, Fecha_publicacion, Numero, Curso, Anio, Periodo
FROM dbo.NOTICIA
WHERE Numero = @Numero AND Curso = @Curso AND Anio = @Anio AND Periodo = @Periodo
ORDER BY Fecha_publicacion DESC;

-- Obtener una noticia por atributos
GO
CREATE PROCEDURE dbo.sp_get_noticia
	@Titulo VARCHAR(200),
    @Fecha_publicacion DATETIME,
    @Numero INT,
    @Curso VARCHAR(10),
    @Anio INT,
    @Periodo CHAR(1)
AS
SELECT Titulo, Mensaje, Autor, Fecha_publicacion, Numero, Curso, Anio, Periodo
FROM dbo.NOTICIA
WHERE Titulo = @Titulo AND Fecha_publicacion = @Fecha_publicacion AND Numero = @Numero AND Curso = @Curso AND Anio = @Anio AND Periodo = @Periodo;

-- Crear una noticia
GO
CREATE PROCEDURE dbo.sp_create_noticia 
	@Titulo VARCHAR(100),
	@Mensaje TEXT,
	@Autor VARCHAR(50),
	@Fecha_publicacion DATETIME,
    @Numero INT,
    @Curso VARCHAR(10),
    @Anio INT,
    @Periodo CHAR(1)
AS
INSERT INTO dbo.NOTICIA (Titulo, Mensaje, Autor, Fecha_publicacion, Numero, Curso, Anio, Periodo)
VALUES (@Titulo, @Mensaje, @Autor, @Fecha_publicacion, @Numero, @Curso, @Anio, @Periodo);

-- Actualizar una noticia
GO
CREATE PROCEDURE dbo.sp_update_noticia
	@Titulo VARCHAR(100),
	@TituloNuevo VARCHAR(100),
    @Mensaje TEXT,
    @Fecha_publicacion DATETIME,
    @Numero INT,
    @Curso VARCHAR(10),
    @Anio INT,
    @Periodo CHAR(1)
AS
UPDATE dbo.NOTICIA
SET Titulo = @TituloNuevo, Mensaje = @Mensaje
WHERE Titulo = @Titulo AND Fecha_publicacion = @Fecha_publicacion AND Numero = @Numero AND Curso = @Curso AND Anio = @Anio AND Periodo = @Periodo;

-- Eliminar una noticia
GO
CREATE PROCEDURE dbo.sp_delete_noticia
	@Titulo VARCHAR(200),
    @Fecha_publicacion DATETIME,
    @Numero INT,
    @Curso VARCHAR(10),
    @Anio INT,
    @Periodo CHAR(1)
AS
DELETE FROM dbo.NOTICIA
WHERE Titulo = @Titulo AND Fecha_publicacion = @Fecha_publicacion AND Numero = @Numero AND Curso = @Curso AND Anio = @Anio AND Periodo = @Periodo;

-- ### PROCEDIMIENTOS EVALUACIONES ### --

-- Obtener las evaluaciones de un rubro
GO
CREATE PROCEDURE dbo.sp_get_evaluaciones_rubro
    @Rubro VARCHAR(100),
    @Numero INT,
    @Curso VARCHAR(10),
    @Anio INT,
    @Periodo CHAR(1)
AS
SELECT Nombre, Notas_publicadas, Fecha_entrega, Peso_nota, Grupal, Especificacion, Carpeta_especificacion, Tipo_carpeta_especificacion, Rubro, Numero, Curso, Anio, Periodo
FROM EVALUACION
WHERE Rubro = @Rubro AND Numero = @Numero AND Curso = @Curso AND Anio = @Anio AND Periodo = @Periodo;

-- Info de la evaluacion, procedimiento a vista --

-- TODO: crear vista en archivo de vistas
-- Obtener la información de una evaluación
-- GO 
-- CREATE PROCEDURE dbo.sp_get_info_evaluacion
-- 	@Nombre VARCHAR(100),
--     @Nombre_r VARCHAR(100),
--     @Numero INT,
--     @Curso VARCHAR(10),
--     @Anio INT,
--     @Periodo CHAR(1)
-- AS
-- SELECT Id_evaluacion, Archivo_especificacion, Id_Espec, Fecha_creacion_espec, Archivo_entregable, Fecha_creacion_entre, Id_Entre, Id_grupo, NotaFinal, Observaciones, Retroalimentacion, Id_retro
-- FROM INFO_EVALUACION
-- WHERE ;

GO
CREATE PROCEDURE dbo.sp_get_info_evaluacion
    @Evaluacion VARCHAR(100),
    @Rubro VARCHAR(100),
    @Numero INT,
    @Curso VARCHAR(10),
    @Anio INT,
    @Periodo CHAR(1),
    @Estudiante VARCHAR(50)
AS
SELECT 
    Nombre, Notas_publicadas, Fecha_entrega, Peso_nota, Grupal, Rubro, Numero, Curso, Anio, Periodo,
    Especificacion, Carpeta_especificacion, Tipo_carpeta_especificacion,
    Entregable, Carpeta_entregable, Tipo_carpeta_entregable, Fecha_entregable,
    Detalle, Carpeta_detalle, Tipo_carpeta_detalle,
    Id_evaluacion_grupo, Nota, Observaciones
FROM dbo.INFO_EVALUACION
LEFT JOIN dbo.EVALUACION_INTEGRANTES as EI ON
    EI.Id_grupo = Id_evaluacion_grupo AND
    EI.Estudiante = @Estudiante
WHERE
    Nombre = @Evaluacion AND Rubro = @Rubro AND Numero = @Numero AND Curso = @Curso AND Anio = @Anio AND Periodo = @Periodo;


GO
CREATE PROCEDURE dbo.get_students_group
	@Numero INT,
    @Curso VARCHAR(10),
    @Anio INT,
    @Periodo CHAR(1)
AS
SELECT Estudiante, Numero, Curso, Anio, Periodo
FROM dbo.ESTUDIANTE_GRUPO
WHERE Numero = @Numero AND Curso = @Curso AND Anio = @Anio AND Periodo = @Periodo

GO
CREATE PROCEDURE dbo.sp_get_student_groups
    @Student VARCHAR(50)
AS
SELECT CG.Codigo, CG.Nombre_curso, CG.Creditos, CG.Carrera, CG.Numero_grupo, CG.Anio_semestre, CG.Periodo_semestre
FROM dbo.CURSO_GRUPO as CG
LEFT JOIN dbo.ESTUDIANTE_GRUPO as EG ON
    EG.Numero = CG.Numero_grupo AND
    EG.Curso = CG.Codigo AND 
    EG.Anio = CG.Anio_semestre AND
    EG.Periodo = CG.Periodo_semestre
WHERE
    EG.Estudiante = @Student
ORDER BY CG.Anio_semestre DESC, CG.Periodo_semestre DESC;

GO
CREATE PROCEDURE dbo.sp_get_profesor_groups
	@Cedula VARCHAR(50)
AS
SELECT CG.Codigo, CG.Nombre_curso, CG.Creditos, CG.Carrera, CG.Numero_grupo, CG.Anio_semestre, CG.Periodo_semestre
FROM dbo.CURSO_GRUPO as CG
LEFT JOIN dbo.PROFESOR_GRUPO AS PG ON
	PG.Numero = CG.Numero_grupo AND
	PG.Curso = CG.Codigo AND
	PG.Anio = CG.Anio_semestre AND
	PG.Periodo = CG.Periodo_semestre
WHERE
	PG.Profesor = @Cedula
ORDER BY CG.Anio_semestre DESC, CG.Periodo_semestre DESC;

--- Procedimientos para asignar evaluaciones
GO
CREATE PROCEDURE dbo.sp_assign_evaluation
	@Nombre_evaluacion VARCHAR(100),
	@Rubro VARCHAR(100),
	@Peso DECIMAL(5,2),
	@Fecha DATETIME,
	@Espec VARCHAR(100),
	@Individual BIT,
	@Numero INT,
    @Curso VARCHAR(10),
    @Anio INT,
    @Periodo CHAR(1)
AS
BEGIN


END;

-- ACTUALIZADOS
--dbo.sp_create_grupo_estudiante
--dbo.sp_create_grupo_profesor
--dbo.sp_create_initial_rubro
--dbo.sp_get_semestre
--dbo.sp_create_rubro
--dbo.sp_get_rubro
--dbo.sp_get_rubros_grupo
--dbo.sp_update_rubro
--dbo.sp_delete_rubro
--dbo.sp_get_group_folders
--dbo.sp_get_group_files
--dbo.sp_create_initial_folders
--dbo.sp_get_folder
--dbo.sp_get_folder_by_name
--dbo.sp_get_root_folder
--dbo.sp_update_folder
--dbo.sp_create_folder
--dbo.sp_delete_folder
--dbo.sp_get_files
--dbo.sp_get_file
--dbo.sp_create_file
--dbo.sp_update_file
--dbo.sp_delete_file
--dbo.sp_get_noticias_grupo
--dbo.sp_get_noticia
--dbo.sp_create_noticia
--dbo.sp_update_noticia
--dbo.sp_delete_noticia
--dbo.sp_get_evaluaciones_rubro


-- ELIMINADOS
--dbo.sp_get_semestre
--dbo.sp_get_grupo
--dbo.sp_get_file_from_name

-- TIPOS DE CARPETA
-- 0 -> NORMAL
-- 1 -> RAIZ
-- 2 -> ESPECIFICACIONES
-- 3 -> ENTREGABLES
-- 4 -> DETALLES