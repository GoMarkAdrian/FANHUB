CREATE TYPE [dbo].[OrderDetails] AS TABLE(
	[OrderNo][varchar](max) NULL,
	[ProductID][int] NULL,
	[Quantity][int] NULL,
	[UserID][int] NULL,
	[Status][varchar](50) NULL,
	[PaymentId][int] NULL,
	[OrderDate][datetime] NULL
)