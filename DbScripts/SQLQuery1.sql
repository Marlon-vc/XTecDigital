INSERT INTO dbo.PERIODO (Nombre)
VALUES ('1'), ('2'), ('V')

SELECT * FROM dbo.PERIODO

SELECT * 
FROM dbo.SEMESTRE
JOIN dbo.CURSO_SEMESTRE ON dbo.SEMESTRE.Id = dbo.CURSO_SEMESTRE.Id_semestre
JOIN dbo.CURSO ON dbo.CURSO_SEMESTRE.Id_curso = dbo.CURSO.Codigo
JOIN dbo.GRUPO ON dbo.GRUPO.Id_curso = dbo.CURSO.Codigo
JOIN dbo.RUBRO ON dbo.RUBRO.Id_grupo = dbo.GRUPO.Id
JOIN dbo.GRUPO_ESTUDIANTE ON dbo.GRUPO_ESTUDIANTE.Numero_Grupo = dbo.GRUPO.Id
JOIN dbo.GRUPO_PROFESOR ON dbo.GRUPO_PROFESOR.Numero_Grupo = dbo.GRUPO.Id;