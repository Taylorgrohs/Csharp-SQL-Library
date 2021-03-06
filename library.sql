create database library
go
USE [library]
GO
/****** Object:  Table [dbo].[authors]    Script Date: 3/2/2016 4:38:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[authors](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[book_author]    Script Date: 3/2/2016 4:38:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[book_author](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[book_id] [int] NULL,
	[author_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[books]    Script Date: 3/2/2016 4:38:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[books](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
