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
  [Especificacion] varchar(100) NOT NULL,
  [Carpeta_especificacion] varchar(100) NOT NULL,
  [Tipo_carpeta_especificacion] varchar(20) NOT NULL,
  [Rubro] varchar(100) NOT NULL,
  [Numero] int NOT NULL,
  [Curso] varchar(10) NOT NULL,
  [Anio] int NOT NULL,
  [Periodo] char(1) NOT NULL,
  PRIMARY KEY ([Nombre], [Rubro], [Numero], [Curso], [Anio], [Periodo])
)
GO

CREATE TABLE [EVALUACION_GRUPO] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Nota] decimal(5,2),
  [Observaciones] text,
  [Entregable] varchar(100),
  [Carpeta_entregable] varchar(100),
  [Tipo_carpeta_entregable] varchar(20),
  [Detalle] varchar(100),
  [Carpeta_detalle] varchar(100),
  [Tipo_carpeta_detalle] varchar(20),
  [Evaluacion] varchar(100) NOT NULL,
  [Rubro] varchar(100) NOT NULL,
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
  [Tipo] varchar(20) NOT NULL,
  [Numero] int NOT NULL,
  [Curso] varchar(10) NOT NULL,
  [Anio] int NOT NULL,
  [Periodo] char(1) NOT NULL,
  PRIMARY KEY ([Nombre], [Tipo], [Numero], [Curso], [Anio], [Periodo])
)
GO

CREATE TABLE [ARCHIVO] (
  [Nombre] varchar(100) NOT NULL,
  [Fecha_creacion] datetime NOT NULL,
  [Tamanio] int NOT NULL,
  [Carpeta] varchar(100) NOT NULL,
  [Tipo_carpeta] varchar(20) NOT NULL,
  [Numero] int NOT NULL,
  [Curso] varchar(10) NOT NULL,
  [Anio] int NOT NULL,
  [Periodo] char(1) NOT NULL,
  PRIMARY KEY ([Nombre], [Carpeta], [Tipo_carpeta], [Numero], [Curso], [Anio], [Periodo])
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

ALTER TABLE [EVALUACION] ADD FOREIGN KEY ([Rubro], [Numero], [Curso], [Anio], [Periodo]) REFERENCES [RUBRO] ([Nombre], [Numero], [Curso], [Anio], [Periodo]) ON DELETE CASCADE ON UPDATE CASCADE
GO

ALTER TABLE [EVALUACION_GRUPO] ADD FOREIGN KEY ([Evaluacion], [Rubro], [Numero], [Curso], [Anio], [Periodo]) REFERENCES [EVALUACION] ([Nombre], [Rubro], [Numero], [Curso], [Anio], [Periodo]) ON DELETE CASCADE ON UPDATE CASCADE
GO

ALTER TABLE [EVALUACION_INTEGRANTES] ADD FOREIGN KEY ([Id_grupo]) REFERENCES [EVALUACION_GRUPO] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO

ALTER TABLE [CARPETA] ADD FOREIGN KEY ([Numero], [Curso], [Anio], [Periodo]) REFERENCES [GRUPO] ([Numero], [Curso], [Anio], [Periodo]) ON DELETE CASCADE ON UPDATE CASCADE
GO

ALTER TABLE [ARCHIVO] ADD FOREIGN KEY ([Carpeta], [Tipo_carpeta], [Numero], [Curso], [Anio], [Periodo]) REFERENCES [CARPETA] ([Nombre], [Tipo], [Numero], [Curso], [Anio], [Periodo]) ON DELETE CASCADE ON UPDATE CASCADE
GO

ALTER TABLE [EVALUACION] ADD FOREIGN KEY ([Especificacion], [Carpeta_especificacion], [Tipo_carpeta_especificacion], [Numero], [Curso], [Anio], [Periodo]) REFERENCES [ARCHIVO] ([Nombre], [Carpeta], [Tipo_carpeta], [Numero], [Curso], [Anio], [Periodo])
GO

ALTER TABLE [EVALUACION_GRUPO] ADD FOREIGN KEY ([Entregable], [Carpeta_entregable], [Tipo_carpeta_entregable], [Numero], [Curso], [Anio], [Periodo]) REFERENCES [ARCHIVO] ([Nombre], [Carpeta], [Tipo_carpeta], [Numero], [Curso], [Anio], [Periodo])
GO

ALTER TABLE [EVALUACION_GRUPO] ADD FOREIGN KEY ([Detalle], [Carpeta_detalle], [Tipo_carpeta_detalle], [Numero], [Curso], [Anio], [Periodo]) REFERENCES [ARCHIVO] ([Nombre], [Carpeta], [Tipo_carpeta], [Numero], [Curso], [Anio], [Periodo])
GO

