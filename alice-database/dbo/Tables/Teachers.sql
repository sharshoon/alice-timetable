CREATE TABLE [dbo].[Teachers] (
    [TeacherId]          INT            IDENTITY (1, 1) NOT NULL,
    [firstName]          NVARCHAR (MAX) NULL,
    [lastName]           NVARCHAR (MAX) NULL,
    [middleName]         NVARCHAR (MAX) NULL,
    [rank]               NVARCHAR (MAX) NULL,
    [photoLink]          NVARCHAR (MAX) NULL,
    [calendarId]         NVARCHAR (MAX) NULL,
    [academicDepartment] NVARCHAR (MAX) NULL,
    [id]                 INT            NOT NULL,
    [fio]                NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Teachers] PRIMARY KEY CLUSTERED ([TeacherId] ASC)
);

