this script of sql

USE [StatusFlow]
GO

/****** Object:  Table [dbo].[Statuses]    Script Date: 29/08/2024 12:17:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Statuses](
	[StatusID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[IsInitial] [bit] NULL,
	[IsOrphan] [bit] NULL,
	[IsFinal] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[StatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Statuses] ADD  DEFAULT ((0)) FOR [IsInitial]
GO

ALTER TABLE [dbo].[Statuses] ADD  DEFAULT ((0)) FOR [IsOrphan]
GO

ALTER TABLE [dbo].[Statuses] ADD  DEFAULT ((0)) FOR [IsFinal]
GO
USE [StatusFlow]
GO

/****** Object:  Table [dbo].[Transitions]    Script Date: 29/08/2024 12:39:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Transitions](
	[TransitionID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[FromStatusID] [int] NOT NULL,
	[ToStatusID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TransitionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Transitions]  WITH CHECK ADD FOREIGN KEY([FromStatusID])
REFERENCES [dbo].[Statuses] ([StatusID])
GO

ALTER TABLE [dbo].[Transitions]  WITH CHECK ADD FOREIGN KEY([ToStatusID])
REFERENCES [dbo].[Statuses] ([StatusID])
GO




