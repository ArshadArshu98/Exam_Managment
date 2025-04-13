use [ExamManagement]

DROP TABLE IF EXISTS ExamDtls;
DROP TABLE IF EXISTS ExamMaster;
DROP TABLE IF EXISTS StudentMst;
DROP TABLE IF EXISTS SubjectMst;
GO
--=====================================Tables======================================
CREATE TABLE SubjectMst (
    SubjectID INT IDENTITY(1,1) PRIMARY KEY,
    SubjectName NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE StudentMst (
    StudentID INT IDENTITY(1,1) PRIMARY KEY,
    StudentName NVARCHAR(250) NOT NULL ,
    Mail NVARCHAR(250) NOT NULL UNIQUE
);
GO
--executed
CREATE TABLE ExamMaster (
    MasterID INT IDENTITY(1,1) PRIMARY KEY,
    StudentID INT NOT NULL,
    ExamYear INT NOT NULL,
    CreateTime DATETIME NOT NULL DEFAULT GETDATE(),
    TotalMark  DECIMAL(5,2) not null,
    PassOrFail bit not null
    
    CONSTRAINT FK_ExamMaster_Student FOREIGN KEY (StudentID) REFERENCES StudentMst(StudentID),
    CONSTRAINT UQ_Student_Year UNIQUE(StudentID, ExamYear) 
);
GO

CREATE TABLE ExamDtls (
    DtlsID INT IDENTITY(1,1) PRIMARY KEY,
    MasterID INT NOT NULL,
    SubjectID INT NOT NULL,
    Marks DECIMAL(5,2) NOT NULL ,
    
    CONSTRAINT FK_ExamDtls_Master FOREIGN KEY (MasterID) REFERENCES ExamMaster(MasterID),
    CONSTRAINT FK_ExamDtls_Subject FOREIGN KEY (SubjectID) REFERENCES SubjectMst(SubjectID)
);
GO
--====================================Insert=================================
INSERT INTO SubjectMst (SubjectName)
VALUES ('Mathematics'), ('Science'), ('English'), ('History'), ('Computer');

--=============================UDTT==========================================
CREATE TYPE dbo.ExamDetailType AS TABLE
(
    SubjectID INT,
    Marks DECIMAL(5,2)
);
--========================================Stored Procedures===================
CREATE PROCEDURE sp_GetAllStudents
AS
BEGIN
    SELECT StudentID, StudentName, Mail
    FROM StudentMst;
END
GO
--====================
CREATE PROCEDURE sp_AddOrUpdateStudent 
    @StudentID INT,
    @StudentName NVARCHAR(250),
    @Mail NVARCHAR(250)
AS
BEGIN
  
    IF EXISTS (SELECT 1 FROM StudentMst WHERE StudentID = @StudentID)
    BEGIN
        UPDATE StudentMst
        SET StudentName = @StudentName,
            Mail = @Mail
        WHERE StudentID = @StudentID;
    END
    ELSE
    BEGIN
        INSERT INTO StudentMst (StudentName, Mail)
        VALUES (@StudentName, @Mail);
    END
END
GO
--===================
CREATE PROCEDURE sp_CheckEmailAlreadyExist
    @StudentID INT,
    @Mail NVARCHAR(250)
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (
        SELECT 1 FROM StudentMst
        WHERE Mail = @Mail AND StudentID != @StudentID
    )
        RETURN 1; 
    RETURN 0;
END
GO
--==========================
CREATE PROCEDURE sp_GetAllSubjects
AS
BEGIN
    SELECT SubjectID,SubjectName
    FROM SubjectMst;
END
GO
--===============
CREATE PROCEDURE dbo.	
    @StudentID INT,
    @ExamYear INT,
    @Marks dbo.ExamDetailType READONLY
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewMasterID INT;
    DECLARE @TotalMark DECIMAL(5,2);
    DECLARE @PassOrFail BIT;

    SELECT @TotalMark = SUM(Marks) FROM @Marks;

    IF NOT EXISTS (SELECT 1 FROM @Marks WHERE Marks < 25)
        SET @PassOrFail = 1; 
    ELSE
        SET @PassOrFail = 0;

    INSERT INTO ExamMaster (StudentID, ExamYear, TotalMark, PassOrFail)
    VALUES (@StudentID, @ExamYear, @TotalMark, @PassOrFail);

    SET @NewMasterID = SCOPE_IDENTITY();

    INSERT INTO ExamDtls (MasterID, SubjectID, Marks)
    SELECT @NewMasterID, SubjectID, Marks
    FROM @Marks;

    SELECT @NewMasterID AS MasterID;
END;
GO
--========================
CREATE PROCEDURE dbo.CheckExamResultExist
    @StudentID INT,
    @ExamYear INT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM ExamMaster
        WHERE StudentID = @StudentID AND ExamYear = @ExamYear
    )
        SELECT 1 AS ExistsFlag;
    ELSE
        SELECT 0 AS ExistsFlag;
END;
GO
--===================================
CREATE PROCEDURE dbo.GetExamMarks
    @StudentID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        em.MasterID,
        em.StudentID as StudentId,
        s.StudentName,
        em.ExamYear,
        em.TotalMark,
        em.PassOrFail,
        ed.SubjectID as SubjectId,
        subj.SubjectName,
        ed.Marks as Mark
    FROM ExamMaster em
    INNER JOIN StudentMst s ON em.StudentID = s.StudentID
    INNER JOIN ExamDtls ed ON em.MasterID = ed.MasterID
    INNER JOIN SubjectMst subj ON ed.SubjectID = subj.SubjectID
    WHERE (@StudentID IS NULL OR @StudentID = 0 OR em.StudentID = @StudentID)
    ORDER BY em.ExamYear DESC, em.StudentID;
END




