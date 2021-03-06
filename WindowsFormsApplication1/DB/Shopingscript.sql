USE [shopping]
GO
/****** Object:  Table [dbo].[SaleDetail]    Script Date: 3/17/2022 1:53:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleDetail](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[billno] [int] NULL,
	[ItemNo] [varchar](50) NULL,
	[itemName] [varchar](50) NULL,
	[QTY] [varchar](50) NULL,
	[Tax] [decimal](18, 0) NULL,
	[price] [decimal](18, 0) NULL,
 CONSTRAINT [PK_SaleDetail] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SaleMaster]    Script Date: 3/17/2022 1:53:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleMaster](
	[billno] [int] IDENTITY(1,1) NOT NULL,
	[date] [datetime] NULL,
	[customer] [varchar](50) NULL,
	[tax] [decimal](18, 0) NULL,
	[total] [decimal](18, 0) NULL,
 CONSTRAINT [PK_SaleMaster] PRIMARY KEY CLUSTERED 
(
	[billno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[GET_ALL_SALES]    Script Date: 3/17/2022 1:53:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GET_ALL_SALES] @startdate varchar(max),@endDate varchar(max)
as
begin
select billno as Id, date as Date,customer As Customer ,tax as Tax ,total as Total from salemaster
where ( @startdate is  null or @startdate is null) or (date between @startdate and @endDate) 
end

GO
/****** Object:  StoredProcedure [dbo].[INSERT_IN_SALEMASTER]    Script Date: 3/17/2022 1:53:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[INSERT_IN_SALEMASTER] 
@date varchar(max),@customer varchar(max),@tax decimal ,@total decimal,@id int output
as
begin
insert into  [dbo].[SaleMaster]values(@date,@customer,@tax,@total)
SELECT @id=SCOPE_IDENTITY()
end
GO
/****** Object:  StoredProcedure [dbo].[INSERT_INTO_SALEDETAIL]    Script Date: 3/17/2022 1:53:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[INSERT_INTO_SALEDETAIL] @billno int,@itemNo varchar(max),@itemName varchar(max),@price decimal,@qty int,@tax decimal
as
begin
insert into[dbo].[SaleDetail] values(@billno,@itemNo,@itemName,@qty,@tax,@price)
end
GO
