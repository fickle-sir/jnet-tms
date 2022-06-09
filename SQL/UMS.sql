IF EXISTS (SELECT 1  FROM  sysobjects  where  id = object_id('U_Enterprise') AND type = 'U')
   DROP TABLE U_Enterprise
GO

CREATE TABLE U_Enterprise (
   ID            int           NOT NULL IDENTITY(1,1),
   UID           int           NOT NULL,
   Name          nvarchar(50)  NOT NULL,
   Contact       nvarchar(20)  NOT NULL,
   Tel           varchar(12)   NOT NULL,
   Districts     varchar(20)   NOT NULL,
   Address       nvarchar(50)  NOT NULL,
   CreatedTime   datetime      NOT NULL,

   CONSTRAINT PK_Enterprise PRIMARY KEY CLUSTERED(ID)
)
GO

IF EXISTS (SELECT 1  FROM  sysobjects  where  id = object_id('C_District') AND type = 'U')
   DROP TABLE C_District
GO

CREATE TABLE C_District (
   ID            int           NOT NULL,
   PID           int           NOT NULL,
   Name          nvarchar(50)  NOT NULL,
   CONSTRAINT PK_District PRIMARY KEY CLUSTERED(ID)
)
GO