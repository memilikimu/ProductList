USE [ProductList]
GO
/****** Object:  Table [dbo].[Sale]    Script Date: 02/05/2025 01:03:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sale](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Product] [nvarchar](50) NOT NULL,
	[Price] [int] NOT NULL,
	[Amount] [int] NOT NULL,
	[SellPrice] [int] NOT NULL,
	[SellAmount] [int] NOT NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Sale] ON 
GO
INSERT [dbo].[Sale] ([Id], [Product], [Price], [Amount], [SellPrice], [SellAmount]) VALUES (1, N'Produk1', 50000, 20, 60000, 19)
GO
INSERT [dbo].[Sale] ([Id], [Product], [Price], [Amount], [SellPrice], [SellAmount]) VALUES (2, N'Produk2', 30000, 20, 35000, 19)
GO
INSERT [dbo].[Sale] ([Id], [Product], [Price], [Amount], [SellPrice], [SellAmount]) VALUES (3, N'Produk3', 35000, 8, 50000, 8)
GO
INSERT [dbo].[Sale] ([Id], [Product], [Price], [Amount], [SellPrice], [SellAmount]) VALUES (4, N'Produk4', 90000, 30, 150000, 26)
GO
INSERT [dbo].[Sale] ([Id], [Product], [Price], [Amount], [SellPrice], [SellAmount]) VALUES (6, N'Produk6', 40000, 90, 60000, 80)
GO
INSERT [dbo].[Sale] ([Id], [Product], [Price], [Amount], [SellPrice], [SellAmount]) VALUES (10, N'Produk10', 9000, 7, 12000, 6)
GO
INSERT [dbo].[Sale] ([Id], [Product], [Price], [Amount], [SellPrice], [SellAmount]) VALUES (5, N'Produk5', 70000, 8, 90000, 7)
GO
INSERT [dbo].[Sale] ([Id], [Product], [Price], [Amount], [SellPrice], [SellAmount]) VALUES (7, N'Produk7', 56000, 40, 70000, 30)
GO
INSERT [dbo].[Sale] ([Id], [Product], [Price], [Amount], [SellPrice], [SellAmount]) VALUES (8, N'Produk8', 94000, 60, 120000, 60)
GO
INSERT [dbo].[Sale] ([Id], [Product], [Price], [Amount], [SellPrice], [SellAmount]) VALUES (9, N'Produk9', 40000, 120, 80000, 100)
GO
INSERT [dbo].[Sale] ([Id], [Product], [Price], [Amount], [SellPrice], [SellAmount]) VALUES (11, N'Produk11', 80000, 90, 90000, 85)
GO
SET IDENTITY_INSERT [dbo].[Sale] OFF
GO
