CREATE TABLE [SEMESTRE] (
  [Anio] int NOT NULL,
  [Periodo] char(1) NOT NULL,
  PRIMARY KEY ([Anio], [Periodo])
)
GO

CREATE TABLE [CURSO] (
  [Codigo] varchar(10) PRIMARY KEY,
  [Nombre] varchar(100) NOT NULL,
  [Creditos] int NOT NULL,
  [Carrera] varchar(100) NOT NULL,
  [Habilitado] bit NOT NULL
)
GO

CREATE TABLE [GRUPO] (
  [Numero] int NOT NULL,
  [Curso] varchar(10) NOT NULL,
  [Anio] int NOT NULL,
  [Periodo] char(1) NOT NULL,
  PRIMARY KEY ([Numero], [Curso], [Anio], [Periodo])
)
GO

CREATE TABLE [PROFESOR_GRUPO] (
  [Profesor] varchar(50) NOT NULL,
  [Numero] int NOT NULL,
  [Curso] varchar(10) NOT NULL,
  [Anio] int NOT NULL,
  [Periodo] char(1) NOT NULL,
  PRIMARY KEY ([Profesor], [Numero], [Curso], [Anio], [Periodo])
)
GO

CREATE TABLE [ESTUDIANTE_GRUPO] (
  [Estudiante] varchar(50) NOT NULL,
  [Numero] int NOT NULL,
  [Curso] varchar(10) NOT NULL,
  [Anio] int NOT NULL,
  [Periodo] char(1) NOT NULL,
  PRIMARY KEY ([Estudiante], [Numero], [Curso], [Anio], [Periodo])
)
GO

CREATE TABLE [NOTICIA] (
  [Titulo] varchar(200) NOT NULL,
  [Mensaje] text NOT NULL,
  [Autor] varchar(50) NOT NULL,
  [Fecha_publicacion] datetime NOT NULL,
  [Numero] int NOT NULL,
  [Curso] varchar(10) NOT NULL,
  [Anio] int NOT NULL,
  [Periodo] char(1) NOT NULL,
  PRIMARY KEY ([Titulo], [Fecha_publicacion], [Numero], [Curso], [Anio], [Periodo])
)
GO

CREATE TABLE [RUBRO] (
  [Nombre] varchar(100) NOT NULL,
  [Porcentaje] decimal(5,2) NOT NULL,
  [Numero] int NOT NULL,
  [Curso] varchar(10) NOT NULL,
  [Anio] int NOT NULL,
  [Periodo] char(1) NOT NULL,
  PRIMARY KEY ([Nombre], [Numero], [Curso], [Anio], [Periodo])
)
GO

CREATE TABLE [EVALUACION] (
  [Nombre] varchar(100) NOT NULL,
  [Notas_publicadas] bit NOT NULL,
  [Fecha_entrega] datetime NOT NULL,
  [Peso_nota] decimal(5,2) NOT NULL,
  [Grupal] bit NOT NULL,
  [Ruta_especificacion] varchar(200) NOT NULL,
  [Nombre_r] varchar(100) NOT NULL,
  [Numero] int NOT NULL,
  [Curso] varchar(10) NOT NULL,
  [Anio] int NOT NULL,
  [Periodo] char(1) NOT NULL,
  PRIMARY KEY ([Nombre], [Nombre_r], [Numero], [Curso], [Anio], [Periodo])
)
GO

CREATE TABLE [EVALUACION_GRUPO] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Nota] decimal(5,2),
  [Observaciones] text,
  [Ruta_entregable] varchar(200),
  [Fecha_entregable] datetime,
  [Ruta_detalle] varchar(200),
  [Nombre_e] varchar(100) NOT NULL,
  [Nombre_r] varchar(100) NOT NULL,
  [Numero] int NOT NULL,
  [Curso] varchar(10) NOT NULL,
  [Anio] int NOT NULL,
  [Periodo] char(1) NOT NULL
)
GO

CREATE TABLE [EVALUACION_INTEGRANTES] (
  [Estudiante] varchar(50) NOT NULL,
  [Id_grupo] int NOT NULL
)
GO

CREATE TABLE [CARPETA] (
  [Nombre] varchar(100) NOT NULL,
  [Solo_lectura] bit NOT NULL,
  [Raiz] bit NOT NULL,
  [Numero] int NOT NULL,
  [Curso] varchar(10) NOT NULL,
  [Anio] int NOT NULL,
  [Periodo] char(1) NOT NULL,
  PRIMARY KEY ([Nombre], [Numero], [Curso], [Anio], [Periodo])
)
GO

CREATE TABLE [ARCHIVO] (
  [Nombre] varchar(100) NOT NULL,
  [Fecha_creacion] datetime NOT NULL,
  [Tamanio] int NOT NULL,
  [Nombre_c] varchar(100) NOT NULL,
  [Numero] int NOT NULL,
  [Curso] varchar(10) NOT NULL,
  [Anio] int NOT NULL,
  [Periodo] char(1) NOT NULL,
  PRIMARY KEY ([Nombre], [Nombre_c], [Numero], [Curso], [Anio], [Periodo])
)
GO

ALTER TABLE [GRUPO] ADD FOREIGN KEY ([Anio], [Periodo]) REFERENCES [SEMESTRE] ([Anio], [Periodo]) ON UPDATE CASCADE
GO

ALTER TABLE [GRUPO] ADD FOREIGN KEY ([Curso]) REFERENCES [CURSO] ([Codigo]) ON UPDATE CASCADE
GO

ALTER TABLE [PROFESOR_GRUPO] ADD FOREIGN KEY ([Numero], [Curso], [Anio], [Periodo]) REFERENCES [GRUPO] ([Numero], [Curso], [Anio], [Periodo]) ON DELETE CASCADE ON UPDATE CASCADE
GO

ALTER TABLE [ESTUDIANTE_GRUPO] ADD FOREIGN KEY ([Numero], [Curso], [Anio], [Periodo]) REFERENCES [GRUPO] ([Numero], [Curso], [Anio], [Periodo]) ON DELETE CASCADE ON UPDATE CASCADE
GO

ALTER TABLE [NOTICIA] ADD FOREIGN KEY ([Numero], [Curso], [Anio], [Periodo]) REFERENCES [GRUPO] ([Numero], [Curso], [Anio], [Periodo]) ON DELETE CASCADE ON UPDATE CASCADE
GO

ALTER TABLE [RUBRO] ADD FOREIGN KEY ([Numero], [Curso], [Anio], [Periodo]) REFERENCES [GRUPO] ([Numero], [Curso], [Anio], [Periodo]) ON DELETE CASCADE ON UPDATE CASCADE
GO

ALTER TABLE [EVALUACION] ADD FOREIGN KEY ([Nombre_r], [Numero], [Curso], [Anio], [Periodo]) REFERENCES [RUBRO] ([Nombre], [Numero], [Curso], [Anio], [Periodo]) ON DELETE CASCADE ON UPDATE CASCADE
GO

ALTER TABLE [EVALUACION_GRUPO] ADD FOREIGN KEY ([Nombre_e], [Nombre_r], [Numero], [Curso], [Anio], [Periodo]) REFERENCES [EVALUACION] ([Nombre], [Nombre_r], [Numero], [Curso], [Anio], [Periodo]) ON DELETE CASCADE ON UPDATE CASCADE
GO

ALTER TABLE [EVALUACION_INTEGRANTES] ADD FOREIGN KEY ([Id_grupo]) REFERENCES [EVALUACION_GRUPO] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO

ALTER TABLE [CARPETA] ADD FOREIGN KEY ([Numero], [Curso], [Anio], [Periodo]) REFERENCES [GRUPO] ([Numero], [Curso], [Anio], [Periodo]) ON DELETE CASCADE ON UPDATE CASCADE
GO

ALTER TABLE [ARCHIVO] ADD FOREIGN KEY ([Nombre_c], [Numero], [Curso], [Anio], [Periodo]) REFERENCES [CARPETA] ([Nombre], [Numero], [Curso], [Anio], [Periodo]) ON DELETE CASCADE ON UPDATE CASCADE
GO

