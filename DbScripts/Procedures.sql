-- Procedimientos almacenados
GO
CREATE PROCEDURE dbo.sp_get_courses 
AS
SELECT Codigo, Nombre, Carrera, Habilitado
FROM dbo.CURSO;

-- EXECUTE dbo.sp_get_courses;

GO
CREATE PROCEDURE dbo.sp_get_active_courses
AS
SELECT Codigo, Nombre, Carrera, Habilitado
FROM dbo.CURSO
WHERE Habilitado = 1;

-- EXECUTE dbo.sp_get_active_courses;

GO
CREATE PROCEDURE dbo.sp_get_course
	@CourseId VARCHAR(50)
AS
SELECT Codigo, Nombre, Carrera, Habilitado
FROM dbo.CURSO
WHERE Codigo = @CourseId;

--EXECUTE dbo.sp_get_course 'CE1101';

GO
CREATE PROCEDURE dbo.sp_set_course_state
	@CourseId VARCHAR(50),
	@Enabled BIT
AS
UPDATE dbo.CURSO
SET Habilitado = @Enabled
WHERE Codigo = @CourseId;

-- EXECUTE dbo.sp_set_course_state 'CE1101', 1;

GO
CREATE PROCEDURE dbo.sp_create_course
	@Codigo VARCHAR(50),
	@Nombre VARCHAR(50),
	@Carrera VARCHAR(50),
	@Habilitado BIT
AS
INSERT INTO dbo.CURSO (Codigo, Nombre, Carrera, Habilitado) VALUES
	(@Codigo, @Nombre, @Carrera, @Habilitado);

-- EXECUTE dbo.sp_create_course 'delete', 'Lenguajes, Compiladores e Interpretes', 'Computadores', 1;

GO
CREATE PROCEDURE dbo.sp_delete_course
	@CourseId VARCHAR(50)
AS
DELETE FROM dbo.CURSO
WHERE Codigo = @CourseId;

-- EXECUTE dbo.sp_delete_course 'delete';

GO
CREATE PROCEDURE dbo.sp_update_course
	@Codigo VARCHAR(50),
	@Nombre VARCHAR(50),
	@Carrera VARCHAR(50),
	@Habilitado VARCHAR(50)
AS
UPDATE dbo.CURSO
SET Nombre = @Nombre, Carrera = @Carrera, Habilitado = @Habilitado
WHERE Codigo = @Codigo;

-- EXECUTE dbo.sp_update_course 'CE1101', 'Introducci�n a la programaci�n', 'Computadores', 1;

-- Procedimientos almacenados de periodo
GO 
CREATE PROCEDURE dbo.sp_get_periods
AS
SELECT Id, Nombre 
FROM dbo.PERIODO;

GO 
CREATE PROCEDURE dbo.sp_get_period
	@periodId INT
AS
SELECT Id, Nombre 
FROM dbo.PERIODO
WHERE Id = @periodId;

-- Procedimientos para inicializar semestre
GO 
CREATE PROCEDURE dbo.sp_create_semester
	@semesterId INT,
	@anioSemester INT,
	@idPeriodo INT
AS
INSERT INTO dbo.SEMESTRE (Id, Anio, Id_periodo)
VALUES (@semesterId, @anioSemester, @idPeriodo);

GO 
CREATE PROCEDURE dbo.sp_create_curso_semestre
	@idCursoSemestre INT,
	@idCurso VARCHAR(50),
	@idSemestre INT
AS
INSERT INTO dbo.CURSO_SEMESTRE (Id, Id_curso, Id_semestre)
VALUES (@idCursoSemestre, @idCurso, @idSemestre);

GO
CREATE PROCEDURE dbo.sp_create_grupo
	@numeroGrupo INT,
	@idCurso VARCHAR(50)
AS
INSERT INTO dbo.GRUPO (Numero, Id_curso)
VALUES (@numeroGrupo, @idCurso);

GO
CREATE PROCEDURE dbo.sp_create_grupo_estudiante
	@numeroGrupo INT,
	@carnet VARCHAR(50)
AS
INSERT INTO dbo.GRUPO_ESTUDIANTES (Numero_Grupo, Estudiante)
VALUES (@numeroGrupo, @carnet);

GO
CREATE PROCEDURE dbo.sp_create_grupo_profesor
	@numeroGrupo INT,
	@cedula VARCHAR(50)
AS
INSERT INTO dbo.GRUPO_PROFESOR (Numero_Grupo, Profesor)
VALUES (@numeroGrupo, @cedula);

GO
CREATE PROCEDURE dbo.sp_create_carpeta
	@idCarpeta INT,
	@soloLectura BIT,
	@ruta VARCHAR(50),
	@idGrupo INT,
	@nombreCarpeta VARCHAR(50)
AS
INSERT INTO dbo.CARPETA (Id, Nombre, Solo_lectura, Ruta, Id_grupo)
VALUES (@idCarpeta, @nombreCarpeta, @soloLectura, @ruta, @idGrupo);

GO
CREATE PROCEDURE dbo.sp_create_rubro
	@nombreRubro VARCHAR(50),
	@porcentaje DECIMAL(5,2),
	@idGrupo INT
AS
INSERT INTO dbo.RUBRO (Nombre, Porcentaje, Id_grupo)
VALUES (@nombreRubro, @porcentaje, @idGrupo);




-- Procedimientos almacenados de archivos y carpetas

GO
CREATE PROCEDURE [dbo].[sp_get_folders]
	@GroupId INT
AS
SELECT Id, Nombre, Solo_lectura, Ruta, Id_grupo
FROM [dbo].[CARPETA]
WHERE Id_grupo = @GroupId;

-- [dbo].[sp_get_folders] 1;

GO
CREATE PROCEDURE [dbo].[sp_update_folder]
	@Id INT,
	@Nombre VARCHAR(100),
	@SoloLectura bit,
	@Ruta VARCHAR(50),
	@IdGrupo INT
AS
UPDATE [dbo].[CARPETA]
SET Nombre = @Nombre
WHERE Id = @Id AND Id_grupo = @IdGrupo;

GO
CREATE PROCEDURE [dbo].[sp_get_files]
	@FolderId INT
AS
SELECT Id, Fecha_creacion, Nombre, Tamanio, Id_carpeta
FROM [dbo].[ARCHIVO]
WHERE Id_carpeta = @FolderId;

-- [dbo].[sp_get_files] 1;

GO
CREATE PROCEDURE [dbo].[sp_create_file]
	@Id INT,
	@FechaCreacion DATE,
	@Nombre VARCHAR(50),
	@Tamanio DECIMAL(8,2),
	@IdCarpeta INT
AS
INSERT INTO [dbo].[ARCHIVO] (Id, Fecha_creacion, Nombre, Tamanio, Id_carpeta) VALUES
	(@Id, @FechaCreacion, @Nombre, @Tamanio, @IdCarpeta);

-- [dbo].[sp_create_file];

GO
CREATE PROCEDURE [dbo].[sp_update_file]
	@Id INT,
	@FechaCreacion DATE,
	@Nombre VARCHAR(50),
	@Tamanio DECIMAL(8,2),
	@IdCarpeta INT
AS
UPDATE [dbo].[ARCHIVO]
SET Nombre = @Nombre
WHERE Id = @Id AND Id_carpeta = @IdCarpeta;

-- [dbo].[sp_update_file];
