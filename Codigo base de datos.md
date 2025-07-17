Codigo de base de datos, en caso de ser necesario su uso
CREATE DATABASE ProcesamientoPDF;

CREATE TABLE DocKey (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Clave NVARCHAR(100),
    DocName NVARCHAR(255) 
);

CREATE TABLE LogProcesamiento (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    NombreOriginal NVARCHAR(255),      
    NuevoNombre NVARCHAR(255),                  
    Estado NVARCHAR(50),               
    FechaProcesamiento DATETIME DEFAULT GETDATE()
);

CREATE PROCEDURE sp_InsertarLogProcesamiento
    @NombreOriginal NVARCHAR(255),
    @NuevoNombre NVARCHAR(255),
    @Estado NVARCHAR(50)
AS
BEGIN
    INSERT INTO LogProcesamiento (NombreOriginal, NuevoNombre, Estado)
    VALUES (@NombreOriginal, @NuevoNombre, @Estado)
END;

CREATE PROCEDURE sp_ObtenerLogs
AS
BEGIN
    SELECT * FROM LogProcesamiento ORDER BY FechaProcesamiento DESC;
END;

CREATE PROCEDURE sp_InsertarDocKey
    @Clave NVARCHAR(100),
    @DocName NVARCHAR(255)
AS
BEGIN
    INSERT INTO DocKey (Clave, DocName) VALUES (@Clave, @DocName);
END;

CREATE PROCEDURE sp_ActualizarDocKey
    @Id INT,
    @Clave NVARCHAR(100),
    @DocName NVARCHAR(255)
AS
BEGIN
    UPDATE DocKey
    SET Clave = @Clave,
        DocName = @DocName
    WHERE Id = @Id;
END;

CREATE PROCEDURE sp_EliminarDocKey
    @Id INT
AS
BEGIN
    DELETE FROM DocKey WHERE Id = @Id;
END;

CREATE PROCEDURE sp_ObtenerDocKeys
AS
BEGIN
    SELECT * FROM DocKey ORDER BY Id;
END;
