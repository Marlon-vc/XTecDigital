CREATE TABLE [SEMESTRE] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Anio] int NOT NULL,
  [Periodo] char(1) NOT NULL
)
GO

CREATE TABLE [CURSO] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Codigo] varchar(10) NOT NULL,
  [Nombre] varchar(100) NOT NULL,
  [Creditos] int NOT NULL,
  [Carrera] varchar(100) NOT NULL,
  [Habilitado] bit NOT NULL
)
GO

CREATE TABLE [GRUPO] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Numero] int NOT NULL,
  [Id_curso] int NOT NULL,
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
  [Fecha_publicacion] timestamp NOT NULL
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
  [Fecha_entrega] timestamp NOT NULL,
  [Peso_nota] decimal(5,2) NOT NULL,
  [Grupal] bit NOT NULL
)
GO

CREATE TABLE [EVALUACION_INTEGRANTES] (
  [Estudiante] varchar(50) NOT NULL,
  [Id_evaluacion] int NOT NULL,
  PRIMARY KEY ([Estudiante], [Id_evaluacion])
)
GO

CREATE TABLE [CARPETA] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Id_grupo] int NOT NULL,
  [Nombre] varchar(100) NOT NULL,
  [Solo_lectura] bit NOT NULL,
  [Raiz] bit NOT NULL,
  [Ruta] varchar(150) NOT NULL
)
GO

CREATE TABLE [ARCHIVO] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Id_carpeta] int NOT NULL,
  [Nombre] varchar(100) NOT NULL,
  [Fecha_creacion] timestamp NOT NULL,
  [Tamanio] decimal(8,2) NOT NULL,
  [Visible] bit NOT NULL
)
GO

CREATE TABLE [ENTREGABLE] (
  [Estudiante] varchar(50) NOT NULL,
  [Observaciones] text,
  [Id_evaluacion] int NOT NULL,
  [Id_entregable] int NOT NULL,
  [Id_detalle] int NOT NULL,
  PRIMARY KEY ([Estudiante], [Id_evaluacion])
)
GO

ALTER TABLE [GRUPO] ADD FOREIGN KEY ([Id_curso]) REFERENCES [CURSO] ([Id])
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

ALTER TABLE [EVALUACION_INTEGRANTES] ADD FOREIGN KEY ([Id_evaluacion]) REFERENCES [EVALUACION] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [CARPETA] ADD FOREIGN KEY ([Id_grupo]) REFERENCES [GRUPO] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [ARCHIVO] ADD FOREIGN KEY ([Id_carpeta]) REFERENCES [CARPETA] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [EVALUACION] ADD FOREIGN KEY ([Id_especificacion]) REFERENCES [ARCHIVO] ([Id])
GO

ALTER TABLE [ENTREGABLE] ADD FOREIGN KEY ([Id_evaluacion]) REFERENCES [EVALUACION] ([Id]) ON DELETE CASCADE
GO

CREATE UNIQUE INDEX [SEMESTRE_index_0] ON [SEMESTRE] ("Anio", "Periodo")
GO

CREATE UNIQUE INDEX [CURSO_index_1] ON [CURSO] ("Codigo")
GO

CREATE UNIQUE INDEX [GRUPO_index_2] ON [GRUPO] ("Id_curso", "Id_semestre")
GO

CREATE UNIQUE INDEX [NOTICIA_index_3] ON [NOTICIA] ("Id_grupo", "Titulo", "Fecha_publicacion")
GO

CREATE UNIQUE INDEX [RUBRO_index_4] ON [RUBRO] ("Id_grupo", "Nombre")
GO

CREATE UNIQUE INDEX [EVALUACION_index_5] ON [EVALUACION] ("Id_rubro", "Nombre")
GO

CREATE UNIQUE INDEX [CARPETA_index_6] ON [CARPETA] ("Id_grupo", "Nombre")
GO

CREATE UNIQUE INDEX [ARCHIVO_index_7] ON [ARCHIVO] ("Id_carpeta", "Nombre")
GO
