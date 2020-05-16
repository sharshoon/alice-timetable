CREATE TABLE [dbo].[Users] (
    [ID]                  NVARCHAR (450) NOT NULL,
    [Name]                NVARCHAR (MAX) NULL,
    [Group]               NVARCHAR (MAX) NULL,
    [DisplaySubjectType]  BIT            NOT NULL,
    [DisplaySubjectTime]  BIT            NOT NULL,
    [DisplayEmployeeName] BIT            NOT NULL,
    [DisplayAuditory]     BIT            NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([ID] ASC)
);

