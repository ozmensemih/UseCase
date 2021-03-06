USE [master]
GO
/****** Object:  Database [UseCase]    Script Date: 16.03.2021 20:03:51 ******/
CREATE DATABASE [UseCase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'UseCase', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\UseCase.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'UseCase_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\UseCase_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [UseCase] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [UseCase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [UseCase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [UseCase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [UseCase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [UseCase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [UseCase] SET ARITHABORT OFF 
GO
ALTER DATABASE [UseCase] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [UseCase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [UseCase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [UseCase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [UseCase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [UseCase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [UseCase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [UseCase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [UseCase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [UseCase] SET  ENABLE_BROKER 
GO
ALTER DATABASE [UseCase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [UseCase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [UseCase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [UseCase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [UseCase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [UseCase] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [UseCase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [UseCase] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [UseCase] SET  MULTI_USER 
GO
ALTER DATABASE [UseCase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [UseCase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [UseCase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [UseCase] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [UseCase] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [UseCase] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [UseCase] SET QUERY_STORE = OFF
GO
USE [UseCase]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 16.03.2021 20:03:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoices]    Script Date: 16.03.2021 20:03:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoices](
	[Id] [uniqueidentifier] NOT NULL,
	[DateCreated] [datetime2](7) NULL,
	[UserCreatedId] [uniqueidentifier] NULL,
	[DateModified] [datetime2](7) NULL,
	[UserModifiedId] [uniqueidentifier] NULL,
	[ConcurrencyStamp] [timestamp] NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[CategoryId] [uniqueidentifier] NULL,
	[InvoiceName] [nvarchar](max) NULL,
	[InvoicePrice] [decimal](18, 2) NOT NULL,
	[InvoiceDate] [datetime2](7) NOT NULL,
	[InvoiceExpiryDate] [datetime2](7) NULL,
	[PaymentDate] [datetime2](7) NULL,
	[PaymentStatus] [bit] NOT NULL,
 CONSTRAINT [PK_Invoices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleClaims]    Script Date: 16.03.2021 20:03:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_RoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 16.03.2021 20:03:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[Discriminator] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserClaims]    Script Date: 16.03.2021 20:03:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLogins]    Script Date: 16.03.2021 20:03:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UserLogins] PRIMARY KEY CLUSTERED 
(
	[ProviderKey] ASC,
	[LoginProvider] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 16.03.2021 20:03:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 16.03.2021 20:03:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[Discriminator] [nvarchar](max) NOT NULL,
	[Deposit] [decimal](18, 2) NULL,
	[SubscriptionStartDate] [datetime2](7) NULL,
	[SubscriptionEndDate] [datetime2](7) NULL,
	[SubscriptionType] [int] NULL,
	[TaxNumber] [nvarchar](max) NULL,
	[IdentityNumber] [nvarchar](max) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserTokens]    Script Date: 16.03.2021 20:03:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTokens](
	[UserId] [uniqueidentifier] NOT NULL,
	[LoginProvider] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210313182458_init', N'3.1.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210316063655_init3', N'3.1.13')
GO
INSERT [dbo].[Invoices] ([Id], [DateCreated], [UserCreatedId], [DateModified], [UserModifiedId], [UserId], [CategoryId], [InvoiceName], [InvoicePrice], [InvoiceDate], [InvoiceExpiryDate], [PaymentDate], [PaymentStatus]) VALUES (N'15e7f700-7c3b-4837-b119-00fd4052c331', NULL, NULL, NULL, NULL, N'a945f417-f365-4ad0-4d96-08d8e849987f', NULL, N'MART 2021', CAST(50.00 AS Decimal(18, 2)), CAST(N'2021-03-30T23:59:59.0000000' AS DateTime2), NULL, NULL, 0)
INSERT [dbo].[Invoices] ([Id], [DateCreated], [UserCreatedId], [DateModified], [UserModifiedId], [UserId], [CategoryId], [InvoiceName], [InvoicePrice], [InvoiceDate], [InvoiceExpiryDate], [PaymentDate], [PaymentStatus]) VALUES (N'e5457726-2839-4060-b486-2d54dc367034', NULL, NULL, NULL, NULL, N'e40ce8e8-1bef-4b5e-1ae4-08d8e847f5a8', NULL, N'ARALIK 2020', CAST(50.00 AS Decimal(18, 2)), CAST(N'2020-12-31T23:59:59.0000000' AS DateTime2), CAST(N'2021-01-20T23:59:59.0000000' AS DateTime2), CAST(N'2021-03-16T18:32:00.8375709' AS DateTime2), 1)
INSERT [dbo].[Invoices] ([Id], [DateCreated], [UserCreatedId], [DateModified], [UserModifiedId], [UserId], [CategoryId], [InvoiceName], [InvoicePrice], [InvoiceDate], [InvoiceExpiryDate], [PaymentDate], [PaymentStatus]) VALUES (N'e8234e5a-9dc3-4eb2-906a-34ee4e714e5d', NULL, NULL, NULL, NULL, N'e40ce8e8-1bef-4b5e-1ae4-08d8e847f5a8', NULL, N'ŞUBAT 2021', CAST(50.00 AS Decimal(18, 2)), CAST(N'2021-02-28T23:59:59.0000000' AS DateTime2), CAST(N'2021-03-20T23:59:59.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Invoices] ([Id], [DateCreated], [UserCreatedId], [DateModified], [UserModifiedId], [UserId], [CategoryId], [InvoiceName], [InvoicePrice], [InvoiceDate], [InvoiceExpiryDate], [PaymentDate], [PaymentStatus]) VALUES (N'02830ed8-3c6d-4fdf-8d58-39d8e58d57ec', NULL, NULL, NULL, NULL, N'f369838d-2df8-43bc-1ae3-08d8e847f5a8', NULL, N'MART 2021', CAST(50.00 AS Decimal(18, 2)), CAST(N'2021-03-30T23:59:59.0000000' AS DateTime2), NULL, NULL, 0)
INSERT [dbo].[Invoices] ([Id], [DateCreated], [UserCreatedId], [DateModified], [UserModifiedId], [UserId], [CategoryId], [InvoiceName], [InvoicePrice], [InvoiceDate], [InvoiceExpiryDate], [PaymentDate], [PaymentStatus]) VALUES (N'152a853f-0294-49fc-8b6c-930ee5c88996', NULL, NULL, NULL, NULL, N'f369838d-2df8-43bc-1ae3-08d8e847f5a8', NULL, N'OCAK 2021', CAST(50.00 AS Decimal(18, 2)), CAST(N'2021-01-31T23:59:59.0000000' AS DateTime2), CAST(N'2021-02-20T23:59:59.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Invoices] ([Id], [DateCreated], [UserCreatedId], [DateModified], [UserModifiedId], [UserId], [CategoryId], [InvoiceName], [InvoicePrice], [InvoiceDate], [InvoiceExpiryDate], [PaymentDate], [PaymentStatus]) VALUES (N'8b754e13-1b6b-49b9-8073-a2f102600e57', NULL, NULL, NULL, NULL, N'e40ce8e8-1bef-4b5e-1ae4-08d8e847f5a8', NULL, N'OCAK 2021', CAST(50.00 AS Decimal(18, 2)), CAST(N'2021-01-31T23:59:59.0000000' AS DateTime2), CAST(N'2021-02-20T23:59:59.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Invoices] ([Id], [DateCreated], [UserCreatedId], [DateModified], [UserModifiedId], [UserId], [CategoryId], [InvoiceName], [InvoicePrice], [InvoiceDate], [InvoiceExpiryDate], [PaymentDate], [PaymentStatus]) VALUES (N'933e09f2-53ce-42f6-b0db-abede03f785a', NULL, NULL, NULL, NULL, N'a945f417-f365-4ad0-4d96-08d8e849987f', NULL, N'ŞUBAT 2021', CAST(50.00 AS Decimal(18, 2)), CAST(N'2021-02-28T23:59:59.0000000' AS DateTime2), CAST(N'2021-03-20T23:59:59.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Invoices] ([Id], [DateCreated], [UserCreatedId], [DateModified], [UserModifiedId], [UserId], [CategoryId], [InvoiceName], [InvoicePrice], [InvoiceDate], [InvoiceExpiryDate], [PaymentDate], [PaymentStatus]) VALUES (N'37c1f56d-c8a4-4b24-883c-b9cd61f5b47f', NULL, NULL, NULL, NULL, N'e40ce8e8-1bef-4b5e-1ae4-08d8e847f5a8', NULL, N'MART 2021', CAST(50.00 AS Decimal(18, 2)), CAST(N'2021-03-30T23:59:59.0000000' AS DateTime2), NULL, NULL, 0)
INSERT [dbo].[Invoices] ([Id], [DateCreated], [UserCreatedId], [DateModified], [UserModifiedId], [UserId], [CategoryId], [InvoiceName], [InvoicePrice], [InvoiceDate], [InvoiceExpiryDate], [PaymentDate], [PaymentStatus]) VALUES (N'e0ca7de8-4d25-41c8-9e81-c0cb755eb4cf', NULL, NULL, NULL, NULL, N'f369838d-2df8-43bc-1ae3-08d8e847f5a8', NULL, N'ŞUBAT 2021', CAST(50.00 AS Decimal(18, 2)), CAST(N'2021-02-28T23:59:59.0000000' AS DateTime2), CAST(N'2021-03-20T23:59:59.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Invoices] ([Id], [DateCreated], [UserCreatedId], [DateModified], [UserModifiedId], [UserId], [CategoryId], [InvoiceName], [InvoicePrice], [InvoiceDate], [InvoiceExpiryDate], [PaymentDate], [PaymentStatus]) VALUES (N'313b6334-c19d-481c-bae3-e45745cdcd45', NULL, NULL, NULL, NULL, N'f369838d-2df8-43bc-1ae3-08d8e847f5a8', NULL, N'ARALIK 2020', CAST(50.00 AS Decimal(18, 2)), CAST(N'2020-12-31T23:59:59.0000000' AS DateTime2), CAST(N'2021-01-20T23:59:59.0000000' AS DateTime2), CAST(N'2021-03-16T18:34:02.0986154' AS DateTime2), 1)
GO
INSERT [dbo].[Roles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp], [Discriminator]) VALUES (N'5c11b9fd-0fc5-4820-9154-08d8e847f5ca', N'Cashier', N'CASHIER', N'e7889078-db37-4d8a-8036-e0fa18958f5c', N'ApiRole')
INSERT [dbo].[Roles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp], [Discriminator]) VALUES (N'923c35e8-e9e0-436d-9155-08d8e847f5ca', N'Customer', N'CUSTOMER', N'76d0c31f-b97e-4458-9457-eb2e2a953599', N'ApiRole')
INSERT [dbo].[Roles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp], [Discriminator]) VALUES (N'c9a3ab95-2bb8-4ef7-9156-08d8e847f5ca', N'Corporation', N'CORPORATION', N'd4dc5d32-46a6-4c52-b83a-f8d0225738b6', N'ApiRole')
GO
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'a0734fcb-13ea-4d4a-1ae2-08d8e847f5a8', N'5c11b9fd-0fc5-4820-9154-08d8e847f5ca')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'f369838d-2df8-43bc-1ae3-08d8e847f5a8', N'5c11b9fd-0fc5-4820-9154-08d8e847f5ca')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'a945f417-f365-4ad0-4d96-08d8e849987f', N'c9a3ab95-2bb8-4ef7-9156-08d8e847f5ca')
GO
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [LastName], [Address], [IsActive], [Discriminator], [Deposit], [SubscriptionStartDate], [SubscriptionEndDate], [SubscriptionType], [TaxNumber], [IdentityNumber]) VALUES (N'a0734fcb-13ea-4d4a-1ae2-08d8e847f5a8', N'semihcashier', N'SEMIHCASHIER', NULL, NULL, 0, N'AQAAAAEAACcQAAAAEHFID04bSM4hCYOvtkseG3k4j8FUfF04/GwEDWYHwCn7/LBMIF19C32oUtKhp4ZOWg==', N'F36GDVELMIKKLIB3C2SHAFRUQV7DWPAE', N'e055e414-067f-48d3-9b4e-b86504a36e31', NULL, 0, 0, NULL, 1, 0, N'Semih', N'Özmen', N'Ataşehir/İstanbul', 1, N'Cashier', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [LastName], [Address], [IsActive], [Discriminator], [Deposit], [SubscriptionStartDate], [SubscriptionEndDate], [SubscriptionType], [TaxNumber], [IdentityNumber]) VALUES (N'f369838d-2df8-43bc-1ae3-08d8e847f5a8', N'semihcustomer1', N'SEMIHCUSTOMER1', NULL, NULL, 0, N'AQAAAAEAACcQAAAAECRLzzTxDUdxmkWm5itzKRNQOFLO7vPK+MtmkbmmB/Kx/qKR2QKGsjU7TX1N4FRjOQ==', N'4DGZN3HN3AD4IOZQ2SPUJ3VAESSZX6JM', N'2bee2aa7-5c88-4838-9b4a-18533227bebb', NULL, 0, 0, NULL, 1, 0, N'Semih', N'Özmen', N'Esenler', 1, N'Customer', CAST(100.00 AS Decimal(18, 2)), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2021-03-16T14:46:40.9410153' AS DateTime2), 1, NULL, N'12345678999')
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [LastName], [Address], [IsActive], [Discriminator], [Deposit], [SubscriptionStartDate], [SubscriptionEndDate], [SubscriptionType], [TaxNumber], [IdentityNumber]) VALUES (N'e40ce8e8-1bef-4b5e-1ae4-08d8e847f5a8', N'semihcorporation', N'SEMIHCORPORATION', NULL, NULL, 0, N'AQAAAAEAACcQAAAAEL5cguSuuuLm3VgJnHa5ftx9GnhTcwPlMgQEppVbvYglQNuxfT0/XxX3j9XBy0Xy5w==', N'UFKGVRYD2QT6WBT2253472NVXURP4QP4', N'c19e1cf3-22cb-440e-a0ec-2854041f60c9', NULL, 0, 0, NULL, 1, 0, N'Semih', N'Corporation', N'Beykoz', 1, N'Corporation', CAST(100.00 AS Decimal(18, 2)), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 2, N'9114175381', NULL)
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [LastName], [Address], [IsActive], [Discriminator], [Deposit], [SubscriptionStartDate], [SubscriptionEndDate], [SubscriptionType], [TaxNumber], [IdentityNumber]) VALUES (N'a945f417-f365-4ad0-4d96-08d8e849987f', N'semih2corporation', N'SEMIH2CORPORATION', NULL, NULL, 0, N'AQAAAAEAACcQAAAAELIK72mn+0uzAr2PMnfGnau23jlcbMW5tsfAlCHO6JkoQyT0uahe2LIddUFGyK7Blg==', N'5EOMNJD3PHBMYXFE5NS3S4O3S7RJR2VM', N'b0c58b1d-e0f1-4d2e-8f5e-bb7b5bf66b40', NULL, 0, 0, NULL, 1, 0, N'Semih2', N'Cor2', N'Kartal', 1, N'Corporation', CAST(100.00 AS Decimal(18, 2)), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 2, N'7354524251', NULL)
GO
/****** Object:  Index [IX_Invoice_UserId]    Script Date: 16.03.2021 20:03:51 ******/
CREATE NONCLUSTERED INDEX [IX_Invoice_UserId] ON [dbo].[Invoices]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Invoices_UserCreatedId]    Script Date: 16.03.2021 20:03:51 ******/
CREATE NONCLUSTERED INDEX [IX_Invoices_UserCreatedId] ON [dbo].[Invoices]
(
	[UserCreatedId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Invoices_UserModifiedId]    Script Date: 16.03.2021 20:03:51 ******/
CREATE NONCLUSTERED INDEX [IX_Invoices_UserModifiedId] ON [dbo].[Invoices]
(
	[UserModifiedId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_RoleClaims_RoleId]    Script Date: 16.03.2021 20:03:51 ******/
CREATE NONCLUSTERED INDEX [IX_RoleClaims_RoleId] ON [dbo].[RoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 16.03.2021 20:03:51 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[Roles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserClaims_UserId]    Script Date: 16.03.2021 20:03:51 ******/
CREATE NONCLUSTERED INDEX [IX_UserClaims_UserId] ON [dbo].[UserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserLogins_UserId]    Script Date: 16.03.2021 20:03:51 ******/
CREATE NONCLUSTERED INDEX [IX_UserLogins_UserId] ON [dbo].[UserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserRoles_UserId]    Script Date: 16.03.2021 20:03:51 ******/
CREATE NONCLUSTERED INDEX [IX_UserRoles_UserId] ON [dbo].[UserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 16.03.2021 20:03:51 ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[Users]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 16.03.2021 20:03:51 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[Users]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Invoices]  WITH CHECK ADD  CONSTRAINT [FK_Invoices_Users_UserCreatedId] FOREIGN KEY([UserCreatedId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Invoices] CHECK CONSTRAINT [FK_Invoices_Users_UserCreatedId]
GO
ALTER TABLE [dbo].[Invoices]  WITH CHECK ADD  CONSTRAINT [FK_Invoices_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Invoices] CHECK CONSTRAINT [FK_Invoices_Users_UserId]
GO
ALTER TABLE [dbo].[Invoices]  WITH CHECK ADD  CONSTRAINT [FK_Invoices_Users_UserModifiedId] FOREIGN KEY([UserModifiedId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Invoices] CHECK CONSTRAINT [FK_Invoices_Users_UserModifiedId]
GO
ALTER TABLE [dbo].[RoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_RoleClaims_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleClaims] CHECK CONSTRAINT [FK_RoleClaims_Roles_RoleId]
GO
ALTER TABLE [dbo].[UserClaims]  WITH CHECK ADD  CONSTRAINT [FK_UserClaims_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserClaims] CHECK CONSTRAINT [FK_UserClaims_Users_UserId]
GO
ALTER TABLE [dbo].[UserLogins]  WITH CHECK ADD  CONSTRAINT [FK_UserLogins_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserLogins] CHECK CONSTRAINT [FK_UserLogins_Users_UserId]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Roles_RoleId]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users_UserId]
GO
ALTER TABLE [dbo].[UserTokens]  WITH CHECK ADD  CONSTRAINT [FK_UserTokens_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserTokens] CHECK CONSTRAINT [FK_UserTokens_Users_UserId]
GO
USE [master]
GO
ALTER DATABASE [UseCase] SET  READ_WRITE 
GO
