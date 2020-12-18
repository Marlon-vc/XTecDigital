CREATE TABLE [SEMESTRE] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Anio] int NOT NULL,
  [Periodo] char(1) NOT NULL
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
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Numero] int NOT NULL,
  [Id_curso] varchar(10) NOT NULL,
  [Id_semestre] int NOT NULL
)
GO

CREATE TABLE [PROFESOR_GRUPO] (
  [Profesor] varchar(50) NOT NULL,
  [Id_grupo] int NOT NULL,
  PRIMARY KEY ([Profesor], [Id_grupo])
)
GO

CREATE TABLE [ESTUDIANTE_GRUPO] (
  [Estudiante] varchar(50) NOT NULL,
  [Id_grupo] int NOT NULL,
  PRIMARY KEY ([Estudiante], [Id_grupo])
)
GO

CREATE TABLE [NOTICIA] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Id_grupo] int NOT NULL,
  [Titulo] varchar(100) NOT NULL,
  [Mensaje] text NOT NULL,
  [Autor] varchar(50) NOT NULL,
  [Fecha_publicacion] datetime NOT NULL
)
GO

CREATE TABLE [RUBRO] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Id_grupo] int NOT NULL,
  [Nombre] varchar(100) NOT NULL,
  [Porcentaje] decimal(5,2) NOT NULL
)
GO

CREATE TABLE [EVALUACION] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Id_rubro] int NOT NULL,
  [Id_especificacion] int NOT NULL,
  [Nombre] varchar(100) NOT NULL,
  [Notas_publicadas] bit NOT NULL,
  [Fecha_entrega] datetime NOT NULL,
  [Peso_nota] decimal(5,2) NOT NULL,
  [Grupal] bit NOT NULL
)
GO

CREATE TABLE [EVALUACION_GRUPO] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Id_evaluacion] int NOT NULL,
  [Observaciones] text,
  [Id_entregable] int,
  [Id_detalle] int
)
GO

CREATE TABLE [EVALUACION_INTEGRANTES] (
  [Estudiante] varchar(50) NOT NULL,
  [Id_grupo] int NOT NULL,
  PRIMARY KEY ([Estudiante], [Id_grupo])
)
GO

CREATE TABLE [CARPETA] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Id_grupo] int NOT NULL,
  [Nombre] varchar(100) NOT NULL,
  [Solo_lectura] bit NOT NULL,
  -- [Ruta] varchar(150) NOT NULL,
  [Raiz] bit NOT NULL
)
GO

CREATE TABLE [ARCHIVO] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Id_carpeta] int NOT NULL,
  [Nombre] varchar(100) NOT NULL,
  [Fecha_creacion] datetime NOT NULL,
  [Tamanio] int NOT NULL
)
GO

CREATE TABLE [ARCHIVO_EVALUACION] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Nombre] varchar(150) NOT NULL,
  [Ruta] varchar(250) NOT NULL,
  [Fecha_creacion] datetime NOT NULL
)
GO

ALTER TABLE [GRUPO] ADD FOREIGN KEY ([Id_curso]) REFERENCES [CURSO] ([Codigo])
GO

ALTER TABLE [GRUPO] ADD FOREIGN KEY ([Id_semestre]) REFERENCES [SEMESTRE] ([Id])
GO

ALTER TABLE [PROFESOR_GRUPO] ADD FOREIGN KEY ([Id_grupo]) REFERENCES [GRUPO] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [ESTUDIANTE_GRUPO] ADD FOREIGN KEY ([Id_grupo]) REFERENCES [GRUPO] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [NOTICIA] ADD FOREIGN KEY ([Id_grupo]) REFERENCES [GRUPO] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [RUBRO] ADD FOREIGN KEY ([Id_grupo]) REFERENCES [GRUPO] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [EVALUACION] ADD FOREIGN KEY ([Id_rubro]) REFERENCES [RUBRO] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [CARPETA] ADD FOREIGN KEY ([Id_grupo]) REFERENCES [GRUPO] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [ARCHIVO] ADD FOREIGN KEY ([Id_carpeta]) REFERENCES [CARPETA] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [EVALUACION] ADD FOREIGN KEY ([Id_especificacion]) REFERENCES [ARCHIVO_EVALUACION] ([Id])
GO

ALTER TABLE [EVALUACION_GRUPO] ADD FOREIGN KEY ([Id_evaluacion]) REFERENCES [EVALUACION] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [EVALUACION_INTEGRANTES] ADD FOREIGN KEY ([Id_grupo]) REFERENCES [EVALUACION_GRUPO] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [EVALUACION_GRUPO] ADD FOREIGN KEY ([Id_entregable]) REFERENCES [ARCHIVO_EVALUACION] ([Id])
GO

ALTER TABLE [EVALUACION_GRUPO] ADD FOREIGN KEY ([Id_detalle]) REFERENCES [ARCHIVO_EVALUACION] ([Id])
GO

CREATE UNIQUE INDEX [SEMESTRE_index_0] ON [SEMESTRE] ("Anio", "Periodo")
GO

CREATE UNIQUE INDEX [GRUPO_index_1] ON [GRUPO] ("Id_curso", "Numero", "Id_semestre")
GO

CREATE UNIQUE INDEX [NOTICIA_index_2] ON [NOTICIA] ("Id_grupo", "Titulo", "Fecha_publicacion")
GO

CREATE UNIQUE INDEX [RUBRO_index_3] ON [RUBRO] ("Id_grupo", "Nombre")
GO

CREATE UNIQUE INDEX [EVALUACION_index_4] ON [EVALUACION] ("Id_rubro", "Nombre")
GO

CREATE UNIQUE INDEX [CARPETA_index_5] ON [CARPETA] ("Id_grupo", "Nombre", "Raiz")
GO

CREATE UNIQUE INDEX [ARCHIVO_index_6] ON [ARCHIVO] ("Id_carpeta", "Nombre")
GO
