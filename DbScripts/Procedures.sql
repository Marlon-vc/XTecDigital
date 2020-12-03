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

EXECUTE dbo.sp_get_course 'CE1101';

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

-- EXECUTE dbo.sp_update_course 'CE1101', 'Introducción a la programación', 'Computadores', 1;