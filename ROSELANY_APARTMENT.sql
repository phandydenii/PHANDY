USE [master]
GO
/****** Object:  Database [ROSELANY_APARTMENT]    Script Date: 4/10/2023 7:50:21 AM ******/
CREATE DATABASE [ROSELANY_APARTMENT]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ROSELANY_APARTMENT', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\ROSELANY_APARTMENT.mdf' , SIZE = 4160KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ROSELANY_APARTMENT_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\ROSELANY_APARTMENT_log.ldf' , SIZE = 1600KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ROSELANY_APARTMENT] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ROSELANY_APARTMENT].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ROSELANY_APARTMENT] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ROSELANY_APARTMENT] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ROSELANY_APARTMENT] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ROSELANY_APARTMENT] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ROSELANY_APARTMENT] SET ARITHABORT OFF 
GO
ALTER DATABASE [ROSELANY_APARTMENT] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ROSELANY_APARTMENT] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [ROSELANY_APARTMENT] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ROSELANY_APARTMENT] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ROSELANY_APARTMENT] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ROSELANY_APARTMENT] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ROSELANY_APARTMENT] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ROSELANY_APARTMENT] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ROSELANY_APARTMENT] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ROSELANY_APARTMENT] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ROSELANY_APARTMENT] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ROSELANY_APARTMENT] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ROSELANY_APARTMENT] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ROSELANY_APARTMENT] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ROSELANY_APARTMENT] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ROSELANY_APARTMENT] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ROSELANY_APARTMENT] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ROSELANY_APARTMENT] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ROSELANY_APARTMENT] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ROSELANY_APARTMENT] SET  MULTI_USER 
GO
ALTER DATABASE [ROSELANY_APARTMENT] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ROSELANY_APARTMENT] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ROSELANY_APARTMENT] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ROSELANY_APARTMENT] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
USE [ROSELANY_APARTMENT]
GO
/****** Object:  StoredProcedure [dbo].[INSERT_ELECTRIC]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[INSERT_ELECTRIC]
(
	@checkinid int,
	@predate date,
	@prerecord decimal,
	@curredate date,
	@currrecord decimal,
	@price int
)
AS 
BEGIN

	insert into electricusage_tbl values(@checkinid,@predate,@prerecord,@curredate,@currrecord,@price)
END

GO
/****** Object:  StoredProcedure [dbo].[INSERT_GUEST]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[INSERT_GUEST]
(
	@name varchar(50),
	@namekh	nvarchar(50),
	@sex varchar (10),
	@dob date,
	@address nvarchar(100),
	@nationality nvarchar(50),
	@phone varchar(50),
	@email varchar (50),
	@ssn varchar(50),
	@passport varchar(50),
	@status varchar(20)
)
as
begin 
	insert into guest_tbl values(@name,@namekh,@sex,@dob,@address,@nationality,@phone,@email,@ssn,@passport,@status)
end

GO
/****** Object:  StoredProcedure [dbo].[INSERT_WATER]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[INSERT_WATER]
(
	@checkinid int,
	@predate date,
	@prerecord decimal,
	@curredate date,
	@currrecord decimal,
	@price int
)
AS 
BEGIN

	insert into waterusage_tbl values(@checkinid,@predate,@prerecord,@curredate,@currrecord,@price)
END

GO
/****** Object:  StoredProcedure [dbo].[InsertWaterElectric]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE proc [dbo].[InsertWaterElectric]
(
	@checkinid int,
	@startdate date,
	@enddate date,
	@wstartrecord decimal(16,2),
	@wendrecord decimal (16,2),
	@estartrecord decimal (16,2),
	@eendrecord decimal (16,2),
	@weprice int
)
as 
begin 
	insert into waterelectricusage_tbl values(@checkinid,@startdate,@enddate,@wstartrecord,@wendrecord,@estartrecord,@eendrecord,@weprice);
end

GO
/****** Object:  StoredProcedure [dbo].[UPDATE_CHECKIN]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[UPDATE_CHECKIN]
(
	@id int,
	@startdate date,
	@enddate date,
	@child int,
	@man int,
	@women int
)
as 
begin

update checkin_tbl set 
	startdate=@startdate,
	enddate=@enddate,
	child=@child,
	man=@man,
	women=@women 
where id=@id


end

GO
/****** Object:  StoredProcedure [dbo].[UPDATE_INVOICE]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







CREATE proc [dbo].[UPDATE_INVOICE]
(
	@id int,
	@invoicedate date,
	@grandtotal decimal,
	@totaldollar decimal,
	@totalriel decimal,
	@totalother decimal,
	@updateby varchar(50),
	@updatedate date,
	@note nvarchar(100),
	@status varchar(50),
	@paid bit,
	@payriel decimal,
	@paydollar decimal
)
as
begin 
	update invoice_tbl set 
			invoicedate=@invoicedate,
			grandtotal=@grandtotal,
			totaldollar=@totaldollar,
			totalriel=@totalriel,totalother=@totalother,
			printed=1,
			paid=@paid,
			updateby=@updateby,
			updatedate=@updatedate,
			payriel=@payriel,
			paydollar=@paydollar,
			note=@note,
			status=@status	
			where id=@id
end

GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[BrandId] [int] NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[Sex] [nvarchar](50) NULL,
	[IsDeleted] [bit] NULL,
	[FullName] [nvarchar](50) NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[booking_tbl]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[booking_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[bookingdate] [date] NULL,
	[userid] [varchar](50) NULL,
	[guestid] [int] NULL,
	[roomid] [int] NULL,
	[exchangeid] [int] NULL,
	[total] [decimal](18, 2) NULL,
	[paydollar] [decimal](18, 2) NULL,
	[payriel] [decimal](18, 2) NULL,
	[updateby] [varchar](50) NULL,
	[updatedate] [date] NULL,
	[checkindate] [date] NULL,
	[expiredate] [date] NULL,
	[note] [nvarchar](100) NULL,
	[status] [varchar](50) NULL,
 CONSTRAINT [PK__booking___3213E83FC214ECF1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Branches]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Branches](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[create_date] [datetime] NOT NULL,
	[create_by] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Branches] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[building_tbl]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[building_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[buildingname] [varchar](50) NULL,
	[buildingnamekh] [nvarchar](100) NULL,
 CONSTRAINT [PK_builging_tbl] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[checkin_tbl]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[checkin_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[checkindate] [date] NULL,
	[roomid] [int] NULL,
	[userid] [varchar](50) NULL,
	[guestid] [int] NULL,
	[child] [int] NULL,
	[man] [int] NULL,
	[women] [int] NULL,
	[startdate] [date] NULL,
	[enddate] [date] NULL,
	[payforroom] [decimal](18, 2) NULL,
	[prepaid] [decimal](18, 2) NULL,
	[paydollar] [decimal](18, 2) NULL,
	[payriel] [decimal](18, 2) NULL,
	[active] [bit] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[checkout_tbl]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[checkout_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[date] [date] NULL,
	[guestid] [int] NULL,
	[roomid] [int] NULL,
	[weusageid] [int] NULL,
	[exchangeid] [int] NULL,
	[userid] [nvarchar](100) NULL,
	[totalroomprice] [decimal](18, 2) NULL,
	[paybefor] [decimal](18, 2) NULL,
	[total] [decimal](18, 2) NULL,
	[returnamount] [decimal](18, 2) NULL,
	[totalpayment] [decimal](18, 2) NULL,
	[paydollar] [decimal](18, 2) NULL,
	[payriel] [decimal](18, 2) NULL,
	[description] [nvarchar](100) NULL,
 CONSTRAINT [PK_checkout_tbl] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[checkoutdetail_tbl]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[checkoutdetail_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[checkoutid] [int] NULL,
	[paydemageid] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ExchangeRates]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExchangeRates](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[date] [date] NULL,
	[rate] [decimal](18, 3) NULL,
	[status] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_ExchangeRates] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[expensetype_tbl]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[expensetype_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[typename] [nvarchar](50) NULL,
 CONSTRAINT [PK_expensetype_tbl] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[floor_tbl]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[floor_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[buildingid] [int] NULL,
	[floor_no] [nvarchar](50) NULL,
	[status] [nvarchar](50) NULL,
 CONSTRAINT [PK_floor_tbl] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[guest_tbl]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[guest_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NULL,
	[namekh] [nvarchar](50) NULL,
	[sex] [varchar](50) NULL,
	[dob] [date] NULL,
	[address] [nvarchar](50) NULL,
	[nationality] [nvarchar](50) NULL,
	[phone] [varchar](50) NULL,
	[email] [varchar](50) NULL,
	[ssn] [varchar](50) NULL,
	[passport] [varchar](50) NULL,
	[status] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[invoice_tbl]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[invoice_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[invoicedate] [date] NULL,
	[roomid] [int] NULL,
	[weusageid] [int] NULL,
	[guestid] [int] NULL,
	[userid] [varchar](50) NULL,
	[exchangerateid] [int] NULL,
	[grandtotal] [decimal](18, 2) NULL,
	[totalriel] [decimal](18, 2) NULL,
	[totaldollar] [decimal](18, 2) NULL,
	[totalother] [decimal](18, 2) NULL,
	[payriel] [decimal](18, 2) NULL,
	[paydollar] [decimal](18, 2) NULL,
	[createby] [varchar](50) NULL,
	[createdate] [date] NULL,
	[updateby] [varchar](50) NULL,
	[updatedate] [date] NULL,
	[paid] [bit] NULL,
	[printed] [bit] NULL,
	[note] [nvarchar](100) NULL,
	[status] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[invoicedetail_tbl]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[invoicedetail_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[invoiceid] [int] NULL,
	[paydemageid] [int] NULL,
 CONSTRAINT [PK_invoicedetail_tbl] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[item_tbl]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[item_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[itemname] [varchar](50) NULL,
	[itemnamekh] [nvarchar](100) NULL,
	[price] [decimal](16, 4) NULL,
	[remark] [nvarchar](100) NULL,
	[status] [nvarchar](50) NULL,
 CONSTRAINT [PK__item_tbl__3213E83F9FE90843] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LoginHistories]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoginHistories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LoggedBy] [nvarchar](max) NULL,
	[LoggedDate] [datetime] NULL,
	[IPAddress] [nvarchar](max) NULL,
	[HostName] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.LoginHistories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[otherexpense_tbl]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[otherexpense_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[date] [date] NULL,
	[expensetypeid] [int] NULL,
	[amount] [decimal](18, 3) NULL,
	[note] [nvarchar](100) NULL,
	[createby] [nvarchar](50) NULL,
	[createdate] [date] NULL,
	[image] [nvarchar](100) NULL,
 CONSTRAINT [PK_otherexpense_tbl] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[paydemage_tbl]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[paydemage_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[date] [date] NULL,
	[guestid] [int] NULL,
	[itemid] [int] NULL,
	[price] [decimal](18, 4) NULL,
	[paid] [bit] NULL,
	[note] [nvarchar](100) NULL,
 CONSTRAINT [PK_paydemage_tbl] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[payslip_tbl]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[payslip_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[date] [datetime] NULL,
	[staffid] [int] NULL,
	[salary] [decimal](18, 2) NULL,
	[vat] [decimal](18, 2) NULL,
	[penanty] [decimal](18, 2) NULL,
	[bonus] [decimal](18, 2) NULL,
	[totalsalary] [decimal](18, 2) NULL,
	[note] [nvarchar](50) NULL,
 CONSTRAINT [PK_payslip_tbl] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[position_tbl]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[position_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[positionname] [varchar](50) NULL,
	[positionnamekh] [nvarchar](50) NULL,
	[status] [bit] NULL,
 CONSTRAINT [PK_position_tbl] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[room_tbl]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[room_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[room_no] [nvarchar](50) NULL,
	[roomtypeid] [int] NULL,
	[servicecharge] [decimal](18, 0) NULL,
	[floorid] [int] NULL,
	[roomkey] [nvarchar](50) NULL,
	[price] [decimal](16, 2) NULL,
	[status] [nvarchar](50) NULL,
	[note] [nvarchar](100) NULL,
 CONSTRAINT [PK__room_tbl__3213E83FD7C75E81] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[roomdetail_tbl]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[roomdetail_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[roomid] [int] NULL,
	[itemid] [int] NULL,
	[price] [decimal](16, 4) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[roomtype_tbl]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[roomtype_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[roomtypename] [varchar](100) NULL,
	[roomtypenamekh] [nvarchar](100) NULL,
	[note] [nvarchar](100) NULL,
 CONSTRAINT [PK__roomtype__3213E83F051B2804] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[salary_tbl]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[salary_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[staffid] [int] NULL,
	[date] [date] NULL,
	[salary] [decimal](18, 2) NULL,
	[note] [nvarchar](50) NULL,
	[createdate] [datetime] NULL,
	[createby] [nvarchar](50) NULL,
 CONSTRAINT [PK_dbo.Salaries] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[staff_tbl]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[staff_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[positionid] [int] NULL,
	[name] [varchar](50) NULL,
	[namekh] [nvarchar](50) NULL,
	[sex] [varchar](10) NULL,
	[phone] [nvarchar](50) NULL,
	[dob] [date] NULL,
	[address] [nvarchar](100) NULL,
	[email] [nvarchar](50) NULL,
	[identityno] [nvarchar](50) NULL,
	[photo] [nvarchar](100) NULL,
	[status] [bit] NULL,
	[createby] [nvarchar](50) NULL,
	[createdate] [date] NULL,
 CONSTRAINT [PK_staff_tbl] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[waterelectricusage_tbl]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[waterelectricusage_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[guestid] [int] NULL,
	[startdate] [date] NULL,
	[enddate] [date] NULL,
	[wstartrecord] [decimal](18, 2) NULL,
	[wendrecord] [decimal](18, 2) NULL,
	[estartrecord] [decimal](18, 2) NULL,
	[eendrecord] [decimal](18, 2) NULL,
	[wepriceid] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[weprice_tbl]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[weprice_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[date] [date] NULL,
	[waterprice] [decimal](18, 0) NULL,
	[electricprice] [decimal](18, 0) NULL,
	[status] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_waterpowerprice_tbl] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[Booking_V]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE view [dbo].[Booking_V]
as
SELECT booking_tbl.id, 'RL'+ RIGHT('000000' + CONVERT(NVARCHAR,booking_tbl.id) , 6) AS bookingno,
booking_tbl.bookingdate, AspNetUsers.FullName, booking_tbl.exchangeid, 
booking_tbl.total, booking_tbl.paydollar, booking_tbl.payriel, booking_tbl.checkindate, booking_tbl.expiredate, booking_tbl.note, 
booking_tbl.status, room_tbl.id AS roomid, room_tbl.room_no, room_tbl.servicecharge, room_tbl.price, room_tbl.roomkey, 
roomtype_tbl.id AS roomtypid, 
         roomtype_tbl.roomtypename, roomtype_tbl.roomtypenamekh, guest_tbl.id AS guestid, guest_tbl.name,guest_tbl.namekh, guest_tbl.sex, 
		 guest_tbl.dob, guest_tbl.address, guest_tbl.nationality, guest_tbl.phone, guest_tbl.email, guest_tbl.ssn, 
		 guest_tbl.passport, guest_tbl.status AS gueststatus
FROM  booking_tbl INNER JOIN
      guest_tbl ON booking_tbl.guestid = guest_tbl.id INNER JOIN
      room_tbl ON booking_tbl.roomid = room_tbl.id INNER JOIN
      roomtype_tbl ON room_tbl.roomtypeid = roomtype_tbl.id inner join 
	  AspNetUsers on AspNetUsers.Id=booking_tbl.userid

GO
/****** Object:  View [dbo].[CHECK_IN_V]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[CHECK_IN_V]
AS
SELECT checkin_tbl.id, checkin_tbl.checkindate, checkin_tbl.child, checkin_tbl.man, 
checkin_tbl.women, checkin_tbl.startdate, checkin_tbl.enddate, checkin_tbl.payforroom,checkin_tbl.paydollar,checkin_tbl.payriel,
checkin_tbl.active, guest_tbl.name, guest_tbl.namekh, guest_tbl.sex, 
guest_tbl.dob, guest_tbl.address, guest_tbl.nationality, guest_tbl.phone, guest_tbl.email, guest_tbl.ssn, guest_tbl.passport, 
         guest_tbl.status, room_tbl.room_no, room_tbl.servicecharge, room_tbl.roomkey, room_tbl.price, roomtype_tbl.roomtypename, roomtype_tbl.roomtypenamekh
FROM  checkin_tbl INNER JOIN
         room_tbl ON checkin_tbl.roomid = room_tbl.id INNER JOIN
         roomtype_tbl ON room_tbl.roomtypeid = roomtype_tbl.id INNER JOIN
         guest_tbl ON checkin_tbl.guestid = guest_tbl.id

GO
/****** Object:  View [dbo].[CHECK_OUT_V]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE view [dbo].[CHECK_OUT_V]
AS

SELECT	checkout_tbl.id,'RL'+ RIGHT('000000' + CONVERT(NVARCHAR,checkout_tbl.id) , 6) AS no,
		checkout_tbl.date, 
		checkout_tbl.total as grandtotal, 
		checkout_tbl.paybefor, 
		checkout_tbl.totalroomprice,
		checkout_tbl.returnamount, 
		checkout_tbl.totalpayment, 
		checkout_tbl.paydollar,
		checkout_tbl.payriel,
		checkout_tbl.description, 
		guest_tbl.id as gid,guest_tbl.name, guest_tbl.namekh, guest_tbl.sex, guest_tbl.dob, guest_tbl.address,guest_tbl.nationality, 
		guest_tbl.phone, guest_tbl.email, guest_tbl.ssn, guest_tbl.passport, guest_tbl.status, 
		room_tbl.id as roomid,room_tbl.room_no, room_tbl.servicecharge, room_tbl.price,
		waterelectricusage_tbl.id as weid, 
		waterelectricusage_tbl.startdate, 
		waterelectricusage_tbl.enddate, 
		waterelectricusage_tbl.wstartrecord, 
		waterelectricusage_tbl.wendrecord, 
		waterelectricusage_tbl.estartrecord, 
		waterelectricusage_tbl.eendrecord,	
		weprice_tbl.waterprice as waterpriceR,
		weprice_tbl.electricprice as electricpriceR,
		case when (waterelectricusage_tbl.eendrecord-waterelectricusage_tbl.estartrecord)*weprice_tbl.electricprice <0 then 0 
		 else (waterelectricusage_tbl.eendrecord-waterelectricusage_tbl.estartrecord)*weprice_tbl.electricprice/ExchangeRates.rate end  as totaleletric,
		 case when (waterelectricusage_tbl.wendrecord-waterelectricusage_tbl.wstartrecord)*weprice_tbl.waterprice <0 then 0 
		 else (waterelectricusage_tbl.wendrecord-waterelectricusage_tbl.wstartrecord)*weprice_tbl.waterprice/ExchangeRates.rate end  as totalwater,
		case when (waterelectricusage_tbl.eendrecord-waterelectricusage_tbl.estartrecord)*weprice_tbl.electricprice <0 then 0 
		 else (waterelectricusage_tbl.eendrecord-waterelectricusage_tbl.estartrecord)*weprice_tbl.electricprice end  as totaleletricR,
		 case when (waterelectricusage_tbl.wendrecord-waterelectricusage_tbl.wstartrecord)*weprice_tbl.waterprice <0 then 0 
		 else (waterelectricusage_tbl.wendrecord-waterelectricusage_tbl.wstartrecord)*weprice_tbl.waterprice end  as totalwaterR,
		 AspNetUsers.FullName
FROM  checkout_tbl INNER JOIN
         guest_tbl ON checkout_tbl.id = guest_tbl.id INNER JOIN
         room_tbl ON checkout_tbl.roomid = room_tbl.id INNER JOIN
         waterelectricusage_tbl ON checkout_tbl.weusageid = waterelectricusage_tbl.id inner join
		 weprice_tbl on weprice_tbl.id=waterelectricusage_tbl.wepriceid inner join
		 ExchangeRates on ExchangeRates.id = checkout_tbl.exchangeid inner join 
		 AspNetUsers on AspNetUsers.Id = checkout_tbl.userid

GO
/****** Object:  View [dbo].[CheckIn_v]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[CheckIn_v]
as
SELECT checkin_tbl.id, checkin_tbl.checkindate, checkin_tbl.child, checkin_tbl.man, checkin_tbl.women, 
		checkin_tbl.payforroom,checkin_tbl.prepaid, checkin_tbl.active,guest_tbl.id as guestid, guest_tbl.name, 
		guest_tbl.namekh, guest_tbl.sex, guest_tbl.dob, guest_tbl.address, 
		guest_tbl.nationality, guest_tbl.phone, guest_tbl.email, guest_tbl.ssn, guest_tbl.passport, 
        guest_tbl.status as gueststatus,room_tbl.id as roomid, room_tbl.room_no, room_tbl.servicecharge, room_tbl.roomkey,
		room_tbl.price, room_tbl.status as roomstatus,
		roomtype_tbl.id as roomtypeid,roomtype_tbl.roomtypename, roomtype_tbl.roomtypenamekh,floor_tbl.id as floorid,floor_tbl.floor_no,
		building_tbl.buildingname,
		waterelectricusage_tbl.id as weid,
		waterelectricusage_tbl.startdate,waterelectricusage_tbl.enddate,waterelectricusage_tbl.wstartrecord,
		waterelectricusage_tbl.wendrecord,
		waterelectricusage_tbl.estartrecord,waterelectricusage_tbl.eendrecord
FROM  checkin_tbl INNER JOIN
         room_tbl ON checkin_tbl.roomid = room_tbl.id INNER JOIN
         roomtype_tbl ON room_tbl.roomtypeid = roomtype_tbl.id INNER JOIN
         guest_tbl ON checkin_tbl.guestid = guest_tbl.id inner join 
		 floor_tbl on floor_tbl.id=room_tbl.floorid inner join
		 building_tbl on building_tbl.id = floor_tbl.buildingid inner join
		 waterelectricusage_tbl on waterelectricusage_tbl.guestid=guest_tbl.id inner join
		 weprice_tbl on weprice_tbl.id=waterelectricusage_tbl.wepriceid
where waterelectricusage_tbl.id in (select MAX(id) from waterelectricusage_tbl group by guestid) and guest_tbl.status='CHECK-IN'

GO
/****** Object:  View [dbo].[CheckOut_V]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE view [dbo].[CheckOut_V]
as
	SELECT
	    checkin_tbl.id as checkinid, 
		checkin_tbl.checkindate, 
		room_tbl.room_no, 
		roomtype_tbl.roomtypename, 
		room_tbl.price,
		room_tbl.servicecharge,
		floor_tbl.floor_no, 
		building_tbl.buildingname, 
		guest_tbl.id as guestid,
		guest_tbl.name, guest_tbl.namekh, guest_tbl.sex, 
		guest_tbl.dob, guest_tbl.address, guest_tbl.nationality, 
		guest_tbl.phone, guest_tbl.email, guest_tbl.ssn, 
		guest_tbl.passport, guest_tbl.status,
		invoice_tbl.invoicedate as startdate,
		DATEADD(MONTH,1,invoice_tbl.invoicedate) as enddate,
		waterelectricusage_tbl.wstartrecord,
		waterelectricusage_tbl.wendrecord,
		waterelectricusage_tbl.estartrecord,
		waterelectricusage_tbl.eendrecord
FROM	checkin_tbl INNER JOIN
		guest_tbl ON checkin_tbl.id = guest_tbl.id INNER JOIN
		invoice_tbl ON checkin_tbl.id = invoice_tbl.id INNER JOIN
		room_tbl ON checkin_tbl.id = room_tbl.id INNER JOIN
		floor_tbl ON room_tbl.floorid = floor_tbl.id INNER JOIN
		roomtype_tbl ON room_tbl.roomtypeid = roomtype_tbl.id INNER JOIN
		building_tbl ON floor_tbl.buildingid = building_tbl.id INNER JOIN
		waterelectricusage_tbl on waterelectricusage_tbl.guestid = guest_tbl.id
where invoice_tbl.status='ACTIVE'
group by checkin_tbl.id, 
		checkin_tbl.checkindate, 
		room_tbl.room_no, 
		roomtype_tbl.roomtypename, 
		room_tbl.price,
		room_tbl.servicecharge,
		floor_tbl.floor_no, 
		building_tbl.buildingname, 
		guest_tbl.id,
		guest_tbl.name, guest_tbl.namekh, guest_tbl.sex, 
		guest_tbl.dob, guest_tbl.address, guest_tbl.nationality, 
		guest_tbl.phone, guest_tbl.email, guest_tbl.ssn, 
		guest_tbl.passport, guest_tbl.status,
		invoice_tbl.invoicedate,
		DATEADD(MONTH,1,invoice_tbl.invoicedate),
		waterelectricusage_tbl.wstartrecord,
		waterelectricusage_tbl.wendrecord,
		waterelectricusage_tbl.estartrecord,
		waterelectricusage_tbl.eendrecord

GO
/****** Object:  View [dbo].[EXPENSE_V]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[EXPENSE_V]
as

SELECT otherexpense_tbl.date, expensetype_tbl.typename, 
otherexpense_tbl.amount, otherexpense_tbl.note
FROM  otherexpense_tbl INNER JOIN
         expensetype_tbl ON otherexpense_tbl.expensetypeid = expensetype_tbl.id
union
SELECT​​ date,typename=CONCAT('Return to guest ',guest_tbl.name),returnamount as amount,description as note
FROM checkout_tbl inner join guest_tbl on guest_tbl.id=checkout_tbl.guestid where checkout_tbl.returnamount>0

GO
/****** Object:  View [dbo].[INCOME_V]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE view [dbo].[INCOME_V]
AS
select bookingdate as date,total as total,incometype='Book',note from booking_tbl
union
select checkindate as date,payforroom as total,incometype='CheckIn',note='' from checkin_tbl
union
select invoicedate as date,grandtotal as total,incometype='Invoice',note from invoice_tbl
union
select date,totalpayment as total,incometype='CheckOut',description as note from checkout_tbl




GO
/****** Object:  View [dbo].[InvoiceV]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[InvoiceV]
as
SELECT   invoice_tbl.id,
		'RL'+ RIGHT('000000' + CONVERT(NVARCHAR,invoice_tbl.id) , 6) AS invoiceno,
		invoice_tbl.invoicedate, invoice_tbl.guestid, 
		 AspNetUsers.FullName, 
         guest_tbl.name, guest_tbl.namekh, guest_tbl.sex, guest_tbl.dob, guest_tbl.address, guest_tbl.nationality, guest_tbl.phone, guest_tbl.email, 
		 guest_tbl.ssn, guest_tbl.passport,checkin_tbl.checkindate,room_tbl.id as roomid,room_tbl.room_no, roomtype_tbl.roomtypename, roomtype_tbl.roomtypenamekh,
		 room_tbl.price,room_tbl.servicecharge,floor_tbl.floor_no, building_tbl.buildingname, building_tbl.buildingnamekh, 
		 waterelectricusage_tbl.startdate as startdate, 
		 waterelectricusage_tbl.enddate as enddate,
		 invoice_tbl.grandtotal, invoice_tbl.totalriel, invoice_tbl.totaldollar, invoice_tbl.totalother, 
		 invoice_tbl.paid,
		 invoice_tbl.note,
		 invoice_tbl.status, 
		 ExchangeRates.rate,invoice_tbl.paydollar,invoice_tbl.payriel,
		 waterelectricusage_tbl.wstartrecord as wstartrecord, 
		 waterelectricusage_tbl.wendrecord as wendrecord, 
		 waterelectricusage_tbl.estartrecord AS estartrecord, 
         waterelectricusage_tbl.eendrecord AS eendrecord,
		 weprice_tbl.electricprice,
		 weprice_tbl.waterprice,
		 case when (waterelectricusage_tbl.wendrecord-waterelectricusage_tbl.wstartrecord)*weprice_tbl.waterprice <0 then 0 
		 else (waterelectricusage_tbl.wendrecord-waterelectricusage_tbl.wstartrecord)*weprice_tbl.waterprice/ExchangeRates.rate end  as totalwater,
		 case when (waterelectricusage_tbl.eendrecord-waterelectricusage_tbl.estartrecord)*weprice_tbl.electricprice <0 then 0 
		 else (waterelectricusage_tbl.eendrecord-waterelectricusage_tbl.estartrecord)*weprice_tbl.electricprice/ExchangeRates.rate end  as totaleletric,

		 case when (waterelectricusage_tbl.eendrecord-waterelectricusage_tbl.estartrecord)*weprice_tbl.electricprice <0 then 0 
		 else (waterelectricusage_tbl.eendrecord-waterelectricusage_tbl.estartrecord)*weprice_tbl.electricprice end  as totaleletricR,
		 case when (waterelectricusage_tbl.wendrecord-waterelectricusage_tbl.wstartrecord)*weprice_tbl.waterprice <0 then 0 
		 else (waterelectricusage_tbl.wendrecord-waterelectricusage_tbl.wstartrecord)*weprice_tbl.waterprice end  as totalwaterR
FROM	guest_tbl
		inner join checkin_tbl on guest_tbl.id=checkin_tbl.guestid
		inner join invoice_tbl on guest_tbl.id=invoice_tbl.guestid
		inner join waterelectricusage_tbl on waterelectricusage_tbl.id = invoice_tbl.weusageid
		inner join weprice_tbl on waterelectricusage_tbl.wepriceid=weprice_tbl.id
		inner join ExchangeRates on invoice_tbl.exchangerateid=ExchangeRates.id
		inner join room_tbl on room_tbl.id=invoice_tbl.roomid
		inner join roomtype_tbl on roomtype_tbl.id=room_tbl.roomtypeid
		inner join floor_tbl on floor_tbl.id=room_tbl.floorid
		inner join building_tbl on building_tbl.id=floor_tbl.buildingid
		inner join AspNetUsers on AspNetUsers.Id = invoice_tbl.userid

GO
/****** Object:  View [dbo].[NewInvoice]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[NewInvoice]
as
SELECT guest_tbl.id as guestid,guest_tbl.name, guest_tbl.namekh, guest_tbl.sex, guest_tbl.dob, guest_tbl.address, guest_tbl.nationality, 
		guest_tbl.phone, guest_tbl.email, guest_tbl.ssn, guest_tbl.passport, guest_tbl.status,checkin_tbl.id as checkinid, checkin_tbl.checkindate, 
		room_tbl.id as roomid, room_tbl.room_no, roomtype_tbl.roomtypename, room_tbl.price,room_tbl.servicecharge,floor_tbl.floor_no, building_tbl.buildingname, 
		waterelectricusage_tbl.id as weid,
		waterelectricusage_tbl.startdate,
		waterelectricusage_tbl.enddate,
		waterelectricusage_tbl.wendrecord as wstartrecord,
		waterelectricusage_tbl.eendrecord as estartrecord,
		invoice_tbl.id as invoiceid,
		invoice_tbl.invoicedate,
		invoice_tbl.paid,invoice_tbl.printed
		--DATEDIFF(day,waterelectricusage_tbl.enddate,GETDATE()) day,
		--case 
		--	--when DATEADD(MONTH,1,invoice_tbl.invoicedate) between  FORMAT (DATEADD(MONTH,-1,GETDATE()), 'yyyy-MM-dd') and  FORMAT (getdate(), 'yyyy-MM-dd')
		--	--when DATEDIFF(day,invoice_tbl.invoicedate,GETDATE())  between  30 and  38
		--	when DATEDIFF(day,waterelectricusage_tbl.startdate,GETDATE())  between  28 and  32
		--	then 'true' 
		--	else  'false' 
		--end --aready paid
		--as PrintInvoice
		
FROM	guest_tbl
		inner join checkin_tbl on guest_tbl.id=checkin_tbl.guestid
		inner join invoice_tbl on guest_tbl.id=invoice_tbl.guestid
		inner join waterelectricusage_tbl on waterelectricusage_tbl.id = invoice_tbl.weusageid
		inner join room_tbl on room_tbl.id=invoice_tbl.roomid
		inner join roomtype_tbl on roomtype_tbl.id=room_tbl.roomtypeid
		inner join floor_tbl on floor_tbl.id=room_tbl.floorid
		inner join building_tbl on building_tbl.id=floor_tbl.buildingid
where invoice_tbl.status='ACTIVE' 
--and guest_tbl.id in (select max(id) from guest_tbl group by id)
--and DATEDIFF(day,waterelectricusage_tbl.startdate,GETDATE())  between  28 and  32
--and DATEADD(MONTH,1,invoice_tbl.invoicedate)=FORMAT(GETDATE(),'yyyy-MM-dd')
and DATEADD(MONTH,1,waterelectricusage_tbl.enddate)=GETDATE()

GO
/****** Object:  View [dbo].[OTHER_EXPENSE_V]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[OTHER_EXPENSE_V]
AS
--SELECT otherexpense_tbl.id, otherexpense_tbl.date, otherexpense_tbl.expensetypeid, expensetype_tbl.typename, 
--otherexpense_tbl.amount, otherexpense_tbl.note, AspNetUsers.FullName
--FROM  otherexpense_tbl INNER JOIN
--         expensetype_tbl ON otherexpense_tbl.expensetypeid = expensetype_tbl.id INNER JOIN
--         AspNetUsers ON otherexpense_tbl.createby = AspNetUsers.Id
--GO
SELECT otherexpense_tbl.date, expensetype_tbl.id as expensetypeid,expensetype_tbl.typename, 
otherexpense_tbl.amount, otherexpense_tbl.note
FROM  otherexpense_tbl INNER JOIN
         expensetype_tbl ON otherexpense_tbl.expensetypeid = expensetype_tbl.id
union
SELECT​​ date,(select id from expensetype_tbl where typename='Return To Guest') as expensetypeid,typename='Return To Guest',returnamount as amount,note=CONCAT('Return to guest ',guest_tbl.name)
FROM checkout_tbl inner join guest_tbl on guest_tbl.id=checkout_tbl.guestid 
where checkout_tbl.returnamount>0


GO
/****** Object:  View [dbo].[PaySlip_V]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE view [dbo].[PaySlip_V]
as
SELECT payslip_tbl.id,RIGHT('000000' + CONVERT(NVARCHAR,payslip_tbl.id) , 6) AS no,
payslip_tbl.date,
payslip_tbl.salary, payslip_tbl.vat, payslip_tbl.penanty, 
payslip_tbl.bonus, payslip_tbl.totalsalary, payslip_tbl.note, staff_tbl.name, 
staff_tbl.namekh, staff_tbl.sex, staff_tbl.phone, position_tbl.positionname, position_tbl.positionnamekh
FROM  staff_tbl INNER JOIN
         position_tbl ON staff_tbl.positionid = position_tbl.id INNER JOIN
         payslip_tbl ON staff_tbl.id = payslip_tbl.staffid

GO
/****** Object:  View [dbo].[PRINT_INVOICE]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE view [dbo].[PRINT_INVOICE]
AS

select  checkin_tbl.id as checkinid,checkin_tbl.checkindate,
		checkin_tbl.roomid,guest_tbl.id as guestid,guest_tbl.name,guest_tbl.namekh,
		waterelectricusage_tbl.id as weid,waterelectricusage_tbl.startdate,DATEADD(MONTH,1,waterelectricusage_tbl.enddate) as enddate,
		room_tbl.room_no,room_tbl.price,room_tbl.servicecharge,roomtype_tbl.roomtypename,
		waterelectricusage_tbl.wstartrecord,waterelectricusage_tbl.estartrecord,
		--'Update' as action,
		case when datediff(day,checkin_tbl.checkindate,GETDATE()) between 28 and 35 then 'Update' else '' end as action
from checkin_tbl 
inner join guest_tbl on guest_tbl.id = checkin_tbl.guestid
inner join waterelectricusage_tbl on waterelectricusage_tbl.guestid=guest_tbl.id
inner join room_tbl on room_tbl.id = checkin_tbl.roomid
inner join roomtype_tbl on roomtype_tbl.id=room_tbl.roomtypeid
inner join floor_tbl on floor_tbl.id=room_tbl.floorid
inner join building_tbl on building_tbl.id=floor_tbl.buildingid
where checkin_tbl.active=0
and waterelectricusage_tbl.id in (select MAX(id) from waterelectricusage_tbl group by guestid)
--and DATEADD(MONTH,1,checkin_tbl.checkindate)=FORMAT(GETDATE(),'yyyy-MM-dd')
and guest_tbl.status='CHECK-IN'
union

SELECT 	checkin_tbl.id as checkinid,checkin_tbl.checkindate,
        checkin_tbl.roomid,guest_tbl.id as guestid,guest_tbl.name,guest_tbl.namekh,
		waterelectricusage_tbl.id as weid,waterelectricusage_tbl.enddate as startdate,DATEADD(MONTH,1,waterelectricusage_tbl.enddate) as enddate,
        room_tbl.room_no,room_tbl.price,room_tbl.servicecharge,roomtype_tbl.roomtypename,
		waterelectricusage_tbl.wendrecord as wstartrecord,waterelectricusage_tbl.eendrecord as estartrecord,
		
		case when invoice_tbl.printed=1 and invoice_tbl.paid=0 and datediff(day,waterelectricusage_tbl.enddate,GETDATE()) >=0
		then CONVERT(varchar,invoice_tbl.id) 
		else 
			case when datediff(day,waterelectricusage_tbl.enddate,GETDATE()) between 28 and 35 
			then 'Insert' else '' end 
		end as action
		--datediff(day,waterelectricusage_tbl.enddate,GETDATE()) as day
		
FROM	guest_tbl
		inner join checkin_tbl on guest_tbl.id=checkin_tbl.guestid
		inner join invoice_tbl on guest_tbl.id=invoice_tbl.guestid
		inner join waterelectricusage_tbl on waterelectricusage_tbl.id = invoice_tbl.weusageid
		inner join room_tbl on room_tbl.id=invoice_tbl.roomid
		inner join roomtype_tbl on roomtype_tbl.id=room_tbl.roomtypeid
		inner join floor_tbl on floor_tbl.id=room_tbl.floorid
		inner join building_tbl on building_tbl.id=floor_tbl.buildingid
where invoice_tbl.status='ACTIVE'
and guest_tbl.status='CHECK-IN'

--and DATEADD(MONTH,1,invoice_tbl.invoicedate)=FORMAT(GETDATE(),'yyyy-MM-dd')
and invoice_tbl.id in (select max(id) from invoice_tbl group by guestid)

GO
/****** Object:  View [dbo].[ROOM_LIST]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[ROOM_LIST]
as
SELECT  room_tbl.id, 
		room_tbl.room_no, 
		room_tbl.roomtypeid, 
		roomtype_tbl.roomtypename, 
		roomtype_tbl.roomtypenamekh,
		room_tbl.servicecharge, 
		room_tbl.floorid,
		floor_tbl.floor_no, 
		building_tbl.buildingname, 
		building_tbl.buildingnamekh,
		room_tbl.roomkey, 
		room_tbl.price, 
		room_tbl.status, 
		room_tbl.note 	
FROM	room_tbl INNER JOIN
        roomtype_tbl ON room_tbl.roomtypeid = roomtype_tbl.id INNER JOIN
        floor_tbl ON room_tbl.floorid = floor_tbl.id INNER JOIN
        building_tbl ON floor_tbl.buildingid = building_tbl.id

GO
/****** Object:  View [dbo].[STAFF_V]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create view [dbo].[STAFF_V]
AS
SELECT staff_tbl.id, staff_tbl.name, 
staff_tbl.namekh, staff_tbl.sex, 
staff_tbl.phone, staff_tbl.dob, staff_tbl.address, 
staff_tbl.email, staff_tbl.identityno, staff_tbl.photo, 
staff_tbl.status, staff_tbl.positionid, position_tbl.positionname, position_tbl.positionnamekh
FROM  staff_tbl INNER JOIN
         position_tbl ON staff_tbl.id = position_tbl.id

GO
/****** Object:  View [dbo].[V_INVOICE_MAX_BY_GUEST]    Script Date: 4/10/2023 7:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[V_INVOICE_MAX_BY_GUEST]
as
SELECT   invoice_tbl.id,
		'RL'+ RIGHT('000000' + CONVERT(NVARCHAR,invoice_tbl.id) , 6) AS invoiceno,
		invoice_tbl.invoicedate, invoice_tbl.guestid, 
		 AspNetUsers.FullName, 
         guest_tbl.name, guest_tbl.namekh, guest_tbl.sex, guest_tbl.dob, guest_tbl.address, guest_tbl.nationality, guest_tbl.phone, guest_tbl.email, 
		 guest_tbl.ssn, guest_tbl.passport,checkin_tbl.checkindate,room_tbl.id as roomid,room_tbl.room_no, roomtype_tbl.roomtypename, roomtype_tbl.roomtypenamekh,
		 room_tbl.price,room_tbl.servicecharge,floor_tbl.floor_no, building_tbl.buildingname, building_tbl.buildingnamekh, 
		 waterelectricusage_tbl.startdate as startdate, 
		 waterelectricusage_tbl.enddate as enddate,
		 invoice_tbl.grandtotal, invoice_tbl.totalriel, invoice_tbl.totaldollar, invoice_tbl.totalother, 
		 invoice_tbl.paid,
		 invoice_tbl.note,
		 invoice_tbl.status, 
		 ExchangeRates.rate,invoice_tbl.paydollar,invoice_tbl.payriel,
		 waterelectricusage_tbl.wstartrecord as wstartrecord, 
		 waterelectricusage_tbl.wendrecord as wendrecord, 
		 waterelectricusage_tbl.estartrecord AS estartrecord, 
         waterelectricusage_tbl.eendrecord AS eendrecord,
		 weprice_tbl.electricprice,
		 weprice_tbl.waterprice,
		 case when (waterelectricusage_tbl.wendrecord-waterelectricusage_tbl.wstartrecord)*weprice_tbl.waterprice <0 then 0 
		 else (waterelectricusage_tbl.wendrecord-waterelectricusage_tbl.wstartrecord)*weprice_tbl.waterprice/ExchangeRates.rate end  as totalwater,
		 case when (waterelectricusage_tbl.eendrecord-waterelectricusage_tbl.estartrecord)*weprice_tbl.electricprice <0 then 0 
		 else (waterelectricusage_tbl.eendrecord-waterelectricusage_tbl.estartrecord)*weprice_tbl.electricprice/ExchangeRates.rate end  as totaleletric,

		 case when (waterelectricusage_tbl.eendrecord-waterelectricusage_tbl.estartrecord)*weprice_tbl.electricprice <0 then 0 
		 else (waterelectricusage_tbl.eendrecord-waterelectricusage_tbl.estartrecord)*weprice_tbl.electricprice end  as totaleletricR,
		 case when (waterelectricusage_tbl.wendrecord-waterelectricusage_tbl.wstartrecord)*weprice_tbl.waterprice <0 then 0 
		 else (waterelectricusage_tbl.wendrecord-waterelectricusage_tbl.wstartrecord)*weprice_tbl.waterprice end  as totalwaterR
FROM	guest_tbl
		inner join checkin_tbl on guest_tbl.id=checkin_tbl.guestid
		inner join invoice_tbl on guest_tbl.id=invoice_tbl.guestid
		inner join waterelectricusage_tbl on waterelectricusage_tbl.id = invoice_tbl.weusageid
		inner join weprice_tbl on waterelectricusage_tbl.wepriceid=weprice_tbl.id
		inner join ExchangeRates on invoice_tbl.exchangerateid=ExchangeRates.id
		inner join room_tbl on room_tbl.id=invoice_tbl.roomid
		inner join roomtype_tbl on roomtype_tbl.id=room_tbl.roomtypeid
		inner join floor_tbl on floor_tbl.id=room_tbl.floorid
		inner join building_tbl on building_tbl.id=floor_tbl.buildingid
		inner join AspNetUsers on AspNetUsers.Id = invoice_tbl.userid
where invoice_tbl.id in (select max(id) from invoice_tbl group by guestid)

GO
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'202011300233239_InitailModel', N'SCHOOL_MANAGEMENT_SYSTEM.Migrations.Configuration', 0x1F8B0800000000000400DD5C5B6FE336167E5FA0FF41D053BB48AD5C7606B381DDC2759236D83809C69E62FB14D012ED082351AA44A50916FBCBF6617FD2FE853D942859E245175BBEA428508C45F23B87871FC9C3C3C3FCEF3FFF1DFEF8EA7BC60B8E62372023F36C706A1A98D881E392D5C84CE8F2FB4FE68F3F7CF397E1B5E3BF1ABFE6F52E583D6849E291F94C69786959B1FD8C7D140F7CD78E823858D2811DF8167202EBFCF4F4EFD6D9998501C2042CC3187E4E08757D9CFE809F9380D838A409F2A68183BD987F8792598A6ADC231FC721B2F1C89C4D7E7978B87B9A8EEFC73F5F4FAFEFE74FB3DF66F3EBE9206B6A1A63CF45A0D60C7B4BD3408404145150FAF24B8C67340AC86A16C207E4CDDF420CF596C88B31EFCCE5BA7ADB7E9D9EB37E59EB8639949DC434F03B029E5D70435962F38DCC6D168604535E83C9E91BEB756ACE9179EBE0F4D3E7C0030388022F275EC42A8FCC6921621C87F7980EF286830CF22602B83F82E8EBA08C7862B46E775210EB7C70CAFE3B3126894793088F084E6884BC13E3315978AEFD0FFC360FBE6232BA385B2C2F3E7DF8889C8B8F7FC3171FCA3D85BE42BDCA07F8F41805218E4037BC2CFA6F1A56B59D25362C9A95DA6456012EC11C318D297ABDC364459F61F69C7F328D1BF7153BF9174EAE2FC48529058D6894C0CFFBC4F3D0C2C345B9552B93FDBF46EAF9878FBD48BD472FEE2A1D7A413E4C9C08E6D567ECA5A5F1B31B66D3AB32DE4FBCDA4D14F8EC77955F59E9D32C48229B7526D05699A368856955BBA1B5266F2B4A33A8FE699DA31E3FB599A632BD95555987369909B9887DCF865CDFDDCA6DCDB87118C2E0A5D46216A9235CC3CE3510A04E0C5D8335A9CEDA928A4067FFCC6BE4B58F5CAF8745B285147056966EE4E3A2973F054049443AEBFC88E218D608E717143FD7A80EFFEC41F519B69308A83BA3C80F772EEDF13920F83EF1176C46EC4F566F4333FF23B841360DA26BC25A6D8D7717D85F83845E13E70A51FC85DA3920FB3977FDF600BDA833B66D1CC7374066EC4C02F0C573C05B422FCE3BC3B1D5EAD04ECAC443AEAFF6528475F529AFBAF654D435246F45534DE5B1D4A97A17AC5CD24ED5BCAA5ED5AC46A3AABC5A575519583B4D794DBDA26985463DB35ABDF980E908F5EF04A6B0C7EF056EB779EBD682921967B042E29F31C1112C63CE23A21447643D026DD68D43380BE9F031A13BDF9B5249BF222FE95BD446B3215D04FA9F0D29ECF1CF86544DF8FCE23ACC2B697134CA2B037CABFAEA5357F39C1334DBF774A87473DFC2F7B306E8A6CB388E03DB4D67812228C6431A55FDC187339AE31B596FC41809740C88EEB22D0FBE40DF4C91540FE40A7B9862636C6741C3098A6DE4C866840E391D14CB77548562EB584955B9BF4A3281E938628D103B04C530535D42E569E112DB0D91D76825A165CB2D8CF5BD9021965CE1101326B0D1126D84AB43234C81428E30284D161A5A25C6D51351E3B5EAC6BCC9855D8FBB14B1D80B271B7C670D2FB9FFB61362D65B6C0FE4AC37491B05B461BE4310949F55DA12403CB81C1B4185139386A0DCA5DA0B41AB163B0041AB26797704CD8EA86DC75F38AF1E1B3DAB07E5FD6FEBB5E63A00372BF638326A66BE27B4A1D00247323DAF16AC10BF52C5E10CF4E4E7B398BBBA224518F80CD36AC866EDEF2AFD50AB1E4424511DE09A680DA0FC82500292265407E5F2585EAD76DC8BE8009BC7DD6A61F9DA2FC096382063972F4A4B15F5D7A922395B9D3E8A9E156C9048DEEAB050C25110425CBCAA1D6F61145D5C56364C1B5FB88B375CEA181F8C1A033578AE1A23E59DE9DD4A39359BADA472C8BAB8645B5949709F3456CA3BD3BB9538479B8DA4700A3AB8055B99A8BA85F734D9F24847B1DB1465432B4BA6E21F869626EB6A384561E89255290B8B7F3166590AD6E4FB59F774243FC3B0EC58919554685B48A2418456582805D1A0E98D1BC5F40A51B4402CCE33717CA99A726FD52CFFB9C8F2F6290F62BE0FE4B5D9BFF9CD6AC3B57E65E3953D130E7803DDF5997B93C6D41564503737588A1CF250A408E34F022FF189DEDBD2B7CE2EF3CAEDB32F32C2D012F497BC29C97492CF5B1D8756A324CF905D8C58E1D96C3E6A7A089DED73BFB46C7D9DAFAA47C9435765145D38EB60A3A87371361F39D195EC3E708D08BB99713C7FA50CC03F75C428A5404860A5B2F6A8D52C953266B5A43DA2908A5286148A3A68594E38A928592ED8084F6351758DF612E4149332BA5CDA1E59916C528656146F80ADD0592C6B8FAAC84729032B8ADB63AF9353C415F588F734ED01A79F4D2D3B106FB7AB693076B33CF6B32996EEFDCB40A5CF1DB1F8CDBE04C6BF1F25B5B4A7C27EA8950545B6A3960643BF2655AECFAB4B52ED9DBF1EB372275E59F6EB7202F478DD08BC539A482744B14A21BD38290A27C2213F9D353FD6918E6B5915D3C8CD08E47A8B29F607ACC260F6BB37F15CCC16F8BCC214117789639AE58198709AFC243CF1399EE736561C3B9EE274AB7B73531DB33DA474911714D9CF2892132CB67892B2069562D7B7C4C1AF23F35F69ABCB340CC2FE957E3E316EE32FC4FD3D8182799460E3DF72C2683F29FAF567B1237D50D1DEAAB7FF7CCA9A9E180F11CC984BE354B0E526235C7D66D1499BACE916DA6CFCF8E2FD4EA8CAFB0525AA3021367FAEB070692F4F15722DBFF5D1EB775D55533E47D80A51F1E4A02FBC5E4CA87B52B00996F63981033F69FA9CA05B67D5CF0B36514DFBB4C025DDC1C48705ED97A1BCE501B71AC501691F4B526AE7C6C4ECADB2340FBD3749F9DB5B4D743947BB03DC1679D81B30E39DA530F7B63B2A32947BC33E24B5779E967C2C99C8EB1C91C32620EF33E7B8E6F6E84F956A7C04C9718A649FC32714EF9B6BBAA0EE916765764B1B3E32B2F114B0C32707EF9B6CBA30EF9193AD530AF09171ED50FBE78199D67A0B3D7842AF9C9BA4B99C51C5829B1276B3C0399CF017019020F328B37796EA0CB1BAECD60681EB2A7AA1FAD43451B0347124B9528D7AB1DDFACA37FCDACEF23AF56235099D75B2F9FA5F2B9BD7A997AD49933C44AAB132515195FEDDB08ED5E54ABDA7D4E24A4F1A32D99B7CD6DA9BF6F79449DC8B512AB3477347FC7E12877B31499F53A743A2B07CDD0B7B67E92F38C2FE1DBBAB3504FB7B8E04DB955DB3A8734B9641BE790B1AE5558408CD1453E4C0963A8EA8BB4436856216634E1F8AA7713B76D3B1C0CE2D7948689850E832F6175E25E0C59C803AF969367455E7E14398FECD933EBA006ABA2C36FF407E4A5CCF29F4BE51C4843410CCBBE0115D3696944576576F05D27D405A0271F3154ED11CFBA10760F10399A117BC896E40BF3BBC42F6DB3A02A803691E88AAD987572E5A45C88F39C6BA3DFC040E3BFEEB0FFF07EB01CEF1C8540000, N'6.1.3-40302')
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'202011300452314_AddBranchModel', N'SCHOOL_MANAGEMENT_SYSTEM.Migrations.Configuration', 0x1F8B0800000000000400DD5CDD6EE3B815BE2FD0771074D516592B3F9DC134B077E17192DDA071128C3D8BF62AA025DA1146A2B412954D50F4C97AD147EA2B94942859FCD38F2D5BCA6080812D91DF393CFC481E1E9F93FFFDE7BFE39F5E7DCF788151EC0668629E8D4E4D03223B705CB49998095EFFF0C9FCE9C73FFE617CEDF8AFC6AF79BB0BDA8EF444F1C47CC638BCB4ACD87E863E8847BE6B47411CACF1C80E7C0B3881757E7AFA37EBECCC8204C224588631FE9220ECFA30FD42BECE0264C31027C09B070EF462F69CBC59A4A8C63DF0611C021B4ECCC5EC978787BBA7F9F47EFAF3F5FCFA7EF9B4F8E762793D1F655D4D63EAB980A8B580DEDA340042010698287DF935860B1C0568B308C903E02DDF4248DAAD8117433698CB6DF3A6E33A3DA7E3B2B61D73283B8971E0B7043CBB6086B2C4EE3B99DB2C0C494C794D4C8EDFE8A853734ECCCF1140F6B36988A22E675E449BD5DA7A94219C18BA7627056308B1E8BF136396783889E004C10447C03B311E9395E7DA7F876FCBE01B441394785E596DA23879C73D208F1EA32084117EFB02D76C30B78E69587C3F4BEC58742BF5C9067A8BF0C5B969DC13E160E5C1821525A32C7010C19F218211C0D0790418C308510C98DA55922EC8A2FFE7D2080DC9F2328D3978BD8368839F27E6F9870FA671E3BE42277FC234F88A5CB21A49271C2550A161B5543B8244DB2787FC970BBF229F97AEBF33D6EAAD6218E463A36194258DAD2D332BF99A9BFA4BE0C12AD6CE8B25318DC37B884779C75106791311B8DF83E8DBA88C786234EEB7A5F579535A5F9CADD6179F3E7C04CEC5C7BFC28B0FC7A7B862B6CECE3F1D8274B554FFD889D47BF0E26ED2A917E4938D3E22E7C017E8A56FE36737CC8E036EBE9F58B39B28F0E9779E5FD9DBA7459044361D4CA06DB204D106E23D294DA1BAA7758E3A7C6A534D657A2B9BD201EDB2127211C75E0DB9BE8795DB9871D330249397528B5A649FD35F80FA0EDC80E3B1E2DA07AED7C126D9400A71AED76EE4C362949F034249805AEBFC08E298EC11CE2F207EEED80790852DA09D4484BA0B0CFCF0E0D21E9F0304EF137F4557C4F164753635CBDF831B60130FF51AD15E7BE3DD05F6B720C1D7C8A1EEE2576CCBDE6343804ED499DA368CE31B4266E8CC027277ACF3DBABE1E86ED5B79332F380EBABBD14615F7DCA9B6E3D15750BC95BD13453792C55AADE051B173553356FAA57356B51AB2A6BD656550AD64C53D652AF68DAA056CFAC55673E603A43DD3B8129ECF0BDC0A1DFE1FB7221D3E9A3420F7E36A5927E055ED2B5A89D5643BA0974BF1A52D8E1AF86544DF2F8C575A857D2E06A943726F08DDAAB6F5DF56B4ED0ECD8CB811BE6B1851F670FD02D97691C07B69BAE0245508C853478FD890F67D4C737B2D188311232304274971E79E409199B2992EA015D410F62684CED2CC83D03B10D1CD98C64404E0BC5F21355A1D83656C22BF7174926613A8C6827402F413159A92EC2F2B27091ED86C0ABB592D0B3E11146C75EC810DF5CC110222AB0D6124D84AB43235481428E302975161A5B25C6551351E3B5EAE6BCCE85DDCEBB14B1380A276B7C670D2F99FF761062565BEC08E4AC36491305B461BE3E08CAEE2A4D09205E5C864650E1C6A4212873A98E4250DE623D109437C9BB236876456D3AFFC27D7568F4E42FCAC73FD62BCDD50337397B0C8C9A99EF49FA60D20346323DAF56F4257CC58ACB19D193DDCF62E6EA8A14A1E00B88B92C84D834B61E2F63469E9F605503B0488ED49D77646B40441656016E995A03CA7E619480A415D942B93C1858A91D73435AC0E681BB4A58767808B02512C9D8E55F5A4B0DF5BFC78AEC6E747D294656B0415A258D6E1B251C0521C4DD8F1F7803A3E802BBB2619A38D36DDCE9D2C0D8645418A8C6F5D518291F4CE756CAA9596F259547D7C6A7DBCB4A82FFA5B1523E98CEADC4385A6F248557D1C2AFD8CB44BC0FD0D162CB4325C57155BC1B5B59F6207B30B6346986E3390843176D4A6987EC89B1C8720E673F2CDAE7DFF9198665C78A34BC42DB42120E22B081C25B229A687AE34631BE0218AC000D14CD1C5F6AA63C9C35DB7F2E923F7FE569CC4F82BC3DFDCC7E9C6D9417A8706B18D40D19AA4F7DA334205F2282AEA341334181072245F47F1678898FF44E9ABE77F61B60B97FF6A43902975F5706E25EB4C6A339760AB49522AC4B792D58557210A5A994DC789E198D78A3DB4776670DE7B0B5E74E75F7BE19D4D32CC93BEB2166ACF088779F353D84CEF6F985A86C7DDD25498F92C74CCB28BA386A6FB3A8738D779F39F10AD27EE26A110EB3E258E25419803D6A8951CABD91C04AEF9AA3F2E951654CFE4D73442107AA0C29BC6AA16539D38953B2FC62273C8D45D52D9A4B90739BCAE8F2DBE6C88A2CA732B4E2F50ED80A9DC577CD511589506560C5EBE6D8DBAC2871471DF099A6BD187773A8658194FD4E350DC661B6C76E0EC552C24919A8F4B825164B2991C0D8F341524B1B4DE8865A59306D3F6A6930F47B1297B7C16F4995C9267A4C2E1983DBF6AB9251F478ED087C509A489105B14921BD883008918431BBD5D757354AD7FCAC8969E46624E47A8B31F447B4C168F19B37F35C4837F8BCC11C20770D639C252099E7A7A79F845AC8E1D4255A71EC788AA8885C9CC8CFD611B2085D6AD3DA3CC13D2AA2D00B88EC6710C9E57FFBD7F6D1CFB893DABE5CCB3FF9E0F5CF65B8AEEAF78E3EB14ABBA7C95A9D4CA6F43BD82D72E0EBC4FC57DAEB328D88D24FE9E313E336FE8ADCDF12F262490C6AFC5B4E3EEFA6DCA7FA7A3DD0E2ACE656BDFDC753D6F5C47888C82678699C0AB6DC6586F992AD56DA645DF7D066E742AEF7BBA0B85A28CDF6F8B17A0FD2804AF5352B177752F6D4627B940195A54D7B212ACA97BAC2EBC484BAF2A45DB0B4A549AAC3AFC960D5A54ABBA8A62D534A9D8A3D8B949A6F4379CF1E8F1AC59DF7DD3A6FC33A9BA45A90BD16BA5CEF7148C74F7365FD2ECB213A3B1D15D50E9D61F749ED8397380CA5AA619B2ED66F31C331EB172A7E10FCAECA16069068ABC8FBEBBF38E1D85CD3C5E9079EE1DDAE0461606463D9A0FD171A1C9B6CBAC8FDC0C9D6AA9C60605CEBEBFCEC99698D8FD0DE8B03E43445715A5559FF1549FFD9EF1FE456BF0AC8C4675E64F64E911D2AA2F3DE9E24837FAD92945584AB535175C2B654D40ADC36D10BD5E7C08A82A56529C9955A548B6D3756E64E540E96B5A916ABC91CAF92CD4E974AD9AC4DB56C4D3E761F350DCA8C68559D49CD2E59955CF79E6A18B891D494CCD479C495A919EFA964A113A370AB479354F07E2A143A3149974BA74545829C1F404EE6D2DF4626DE41EC6EB610F42F252368736772D1E616AD83DC351034CA9B08F19F39C4C02107F634C2EE1AD898BCA611ECF44F5AA45141FA3BCA0A3AB7E821C16182C990A1BFF2B8701A7531AAE4A76517BCCEE38730FDEB4C5D0C81A8E9D2C8FF03FA9CB89E53E87DA388386920A8EFC2E2C5742E318D1B6FDE0AA4FB00350462E62B5CAE25F4438F80C50F68015EE02EBA11FADDC10DB0DFB6F1451D48FD44F0661F5FB96013013F6618DBFEE42BE1B0E3BFFEF87F7B7568B5225C0000, N'6.1.3-40302')
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'202012010337293_AddDepartmentModel', N'SCHOOL_MANAGEMENT_SYSTEM.Migrations.Configuration', 0x1F8B0800000000000400DD5CDD6EE3B815BE2FD0771074D516592B3F9DC134B077E17192DDA071128C3D8BF62AA025DA1146A2B412954D50F4C97AD147EA2B94942859FCD38F2D5BCA6080812D91DF393CFC481E1E9F93FFFDE7BFE39F5E7DCF788151EC0668629E8D4E4D03223B705CB49998095EFFF0C9FCE9C73FFE617CEDF8AFC6AF79BB0BDA8EF444F1C47CC638BCB4ACD87E863E8847BE6B47411CACF1C80E7C0B3881757E7AFA37EBECCC8204C224588631FE9220ECFA30FD42BECE0264C31027C09B070EF462F69CBC59A4A8C63DF0611C021B4ECCC5EC978787BBA7F9F47EFAF3F5FCFA7EF9B4F8E762793D1F655D4D63EAB980A8B580DEDA340042010698287DF935860B1C0568B308C903E02DDF4248DAAD8117433698CB6DF3A6E33A3DA7E3B2B61D73283B8971E0B7043CBB6086B2C4EE3B99DB2C0C494C794D4C8EDFE8A853734ECCCF1140F6B36988A22E675E449BD5DA7A94219C18BA7627056308B1E8BF136396783889E004C10447C03B311E9395E7DA7F876FCBE01B441394785E596DA23879C73D208F1EA32084117EFB02D76C30B78E69587C3F4BEC58742BF5C9067A8BF0C5B969DC13E160E5C1821525A32C7010C19F218211C0D0790418C308510C98DA55922EC8A2FFE7D2080DC9F2328D3978BD8368839F27E6F9870FA671E3BE42277FC234F88A5CB21A49271C2550A161B5543B8244DB2787FC970BBF229F97AEBF33D6EAAD6218E463A36194258DAD2D332BF99A9BFA4BE0C12AD6CE8B25318DC37B884779C75106791311B8DF83E8DBA88C786234EEB7A5F579535A5F9CADD6179F3E7C04CEC5C7BFC28B0FC7A7B862B6CECE3F1D8274B554FFD889D47BF0E26ED2A917E4938D3E22E7C017E8A56FE36737CC8E036EBE9F58B39B28F0E9779E5FD9DBA7459044361D4CA06DB204D106E23D294DA1BAA7758E3A7C6A534D657A2B9BD201EDB2127211C75E0DB9BE8795DB9871D330249397528B5A649FD35F80FA0EDC80E3B1E2DA07AED7C126D9400A71AED76EE4C362949F034249805AEBFC08E298EC11CE2F207EEED80790852DA09D4484BA0B0CFCF0E0D21E9F0304EF137F4557C4F164753635CBDF831B60130FF51AD15E7BE3DD05F6B720C1D7C8A1EEE2576CCBDE6343804ED499DA368CE31B4266E8CC027277ACF3DBABE1E86ED5B79332F380EBABBD14615F7DCA9B6E3D15750BC95BD13453792C55AADE051B173553356FAA57356B51AB2A6BD656550AD64C53D652AF68DAA056CFAC55673E603A43DD3B8129ECF0BDC0A1DFE1FB7221D3E9A3420F7E36A5927E055ED2B5A89D5643BA0974BF1A52D8E1AF86544DF2F8C575A857D2E06A943726F08DDAAB6F5DF56B4ED0ECD8CB811BE6B1851F670FD02D97691C07B69BAE0245508C853478FD890F67D4C737B2D188311232304274971E79E409199B2992EA015D410F62684CED2CC83D03B10D1CD98C64404E0BC5F21355A1D83656C22BF7174926613A8C6827402F413159A92EC2F2B27091ED86C0ABB592D0B3E11146C75EC810DF5CC110222AB0D6124D84AB43235481428E302975161A5B25C6551351E3B5EAE6BCCE85DDCEBB14B1380A276B7C670D2F99FF761062565BEC08E4AC36491305B461BE3E08CAEE2A4D09205E5C864650E1C6A4212873A98E4250DE623D109437C9BB236876456D3AFFC27D7568F4E42FCAC73FD62BCDD50337397B0C8C9A99EF49FA60D20346323DAF56F4257CC58ACB19D193DDCF62E6EA8A14A1E00B88B92C84D834B61E2F63469E9F605503B0488ED49D77646B40441656016E995A03CA7E619480A415D942B93C1858A91D73435AC0E681BB4A58767808B02512C9D8E55F5A4B0DF5BFC78AEC6E747D294656B0415A258D6E1B251C0521C4DD8F1F7803A3E802BBB2619A38D36DDCE9D2C0D8645418A8C6F5D518291F4CE756CAA9596F259547D7C6A7DBCB4A82FFA5B1523E98CEADC4385A6F248557D1C2AFD8CB44BC0FD0D162CB4325C57155BC1B5B59F6207B30B6346986E3390843176D4A6987EC89B1C8720E673F2CDAE7DFF9198665C78A34BC42DB42120E22B081C25B229A687AE34631BE0218AC000D14CD1C5F6AA63C9C35DB7F2E923F7FE569CC4F82BC3DFDCC7E9C6D9417A8706B18D40D19AA4F7DA334205F2282AEA341334181072245F47F1678898FF44E9ABE77F61B60B97FF6A43902975F5706E25EB4C6A339760AB49522AC4B792D58557210A5A994DC789E198D78A3DB4776670DE7B0B5E74E75F7BE19D4D32CC93BEB2166ACF088779F353D84CEF6F985A86C7DDD25498F92C74CCB28BA386A6FB3A8738D779F39F10AD27EE26A110EB3E258E25419803D6A8951CABD91C04AEF9AA3F2E951654CFE4D73442107AA0C29BC6AA16539D38953B2FC62273C8D45D52D9A4B90739BCAE8F2DBE6C88A2CA732B4E2F50ED80A9DC577CD511589506560C5EBE6D8DBAC2871471DF099A6BD187773A8658194FD4E350DC661B6C76E0EC552C24919A8F4B825164B2991C0D8F341524B1B4DE8865A59306D3F6A6930F47B1297B7C16F4995C9267A4C2E1983DBF6AB9251F478ED087C509A489105B14921BD883008918431BBD5D757354AD7FCAC8969E46624E47A8B31F447B4C168F19B37F35C4837F8BCC11C20770D639C252099E7A7A79F845AC8E1D4255A71EC788AA8885C9CC8CFD611B2085D6AD3DA3CC13D2AA2D00B88EC6710C9E57FFBD7F6D1CFB893DABE5CCB3FF9E0F5CF65B8AEEAF78E3EB14ABBA7C95A9D4CA6F43BD82D72E0EBC4FC57DAEB328D88D24FE9E313E336FE8ADCDF12F262490C6AFC5B4E3EEFA6DCA7FA7A3DD0E2ACE656BDFDC753D6F5C47888C82678699C0AB6DC6586F992AD56DA645DF7D066E742AEF7BBA0B85A28CDF6F8B17A0FD2804AF5352B177752F6D4627B940195A54D7B212ACA97BAC2EBC484BAF2A45DB0B4A549AAC3AFC960D5A54ABBA8A62D534A9D8A3D8B949A6F4379CF1E8F1AC59DF7DD3A6FC33A9BA45A90BD16BA5CEF7148C74F7365FD2ECB213A3B1D15D50E9D61F749ED8397380CA5AA619B2ED66F31C331EB172A7E10FCAECA16069068ABC8FBEBBF38E1D85CD3C5E9079EE1DDAE0461606463D9A0FD171A1C9B6CBAC8FDC0C9D6AA9C60605CEBEBFCEC99698D8FD0DE8B03E43445715A5559FF1549FFD9EF1FE456BF0AC8C4675E64F64E911D2AA2F3DE9E24837FAD92945584AB535175C2B654D40ADC36D10BD5E7C08A82A56529C9955A548B6D3756E64E540E96B5A916ABC91CAF92CD4E974AD9AC4DB56C4D3E761F350DCA8C68559D49CD2E59955CF79E6A18B891D494CCD479C495A919EFA964A113A370AB479354F07E2A143A3149974BA74545829C1F404EE6D2DF4626DE41EC6EB610F42F252368736772D1E616AD83DC351034CA9B08F19F39C4C02107F634C2EE1AD898BCA611ECF44F5AA45141FA3BCA0A3AB7E821C16182C990A1BFF2B8701A7531AAE4A76517BCCEE38730FDEB4C5D0C81A8E9D2C8FF03FA9CB89E53E87DA388386920A8EFC2E2C5742E318D1B6FDE0AA4FB00350462E62B5CAE25F4438F80C50F68015EE02EBA11FADDC10DB0DFB6F1451D48FD44F0661F5FB96013013F6618DBFEE42BE1B0E3BFFEF87F7B7568B5225C0000, N'6.1.3-40302')
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'202012010402493_AddDepartmentModel1', N'SCHOOL_MANAGEMENT_SYSTEM.Migrations.Configuration', 0x1F8B0800000000000400ED5DDD6EDB3816BE5F60DF41D0D5EE2263E7675B74037B06A993CC041B2741ED0E76AF025AA21DA112A591A84C82C13CD95EEC23CD2B0C2951327FF563CBB65214058A4424BF7378F8513C3C3AA7FDE37FFF1FFDF012F8D6338C132F4463FB64706C5B1039A1EBA1D5D84EF1F2BB0FF60FDFFFF52FA32B3778B17E2EFA9DD17E64244AC6F613C6D1F97098384F3000C920F09C384CC2251E386130046E383C3D3EFED7F0E4640809844DB02C6BF42945D80B60F60BF975122207463805FE3474A19FB0E7A46596A15A77208049041C38B667939FEEEF6F1FA71777173F5E4DAFEEE68FB3FFCEE657D3413ED4B62E7C0F10B566D05FDA164028C40013A5CF3F277086E310AD66117900FCF96B0449BF25F013C82673BEEEDE745EC7A7745EC3F5C002CA49131C062D014FCE98A186F2F08DCC6D978624A6BC2226C7AF74D69939C7F6C71820E7C9B66451E7133FA6DD6A6D3DC8118E2C53BFA392318458F4CF9135497D9CC6708C608A63E01F590FE9C2F79C7FC3D779F805A2314A7D9F579B284EDA8407E4D1431C4630C6AF9FE0924DE6C6B5ADA1386E280F2C877163F289DE207C766A5B77443858F8B064056794190E63F82344300618BA0F006318238A0133BB2AD22559F4EF421AA121D95EB635052FB710ADF0D3D83E7DF7CEB6AEBD17E8164F98069F914776231984E3146A34AC96EAC49068FBE892BF0AE197E4E7B9176C8CB578AD9806F9B1D1347849A3E19A99957CBD8411887140ECBD0D67D728DF78FB8DB7FBE06D61EA4FA10FAB983B2D5FE517497407F1A01838C821AF6302F76B187F19F0884756E3716B5A9F36A5F5D9C96279F6E1DD7BE09EBDFF273C7BB77F8A6B56EBE4F4C32E48574BF5F79D48BD03CFDE2A5B7A493E715062E2BF7C827ED69A3C7951EEC608EBFDC8BA5DC761407F17F995B73ECEC23476E864426397398857106F49690AD53DAD0BD4FE539B6AAAD25BDB954E68939D5088D8F76E28F4DDADDCC68CBB8822B27819B5A845B6F10024A8AFC00DD81F2BAE02E0F91DBC241B482197C2A51707B09CE5C7905012A0D63A3F802421EF08F727903C75EC03A8C266D0496342DD190641B473690F4F21827769B0A03B627FB23A5B9AF9AFE1357088877A85E8A8ADF16E43E74B98E22BE45277F1337654EFB1214027EA5C380E4C926B4266E84EC2945E5CAAFDF66A38FAB63AB49332F18117E8BD14E9BDFA58745D7B2AFA1E8AB762E8A6F358AA54BD0D571E6AA66AD1D5AC6ADEA35655D6ADADAA14AC99A6ACA759D1AC43AD9E79AFCE7CC06C85BA770233D8FE7B817DBFC31FCA85CC968F0ADDF9D99449FA19F869D7A236DA0DD94BA0FBDD90C1F67F37646A92C7CF9E4BBD920657A3A233816FD45F7FEBAADF739266FBDE0EC234F72D7C3FEF00D376B94892D0F1B25DA0098AB19086A83FF1E1ACFAF8463E1B394642264688EED1238F3C2173B36552DDA34BE8430CAD0B27FF38330189035CD58C64426E0BC58A1355A3D83A56222AF70F4526613A8CE920402F4109D9A91EC2EAB6F090E345C0AFB59234B2E11146E75ECA905B2E61041115586B8926C2F5A111AA4029475A943A0B8D861CE3AA8968F05A4D6B5EE7C2AED75D8958EC859335BEB38197CC7FDB0931AB2DB60772569BA48902C630DF2108CAEE2A4D09205F5CFA4650E9C664202873A9F64250D1620720A868923747D0FC8ADA74FDA5FB6ADFE8295E94F77FAC579AEB00DC14ECD1336AE6BE271983C90818ABF4BC5CD046F8A24B20207AB2FB59C25C5D9922147C06B1903D93D8D6DAE365CC28F26A86D500EB44041D089FEC5003C442420A84E811D780C874AE025C53BE06947DAA548094ADDD42B922AA58A91DF3675AC01611C04A58760A49B01C1B556CFE932DD7D1FC6157DE268DEE41E5CC4A3628DBADD1B585C3D110427E8D8A136F6014538458354C13AFBC8D5FCE4D8C2D4685816A7C6883918AC9746EA5829AF556D2B9866D9CC3ADAC243972062B1593E9DC4A8CA3F546D2B8272D1C94AD4C243A131D6DB622E6529E7B65DB6898A7CFB207A3A121CF76340551E4A1159777CB9E58B33CE976F2DDAC7D026A90630C9D4493875A6A5B4AC2610C56506A25A289A6D75E9CE04B80C102D088D3C40D946EDA53DEF0FA2F448A07B9BA8CC54950F4A73FB3AFBC8D126335FE1183BA2653A5677B366BC811C134D0A2A9D0C007B1E633C224F4D30099BD3DF3E8FC63223F3E7FD21C4148D4E3818486D67834594F83B6D0C48729AF25AB2A9EA6B294CA7D40644623DEF0FE5B97DCE13CBFF6FCA91AFC8D43BDE390E92CDA9C3D82D3DF9E3FD5C30FCDA003AD927A3AEF62C5CA5BD5E6AB668630D9BEB89DF3D637DDD8CD2845009F473105F50FB68AA6EBD5E62B275F63DB2F5C2DC26E761CCBE2E301D8A396185C229802C6B535471573F5784CB1A539A29490C7434A4D2DB4E4D3EE0425F9868DF00C16D5F7682E414DB4E3D1D5D6E6C89A943B1E5AD3BC01B64667B9AD39AA262B8F07D63437C75EA7E8C96FD41E9F69C6E04A37875A1E8CDBEE543360ECE6F5D8CDA1C8653FF140DCE396582CBF490163CF7B492D6344AA1B6AE501D9EDA865C030BF93842422F1955499F964C614328384D77E55669419AF1D81774A13253A257729A597512A291A356291A1FAD27025549477B1ADC28C845CAF0986C1807618CC7EF127BE977D61293A4C01F29630C179369C7D7A7CFC412A28EF4F71F730495C5F1359532BBCC5D5DA434AAB476D5A9BB4BA45791E7A06B1F30462B51675FB4253FA33EEA4D0B4D0F26F0178F93B0FD74D11F4B765FD7A96D51C77D9C7C26AED9E258476B298CAB7F61BE4C297B1FD5B36EA3CFB58427FCA1E1F5937C967E4FD9292863931A8F5BB5AE0D24D496175D4A4A705A0CDAD7AF39FC77CE891751F93B3EDDC3A966CB9C90A8B65A1ADB4C9876EA1CDC6C5A26F774309F59686D7E3FBEA77900154A9E15B78B893D2CA16AF4715505B3EB915A2A644B22BBC4E4C682A81DC04CB58FEA83BFC9A4C565F0EB9896AC652C8CCA9D8B210B2F96BA81879C0A34613CA78B3CE5BBFCE26A5DE6CAB8DAED694EDD2F1334422BECA92ABCE4E474D455567D887A4F6CECBA8FA5239B5CE243D6CC1D43E6BA42ABEF37E55A5513D48E6D7A4041FBE006ADF5C337D7EE9791549BB32A79E918D258A1FBE9869DF64337D90E939D95A952CF58C6B873A3F0FCCB4C647E8C10B90D40C667959759545158545F9672D72AB5F8464E1732F326FD3248ECBE87C619122816FD449116A961A56D7E414544489CD3A61F93F6FA14F8737095B73DE2870DDC52CD49C872F0B56F6BF2257E9512DB6DD5C99DF523959D6A75AACA17AA54A363BC62A65B33ED5B20D352187A8ABD25665E86ADD6A5EC755C9996FA98E4A98494DD95E9DEB5D99DAF396CAA63A318AB07B0C49296FA74AAA139374B9755A5445A9F925C405E0FE8302E28624DE6A0D41FFBB02041DE1F02FFBDCA06558F8209246451729D0348518B8C433B888B1B7040E26CD34549EFDFB3C59F8917EB05940F706DDA7384A3199320C16BE10B7A3BE4C95FCACF44BD479741F65FFD45C1753206A7AF413C33DFA987ABE5BEA7DAD096D1920A893C402D3742D310D50AF5E4BA4BB10350462E62B7DBB390C229F8025F768069EE126BA11FADDC215705ED7814C1348FD4288661F5D7A601583206118EBF1E457C2613778F9FE4F32A92915A7630000, N'6.1.3-40302')
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'202012081614122_AddEmployee_Parent_Education_Salary', N'SCHOOL_MANAGEMENT_SYSTEM.Migrations.Configuration', 0x1F8B0800000000000400ED5DD96EE4B8157D0F907F10F494041E9797E946C7B067E0F13263A4BDA0CB3D489E0AB4449795D65223A97A6C04F9B23CE493F20B212551E22E6A2D55A3D040C3259287E4D5E1E57675EFFFFEF3DFD31F5F03DFFA0AE3C48BC233FB70FFC0B660E844AE172ECFEC75FAFCDD07FBC71FFEF887D32B3778B57E25F98E713E54324CCEEC97345D9DCC6689F3020390EC079E134749F49CEE3B5130036E343B3A38F8EBECF0700611848DB02CEBF4D33A4CBD00663FD0CF8B2874E02A5D03FF3672A19F14CF51CA3C43B5EE4000931570E0993DBFF8E5FEFEE3E2F6FCEEFCE7ABDBABBBC7C5FC1FF3C7ABDBFDBCA86D9DFB1E40CD9A43FFD9B6401846294851A34F3E27709EC651B89CAFD003E03FBEAD20CAF70CFC04169D39A9B29BF6EBE008F76B56152450CE3A49A3A021E0E17121A8195FBC95B8ED529048945748E4E91BEE7526CE33FBA71884CE8B6DF1559D5CF831CE562BEBFD1C61CF52E5DB2B19838885FFED59176B3F5DC7F02C84EB3406FE9EF5B07EF23DE76FF0ED31FA02C3B370EDFB74B351C3511AF3003D7A88A3158CD3B74FF0B9E8CC8D6B5B33B6DC8C2F5816A3CAE41DBD09D3E323DBBA439583271F96ACA084324FA318FE0C43188314BA0F204D611C620C98C955A89DAB0BFF4F6A433444C3CBB66EC1EB47182ED39733FBE8DD3BDBBAF65EA14B9E142DF81C7A6834A24269BC869216EA6B7562885ABB70D17FA4F24BF4F7A317B4C67A7AD37403FD69D40DBAA6D359C54C2D5F2FE10AC46980E4DD85B315CA8EB73BDE8EC1DB2B77ED143ABC3D6D4B901D6B0D580B83951FBD41B8F06A2BAD0172D77E2EF7850FBF42BF670E8915265F3C7FF85A9EE32858BC4110771E5D69D40FCE368C7854D31DF8EA2D333E2818675B9FA09F65485EBC55BE0CAD06EFA2CA768D5EC1A7C8A7F54399BA9847EBD8C1828894591E41BC84695B955436A383462A30760AC94021E52BE5FA1AF528D5DAA52B5200622F05FE628EBAB74E08D64F11621B081BA385FA4582E9586B51EBE2CBCBF8152F61E8C2787025BD7A89C23EC45A3755026FF80907A9AD0570DD1826C9E07579C172F03ADCE8A9F334B5AA30869345B2004EEA7D855D87F880736A9F2B01E5FC4C0E3B64B3339952491E6A6A6693C479994B974DCABA56D15B5A5DCBE87C92D655C9EA165279BA2D1D5E51073C183ADD160F25CA6EF930E67EE6F728FEB2F023B21F1D7A068B126F948A76FB9971F733E5E8956F68C464512F49F274D24B0F20EE78329823ECF4D198FAA866E7707870D08376A8DF28F453CF48FB82BB28D5C9CCF448B64675678361F1CF11D6A84555F5DB837E7AF64D6BE65C8749B532972468643EBD9336268A0443EB74F26D797D799EACEE60BA4F0AEEE790D73182C38B967D1A71CF322E5729ED2353A57D7CF8F47CFCE1DD7BE01EBFFF1E1EBF1B5F81CB34D4D187218E326AAF77DEF752AB92CC9F13F476A44CA6DFF7A2C85691594C15F82CC9D20BA53154FFB426A8D3A7366EA9486F6956DCA13623815431F66820ED1DB65E63C6CD810FE2B72E4BDA1C61B7A41D73490B82688D3722C5D22242E2DB6D1F19D59EB352BA48E19204A5CEA777D2E8E7AB156276D63AAC71BA0C340EEA1B1871E369DDAB9A1B08D34588412D1751F8ECC5012C7BD9F638FC0124099A83DD5F40D2C70594BEB23974D631A2EE3C05C16AF0DA1EF0B5D3DD3A781A612B4BD5D5DBAB79FC3DBA060E9A0CAE425CAA33DEC7C8F912ADD3ABD0C51AF873EA880AD910A097E69C3B0EDA255F233243F7829E69DACD56585B6D7A1370E1032F90EF0238BDBA2059AB19439E43983814D99A5ED87C8C965E68D6549255DDD43C476D538B6C4D9B8AC1CC5A5AE4543734CB50DBCE3C576F7BACEC0DF5BFC9CA60A7BFCB9AFA7279535BB4ECF5E14A079F9BB29A7E05FEBAEFAA5A8D864C09F43F1A32D8E98F86AC99E8F1572F3B6037387A209911BC517EF9A946FD98E35A36F67060BA3976E5E3E800D570394F92C8F1B251C0593512430EB6F1680167D55875E4FDA02D2F507710BD3D3CD1A1269CD97F1164A286253321054B2C4C58D0439BE7E77D78097D9842EBDCC9BF1DBA0089035CF18D20D9B8EC134469880FF23D80773B091A925E988AFCF742C75B015FDF74AE98E144859B5556C0A75CC215BE250B53FDBB30A9B9B2A914EB2FABE1C455279DD319452B43B651C639B5D49059EAF4C43A89810F054D5B104D967D6217C664A0F86E4C6A676D7237C344D1BC5CC9168DAD39C5C3EA8B95064454DBA81B507C022C54B67F0C122ADF8B49E5CCD9F5662828B108521245671E44318532346CC0428D5DD176D050DD813178A87E37DB4144DE0042C513A53544C5116251664E3E9509C556104FD1F81148A77817DB4138899D828A1E3AA3858A21ACE9CC802CD1364C425DD100A26E64B4E2A15A4A2350512D0993CAE5F60E231191BF5555BD6CE5156BF5A689E181B9E653DDCB6E85E653347E04BA29DEC576683EC5DD8C8A227517351553847BF9519460CD0D91421116B714836842BDC446A0A75E24260D501A8B6D82A0C58D9C2901F8EBB9A91194BB175410B4B8381885A0ACC436405056245B47D0FC22D6F4FD73B7B253A3277B1D3CFE3A522BAE0D709391C7C4A899DFB0A032292A0163919E974F3811BECA3EB042ED2C6E2193E24287A708069FC39439B74F6CABBAD7E1AE478465260B501DB9CA40E893EE1AA0F2D44D86431D82D6C1144B31294AB9EAAD03294F5EA430D461580D50BE9B968190038D1A80C21C4428CE6E4C6B40F841AE03AC14410D68BE5C9641911D4B0D40F11D81505CD0980D7A474C92B4DD2B96890D6089F99016B698DC39586A908B242DBF72A772293E84E7F54EFD0D6AD9216650080AACFECE94022AD5053F17B1DD6C22025A4568C4A0BAD893F74072B5D75E1C92CB3C0A8C5180DDC522F14E24914ACD2593557317249309A57E7542515F2C9908B88D4464DF374B445277E9617CED41F7839E077452D15C740C2416E1E3425124DAE377A30378AAEDE54CA61183EAC87D2011C8BE4A13A55077266C7A2A4C75A298433592D01CE252389279B9B35084EF394489680F278D8E27A93E94CB008D3454079203F14265A72C4AC2E4D4ACC9B919D5A1828F1AA9D49C712978423AD3BB94C81AA75E4AB2A39B2687379DA4C41DB428A4443AD3BB948A615A2F24C9F1418303844E226237FB3DE91B62F957EE4BCBB4D359EE18BC78703A5378103FBD05AB95172E298FE2C5136B9EBB13BFF86EDEDCB5769063CC1C46DAFC2EBAAC298D62B0845C2AAA1AB5F4DA8B93F412A4E00960BBC70B3710B24977E18A7D04A992DD688BAF916C29487EFC77A1CB8D5C7E4BCE2F0AA86BD455BC30CD7A0D8565BC58D0C24EDEB1B29618B35F44FE3A08D5A731EAD2F9272D74F9FC893902F305260DC42434C6C35F614AD09E2456CA98D79C54859320E1550AE7752C338C78436F2FFAE48E7A3F65C01F5DE11D8726C7216A6FD72785AA43B9E60CD2941D8640CC152D0DA3BDBBD5E009CEA8195021D51CB9F03A4DC3158FCC31281F6C340EF5D81CABF4C24623950F7703D0640096BB9B5EC79F62036732FC944587197DD5E7013486FAA30135126BE24DA3E98DBFD588BCFB651A934F33470D85092B6C3861E1FC99A7341E247B688E433CA1D130E499394AE1009906291E35D1C199730156FB668F1AE9F1CA3B19A7C7AB0473BCCC41318D933D302F9F391FA6CB670F1AC8952FBF6A569E722ACCF4A27A3CAC46ED5BDB6F4A435327BDBDEAE8EAAEB08596D614DE8E5512E7E29646E4929A8C18E2C8961D36E4E96E95B4A5AB2472C5D0E7F82BAED99B8F3D55C1ED1877DFDAEA2377B3CA6CFFA366E380F6A1CAE80DEA796334E942844FDB8D7D83B1AF3A2E6F3FF2190B99E6E35F5F7CD3875C1B7A4BE205C2106FAC34416AFFD6D4102AD913033F5AFA2AA33F350AF9E88446517D88B2B1B748AE6CFB7C7785CD57F337A62AB81DF32CF12F494391673BBD6FC0459541467B26F2E683CD29598B300C37AFC4D391ABA6A723BC6345018C4A3347651D2CD2986C8A3922E7459186E4921AB492F695C834924E6885A790A83C87790DA277441A5D4C354796F849A4A125C92DB0256DE6D3CC5125AE14696049B23976E557919FDD27BCBE52DA22F5B3C0CA8DA0BBADB01418C3A8C77E166894CB3A1A887ADC10AB704A278015CF27492DA501573FD4CA0DE1BB514B81A1D6498CE737562569DDD5A93119776E8CDAD7B9B353E33523F0A034118CB9F82C65EDC593F27769CC55185231165E59CFB1BD56D6E3A430EAE22DABF22CB645C488C8F596A430D8C719F6E7BFF917BE878FDECA0CB720F49E6192E62E0CEDA383830FB675EE7B20C96DEF0A9BB113FE63282323B2C3636C4406DD60C6176F6E8A865192C4651C848B9E24A50EF146F043EA6199D67A1AED10B324FC0A62E7051F77F37182BA3BDCC77FA7BD38DC27ADFC53005EFF4CC375700EAAF43AB77BADDBFC5A15464A5BFB562561353C6998BE9BD085AF67F6BFB27227D6CDDF1754D13DEB3E465AF9C43AB0FEDDBC09826154E3D7278216E6513D2009B12B5B13948B5DD995E85B3468E43E6FB675CCF0C1E24D070C29D769B4C862CC9B36802EDBA911F2F0F44F5EDA29C0A4927A6DC2CD770363434576D21F4C98F84E484C28F88E4842B8F74E785448F74E3854D8F6D61A8D0ADBDEAD4F7C68F636F4EEA25EFB54FC6DC2877F1BCA7A020B1C6950EF6E3A850BDCBD5BE00C32029B06B7DE8D989E468C6C552006846E4673C5FAA02B6C8F2B053A70B4C90EBB5D9CE86E8A471A0BBAAFC66EDDB8575B048D31FAE514C6313D7A39DAA91BEFB8D3F8AFECF19E75937C0EBDDFD628E11109140F7A3E46593F2B14BD3DCF4463E49A4B1569D1BC28AB403BBE6136726EA3D6E4453BB4A6693CDDDD2CDAD32CCA46BA7DF623D07A03B33D2A596BA9B3555AF94AB6DDD769D5F66156DB6C6D6521563BAD2DA461543B214A42A5F685D78B0855A150DB6029C3A0CA86AB4967E56151DB344D19123553881D03A29ACF65A4E406D72B12EB98AD9DE0A6B5C011E24E761AE8626CC921770F0AE3966F32F4626FB3E3831859B137EC4D52BBFF708AC4A168DE9BB1E21C8A4E335B86D26BE55C5AE906C944B55A6DC2D37D6B2111691FACD47B1B294EE166D9A37584B459066D5938C3262F6F2802557EC2E946988750EC143373D4E0845B1298A65130C229308872EE4EB7A24100C4E97348E794628A24320D2438050211C7FE540B4C83174E9E384A6F0A53244DCB60805389FF57056290B467C4B07F6346FAD37CF96D7A593312B94C03FC4D412391781C540B4C830A4E5E2329BF3B9FA246EA16A46F0261A524515414CA69C4507C632B28D597930D6E9EA71F706F62642B62EB28C8366258BDB1C9A6FA9672E2646B143C6F625CDBD4A26BC34C335E776D3C149EE8AB9F7FADC587AFCC41AC26C45DFE45EA99ED3E45E8C5E7A7F5799A2444028F4E1F920A35D089B25A74C1A3F88AA8532CA11E2A4D568D3AB893327E9E367C9EB40E650417A10AEA2C45AC844A9456A30EC9A488BEA709BE27AB4011E848191F2ED70842156CB2ACA2F364750753791C0E5565950A525658655157AA0E00C2574C360A42752441564996E619A00BCA5EA846C8A1EF543349168B54AD288B3CFA6A1541797475176B166DDD451E7DDD8A5037C3C71D64D52A1D57449C1A641B62AA9C3230E744E20A8A3A9E0F83B1D12E0F1033B04993D992FC2CC5857BE8A1BB8304046CDF616136E37D3777EF72AFC1FEDA77959D4F6937B9DDBB3870303F691C2C599CDB9A6D810445193177C3C1FBDABF6776D2A7DD3476EFE278C1F998F6D70415AE3BE552BC767924E129C7E2EB4528CCDA45E1BAA97FA10C157AAF1791F4A91D1A84DA13BD30A1DDF63AC426C0F9AF4B9878CB0AE2146186D061F6D9659E9BF03922DB7DAE45240B673B770B53E0A24DF8799C7ACFC0495132B6FEF5C2A56D65169558E13C41F726BC5FA7AB758ABA0C83279FD123F8D840577F164F906DF3E9FD2ADBCCF6D105D44C0F5B4DDF873FAD3DDF2DDB7D2DB1D65340E0F388C2D616BFCB14DBDC2EDF4AA43B214A8A0AA8105F798CF288B53C024BEEC339C0DFA6376F1BA2DF47B804CE5B659BA902A97F11ACD84F2F3DB08C419014185579F41371D80D5E7FF83F64847C0DD6CE0000, N'6.1.3-40302')
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'202012110347351_UpdateEmployeeMarintal_status', N'SCHOOL_MANAGEMENT_SYSTEM.Migrations.Configuration', 0x1F8B0800000000000400ED5D596FE4B8117E0F90FF20E82909BC6E1F3B838961EFC2EB63D7C8F8C0B467913C3568896E2BA3A35752CFDA08F2CBF2909F94BF10521225DEA2CE560F1A030CDC3CBE228BC5E255AAFADF7FFE7BFAE36BE05B5F619C785178661FEE1FD8160C9DC8F5C2E599BD4E9FBFFB60FFF8C31FFF707AE506AFD6AFA4DC312E876A86C999FD92A6AB93D92C715E600092FDC073E228899ED37D270A66C08D664707077F9D1D1ECE2082B01196659D7E5A87A917C0EC07FA7911850E5CA56BE0DF462EF493221DE5CC3354EB0E04305901079ED9F38B5FEEEF3F2E6ECFEFCE7FBEBABDBA7B5CCCFF317FBCBADDCFABDAD6B9EF01D4AC39F49F6D0B846194821435FAE47302E7691C85CBF90A2500FFF16D0551B967E027B0E8CC4955DCB45F0747B85FB3AA228172D6491A050D010F8F0B46CDF8EAADD86D978C44ACBC422C4FDF70AF33769ED93FC520745E6C8B277572E1C7B8582DAFF773843D4B556EAF94182458F8DF9E75B1F6D3750CCF42B84E63E0EF590FEB27DF73FE06DF1EA32F303C0BD7BE4F371B351CE5310928E9218E56304EDF3EC1E7A23337AE6DCDD87A33BE62598DAA9377F4264C8F8F6CEB0E11074F3E2CA58262CA3C8D62F8330C610C52E83E80348571883160C657813A470BFF4FA8213144D3CBB66EC1EB47182ED39733FBE8DD3BDBBAF65EA14B528A167C0E3D341B51A5345E43490BF5549D18A2D62E5CF41F217E89FE7EF482D6584F6F9A6EA03F8DBA41533A9D5592A995D74BB802711A207E7791D90A6527B73BB91D436EAFDCB553E8F0F6625B82ECA4D6406A61B0F2A33708175E2DD11A2077EDE77C5FF8F02BF47B96219160F2C5F387A7F21C47C1E20D82B8F3EC4AA37E70B661C6234A77E0ABB7CCE4412171B6F509FA5981E4C55BE5DBD06AF22EAA62D768083E453EAD1FCADCC53C5AC70E6644A42CF208E2254CDBAAA4B2191D345281B15348060A29DF29D753D4A3547B97AE480188BD14F88B39EADE3A195CDF84FA5D842991869DC454175F5EC627BC84A10BE3C1B9BA7A89C23ED85AB796026FF81509E9B50570DD1826C34BA3172C07A7E1464F9DD7B15585311C2F92057052EF6B29473F4568C501E19416DD3EB70ACA059CDC86C8966FB2E69232D4DACD66890B37972F5BB575ADA2CFBCBA96D1E524ADABB2D52DA4CA74DB5BBCA20E783074BAED2E4A94DDFE62CC03CFEF51FC65E147E4C03AF40A1625DE288476079E710F3CE5EC959F78C46C512F49CA74D24B0F20EE78759823ECF4D198FAA8E6E4707870D0D3F1447F50E887CE48E782BB28D5F1CCF4CEB64675679361F1CF11F6A805A9FAE3413F3DFBA63573AEC3A45A99CB1234329FDF491B134582A1753AF9B67CDF3C4F567730DD2715F773C8EB18C1E14DCB3E8DB86719D7AB94F691A9D23E3E7C7A3EFEF0EE3D708FDF7F0F8FDF8DAFC0651AEAE8C3105719B5EF3FEF7BA1AA14E6CF091A1DA924D3E3BD288A55C22CE60AF22C29D28B4863A8FEC59AA04E5FB4714B45F19616C51D6A33130889B1670369EFB0748D256E0E7C10BF75D9D2E608BB2DED985B5A10446B7C1029B6161162DFEEF8C8A8F65C2AA59B142E4B50EA7C7E278D7EBE5A21C9CE5A87354E9789C6417D03336E3CAD7B55F30261BA0931A0721185CF5E1CC0B2976DAFC31F4092A035D8FD05247D3C40E989CDA1B38E91E8CE5310AC06A7F6809F9DEED6C1D30847598A566F43F3F87B740D1CB4185C85B85667BC8F91F3255AA757A18B35F0E7D41115B221402FCD39771C744ABE46C20CDD0B7AA569B75A616DB5E943C0850FBC407E0AE0F4EA8214AD560C790961E150146BFA60F3315A7AA1595349517553F312B54D2D8A356D2A06336B695152DDD0AC406D3BF352BD9DB1B211EAFF9095C14EFF9435F5EDF2A68E68D9F061A283AF4D19A55F81BFEE9B54ABD9902981FE6743063BFDD9903513257FF5B20B7683AB075218C11B9597DF6AD4CF39AE65634F07A69B63131F4707A8A6CB7992448E97CD02CEEC911872B08D471B38ABC6AA23EF076D7981BA83C4DBC30B1D6AC299FD1781276A58B21252B0C4C284053DB479F9BC0F2FA10F53689D3BF9C745172071802B8E08E28DCBA6209186F822DF03F8B493A029E985A928FF5EE8782BE0EB9BCE55335CA870B34A027CCE255CE157B230D58F8509E5CAE852A45F92E1D855C79DD319255686D24619E7D48A86CC52A727A99318F850D0B405D164A54FECC29812288E8D0975D66877339228DA9F2BA545638C4EC961F5494B0341541BB11B88F804A450D9FE318450392E26C499BBEBCD88A0C4224829283AF3204A522843C30652A8B12BDA0E315477600C39548FCD7608226F00A19213A535442523C4A2CC5CF85426145B21788AC68F20748AB1D80E8193D829A8C44367B45049086B3A33A094681B26115DD100A26E66B49243359746104535274C88CBED1D461244FE555535D8CA27D66AA489E181B9E653BDCB6E85E653347E0471538CC576683EC5DB8C4A44EA1E6A2A4911DEE5475182352F440A4558BC520CA209F51C1B413CF52C316980D2586C13025ABCC8990A00FF3C373501E5DE0515025A3C1C8C22A02CC73620A02C4BB64E40F38758D3F1E75E65A7269EEC73F0F8FB482DBB36209B0C3F26269AF90B0BAA93A21A3016C5F3F20967C257D90756A89DC52B64523CE8F02282C1E73065EEED13DBAADE75B8E711619BC9025457AE3210FAA6BB06A8BC7593E15097A07530C5564C8A52EE7AEB40CA9B17290C75195603949FA66520E442A306A0300711AAB307D31A107E92EB002B4550039A6F976550E4C45203507C47205417346683DE1193246DF78A6D620358623EA4852D16770E969AE4A290965FB953A5141FC2F37AA7FE05B5EC103329040556FF664A0195EA825F8BD86E366101AD22346C503DECC97B2079DA6BCF0EC9631E05C628C0EE6C91B82F9270A5E691C9AA790B92F18452BF3AA6A81F964C18DC8623B2EF9B252CA97BF4307EF6A0FB41AF033AAE681E3A06628BF071A1C812EDF5BBD1053CD5F67225D3B04175E53E100B645FA5895CA8BB1336BD15A63A51ACA11A4E682E71291CC9BADC9929C2F71C2247B4979346D793541FCA6D80861BAA0BC981E44265A72C72C2E4D6ACC9BD19D5A1421E355CA9B9E352C809E94CEF5C227B9C7A2EC9AE6E9A5CDE74E21277D1A2E012E94CEF5C2AA6693D9324D7070D2E103AB1883DECF7A46F88E55F792E2DF34E67B9E7F022E174A670317E7A0B562B2F5C522EC78B146B9EFB1BBFF86EDEDCF7769063CC1C86DBFC29BAA494463158422E1791462DBDF6E224BD04297802D8EEF1C20D8462D253B8E21C4148B2076D7118C9918294C77F17BADCC827B8E4FEA280BA465DC51BD3ACD750D8C68B152DEC051E2B6B8931FB45E4AF83507D1BA3AE9D7FD242D7CF53CC11982F30692026A3311EFE0A5382F624B152C672CD7155B809128652B8AF6325C3486EE8E3459FB2A33E4F19C88FAEF24E86262743D4D9AE4F11AA2EE59A4B90A6EE3002C43CD1D230DAB75B0D9EE0AD9A011572CD910BB7D4345C91648E41F960A371A86473ACD20B1B8D5426EE26A0C9042C4F37BDCE3FC501CE64FA29AB0E33FBAACF03680CF547036A24D6C49B46D31B7FAB1179FFCC34269F678E1A0A0B56D870C1C2E5334F693C4896688E433CA1D13024CD1CA570804C8314494D7470E65C80D5BE5952233D5E7927E3F47895618E973928A671B204F3FA99F361BA7E96D080AF7CFD55B3FA945361A61755F2B01AB56F6DBF290D4DDDF4F6AAA3ABB7C2165A5A53793B76499C8B5B1A91CB6A326388235B76DA90D4DD2E694B7749E489A1CFF9573CB3379F7BAA8ADB31EFBEB5DD47EE669539FE47CDE601ED4395D11B547A6334E94684CFDBCD7D83B9AFBA2E6F3FF3190B99E6F35F5F7DD3975C1B1A25F1016188112B4D90DA8F9A1A42C57B62E047735F65F4A746211F9DD028AA0F5136368AE4C9B6CFB12B6CBE9A8F98AAE276ACB3C4BF240D45D2767ADF4016550619ED2591371F6C2E92B508C3C8E695783B72D5F4768477AC28805179E6A8AC83451A93CD3147E4BC28D2905C568356D2BE129946D219ADF0141C959730A7207A47A4D1C55C7364899F441A5A92DD025BD2663ECF1C55E24A910696649B63577E15F9D57DC2FB2BA52D523F1BACDC08BADB0E4B81318C7AEC678346B9ACA381A8E4865885533A01AC489FA468290DB8FA11ADDC10BE9B682930D43A89F1FCC6AA24ADBB3A3526E3CE8D51FB3A77766ABC66023CA89808C65C7C91927A9152FE2E8DB90A432AC6C22BEB39B6D7CA7A9C14465DBC65555EC4B6081B9170BD25290CF67181FDF96FFE85EFE1ABB7B2C02D08BD6798A4B90B43FBE8E0E0836D9DFB1E4872DBBBC266EC84FF18CAC888ECF0181B91413798F1D59B9BA2619424711907E1A22749A943BC11FC907A98A7B59E463BC42C09BF82D879C1D7DD7C9CA0EE0EF7F1DF692F0EF7492BFF1480D73FD3701D9C832ABDCEED86759B875561A4B4B5A32A09ABE149C3F4DD842E7C3DB3FF95D53BB16EFEBEA0AAEE59F731D2CA27D681F5EFE64D100CA31A0F9F085A9847F58024C4AE6C2DA05CECCAAE82BE459346EEF3665BE70C1F4DDE74C2907A9D668B2C08BD6903E8BA9D1A218F5FDF699A8592E545006A1377BE1B181B33B2530F9978F19D909898F01D9184B8EF9DF0A8D8EE9D70A8F8EDAD551B15BFBD5B9FF818ED4F5E3AAA9EED7305681347FCDBD0DA13D8E948A37B77D3295C04EFDD4E679019D834CAF56EC6F4346364BB02313274F39D86647FD015B6C79D021D41DAE4A8DD2E607437C5230D0ADD5763B76EDEAB4D83C698FD7211C6C13D7AB9E3A99BEFB8D3F8AF2C79CFBA493E87DE6F6B94F188188A273D1FACAC9F1D8ADEB067A2C172CDB98AB4685E9555A01D47980DA1DBA83579D50EAD691A5877B78AF6B48AB2216F9FFD08B43EC06C8F4AD69AEC6C9556BE921DF7755AB57DBCD536475B59ACD54E7B0B693CD54E889298A97DE1F5C242554CD43658CA78A8B2E96AD259797CD4364D53C646CD1462C7C8A8E66B19A9B9C1FD8AC44C666B17B8696D708400949D26BA186472C8D383C2CAE59B8CC1D8DBEAF8208658EC0D7B93A2DD7F5C45E25934EFCD58010F45EF992D63EAB5F232ADF48764A25AAD3671EABEB5D888B433566ADC460A58B859E9D17A44DAAC046D595CC32683379400550EC3E94698C752EC143C73D428855B12A1A65154C2294810E5E59D6E45834888D397219D778A290A916944C1290810F1F04FB5C0348AE1E40547E956618A42D3322AE054020156111924ED1931FEDF9821FF349F809B3ED68C245CA691FEA6A09148600EAA05A6D10527AF91941FA04F5123758BD63781F85292702A0AE534624CBEB11594EA13CA062FCFD38FBC3731612B82EC28846DC4F87A630B9BEAA3CA890B5BA3287A1393B54D6DBA362C69C6FBAE8DC7C4139DF6F3C35A7C01CB5CC46A62DDE59FA69ED9EE5384063EBFADCFF324B1127874FA9254A04067CAA8E8A248F184A85B2C810E952723A38EF2A40CA4A78DA327A5A10CE52290A0EE52442254A6948C3A3693220C9F260A9F8C8022E29132505CAE1104126CB68CD079B2BA83A93C20878A58A5829404AB226AA2EA48203C61725010C8910C19912CCF33401794BD404628A1EF54334E169B542D2B8B327AB28AE83C3ADAC59E454BBB28A3A7AD8879337C004256ADD20146C4A5417620A6EA2923744E24C0A0A8E3F978181BEDF200C1039B3499ADC9AF525CDC871EBA3B4864C0F61D165633DE8973F72EF71AF5AF7D57D9F594F697DBBD8B0347F59306C49205BCAD3916485094A173371CC5AFFD38B38B3EEDAFB17B17C78BD2C7B4BF26BA70DD2D9762D8E52185A71C94AF17A6307B17850FA7FE9932540CBE5E58D2A7766810734F74C7844EDBEB109B00E7BF2E61E22D2B885384194287396797656EC2E7881CF7B91691229CEDDC2D4C818B0EE1E771EA3D032745D9D8FAD70B97B69559546285F304DD9BF07E9DAED629EA320C9E7C468FE06B031DFD2CB020DBE6D3FB557698EDA30BA8991EB69ABE0F7F5A7BBE5BB6FB5A62ADA780C0F71185AD2D1ECB14DBDC2EDF4AA43B215C8A0AA8605F798DF288B53C024BEEC339C0DFA6376F1B12BF8F70099CB7CA365305523F102CDB4F2F3DB08C41901418557DF413C9B01BBCFEF07FAC6380D300CF0000, N'6.1.3-40302')
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'202012161516408_EditEmployeeDOB', N'SCHOOL_MANAGEMENT_SYSTEM.Migrations.Configuration', 0x1F8B0800000000000400ED5D596FE4B8117E0F90FF20E82909BC6E1F3B838961EFC2EB63D7C8F8C0B467913C3568896E2BA3A35752CFDA08F2CBF2909F94BF10521225DEA2CE560F1A030CDC3CBE228BC5E255AAFADF7FFE7BFAE36BE05B5F619C785178661FEE1FD8160C9DC8F5C2E599BD4E9FBFFB60FFF8C31FFF707AE506AFD6AFA4DC312E876A86C999FD92A6AB93D92C715E600092FDC073E228899ED37D270A66C08D664707077F9D1D1ECE2082B01196659D7E5A87A917C0EC07FA7911850E5CA56BE0DF462EF493221DE5CC3354EB0E04305901079ED9F38B5FEEEF3F2E6ECFEFCE7FBEBABDBA7B5CCCFF317FBCBADDCFABDAD6B9EF01D4AC39F49F6D0B846194821435FAE47302E7691C85CBF90A2500FFF16D0551B967E027B0E8CC4955DCB45F0747B85FB3AA228172D6491A050D010F8F0B46CDF8EAADD86D978C44ACBC422C4FDF70AF33769ED93FC520745E6C8B277572E1C7B8582DAFF773843D4B556EAF94182458F8DF9E75B1F6D3750CCF42B84E63E0EF590FEB27DF73FE06DF1EA32F303C0BD7BE4F371B351CE5310928E9218E56304EDF3EC1E7A23337AE6DCDD87A33BE62598DAA9377F4264C8F8F6CEB0E11074F3E2CA58262CA3C8D62F8330C610C52E83E80348571883160C657813A470BFF4FA8213144D3CBB66EC1EB47182ED39733FBE8DD3BDBBAF65EA14B528A167C0E3D341B51A5345E43490BF5549D18A2D62E5CF41F217E89FE7EF482D6584F6F9A6EA03F8DBA41533A9D5592A995D74BB802711A207E7791D90A6527B73BB91D436EAFDCB553E8F0F6625B82ECA4D6406A61B0F2A33708175E2DD11A2077EDE77C5FF8F02BF47B96219160F2C5F387A7F21C47C1E20D82B8F3EC4AA37E70B661C6234A77E0ABB7CCE4412171B6F509FA5981E4C55BE5DBD06AF22EAA62D768083E453EAD1FCADCC53C5AC70E6644A42CF208E2254CDBAAA4B2191D345281B15348060A29DF29D753D4A3547B97AE480188BD14F88B39EADE3A195CDF84FA5D842991869DC454175F5EC627BC84A10BE3C1B9BA7A89C23ED85AB796026FF81509E9B50570DD1826C34BA3172C07A7E1464F9DD7B15585311C2F92057052EF6B29473F4568C501E19416DD3EB70ACA059CDC86C8966FB2E69232D4DACD66890B37972F5BB575ADA2CFBCBA96D1E524ADABB2D52DA4CA74DB5BBCA20E783074BAED2E4A94DDFE62CC03CFEF51FC65E147E4C03AF40A1625DE288476079E710F3CE5EC959F78C46C512F49CA74D24B0F20EE78759823ECF4D198FAA8E6E4707870D0D3F1447F50E887CE48E782BB28D5F1CCF4CEB64675679361F1CF11F6A805A9FAE3413F3DFBA63573AEC3A45A99CB1234329FDF491B134582A1753AF9B67CDF3C4F567730DD2715F773C8EB18C1E14DCB3E8DB86719D7AB94F691A9D23E3E7C7A3EFEF0EE3D708FDF7F0F8FDF8DAFC0651AEAE8C3105719B5EF3FEF7BA1AA14E6CF091A1DA924D3E3BD288A55C22CE60AF22C29D28B4863A8FEC59AA04E5FB4714B45F19616C51D6A33130889B1670369EFB0748D256E0E7C10BF75D9D2E608BB2DED985B5A10446B7C1029B6161162DFEEF8C8A8F65C2AA59B142E4B50EA7C7E278D7EBE5A21C9CE5A87354E9789C6417D03336E3CAD7B55F30261BA0931A0721185CF5E1CC0B2976DAFC31F4092A035D8FD05247D3C40E989CDA1B38E91E8CE5310AC06A7F6809F9DEED6C1D30847598A566F43F3F87B740D1CB4185C85B85667BC8F91F3255AA757A18B35F0E7D41115B221402FCD39771C744ABE46C20CDD0B7AA569B75A616DB5E943C0850FBC407E0AE0F4EA8214AD560C790961E150146BFA60F3315A7AA1595349517553F312B54D2D8A356D2A06336B695152DDD0AC406D3BF352BD9DB1B211EAFF9095C14EFF9435F5EDF2A68E68D9F061A283AF4D19A55F81BFEE9B54ABD9902981FE6743063BFDD9903513257FF5B20B7683AB075218C11B9597DF6AD4CF39AE65634F07A69B63131F4707A8A6CB7992448E97CD02CEEC911872B08D471B38ABC6AA23EF076D7981BA83C4DBC30B1D6AC299FD1781276A58B21252B0C4C284053DB479F9BC0F2FA10F53689D3BF9C745172071802B8E08E28DCBA6209186F822DF03F8B493A029E985A928FF5EE8782BE0EB9BCE55335CA870B34A027CCE255CE157B230D58F8509E5CAE852A45F92E1D855C79DD319255686D24619E7D48A86CC52A727A99318F850D0B405D164A54FECC29812288E8D0975D66877339228DA9F2BA545638C4EC961F5494B0341541BB11B88F804A450D9FE318450392E26C499BBEBCD88A0C4224829283AF3204A522843C30652A8B12BDA0E315477600C39548FCD7608226F00A19213A535442523C4A2CC5CF85426145B21788AC68F20748AB1D80E8193D829A8C44367B45049086B3A33A094681B26115DD100A26E66B49243359746104535274C88CBED1D461244FE555535D8CA27D66AA489E181B9E653BDCB6E85E653347E0471538CC576683EC5DB8C4A44EA1E6A2A4911DEE5475182352F440A4558BC520CA209F51C1B413CF52C316980D2586C13025ABCC8990A00FF3C373501E5DE0515025A3C1C8C22A02CC73620A02C4BB64E40F38758D3F1E75E65A7269EEC73F0F8FB482DBB36209B0C3F26269AF90B0BAA93A21A3016C5F3F20967C257D90756A89DC52B64523CE8F02282C1E73065EEED13DBAADE75B8E711619BC9025457AE3210FAA6BB06A8BC7593E15097A07530C5564C8A52EE7AEB40CA9B17290C75195603949FA66520E442A306A0300711AAB307D31A107E92EB002B4550039A6F976550E4C45203507C47205417346683DE1193246DF78A6D620358623EA4852D16770E969AE4A290965FB953A5141FC2F37AA7FE05B5EC103329040556FF664A0195EA825F8BD86E366101AD22346C503DECC97B2079DA6BCF0EC9631E05C628C0EE6C91B82F9270A5E691C9AA790B92F18452BF3AA6A81F964C18DC8623B2EF9B252CA97BF4307EF6A0FB41AF033AAE681E3A06628BF071A1C812EDF5BBD1053CD5F67225D3B04175E53E100B645FA5895CA8BB1336BD15A63A51ACA11A4E682E71291CC9BADC9929C2F71C2247B4979346D793541FCA6D80861BAA0BC981E44265A72C72C2E4D6ACC9BD19D5A1421E355CA9B9E352C809E94CEF5C227B9C7A2EC9AE6E9A5CDE74E21277D1A2E012E94CEF5C2AA6693D9324D7070D2E103AB1883DECF7A46F88E55F792E2DF34E67B9E7F022E174A670317E7A0B562B2F5C522EC78B146B9EFB1BBFF86EDEDCF7769063CC1C86DBFC29BAA494463158422E1791462DBDF6E224BD04297802D8EEF1C20D8462D253B8E21C4148B2076D7118C9918294C77F17BADCC827B8E4FEA280BA465DC51BD3ACD750D8C68B152DEC051E2B6B8931FB45E4AF83507D1BA3AE9D7FD242D7CF53CC11982F30692026A3311EFE0A5382F624B152C672CD7155B809128652B8AF6325C3486EE8E3459FB2A33E4F19C88FAEF24E86262743D4D9AE4F11AA2EE59A4B90A6EE3002C43CD1D230DAB75B0D9EE0AD9A011572CD910BB7D4345C91648E41F960A371A86473ACD20B1B8D5426EE26A0C9042C4F37BDCE3FC501CE64FA29AB0E33FBAACF03680CF547036A24D6C49B46D31B7FAB1179FFCC34269F678E1A0A0B56D870C1C2E5334F693C4896688E433CA1D13024CD1CA570804C8314494D7470E65C80D5BE5952233D5E7927E3F47895618E973928A671B204F3FA99F361BA7E96D080AF7CFD55B3FA945361A61755F2B01AB56F6DBF290D4DDDF4F6AAA3ABB7C2165A5A53793B76499C8B5B1A91CB6A326388235B76DA90D4DD2E694B7749E489A1CFF9573CB3379F7BAA8ADB31EFBEB5DD47EE669539FE47CDE601ED4395D11B547A6334E94684CFDBCD7D83B9AFBA2E6F3FF3190B99E6F35F5F7DD3975C1B1A25F1016188112B4D90DA8F9A1A42C57B62E047735F65F4A746211F9DD028AA0F5136368AE4C9B6CFB12B6CBE9A8F98AAE276ACB3C4BF240D45D2767ADF4016550619ED2591371F6C2E92B508C3C8E695783B72D5F4768477AC28805179E6A8AC83451A93CD3147E4BC28D2905C568356D2BE129946D219ADF0141C959730A7207A47A4D1C55C7364899F441A5A92DD025BD2663ECF1C55E24A910696649B63577E15F9D57DC2FB2BA52D523F1BACDC08BADB0E4B81318C7AEC678346B9ACA381A8E4865885533A01AC489FA468290DB8FA11ADDC10BE9B682930D43A89F1FCC6AA24ADBB3A3526E3CE8D51FB3A77766ABC66023CA89808C65C7C91927A9152FE2E8DB90A432AC6C22BEB39B6D7CA7A9C14465DBC65555EC4B6081B9170BD25290CF67181FDF96FFE85EFE1ABB7B2C02D08BD6798A4B90B43FBE8E0E0836D9DFB1E4872DBBBC266EC84FF18CAC888ECF0181B91413798F1D59B9BA2619424711907E1A22749A943BC11FC907A98A7B59E463BC42C09BF82D879C1D7DD7C9CA0EE0EF7F1DF692F0EF7492BFF1480D73FD3701D9C832ABDCEED86759B875561A4B4B5A32A09ABE149C3F4DD842E7C3DB3FF95D53BB16EFEBEA0AAEE59F731D2CA27D681F5EFE64D100CA31A0F9F085A9847F58024C4AE6C2DA05CECCAAE82BE459346EEF3665BE70C1F4DDE74C2907A9D668B2C08BD6903E8BA9D1A218F5FDF699A8592E545006A1377BE1B181B33B2530F9978F19D909898F01D9184B8EF9DF0A8D8EE9D70A8F8EDAD551B15BFBD5B9FF818ED4F5E3AAA9EED7305681347FCDBD0DA13D8E948A37B77D3295C04EFDD4E679019D834CAF56EC6F4346364BB02313274F39D86647FD015B6C79D021D41DAE4A8DD2E607437C5230D0ADD5763B76EDEAB4D83C698FD7211C6C13D7AB9E3A99BEFB8D3F8AF2C79CFBA493E87DE6F6B94F188188A273D1FACAC9F1D8ADEB067A2C172CDB98AB4685E9555A01D47980DA1DBA83579D50EAD691A5877B78AF6B48AB2216F9FFD08B43EC06C8F4AD69AEC6C9556BE921DF7755AB57DBCD536475B59ACD54E7B0B693CD54E889298A97DE1F5C242554CD43658CA78A8B2E96AD259797CD4364D53C646CD1462C7C8A8E66B19A9B9C1FD8AC44C666B17B8696D708400949D26BA186472C8D383C2CAE59B8CC1D8DBEAF8208658EC0D7B93A2DD7F5C45E25934EFCD58010F45EF992D63EAB5F232ADF48764A25AAD3671EABEB5D888B433566ADC460A58B859E9D17A44DAAC046D595CC32683379400550EC3E94698C752EC143C73D428855B12A1A65154C2294810E5E59D6E45834888D397219D778A290A916944C1290810F1F04FB5C0348AE1E40547E956618A42D3322AE054020156111924ED1931FEDF9821FF349F809B3ED68C245CA691FEA6A09148600EAA05A6D10527AF91941FA04F5123758BD63781F85292702A0AE534624CBEB11594EA13CA062FCFD38FBC3731612B82EC28846DC4F87A630B9BEAA3CA890B5BA3287A1393B54D6DBA362C69C6FBAE8DC7C4139DF6F3C35A7C01CB5CC46A62DDE59FA69ED9EE5384063EBFADCFF324B1127874FA9254A04067CAA8E8A248F184A85B2C810E952723A38EF2A40CA4A78DA327A5A10CE52290A0EE52442254A6948C3A3693220C9F260A9F8C8022E29132505CAE1104126CB68CD079B2BA83A93C20878A58A5829404AB226AA2EA48203C61725010C8910C19912CCF33401794BD404628A1EF54334E169B542D2B8B327AB28AE83C3ADAC59E454BBB28A3A7AD8879337C004256ADD20146C4A5417620A6EA2923744E24C0A0A8E3F978181BEDF200C1039B3499ADC9AF525CDC871EBA3B4864C0F61D165633DE8973F72EF71AF5AF7D57D9F594F697DBBD8B0347F59306C49205BCAD3916485094A173371CC5AFFD38B38B3EEDAFB17B17C78BD2C7B4BF26BA70DD2D9762D8E52185A71C94AF17A6307B17850FA7FE9932540CBE5E58D2A7766810734F74C7844EDBEB109B00E7BF2E61E22D2B885384194287396797656EC2E7881CF7B91691229CEDDC2D4C818B0EE1E771EA3D032745D9D8FAD70B97B69559546285F304DD9BF07E9DAED629EA320C9E7C468FE06B031DFD2CB020DBE6D3FB557698EDA30BA8991EB69ABE0F7F5A7BBE5BB6FB5A62ADA780C0F71185AD2D1ECB14DBDC2EDF4AA43B215C8A0AA8605F798DF288B53C024BEEC339C0DFA6376F1B12BF8F70099CB7CA365305523F102CDB4F2F3DB08C41901418557DF413C9B01BBCFEF07FAC6380D300CF0000, N'6.1.3-40302')
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'202204260728103_initProject', N'sambocappweb.Migrations.Configuration', 0x1F8B0800000000000400ED3DDB6EDC3A92EF0BEC3F34FA69779071DBCE497026B067E063C7279EDC8CD809F6CD90BBE9B636BAF448EA249EC57ED93EEC27ED2F2C29EAC24BF12651ADEE9C4680C02D92556455B1582C168BFFF73FFF7BF2B71F7134F986B23C4C93D3E9D1C1E174829279BA0893E5E9745D3CFCF9D7E9DFFEFAAFFF72F27A11FF987CA9EB3D27F570CB243F9D3E16C5EAD56C96CF1F511CE4077138CFD23C7D280EE6693C0B16E9ECF8F0F02FB3A3A319C220A618D66472F2699D14618CCA1FF8E7799ACCD1AA5807D1FB7481A2BCFA8E4B6E4AA8930F418CF2553047A7D33C88EFD379B05A7D47F707B4FA7472168501EECA0D8A1EA6932049D2222870475F7DCED14D91A5C9F266853F04D1EDD30AE17A0F4194A36A00AFDAEAB663393C266399B50D6B50F3755EA4B123C0A3E715716662F34E249E36C4C3E47B8DC95C3C915197243C9D9E61E06B4CEC1825C57422227C751E65A43248E503B6EDB3095BE35923195880C8BF6793F37554AC33749AA0759105D1B3C9F5FA3E0AE76FD1D36DFA1525A7C93A8AD8AEE2CEE232EE03FE749DA52B94154F9FD0433580ABC57432E3DBCDC4864D33A60D1DD655523C3F9E4E3E60E4C17D841A4960487053A419FA1D25280B0AB4B80E8A02650981814A5A4AD8055C3591083E335E3B5817B8273524F2F72D9E3A9D8111EEDD8645D440C413044FF6E9E47DF0E31D4A96C5E3E9F4C5119EDE97E10FB4A8BF54283E2721D60DB85191AD3D74E1EDE3E63BF17915A5C1022D2E432F24B043F6DB9306D5F18B17430CF4060BF43AD70DD10F7D3F04DFC26539773453613AF984A2B252FE18AEA8A6E614CA1D5FFB324BE34F6924080D57E9EE265D6773C2C3D454F336C896A8E07B7E326B95A395CAA41DEBA632498DBDCA54E022FF6F7E7A90FFB5FAC70B5E7B29CBF37099FC3DBD7716B1BAE15EBE14B8306DFAAEC4D88ECC5A1880B01C1DFF3AC88A5932172DFC58001530ED62E46B20CA55A114716831A8E5F8AEACC12C006C81ACF4B95248D1EB3A43F86AE80DAD0275879468FA5316F75A793A6883BD1E50E3BA40AB202B88752061D537BCCEC2342B11A8A78D0F6BF1667DFF9F685E68D10C62135FA07C9E852BBAE7D421F731CA772858DC3EA2F70815188317CB588FB0C2F4294DE3C17161A33B2BBC68EBD78942EBEB9B5DE5D570EB86BFA5582F058933FEABFC360BBEA128F200EB3C8D571122E2D56548C68D0CFED303EBB04CC6E83C8DD26CE8797E955F62FA5D044FBDE99A21A2093F26BDA5AD8234D23E352BF0B76586F2DC5536F02CE9DAF43C4A730491CEC4BC0B8465192D9C99A7B442DA8509B4458831C156696D11BE44B24584E25EB608DB013793A46DB9B74C14B836B103B6DF89AE5698E6E5B8A8EDEBB81FE59BEF28CF37B7C5BB0CB3BC18C705F22E180B3359FFC85F57C9DBC718E916DC813A709E2645302FAE1FD36484E1634DB1D08EFAE51058AFD33C3498F8038DF716AF98CB2C88B5F36B20DC57F9CD1AFFA6AAAC9FB9F53A0EC2483B023BBE5960C1F2F9106671072B43647A90E7DFD36CF126C875CE4E3F26F40D9AAFC946199B74F16A706CE5E4FDB08EEFB553C93B2E6FACB9FD9E5E621D9466AF13D2AA37BC77E9FC6BBA2EAA8DE3E762EE6ADA3600BC74E76C3EC756F9251666B4384FD74961B2A7CC2E58E362E5476F2A0DF5F32808E31C7618F266CF5D5D95711C82356407225CCDD5B3F92E5D86895D57EBAAEAAED21AC6AE56D55CBB4A80D9F5B4AAA9EE6859C1D84F5AABD776A8B6EC09BC92433A33F97D13AF7196AF3EA0E2A06E7D40E15EE2A5116125FDF54002FB6C62DDB8B5AD8F6D6DEBE747F70FCF7F7DF132583C7FF90B7AFEE267DC5B8D757453B28F9E190FBC369598BE04D1DA37AA4EB3A15402FE67430976FB6743D94DFCF95B581AF833738BBA32066F55BF9667D73927F46CD3D3811BE6A6916F4607749A2E642DF23F5B08D4ED9F2CB0288355C980BA48FD58DABFEEEF9648DC6F51BA3CC7CBE732CD9E5C1D7A6CDB1DF5E6FDC13CB88463D769EEECA9AFDBEDB9ACC0C54E85DEC1BDEBE2517BD4E8E7409AF0D3CB79B42966D8D321AC312CD8164F0787304A74B1179E8C758A678C21DE3EAEE3FB44EFC21C083591426FC7F88EB8BF84E87BE97F6ACD5ECB19ABF458F0CB29E0B8A835E91D5FB3755C801524C7055CABDF396E3A5F93B3D8AED680D87EBF5628706D95455073ED1A657198E7E5215037BEB710F69C57E01A6BCF51B3886A1167FB441D9EC2C105F59D2C1D7762AB56F7192B4B7AD0DCC28B4EACD1769B19B4F57E56EC923EECCAEB3D97555C361DC70E156160D89D1C1D1E0F12396D70F00F7385B1A7A25740F3B5B1B5BC6069CD1153F854846EC27FEA558BC79B9C5D0298C7BC05DA3D5C550A522DC16C205EBFC2D485D21FD2422F73DD6E30184D23ED66B0AE7427D7968D22A992D218926BBA1EBC3B187616E69CAD11E7D574BB4468711FCCBFBA2EE475BBFD42AE9A49A3C4859A23EC86B98260BC7835503282F728CF83A596D0BFF8B8F2E572E5B0BB8F806BBC9F590A5CA3B907D4D71F07BCB9C26ED9557758C03AD06D16B862AF05A40C8B7813623177F785B26DF7E2AEC08589B434988D7E4E5628A24E7726AFCF160BF60AD9607D7C931A6F5C0C1AA84565AE4A2EE526EC4CD3BDAC2B7031342228FBEEA419705E8E70D9EE8D93014AECC13E01D4B0176BC74D00254C07D04460EADC49F55B2B41534D321474757BD90A52FF3A6B505269AF4515B8C6D97A92FFB7281FD4A72071766A90367BA9DA2AA9222C611268D8D801F622825669E67C84455BEDC544816BB480E1922D3EACC68FA52F2BFC36C671D8973018FEC86753A17A17EB8C5A2BFDF8A1490964708676CB00F42EAD3778433302CF6A94A1643EC4319FFAB6603351407B9216DFB1B55A2B522A946C47B986B794721568319F1CF359D59BFE99E4587274592EF6F6EAAE44B6D49DF67FAF6837EE14ED4ADA17A3D00C7C959DE814F8D235CBEFBBAA5AABAEE452496B01555C94D7599EA7F3B0EC557D554299279A1F2C5E3027D649A329E5C534D49809584843725F1C77ED74FA2789A23648EAE10248A80EE6911C4948AA95953C5980AD9D1CCF96302964E10F9379B80A22DBFE08002CE70FE154834A2CB9402B92CB26296C096FD3073187BEDC9F06AD30D34D743B9931D2A5173A21EB9A4A085429D85ACE97595CEDA54A91B48D01C89EA3F1700F0F0E3CC912DC8B0D08104C4F1BC4FCF9E62832C3E73056EA0D38A131A32BDA5CE00EDA084C84AC97435F8A0742BD09650311D2066F95117C1C198113C528D96AC81AC3488D98B7CFC46B2312408880BC272611ED2652DA616F42B6B424B1E980F276FB185256E5F8B1150031E1CF205226A40B524859954F642352C60F7B0429E349B2735246F333D9F25F48D634888CF1A99E142246F7CA1B91306ECC230818478FED972FFE1506B3E9C37BD1BC59549C03AE8FA0F634AFD87E6CD4BE62E9BAF552035FBE5671D97013BB65769B55C35E84F417B805E0EDB58041C448DB970D489396D036F8C5EC1BA30897F9A6AB4A161CAEBD327B7DE0E2B6BDF8D9DF9B0550D6BD194418AD7BB601C1B4668C951742BA9B37AA98CA778E4CB2A2B98024CB48176154DF5B02100CAC138D7DDAA0F82909EF22765BA31F5DB5A2832EEC25745BA2F7C6D776BBAFE394B74B34AE75C35513CE5BDA6DCD355E53B1F2E4FB73E36B3BB31987BE96E83BE2DAD7C529AB44C12A68B99506EE7286BDC0D9843BC358063C8BB4E8D40664CF820136BD00AE758C2283726C934A2634814EAD24D4F1A3F6A2A68E8E92C00E285BCA5E6C40A29484B5C1CDC7788E29425A771A1891E6416CC6F5A2019DD89CBCEC94FF0C0800527156170D24FBDA213FBBCE99AF0924DABC275F3DD40D88919A12567A074C9E3D8834BD2EBB59BEAE15262893CF892EEECBF0ED1FD0BD053CB02A3E31AFE2DB44C920C06F5001C485E5D3C9EB269C0C0CEC92C40C0656A2D700A30B9B0958ED390721B5871106300A00364D5B931982C06E3C0C80AA08407914A2D636C091220720A040788103D8FA211B2DD8EA3CD9016CF5EA8C162A553C06A08C033B0421F2A70016D0883F5D05889E5598C484F71BC1DD929D719650DB8D9F0E2CBBCFB6044C35AF0E68BD0C5902D4813202A993094140DA0445E6B9AE2797E0913080637253C04CE5135F18A0313B200816B773B507A552B5D216D500925CB283E0D03B94A6C6A5850636AF0C5D2B00AAB1B0BB211320839E01750CB30EC30B231F303D61EA03ABA426BA9AB3CCCCF1D5CDE0A4655AB2425C22AA01B015E5C5A07F9E301644139F599629A50B09167D80907B91E93C5DD935A45084013320B815BEF7E8F9D055484CD4B1AD3C0FC1E856966F8C6DA49305309ED540C22E03573CDC0750C02272931F823E7693194B6564E9C8A10FD16460418696772AD5A696994A50E4A1766442EC612F2A0921860A2AD583F14EA54AA39B890404CE69C7C587CEF522111F21A7A05035108F5A86EE19746A46F65129F403E7A8EAA36838079589A81D46AF781542268245041337047D0C13331266CFA0A1883E6A4900C7EE1C7A53C8228FBC4C2DC7901CF07CD81C94C3AEBCD0F646434FFB301C0049B3D5F1465C200FAD9AA8860012709CEA1012607C76A453078D0020071149074174173F07A173A4D726054C9DEE11B4A52D0EEFED4ED661FBDA76761A0FEC07B3B9B5B9AF6492591F413B1F423323E477FA1AB2D91C3BC3707D6DD880540F32D10C67A696A7A6CC401A7F818636EA735209906762A8CC2AE5B99FF1E4AFDBD08736A5A04BEAF2A84D2759B667592C0D204BD8F6D4CA93755DDF9C6F8E599AB293D9CDFC11C541F5E16486ABCCD1AA580711CDE75117BC0F56AB3059E66DCBEACBE46615CCF128CEFF7C339DFC88A3243F9D3E16C5EAD56C9697A0F383B84901314FE359B04867C787877F991D1DCD620A6336E7E82C1E0A35988A340B96482825F97D16E832CCCA770B83FB806435385FC45235F05049E161AB5142E74632276BFF5BDD8AFC4D5B421952B85326F938AE027289874A6A94A3460AEF98DC1C03B89907519001292BCED3681D27EAF345756BF1823D0BC974F9DE0C95262D8260D21277888436B7346329049629EE019B24FFD3422715ECE1F3694659C07C893B44924B1482F71BF080B81A5A9D1D9485547F93A19CCC0439968E92A569236833712E3ACD54BA4AFA9AA99019E03053E1E6C3CC549A26866D4FBFB8411045BBFEB63D7C6E5D35BD99DC9CAE77E0B0BAED30ECAD1211B00014B909D430EA58214E2528E287D450E8D0EBFCE99C1EE44ADC218AEA8AFDBE3502E845F4BA09DD06C58D8FA366E1E823ACD510AFB330CDC2426071FBD5613DAADF60E116A4FAA3CB18F37916AEE859343F44A6C01EDE3B142C6E1FD17B840ACC455198E5527BC855A34F691AF340B902A715BD4E34292CEAF5677B584DEE491652F3D11ECE555E0D4610DBF6B30BACDB2CF886A20800C796D8433C4FE35584CA1D843458B16C28CB4A05054B558C3008F2463C0B89FDEE42BBCB75145D044F22E19ACF0E54CB10C993F851985FCC676758E2B4623E3BCA3FFEB6A44F794873A02D729A073044AEC061BC519A238972F547177E36CFFAF1FC6C3E6FCDD2CAFA4CFBAEB06A3FB1C542AB6B3CB6F53E126F543E3147935B0805ED60789B200CC39FD2CB243389F9EC60270410A8F6AB43AFB04A267F5D256F1F6324A87FA9D065B54B8A605E5C3FA689B4D6B125F6107F27C1E34207EB6F0E36649A87B2B9D67E755833B1F25B66412C4A05FBDD45C7DEACF16FEA52E7B52C53E0B096D0C70DB955847E728481D9F51066B1A8FCC532070E0479FE3DCD166F825CF014F0250E6B319AAFC90E002FBCF14A588BF922875E12F1AC1F98E63AC9167482A7A0285CC3411EBFA797785EA5D9EB8424E915A5522A75D036E9FC6BBA2E2A8BFC733117D48E5CDC0136D067B1CCC11D309F635BE9128B285A9CA7EB44D8E901C56E2E1059F7B65FB766BD0562F3FA2EBEF29509F7E5D702C6300BB01FDF55D9657A00CC9BD5CD6747585F82680D01ABBE6FA53829E32CBB8B13BD2AD34F9C1430D4BA0757C7DFBE8592612114B9F8A8689BB748725331054309ED1688862ABAB4BB64945711FA09060C625845515F8464A1A82E478EC63921A8B22FDB7421A4162CD337FF836E9BDB305A1FDC292FEA75E30CDC7418AE88E9F75848A6D47C6AA867EBE2517471D6DF9CB69080E7BBFDEA60B2CB6106CEA105603841871082E6A12B71AF8EDCCCE3AA89D823E6B38B5B1A6F87126923CB7C76E31AE42C67BFDB43FB12A2EFE5CE41341CB882ADD12140B0726FEFA82154DBC6476A04F10755F950CCBF2F7E31D77ABB734C07649B376F723239EED0D6986A6E7489A8E3E77D498332AB88AD24A800FCC167AE3F0EF5E0CDC6B802B849DD3DA45E8C21C92FE3EA92E9AF214C905566AD4D56553574FF11A0A4FE4DF84FE9B8AAFEEADE37D968E64BC68A4EED72BEADE4326D22768CF9EC0C4B261B57E0304F531110FDB235DAB3CD6DD2577B369950DCB5A7BAE9D86BDA90677DBE22E2DEA33C0F96C2809A8F5B2369C295BDBEE2C6A7CB71973943FBAD36A93B87798EC47A2159515FD673A98DDC39AF6F3E0CE331CEA5BC44B55F5D21C90B14FBDD611DBE3E5B2CE4E837E6B33DAC37291421D37EDD1A71E4EEC0F61546363796BB2C6A5B0F238A40B663CE39674E866C051B70D78A85DDFA2CEF59E4D2EE9045272A54BEBF2436D674F5734FCC70B9DE6DDAEE6F8B796738CDF7D797CB65764077D6C2CDB6959FA4B7D27589E6E3F670B4CA2CD09BA734656307AE2A1A6EB399CFA793E718AC4D34AF86F8B1DC5B86DF04A1633EBB1CC705E2315C30C6E1E7C53AABB399703BA1F6F36EDFB07A97D6895785384D301DAB5E9ECAA4DF73240A53F379CBF4859FD55E9D1AC65A6F6CE71A3F166FBC469BF58C34738B32FB293823658711AB34D8AB2FCDEF263B4C9599854B19538E9A248029479B575962C4542DB4CA7452875662E5F89417283E20150E6EFE119D4761A9C0EB0AEF83247C4079719B7E45C9E9F4F8F0E8783A398BC220A7397CAA2434AFC4C702ACB2D21C3D275969D0229E89CDDD73DB102879BE8880CC36844D753893329BCBC95BF424B2B91620DD3B112733B1E10920B5345D7448285B4E85DF11663CB942791D1405CA9276464C271FD65144C2F74FA70F4194CB29A5C5082D21210C834A7AC1E12A59A01FA7D3FF2A9BBE9A5CFDC71DDFFAD9E4638699FE6A7238F9EFCE1DA12B24EDC602FF5D8464967504C6E48EA110936F41367F0CC83334C18F772859168FA7D3174787FD31903D94671CBC97C01E78F9B886256CE231D0403E7EF1C2B9DBB50B41D7611331581D693525E5457A67A7245D573C33A5DEE6F7026BCF1538D5CACEB2A44ADBE2A61CCB46BD7462BD6B149966819C36E5B133BC3E3AFED55DE571A962FA6A68264F8C4626CDFDB496C99F471AF9733E37A164DBAA64D366FD6893C0E8B8E7BC2C3507E05AA8EE4B29971E460FDBB9CF7296187B256B039F4B18E31734E3DBE8399F1BDF861A8E4D8798D43114D07D5838F7854F18D31D8E9824A6DFD060B3E8DFE2E0C7BFBB8262B3C4789D7E4CC6981E646B53C5F4142A26518C7F1B954D19D38FB35CB6987EA09A64317DA751134967CB47EB7554F914F7AE2EA71EEC6D7BC358970A651314EC6865C92099342A9EE7669B55C5336029BB8A67F87CA215CFC0EBCC2B1AB02F9D81B669583CF796CDC9E21934979FA5FB2A55056C6A3BF7D279251092B474EF1E9F9AC583CD202466F100914BCCE2179E1712CA1958BAC30252AEF45BA1C5542BDDBB06E4566116CB0E1E0758F79AF69675CBF2F3B3C955FE3909FFB1C605B7981A82F74198597D16544372939D354AB6CBF5C3245BF130D1D96C2BCEE03A490690A7C44E320C69498016D7BAB423D68FC19BE54FE89967EB8E1B8567D8638A7627E9918F99ED844715F463FD88BB590AB64B4DD4A3E8D01BDA74139C55E73CD9D9C562933B583831C9CE924ECC6DE2E64DE75BF73BFFAED2A2F875F0B649527ABAE2C053F46ECE4BF8B45C8265BDE54689745ED0CD2C6933A6F8E81A9334C50738366D8A0F785CDE14DB0D83BD93509B696467B5C52615AD29F5C7CE125165B3F4343EE454018E27A3427B076DEECC532889C7CEF2738C49F1B3900EF49EF5F7BE1A43DE8E0E8FDD0FD1210780E6C071DB67ADBA33DD0D421942BF0020DB0840919F36F64E9B59442B7E5D230B7D9C990F18A5D8E18814109836CB885FBB9D4B38D28F8634E548AF98176BF50CA7F2D85DF53CC429A4CDC1538780068BC0A92E31C84D26115D7F7F314664B944E9ED0DDEED0803ECC34675968D9DE5629BAEC383AF81CDD7D133D4A74DD6E1A15F6DBE8EE10E6694592F76563480AC196ED35102D0CB6A94526DF4F4FDC919363C2F32509A0DCF28F677699C2666B9D5FB3926E720461CF97F73D769E4AC147B6E70E78E75CA0BEF7E6C2873C4CED27ECB0E8BB934166EEB25DBB6D752C924BFF0ED2D2B33627876D9F83CEE627264748F17B3BA40621943EF034C9B19C333E5993C19BD203B6A9EFD3ADC2B7AA76BE4CE96C4D8C32433E9473268F2D7B0919867799ECEC3B2037504037325FE4E732F1B4FF5B28FC2E5ED6A9C242DC4015FF07E1D1521B93781BB810929514D0618334A9203480B78807F92005693BD080312CC9D175910CA593CAEB3309987AB2082062354B6143242ED06AC58728156E40A40524063B5C127A67A9071372804D137D1E364C648835E48FE9EDEDF296F10B58C64AAB06C643FF34C3C3C38D00906B9FCCB022A7F0F2206DA47ACBD0B8174A95981469F1A78239C6F7202DCC937B19D18A555004DE2016EF6B75F07E1B92D17FACE7838AB82025995B3601C5EF3D7DCEEA0D75059AE09B7E238DE8965F6C220DF0D600103A5C3AC0BC6F7D3BD0B8ACDABB10EE1CA63480CF4E0E9662586DE1950494C55FA334A8CEA61D82D97182033DD6605A6DC6CA8E48516FE8CE2A2C8E1B76DD2D2581FC0BDEBC1C5641CB36404C1703351469588FA22C39DE64248CB41AE12CB44BEC05E249A7B142230FA71108130BC16EC5D1A34EFDFCAB84CAFCC6E4428E4A0EB3B6DCC2EB363E5AB71BB56A1C85E4880187008305B3CCCA6D6F47AA177D1B17A3D13D8E71ADF8BDBA81849F1A046F101758C5CE82E4220C081C56593BA46F37CA35A4CB646DF6C859619434436AF5176498F70D19083F84D0D4E53C5C22394FC1C8E54D7A5667C972A13B0238690292544ACC7B2552AB3971536A84F0172203931BFE7E35D58F4AF7E013921CCAF6A6D445EE899F29DF268B965275385E526FBD95E36AA201A19D04012A17DF3C1BB30285FA501B23C685F6ED9A4088CE104D9B4188CE0FE709085517D1F6C7C44290B6AC729174A01793721CFE6B6BB4C0DEF8F6C87BF14CE09338884BCE6DF1469A2CD84F73F2421A99E29010240A693366C048CC1A06F899C4E17F72996001A7EC2D6909E6E52A2A3CB91121D2DD6A123352CD0B5DE5A19575B0622AA8BCD5860F84AC8563059EB5F02CD164218DA720B12898B834C28B1064CAED5075450B56442099CE24A48813A7AB4D571B50BEEEA3C508BBBAAA3C75D1D7CBAE0A68A528B9A56D163A6076826C4BCBF5B42CA174308991AA1253EEA120771D122151E526A313F24F79A3C4BA42AE05CE16BD90C0F72E12AB1B39574F8AF9997D66D3B50BB8D94C8EB0A3AC4B48E3D520D3A3D22338AE62AB28CA22D8250D4A556BA5ACB36A15CA1BF5D98C55DAB9411F2C5103EE12D76133E6EF72EA1E34A216CDC53DB0EB8142BBA54C380D36E5D2F2FD7C8C8E86708037DA0D608B6DADAC880AB021074F552AA1D700599D84235123BE2F046BE7289312D2FF0D2C218A3B011C947114F98FA8049A90939E677B780D958BE2AC8174856B72ACA1880400BC400727EB816A410626581F1EBA269957E61A6C3EC67CD8019A3B46C56FEEE3D3C3E2014E2AE3A62D4B57B3CFF444B9E32AFFDDA7F6870FC2334468B4849BD73861D8258A62182D270E65ED8644ABD13A5B674CD4481820107270A67D14B44A94ABD13A5D294669AA81E621D9224EC2A205184167AD40AB46B3AB500B9F50620C1F0FA028E4502C66E11B4C4751DDAAD95BDE70B348317F75F4D6BFAB1F7D0CD113700191CC374C0D36E7EC3439743A1484316F5BE8D83C4167B23951C55A221912104051C14283172A1057940084390C22C2B6349C8502450C606C076A2451C815F8B5131338492DE64D09D810394B03E32D71F9A330392CA346401F6D3220C0F24918F79014218CE8295A7C14CAFD9CF9A41F33B61A6A5BFA1AA4C05E569E72076C29003054EEC80F19ACEF534277B80550759741BB10DA547EE9BB293197532541FF04FE931FB93D9A775427207D05F17A87C68B6067182612624E5037300D6D4B94A1ED2FA1C4EE8515D454A8F57048BA008CEB2227C206F7F6529798DA74C4652BE7042F2FDDDA3C555F2715DACD6051E328AEF236EF124E7793AFC2733A9CF271FCBA74C731F43C0DD0C49BA858FC96FEB305A34FDBE04EE822B409083C22A5300E1654132062C9F1A481FCAF7D06C0055E46BCE376F51BC8A30B0FC63721390241DEE7DC3E2F70E2D83F9D375F3568A0A8899113CD94F2EC2803C72965730DAF6F82796E145FCE3AFFF0F2E4492F986670100, N'6.1.0-30225')
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'202205240427182_initProject', N'sambocappweb.Migrations.Configuration', 0x1F8B0800000000000400ED3DDB72DBB892EF5BB5FFA0D2D3EEA91C2B7626537352F63995B1E3339EC9C51339A93D4F2E9A82656E28524352497CB6F6CBF6613F697F610182175C1A371214A544952A574400DD40770368341ADDFFF73FFF7BFAB7AFAB78F2196579942667D3E3A3A7D3094AC2741125CBB3E9A6B8FFF34FD3BFFDF55FFFE5F4D562F575F2B1AEF78CD4C32D93FC6CFA5014EB17B3591E3EA055901FADA2304BF3F4BE380AD3D52C58A4B393A74FFF323B3E9E210C628A614D26A7EF374911AD50F903FF3C4F9310AD8B4D10BF491728CEABEFB8645E429DBC0D56285F07213A9BE6C1EA2E0D83F5FA0BBA3BA2D5A793977114E0AECC517C3F9D0449921641813BFAE2438EE6459626CBF91A7F08E29BC735C2F5EE833847D5005EB4D56DC7F2F4848C65D636AC41859BBC48578E008F9F55C49989CD3B9178DA100F93EF152673F148465D92F06CFA1203DF6062AF50524C2722C217E771462A83543E62DB3E99B0359E3492810588FC7B3239DFC4C526436709DA1459103F995C6FEEE228FC0D3DDEA49F5072966CE298ED2AEE2C2EE33EE04FD759BA4659F1F81EDD5703B85A4C2733BEDD4C6CD83463DAD0615D25C5B393E9E42D461EDCC5A891048604F322CDD0DF5182B2A0408BEBA02850961018A8A4A5845DC0551389E033E3B58375817B524322FFBFC153A73330C2BD9BA8881B887882E0C93E9DBC09BEBE46C9B278389B3E3FC6D3FB32FA8A16F5970AC58724C26B036E54641B0F5DF8ED61FB9DF8B08ED360811697911712D821FBF95183EAE4F9F321063AC702BDC97543F443DFB7C1E76859CE1DCD54984EDEA3B8AC943F446BBA52730BCA2D5FFB324B57EFD358101AAED2ED3CDD6421E1616AAA7913644B54F03D3F9DB58BA3D592493BD66DC924350E4BA60217F9BBFDE941FE6AD71F2F78EDA52CCFA365F26B7AE72C6275C3837C297061DAF4DD89B11E99B5300061393EF969901DB3642E5AF8D1002A60DACDC8D74094BB4229E2D06650CBF16D5983D900D80279D1E74AA1855ED719C257436F6815A83BA444D39FB2B8D7CED3613538AC036A5C17681D6405D10E24ACFA86D75994662502F5B4F1A12DCE3777FF89C2428B66109DF802E56116ADE9995387DCC7285FA36071F380DE2054600C5E34633DC20AD3FB345D0D8E0B2BDD59E165B57E9528567D7DB3ABBC1A6EDDF0E714AF4B41E28CFF2ABFC982CF288E3DC03A4F57EB1811F1EA3224E34106FFD703EBB04CAED0791AA7D9D0F3FC2ABFC4F4BB081E7BD3354364257C97F496B60AD248E7D4ACC0DF9619CA7357D9C0B3A46BD3F338CD11443A13F32E109665B470669E520B693726501721CA045BA5D545F8124917118A7BE9226C07DC5492B6E5413351E0DAC609D8FE24BA5E639A97E3A2BAAFE379946FBEA73CDFDE11EF32CAF2621C13C8EB602CCC64FF23FFBB4A7E7B5821DD863B5007CED3A408C2E2FA214D46183E5E2916DA51FF3804D6EB348F0C2AFE40E3BDC13BE6320B56DAF93510EEAB7CBEC1BFE952D64FDD7AB50AA2583B023BBE5960C1F2791F65AB0E5A86C8F420CFBFA4D9E29720D7193BFDA8D073146EC84119AB74ABF5E0D8CAC9FB76B3BAD34E25EFB8BCB1E6E64B7A89D7A0347B959056BDE1BD4EC34FE9A6A80E8E1F8AD055B56D0078E9CECB30C45AF9251666B4384F374961D2A7CC2658E366E567DD542AEAE77110AD72D860C8AB3DB77555C67008D6900D88703557CBE6EB741925765DADABAABB4A6B18BB5A5573ED2A0166D7D3AAA6BAA36505633F69AD5EC7A15AB327F04A0EE9D4E4378DBFC6CB7CFD16154775EB230AF7126F8D082FD29F8E24B04F26D68D5BDDFAC456B77E767C77FFECA7E73F068B673FFE809E3DFF16CF56635DDD94ECA377C603EF4D25A68F41BCF18DAAD36C281701FFB3A104BBFBB3A1EC26FEFC392A15FC99B9455D1983B7AA5FCBB3EB9C137AB6EDE9C00D73DBC8B7B306749A2E642FF23F5B08D4DD9F2CB0288355C980BA48FD58AB7FDDDF1D91B89FE374798EB7CF659A3DBA1AF4D8B67B6ACDFBCE2CB88463D769EE6CA9AFDB1DB8ACC0C54E85DECEBD9BE2417BD5E8E7429AF0D3CB7DB4C967D8D325ACD12DD8164F0783304A74BE179E94758A678C21DE3C6C567789DE8439106A2285DEAEF11D717F8CD097D2FED4AABD96335669B1E0B753C07051AFA4B77CCDD6700156900C1770AD5E868BF3724770BFD6ABDB8DB637441DF686688B7BC3C2C722BB36DC47F959830AC28848A79DFAC1136EB20C13EF751A06867B275FF8A88C1A94B181D69968152C11FE76AF7F5832A821E602C5D167943D7679ABC0B63DCC72D52CAF88749BE8656C2047CD7C0857387BE94AC30DF1E3E97A9214DB1FCE190A5C3B759AACB9768DB25594E7E542DE8DEF2D8403E715B8C6B257D52CA21AA8F3D956EDDAC8C1057565593A6EC556ADDE6CAC2CE9D0E616FDFC2285AE769B19B4F56156ECD37AD895D7072EABB86C72E519CA3BCD60D93A7E7A32883267B81C1EE6F97BCF855E01CD9751D4F271BE35474CAEB7319A47FFD42F2D1EA3007479FC32660481EE4F1DA4070E25982DBCF5AA3075A1F4DBB4D0CB5CB7D76F46D5486B48AC2BDDCAB565A548AAA45486E49AAE4E5B0E8A9D853A67ABC47955DD2E115ADC05E127D78DBC6E77D8C855336994370566EFEC619EAF191FED0E14C8E60DCAF360A925F40F3EAC504EF3E9673C2FE68F7981B40E91AA59D5B63E182015B8EEABC5C7CB9DEE0A118DBBAFD296E837CE414D929490F1ABC5A6BE6370933AB1FD41EE14B83639CAB45746B6166F837863D5E01F2870BDA9952EB8FAC31855AA37D102BD8E12677B52D3F020C72AD12087FD924C71A4BD6A1DE8066759A3361F377CDC8896CFFAF5017F3C22EAB029B904C3E97E03C1353EE8ED0A5CA35D3EA803F30C185381BD1050455700EB407116E08ABD8EA7A5C3FE2F111673F79B56B6ED41DC15B830919606A3941FBF148AA853349FEB978B051BDC64B03EFE921A63010CAAF2BC455F5E7DEE70AB52B73B283C0A5C4945202F07C61A98F9CA6410E5A9467FB5D21B3EFC4C891ADB563435A367CF412114560C3A7F3B9DFA99A687DD51818BA11141D9D748C480F3B212B1DD1B279AB9D8834330F321ADEC63073317A60378A860EADC4AF5DB7385A69A74B4D0D5ED75BA90FAD779051DD55F78D757D171AEC2C8DF1D8A6DFEFB06E55DB6E9BADD41B757E0FAA3225081F199F1DAC1DABEB446066DDE8F67C82A4A368CDE81428C369E4EAE33FCBF2AF9D14FD3C93C0C48BFDDC997879893FEA02BF7A19A4DCA4DA89E34B77CCD76FB012B481B0F5CABD796F33BD7A16E4BC1A89BCD3E2D0786EBABED6E00EF83C4D9CB86B439A8150A5CE3A81584254C34709B7DC65E44D032CA310FBBE8096CDBC3E2A0C0E5E57DEAB6BC002C62E2FAC1149B43E07A1E120979BBB5616D05D982A429E929584B53445C3F5D5D055916E1F912C44683821F8481F1FAC60F1ED3C3713F5890C1C5D20F161291EA3A0EC2E1C74330992FD8FCE0A212CEB89AF5B5A8C641B2DCE84F4E9EAE263E4571DCB7B743DD6F280F4A65AFC11312AB2EDC56D5DAE3915C2A9D8D802ABD0E4673DA0737A5A76C74D07674327B1315A35C5396C8999C4AC787DBC39DBB3D7C8FD669E6EC6D405B1D8EA50A5CA3455B2CD9E2E39AF25DF99823FAAC5F3586790FFA310A867FF3B8AD3867171BBA3FE6FDF8A1C9A766780DD42D7D9A45AC224F8CC0B31A65283168B9DD50A943AD371345A11991E25BB616AB180985805E24D6F0968FB3022D26E3643EAB7AD33F0D274B8E2EDBC5E18274644BA6736066FF4199F72320F3BEE4CC320ACDC07940C89A0267AC60F97D5B556B972BB9545AB5802AFDCE960FE9DAF96889DB1C4E960A5C39268E278BB1A3D4A75F12636CC38150FB8A4BE98AB64A5EB57DCCFE42643A22BE0F427497A69FAEB7E1E11CFB8BCCE96ABC4C97295D9A061EE23A78244F71A8F6B6ED51FE919DE3C673F38AE167AC14DD769CE3EF82E493A741391235084312CDD95BE2B78EE8475987C78DFFF9315AA0D45599281B1DB409052E921327BD2DC6794EF399F2F36034564C8097799E8651C9E70AC54BCCF84D12A2F27567FDA3DC5BF89EBD4A16932AF79CAA45ABF953AA94F9EE98CA9840784E4424731DEEDAD9F44FD2F06D90D467070009DD127924C71292CA4C554401C9494A6EBDA2A490E75D9484D13A886DFB2300B03C8C124E35A8C4920BB42637AC49614B789B3EB0EDE0FE34688545C644B7D319235D7AA113F2BFAB8440950CBEE5FCAFC451C35EAA14E9E31980ECBB691EEED3A3234FB204F7620B0204D3D30631FF9E7D1499C15FA26542864098AE5C37D85AE0825457705A8D38A880D800F07C2D3C10EA6D2C3610216DF0E2EAE3C9089CB256C95643FE5A466AF89A665E1B9100420464603589683791D20E7B1BB2A525894D079479F6C690B22ADBB0AD0088A9870791322171B142CAAACCA65B91327ED82348194F92BD93329A29DA96FF42DAE841648C4F3AAD10317AF1B41509E3C63C828071F4D87DF96A36F8520ECCAA0F7F25ED4DA3E26EB3FB086A4FF58AEDC756F52B96AE3B2F35701A3815970D39E15A66B7F93DED45489F4A4E00DE06991E448CB47DD9823469096D835FCC033A8A7099F3A6A864C121890A73D607D200D98B9F7D16160065DD9B4184D1BA675B104C6BC6585921A44C0FA38AA91CC1DE242B9A70F6B28C74114675147C00C1C06BA2B14F5B143F25E15DC46E67D647D755D1612DEC25743BB2EE8DBFDAEDFF1AA78C26AA31AD830D1456FB6E7BAE312CA99525DF9F195FDB99ED18F4B544DF13D3BE2ECA944A14AC424EB5D2C085D6B317389B6055309601EF222D3AB505D9B360804D2F80A07CA3C8201C64462517868833AD44B481A2EC854E1FA806003EA0AC69FB62C35FD95DC549CAB484B6C12F06941A45B880F7B92AE6EB1EEBB69CE7638BD88B96E69D2F03BD7ABD3B8840A97BB005695213D70679F36A7C2419125F32A999AC7CD6C44A107D2DEA223BAAB75012D8011724652FB6B0E529096B839B7FD139A60869EDFDE0FB330F6233AE991FE8C4F6E465AF0CFCC0731F1567756F7FE4CB40E8225077DBA87936B4FDAB46F550B720466A4A58AD3BB8D596A4E955D94DF21E1BB740997C917D71573ED6FE0A4529C003ABBCAEF3CA3956940C027C8E0AC071359F4E5E35FEAEA0E7A9246630B012BD0618DDD84CC0EAAB3D10527B5B6A00A30060D3B43DD3431058CB880150F5DE4F1E85B86A1BE048AE4D1050C0FFC9016CE585A3075B39BC3880A56E177AA874E13100656ED82210227F4D69018D5CF8A900D1CB540390F3720EC21CAECB2C642D8E3EA3EC513575D8723330DED40E134ABEBFB084DADACA746059D3A42560BA17E880D61BA325401D2823903A9B2F04A4CD106C01A44D5EAA02C526473500ACF252A23A58180452CE7D69025A670704A1B53907CD4BAD5E36048BB5011C93AB0A96603E1196015A9D0F0802D4E618320061CC6C101CCE3C6A0F4A35E7253BA801646DE58160B516344B20AA4EF1D63203301218160242E3FE9A1A336606100867BD31002B8D311094CA4A63EC0B3983C0BDA047392B002AA2B2E77D1320C34E6AB58B9297A72031CA97B786C6E5C33DA875F50C5068CE68B9B0DAC9BF979A30F5011D54F3B88A3BF7989F5735C392946049C77779500580ADB82E3E37E409634134E1BD0F4029DD8B20F10A10BA5D643A4FF5660D2914AF8018109CFEDC7BF4FCCB15484CD44F5B781E828F5B58BE31270F9D2C80CF590C24EC3270F8390644018B871BFC10F44F3798B15447181D39F42F341858D031C63B95EA838C994AD0C303EDC884A707BDA824BC305050A91E8C772A55BB89994880DFBC765CBCE77C2F12F10EF20A0A5503F1B8CAD013B96E99912DC08AF5813303F7596838F3AF89A81D460FFB250344B07060E686A077616646C29CC83514D13B2D0BE0D853706F0A999D6B016A397AE47243B5F7C965775EE8A8AEA1A7BD172E80A439B67B23AEEC12AA21AAC17F141CA7DA8314189F1DE9D43EA300C84144D24110DDC5CF41E81CE9B54D015367F70675690BDF3D5125D67BEFF1CAA1EDEC34FAEB0DA6736B1317CA24B3F640E38667E383C68C90B7C168C866E37506C3F5756053A4DC920967E136C50D4DEF38C50C8A31096928A5779502C0F9221014715FA68EC9EF47B828577AFE3003116C4B1ADA687C7D1878B579C90345A448BB1041B44E2C90E300E0C6C291A3326F6909A1725C9100F9130F26B8AF920C06351C72C5E836F4A1B56F2846A83C6A936B81AD73014B03E8F064EB46E0E94056C75A6AEEBD9BB2D3D93C7C40ABA0FA703AC35542B42E36414C4389D5056F82F53A4A9679DBB2FA3299AF8390DCD4FD793E9D7C5DC5497E367D288AF58BD92C2F41E747AB26026F98AE66C1229D9D3C7DFA97D9F1F16C4561CC428ECEE22D7D83A948B360898452125E7D812E49AAA78BA008EE021269EA7CB192AA81B7FC0A736C8D12BAC8973959DB6AEB56E4FF95431E109C8DBBF697FD232A209778A8A446396AA430A8CACD310092F032C88088C1E769BC59256A870F756B3124130BC914AEC90C95460F8360D212778865F4491ADF0D02CB14F7804DB27C69A1930AF6F0F9B4E22C60BEC41D22090307C1FB59BAD9D341AB9377B190EA6F3294D39920C7E2AC9949D34658CDC4B9E83453E92EE96BA6426A80C34C859B0F3353C95FBE3DFDE2064114EDFADBEEF0B9B5EEF56672E3EED481C3EAB6C3B0B70A5DC5025044B352C3A89D37B92541E1D0A98642878E16C0E2CD95B84314972BF6FBCE08A017D1EB26745B1437FEE51D0B47FF264F0DF13A8BD22C2A0416B75F1DF6A34D993648D890EA8F2E636C72868943640AECE1BD46C1E2E601BD41A8C05C1485592EB5875C357A9FA62B1E2857E0B4A3D7797E844DBDFE6C0FAB49FDC3426A3EDAC3B9CAABC10862DB7E76817593059F511C03E0D8127B88E7E96A1DA3F204210D562C1B4AB35241C152B54218449AF190D8EF2EB4BBDCC4F145F02812AEF9EC40B53246F0E29D30BF98CFCEB0C469C57C76947FFC6D4973914A73A02D729A073044AEC061BC719A238972F547177E5E202C9D48DC0EDACF3BB3B5B266F6BE3BACFA6AC162A3D5351E5B7B1F89372A9B98A3CA2DF8E67750BC4D1086E1CF659B239D05C27C76D013020854FBD5A157784926FF2389C74B7F78AE6F62A1CB6E971441585CD33C34FC5EC796D843FC7B95719C85557F73D021D33C92D5B5F6ABC39E8917BF6516AC44A960BFBBACB1F30DFE4D4DEAFC2ACB1438EC2534F337B78BD04F8E3030BBEEA36C252EFE62990307AA1C41BF04B96029E04B1CF662146EC809006FBCABB5B017F3450EBD24E259A72CE13AC9167482A7A0285CC3411EBFA497785EA5D9AB84244E10A5522A75586DD2F053BA292A8DFC43110ACB8E5CDC0136D067B1CCC11C10865857BAC4228A16E724FB8B6015908BDD4C20F2DADB7EDD99FD1670E7ECBBF9CA6FD8DCB75F0B18C36CC07E6C576597E90530AF56379F1D617D0CE20D04ACFABE93E2A474CDED2E4EF4ED623F7152C050AF3DB83AFE46D2EA64E2D2C315B9D8A8689BDF9064A6620A8612DA1D100D95437277C9285FCEF4130C18C4B00B45FD329D85A27AAD3E1AE7043FDCBE6CD3791D5BB04CDFFC3B3D36B79ED73EB853BE9CEEC619B8E9305C110336B3904CC19CD5505F6E8A07D1C4597F733A420296EFF6AB83CA2EBB1938BB1680EE041D5C084AA71651516E3E3AC3117BC47C76314BE3E350221D6499CF6E5C838CE5EC777B681F23F4A53C39888A0357B0336B481B0AA1EF1AD2044E705F43D44D55548E84990FC54753B75E487374E1383FD7B2F56AED6AB66A1213B3509A8FF670C24D46A2D6BC6E3200B3F0A44217B8942BF23EC897D8438C483E5BFCED5E7246E24B766676F0313EFA5F1B301141BA5C1CE89A0F34532A9CB789240442913DCC1C586BF35D73F902DEFDF466BFE1D5938D0818417CA7AA30F47CCE17BF980028DD39A603B2CB462D392C3BE7CC620CDA3EBA44D44FD17C498332FC9DAD24A8007CE733D71F877AF0666B5C01AE8FDC6F8EBC1C12257BB5ABA9BAFF0A6182AC3AEEDBE4275143F7EF194FEACFA37F4AD7F8F557F7BEC9C604BE642CAFFD2E7E3F4A2ED32662C798CFCEB064B271050EF3341501D12F3BB37AB621EFFAAE9E4D803CF7D553DD74EC3D4D05C1870F842F4FE13728CFF1E157F4BDAD3EEE94A4B171117DC81B1345B19BD4E9000C7320BEAF645D5E62F8127B882B44F67D7146B45FED2125D26A95ECD66A2587C2EC2B4552E04C7739328318469236F82C2642A8BF39C86396AEFE810241976CBFBA98236538F5B76F4906DBC8A9BD85AF89B3DA41EAD46D8711B7829C0C4AAC71249AAEC5327BA8CBBA91AC2909450E76E7D2EB5ED407DBAFAE90E4A59AFDBE3382298497E92B9C7CE45E770135B4DF699B55E7F76523B15E889BDC97F55C946577CEEB9B0FC3788C73299F01DBAFAE90E439CF7E7738E85EBF5C2CE46737CC677B58BFA4906B7EFB7567C4B10DBCDD57129B30DDEE52A86E3ACCFE9854F864C9E14BDC2102663AA1C81DE6D54A3A320A45EE30E52D9C2F19EA86EFA00770B38F8B96D67702B2F1EDDDE7A0B6F5301B01901693F3C93167CDB4820D78698985DDFA2CCF75B9B43B64D1770A2A1FD302FE1DC6869182107A9CB21D1D458C1076D52C4BFEEE7C90983640635F4E37193EDC59AC6E3A8C7624669365219932CD9AA1C2F09C3DCB00973227436C946CC45DA1FEE6A0FB84985382EA433FED9C04FB59AF7ED704407590E46D7AB5B1122B1B2CE5D29DE11CCDCED39763652E1F774EC1CD76752F21BD8DC4082DCDC7DDE12817D6B63767D9044B1D38AC6D3ECC5CECEF8BEDE922050EC270DF2508430C0661883B046168D093400B8A9ED122F7DEC920F912071E92F43E1C0BE55C9DBAF64B2080C3D23980C32AC8B2088B7510430707B9D41E7200D90003770BA08F570348765D40AEAE0B5FD2ECD3751C84425F98CF6EB0401B2957E02A0B417B1F2B6E24600517E94F961B49596CBF3A687A2482B8D8BBE6E337E60F5F854BEFBB41CDA158F0163B93A2DD305B52C9C39BA8904C395C81233C26EEDF3100952F3E985AB7A47CD148F2FDD5AE32A36417850B6EB8CBB7AB6DC200E90D3B57620FF15DE933177D164486F9ECF2FC32109F5D06633C76BDD834A92AB80BE88D3283851AD62E46D4845F1B767966F81EDDA30C25A232C27CDEB1F5C28FD9449D0AC47ADDD84DE3EE58BCF11A5DA4676411B7A822DF386768EAE2DE9A234974DC4171049B0DA437625C3237DAAFF690D22F09F4EE9AF9BCDD43E7BA8ABF278069BEBA7899FA79F47E8F8FA97769FAE95A3ACAF1250E074370478B3BEC6871BA4C695E6E1E52FDD585EE8F65DE0FE9911657E060F8CE48569B3928A762992B54C025852BB0877717249FE4FEB55F1D0C376148226E40CFED842277985207B9826FECF85F6590EFBB88D37CF3EEABB8A2DD30CB3809C696DE16B2270757E0080F807438A00B85626AAEA64A83BDFAD2FC6E12895549BCB8EC62250548AEB072E47995504CCCEA45AB4C27D755143EAC32940F8A8E4885A3F91FF1791C9567BFBAC29B2089EE515EDC904DEB6C7AF2F4F8643A791947414ED3BD55F9CA5ED020294192A445950CCE2281D9F13392C00C2D5633B1B97B1A340225CF179CD98C99B1953CA8137F9DFE861E4536D7C2844F678CF23A13782D363C05145ED2053C330965CBD94E9FE11468711D1405CA9256999E4EDE6EE298447A259B7B9C4B022A8217738731A844AABEB84A16E8EBD9F4BFCAA62F2657FF71CBB77E32799761A6BF983C9DFC77E78ED05945BB416EB78A886C111D813169C628C4E47390850FE499CE9BE0EB6B942C8B87B3E9F3E3A7FD31905B20CF3878CF327BE04526873C55C1260BA206F2C9F3E7CEDDAEEF8C741D3611835D23ADA6A47CBEDFDB29499522CF4C217F0D226A066BCF15382BD7DEB2A4CAF0E5B638968D7AAD89B5C159649A0572DA94C7CEF0FAF8E427F7258FCB2AD6778566528A6964D2DC4F6B99FC76A4917F99E526946C5B956CDAEC1F6DBE301DF79CB7A526268016AAFB56CA6512D3C376EEB39C50CC7E91B581CFE516F30B9AB916E9399F9B6B11351C9B0E3159C628A0BBA870EE0B9F5BAC3B1C319F58BFA1C16AD1BFAD82AFFFEE0A8A4D28E675FA31C9C57A90ADCD2AD653A8989C62FE755436BB583FCE7289C5FA816AF28AF59D464D70215B3E5AEFA3AA9C5C7BBB9D7AD0B7ED15635DD6AC6D50B0A396258364326E799E9B6D022ECF80A5445C9EE1F339B93C03AF937469C0FEE80CB4CDD8E5B9B76CFA2ECFA0B9545EDD77A92A8695B6733F3AEF04423EAFEEDDE3B37879D019841C5E1E207239BCFCC2F3424239595777584076AE7E3BB49895AB7BD780345CCC66D9C1E200AFBDA6B365DDB2FCFC6472957F48A23F36B8E0065343B03E0833ABCF866AC883B5B74AC96E997E98BC5C1E263A9B98CB195C27C900525AD94986218315D0E25A97A10AA80F3B959AE54FE89967ED8E1B8567D8638A7627E9913DD4EC8447E52F0C5485535A99A560B796897A141D7A439B6E83B3EAF4587BBB596CF3040BE7B0DA5BD28969B0DCACE97CEB7EF7DF55062DBF06DE369F564F531C788BDECD7809DF964BB0AC8FDC2891EE0BBAA9256D722D1F5D63F26BF900C766D8F2018F4BB1657B60B05E25E02C5576AB84EC81665E25A2615789858F29B486EC429D24B5F101F6004B4A7AE505269BF3CA87B4F239AF863B34A8F346EDAFECF2B9A73C5FACE65D6FB5EC59A2CDE3B4B78AC73675365362A5BD25A2EAF8D3F31C2327627174B210DA3B2886CE3C855224ED2D3FC79814DF0AE940437CFF8B1CA3F7ECF1D313F76D03B2256A7C17767DD6AA3BD3FD6C2943E8E74B68EB4C2CF2D346E16BF33669C5AFAB93B20FF79B011D9E3B785B0002D3E670F26B02E0D239F5A3218D6DDCCB7DCE7A79861325EDEFF23C844383CD1D7607DF280B1FCC2ECF199A3C4DBAFEFE603C833849902AE9D1DE9EE6F8CC493D2D126DE2A4EE37C609B026F83CFEE9530EED2D1FEBA07BF6C7719B05BACD65D49DA1751EA31D1609381FD0DECA4221A414F26CA211920BF995B8F659ED10703BAC722EEF410EF690DD7870D2878DEA0C3C7BCBC536958F075B3C9BCBA79FF6CF24F2F1D0AF3697CF701B059C17676FF7093EB94E4FDD4F48ABE379CF1112EC7890173EBF8EDFCD06BEC4F87EB746652E9BBD5D52815C386EDB9804A097314E4AA0D37332CB79733C9FDDA1E4399E511CA21D384DCCAE37D43B383907B18D91BFDB0B7800E798D95B4D434C53E3B656F2AD7B2D946D7E1BCFE211418A495F537B9D01A75ACD5188B1C4241A12FE5F5E86353AC6872F126A0A179FB8FB5AD0DC385EA03B4BF637E4112327ADD9CE22212785392CD99CFB789D71C6D6F4684F7965B296BD15622F2E890359A1AD9E0677001CDBBC0CEED3619ACC65984E0F01BBCC1DD3530696E687C51D7A26A790F10B3F804D517DC1823EBA7D8122E3356D07A04C021AFF801586BEBEA0C17434DDEF99DA44349ECD41755E1A37B5B76AD64BDFF56189B2DE158144317BBB1D72E9663C1B3381A43307FBE390F64728D5CBDEAACA3BF64493CB3BE3B6C0B16D7BAD724CB61AAD6876702C2D53D83800757818E6E51A83496AD37DE3B30ADB6619B9CA0718D5E3A1DE946712DBF482ECB8F21C6CAB9D88A7CEE8B25791AD609299D64732687ACCDB6AFC133967CBFEAA704D3A0DCFFA1B930AC63364BB279D76A09A04313EA0D9BE10B5F339E5B2C478D80463DBC7A676C7C026598C87AE71C9627CF44E4C15E3A18F5CAA180FF0DA6C313E062C248BF1097288293CFC03562021CBDEAED15C5A17CF9CA812BD7CA787E097799E8651B92C5628D81413B79A3C0758892FB50F211942D52D9266E5882F78B3898B88C421C5DDC08397862A035C31C71F0E202DE001FE490258A9F1451490E088E4222692B3E25C67511246EB2086062354B6541F09B51BB062C9055A137364524063B5C127A64E9171372884496CA2C7E98C9106BD90FC9ADEDD2A23F2B68C64AAB06C643FF34C7C7A74A4138C5FDB8B871250F97B1031500D6E182190920428D0F01ED1A370BEC9B1712B6736706294760168127970B3BFFD3A08CF6DB9D077C6C3594A14C8AA1C20E3F09A0F1B7D5B4650CCD5FB8010659AE39D58662F0C72AC4D1630503ACCBEA00BA13D8CA018828C2A902AC3FF8D2131E53389512586C6E054494C55FA2D4A0C107C741F24064812BD5D8129CD882A79A185DFA2B828D269EF9AB434DA0790C760703119472D194130DC54945125A20E0C7AAB09B0DA7290ABC432912FB01789262EA9088C7E1C4420D4231D461AE0D8AB0A5C6264D35184428E3C76AB0D5CC59C58F96ADCA95528B2171220101A04982D1EE650ABA1C130A2630A01A7C02AC7541A558CA4A04846F101D718B9D05D844080038BCB36D71A383C9A414C7666BDD98955660C11D9FE8AB24FEB0817F36110BBA9C168AAD87884926FC390EABAD58C6F52659E578A0F7E951222D663D92A95D9CB0AFB045B01722039D10E7C186151BE3757A0035E738F222FF5DBB8E6C5A35652B897742C4FF9027B19F95D781CC9011B483AD4CF0115BC922FB19D44037E58ABC0253E5B1D4528D8B766F44D805222A8033ECBBDEA8BBD0C702FDB58487CC120B200BC1F184608D4CFF714F89A171C2349007122BD55FA92B2DC6BAAF0BC6B3FBB8842E9352F031A88FDAAF12978D2738F80DE042830F13EEE638AC018B6D16D8BC1085651075918D524CA3A4497B2A0BE4FE17CA7A14B0FE8C263D76F52D40EE1C3C845A76B1438F5DA2012F2AAEC5D9926384A50D63C2F59A0329BF245500477412E6F17A4D51C15805FD874F2AAF126035DB3E6E1035A0567D3C51D71ADA35E696C8D1C1021181DDD8E94E868B10E1DA96181AEBDC49171B56520A2BAD88C0586AF846C0593350A48A0D94208435B6E41227173900925D680C9B57E8B0ABA2C995002CE1D1252A08E1E6DE5C5E282BB7213D0E2AEEAE87157FE102EB8E942A9454DABE831D37B751362FE1A4C42CA174308991A91253E7A5306E2A2452A3CA4D48CA2C90926A3688B2014C55D7C1B36358CB390C9E004CD43B658858DAF65C228DD26C858A52AE01AC0D7B2611B7463A5C4CE56D2E16FEBD977A0B6922B91D7157488691D7BA41A747A4466144DFA0119455B04A1A84BED5030F1E961446C0595B4DE0BF50C78A588EA3266B98A0AF752AA69C2DE04EF06D0B6654A7C6D986CF316AF9D1542B962DB77990B5C286219215F0CE1636AD84CFE26BCAD8CAB2D521132696A18B07016670911570AE1622A98472419CC75F8D44BB850C98CB7359A4A08DB2208536D71BCC524B5C6A2181A5FACC346EC9B5618CB88633226FA19C2404ACCE4E26D8B3278AE5825801957CB80B1B28B4AA8AAEF2A1CD4FC6A1E0E358F0003A10520A5CA321B5AB5B63C050235C3DB727B55D5A0A69A54543BF594BEB794D9517E5672A32C3540A6AFE564D0D57715ECEAF198009C3987C3E767FE5DD584A90F9CA6358FB078C31E7062C6E8C502C9E0A07A77054058412BF78C1FAE052984D743C0F875EF8B9437E54C87D9CF9A0133E7F1B259F9BBF7F0F827321077D56F685CBBC7F34F346250E6B55FFB0F0D7E11028DD1E2ED88DE2ECD0E412CD310416933282101A5DE89521FF2CD44819E470C4E14CE982111A52AF54E946A8137D3047800303849D8CD4BA2082DF4B82AD0AEE99605E8466300120CBF5EC0DED9C0D82DDCB8B9AE4386AAB2F77C8166F0A2E9A9694D3FF61EBAD907192083A3E332BF15823611BA1D0A451AB2A84D3B1C24B6D81BA9643F5B0D890C4EB9E0A04089910B2DC803421882146659194B42862281D25B12D6132D3C2BFD6A8C8A992194F42683CE2B10A084B51321371C85E5A31C9054A6210B60AD1161782009ECF80610C3C2438EEB3E642229FBCF176808209A72B8D61E860EB87701E3363981715DE68C1B657FE790DD42F0F2904D3065D3F73AB34A97E18ABE4CE068B50E4F807B8AC45FF6B376D8ACA98669E96FA82AA550E9D233884638E44001B71460BC26E7158DFB0AA0BF43BAFB564E0175BC9DC6EBA2293B9D516352F501FF2CD22C58A237E902C579F9F574F67E9390A03EF4D705220A7903E214C34C482053C6CBA3A97395DCA7B5B389D0A3BA8A1052E80D2A824550042FB322BA0FC2021787086F622423C2C720DE2092F0F90E2DAE92779B62BD29F090D1EA2EE6D424E2B4A2C37F3A93FA7CFAAE0C6A9CFB1802EE6644E220BD4B7EDE44F1A2E9F725100749018278C35401AB082FC94A86968F0DA4B7A978F5A3025491AF71E2B941AB758C81E5EF92794042CFBAF70D8BDF6BB40CC247FC9D181C333510332378B29F5E44C1320B567905A36D8F7F62195EACBEFEF5FF01CBE7B7C4B9F60100, N'6.1.3-40302')
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'202205240640288_initProject_Dy', N'sambocappweb.Migrations.Configuration', 0x1F8B0800000000000400ED7DDB72DCB892E0FB46CC3F28F43433D16359EED31D671CF24CD8B235AD6E5FD496DA31F3A4A0AAA012D72CB29A64D9D66CEC97EDC37ED2FEC2020449DC12371220ABDC158E70A870C90490890490CCCBFFFB3FFFF7ECDFBFADB3A32FA8ACD2227F717CFAE4E9F111CA17C532CD572F8EB7F5FDBFFCFDF8DFFFED1FFEC7D99BE5FADBD1A7AEDD8FA41DEE99572F8E1FEA7AF3FCE4A45A3CA075523D59A78BB2A88AFBFAC9A2589F24CBE2E4D9D3A7FF7A727A7A823088630CEBE8E8ECE336AFD3356A7EE09FE745BE409B7A9B64EF8A25CAAAB61CD75C37508FDE276B546D92057A715C25EBBB62916C365FD1DD13DAFCF8E86596267828D728BB3F3E4AF2BCA8931A0FF4F91F15BAAECB225F5D6F704192DD3C6E106E779F64156A27F09C35779DCBD367642E27AC63076AB1ADEA62ED09F0F4C776714EE4EE8396F8B85F3CBC7C6FF032D78F64D6CD12BE387E89816FF162AF515E1F1FC9089F9F6725690CAEF213BEEF0F477C8B1F7ACEC00C44FEFD7074BECDEA6D895EE4685B9749F6C3D1D5F62E4B17BFA1C79BE233CA5FE4DB2CE3878A078BEB84025C7455161B54D68F1FD17D3B81CBE5F1D189D8EF44EED877E3FAD0695DE6F58FCF8E8FDE63E4C95D867A4EE096E0BA2E4AF41F28476552A3E55552D7A8CC090CD4ACA5825DC2D52D12C167C7EB06EB351E490789FC7D83B7CE6060847A37699DF510F106C19BFDF8E85DF2ED2DCA57F5C38BE39F4EF1F6BE48BFA16557D2A2F8234FB16CC09DEA721B6008BF3D4C3F883F3659912CD1F2220DB2046EC85E3D1A503DFBE9A71813BDC60CBDAD4C530CB3BEEF932FE9AAD93B86AD707CF411654DA3EA21DD50492D08945BB1F54559AC3F1699C43442A3DBEB625B2E080D0B5BCB9BA45CA15A1CF9D909138E4E22930E6C98C8242D0E2253838BFC3FFDF620FF1BE54F10BCEE5C5655E92AFFB5B8F366B1AEE381BF34B8F0DA8C3D89F13DB264300066397DF6F7282766435CB40C73036881190FA35013D19E0A0D8B438741C7C7B74D0BEE00E02B54A12FD44282DE34184257CB6868136838A4C6309EA67AD4C933401A1CE4801ED76BB449CA9ADC0E14ACE68E57655A940D02FDB609715BBCDEDEFD4FB4A88D68A2DC895FA36A51A61BFAE634210F31CBB72859DE3CA07708D51843909BB119618BE96351ACA3E3C297EEB20E22ADDFE41AA96FEE7659B5D3ED3ABE2AB05C4A726FFC97D54D997C41591600D679B1DE6488B0D79029591F32F8CF00A4C33CB946E7455694B1F7F9657581D7EF75F2387A5D4B4424E1877C34B7B590667AA796352E5B95A8AA7C7903EF92A15DCFB3A242D0D2D988F71A615E464B6FE2696F21EC6002EF22E432C137617711B146B98B48D5A3EE22FC00FCAE24ACE7E166A2C135C50BD8FD25BAD9E0356FE645EFBE9EEF51B1FB9ED27CBA27DE455A56F53C2A90B7C95C98C9F947FEBACC7F7B5823D3811B6900E7455E278BFAEAA1C867983E96144BE3AC7F8E81F5AAA852CB153FD27C6FF089B92A93B5717F45C27D595D6FF16F2ACAC65DB7DEAC933433CEC08D6E0E58307FDEA7E57AC02D43267A52555F8B72F94B5299949D61AED0D768B1250F657CA55B6FA2636B36EFFBEDFACEB89582E30A469A9BAFC505964145F92627BD46C37B5B2C3E17DBBA7D38FE512F7CAFB63D8020C379B958E05BF9056666B43C2FB6796DBB4FD955B0D6C32A8CDCD45ED4CFB3245D57B0C250BCF6DC764D39C521D8425520C2CD7C359B6F8B559ABB0DB56BAA1F2A6D611D6ADBCC77A80498DB48DB96FA81360DACE3A4AD463D87BA9B3D81D750C8744D7ED7DB6BBCAC36EF51FDA4EBFD84C2BDC04723C242FAF31305EC0F47CE9DD9DDFA99EBDDFAC7D3BBFB1FFFFED3CFC9F2C79FFF867EFCE97B7C5BCDF5E9A6211FFD661CF96C6A307D4AB26D68548376432304C2EF8606ECEEEF866698B8F84BDA5CF04FEC3DBAC618BC53FB8E9F7DF79C34B2A9B78330CDA9914F2303066D17721685DF2D04EAEE6F169895C1A6644243B87E2EE9DF8D774738EE5556ACCEF1F1B92ACA475F851EDF774FB5797F310D2EA1D85551796BEABB7E072A6B70F15B61B471EFB67E307E6A0CF3419AD033C8F7689BCD70A08FB056B360573C0314C22837D95E04BAAC533C734CF1E661BBBECBCD2ACC48A8091706FB8CEF89FB538ABE36FA2776ED75DCB15A8D85789C028A8B4E92DE8A2D99E2026CA0282EE056A31417E7DBB244F9C2FB16D0F53B9C0DBA8DDD2E90E526E068746FD1B3A72BD3579C303884EDEAA604F6E042C27BFE1F97BB7EB371613A800BD309B97019E2A8DF58BE8A8639096B4288D4F4460A8367D16CCCFA6DB1482C5F3F43E1A33C6A1104914EBB749DAC102EBB37BB37455507BE4659FA05958F433C66F8BE875DAEDBE5ED22DDE6661E8B642E5CC530C874E7AE62B125D66443F51972FFC38D46836BA7741A1DD5AE50B94EABAA11E4C3E8CE201C28AFC13597D6B423117D07796B58F406B6025CF0C5A672C7ADDC8BBDDEAC8D95979CBDC738EB5C69A8C37606ED7DD815FB240F87D2FA40651D956D0665B16C242DFAD5D3A7CFA25CE62C260A7182308C14F41A68A154F38E21229C29623300CFD075FADF66D1123016C51017AC39E3580C77B851DC6C1A3013781CB69886ACF4FBA236F3DC301F4CEBD5C8A8CEEE1ADDAAADD54B91D2487B19525BFA9A0E7A5CEC1CAE73AE97B8A057B737DF160F49BEF2BEB475FD0E07B906D747DCEB92B9F7A145BA4EB2E3A3AB12FFD5463BC38F98EB4542B0FB1F1204FC876D1D0D7E47DF201F6F9D1DBCE3A96D2E105ADE258BCFBE7CDEF53BF0B906D714CF12803DADBE30719C85AD211222850D7B87AA2A591917FA6F21B4AD5EFBE915DE17D78F558D8CE6E7BA5DC57A1F14ED1A5CF7ADF0092284D788BC2CC73E4E72F30531AAEA9D2E64F666B9EDBEA5F9719DDCFFC0771A5CDB0A95C64FA3438E6A80BDF115F8BF50E26B17A37CC81D0F6356AEDEA64BF436CDBDAFE07DC7031FEB588328B59A65CA52A34941A42F95AB0EB5FD591DE2CB7F1344C51C5E2D20A20187924FE8B1E15FDA84CE877BBB06D76C1FD9F461D02246B0E13F7CE962D9806DA0A83670C3516A98C63DEA9714B3B9BF4501DFF7C0EE1A5C78915616E56B18FB2B8A6850ECB4AB97CB251F4A2ADA187F29AC9157A25E79DEA3AF6FBE0CF87AD8F53B5C7834B8F27681823C183B60F64F83512E4F1DFACBB559F111664B74D826B9A9592DD80E174249627C2897FEE6DA4DA7C389A8C3957F29D2057E9A8CFCFA1D42D45C3F149BB19AA9CE387FF4E77CCE04F832BEBDF6F9C4F6E1B6986381D232C85893C726ED86D9DA23CC0C7F2F495BC252619C613CA7FA2AC93FCF82F8E56241FCCCAC164471B1CF31F38F6881D24D4DCD44A6BFA9BC5C9399DF1477E82A61B7BD689F64C70AB8D933B330F72BE0B1DE1CDAB7AC097BA28B35CAC35CAAF6B5E5608610DA41B126F2A0BA1ACDA0FA6ADF411121661810AD9607434A350369AA46292A284ABF7B18E97378B2697055F6732A92D82ABEE65637B148A843B9F8F9A26DA3514E8F399CB7A127E2FB6481EE8AE2F3D5148FE82CDC25D6739A59B12AA8688A3CC54DD08BACE72CFF2CCFDD6EB661E64AD14DA37FB9B3DF9A232D6AE2786D8E8C7E16393CAF2BE555997E49FC6341B4DD0E370A0DAEA593B5651076F62075B1DC2EBC15FE6DB7830E4F832B84DEAC5D6322EAA7570B51DCB3280DDC8C92E3E0FEBD7EBCCC31ED893DF238DAA50B144DE1D0C59419AF99AD9BB95A2E4D7196FACDB74D5A064A61F836CD3F7F4A97A898FEA46E6E612E21B422E1DF01A55117390A5087B472E49635624A11B94E518D280D8269693AC8B29E862FD70E67BCAE861E82834C65B9AE87C357277CD91A35DF8AC69EC30C5C1061C50F6F9E84EBF2080EF9D663BAA6CC2DA0A5ED000B44D6E65669CFC9477D33555C1ADA86929E747C832528697490A21A5C96BBFF5F23FDFAEF5B540D39A6BB7E075D8806D79FED02D5189F1DAF1BACE9B935B5A860C3840D58A7F996BB77847E4D560B4CC970D0B5E7504726ED21D46D9A5BB1253B7EC006CAC103B71A75E4FC2E0C68982898F5B0D9277160F1F99AF600F898E4DEAEE9A4CFE15AA1C135CFB58290844B58EE72CEB8B3085AA515A6E1907B02DFF7201C34B882042F9ECA75D6216D6F184C993D4B6FE02991ACBC934D6B1264CBE26E3463AD6C497BC30C759D94658AF74B9259150A611026569FA730786C264761B0204B5C92305848D2ACAB2C59C49F0FC164F74A0B838B7278C2E2338CD5A86649BEDA9A5F4E81FC793EA7593676B4B19C82B40FA566D4E00B89BF2EDCB6CDD8F348AD55DE4640937156A7740C9E66A7A4D3E1B663E2D99BB49EC5B7AF41CE7D803F3DB8DCED9CCBDD47B4294A6F8B1DDAEBF02CD5E09A2D21644396109F293F3411D0D22F66A9112758F0A7340981D562AD32512AB6D75B7A3E56E3E881AFE9A5C6DFDB6218930F8A52E0E0A818881078572362901184D35C2F456CA3686E46A4FA966FC55F8CA44AE05E24B7F0B5352102C434325AAF8CA9493CAF194D5337EA7AC62FC790E3E2F08174664DA677EEE8F079A3F72367F4386699EEBCB732CDCF718D41884CA94039C5D3FBB66DC6C4955AAB482DA0C928E1D51A74FAC9ADA6D3E16DA9C1F585ACCE6D3D4FDC982F810C74BFD787DECBAA2A166943E716C54B4CF82DBE673561CCBA1FCD692E8E0CDF188FE81ED4F660BB95AE0AD9F17C63BC40784FA41BBC0BF0D05E1CFFB3327D1724DD7E0790D04B8888E45441D25E2DEB34C9F0759F68AAD2BC56F75D9A2FD24D92B98E4702E07880104AF5A8E49AD76843B4A279EDBAF02E63E0FBC1E3E9D14A42C6B66E67271C779999EED7E28E0FC5A76302B119C45EBF928F2BEE5C25010458890F1028C27DFAE449205E8247310103C1EBE982580CDC380BCFE0927495932910A26BE506DF0A14485D032F69244005D80680174AF040A8A71036D042BAE0C5CDE7E391CDA6B3BB6D1EB6E75992AE2B3DB380CD41AE115BDA696D4502305177C322CD9A5676161DC652C6694FC15BC625711940A74ADD092E6B62B0BA73196D1E99CB5A24162E6B5A4DC465E2B467E0327149F68ECBC8A4DC99AC691D99C7280E0B8B5165D1241C26CC79060613D663F7F9AB3FE01B3EB05F7D443572B01B95A0811EC3A823AF57FC3826BD5FF1EBBAF35CF32A2B56574555DF923FFA547C3A2A83AD211EEA1AFAB0100C1CE02461A871D8C8389609B8C9B8D02EF8F97EB331973D11B68E173CB262736F7D20AFBB3BFBB9A7D5065076A389C28CCE239B80319D09E3A4855052F7CECAA66A4A521BAF18F293AA3C328419F5694D01049165A2754C13B29F76E17DD86E67E4A3AF54F49085A3986E47E4DEFCD26EFF659C366D8E41B50E76D068ED879DB9D6FC3B4E9AFC706A7CE360A651E81B177D4F54FB52D8671DF57531A019CDDB4C11EE0CA5891BCD8164C1A8A330113C0017BAA976005EAC032FA60B623ED7C28CECD207E436D3568DCE3D9A5D9488DE1C4816263C22BBC8039840D2C08BE982988F5C3F23BB3461B8CC741523728D6613218617078E06048BC81E3CE2C92409BF782E48BBE895B3B08412324E47497DFC3846CFAB2EBCA93B8368A3CE09074F17CE2E0AABE88630812CD12DAADBE1C3C252CECA3A467902C7F80BC23233481508F50472055AC4DD972C8620675AD2BA443CE398878FECE8C1400EB1D2602C11CDEA1C06358540B213C06514404CC85978108E71A4E30B4BC023C6112C4E993BD399E32401C023F29A712C13C833E342BBE097E399CDC25C807BB88EF8265F71467931B48D3B6B19DCCCF963913A8F476128FD0826E026FDE2BA20EF8316CCC443B2239D9EC85AAF3A9E83A8B3B20FEFE85CF114B01105927614131C79DA8575C12D3A14CFC94246D315D0FD3100DBCC6BB1020C623A7ED92B5B15C0DB4C475993EB996AD706D9B4990CE70C5E6BD35BCDE9A73A011BE957C249EEE05E1371D39B6698241C00EE814AD526F3F55D132BE01B1424034FAC7520AC5A3F2F993308F06B54033E58D5F1D19BDE750B74A252D80C06D6A03700A3079B0D5867A5064262867F16301A002E5DD9E7290802FF91CF02A875375567214B6D0B1CC54A1F020A98F27B806D0DCACD605BDB6D0FB0D482D80C950A1E0B50CE582C05218A16770ED088ED9A0E10B50BB40069F582F070982AD50A857EDA8281741F02AD1CCB7288C33CCBEAEDC044DB13787EAA418F2354F6F1D80496FF56EF08989E2826A0DDF1EA08D004CA0AA4FB040501611FEC2C402E105ADE258BCF1090AECE09C82BDCF0FAB1AA112839C4165680D44F3B435DD83C0864DBA60FAD6707BA4D97E82D3EF740685DA58BD437339864076201D708BC5FF0A357B70DB806F62DF01E7D7DF345C3585D9D1548F3E90B82D07E32B474279A6AA837D5E55B3AD34C88F04AF4C915AD301A153B0CA2FD446103C1949E20185E59ED0E4A273B15ADB40564A7738360317DA62310DDA044DDA5051889120D01A141C06D9D39A50F0844D0A5D9B88FE88040F6A33A33EB58C88B101E057D583B01D02D2AAF7DB101B2DC6B9CEE344D500F084A1B2244EACE3D1BE07BBC184BE1886B0F5CEA0D81178487A43DF4423F2DE555A13C9A7C822D00605BC249EB72222E8CC3A249B100809532450B10E6A18917C00D9E3E440C4BA18910C081101E24A3672F7AB5436CA2777B1769083ABEF374E39E72265E005DDD2D4B3864E2B0AB36B4020E4EDDE214CC6EDDDC5CDA37A16939CCDEDB1C2CE85D187C95BA97A17D9520A764E3CC24B7E451AB24791F6B56A99B4CF0556A0F04FB22013EB5C679895EB5A39648749ED5AC503B91805286AA384C624655A96BE483A0571F2368047DBA6D5107CC1EF6590416C1C1B9519882D9BD919B09A7E230AC88D9A15102C72B0446AF90DDF10E582D4F6F3D61AAEEFE7AFCC90B692D0CEBE9EEA10720E93518C116577517332CAAC5B70C9CA7DEBB0C989FDBD2E9FDC900905158D28311FDD9CF83E93CD76B4A06D3FAEAC0776907BF1EF94A6CF6EC112F87AEBBD3EACB13EDCE2DB99B00AB6472481126A17149E186DEA9880C2BA17142E180707AE940B3676A4FDDEC61FF0A60E08A87C590D92B3E151C104E7D1B68F654C1A69BB96ADA0B0C5830ED1D3263C1909703D02A0547CF54B17107E66BB6831706ADB584E786CED48986D96B6DDF057E675F73822D848EE87A8B6E70DC3AC27B4D3E3AF14DB996812570B55A1627E360B7CC2F90A029362D9283A5320C37944E4A9325545D3807535B616A66635B6E529CE2DAB05266F35A005CA805829204A9AB63B31515E662B016E5262269C00D6B63B00FE5375CAB040FB0224A720068418C868FD2F875A68FC272B44A78E342E88C1D1540E1D883CB47A05D068BA60132DF1B36F5D80A0628ACB93A6B9B399A307283411ABF06907EC80627B0CEA90B35DDDB4AF5756727D78B07B44EDA82B313DC648136F536C96824F5AEE25DB2D9A4F9AA623DDB92A3EB4DB2207619FF727D7CF46D9DE5D58BE387BADE3C3F39A91AD0D593759F346051AC4F926571F2ECE9D37F3D393D3D595318270B619D65CBAE1E535D94C90A49B52423CC125D90EC94AF933AB94B48A0EDF3E55A69065A8669BE38752821E32F9592DDE7A8AE17F9BB35E20662D30BA6621230B6AE1778AAA445336BA4F966A476C700488EEEA404921C9C17D9769DEB8D04F5BDE588D43C245BB46A3B541A3C1D82496BFC2192B5B9A1E1ED21B05CF508D82431A9113A69E00EFF8F4D56244BB4BC48E5618B35FE1049147C08DE2BC5FAC004ADCB37CA43EACA54286727121FCBBBE644D936923493F7A2D74EA5A764A89D0A5D033C762ADC3DCE4E25FF8BFD69891F0499B5BBB2DDA133FB80319AC8BD89EC000AEBFBC6216F1BB99B07A009E6AD87D119FC0B2241E304A08742A78E9680F0166AFC21CAE28A2FDF19060CC27AC3986E427613030FF170CC2189F410AFCAB428D35A22312BF5388FB64DA643E940EA0A7DE6D8A73995A7C855B8C37B8B92E5CD037A87508DA92833B35AEB0EB9EDF4B128D62250A1C2EB44EF52134A877A57EC0EEB4D0E4882BED01DCE65D54E46625B56EC03EBA64CBEA02C03C0F135EE10CF8BF52643CD0B4299AC5C17EB66A58382B96A8D3088A21421F1E53E6B77B1CDB2D7C9A3BC707DB1C7AA352992961FA4FDC5157BC392B71557ECC9FFB86C45D3A72B7B805579ED0318A250E131DFACA890B2725DA10F3D5F23CC9D483E0E58F1CE1CADFC97C4B127ACFEEBA9C3416BEA3CF7ED7D26DAE874629E576EC99F6BC0C5DB06210E7D1A2D934A24AED8E39E9040A058A9C7A8B048267F5DE6BF3D345FCB85B1C9953EA75D5E278BFAEAA1C895B38EAF7187F81FC403541A6057E671872CAA54BDAEB1528F33130BBF5599AC65AEE0CB7D64ECF516FFA62A7551CA72151E67C93A4933E914A1459E3030B9EED3722D0B7FB9CE830249557D2DCAE52F4925690AC41A8FB3182DB6E405800FDEF5463A8BC52A8F5112F67CBF5DDFC9F4102A06C1D3AC28DCC2831FBF1617785F15E59B9CE48D94B952A9F59036C5E273B1ADDB1BF91FF542123B6AF500D8C098E53A0F75C06281EF4A179845D1F2BCD8E6D24B0FA8F65381A8B29795EECC790B58AC8F3D7C55BF67FFE3D701469C03388CEEAA1932FD002C5EABFB624F589F926C0B016BCB77929DB4DE07C3D989FABB8F63270D0CBDECC1CD7119C92A5CCAA247A8F2D151D13EBF21454DC555C462DA1D600D9DCFC570CE68FCFBC631060C22AEA0E8A299F05074114E66A39CE46A30966C26C70A079299BBFF459FCDCCB92404759A681BC32803778D4315395F150FC996CB4A0FF5E5B67E90559C5D99D71312D07CB3528F2BBB6A66E06D5A009A130C3021688C5AE48B725FE80D471E1157ECA396C6CFA15C79C872C57E548394E57CB93BB44F29FADABC1CE48B8350B133328433B81E2B4474B6E40E4244DF358E10E9F0A9225EACF1D034A42B4973444BBE334B18E68F329E5B601F1B276ED175D5AD6D2A710B148255DF7BA988F4A5A738DFA8CACE8DAF96B32E3EA35C9E485FE80E67D17078FDB6E882B3F0F0944A1FB8942AEA9E126BDC21A6EB648570D9BD62BB26D6ECCCEE1003808DFFCAC4850B1BF29DC9D43DD24E6971DEE60A134855EE302B402E56BB2617014FD8D1E4B7F801BBB08015C45FF4E504399487A21717D86C38C54C40765907AA2631146C9FAC290E67E788CE393B14376823ECBA72820EC05F7CE786A3D008DA4C4615E06BA3FF87C6203A05E5F386EF978DF112C20659A71D72C9E6AB871EDE9182B4BF4EFF5BB1FAE84AFDC7A6EA9EC49AB99C3C86988969A94CBBC803E38ABD61A9CB265478ECD34206444B76467AB2800A63A5A72E488483F4D4778D233D3F622A5E4AEFDCAECC0FCA876DAD82690A3D4C67DAD90376CD424D7C53F399789085531ECB837DF0657F1ED4779DFB5EA5E59B00665BA19C1BDEA1AA4A56D284FAC29DE2343EE676087EE322740FE33A1380384A99FB96D7558923D6B8435C2372F79477042B7587942B2766BE5B27A61A667D2C172941D9FDF9C80E220E276D2B54CA10BA320F7E2C8BF57FA1447ACFB0521F95B80AA72BFB9E789045E51FCD7C7D0CFF015CA7EF1B87DD6AF23A6DB066A9FCF944AE7387BAEA3AA9B775A9CAE3DB47E32824BF4958A92F245554F3E53BC39852D0BFB1CC296685F067504BFF9DD69B0E76899D89F4524E8EB1A4173278F853DEDC3D0EE131CE95AA8760A5BE90D43DCF977B285BAE5E2E97AAA72057EC0EEB9702F22662A53BC38E2CA9CB584EEC53C0F873A1BE6B9CF3316FF1A99C23D6F8430454C552953FCCCBB5F26494AAFC61AA47B85813EB2BF3E11E20ECBE36F4E9D8AD477327F9EF3B4DBF3862FF32FF52A40B7C439480B0628F535F553CFB3AF93F141B793E5D998F391C35D09121F1E55E5AF5DEF044D6BBCA75BE267B1AB325A5D2D3FD0EF0B7F3735C7C6CC273291FC7840A7778BF970BBC23081DD5C357AEF3B09C4EF2CF2A3C56EAE544470C59A1CF9252953F4C6580428587BE1E2D50BAA9E9072B496B2F56798C714D467253DCA1AB443EBAE53AFF2F02F2EEE3CBBF3333521AED78EC7901C56D76382EE06E71AE6815B887AB01BBB7F89A43B6955CF1B4E6A89BD6255B02D3974E6FD87A9F2CD05D517CBE52EE99628D3BC40C3C6CB201A74C56AC0A1A865C84D495FAACBBE6ACD90C3B6BFE2CCFB5678D5CE70B15B8F20B15EEF0EEC0B3EB6EC0D995E8CFAE64E8D995E8CEAE64D8D9B51726AF2C0DE95839DE252DF517E5DA9E71A4F952F7A57DB9935FDAAFBAC404E3094453C20E2190A6679CC75998E7503B6822A8A4EB3C5FE10D4F150F42457C830FED73A37EBCCC31E5481A6BE1A9C195FBCC365D28F34CD58CDF26189D1796FA2065E53ED0EA6616406C06A1C6E7CABE494B48652B54786880D3FC739B6E565000B3620F0505395B35FE9172DD77F6AC10128D8C167A5C02EB0182CFD43B8EF093D2A1C8C080EA41B0010767B972D898559DB35A3B1CB2EC760CD5CF690DFC170CABAEE4EF09B865073ACD5921C4D9BAEAD5C0F74E40FEDFF9F8EA2CB7D1584A7790069058DF35CEA3A1CBD054E309C990E43A7FA8303C6F2F5BC0BDD6CB2030CDB7F2A9D095793C7B179852D2AB9716ED1C07879157BF1B72877970F2941EBE3CC7AA86736AEDCE50EE6392073039275006500AEEB6AB6709196D2A0737EF0B7787A24246B8D194E5A00DA1B0B17B2475D4E8B814810C7AE1F8C5F743E2176760FCE26C40FCE21E3D8951AC1919ADF21F9D0A52ACF1A06171279190147858B302B18F57DEB18FD74959A698AD930C7A38A8B51EAA71C8162DF1B7440BF1C90AA92E34C8D785E66B517EBECA1259C3C515FBC1026DF5840A5F5E48985F807C90800D7CB83F5F6D95CB222BF5B8E991E49BF2E8FAC2EFEC43499B6974F4D76E288DAACBE76EB85FA4EFDD04D94D5A2BAA1CA1C2131EA7D63E05A08AD50793BF892E5F3409EBF86B178133E8C205778C73A90E14F8B5CFB5AB847F156ADC217E687C37D32F12CB70C5EEB03EA58908A529F0F8CC12284EE4EB6D9FE559F8B8B5D5267FD6C3DAC56454B009E310DBC58FE81E91EF614866A6BE78C7E44518B5893E8BB6B3DCD84DE5EE5CB4091A987B64506EBF80DCDF39659A8FBFE34943BF21FBD344D32FCED591C4C02F6E6BF52BA050E1090F8074B8DC49957246F4BE498FBD2DE97FF7F9DBDBDCE94252F76605488AF666E6559BC75D4EA64E9B1C1F5DB5C90FF099DC044578421A3CB9FE333BCFD2E6DED0357897E4E93DAAEA1B6218FAE2F8D9D3D367C7472FB334A9482E9FECBE4D13FF9C061B4DF2BCA89BF3D4296FFCE98F246F3C5AAE4FE4EEFED9E70994AA5A0A4F2E6EC776A6E4DA7CEB67BFA14799CC1D33E193FD4827E8CE4EE48E6780B02443C03B93AC6CB3DB6928811A2DAF92BA4665CE04F1F1D1FB6D9691043BC48036AB140695C1CB29DB3954F2AA3EBFCC97E8DB8BE3FFD5747D7E74F99FB762EF1F8E1AA79EE7474F8FFEF7E081D05D45874134A3754A84FB40605C76770A31FF92948B07126AE05DF2ED2DCA57F5C38BE39F4E9F8EC74034888171885609EEC0EB52CD34A3834D04A201F2B39F7EF21E76A76F340DD8B618BC8C74DA92EADD706FB724BDCE04260AF9DFC2A276B0EE548193A1EF2D49DAC4EA7EC2B1E9344A2676CA0A99680EC86957113B47EBD3677FF717794232F7B1129ACBE46EE049FB389D79F2FBE14631BA841F53F27D75BCE9727EB034ED26EA791F4B7D5C332354FFA35430B236C3F61EB39AC7DD5DC8BAC01752BA8705CDA9D446EEE75EA5A687E332202EB93B057497D6DE631153BA0F8723A7711F3735F85AF48FEBE4DB3FF982E2F3B807DD7E5C4EF711CBC692B98F642A2E957BF83B2A9FD47D1C65857CEEE340F5E9DCC76EA33E48AF2B1D9DCF515D2AF4BD3D4E03DCB7DD2FC6A664E553ACE0C05B960A924B741E786FB2BCE781012BF9CF03C31753A10706DEE5463780FDD91B284B941E78B47CD6F4C0A0850CEAC34FA9360EAF71703F7B9F04521AF5E1C31393A707B83348A9D303401452A78785176409D51CE9C3610149D1C79DD07232F4E14303B29F7387E5008D032C7B6D6FCBAE6753FCC3D165F5479EFEB9C51537783524ED83B4B3C61CA896F4E37B7B29D92DD50F970E3DC046E7F3A17B831BC41940267137CEB0240E077A5C99128303ED6183243BFF49230B7CBB13661118F69CAC3D887B54EB0637E6D1D99A014DE14CE2762ED82D31D1CD62C06868D72928ABCF4ABEB787C5942F583875F8DE2E9D9C7DDC4F9B2EF61EF7FDBB4D5C1E56C1CBD2988F54C5815FD187292FE1AFE50A2CE72737CA95EF05C3AE252CA77988A171113B4280E3139B8780276436777D30384B093837F8DE4A0931C1B8B36183D3BBBCC9381E16A4C0260155BF700E6F37AAAA768576AAA671A9BA0C21183790B66F90FCE9A3270680A5A4040F0293CF081E42068919C1E33D05F559B5F79777C5CCDC813F975743BF55BA93C498E57A6F0F8A296FE2B6B4D37BBB88BA47EDC8D7A99AA6D6D37446EAEF71DDF7A62994407A6FE939C7A6F85E960EFCBC32FEF39CD526FAF4E933FF6303D2101B2C52767DD7EA07335C63A042186721EA6A222ED3D3E5C2C7B25A1BD96FA8E97908A3AA8866EC036C6800866119AEC32A768464D7E3D69066DD196514E92C9EE134D27B2B9EBB8CD42D01D002BF6832E2FE84FFAA1A3FA6537C4F21BE65B8FAD920F04DA6EA48F0C5E4D5231FC21E26B6819809CE07BDB7CC14E09A0490D8C1CC6580F9A48399F6108FA73E1DB569BC7FB33E68BD384897DB796F55036282E891BB9AE5871E6E549203074C485D8239B3F2DED2B18BE936469602DCD1A76C1E4ED02E5DF30EB3049CF6786F79A196322707D6F7493994C3721CF3BC8F01778094F371193B28D776C3276D0C19F58986F7968A2C6371800F3B7CCAE2714F492E5F718071B194C5F10E0A38FDEFDE9E13620EE191773F297B70E03347CA231C805FC434C2610F1BF88BD85FF76804D2F7EEAD30E5D2000FBFD106D9715DDA2BBFF393F61AA547E69306FB21673DC769D6A51CC321ECB922D840D87DBBACA14D00A07C12C8008394B30D871D2DCB3D1C16AE9484380EF008E3961213073E25E50CC59135C1FEFB9FF51CB5FF270D1AA42613DEDB0B5FE5B2CB87301E97A3383064378B3937507DE6E210D05C0DF0DCB4B042FAE200523D733DC79C86C7B2180718DAC6E300731A9D9CC338C018851CC601E0DD698EC161134E4C87DF389031B6707CFB403049F0DE4AE9A5FEDBA80B713D560DC8DCBBB7EFB2D95E43577CCAE0C0777D3E7B7058D0AE1FE00780E6930A0F7F22B71985235D61F934C3BE4FD8AEE7C837349F97382C018414C523950C5C82E2C0C7829CA93830F849DF08DA5CC07B2B4E815CC27EFB4401304EC8CA098847B2B59A7738B0710A947C38308A43C45FAF8DD948DAEF63735A2F04BB1FF417CED1BBB7777639CDAF9FAC147B8F12942C3F7060F648A1C7F058C3E42E8370A45B5E9B5B3808746FCE1E2A6F769CBB2163AC5842424DAA7B10D9A2C9779BB1D7F599E5BEF2DA64B77BCBC4411CB82399593A85C71C003873898E3966C034196E9C41C780DDE4DE1DC9032B7B70CD01235353F086859FC0B65663C182DF67C60245563F840140B904BEE1016B2CD9C68206D3F90ED7A9B144BE81ED9DBABCBE7ED7DEB6DBA8FB6E08532BF74FC16AA2DDBD3D0E8574BDA1BFF8A8497B0F0676310DECA054B97B7B55DEB1308542DE5E3F01C7F71D25E5B86CBF46D61CE086DFA400F600EA111C2DC8A7732E29F0F083CF29758963F6861060746686A3579E4B0C3C0AB2A7E439E856072D9E3E23EE5E65778097CC261FC9A4E9336FD218E040C6DBBDBDC309797303DFE1BEF87EF4FDAE6E582FABAA58A40DDFB628F81C9EB7864492F88468585BCA36D90E8BE4B17D2256BCDB66754A12BDE061E0C92B535501AEB9B35500482B4480FFAC006CCF883A4D48F609A2E54BD5B4C357659A2FD24D924193911A3BCA26B2DA3D58B9E635DA90B76E5E437375C127E7A65571F728A44D6C5B8FB3138E1BCC4CF26B7177AB4D79C408C935E1C9C8178B447CFAE48989317E655AAD0650F33B0A1BE826178709942C8C1A34A23FE92C94EF9398DEAAA923BD086514007DA65461F7B3D2283477A5C2D81D0FA781D5206B93ACCE436B312FD76D93A2A2D29F03521A2F8176729D3B33A8C94C78C0406D9C73C194A32C0EA358B2B868906AF32BCCC1318D93F9AC1C43939CE838A6ADFD1E3906C8EEB20F1C4326322BC3346F541DBFD0CAEF915DD4A7F94E724B7FFB00124546679379AE25333086DF1565568EE832AFDC1A32D8300A0A8D78228A15EE2CD1277E9181D1C2280CA19F691C6E8093DB6870C9A96366610A3508F8AD318634F762159B09AF56A9CA9D498098E41060BE3ACEA3D6B0067158C7168D5D83550D6F3C2B1B29F189ADEC03CA18B5D29F85408091D9654A5903472AB7B0C9CEC89B9D903273B0C8F412659FE48810312F8ADED4A234D51C3C52CDF7A148F53D6AE657A936F612B79A34528C8E7D039E84ACD09D2168942A1E4A5B128501E07969A8A17E7BF4223F107F4B83880FED3423D13591DF19ADFA063CB958E1AE121D9E97861623F7BC3BD1F9783E33121D08B4C3E8C4871F69C8440B7695D0EA5C346B3FD9CEEEE223CC42E03690C1AD26D3232FCCDB06A230EF0ADDC9DD05B7E0E1F46591043A34370D3546EEED2B28748706151FEA6056E2C7DDDFD3137CAA3DEE43EC79773973B696DDFFB57497DB09E493EB3CB8810BC8A00119892B8C13D7506DAC34D0459FD0A003623BCCC22F9DA76CEFFF6CE414C1AF96A7A958E1CE23BF4BAED202B048DCA1770ED6D06AA4EC80DDEC35B86427F6599882F73CA51E42FA33A3A9150E0D5AE2CE03829F2B0F49AC88737EA8938BC3047A675E0DBEDE9F6B260E2026E5B75ACB729E7A7D139176ACD887151A1F1A155024F2EBE6A7A1C9C83302F210D260123D5EE66481393E664FCD06337CC6F6E08559BF61F3EE110D2FE80D60044F0AC84A05B250D975D317BD7B481CBE1864F7421A4EC4216F9AD111E732DC0395BDB3D9125D907804AF933AB94B2AF5B820BDAE510D18F21F1FBDE9CDFF415BFAEBC5035A272F8E9777C41782BA11F02D2A80856074F438D2A2A3D52674A485033A6675A3E2627520A2AEDA8E0586AF85EC0493FF8AA380E62B210CACDE6189E4C3415D28B905BC5C9BF7A8A662C98612B0C65590026DCC685BB3631FDCAD5DA71177DBC68CBB3560F5C14D05A511356D62C64C0D216D8845BB2505A9580D21E45AA48EF8A86913888B56E9F0905A3B0AA6055550B02A08455BEB320DF6D90CC0D1554138EABBEC76D1B7B0EE7496BE03DCEB7CB50E9BD8CA8651313151B12A4D403923B6725953C88C498B9D6F64C2CFDAB90FA0339DD022EF1A9810D336EE480DE8CC88EC28D8E73E0505AB8250B05A0B863E67B08A81554118BA5AFB24A4A4B23022BE816E3FDC4BED2C789534A82A66B5890EF74A6969C3DE67DC04D0B23A2D3E96DBD27E5131EE3BA95E7379F1D96D42FE4015A1580DE1E35AB888973E279D8A8B55E91632EF5B58B0B4DF4A15146D3904BFA9B28F9F7EE75100D362DDB8ABA6D602B9CB0AA002EF6B20F8B4D265E9FBCF4B0082B60646D0543AC0E73F56A838F85A100F6BE0854B732C2B2D2C38DDDE2B4CDFAE20645510A64E597D8BF9C1198B666A62B5091B518D3B616C4257AA9868318481D4D8974B544BABE0856ADDEE298556B6FD4955EAEA06A5E5DA1D4AABADD3A19A356022B4025CA9A6CE65AD981A5883404F7056EFFECAB1BC706CAF1BB7974D1B8C5FC1D296EBE8D186739080738A16584122463A38E2DA03EA1243580451730BA844307AB942D128E922210010D6D0A176224ED76129247F7E60FE268F7F61C8AA7AA319305F6C9830A77069BA35BF474F4F745A87A8ABF76AF71D9E483F594B4589C74AC74F0DF6D186E6E8E0CD6DFEF0C04F41AE332C825629D440026A832F4AA7C5B12F0AE4B01C7D51046D95B2286D6DF04569C5B07D4D0097DCE84BC21F31CA8AD0CA8052810ECD2416A04F56119620BEBC80FD2581B93B38560A43873491CDE8C50AC3E465DD62DF9B168E9EBADD2B1058064F5742F128041552F43894AA0CCBA2D7AB0990F8EA604BA57ABE1996C8E226074E0AE418B5D2617940083196C2CE2B737148AC25D0FA2FC1F744075FA7B03746CDCE906A462F83E4A7034CDEE4C9230C5AFEFED08CF75CF35D41E82928A89A6E6D49A0E9319DB16E7A3ADF0ED06B851BA74E1B3DE9F4A8BA4D3735C8D259B1DDE6C6760DA8E7269A8EE269004CCAEC8D2031A4F8D1AD65C8AED030454939D874ECCB824D534737BDCDFD48CA459E96C1821C9AA5ABC1B9380558D549A722D799964255CFCA30022C096C240D2C868335B5307C4827DA8C5FAC302C80ACBB157A07983A600A0CCCDB66302C323CAFCDA41C0F292A853E90CEB5E9FAD1A4471D325DD9EE159CADD138561AB8AC006D87CD8A8DD3E675B35CCF7053D5BD2FB5E69F511E9731270A983002F3B5193A82EA00AD2A0052034CA250E882E9F6167A7DDDD909D54BB705F8675D94C90ABD2B9628AB9AD2B3938FDB9C44ECA5BF5E23F2B6EF419C6198390981CF5904F66D2EF3FBA2334C9446D43591E205BF4375B24CEAE46559A7F7C9A2C6D50B84EFC32497D6A724DB92BBDAFA0E2D2FF30FDB7AB3ADF194D1FA2E134E7C62E068C27F76A28CF9EC43930EA30A31053CCC940439FE90BFDAA6D9B21FF70510E4580382584EB6D1A8092D892443ABC71ED2FB42FEC0AE03D42E5F6FF07983D69B0C03AB3EE4D709495AE03F36CC7E6FD12A593CE272F2EDA2D403B113425CF6B3D769B22A9375D5C260FDF14FCCC3CBF5B77FFBFFF31B22ECC4520200, N'6.1.3-40302')
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'202205241059042_migrateOum', N'sambocappweb.Migrations.Configuration', 0x1F8B0800000000000400ED7DDB72DCB892E0FB46EC3F28F4B43BD1A3B2ECE38E3E1DF64CD8B2755ADDBEA82DB563E749C1624125AE596435C9B2ADB3B15FB60FFB49F30B038004894BE2468264495DD11D0E152E99003201241379F9CFFFF7FF5FFCFBF74D7AF415156592672F8F4F4F9E1C1FA12CCE5749B67E79BCAB6EFFF5A7E37FFFB7FFFEDF5EBC5D6DBE1F7D66ED9E9176B86756BE3CBEABAAEDCF8B4519DFA14D549E6C92B8C8CBFCB63A89F3CD225AE58BA74F9EFC7D717ABA4018C431867574F4E2D32EAB920DA23FF0CFB33C8BD1B6DA45E9FB7C85D2B229C7355714EAD1876883CA6D14A397C765B459E671B4DD7E43CB93BAF9F1D1AB3489F050AE507A7B7C1465595E45151EE8CF7F94E8AA2AF26C7DB5C505517A7DBF45B8DD6D9496A899C0CF5D73D7B93C794AE6B2E83A3250F1AEACF28D27C0D367CDE22CE4EEBD96F8B85D3CBC7C6FF13257F764D674095F1EBFC2C07778B13728AB8E8F64843F9FA505690CAEF209DFF78723BEC50F2D67600622FFFD7074B64BAB5D815E6668571551FAC3D1E56E9926F16FE8FE3AFF82B297D92E4DF9A1E2C1E23AA100175D16F91615D5FD2774DB4CE062757CB410FB2DE48E6D37AE4F3DAD8BAC7AF6F4F8E803461E2D53D47202B70457555EA07FA00C15518556975155A1222330105D4B05BB848B2D12C167C7EB06EB0D1E098344FEBEC65BA7373042BDEBA44A5B887883E0CD7E7CF43EFAFE0E65EBEAEEE5F1F353BCBDCF93EF68C54A1A147F64093E1B70A7AAD80518C26F77D30FE28F6D9A472BB43A4F822C811BB2D7F706544F9F3F1F63A25798A177A5698A61D6F743F43559D3BD63D80AC7479F504A1B9577C9B63EA98503E5466C7D5EE49B4F792A318DD0E8E62ADF1531A1616E6B791D156B5489237FB1E80E47A723B31E58BF2393B4381C991A5CE4DFE9B707F9D778FE04C1EBCE656599ACB35FF3A5378BB18E07FED2E0C26B33F426C67264D1C10098E5F4E94FA3DC9894B8681546026880192FA35013D1DE0A94C5A1CB80F1F10D6DC15D007C857AE80BB5D0416F1A0CA1AB653475136838A4C6301E5A3DE8E6E9711A1CCE013DAE37681B1515910E14ACE68E9745921714817EDB849016AF76CBFF8DE2CA88661499F80D2AE322D9D6DF9C26E42166F90E45ABEB3BF41EA10A630822199B1136983EE5F966745C58E82EAA20A7F5DB4C73EA9BBB5D94CD7459C7D7393E97A2CC1BFF45795D445F519A068075966FB62922ECD5674AD60F19FC6700D2619EDCA0B33CCD8BB1F7F945798ED7EF4D743F785D0B444EC28FD9606E6B20CDF49D5A54B86C5DA0B2F4E50DBC4BFA763D4BF312414B6723DE1B847919ADBC89A79542BA8B0994458830C137E96411B1469145A4EA41B2083F003F91A4EB79904C34B8A6F80276FF12DD6EF19AD379D5B2AFE7F7A8D8FD81D27CBA4FBCF3A428AB795420EFA2B93093FB8FFC7591FD76B741A60B77A4019CE55915C5D5E55D9ECD307D7C52AC8CB3FE710CAC9779995844FC91E67B8D6FCC75116D8CFB6B24DC17E5D50EFFAE8FB261E2D6DB4D94A4C619B8D1CD010BE6CFDBA4D8F4903264A24765F92D2F56BF44A549D9194684BE42F18E7C2863916EB31D1D1BDDBC1F769BA5712B05C7158C34D7DFF2737C06E5C5DB8CF41A0CEF5D1E7FC97755F3E1F84715FB8AB62D8020C37915C7582A3FC7CC8C5667F92EAB6CF2945D056BBDACC29C9B5A41FD2C8D924D092B0C45B1E78635E51487600B55810837F3D56CBECBD749E63654D6543FD4BA8575A84D33DFA112606E236D5AEA074A1B58C759B71AF439C4247B028F52C82426BF6FED355E95DB0FA83A61BD4F6AB8E7F86A44F890FE72A280FDE1C8B973275B3F7595AD9F9D2E6F9FFDF4FCC768F5ECC7BFA167CF1FE3B7D55C4F37947CF59BF1C87713C5F4394A77A151F5DA0DF41008BF1B28D8FDDF0D7498B8F86B4205FC85BD076B8CC13BB567FCECBBE7A4914DBD1D84694E8D7C9A33A0D776217751F8DD42A0EEFF668159196C4A26D487EBE73AFDD978F784E35E63D6F257E5D5BD66D3E0253DE89D4C2859AC423CF3ED30832626460923239477F9D6BE366618E87B52A05590592F2967259B683DBE7854E32A854744B76F5AF7ED95E6EB33BC16EBBCB8F7DE645CDF07AA2CFF8B3D90108A5DE6A5F74318EB77A0B20617BF1506DBCEEFAA3BE34B7E187B0F42CF20E61E3693FC40360E56AB7B573C3DDE5B5066326D0AF42D5CE399638AD777BBCD3233BF108C849A7061302B194FDC9F13F48DAA77BBAF4AC71DAB55088AD729A0176427E98DD8B2D30B820D14BD20DC6A905EF06C5714288BBDA500D6EF206C6B70C5CD026566492090B49CAC4DAFA481908C29919E51EEF3FFE463FD0E7CA8C115E4F3676B313B08C3601521C404DF96F5D6ACDEE57164312F0885AFE651CB47C148F71DFD68C565B766FFC151F5ED6F509A7C45C57DFDBCE06B8ED7F53DEC72DD2E6F16E9C672DD8C648F5F8E61F1ECCE5D79BC23E69A7D351A72FFC3F7AE06D75E693518D52E51B149CA921EE4FDE8DE4138505E836BAE670946A2FA4BC85BC7A2B76017E082DF6C2A77DCC8BDBAEF376B63E55BCEDE6398F9BB34D47E3BA3EE7DD8150FE93CEC4BEB03957554B6596C8E65846CD1B09E3E793A8A3067B1011A27CAC9C0835E032D9472DE31068B33456C1E1629BA4AFE693E5A02067BE9E3E33867A098FE1E6D8A1F1B0533814B6F83A9CF4A7FC82B33CFF57372B68A464685366B74A3B6568522A5915618525BFADAE67A08760EE29CAB101754747BFB3DBE8BB2B5B7D0C6FA1D94241A5C4154A14CAF3FD4382584814BC14F08C5C9264A8F8F2E0BFC57131D117F935DC511816703EECC9DE708AD9651FCC5973B59BF8398A9C135C5C7848AD5EE22368E0FBD3572C848D1F4DEA3B2341B713DFD5B081DA9D77E7A8DF7C5D57D5921A357866E5775BD0F27BF06D76D73F804B17CD920F23D38F49322338B75A32ACCEB854CDFAE76EC05CC8FEBE4FE07BED3E0B21ACBBABEC758D81B0BAEFF81225F7B16E5F975388C59B97A97ACD0BB24F3169CDB8E073ED6B1065145D1654A13A321C048EF8B6B86DAFE311CE2BD9EC61632471D0C88A8C7A5E41391AFFFFB98D0F920B76B70CDF634A68F0E38626027FEB94A17E2096C03057B821B0E529E50AFC15F12CCE6FE76007CDF03BB6B70E1455A5B54A661ACA66A44BD420A5EBE5AADF8086BA38DF197DC1A90685491E703FAF6F66B8F373FD6EF20F0687065CD0205F96064C0EC0F7AA3084F0CFDC524DE6B0CDB24929AD5EEEC20104A27C6C76285F0FF15D5FAF91D1A5CD7C3B9A1C19593351AAAD6DF16F96A175743C1FCD90580EE3B8E240EF5BE00BCC62465CC47599AEA0183B2712FDE3FC8843A5CD9D71CB3CA877CA0D54688CBF6EA2EDF0ED5CD32A792C166289CE9FAC5F87E066713FB35D8825106CAD723638DEE693E26B395529819FE5E90B684A52C52FE38537D1D655F6641FC2AA647B3D5F26D5CEC73CCFC138A51B2AD6AF3A6E965F5571B32F3EB7C892EA3EEFE0F7EF7325391A107DCEC29BB3AB741405D452FED9BAE49A7A4126B14D59454ED6B83D419F06807D5359107C56A34836AAB7D07450E31C380EA6A7930A45433105A35485557A3F493C3489FC3C787065769BFA7463AB6F26F99D5BD7124D4A15C537DD136618AA7C71CCE4BD613F16D14A3659E7FB99C428D948613623DA799E6EBBC3E9A469EE236A820EBAB1C28CEDC24DB3073ADD14DA3815CDAA5E691163572149B47463FCB393CAF0BF065917C8DFCA39834DD0E128506D7CA29CF5A1076F62035D58EFA939A763BE8F034B842E8CD9A352647FDF46AA11AF72C4A03B77484E3E0FEBDBABFC830ED8945FE30DA8DA9EC67E1A2866B662B3A578BD034CE52BFFDBE25014583A8A9DF25D997CFC90AE5D3DFD4540A7309FE3612FE3D501AB19867803AA439476EBA469D5244AE5354234A83605A1A0659D6D3F0E5DAE10CD7D5D497602F6371AEEBE1F2D51DBEDD1AD1B7A2A1F770072EC861C50FCF62C33292DB8E3C0263BCCC91C6E0E804EE8A7D7FFDAB673FA0A5ED001F885D9B1BA53D773EEA9BA9C7A5A16DA8D3B31E5FEF1394343A9CA21A5C16D97FA4DD42FE351E47D38681F97D87CA3ED734EB77D0856870FDD92C5085F10D36CD6A893435B7DA5218840977B149B25D30D77440D718634A8683AEBD871899B49710DB343762CBEEFA011B28170FDC6AD095F3BB30A07E47C1AC97CD433A0E2C5E8FD35E009FA2CC3B3803E973102B34B8E6112B0849282AF77BC69D45D03A29310DFBC8097CDFC3E1A0C1354DA6A540CEE30EF9DCC3604AEDE9DB034F89A46B9F6C5A93205BE5CBC18CB5B665730F33D44D541409DE2F516A552884411859BDFEC2E0B1991C85C1822C9179C26021D9142FD3281E7F3E0493DD2F330CAE9AC3A32E42C9508D6A1A65EB9DF9CB299047DB97244D878E762CB738ED87121D35F885C48B0B374DB3EEF348AD55BE8D8026C3AC4EEB31789A9D924E0769C7C4B3D749358B772B45CE3DC09F1E9C4EF7CEE9F413DAE685B7C54EDDEBF059AAC1355BA6604A9610CF941F690CC0E4ABF9D41827C8F5E7240A81D562AD325112C137BBFA7E2C87D1038BE98526E281C53026EB15A7C3C151311021F0AE46C4202308A7B90A45DD46D14846A4FA866FC50B4652252017C92D7C6D4DC801621A595DAF8C8914EB4643EB068967FC72F4B92E0E0FA4336B329D29CD064D18C844EBF7495CE4657E5B9DBC2AB71F5075C23A9ED420CF0B0C8E7C5C9EF0107F3872EED731C85357067976BABC7DF6D3F31FA3D5B31FFF869E3D9F9E59A6BBEFAD4CF3E3B8C620E44C29C1738AA7F74DD3AC3BAED45AE5D4029A0C3ABC1A834EBF738B763A7C5B6A707D25AB7353CD1339E96B2003DDC7FAA1F7AA2CF338A1746E50BCC284DF61398B06F2633FE86D2E8E0C4B8C47F51ED4F6E8766BBD2A64C7F38DF102E13D916CF12EC0437B79FC2FCAF45D90B0FD0E20A9851011C9A982A4112DAB244AB1B84F34554956A9FB2EC9E2641BA5AEE39100385E2084522D2AB9E60DDA12AD6856B92EBCCB18F87EF0785AB4D221635BB7170B8EBBCC4CF76BBEE48351EA98406C06B1D7AFE471C59DAB2480002BF1213245B84F4E4E02F1123C8A0918085E4F17C462E8D25978069724EB8C4C81105D7B6EF0ADC0038935F03A8D04A800DB00F0421D3C10EA290E1B68215DF0E2E6F3F1C876CBEC6EE987ED591A259B52CF2C6073906BC496765A5B91004CC4242CD28CB6B2B3683F96324E7B0ADE322E89CB00982A752FB88C462176E7B2BAF9C85CD620B170196D35119789D39E81CBC42579705C4626E5CE64B4F5C83C56E3B0B058AD2C9A84C38439CFC060C27AEC3F7FB5173CE503BBE823AA9183495482067A08A30E14AFF8714C2A5FF1EBBAF75CF33ACDD7977959DD903FDA14923A2A83AD211E620D7D5808060E709230D471D8C8389609B8C9B8D02EF8F97EB331973D81BB8E173CB2B973DFFA4A271FF6734F070FA064A31985199D473601633A13C6490BA1A49C9E954DD554BA365E31E4D55579A40F33EAD3F10208463E13AD639A90FDB40BEFC3767B733EFA9E8A1E67E120A6DB93736FFED3EEE19F71DAC45106D53AD841A3B5EF77E75A33503969F2C3A9F18D839946A16F5CF407A2DA97C23EEBA8AF8B01DDD1BCC914E1CE509AB8D11CC82E18F5284C040FC0856EAA1D8017EBC08BE98298CFB53023BBB401B9CDB455A3730F661725A23707B20B133E22BBC803988C5DE4C57441CC47AE9F915D68182E335DC5885C83D94488E1C581AB03828DC81E3CE2C958835F3C17A42C7AE52C2CA1848CD351521F3FAEA3E7250B6FEACE20DAA873C2C5C3C2D98DC22ABA214CC030BA4575BB7CBAB094B3B28EF13C8163FC056199194E1508F5846CF2B04E164390332D695D229E71CCC34776F46020875869309611CDEA1C0635C1679403015C4601C4849C8507E118473ABEB0043CEA38A28B53E6CE74E6384900F01179CD389609CE33E342BBE097E399CDC25C807BB88EF8265FF18EF262681B77D632B899F3D762ED3C3E0A43E947300137E917D705791BB460261E921DE9F444D67AD5F11C543B2BFBF08ECE154F013BE281A41DC504579E76615D708B0EC573B290D17405747F0CC036F35AAC0083988E5F1E94AD0AE06DA6A3ACC9F54CB56B836CDA4C867306AFB5E9ADE6F4539D808DF42BE174EEE05E1371D35B3A4C120E00F740856A93F9664963057C878264E089350E8465E3E7257306017E852AC007AB3C3E7ADBBA6E814E540A9BC1C0287A03B0FA62B30163566A20A4CEF0CF024603C0A56BF73C0541E01FF92C801A77537516F2A96D81A358E9434001537E0FB08D41B9196C63BBED01B6B6203643AD0F1E0BD0D798D7E1C5AC6BEC003A6BB3041C9268B2E7008D18BFE900D5868516208D62111E4EA78BB542A9DFC66020EC25D1CAF25D127298E9BB7A3B30D178059E9F6A11E408B57B7D3681E51FFB1D01D757920928BB9F1D019A405981B0372C0848F7E26701728ED06A19C55F2020ACCE09C86BDCF0EABEAC1078F4882DAC006B47EF14B1B87B10C8A64D1B9BCF0E7497ACD03B7C7182D058A5CBB5616630C990C4028E9E98BFE0AF66DD36E01AD8B7C007F4EDED570D63B13A2B10FA76B64258C84821385CB51B282D106B77A235877AD7EF0A96CE7556467851DB448F561854DD0F83689E4B6C203A052C0886579CBB83D21DC38A86DC0292E9FF20589D6ED511886E50A21ED5028C44AC8680D401C96D9D3905140844D0EBD9B88FE8A340F6ABF577D6B190AF537814F547BE1300DDA2F29A201B208B8CE5245FD100231094265C89D49DFB8481BF29C4B80E475C7BE003C3100442F8A8B5878168A7A57CE1281F703E811F00B00DE1A47559880BE3B068525C0260A54C910B84796862177083AF3F8A0C4BA18956C081103E8E06CF5EF4B087D844EF822FD21074C2E7E9C67D569A780174BBB72C619F89C36EE3D00A3838988B5330BB98737369BE4F4DCB61F624E76041DFA8C157897DA5DA5709729036CE4C72911EB44A9227B46695D86482AF527321D81709F0EF35CE4BF4F01DB444A223AF66859A89043C656A758BE99851D5FB9AF341D0F10F396804DDBE6D517BCC1EF69F0416C1C1D1529882D9D5929B09A72D31AC88D9B95202C7EB1606AF90DD0910582D4FCF4161AAEEBE83FCCD0B29400CEBE9EE2D0820699521C11657755D332CAAC5CF0D9CA7DED30D989FDBD2E97DDB0090A3B0A40723FAB39F07D379AED7940CA6F51B826569071F235924367B1989C2A1EBEEB4FA158D26734BAE2FC02A999C63844968DC63B8A13315916125340E311C104EC51D68F69D0655377BD8D70318B8E2EDD167F68A7F070784D304079A7DAD60D3CD5C353306062C9819F799B16054CC016894828367AAD8DB03F335DBE40B83D65AE57343EFD48986D96BEDF0057EEF1E86822D848EE87AEB7270DC3AC27B4D7E74E29BF23E034BE06A412D4EC6C1869A5F2041536C5A2407AB69186E289D942663A9BA700E66BFC2D4CC86BFDCA438C5B561A5CCA6BE00B8500B04252C5257C766B72ACCC560B9CA4D44D2801BD6C660ABCA6FB846091E6045944405D082188D30A5F1EBCC3085E56894F0C685D0195E2A80C2B107971B41BB0C164D03644AD86FEA632B18A010EBEAAC6DA671C2C80DC671FC1A40FA211B9CC03A2716F6BAB5DB6AEB5E2CAEE23BB4899A82170BDC2446DB6A17A575547756F13EDA6E936C5D763D9B92A3AB6D14E3599CFDEBD5F1D1F74D9A952F8FEFAA6AFBF3625152D0E5C9A64D6010E79B45B4CA174F9F3CF9FBE2F474B1A9612C62619D652BB316539517D11A49B5243BCD0A9D934C996FA22A5A4624E8F7D96AA33403ADD4342F4E0C256488A652923D47B15EE4EFC6A01C88932F98AD49C0BA753DC753252DE8AC91E6CD48ED8E01907CE15101245C38CBD3DD26D31B2CEA7BCBD1B17948B6C8D976A875207708665DE30F91ACCD751D6A1F02CB550F804D92A41AA19306EEF0FFD8A679B442ABF3441EB658E30F9144E487E0BD56AC0F4CD058EE531E122B53A1BC58487C2CEF9A85B26DA4D34CDE8B5E3BB5BE2543ED54480CF0D8A970F771762AF957EC5F97F84190599B95ED0F9DBB078CC1446ECD757B5058DF771CF23651C479009AC0E27A18CCF9403812340E097A28F5D4D10A38BC851A7F88F271C597EF0D030661BD7E4C3721BB8941907838E6F0487A8897459217492591B82BF5B88F7634EBA27421B1429F39B62957E5297215EEF0DEA168757D87DE2354612ACACCACD6BA436E3A7DCAF38D0854A8F0BAD1599A44E95267C5EEB0DE66C049D016BAC3B9289BC9486CDB15FBC0BA2EA2AF284D01707C8D3BC4B37CB34D11FD8250262BD78D2559E9A060AEDA200C222F44487CB9CFDA9DEFD2F44D742F2F5C5BECB16A345DD3EAA3B4BFB8626F58F2B6E28A3DF91F97ADEB54EECA1EE8AABCF6010C51A8F0986F9A9748593956E843CF3708732792AF83AE786FAE56FE2571E80DAB7F3D75B8684D9DE796DE67A28D4E27E629724BBE653D046F1B8471E843B54C2A91B8620F3921824075A51EA3C24732F9EB22FBED8EBE960B63932B7D6EBBAC8AE2EAF22ECF94BB8EAF7187F80FE28D2A0D909579C8907999A8E25A57EA7167E2C36F5D441B992BF8729F33F66A877FD72A75F194E52A3CEE920D7570116E910DE0F3628581C9759B141BF9F097EB3C281095E5B7BC58FD129592A640ACF1B88B51BC235F00F8E2DD6CA5BB58ACF2182561CF0FBBCD52A68750D10B9E6645E1161EFCF82D3FC7FB2A2FDE662487A5CC954AADC76993C75FF25DD548E47F54B174ECA8D53D60036396EB3CD401718C65A573CCA2687596EF32E94B0FA8F65381A8676F57BA37F72D60B13EF4F2557DB0FDAF5F0718E35CC061745774C8F503B02856B7C59EB03E47E90E02D694EF253B69BD0FFAB353ED7B3F8C9D3430F4670F6E8ECB4886E3423E7A842A1F1D55DDE737A4A8A9B88AB198760F5843E773D19F33A87FDF30C680418C7B50B0C82A3C145DB495D928C7C24E0CA55713A4C29F4ABA8EBA554DA4158542BEE97BAF14CDDBCA53DFB6C37C208F8195B94329EFF2AD0C8595B94341DF9302ADD439F1E5EED0969412C9265A4BE0840A5F7825A09F146BF6672F886E3783B784C1C9C8656318BBFF4555489DA35508EAD02036FD2803771D872A721E391E922DC79C1EEAAB5D7527ABFB5999973A057805EA4A3D3E5F55931B6F331BD0B4A687390D35F0923F1ADB426F38F288B8629F279ADD6699294A1DAED88F6AD0C3115FEE0EED7382BED1AF685988162AF6E60CE19C0F861E223ABF0A874344DF751CC9276EF065CA112FD678C830C95AD2A2D6251E100016DC3BA9A0F3CD1ACE2DB0BF9913B7E8BAEEAB9CBC5515FF5B5F8D7F957F41993C91B6D097F7AB77390B54A4B23F57E903B7A68A2A368935EE10A9908DCB6E153B4EB1666F768718576FF88B2B1785AFCF9BABA9FB483BA5C179A31EAC52D5233B1701AFF0C1E4B7F8C4BBB08015C45FF4CB090AAE108A5E5CBCC0FE143301D9E7F70035B9A86007684D3D3A3B47B04005A1B8411BF9DA95137400FEE23B371C8506D06632AA002FEFFE8FEE41740ACA539FEF2BDFF013C20659A71D72C9B2AD871EDEA988B4BF4AFEA95840B152FFB1A9BA27B1662E87A73E26935A2AD75DE48171C5DEB0D465132A3CF6692E03AA4BF6E6F4EC828B0C3D3D7501531C4E4F7DD77DFD7C674A21791C7CF9D4CF5D8532AB4233AB9978AD8B463E94D7DAD8E5FEBCA6EF3AB7FCA48310C254319443CF7B5496CA03685BB8579CC687AC0FC16F5C80FB7E5C670230CE3977DBF0BA7A9D8935EE103788C898F28EE84ADD2165CACD98EDD7CDA8662918CA454A4E037F3EB283188793C29875DC16F9E63F50247DB774A53EAA6F150E2B7B4C3CD825B518CC7C6D0A8C1E5CA7EF3B0EBB55E42B94624D13F99944AE7387BA669D54A95CAAF21004A9739CFCEDD195FA42528F6ABE7C6F18530A74399439C5A42AFE0C6AE9BFD7FAD1DE6EE033915E4A693394F442021C7FCA9BBB8F43788C73ADEA1BBA525F48EA9EE7CB3D942A97AF562BD53B962B7687F54B0E79D075A57BC38E5D4EA4A19CD86650F2E7427DD771EEC7ACC1A7728E58E30F1150094B55FE302F549B59A9CA1FA67A858B3563BD261FE40061F709C9C4866E403EF598FF1E34F61E671BE604A50CA22DF43006AAC3E2CA90B86277587FCAF15FFEF40BFDB22D9258364CAA8B3CF4AB4919AB5E855DE97EB16F20C6EDCBB253492D17D9D71CD3F1432E01E98A3D8456F57DC4372ECB5DBE95E7C3CA3C0C881B3B3219125FEEF5F8D3DA475D28018BC43A9F311AACEB944A4F8F69C045DACFD7FC9E465454DE70850A7778BF1731DE11848EAAEC28D77918F847D917155E57EAE5F74C4E20E8F55CAAF287A90C50A8F0F08B43314AB655FDAE2AB9C789551E63DC90915CE74B7419C9B78C5CE7F130D13CDBC9BB8F2FF738111E420CCC3A40FDD0FB020AB5EF705DC0DDC6116D4A700F973D766FFE2D834C80B9E269ADA6B74D140D094C5B3ABDFDF56D14A3659E7FB9543E93C41A77882978D9A43D6E99345FE775E60811122BF55977CD5DB3ED77D7FC599C69EF1AB9CE172AF0C52A547879790277D7B2C7DD15E9EFAEA8EFDD15E9EEAEA8DFDDF5202CB3BBCCD143CF719667DAFF28D7F61CE7345FE9E270AECC713867A350934B663881EA2CDE7D08A4E939CEC75998CFA166D0E4A092C479BEC21B9E7A3C08153E1F5861C3C1FE5EDD5F649872F117E953832BF799ADA2F9B8F4D57C30B745F583B42BF78156D15900E174841A1F917D4B621F00E164F90A8F078C24FBD2640817DE2FBA620F0505B95B356EBC72DD23FBAC1072430D3EF43A607D0E3E53EF710E3F2983950C0CA8EE051BF0C3972BFB8D597D32516BFB4396BDE3A1FA398DD6FF82993094946B01B76C4FDF4E2B8471B6AE2A1AF8CA04E4DFBD4F89D1A5A31B4A6906A90789F55DC7F9686049F52A3C2119925CE70F1586E7030908C5E41D846993643BF95660651E9FBD31A694F4D55B17ED1D078739AF7E37A47BF4E0E4291DD1798E55ED3ED5DABDA1DCA7280BE03141A0F4A014DC6D5FEF1232DA44CE47D116EE0F4585249E8329CB41EB436163F791D4517B1266F0160E397FDB27E47C0A869CEF4A7B8C8A8495D78CACAEF21F9D0A52ACF1A061BE9448480A3C8CB18170F56BEF70F59BA82812CCD6510A7D38A8B51EAA71C89432F237A40CF16485540F30E4EB01F62D2FBE5CA691ACE1E28AFD6081A6A642852F2F449D5B8B7C91800D7CB83F5BEF1461B12BF590F448BE6479746DE1237B286992430F7EED86325FBB3C77C3FD467AEF26C8AE934A51E508159EF038B5F6290055AC3E58AC4E247CD579B3878B5D044E2F810BEE388E501D2856779B1E5D89D82DD4B843FC485D8F93AF12CB70C5EEB03E2791088516783CB3040A67FA66C7647AF171AB2BF6D283EE5DFE40D884B18FEDE227748BC87B189299A92DDEB3F3228CDAA483D5FBDCD84FE5EE5CB4099A4B61601E05BF1C0A8F9C32F4F1773869EA37647F9A68FA8D233A92B425F94DA5BE020A159EF0004807E14EAAA499F2920C157293167B53D2FE2E59016118FCE957B359D7EF2ABEC31FD674E6E5167F1413316085680A423CD3681995A86E727C74D9E4ABC177328DE971421A9C5CFD999EA509951B5883F75196DCA2B2BA2686A12F8F9F3E397D7A7CF42A4DA292A45F4B6F8F8FBE6FD2ACFCB98E891B65595ED1FBF4E5F15D556D7F5E2C4A8AB13CD924719197F96D7512E79B45B4CA1718D6B3C5E9E902AD360BB97B03D609CA93BF332865B9123EB9B81DCB4CC931861DBEA2D568772F7E43F732991933E19BFD4877D0BD58C81D5F0087251902DE996465E96EAF23615468751955152AB2EE203E3EFAB04B5392138D18D0A6A5C2A03278362526C572A8E455FDF9225BA1EF2F8FFF0FEDFAF3D1C5FFBA117BFF70449D7A7E3E7A72F47F7B0FA4DE55F5308866B44AC8E1DE1318B5ADADCFA01A62F6352AE23B1229E37DF4FD1DCAD6D5DDCBE3E7A74F8663201AC4C03844AB0477E055A12607D3C12607A201F2D3E7CFBD87CDF48DA601DB16833F239DB6A42A1B3ED82D598B33818942FEB5B0A81DAC3B554A12EFFFD77CF948488267E27F38D24E83CE44A6AC9089E680BCEE2A62E7687DFAF427FF238F1295D9490E3DA11B6096F3C73E4E679E7C3CDC280647F1634ABEAF8E375DEE8FCB22C90B3A7813F5BCAFA5362C9F11AAFF552A18599B617B8FF91D8A56D777E83DC23B215BFB5DA82EF01BC09FF27C131A34A7521BB89F5B959A1E8ECB802ECA66B60CD032A9BCC772515E17D15794A603E19CE59B6D8A3A0BD9615383C5A2FFB189BEFF4F5F5098DB36087F90927C5C41B7DF4549D2BABF89EE072D1BFD3A5E7DCC06335503691C19B5A870D9BA7EAE1D4659CCFBA1409DA57989CC2BE746C73696B42B1D9DEFD1EE067924D7690079DB5D30DE6E99A93291D0A65FC19E52960AF2BC33140ABC37DF45230126671BF98B98FB908459A1E1539D5C5C3591280203FF47631F6400FBA337D0CBBC4CAC32519FD15EE3E3675D441B0BE3F5017D515EEDF06FBA7D06DC524D1869E3E07EF4BE090850CC06B749B1F1397C01CA343EF9BF44A5F2D5DE4B66B842F18E08ECF8E6DB6C8340A47CCEFCAEC3C20BB284D7DFF273BC1DF3E26D467A0D82F52E8FBFE4BBAA9172FFA8E2A137740B70F0D05EC531163CCE31E3A1D5591D638ABB2C7B681CE0B3D7F66DC97AD2E21F8E2ECA3FB2E4CF1DAEB8C6AB21691FA49D35E442E533B99FA551B2792442C97EA97EE8CA52156B888D4EA17D8ED25D1F70BD3883464AEDC319B4237BCD521FFB801EAC3106EFD41E3648B2F39F34B2C0D29D308BC0B0E764ED5EDCA35A37B8318FCED60C684A50F4E182FD3A26D82C7A8CA6EE3A05655FE371F4FBF2524D23EC044AC6BD2656217477CC7924C0D1CE32F7F49740D077128B21C8BC9694D28D5F6480C9D5F04A418F1750B1F23ACDD7706ED2072BC54CA95A21CB4792D43F92A5E3B9C1FF9947EC3DCC306357DD5914CD3D5E1E08A5823C3C80E61DFDB4EAB0198702CB59178432E521AB9FBC5CC30A35342E944C087084928E6F1B4EF03E27E81BFD8EEDC45AFB3DE27C4AB060448FE4FE6739F732E0A0ED7781276B451BD80FD058D7248B6FFC48281844D2D9422AE75E646B4378068015CB019E83C0ACA90F4916FDCE1B2A1FE2B25BC0CA2EA43E828FA2FD5878B799D20D74FC0CB6D928FB3E98BB9344CA81FC4844C729A56EB684979A14540F7611759A95812A1235A5B7A7FD96D4DF43B4F7A6698DE391D0738E4DF158960E7CE31BFE466C35CC3F7DF2D4FFDA809E290C6651FBBE6BF583E9AF1D50210C335376F55390E9E922F011A057C93F6D7BB7AFFF4308CBBE117D297A1872010C5383086F99DA000EB18675E632B30469E11DE7E399E5E8782C5278882F48A64818A6BC1FAEFE2FF8C9A0187F9CA5C49D10FF5552BFC0532C72115F4D5CFD34D4757DDE24047F2CD7F570494705EA622ED5C30CD7C1DCBF8FE7DC7B5496C0AB8F38DEBF59BF49BD384897E2FEC19E2BB7CDB608A2A6DF2022435E0C3A1B32E08E08A90E3027987FB074849F774DEA19970BBBCB5CDF9FA02C6BFD1EB3049CFDFDC1F2422525900FACB29352C987E5B82E82C318707B9C723EAE8707FDD87EF8360E21A33EDFFA83A56297B83DC0DB0C9FB97DD8D72097B63DC0B8BACCEDE35D147016F4077B4F88A9D407CA7E5212F5C0778E944E3D00BF88D9D4C35E36F0A3D65FF76AD4662E7FB05BA7CD81DE5FAADD76C9CFFB03F9B373CEEF37069AFE2B882606D059B5F9D0A755F50059C71FECDDCD652FEF4FE620073CCBD6E727AED5BD06BD3CF0B9CEFD90773D87BDC548A9D14358FB8D6035637749B546640280F2B96B030C524E921E76B45DCAF4B070A5DCE9E3001F61DC523EF5C042999C587DA46B84CFB6EEB7FFBB9E83F6FFA4B1CED41CE80F56482A5D76791FC6E352AB0786EC6663E906AA4DB81E029AABC9A69BD25FC8BA1EE0544F5DEF31A7E175C9D7030C6DEB7181B989DC52EAF500631452AF0780B7D45C83FD261C992EBF6120C7D8C2E35B9482B9CD1FEC29BDD2474D7321AEC7AA0109C71FEC77D96C5F43977CA6F3C0B23E9FF43C2C68D7D07C3D40F3B9D0FB7F225F8EAA09E1B3A3FB7EC2B29E03BFA1F974EA61092064561FA864E0F2AA07BE16E404EB81C14FFA8DA04D61FE608F532005BADF3E51000C3B64E5BCE903D95A4D971ED8160ACA991E18C52150B9D7C6A427EDE3D89C568160FF6395C3A9C51FACCC2E6727F73B2BC5DE830ECA2EAD7960F600037A0C35656789CF4792F29A94E8D33E76E9538D3F0AEE866CFFC63A24D45CE087235B784D6089C65D3FB3DC575E9BA3FBC132F188419B065BF53A45F5ED01387509EA3B64C0750EEF71063D066C9A327C200FACED31817B8C4CCD1C1E167E049BF60D050BBECF0C058AAC6E2F3D807279C7C303D6184E0E050D6621EFAF53EBF28F871D669B8EDC4FEC6DBA0D92774358F6B93F05ABF9C11FEC752864190FFDE2A3E61A0FCC70077B4E495C53337C3F585179CFA2AB0AE9C6FD0E38BEEFA0538E4B526E64CD1E811B68E6720FA01EA1F3823C9D73B9CCFB5F7C4E19971C93CE8400A333331CBCF25C3EF341903D4F9E836EB5D7E2E913793FA8A434F092D9CE4732E9FA336FD2D40540A2EE072BC309E9BE03CB705F7D1F7D1F9584F5AA2CF338A17CDBA0E0530FDF18F2DFE21B82B2B69424B7191649BF7D2256BCDFA55542F253E161E0C92B5355016EB8BB550058578800FF4501D8DC11551291A43944CB97A8D9D22F8B248B936D944293911A3B9E4D64B55BB072CD1BB425DFBA5905CDD5059F9C525BC5DDA29036B16D3D5E2C386E3033C9AFF9F2469BA9AD2324D78427235F2C12F1C9C98989317EEDB45A1410FD3D0A1BE826370E1328C963356844F7E55928DFE65EBE5133DE7A11CA7800B4099E85DDDF958E4273572A0CDDF170F66A0DB22637F43CB416D309DED0CC3AA5FE1E90B20F0AB493EBDC9941CDC1C403066AC7B9174CA915C761144BF2290D526D5A98393886C634989563EADC4C3A8E696A1F23C70049A91E02C79089CCCA30F41B55C72F75E5636417F5D37C2FB9A5953E80FCB6A3B3C93C62C90C8CE127A2CCCA112C2FCF8D21BF514741A1114F44B1C29D25DAB44032B0BA701486D0CF741C6E80531F6970C9898566610A356CFC8D31EA38F7C52A3613BE5AA52A772601A2D84380F9EA713E6A0D6B300EEBD8E2F76BB0AA01B167652325A2B5957DC03346ADF4672110E0C8EC32E55903C7B6B7B0C9DE9C377B71CACCC122D39F280FE91C1102348EA237B5284D35178F54F33814A9BE57CDFC2A556A2F71A3493CD6D1B16DC093B02B7467883A4A150FA529198501E07969A8A1BE3D7A911F88BFA541C487769A91E89A5C011DADDA063CB9BAC27D253A3CAFB989CEC7F39991E840A09D8E4E7CF8114AA6BA605F09ADCE656E22B3F808B310B8096470A3C903CA1FE64D03F1306785EEE466C12D78386DD948073A34B771C87E0985EED0A0E2431DCC4AFC71F7F7F4049F6A8FFB107BDE5DDE395BCBEEFF5ABACBED04F2C9751EDCC00564D0801C892B8C13D7506DA074AF8D3EA14107C47698855F98A76CEBFF6CE414C1AF96A7A958E1CE23BF4BAED202B091B843EF1CACA1D5C0B30376B3D7E0929DD867610ADEF3B4F610D2DF19B456B834EA12771E10FC5C794862C538F7873AB9719840EFCCABC1D7FA73CDC401C4A4FC466B59CE53AF6D22D2AE2BF66105EA43A3021A89FCBAF9696832F08E803C843498448F973959608EC7ECA9D96086676C0F5E98F50D9B778FA0BCA03780113C29202B15C84265DF4D5FF4EE21E3F0452FBB17D270220E794B47479CCB700F54B4CE662B744EE211BC89AA681995EA75417A5DA10A30E43F3E7ADB9AFF83B6F457F11DDA442F8F574BE20B51BB11F02D4A80856074F575A44557579BD091160EE83AAB1B155757072262D5762C307C2D642798FC2B8E029AAF843074F50E4B245F0EEA42C92DE0E5DA7E40557D2CD95002D6B80A52A08D196D6376EC83BBB1EB34E26EDA98713706AC3EB8EB83D288BA6E62C65C1B42DA10BF8EB20C242DAB80902C69DD4DB54CEDF005BB28158B500DE1E25A242EF3694DA7405C75950E0FA9B5A3E8B4AC0A8AAE0A42C192183B2D5CF7300760615510160CFC266E5B58CF922E4108789AF0D53A6C622B1B46C58845C5AA34014F32B1950B734086525AEC7C2313FEAE9DFB0098718616396B60425CB771476A4067466447D13D282A28BA2A08056A6A9DB6449B085BC5D255415858AD7D2252A6641811DF40B7276EA57616BC4A6E5F15B3DA44877BADB4B4616FD3C80268BB3A2DBE2E61AB5D1C32EE3DA95E2322F9EC382129A68A50AC86F0712D5C8E9836D1A28AABABD22D64D6B6B060E1D3D2A988845A0857DE3570DA78CD03308C084641ABECCB553F5E2980EB62DD3295B4D60299A53A5081B73510FC6D5DE9B42CEDAB1980A2A981503495F6C511DE60541C7C2D88A76BE0854B230B282D2C38DD3EC3BA67040561570561623A78274A892F1F5A4CFA79F11A7F278C3422A78AA92E8630901AFB7289DA7615BC50ADDB3F85D0CAB643EB9702758BD6E5DA3D5A575BA7532B0C8189D415E04AD13A97B5EAB4DB1A047A8277F5EE1F6F960F37DB479BDB075B936340C1D294EBE8D144A9908073FA2358EF23067038E2DA035A2043B40751210D687A307AB9425194E9023C001036D02DBA10A7EBB01452980260FEA64006C29055AD0D1D305F6C9830A747A2DDE8EFC1D3137DF121EAEA9DF57D8727D24F56BED5C4EB4A874F0D763D87E6E8E0A46E7E4FE1A720D7191641ABEBA29080DAE08BC29453F64581FCB0475F144109A72C4A531B7C519A63D8BE2680A7F1E84BC25F31CA8AD495014F857A68A663017A891B6109C63F2F60375060EE0EFEA2C2D02105281DBD586198BCACD26C7BD78583A76E77760496C1D34352BC0A412D587D1D4A558665D12BF304487C75B0A5521DFA0C4B64F1FE032705728C5AE9B03C20843196C2CE2B7371C8584BA075CB82E5440717AEB012A366674835839741723F02266F725012062D3F7AD0F19E691E33849E828A8A766B4A024DAF5363EBA6A77359019D71B8717685334EAF56B8E9A60619702B26E9DCD8AE0005DD44D3511C288049999D2C248614DFFA1A86648586294ACA41DAB12D0B364D1DDDF4AE04032937F2B40C86F1D02C5DEDE8C529C0AACE7A2A729D692954F5AC0C23C092C0B6DFC0623818890BC38774A274FC6285610164DDADD03BC0D4010B6760DE363B6891E1796D66CDF190A252E803E95C69D74F263D6A9FE9CAE6BCE06C8D36BFD2C065056833ECAED8386D5E37CBF50C3755DDF7A5D6AA75948FCB31270A586602F3B5D96F82EA00AD2A0052034CA2506031825BC3C3B6EEC5A2D64B3705F8679517D11ABDCF57282D69E98BC5A75D460211D7BFDE20F26DDF827881616624B23F67E8D8B6B9C86E73666F298D883591C220BF4755B48AAAE8555125B7515CE1EA18617998A408FB1CA53B22AB6D966875917DDC55DB5D85A78C36CB54B8F189DDA609FF8B8532E6171F69968F32C414F0301312BBF963F67A97A4AB76DCE740EC660D086210DA04D926B42427195ADFB7903EE4F28BBE0E50B37CAD1DEB35DA6C530CACFC985D45241783FFD830FBBD43EB28BEC7E5E4EDA2D003B113425CF6176F92685D449BB281D1F5C73F310FAF36DFFFEDBF00E8E9B0D3AA640200, N'6.1.3-40302')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'00a0d87d-2f99-43e6-b195-3eb3307d0d9c', N'Admin')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'19f3e97c-b7dc-42ed-8044-71602d39491f', N'Manage Balance')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'3407386e-4efb-4fa2-8892-d7b23a43daba', N'Manage CheckOut')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'4eac3236-3fe6-4ab9-836d-25bb9abe5516', N'Block Room')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'528ea9c2-fa3c-4468-9e43-0bb108c2c431', N'Manage Setup')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'61146f74-7cc2-467d-8fac-461ea39d7e3c', N'Manage User')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'656a6e99-2820-4fff-9905-4284c19d51b3', N'Manage Other Expense')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'68602f61-1321-4d90-bcfd-b9b49ade5f34', N'Manage Payment')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'7e1546c8-e864-4051-b10d-5ece01b5ef3e', N'Manage Expense')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'899434a7-f4ca-4755-aa98-472fbba9c1a8', N'Manage Check In')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'9c210ebc-f3e1-4048-a70f-8823c388d0e2', N'Manage Floor')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'9ca2ae26-96d6-45cc-8617-054eb0d81e9f', N'Manage Staff')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'9d26947c-09f0-43b0-9f4f-2cc669024fac', N'Manage Guest')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'b38a8966-303e-45c9-911e-fd56f401d859', N'Cancel Booking')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'b6a888de-87f5-4f93-93cb-b48ae07d5cb4', N'Manage Register')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'c668013f-aa4b-49f1-869d-d5735304dd16', N'Manage Report')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'ca4d29d9-f5fc-4c27-80a6-93827113b9d2', N'Manage Room')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'd9a47484-5446-4067-9e0c-f79af93d667e', N'Manage Process')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'dcb02a81-8e54-41f9-88e7-3b5d41a58ece', N'Manage Utility')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'e0c0e7a7-3fe5-4a88-b548-28ecefd7e1d2', N'Manae Room Type')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'e143538d-b5b9-4ab6-81a2-3b04bd8c2b0f', N'Manage Room Item')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'e77533ff-52bf-4c51-b823-5381b01970ab', N'Manage Invoice')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'ebac689e-b406-4439-931c-8432c5231879', N'Manage Booking')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'009acf55-d2ca-4ee0-88a0-dc66501bd4ae', N'528ea9c2-fa3c-4468-9e43-0bb108c2c431')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'009acf55-d2ca-4ee0-88a0-dc66501bd4ae', N'7e1546c8-e864-4051-b10d-5ece01b5ef3e')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'009acf55-d2ca-4ee0-88a0-dc66501bd4ae', N'e77533ff-52bf-4c51-b823-5381b01970ab')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'009acf55-d2ca-4ee0-88a0-dc66501bd4ae', N'ebac689e-b406-4439-931c-8432c5231879')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'47985478-17fe-4c0f-abe0-3b6ced8e095b', N'19f3e97c-b7dc-42ed-8044-71602d39491f')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'47985478-17fe-4c0f-abe0-3b6ced8e095b', N'61146f74-7cc2-467d-8fac-461ea39d7e3c')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', N'00a0d87d-2f99-43e6-b195-3eb3307d0d9c')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', N'19f3e97c-b7dc-42ed-8044-71602d39491f')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', N'3407386e-4efb-4fa2-8892-d7b23a43daba')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', N'4eac3236-3fe6-4ab9-836d-25bb9abe5516')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', N'528ea9c2-fa3c-4468-9e43-0bb108c2c431')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', N'61146f74-7cc2-467d-8fac-461ea39d7e3c')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', N'656a6e99-2820-4fff-9905-4284c19d51b3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', N'68602f61-1321-4d90-bcfd-b9b49ade5f34')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', N'7e1546c8-e864-4051-b10d-5ece01b5ef3e')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', N'899434a7-f4ca-4755-aa98-472fbba9c1a8')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', N'9c210ebc-f3e1-4048-a70f-8823c388d0e2')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', N'9ca2ae26-96d6-45cc-8617-054eb0d81e9f')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', N'9d26947c-09f0-43b0-9f4f-2cc669024fac')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', N'b38a8966-303e-45c9-911e-fd56f401d859')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', N'b6a888de-87f5-4f93-93cb-b48ae07d5cb4')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', N'c668013f-aa4b-49f1-869d-d5735304dd16')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', N'ca4d29d9-f5fc-4c27-80a6-93827113b9d2')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', N'd9a47484-5446-4067-9e0c-f79af93d667e')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', N'dcb02a81-8e54-41f9-88e7-3b5d41a58ece')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', N'e0c0e7a7-3fe5-4a88-b548-28ecefd7e1d2')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', N'e143538d-b5b9-4ab6-81a2-3b04bd8c2b0f')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', N'e77533ff-52bf-4c51-b823-5381b01970ab')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', N'ebac689e-b406-4439-931c-8432c5231879')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'bfe13258-9e89-4394-b741-c66543dbdfee', N'00a0d87d-2f99-43e6-b195-3eb3307d0d9c')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c9bcc2dc-264f-47b1-9e0c-f4eea1e0f445', N'00a0d87d-2f99-43e6-b195-3eb3307d0d9c')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c9bcc2dc-264f-47b1-9e0c-f4eea1e0f445', N'19f3e97c-b7dc-42ed-8044-71602d39491f')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c9bcc2dc-264f-47b1-9e0c-f4eea1e0f445', N'3407386e-4efb-4fa2-8892-d7b23a43daba')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c9bcc2dc-264f-47b1-9e0c-f4eea1e0f445', N'4eac3236-3fe6-4ab9-836d-25bb9abe5516')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c9bcc2dc-264f-47b1-9e0c-f4eea1e0f445', N'528ea9c2-fa3c-4468-9e43-0bb108c2c431')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c9bcc2dc-264f-47b1-9e0c-f4eea1e0f445', N'61146f74-7cc2-467d-8fac-461ea39d7e3c')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c9bcc2dc-264f-47b1-9e0c-f4eea1e0f445', N'656a6e99-2820-4fff-9905-4284c19d51b3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c9bcc2dc-264f-47b1-9e0c-f4eea1e0f445', N'68602f61-1321-4d90-bcfd-b9b49ade5f34')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c9bcc2dc-264f-47b1-9e0c-f4eea1e0f445', N'7e1546c8-e864-4051-b10d-5ece01b5ef3e')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c9bcc2dc-264f-47b1-9e0c-f4eea1e0f445', N'899434a7-f4ca-4755-aa98-472fbba9c1a8')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c9bcc2dc-264f-47b1-9e0c-f4eea1e0f445', N'9c210ebc-f3e1-4048-a70f-8823c388d0e2')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c9bcc2dc-264f-47b1-9e0c-f4eea1e0f445', N'9ca2ae26-96d6-45cc-8617-054eb0d81e9f')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c9bcc2dc-264f-47b1-9e0c-f4eea1e0f445', N'9d26947c-09f0-43b0-9f4f-2cc669024fac')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c9bcc2dc-264f-47b1-9e0c-f4eea1e0f445', N'b38a8966-303e-45c9-911e-fd56f401d859')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c9bcc2dc-264f-47b1-9e0c-f4eea1e0f445', N'b6a888de-87f5-4f93-93cb-b48ae07d5cb4')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c9bcc2dc-264f-47b1-9e0c-f4eea1e0f445', N'c668013f-aa4b-49f1-869d-d5735304dd16')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c9bcc2dc-264f-47b1-9e0c-f4eea1e0f445', N'ca4d29d9-f5fc-4c27-80a6-93827113b9d2')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c9bcc2dc-264f-47b1-9e0c-f4eea1e0f445', N'd9a47484-5446-4067-9e0c-f79af93d667e')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c9bcc2dc-264f-47b1-9e0c-f4eea1e0f445', N'dcb02a81-8e54-41f9-88e7-3b5d41a58ece')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c9bcc2dc-264f-47b1-9e0c-f4eea1e0f445', N'e0c0e7a7-3fe5-4a88-b548-28ecefd7e1d2')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c9bcc2dc-264f-47b1-9e0c-f4eea1e0f445', N'e143538d-b5b9-4ab6-81a2-3b04bd8c2b0f')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c9bcc2dc-264f-47b1-9e0c-f4eea1e0f445', N'e77533ff-52bf-4c51-b823-5381b01970ab')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c9bcc2dc-264f-47b1-9e0c-f4eea1e0f445', N'ebac689e-b406-4439-931c-8432c5231879')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd66d0b09-5b08-4d77-9247-cc8dae136b00', N'00a0d87d-2f99-43e6-b195-3eb3307d0d9c')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd66d0b09-5b08-4d77-9247-cc8dae136b00', N'19f3e97c-b7dc-42ed-8044-71602d39491f')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd66d0b09-5b08-4d77-9247-cc8dae136b00', N'3407386e-4efb-4fa2-8892-d7b23a43daba')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd66d0b09-5b08-4d77-9247-cc8dae136b00', N'528ea9c2-fa3c-4468-9e43-0bb108c2c431')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd66d0b09-5b08-4d77-9247-cc8dae136b00', N'61146f74-7cc2-467d-8fac-461ea39d7e3c')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd66d0b09-5b08-4d77-9247-cc8dae136b00', N'656a6e99-2820-4fff-9905-4284c19d51b3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd66d0b09-5b08-4d77-9247-cc8dae136b00', N'68602f61-1321-4d90-bcfd-b9b49ade5f34')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd66d0b09-5b08-4d77-9247-cc8dae136b00', N'7e1546c8-e864-4051-b10d-5ece01b5ef3e')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd66d0b09-5b08-4d77-9247-cc8dae136b00', N'899434a7-f4ca-4755-aa98-472fbba9c1a8')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd66d0b09-5b08-4d77-9247-cc8dae136b00', N'9c210ebc-f3e1-4048-a70f-8823c388d0e2')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd66d0b09-5b08-4d77-9247-cc8dae136b00', N'9ca2ae26-96d6-45cc-8617-054eb0d81e9f')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd66d0b09-5b08-4d77-9247-cc8dae136b00', N'9d26947c-09f0-43b0-9f4f-2cc669024fac')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd66d0b09-5b08-4d77-9247-cc8dae136b00', N'b38a8966-303e-45c9-911e-fd56f401d859')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd66d0b09-5b08-4d77-9247-cc8dae136b00', N'b6a888de-87f5-4f93-93cb-b48ae07d5cb4')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd66d0b09-5b08-4d77-9247-cc8dae136b00', N'c668013f-aa4b-49f1-869d-d5735304dd16')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd66d0b09-5b08-4d77-9247-cc8dae136b00', N'ca4d29d9-f5fc-4c27-80a6-93827113b9d2')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd66d0b09-5b08-4d77-9247-cc8dae136b00', N'd9a47484-5446-4067-9e0c-f79af93d667e')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd66d0b09-5b08-4d77-9247-cc8dae136b00', N'dcb02a81-8e54-41f9-88e7-3b5d41a58ece')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd66d0b09-5b08-4d77-9247-cc8dae136b00', N'e0c0e7a7-3fe5-4a88-b548-28ecefd7e1d2')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd66d0b09-5b08-4d77-9247-cc8dae136b00', N'e143538d-b5b9-4ab6-81a2-3b04bd8c2b0f')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd66d0b09-5b08-4d77-9247-cc8dae136b00', N'e77533ff-52bf-4c51-b823-5381b01970ab')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd66d0b09-5b08-4d77-9247-cc8dae136b00', N'ebac689e-b406-4439-931c-8432c5231879')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'e2639d84-b598-47b6-94b1-a3bd625f64f2', N'ca4d29d9-f5fc-4c27-80a6-93827113b9d2')
INSERT [dbo].[AspNetUsers] ([Id], [BrandId], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Sex], [IsDeleted], [FullName]) VALUES (N'009acf55-d2ca-4ee0-88a0-dc66501bd4ae', 1, N'phandorn123@gmail.com', 0, N'ADM+VVR4Ti/lw6q7XzZB2PWvp99Y5lKsv553e1HGCAMan3lVk3SSbFDIsI1YW4JPuw==', N'0d80c12d-832f-49fd-8bcc-64be04f9966d', NULL, 0, 0, NULL, 1, 0, N'phandorn123@gmail.com', N'Male', 0, N'Dom')
INSERT [dbo].[AspNetUsers] ([Id], [BrandId], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Sex], [IsDeleted], [FullName]) VALUES (N'47985478-17fe-4c0f-abe0-3b6ced8e095b', 1, N'kunsina@gmail.com', 0, N'AIIuaI2nBj4IRayin8pecVVuy8ekWNnsqi7aDOKGcuz/bGpMDFlVBTkN01YeUTOoHw==', N'5ab8bd0f-fd2e-4317-9ccb-a879d8b940fa', NULL, 0, 0, NULL, 1, 0, N'kunsina@gmail.com', N'Male', 0, N'Kun Sina')
INSERT [dbo].[AspNetUsers] ([Id], [BrandId], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Sex], [IsDeleted], [FullName]) VALUES (N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', 1, N'admin98@gmail.com', 0, N'ABJoa6MDPz1YUElhElAE7ARavmuGSu408jtfGRVMf4kodsCdnYiieLMDRivHZSL6MA==', N'909625bc-3361-4944-a384-dc9ffc53ac0b', NULL, 0, 0, NULL, 1, 0, N'admin98@gmail.com', N'Male', 0, N'Admin')
INSERT [dbo].[AspNetUsers] ([Id], [BrandId], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Sex], [IsDeleted], [FullName]) VALUES (N'bfe13258-9e89-4394-b741-c66543dbdfee', 0, N'admin@gmail.com', 0, N'AKOrPcPIMCLboQky45LqAAhPxKNO2kHWw7DXng0dfBtJ4mAxP1fWKwFEk/4vPBhsNg==', N'c3bfe344-8f23-42b8-977e-640af44a3e2f', NULL, 0, 0, NULL, 0, 0, N'Admin', NULL, 0, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [BrandId], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Sex], [IsDeleted], [FullName]) VALUES (N'c9bcc2dc-264f-47b1-9e0c-f4eea1e0f445', 1, N'bunchhein2@gmail.com', 0, N'AD/VlIuuj4g7iOHRYtQm2TPUTyx8rtIjt6Ic7vg9xtTyM4vg00M59NUT4ZdiywoMJg==', N'4a9948cd-145a-47e0-86e2-fad9e2ac4509', NULL, 0, 0, NULL, 1, 0, N'bunchhein2@gmail.com', N'Male', 0, N'Bunchhein Chheom')
INSERT [dbo].[AspNetUsers] ([Id], [BrandId], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Sex], [IsDeleted], [FullName]) VALUES (N'd66d0b09-5b08-4d77-9247-cc8dae136b00', 1, N'phandy010@gmail.com', 0, N'AIc6q/uaq3o7e9AYwFyUycJDXt70mfcAsZ3yUIzIBzDFxGoDzkUEg5dm/EJsdoFAZQ==', N'c2cb8182-717f-4060-9793-37ff2bd79771', NULL, 0, 0, NULL, 1, 0, N'phandy010@gmail.com', N'Male', 0, N'Phan Dy')
INSERT [dbo].[AspNetUsers] ([Id], [BrandId], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Sex], [IsDeleted], [FullName]) VALUES (N'e2639d84-b598-47b6-94b1-a3bd625f64f2', 1, N'deniidy063@gmail.com', 0, N'AAbpC2B5qTqyn+SrO47PO1llclQp4PGaLTJt+e+hxFJFr+UmEAS8FMMyREvg/CnKpg==', N'dc431d09-abc3-4afd-8a0e-061ff18d4ca2', NULL, 0, 0, NULL, 1, 0, N'deniidy063@gmail.com', N'Male', 0, N'On Sidet')
SET IDENTITY_INSERT [dbo].[booking_tbl] ON 

INSERT [dbo].[booking_tbl] ([id], [bookingdate], [userid], [guestid], [roomid], [exchangeid], [total], [paydollar], [payriel], [updateby], [updatedate], [checkindate], [expiredate], [note], [status]) VALUES (1, CAST(0xFC440B00 AS Date), N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', 1, 2, 6, CAST(100.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', CAST(0xFC440B00 AS Date), CAST(0xFD440B00 AS Date), CAST(0xFE440B00 AS Date), N'', N'Active')
INSERT [dbo].[booking_tbl] ([id], [bookingdate], [userid], [guestid], [roomid], [exchangeid], [total], [paydollar], [payriel], [updateby], [updatedate], [checkindate], [expiredate], [note], [status]) VALUES (2, CAST(0xFC440B00 AS Date), N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', 2, 1, 6, CAST(100.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', CAST(0xFC440B00 AS Date), CAST(0xFD440B00 AS Date), CAST(0xFE440B00 AS Date), N'', N'Active')
INSERT [dbo].[booking_tbl] ([id], [bookingdate], [userid], [guestid], [roomid], [exchangeid], [total], [paydollar], [payriel], [updateby], [updatedate], [checkindate], [expiredate], [note], [status]) VALUES (3, CAST(0xFC440B00 AS Date), N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', 3, 1, 6, CAST(100.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', CAST(0xFC440B00 AS Date), CAST(0xFC440B00 AS Date), CAST(0xFC440B00 AS Date), N'', N'Expire')
INSERT [dbo].[booking_tbl] ([id], [bookingdate], [userid], [guestid], [roomid], [exchangeid], [total], [paydollar], [payriel], [updateby], [updatedate], [checkindate], [expiredate], [note], [status]) VALUES (4, CAST(0xFC440B00 AS Date), N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', 5, 3, 6, CAST(100.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', CAST(0xFC440B00 AS Date), CAST(0xFC440B00 AS Date), CAST(0xFC440B00 AS Date), N'', N'Expire')
INSERT [dbo].[booking_tbl] ([id], [bookingdate], [userid], [guestid], [roomid], [exchangeid], [total], [paydollar], [payriel], [updateby], [updatedate], [checkindate], [expiredate], [note], [status]) VALUES (5, CAST(0xFC440B00 AS Date), N'c9bcc2dc-264f-47b1-9e0c-f4eea1e0f445', 6, 1, 6, CAST(100.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'c9bcc2dc-264f-47b1-9e0c-f4eea1e0f445', CAST(0x16450B00 AS Date), CAST(0xFC440B00 AS Date), CAST(0xFC440B00 AS Date), N'', N'Active')
INSERT [dbo].[booking_tbl] ([id], [bookingdate], [userid], [guestid], [roomid], [exchangeid], [total], [paydollar], [payriel], [updateby], [updatedate], [checkindate], [expiredate], [note], [status]) VALUES (6, CAST(0xFC440B00 AS Date), N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', 7, 6, 6, CAST(100.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', CAST(0xFC440B00 AS Date), CAST(0xFC440B00 AS Date), CAST(0xFC440B00 AS Date), N'', N'Expire')
INSERT [dbo].[booking_tbl] ([id], [bookingdate], [userid], [guestid], [roomid], [exchangeid], [total], [paydollar], [payriel], [updateby], [updatedate], [checkindate], [expiredate], [note], [status]) VALUES (7, CAST(0xFC440B00 AS Date), N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', 8, 3, 6, CAST(100.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', CAST(0xFC440B00 AS Date), CAST(0xFD440B00 AS Date), CAST(0xFE440B00 AS Date), N'', N'Active')
INSERT [dbo].[booking_tbl] ([id], [bookingdate], [userid], [guestid], [roomid], [exchangeid], [total], [paydollar], [payriel], [updateby], [updatedate], [checkindate], [expiredate], [note], [status]) VALUES (8, CAST(0xFF440B00 AS Date), N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', 9, 6, 6, CAST(10.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', CAST(0xFF440B00 AS Date), CAST(0xFF440B00 AS Date), CAST(0x02450B00 AS Date), N'', N'Active')
INSERT [dbo].[booking_tbl] ([id], [bookingdate], [userid], [guestid], [roomid], [exchangeid], [total], [paydollar], [payriel], [updateby], [updatedate], [checkindate], [expiredate], [note], [status]) VALUES (9, CAST(0x08450B00 AS Date), N'd66d0b09-5b08-4d77-9247-cc8dae136b00', 10, 1, 6, CAST(10.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'd66d0b09-5b08-4d77-9247-cc8dae136b00', CAST(0x08450B00 AS Date), CAST(0x09450B00 AS Date), CAST(0x0A450B00 AS Date), N'', N'Expire')
SET IDENTITY_INSERT [dbo].[booking_tbl] OFF
SET IDENTITY_INSERT [dbo].[Branches] ON 

INSERT [dbo].[Branches] ([Id], [Name], [create_date], [create_by]) VALUES (1, N'Sihanouk Vile', CAST(0x0000AB3500000000 AS DateTime), N'sovan')
INSERT [dbo].[Branches] ([Id], [Name], [create_date], [create_by]) VALUES (2, N'Phnom Penhs', CAST(0x0000AC8700000000 AS DateTime), N'MISVAN')
INSERT [dbo].[Branches] ([Id], [Name], [create_date], [create_by]) VALUES (3, N'Takeo', CAST(0x0000AC8C00000000 AS DateTime), N'so_savann@yahoo.com')
SET IDENTITY_INSERT [dbo].[Branches] OFF
SET IDENTITY_INSERT [dbo].[building_tbl] ON 

INSERT [dbo].[building_tbl] ([id], [buildingname], [buildingnamekh]) VALUES (1, N'A', N'អគារA')
INSERT [dbo].[building_tbl] ([id], [buildingname], [buildingnamekh]) VALUES (3, N'B', N'អគារ B')
INSERT [dbo].[building_tbl] ([id], [buildingname], [buildingnamekh]) VALUES (4, N'AA', N'អគារA')
SET IDENTITY_INSERT [dbo].[building_tbl] OFF
SET IDENTITY_INSERT [dbo].[checkin_tbl] ON 

INSERT [dbo].[checkin_tbl] ([id], [checkindate], [roomid], [userid], [guestid], [child], [man], [women], [startdate], [enddate], [payforroom], [prepaid], [paydollar], [payriel], [active]) VALUES (1, CAST(0xFC440B00 AS Date), 2, N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', 1, 0, 1, 1, CAST(0xFC440B00 AS Date), CAST(0xFC440B00 AS Date), CAST(100.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[checkin_tbl] ([id], [checkindate], [roomid], [userid], [guestid], [child], [man], [women], [startdate], [enddate], [payforroom], [prepaid], [paydollar], [payriel], [active]) VALUES (2, CAST(0xFC440B00 AS Date), 1, N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', 2, 0, 1, 1, CAST(0xFC440B00 AS Date), CAST(0xFC440B00 AS Date), CAST(100.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[checkin_tbl] ([id], [checkindate], [roomid], [userid], [guestid], [child], [man], [women], [startdate], [enddate], [payforroom], [prepaid], [paydollar], [payriel], [active]) VALUES (3, CAST(0xFC440B00 AS Date), 1, N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', 3, 0, 1, 0, CAST(0xFC440B00 AS Date), CAST(0xFC440B00 AS Date), CAST(100.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[checkin_tbl] ([id], [checkindate], [roomid], [userid], [guestid], [child], [man], [women], [startdate], [enddate], [payforroom], [prepaid], [paydollar], [payriel], [active]) VALUES (4, CAST(0xFC440B00 AS Date), 1, N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', 6, 0, 1, 0, CAST(0xFC440B00 AS Date), CAST(0xFC440B00 AS Date), CAST(100.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[checkin_tbl] ([id], [checkindate], [roomid], [userid], [guestid], [child], [man], [women], [startdate], [enddate], [payforroom], [prepaid], [paydollar], [payriel], [active]) VALUES (5, CAST(0xFC440B00 AS Date), 3, N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', 8, 0, 1, 0, CAST(0xFC440B00 AS Date), CAST(0xFC440B00 AS Date), CAST(100.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[checkin_tbl] ([id], [checkindate], [roomid], [userid], [guestid], [child], [man], [women], [startdate], [enddate], [payforroom], [prepaid], [paydollar], [payriel], [active]) VALUES (6, CAST(0xFF440B00 AS Date), 6, N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', 9, 0, 1, 0, CAST(0xFF440B00 AS Date), CAST(0xFF440B00 AS Date), CAST(100.00 AS Decimal(18, 2)), CAST(50.00 AS Decimal(18, 2)), CAST(150.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0)
SET IDENTITY_INSERT [dbo].[checkin_tbl] OFF
SET IDENTITY_INSERT [dbo].[checkout_tbl] ON 

INSERT [dbo].[checkout_tbl] ([id], [date], [guestid], [roomid], [weusageid], [exchangeid], [userid], [totalroomprice], [paybefor], [total], [returnamount], [totalpayment], [paydollar], [payriel], [description]) VALUES (2, CAST(0xFC440B00 AS Date), 2, 2, 6, 6, N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', CAST(0.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(102.86 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(2.86 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'bbu')
INSERT [dbo].[checkout_tbl] ([id], [date], [guestid], [roomid], [weusageid], [exchangeid], [userid], [totalroomprice], [paybefor], [total], [returnamount], [totalpayment], [paydollar], [payriel], [description]) VALUES (3, CAST(0x00450B00 AS Date), 3, 6, 10, 6, N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', CAST(3.33 AS Decimal(18, 2)), CAST(150.00 AS Decimal(18, 2)), CAST(13.10 AS Decimal(18, 2)), CAST(136.90 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'123')
INSERT [dbo].[checkout_tbl] ([id], [date], [guestid], [roomid], [weusageid], [exchangeid], [userid], [totalroomprice], [paybefor], [total], [returnamount], [totalpayment], [paydollar], [payriel], [description]) VALUES (4, CAST(0x4D450B00 AS Date), 4, 3, 13, 6, N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', CAST(70.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(118.67 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(18.67 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'bbu')
INSERT [dbo].[checkout_tbl] ([id], [date], [guestid], [roomid], [weusageid], [exchangeid], [userid], [totalroomprice], [paybefor], [total], [returnamount], [totalpayment], [paydollar], [payriel], [description]) VALUES (5, CAST(0x67450B00 AS Date), 5, 1, 15, 6, N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', CAST(60.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(110.15 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(10.15 AS Decimal(18, 2)), CAST(10.15 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'note12345')
SET IDENTITY_INSERT [dbo].[checkout_tbl] OFF
SET IDENTITY_INSERT [dbo].[checkoutdetail_tbl] ON 

INSERT [dbo].[checkoutdetail_tbl] ([id], [checkoutid], [paydemageid]) VALUES (1, 2, 4)
INSERT [dbo].[checkoutdetail_tbl] ([id], [checkoutid], [paydemageid]) VALUES (2, 3, 7)
INSERT [dbo].[checkoutdetail_tbl] ([id], [checkoutid], [paydemageid]) VALUES (3, 1, 5)
INSERT [dbo].[checkoutdetail_tbl] ([id], [checkoutid], [paydemageid]) VALUES (4, 1, 6)
INSERT [dbo].[checkoutdetail_tbl] ([id], [checkoutid], [paydemageid]) VALUES (5, 1, 3)
SET IDENTITY_INSERT [dbo].[checkoutdetail_tbl] OFF
SET IDENTITY_INSERT [dbo].[ExchangeRates] ON 

INSERT [dbo].[ExchangeRates] ([id], [date], [rate], [status], [IsDeleted]) VALUES (1, CAST(0x5D420B00 AS Date), CAST(4000.000 AS Decimal(18, 3)), 1, 1)
INSERT [dbo].[ExchangeRates] ([id], [date], [rate], [status], [IsDeleted]) VALUES (2, CAST(0x5D420B00 AS Date), CAST(4100.000 AS Decimal(18, 3)), 1, 0)
INSERT [dbo].[ExchangeRates] ([id], [date], [rate], [status], [IsDeleted]) VALUES (3, CAST(0x6A420B00 AS Date), CAST(4000.000 AS Decimal(18, 3)), 1, 1)
INSERT [dbo].[ExchangeRates] ([id], [date], [rate], [status], [IsDeleted]) VALUES (4, CAST(0xE8420B00 AS Date), CAST(4150.000 AS Decimal(18, 3)), 1, 0)
INSERT [dbo].[ExchangeRates] ([id], [date], [rate], [status], [IsDeleted]) VALUES (5, CAST(0xEF420B00 AS Date), CAST(4050.000 AS Decimal(18, 3)), 1, 1)
INSERT [dbo].[ExchangeRates] ([id], [date], [rate], [status], [IsDeleted]) VALUES (6, CAST(0xFC420B00 AS Date), CAST(4050.000 AS Decimal(18, 3)), 1, 0)
SET IDENTITY_INSERT [dbo].[ExchangeRates] OFF
SET IDENTITY_INSERT [dbo].[expensetype_tbl] ON 

INSERT [dbo].[expensetype_tbl] ([id], [typename]) VALUES (1, N'Water Expense')
INSERT [dbo].[expensetype_tbl] ([id], [typename]) VALUES (2, N'Electric Expense')
INSERT [dbo].[expensetype_tbl] ([id], [typename]) VALUES (3, N'Equipment Expense')
INSERT [dbo].[expensetype_tbl] ([id], [typename]) VALUES (4, N'Salary Expend')
INSERT [dbo].[expensetype_tbl] ([id], [typename]) VALUES (5, N'Return To Guest')
SET IDENTITY_INSERT [dbo].[expensetype_tbl] OFF
SET IDENTITY_INSERT [dbo].[floor_tbl] ON 

INSERT [dbo].[floor_tbl] ([id], [buildingid], [floor_no], [status]) VALUES (1, 1, N'G', NULL)
INSERT [dbo].[floor_tbl] ([id], [buildingid], [floor_no], [status]) VALUES (2, 1, N'F1', NULL)
INSERT [dbo].[floor_tbl] ([id], [buildingid], [floor_no], [status]) VALUES (3, 1, N'F2', NULL)
INSERT [dbo].[floor_tbl] ([id], [buildingid], [floor_no], [status]) VALUES (4, 1, N'F3', NULL)
INSERT [dbo].[floor_tbl] ([id], [buildingid], [floor_no], [status]) VALUES (5, 1, N'F4', NULL)
INSERT [dbo].[floor_tbl] ([id], [buildingid], [floor_no], [status]) VALUES (6, 1, N'F5', NULL)
SET IDENTITY_INSERT [dbo].[floor_tbl] OFF
SET IDENTITY_INSERT [dbo].[guest_tbl] ON 

INSERT [dbo].[guest_tbl] ([id], [name], [namekh], [sex], [dob], [address], [nationality], [phone], [email], [ssn], [passport], [status]) VALUES (1, N'Phan Dy', N'ផាន ឌី', N'Male', CAST(0xFC440B00 AS Date), N'Takeo', N'Khmer', N'0972393063', N'phandy010@gmail.com', N'12345678', N'', N'CHECK-IN')
INSERT [dbo].[guest_tbl] ([id], [name], [namekh], [sex], [dob], [address], [nationality], [phone], [email], [ssn], [passport], [status]) VALUES (2, N'Sok Bunthourn', N'សុខ ប៊ុនធឿន', N'Male', CAST(0xFC440B00 AS Date), N'PP', N'Khmer', N'092332424', N'ph', N'1234567', N'', N'CHECK-OUT')
INSERT [dbo].[guest_tbl] ([id], [name], [namekh], [sex], [dob], [address], [nationality], [phone], [email], [ssn], [passport], [status]) VALUES (3, N'Chantha', N'ចន្ថា', N'Male', CAST(0xFC440B00 AS Date), N'PP', N'Khmer', N'1234566', N'p', N'1234556', N'', N'CHECK-OUT')
INSERT [dbo].[guest_tbl] ([id], [name], [namekh], [sex], [dob], [address], [nationality], [phone], [email], [ssn], [passport], [status]) VALUES (4, N'Bunna', N'ប៊ុណ្ណា', N'Male', CAST(0xFC440B00 AS Date), N'PP', N'Khmer', N'091234132', N'ph', N'12233453', N'', N'BOOK')
INSERT [dbo].[guest_tbl] ([id], [name], [namekh], [sex], [dob], [address], [nationality], [phone], [email], [ssn], [passport], [status]) VALUES (5, N'Bunna', N'ប៊ុណ្ណា', N'Male', CAST(0xFC440B00 AS Date), N'PP', N'Khmer', N'091234132', N'phandy010@gmail.com', N'12233453', N'', N'BOOK')
INSERT [dbo].[guest_tbl] ([id], [name], [namekh], [sex], [dob], [address], [nationality], [phone], [email], [ssn], [passport], [status]) VALUES (6, N'Bora', N'បូរ៉ា', N'Male', CAST(0xFC440B00 AS Date), N'PP', N'Khmer', N'0972324524', N'phandy010@gmail.com', N'253535353', N'', N'CHECK-OUT')
INSERT [dbo].[guest_tbl] ([id], [name], [namekh], [sex], [dob], [address], [nationality], [phone], [email], [ssn], [passport], [status]) VALUES (7, N'Na Na', N'ណាណា', N'Male', CAST(0xFC440B00 AS Date), N'', N'Khmer', N'', N'na', N'1111132', N'', N'BOOK')
INSERT [dbo].[guest_tbl] ([id], [name], [namekh], [sex], [dob], [address], [nationality], [phone], [email], [ssn], [passport], [status]) VALUES (8, N'Davin', N'ដាវីន', N'Male', CAST(0xFC440B00 AS Date), N'', N'Khmer', N'', N'', N'', N'', N'CHECK-OUT')
INSERT [dbo].[guest_tbl] ([id], [name], [namekh], [sex], [dob], [address], [nationality], [phone], [email], [ssn], [passport], [status]) VALUES (9, N'testq', N'test', N'Male', CAST(0xFF440B00 AS Date), N'', N'Khmer', N'', N'', N'', N'', N'CHECK-OUT')
INSERT [dbo].[guest_tbl] ([id], [name], [namekh], [sex], [dob], [address], [nationality], [phone], [email], [ssn], [passport], [status]) VALUES (10, N'Phan Dy', N'ផាន ឌី', N'Male', CAST(0x08450B00 AS Date), N'Takeo', N'Khmer', N'0972393063', N'phandy010@gmail.com', N'', N'', N'BOOK')
SET IDENTITY_INSERT [dbo].[guest_tbl] OFF
SET IDENTITY_INSERT [dbo].[invoice_tbl] ON 

INSERT [dbo].[invoice_tbl] ([id], [invoicedate], [roomid], [weusageid], [guestid], [userid], [exchangerateid], [grandtotal], [totalriel], [totaldollar], [totalother], [payriel], [paydollar], [createby], [createdate], [updateby], [updatedate], [paid], [printed], [note], [status]) VALUES (1, CAST(0x18450B00 AS Date), 1, 5, 6, N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', 6, CAST(124.00 AS Decimal(18, 2)), CAST(500621.00 AS Decimal(18, 2)), CAST(124.00 AS Decimal(18, 2)), CAST(2.00 AS Decimal(18, 2)), CAST(500621.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', CAST(0x18450B00 AS Date), N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', CAST(0x18450B00 AS Date), 1, 1, N'', N'ACTIVE')
INSERT [dbo].[invoice_tbl] ([id], [invoicedate], [roomid], [weusageid], [guestid], [userid], [exchangerateid], [grandtotal], [totalriel], [totaldollar], [totalother], [payriel], [paydollar], [createby], [createdate], [updateby], [updatedate], [paid], [printed], [note], [status]) VALUES (2, CAST(0x03450B00 AS Date), 1, 8, 6, N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', 6, CAST(112.00 AS Decimal(18, 2)), CAST(451575.00 AS Decimal(18, 2)), CAST(112.00 AS Decimal(18, 2)), CAST(2.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', CAST(0x18450B00 AS Date), N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', CAST(0x03450B00 AS Date), 0, 1, N'NOte', N'ACTIVE')
INSERT [dbo].[invoice_tbl] ([id], [invoicedate], [roomid], [weusageid], [guestid], [userid], [exchangerateid], [grandtotal], [totalriel], [totaldollar], [totalother], [payriel], [paydollar], [createby], [createdate], [updateby], [updatedate], [paid], [printed], [note], [status]) VALUES (3, CAST(0x18450B00 AS Date), 3, 7, 8, N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', 6, CAST(104.00 AS Decimal(18, 2)), CAST(420107.00 AS Decimal(18, 2)), CAST(104.00 AS Decimal(18, 2)), CAST(2.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(104.00 AS Decimal(18, 2)), N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', CAST(0x18450B00 AS Date), N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', CAST(0x18450B00 AS Date), 1, 1, N'', N'ACTIVE')
INSERT [dbo].[invoice_tbl] ([id], [invoicedate], [roomid], [weusageid], [guestid], [userid], [exchangerateid], [grandtotal], [totalriel], [totaldollar], [totalother], [payriel], [paydollar], [createby], [createdate], [updateby], [updatedate], [paid], [printed], [note], [status]) VALUES (4, CAST(0x37450B00 AS Date), 1, 11, 6, N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', 6, CAST(112.00 AS Decimal(18, 2)), CAST(451575.00 AS Decimal(18, 2)), CAST(112.00 AS Decimal(18, 2)), CAST(2.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(112.00 AS Decimal(18, 2)), N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', CAST(0x37450B00 AS Date), N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', CAST(0x37450B00 AS Date), 1, 1, N'', N'ACTIVE')
INSERT [dbo].[invoice_tbl] ([id], [invoicedate], [roomid], [weusageid], [guestid], [userid], [exchangerateid], [grandtotal], [totalriel], [totaldollar], [totalother], [payriel], [paydollar], [createby], [createdate], [updateby], [updatedate], [paid], [printed], [note], [status]) VALUES (5, CAST(0x37450B00 AS Date), 3, 12, 8, N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', 6, CAST(137.00 AS Decimal(18, 2)), CAST(556632.00 AS Decimal(18, 2)), CAST(137.00 AS Decimal(18, 2)), CAST(2.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(137.00 AS Decimal(18, 2)), N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', CAST(0x37450B00 AS Date), N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', CAST(0x37450B00 AS Date), 1, 1, N'', N'ACTIVE')
INSERT [dbo].[invoice_tbl] ([id], [invoicedate], [roomid], [weusageid], [guestid], [userid], [exchangerateid], [grandtotal], [totalriel], [totaldollar], [totalother], [payriel], [paydollar], [createby], [createdate], [updateby], [updatedate], [paid], [printed], [note], [status]) VALUES (6, CAST(0x19450B00 AS Date), 2, 6, 1, N'd66d0b09-5b08-4d77-9247-cc8dae136b00', 6, CAST(141.00 AS Decimal(18, 2)), CAST(570605.00 AS Decimal(18, 2)), CAST(141.00 AS Decimal(18, 2)), CAST(2.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(141.00 AS Decimal(18, 2)), N'd66d0b09-5b08-4d77-9247-cc8dae136b00', CAST(0x19450B00 AS Date), N'd66d0b09-5b08-4d77-9247-cc8dae136b00', CAST(0x19450B00 AS Date), 0, 1, N'', N'ACTIVE')
INSERT [dbo].[invoice_tbl] ([id], [invoicedate], [roomid], [weusageid], [guestid], [userid], [exchangerateid], [grandtotal], [totalriel], [totaldollar], [totalother], [payriel], [paydollar], [createby], [createdate], [updateby], [updatedate], [paid], [printed], [note], [status]) VALUES (7, CAST(0x19450B00 AS Date), 2, 16, 1, N'd66d0b09-5b08-4d77-9247-cc8dae136b00', 6, CAST(126.00 AS Decimal(18, 2)), CAST(512123.00 AS Decimal(18, 2)), CAST(126.00 AS Decimal(18, 2)), CAST(2.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(126.00 AS Decimal(18, 2)), N'd66d0b09-5b08-4d77-9247-cc8dae136b00', CAST(0x19450B00 AS Date), N'd66d0b09-5b08-4d77-9247-cc8dae136b00', CAST(0x19450B00 AS Date), 1, 1, N'', N'ACTIVE')
SET IDENTITY_INSERT [dbo].[invoice_tbl] OFF
SET IDENTITY_INSERT [dbo].[invoicedetail_tbl] ON 

INSERT [dbo].[invoicedetail_tbl] ([id], [invoiceid], [paydemageid]) VALUES (1, 12, 10)
INSERT [dbo].[invoicedetail_tbl] ([id], [invoiceid], [paydemageid]) VALUES (2, 12, 11)
SET IDENTITY_INSERT [dbo].[invoicedetail_tbl] OFF
SET IDENTITY_INSERT [dbo].[item_tbl] ON 

INSERT [dbo].[item_tbl] ([id], [itemname], [itemnamekh], [price], [remark], [status]) VALUES (1, N'Air Conditionor', N'ម៉ាស៊ីនត្រជាក់', CAST(200.0000 AS Decimal(16, 4)), N'', N'True')
INSERT [dbo].[item_tbl] ([id], [itemname], [itemnamekh], [price], [remark], [status]) VALUES (2, N'Washing Machine', N'ម៉ាស៊ីនបោកខោអាវ', CAST(150.0000 AS Decimal(16, 4)), N'', N'True')
INSERT [dbo].[item_tbl] ([id], [itemname], [itemnamekh], [price], [remark], [status]) VALUES (3, N'Fridge', N'ទូរទឹកកក', CAST(200.0000 AS Decimal(16, 4)), N'', N'True')
INSERT [dbo].[item_tbl] ([id], [itemname], [itemnamekh], [price], [remark], [status]) VALUES (4, N'Television', N'ទូរទស្ស', CAST(70.0000 AS Decimal(16, 4)), N'', N'True')
INSERT [dbo].[item_tbl] ([id], [itemname], [itemnamekh], [price], [remark], [status]) VALUES (5, N'Fan', N'កង្ហា', CAST(30.0000 AS Decimal(16, 4)), N'', N'True')
SET IDENTITY_INSERT [dbo].[item_tbl] OFF
SET IDENTITY_INSERT [dbo].[LoginHistories] ON 

INSERT [dbo].[LoginHistories] ([Id], [LoggedBy], [LoggedDate], [IPAddress], [HostName]) VALUES (1, N'Cheng Mich', CAST(0x0000AE830100894F AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.4896.127 Safari/537.36')
INSERT [dbo].[LoginHistories] ([Id], [LoggedBy], [LoggedDate], [IPAddress], [HostName]) VALUES (2, N'Cheng Mich', CAST(0x0000AE830100FE36 AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.4896.127 Safari/537.36')
INSERT [dbo].[LoginHistories] ([Id], [LoggedBy], [LoggedDate], [IPAddress], [HostName]) VALUES (3, N'Cheng Mich', CAST(0x0000AE8301029E73 AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.4896.127 Safari/537.36')
INSERT [dbo].[LoginHistories] ([Id], [LoggedBy], [LoggedDate], [IPAddress], [HostName]) VALUES (4, N'Bunchhein Chheom', CAST(0x0000AE830103EC16 AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.4896.127 Safari/537.36')
INSERT [dbo].[LoginHistories] ([Id], [LoggedBy], [LoggedDate], [IPAddress], [HostName]) VALUES (5, N'Cheng Mich', CAST(0x0000AE83010655EB AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.4896.127 Safari/537.36')
INSERT [dbo].[LoginHistories] ([Id], [LoggedBy], [LoggedDate], [IPAddress], [HostName]) VALUES (6, N'Cheng Mich', CAST(0x0000AE83010833C1 AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.4896.127 Safari/537.36')
INSERT [dbo].[LoginHistories] ([Id], [LoggedBy], [LoggedDate], [IPAddress], [HostName]) VALUES (7, N'Cheng Mich', CAST(0x0000AE830108E879 AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.4896.127 Safari/537.36')
INSERT [dbo].[LoginHistories] ([Id], [LoggedBy], [LoggedDate], [IPAddress], [HostName]) VALUES (8, N'Cheng Mich', CAST(0x0000AE8301098849 AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.4896.127 Safari/537.36')
INSERT [dbo].[LoginHistories] ([Id], [LoggedBy], [LoggedDate], [IPAddress], [HostName]) VALUES (9, N'Cheng Mich', CAST(0x0000AE9F00BC8169 AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.67 Safari/537.36')
INSERT [dbo].[LoginHistories] ([Id], [LoggedBy], [LoggedDate], [IPAddress], [HostName]) VALUES (10, N'Cheng Mich', CAST(0x0000AE9F00C02F99 AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.67 Safari/537.36')
INSERT [dbo].[LoginHistories] ([Id], [LoggedBy], [LoggedDate], [IPAddress], [HostName]) VALUES (11, N'Cheng Mich', CAST(0x0000AE9F00E25945 AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.67 Safari/537.36')
INSERT [dbo].[LoginHistories] ([Id], [LoggedBy], [LoggedDate], [IPAddress], [HostName]) VALUES (12, N'Cheng Mich', CAST(0x0000AE9F00E34CA1 AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.67 Safari/537.36')
INSERT [dbo].[LoginHistories] ([Id], [LoggedBy], [LoggedDate], [IPAddress], [HostName]) VALUES (13, N'Cheng Mich', CAST(0x0000AE9F00E4117C AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.67 Safari/537.36')
INSERT [dbo].[LoginHistories] ([Id], [LoggedBy], [LoggedDate], [IPAddress], [HostName]) VALUES (14, N'Cheng Mich', CAST(0x0000AE9F00E49FA5 AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.67 Safari/537.36')
INSERT [dbo].[LoginHistories] ([Id], [LoggedBy], [LoggedDate], [IPAddress], [HostName]) VALUES (15, N'Cheng Mich', CAST(0x0000AE9F0101F660 AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.67 Safari/537.36')
INSERT [dbo].[LoginHistories] ([Id], [LoggedBy], [LoggedDate], [IPAddress], [HostName]) VALUES (16, N'Cheng Mich', CAST(0x0000AE9F01034BD6 AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.67 Safari/537.36')
INSERT [dbo].[LoginHistories] ([Id], [LoggedBy], [LoggedDate], [IPAddress], [HostName]) VALUES (17, N'Cheng Mich', CAST(0x0000AE9F0128C19D AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.67 Safari/537.36')
INSERT [dbo].[LoginHistories] ([Id], [LoggedBy], [LoggedDate], [IPAddress], [HostName]) VALUES (18, N'Cheng Mich', CAST(0x0000AE9F012FEFF4 AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.67 Safari/537.36')
INSERT [dbo].[LoginHistories] ([Id], [LoggedBy], [LoggedDate], [IPAddress], [HostName]) VALUES (19, N'Cheng Mich', CAST(0x0000AE9F013398AD AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.67 Safari/537.36')
INSERT [dbo].[LoginHistories] ([Id], [LoggedBy], [LoggedDate], [IPAddress], [HostName]) VALUES (20, N'Cheng Mich', CAST(0x0000AE9F0139BDEA AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.67 Safari/537.36')
INSERT [dbo].[LoginHistories] ([Id], [LoggedBy], [LoggedDate], [IPAddress], [HostName]) VALUES (21, N'Cheng Mich', CAST(0x0000AEA000AC5437 AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.67 Safari/537.36')
INSERT [dbo].[LoginHistories] ([Id], [LoggedBy], [LoggedDate], [IPAddress], [HostName]) VALUES (22, N'Cheng Mich', CAST(0x0000AEA000AF524C AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.67 Safari/537.36')
INSERT [dbo].[LoginHistories] ([Id], [LoggedBy], [LoggedDate], [IPAddress], [HostName]) VALUES (23, N'Cheng Mich', CAST(0x0000AEA000B11019 AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.67 Safari/537.36')
INSERT [dbo].[LoginHistories] ([Id], [LoggedBy], [LoggedDate], [IPAddress], [HostName]) VALUES (24, N'Cheng Mich', CAST(0x0000AEA000B2103B AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.67 Safari/537.36')
INSERT [dbo].[LoginHistories] ([Id], [LoggedBy], [LoggedDate], [IPAddress], [HostName]) VALUES (25, N'Cheng Mich', CAST(0x0000AEA000B29B12 AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.67 Safari/537.36')
INSERT [dbo].[LoginHistories] ([Id], [LoggedBy], [LoggedDate], [IPAddress], [HostName]) VALUES (26, N'Cheng Mich', CAST(0x0000AEA000B39363 AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.67 Safari/537.36')
INSERT [dbo].[LoginHistories] ([Id], [LoggedBy], [LoggedDate], [IPAddress], [HostName]) VALUES (27, N'Cheng Mich', CAST(0x0000AEA000B4EB7B AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.67 Safari/537.36')
INSERT [dbo].[LoginHistories] ([Id], [LoggedBy], [LoggedDate], [IPAddress], [HostName]) VALUES (28, N'Cheng Mich', CAST(0x0000AEA800A10DD1 AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/102.0.5005.63 Safari/537.36')
SET IDENTITY_INSERT [dbo].[LoginHistories] OFF
SET IDENTITY_INSERT [dbo].[otherexpense_tbl] ON 

INSERT [dbo].[otherexpense_tbl] ([id], [date], [expensetypeid], [amount], [note], [createby], [createdate], [image]) VALUES (1, CAST(0xBB440B00 AS Date), 1, CAST(500.000 AS Decimal(18, 3)), N'សរុបការប្រើប្រាស់ទឹក​សម្រាប់ខែ ១២', N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', CAST(0xDF440B00 AS Date), N'')
INSERT [dbo].[otherexpense_tbl] ([id], [date], [expensetypeid], [amount], [note], [createby], [createdate], [image]) VALUES (2, CAST(0xBB440B00 AS Date), 2, CAST(1000.000 AS Decimal(18, 3)), N'សរុបការប្រើប្រាស់ភ្លើងសម្រាប់ខែ១២', N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', CAST(0xBB440B00 AS Date), N'')
INSERT [dbo].[otherexpense_tbl] ([id], [date], [expensetypeid], [amount], [note], [createby], [createdate], [image]) VALUES (3, CAST(0xBB440B00 AS Date), 3, CAST(20.000 AS Decimal(18, 3)), N'ប្តូរត្រនាប់ចានបន្គន់​នៅបន្ទប់លេខ ០០២', N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', CAST(0xBB440B00 AS Date), N'')
INSERT [dbo].[otherexpense_tbl] ([id], [date], [expensetypeid], [amount], [note], [createby], [createdate], [image]) VALUES (4, CAST(0xCE440B00 AS Date), 4, CAST(250.000 AS Decimal(18, 3)), N'Note', N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', CAST(0xDF440B00 AS Date), N'2023-01-02 16.32.15_2023_01_08_20_06_53.jpg')
INSERT [dbo].[otherexpense_tbl] ([id], [date], [expensetypeid], [amount], [note], [createby], [createdate], [image]) VALUES (5, CAST(0xDF440B00 AS Date), 4, CAST(100.000 AS Decimal(18, 3)), N'', N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', CAST(0xDF440B00 AS Date), N'2023-01-02 16.32.05_2023_01_08_19_54_04.jpg')
INSERT [dbo].[otherexpense_tbl] ([id], [date], [expensetypeid], [amount], [note], [createby], [createdate], [image]) VALUES (6, CAST(0xDF440B00 AS Date), 3, CAST(50.000 AS Decimal(18, 3)), N'', N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', CAST(0xDF440B00 AS Date), N'Screen Shot 2023-01-08 at 8.08.06 PM_2023_01_08_20_28_52.png')
INSERT [dbo].[otherexpense_tbl] ([id], [date], [expensetypeid], [amount], [note], [createby], [createdate], [image]) VALUES (7, CAST(0xDF440B00 AS Date), 1, CAST(50.000 AS Decimal(18, 3)), N'', N'a3ee1a89-0bbe-40c8-8132-712d0b49961a', CAST(0xDF440B00 AS Date), N'2023-01-02 16.32.15_2023_01_08_20_35_26.jpg')
INSERT [dbo].[otherexpense_tbl] ([id], [date], [expensetypeid], [amount], [note], [createby], [createdate], [image]) VALUES (9, CAST(0xE9440B00 AS Date), 2, CAST(400.000 AS Decimal(18, 3)), N'12', N'd66d0b09-5b08-4d77-9247-cc8dae136b00', CAST(0x19450B00 AS Date), NULL)
SET IDENTITY_INSERT [dbo].[otherexpense_tbl] OFF
SET IDENTITY_INSERT [dbo].[paydemage_tbl] ON 

INSERT [dbo].[paydemage_tbl] ([id], [date], [guestid], [itemid], [price], [paid], [note]) VALUES (1, CAST(0xFC440B00 AS Date), 1, 6, CAST(50.0000 AS Decimal(18, 4)), 1, N'')
INSERT [dbo].[paydemage_tbl] ([id], [date], [guestid], [itemid], [price], [paid], [note]) VALUES (2, CAST(0xFC440B00 AS Date), 1, 3, CAST(15.0000 AS Decimal(18, 4)), 0, N'')
INSERT [dbo].[paydemage_tbl] ([id], [date], [guestid], [itemid], [price], [paid], [note]) VALUES (3, CAST(0xE0440B00 AS Date), 2, 4, CAST(50.0000 AS Decimal(18, 4)), 0, N'')
INSERT [dbo].[paydemage_tbl] ([id], [date], [guestid], [itemid], [price], [paid], [note]) VALUES (4, CAST(0xE3440B00 AS Date), 5, 6, CAST(10.0000 AS Decimal(18, 4)), 0, N'')
INSERT [dbo].[paydemage_tbl] ([id], [date], [guestid], [itemid], [price], [paid], [note]) VALUES (5, CAST(0xE3440B00 AS Date), 5, 6, CAST(10.0000 AS Decimal(18, 4)), 0, N'')
INSERT [dbo].[paydemage_tbl] ([id], [date], [guestid], [itemid], [price], [paid], [note]) VALUES (6, CAST(0xE3440B00 AS Date), 5, 6, CAST(12.0000 AS Decimal(18, 4)), 0, N'')
INSERT [dbo].[paydemage_tbl] ([id], [date], [guestid], [itemid], [price], [paid], [note]) VALUES (7, CAST(0xE3440B00 AS Date), 5, 5, CAST(150.0000 AS Decimal(18, 4)), 0, N'')
INSERT [dbo].[paydemage_tbl] ([id], [date], [guestid], [itemid], [price], [paid], [note]) VALUES (8, CAST(0xE3440B00 AS Date), 6, 6, CAST(20.0000 AS Decimal(18, 4)), 0, N'')
INSERT [dbo].[paydemage_tbl] ([id], [date], [guestid], [itemid], [price], [paid], [note]) VALUES (9, CAST(0xE3440B00 AS Date), 6, 3, CAST(10.0000 AS Decimal(18, 4)), 0, N'')
INSERT [dbo].[paydemage_tbl] ([id], [date], [guestid], [itemid], [price], [paid], [note]) VALUES (10, CAST(0xE3440B00 AS Date), 7, 4, CAST(100.0000 AS Decimal(18, 4)), 1, N'')
INSERT [dbo].[paydemage_tbl] ([id], [date], [guestid], [itemid], [price], [paid], [note]) VALUES (11, CAST(0xE3440B00 AS Date), 7, 6, CAST(10.0000 AS Decimal(18, 4)), 1, N'')
SET IDENTITY_INSERT [dbo].[paydemage_tbl] OFF
SET IDENTITY_INSERT [dbo].[payslip_tbl] ON 

INSERT [dbo].[payslip_tbl] ([id], [date], [staffid], [salary], [vat], [penanty], [bonus], [totalsalary], [note]) VALUES (1, CAST(0x0000AF6E00000000 AS DateTime), 1, CAST(250.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(250.00 AS Decimal(18, 2)), N'')
INSERT [dbo].[payslip_tbl] ([id], [date], [staffid], [salary], [vat], [penanty], [bonus], [totalsalary], [note]) VALUES (2, CAST(0x0000AF6E00000000 AS DateTime), 1, CAST(250.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(250.00 AS Decimal(18, 2)), N'')
INSERT [dbo].[payslip_tbl] ([id], [date], [staffid], [salary], [vat], [penanty], [bonus], [totalsalary], [note]) VALUES (3, CAST(0x0000AF6E00000000 AS DateTime), 2, CAST(300.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(300.00 AS Decimal(18, 2)), N'')
INSERT [dbo].[payslip_tbl] ([id], [date], [staffid], [salary], [vat], [penanty], [bonus], [totalsalary], [note]) VALUES (4, CAST(0x0000AF8A00000000 AS DateTime), 2, CAST(300.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(300.00 AS Decimal(18, 2)), N'')
INSERT [dbo].[payslip_tbl] ([id], [date], [staffid], [salary], [vat], [penanty], [bonus], [totalsalary], [note]) VALUES (5, CAST(0x0000AF8A00000000 AS DateTime), 1, CAST(250.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(250.00 AS Decimal(18, 2)), N'')
INSERT [dbo].[payslip_tbl] ([id], [date], [staffid], [salary], [vat], [penanty], [bonus], [totalsalary], [note]) VALUES (6, CAST(0x0000AF8E00000000 AS DateTime), 3, CAST(250.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), CAST(255.00 AS Decimal(18, 2)), N'')
INSERT [dbo].[payslip_tbl] ([id], [date], [staffid], [salary], [vat], [penanty], [bonus], [totalsalary], [note]) VALUES (7, CAST(0x0000AF9C00000000 AS DateTime), 1, CAST(300.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(300.00 AS Decimal(18, 2)), N'dddd')
INSERT [dbo].[payslip_tbl] ([id], [date], [staffid], [salary], [vat], [penanty], [bonus], [totalsalary], [note]) VALUES (8, CAST(0x0000AF9C00000000 AS DateTime), 2, CAST(300.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(300.00 AS Decimal(18, 2)), N'12')
INSERT [dbo].[payslip_tbl] ([id], [date], [staffid], [salary], [vat], [penanty], [bonus], [totalsalary], [note]) VALUES (9, CAST(0x0000AF9C00000000 AS DateTime), 3, CAST(300.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(300.00 AS Decimal(18, 2)), N'')
INSERT [dbo].[payslip_tbl] ([id], [date], [staffid], [salary], [vat], [penanty], [bonus], [totalsalary], [note]) VALUES (10, CAST(0x0000AF9C00000000 AS DateTime), 3, CAST(300.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(300.00 AS Decimal(18, 2)), N'ddd')
INSERT [dbo].[payslip_tbl] ([id], [date], [staffid], [salary], [vat], [penanty], [bonus], [totalsalary], [note]) VALUES (11, CAST(0x0000AFBE00000000 AS DateTime), 3, CAST(300.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(312.00 AS Decimal(18, 2)), N'')
SET IDENTITY_INSERT [dbo].[payslip_tbl] OFF
SET IDENTITY_INSERT [dbo].[position_tbl] ON 

INSERT [dbo].[position_tbl] ([id], [positionname], [positionnamekh], [status]) VALUES (1, N'Security', N'សន្តិសុខ', 0)
INSERT [dbo].[position_tbl] ([id], [positionname], [positionnamekh], [status]) VALUES (2, N'Recept', N'អ្នកទទួលភ្ញៀវ', 1)
SET IDENTITY_INSERT [dbo].[position_tbl] OFF
SET IDENTITY_INSERT [dbo].[room_tbl] ON 

INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (1, N'001', 1, CAST(2 AS Decimal(18, 0)), 1, N'001', CAST(100.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (2, N'002', 1, CAST(2 AS Decimal(18, 0)), 1, N'002', CAST(100.00 AS Decimal(16, 2)), N'CHECK-IN', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (3, N'003', 1, CAST(2 AS Decimal(18, 0)), 1, N'003', CAST(100.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (4, N'004', 1, CAST(2 AS Decimal(18, 0)), 1, N'004', CAST(100.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (5, N'005', 1, CAST(2 AS Decimal(18, 0)), 1, N'005', CAST(100.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (6, N'006', 1, CAST(2 AS Decimal(18, 0)), 1, N'006', CAST(100.00 AS Decimal(16, 2)), N'FREE', N' 18/02/23 00:00:00 Open date  06/03/23 00:00:00')
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (7, N'101', 1, CAST(2 AS Decimal(18, 0)), 2, N'101', CAST(100.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (8, N'102', 1, CAST(2 AS Decimal(18, 0)), 2, N'102', CAST(100.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (9, N'103', 1, CAST(2 AS Decimal(18, 0)), 2, N'103', CAST(100.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (10, N'104', 1, CAST(2 AS Decimal(18, 0)), 2, N'104', CAST(100.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (11, N'105', 1, CAST(2 AS Decimal(18, 0)), 2, N'105', CAST(100.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (12, N'106', 1, CAST(2 AS Decimal(18, 0)), 2, N'106', CAST(100.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (13, N'107', 1, CAST(2 AS Decimal(18, 0)), 2, N'107', CAST(100.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (14, N'108', 1, CAST(2 AS Decimal(18, 0)), 2, N'108', CAST(100.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (15, N'109', 1, CAST(2 AS Decimal(18, 0)), 2, N'109', CAST(100.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (16, N'110', 1, CAST(2 AS Decimal(18, 0)), 2, N'110', CAST(100.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (17, N'111', 1, CAST(2 AS Decimal(18, 0)), 2, N'111', CAST(100.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (18, N'112', 1, CAST(2 AS Decimal(18, 0)), 2, N'112', CAST(100.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (19, N'201', 1, CAST(2 AS Decimal(18, 0)), 3, N'201', CAST(100.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (20, N'202', 1, CAST(2 AS Decimal(18, 0)), 3, N'202', CAST(100.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (21, N'203', 1, CAST(2 AS Decimal(18, 0)), 3, N'203', CAST(100.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (22, N'204', 1, CAST(2 AS Decimal(18, 0)), 3, N'204', CAST(100.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (23, N'205', 1, CAST(2 AS Decimal(18, 0)), 3, N'205', CAST(100.00 AS Decimal(16, 2)), N'FREE', N'test')
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (24, N'206', 1, CAST(2 AS Decimal(18, 0)), 3, N'206', CAST(100.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (25, N'207', 1, CAST(2 AS Decimal(18, 0)), 3, N'207', CAST(100.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (26, N'208', 1, CAST(2 AS Decimal(18, 0)), 3, N'208', CAST(100.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (27, N'209', 1, CAST(2 AS Decimal(18, 0)), 3, N'209', CAST(100.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (28, N'210', 1, CAST(2 AS Decimal(18, 0)), 3, N'210', CAST(100.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (29, N'211', 1, CAST(2 AS Decimal(18, 0)), 3, N'211', CAST(100.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (30, N'212', 1, CAST(2 AS Decimal(18, 0)), 3, N'212', CAST(100.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (31, N'301', 1, CAST(2 AS Decimal(18, 0)), 4, N'301', CAST(250.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (32, N'302', 1, CAST(2 AS Decimal(18, 0)), 4, N'302', CAST(250.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (33, N'303', 1, CAST(2 AS Decimal(18, 0)), 4, N'303', CAST(250.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (34, N'304', 1, CAST(2 AS Decimal(18, 0)), 4, N'304', CAST(250.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (35, N'305', 1, CAST(2 AS Decimal(18, 0)), 4, N'305', CAST(250.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (36, N'306', 1, CAST(2 AS Decimal(18, 0)), 4, N'307', CAST(250.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (37, N'307', 1, CAST(2 AS Decimal(18, 0)), 4, N'307', CAST(250.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (38, N'308', 1, CAST(2 AS Decimal(18, 0)), 4, N'308', CAST(250.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (39, N'309', 1, CAST(2 AS Decimal(18, 0)), 4, N'309', CAST(250.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (40, N'310', 1, CAST(2 AS Decimal(18, 0)), 4, N'310', CAST(250.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (41, N'311', 1, CAST(2 AS Decimal(18, 0)), 4, N'311', CAST(250.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (42, N'312', 1, CAST(2 AS Decimal(18, 0)), 4, N'312', CAST(250.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (43, N'401', 1, CAST(2 AS Decimal(18, 0)), 5, N'401', CAST(250.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (44, N'402', 1, CAST(2 AS Decimal(18, 0)), 5, N'402', CAST(250.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (45, N'403', 1, CAST(2 AS Decimal(18, 0)), 5, N'403', CAST(250.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (46, N'404', 1, CAST(2 AS Decimal(18, 0)), 5, N'404', CAST(250.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (47, N'405', 1, CAST(2 AS Decimal(18, 0)), 5, N'405', CAST(250.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (48, N'406', 1, CAST(2 AS Decimal(18, 0)), 5, N'406', CAST(250.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (49, N'407', 1, CAST(2 AS Decimal(18, 0)), 5, N'407', CAST(250.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (50, N'408', 1, CAST(2 AS Decimal(18, 0)), 5, N'408', CAST(250.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (51, N'409', 1, CAST(2 AS Decimal(18, 0)), 5, N'409', CAST(250.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (52, N'410', 1, CAST(2 AS Decimal(18, 0)), 5, N'410', CAST(250.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (53, N'411', 1, CAST(2 AS Decimal(18, 0)), 5, N'411', CAST(250.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (54, N'412', 1, CAST(2 AS Decimal(18, 0)), 5, N'412', CAST(250.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (55, N'501', 1, CAST(2 AS Decimal(18, 0)), 6, N'501', CAST(250.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (56, N'502', 1, CAST(2 AS Decimal(18, 0)), 6, N'502', CAST(250.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (57, N'503', 1, CAST(2 AS Decimal(18, 0)), 6, N'503', CAST(250.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (58, N'504', 1, CAST(2 AS Decimal(18, 0)), 6, N'504', CAST(250.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (59, N'505', 1, CAST(2 AS Decimal(18, 0)), 6, N'505', CAST(250.00 AS Decimal(16, 2)), N'BLOCK', N'Test 3/7/2023 12:00:00 AM')
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (60, N'506', 1, CAST(2 AS Decimal(18, 0)), 6, N'506', CAST(250.00 AS Decimal(16, 2)), N'FREE', N'close2023/?/10')
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (61, N'507', 1, CAST(2 AS Decimal(18, 0)), 6, N'507', CAST(250.00 AS Decimal(16, 2)), N'FREE', N'Test  10/02/23 00:00:00')
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (62, N'508', 1, CAST(2 AS Decimal(18, 0)), 6, N'508', CAST(250.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (63, N'509', 1, CAST(2 AS Decimal(18, 0)), 6, N'509', CAST(250.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (64, N'510', 1, CAST(2 AS Decimal(18, 0)), 6, N'510', CAST(250.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (65, N'511', 1, CAST(2 AS Decimal(18, 0)), 6, N'511', CAST(250.00 AS Decimal(16, 2)), N'FREE', NULL)
INSERT [dbo].[room_tbl] ([id], [room_no], [roomtypeid], [servicecharge], [floorid], [roomkey], [price], [status], [note]) VALUES (66, N'512', 1, CAST(2 AS Decimal(18, 0)), 6, N'512', CAST(250.00 AS Decimal(16, 2)), N'FREE', NULL)
SET IDENTITY_INSERT [dbo].[room_tbl] OFF
SET IDENTITY_INSERT [dbo].[roomdetail_tbl] ON 

INSERT [dbo].[roomdetail_tbl] ([id], [roomid], [itemid], [price]) VALUES (3, 4, 4, CAST(100.0000 AS Decimal(16, 4)))
INSERT [dbo].[roomdetail_tbl] ([id], [roomid], [itemid], [price]) VALUES (4, 10, 4, CAST(300.0000 AS Decimal(16, 4)))
INSERT [dbo].[roomdetail_tbl] ([id], [roomid], [itemid], [price]) VALUES (5, 10, 5, CAST(400.0000 AS Decimal(16, 4)))
INSERT [dbo].[roomdetail_tbl] ([id], [roomid], [itemid], [price]) VALUES (6, 1, 4, CAST(50.0000 AS Decimal(16, 4)))
INSERT [dbo].[roomdetail_tbl] ([id], [roomid], [itemid], [price]) VALUES (7, 31, 1, CAST(200.0000 AS Decimal(16, 4)))
INSERT [dbo].[roomdetail_tbl] ([id], [roomid], [itemid], [price]) VALUES (8, 31, 4, CAST(50.0000 AS Decimal(16, 4)))
INSERT [dbo].[roomdetail_tbl] ([id], [roomid], [itemid], [price]) VALUES (9, 3, 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [dbo].[roomdetail_tbl] ([id], [roomid], [itemid], [price]) VALUES (10, 2, 5, CAST(0.0000 AS Decimal(16, 4)))
INSERT [dbo].[roomdetail_tbl] ([id], [roomid], [itemid], [price]) VALUES (13, 2, 4, CAST(0.0000 AS Decimal(16, 4)))
INSERT [dbo].[roomdetail_tbl] ([id], [roomid], [itemid], [price]) VALUES (14, 2, 2, CAST(0.0000 AS Decimal(16, 4)))
SET IDENTITY_INSERT [dbo].[roomdetail_tbl] OFF
SET IDENTITY_INSERT [dbo].[roomtype_tbl] ON 

INSERT [dbo].[roomtype_tbl] ([id], [roomtypename], [roomtypenamekh], [note]) VALUES (1, N'Normal', N'ធម្មតា', NULL)
INSERT [dbo].[roomtype_tbl] ([id], [roomtypename], [roomtypenamekh], [note]) VALUES (2, N'VIP', N'ពិសេស', N'')
INSERT [dbo].[roomtype_tbl] ([id], [roomtypename], [roomtypenamekh], [note]) VALUES (3, N'Test', N'សាក', N'')
SET IDENTITY_INSERT [dbo].[roomtype_tbl] OFF
SET IDENTITY_INSERT [dbo].[salary_tbl] ON 

INSERT [dbo].[salary_tbl] ([id], [staffid], [date], [salary], [note], [createdate], [createby]) VALUES (1, 1, CAST(0xE9440B00 AS Date), CAST(300.00 AS Decimal(18, 2)), N'NOte1', CAST(0x0000AF8E00000000 AS DateTime), N'admin98@gmail.com')
INSERT [dbo].[salary_tbl] ([id], [staffid], [date], [salary], [note], [createdate], [createby]) VALUES (3, 2, CAST(0xC9440B00 AS Date), CAST(300.00 AS Decimal(18, 2)), N'', CAST(0x0000AF6E00000000 AS DateTime), N'a3ee1a89-0bbe-40c8-8132-712d0b49961a')
INSERT [dbo].[salary_tbl] ([id], [staffid], [date], [salary], [note], [createdate], [createby]) VALUES (4, 3, CAST(0xE9440B00 AS Date), CAST(300.00 AS Decimal(18, 2)), N'', CAST(0x0000AF8E00000000 AS DateTime), N'admin98@gmail.com')
SET IDENTITY_INSERT [dbo].[salary_tbl] OFF
SET IDENTITY_INSERT [dbo].[staff_tbl] ON 

INSERT [dbo].[staff_tbl] ([id], [positionid], [name], [namekh], [sex], [phone], [dob], [address], [email], [identityno], [photo], [status], [createby], [createdate]) VALUES (1, 2, N'Kun Sina', N'គុណ​ ស៊ីណា', N'Male', N'0981234', CAST(0x28220B00 AS Date), N'Phnom Penh', N'admin@gmail.com', N'11111', N'RL_2022_12_14_00_11_28.jpg', 1, N'admin98@gmail.com', CAST(0xED440B00 AS Date))
INSERT [dbo].[staff_tbl] ([id], [positionid], [name], [namekh], [sex], [phone], [dob], [address], [email], [identityno], [photo], [status], [createby], [createdate]) VALUES (2, 1, N'Pu Pov', N'ពូពៅ', N'Male', N'0972393063', CAST(0x28220B00 AS Date), N'Phnom Penh', N'phandy010@gmail.com', N'22222', N'RL_2022_12_14_00_11_28.jpg', 1, N'phandy010@gmail.com', CAST(0xC6440B00 AS Date))
INSERT [dbo].[staff_tbl] ([id], [positionid], [name], [namekh], [sex], [phone], [dob], [address], [email], [identityno], [photo], [status], [createby], [createdate]) VALUES (3, 1, N'Pu Phun', N'ពូភុន', N'Male', N'0972393063', CAST(0x28220B00 AS Date), N'Phnom Penh', N'phandy010@gmail.com', N'3333111', N'RL_2022_12_14_00_11_28.jpg', 1, N'admin98@gmail.com', CAST(0xF0440B00 AS Date))
SET IDENTITY_INSERT [dbo].[staff_tbl] OFF
SET IDENTITY_INSERT [dbo].[waterelectricusage_tbl] ON 

INSERT [dbo].[waterelectricusage_tbl] ([id], [guestid], [startdate], [enddate], [wstartrecord], [wendrecord], [estartrecord], [eendrecord], [wepriceid]) VALUES (1, 1, CAST(0xFC440B00 AS Date), CAST(0xFC440B00 AS Date), CAST(10.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[waterelectricusage_tbl] ([id], [guestid], [startdate], [enddate], [wstartrecord], [wendrecord], [estartrecord], [eendrecord], [wepriceid]) VALUES (2, 2, CAST(0xFC440B00 AS Date), CAST(0xFC440B00 AS Date), CAST(20.00 AS Decimal(18, 2)), CAST(20.00 AS Decimal(18, 2)), CAST(20.00 AS Decimal(18, 2)), CAST(20.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[waterelectricusage_tbl] ([id], [guestid], [startdate], [enddate], [wstartrecord], [wendrecord], [estartrecord], [eendrecord], [wepriceid]) VALUES (3, 2, CAST(0xFC440B00 AS Date), CAST(0xFC440B00 AS Date), CAST(20.00 AS Decimal(18, 2)), CAST(22.00 AS Decimal(18, 2)), CAST(20.00 AS Decimal(18, 2)), CAST(22.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[waterelectricusage_tbl] ([id], [guestid], [startdate], [enddate], [wstartrecord], [wendrecord], [estartrecord], [eendrecord], [wepriceid]) VALUES (4, 3, CAST(0xFC440B00 AS Date), CAST(0xFC440B00 AS Date), CAST(25.00 AS Decimal(18, 2)), CAST(25.00 AS Decimal(18, 2)), CAST(25.00 AS Decimal(18, 2)), CAST(25.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[waterelectricusage_tbl] ([id], [guestid], [startdate], [enddate], [wstartrecord], [wendrecord], [estartrecord], [eendrecord], [wepriceid]) VALUES (6, 1, CAST(0xFC440B00 AS Date), CAST(0xFC440B00 AS Date), CAST(10.00 AS Decimal(18, 2)), CAST(55.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(55.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[waterelectricusage_tbl] ([id], [guestid], [startdate], [enddate], [wstartrecord], [wendrecord], [estartrecord], [eendrecord], [wepriceid]) VALUES (7, 8, CAST(0xFC440B00 AS Date), CAST(0x18450B00 AS Date), CAST(23.00 AS Decimal(18, 2)), CAST(25.00 AS Decimal(18, 2)), CAST(23.00 AS Decimal(18, 2)), CAST(25.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[waterelectricusage_tbl] ([id], [guestid], [startdate], [enddate], [wstartrecord], [wendrecord], [estartrecord], [eendrecord], [wepriceid]) VALUES (8, 6, CAST(0xFC440B00 AS Date), CAST(0x18450B00 AS Date), CAST(55.00 AS Decimal(18, 2)), CAST(66.00 AS Decimal(18, 2)), CAST(55.00 AS Decimal(18, 2)), CAST(66.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[waterelectricusage_tbl] ([id], [guestid], [startdate], [enddate], [wstartrecord], [wendrecord], [estartrecord], [eendrecord], [wepriceid]) VALUES (9, 9, CAST(0xFF440B00 AS Date), CAST(0xFF440B00 AS Date), CAST(11.00 AS Decimal(18, 2)), CAST(11.00 AS Decimal(18, 2)), CAST(11.00 AS Decimal(18, 2)), CAST(11.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[waterelectricusage_tbl] ([id], [guestid], [startdate], [enddate], [wstartrecord], [wendrecord], [estartrecord], [eendrecord], [wepriceid]) VALUES (10, 9, CAST(0xFF440B00 AS Date), CAST(0x00450B00 AS Date), CAST(11.00 AS Decimal(18, 2)), CAST(20.00 AS Decimal(18, 2)), CAST(11.00 AS Decimal(18, 2)), CAST(20.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[waterelectricusage_tbl] ([id], [guestid], [startdate], [enddate], [wstartrecord], [wendrecord], [estartrecord], [eendrecord], [wepriceid]) VALUES (11, 6, CAST(0x18450B00 AS Date), CAST(0x37450B00 AS Date), CAST(66.00 AS Decimal(18, 2)), CAST(77.00 AS Decimal(18, 2)), CAST(66.00 AS Decimal(18, 2)), CAST(77.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[waterelectricusage_tbl] ([id], [guestid], [startdate], [enddate], [wstartrecord], [wendrecord], [estartrecord], [eendrecord], [wepriceid]) VALUES (14, 6, CAST(0x37450B00 AS Date), CAST(0x55450B00 AS Date), CAST(77.00 AS Decimal(18, 2)), CAST(200.00 AS Decimal(18, 2)), CAST(77.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[waterelectricusage_tbl] ([id], [guestid], [startdate], [enddate], [wstartrecord], [wendrecord], [estartrecord], [eendrecord], [wepriceid]) VALUES (15, 6, CAST(0x55450B00 AS Date), CAST(0x67450B00 AS Date), CAST(200.00 AS Decimal(18, 2)), CAST(251.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(160.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[waterelectricusage_tbl] ([id], [guestid], [startdate], [enddate], [wstartrecord], [wendrecord], [estartrecord], [eendrecord], [wepriceid]) VALUES (5, 6, CAST(0xFC440B00 AS Date), CAST(0xFC440B00 AS Date), CAST(30.00 AS Decimal(18, 2)), CAST(55.00 AS Decimal(18, 2)), CAST(30.00 AS Decimal(18, 2)), CAST(55.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[waterelectricusage_tbl] ([id], [guestid], [startdate], [enddate], [wstartrecord], [wendrecord], [estartrecord], [eendrecord], [wepriceid]) VALUES (12, 8, CAST(0x18450B00 AS Date), CAST(0x37450B00 AS Date), CAST(25.00 AS Decimal(18, 2)), CAST(66.00 AS Decimal(18, 2)), CAST(25.00 AS Decimal(18, 2)), CAST(66.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[waterelectricusage_tbl] ([id], [guestid], [startdate], [enddate], [wstartrecord], [wendrecord], [estartrecord], [eendrecord], [wepriceid]) VALUES (13, 8, CAST(0x37450B00 AS Date), CAST(0x4D450B00 AS Date), CAST(66.00 AS Decimal(18, 2)), CAST(120.00 AS Decimal(18, 2)), CAST(66.00 AS Decimal(18, 2)), CAST(120.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[waterelectricusage_tbl] ([id], [guestid], [startdate], [enddate], [wstartrecord], [wendrecord], [estartrecord], [eendrecord], [wepriceid]) VALUES (16, 1, CAST(0xFC440B00 AS Date), CAST(0x18450B00 AS Date), CAST(55.00 AS Decimal(18, 2)), CAST(77.00 AS Decimal(18, 2)), CAST(55.00 AS Decimal(18, 2)), CAST(88.00 AS Decimal(18, 2)), 1)
SET IDENTITY_INSERT [dbo].[waterelectricusage_tbl] OFF
SET IDENTITY_INSERT [dbo].[weprice_tbl] ON 

INSERT [dbo].[weprice_tbl] ([id], [date], [waterprice], [electricprice], [status], [IsDeleted]) VALUES (1, CAST(0xAF440B00 AS Date), CAST(1500 AS Decimal(18, 0)), CAST(2000 AS Decimal(18, 0)), 1, 0)
SET IDENTITY_INSERT [dbo].[weprice_tbl] OFF
ALTER TABLE [dbo].[payslip_tbl]  WITH CHECK ADD  CONSTRAINT [FK_payslip_tbl_staff_tbl] FOREIGN KEY([staffid])
REFERENCES [dbo].[staff_tbl] ([id])
GO
ALTER TABLE [dbo].[payslip_tbl] CHECK CONSTRAINT [FK_payslip_tbl_staff_tbl]
GO
ALTER TABLE [dbo].[room_tbl]  WITH CHECK ADD  CONSTRAINT [FK_room_tbl_roomtype_tbl] FOREIGN KEY([roomtypeid])
REFERENCES [dbo].[roomtype_tbl] ([id])
GO
ALTER TABLE [dbo].[room_tbl] CHECK CONSTRAINT [FK_room_tbl_roomtype_tbl]
GO
ALTER TABLE [dbo].[salary_tbl]  WITH CHECK ADD  CONSTRAINT [FK_Salarys_staff_tbl] FOREIGN KEY([staffid])
REFERENCES [dbo].[staff_tbl] ([id])
GO
ALTER TABLE [dbo].[salary_tbl] CHECK CONSTRAINT [FK_Salarys_staff_tbl]
GO
ALTER TABLE [dbo].[staff_tbl]  WITH CHECK ADD  CONSTRAINT [FK_staff_tbl_position_tbl] FOREIGN KEY([positionid])
REFERENCES [dbo].[position_tbl] ([id])
GO
ALTER TABLE [dbo].[staff_tbl] CHECK CONSTRAINT [FK_staff_tbl_position_tbl]
GO
USE [master]
GO
ALTER DATABASE [ROSELANY_APARTMENT] SET  READ_WRITE 
GO
