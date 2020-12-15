-- Procedimientos de cursos
GO
CREATE PROCEDURE dbo.sp_get_courses 
AS
SELECT Codigo, Nombre, Creditos, Carrera, Habilitado
FROM dbo.CURSO;

GO
CREATE PROCEDURE dbo.sp_get_active_courses
AS
SELECT Codigo, Nombre, Creditos, Carrera, Habilitado
FROM dbo.CURSO
WHERE Habilitado = 1;

GO
CREATE PROCEDURE dbo.sp_get_course
	@CourseId VARCHAR(10)
AS
SELECT Codigo, Nombre, Creditos, Carrera, Habilitado
FROM dbo.CURSO
WHERE Codigo = @CourseId;

GO
CREATE PROCEDURE dbo.sp_create_course
	@Codigo VARCHAR(10),
	@Nombre VARCHAR(100),
	@Creditos INT,
	@Carrera VARCHAR(100),
	@Habilitado BIT
AS
INSERT INTO dbo.CURSO (Codigo, Nombre, Creditos, Carrera, Habilitado) VALUES
	(@Codigo, @Nombre, @Creditos, @Carrera, @Habilitado);

GO
CREATE PROCEDURE dbo.sp_delete_course
	@CourseId VARCHAR(10)
AS
DELETE FROM dbo.CURSO
WHERE Codigo = @CourseId;

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


-- ### INICIALIZACIÓN SEMESTRE ###
GO 
CREATE PROCEDURE dbo.sp_create_semester
	@semesterId INT,
	@anioSemester INT,
	@Periodo CHAR(1)
AS
INSERT INTO dbo.SEMESTRE (Id, Anio, Periodo)
VALUES (@semesterId, @anioSemester, @Periodo);

-- Creación de grupos
GO
CREATE PROCEDURE dbo.sp_create_grupo
	@numeroGrupo INT,
	@idCurso VARCHAR(10),
	@idSemestre INT
AS
INSERT INTO dbo.GRUPO (Numero, Id_curso, Id_semestre)
VALUES (@numeroGrupo, @idCurso, @idSemestre);

GO
CREATE PROCEDURE dbo.sp_create_grupo_estudiante
	@idGrupo INT,
	@carnet VARCHAR(50)
AS
INSERT INTO dbo.ESTUDIANTE_GRUPO (Id_grupo, Estudiante)
VALUES (@idGrupo, @carnet);

GO
CREATE PROCEDURE dbo.sp_create_grupo_profesor
	@idGrupo INT,
	@cedula VARCHAR(50)
AS
INSERT INTO dbo.PROFESOR_GRUPO (Id_grupo, Profesor)
VALUES (@idGrupo, @cedula);

GO
CREATE PROCEDURE dbo.sp_create_initial_rubro
	@IdGrupo INT
AS
INSERT INTO dbo.RUBRO (Id_grupo, Nombre, Porcentaje) VALUES 
	(@IdGrupo, 'Quices', 30.0),
	(@IdGrupo, 'Examenes', 30.0),
	(@IdGrupo, 'Proyectos', 40.0);

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
CREATE PROCEDURE dbo.sp_get_group_folders
	@GroupId INT
AS
SELECT Id, Id_grupo, Nombre, Solo_lectura, Raiz
FROM dbo.CARPETA
WHERE Id_grupo = @GroupId AND Raiz = 0;

GO
CREATE PROCEDURE dbo.sp_create_initial_folders
	@IdGrupo INT
AS
INSERT INTO dbo.CARPETA (Id_grupo, Nombre, Solo_lectura, Raiz) VALUES
	(@IdGrupo, 'RAIZ', 1, 1),
	(@IdGrupo, 'Presentaciones', 1, 0),
	(@IdGrupo, 'Quices', 1, 0),
	(@IdGrupo, 'Exámenes', 1, 0),
	(@IdGrupo, 'Proyectos', 1, 0);

GO
CREATE PROCEDURE dbo.sp_get_folder
	@FolderId INT
AS
SELECT Id, Id_grupo, Nombre, Solo_lectura, Raiz
FROM dbo.CARPETA
WHERE Id = @FolderId;

GO
CREATE PROCEDURE dbo.sp_update_folder
	@Id INT,
	@IdGrupo INT,
	@Nombre VARCHAR(100),
	@SoloLectura bit,
	@Raiz BIT
AS
UPDATE dbo.CARPETA
SET Nombre = @Nombre
WHERE Id = @Id AND Id_grupo = @IdGrupo;

GO
CREATE PROCEDURE dbo.sp_create_folder
	@IdGrupo INT,
	@Nombre VARCHAR(100),
	@SoloLectura BIT,
	@Raiz BIT
AS
INSERT INTO dbo.CARPETA (Id_grupo, Nombre, Solo_lectura, Raiz) VALUES
	(@IdGrupo, @Nombre, @SoloLectura, @Raiz);

GO
CREATE PROCEDURE dbo.sp_delete_folder
	@FolderId INT
AS
DELETE FROM dbo.CARPETA
WHERE Id = @FolderId AND Raiz = 0 AND Solo_lectura = 0;

GO
CREATE PROCEDURE dbo.sp_get_files
	@FolderId INT
AS
SELECT Id, Id_carpeta, Nombre, Fecha_creacion, Tamanio
FROM dbo.ARCHIVO
WHERE Id_carpeta = @FolderId;

-- [dbo].[sp_get_files] 1;

GO
CREATE PROCEDURE dbo.sp_get_file
	@FileId INT
AS
SELECT Id, Id_carpeta, Nombre, Fecha_creacion, Tamanio
FROM dbo.ARCHIVO
WHERE Id = @FileId;

GO
CREATE PROCEDURE dbo.sp_create_file
	@IdCarpeta INT,
	@Nombre VARCHAR(50),
	@FechaCreacion DATETIME,
	@Tamanio DECIMAL(8,2)
AS
INSERT INTO dbo.ARCHIVO (Id_carpeta, Nombre, Fecha_creacion, Tamanio) VALUES
	(@IdCarpeta, @Nombre, @FechaCreacion, @Tamanio);

GO
CREATE PROCEDURE dbo.sp_update_file
	@Id INT,
	@FechaCreacion DATETIME,
	@Nombre VARCHAR(50),
	@Tamanio DECIMAL(8,2),
	@IdCarpeta INT
AS
UPDATE dbo.ARCHIVO
SET Nombre = @Nombre
WHERE Id = @Id AND Id_carpeta = @IdCarpeta;

GO
CREATE PROCEDURE dbo.sp_delete_file
	@FileId INT
AS
DELETE FROM dbo.ARCHIVO
WHERE Id = @FileId;