USE [ATM]
GO
/****** Object:  Table [dbo].[AccDetails]    Script Date: 11/9/2018 3:14:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccNo] [int] NOT NULL,
	[Balance] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AtmUsers]    Script Date: 11/9/2018 3:14:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AtmUsers](
	[AccNo] [int] IDENTITY(3500,1) NOT NULL,
	[CardNo] [bigint] NOT NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[CardPin] [int] NULL,
	[CreatedDate] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TransactionHistory]    Script Date: 11/9/2018 3:14:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionHistory](
	[AccNo] [int] NOT NULL,
	[DepositAmt] [int] NULL,
	[WithdrawalAmt] [int] NULL,
	[Date] [datetime] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  StoredProcedure [dbo].[AddAmt]    Script Date: 11/9/2018 3:14:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddAmt]
	@CardNo bigint,@DepositAmt int,@IsDeposit int
AS
BEGIN
	
	SET NOCOUNT ON;
	declare @Accn int
	set @Accn = (select top 1 AccNo from AtmUsers where CardNo=@CardNo)
   
   if exists (select * from AccDetails where AccNo=@Accn)
   begin
   update AccDetails
   set Balance= Balance+@DepositAmt where AccNo=@Accn
   end

   else
   begin
   insert into AccDetails values(@Accn,@DepositAmt)
   end

   insert into TransactionHistory values (@Accn,@DepositAmt,0, GETDATE())

END

GO
/****** Object:  StoredProcedure [dbo].[AddAtmUser]    Script Date: 11/9/2018 3:14:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddAtmUser] @FirstName VARCHAR(50)
	,@LastName VARCHAR(50)
	,@Email VARCHAR(50)
	,@CardNo bigint
	,@CardPin INT = 0
	,@Password VARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;

	IF NOT EXISTS (
			SELECT AccNo
			FROM AtmUsers
			WHERE CardNo = @CardNo
			)
	BEGIN
		INSERT INTO AtmUsers (
			FirstName
			,LastName
			,Email
			,Password
			,CardNo
			,CardPin
			,CreatedDate
			)
		VALUES (
			@FirstName
			,@LastName
			,@Email
			,@Password
			,@CardNo
			,@CardPin
			,GETDATE()
			)
	END
END

GO
/****** Object:  StoredProcedure [dbo].[Balance]    Script Date: 11/9/2018 3:14:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Balance]
	@CardNo bigint
AS
BEGIN
	
	SET NOCOUNT ON;
	declare @Accn int
	set @Accn = (select top 1 AccNo from AtmUsers where CardNo=@CardNo)
   
   select ISNULL(Balance,0) Balance from AccDetails where AccNo=@Accn

END

GO
/****** Object:  StoredProcedure [dbo].[IsUserExists]    Script Date: 11/9/2018 3:14:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[IsUserExists]
@CardNo bigint,
@CardPin int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    select * from AtmUsers where CardNo=@CardNo and CardPin=@CardPin
END

GO
/****** Object:  StoredProcedure [dbo].[WithdrawAmt]    Script Date: 11/9/2018 3:14:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[WithdrawAmt]
	@CardNo bigint,@WithdrawAmt int,@IsDeposit int
AS
BEGIN
	
	SET NOCOUNT ON;
	declare @Accn int
	set @Accn = (select top 1 AccNo from AtmUsers where CardNo=@CardNo)
   
   if exists (select * from AccDetails where AccNo=@Accn)
   begin

   if((select balance from AccDetails where AccNo=@Accn)>=@WithdrawAmt)
   begin
   update AccDetails
   set Balance = Balance-@WithdrawAmt where AccNo=@Accn
   end

   end
   

   insert into TransactionHistory values (@Accn,0,@WithdrawAmt, GETDATE())

END

GO
