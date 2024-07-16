USE [FANHUB]
GO
/****** Object:  StoredProcedure [dbo].[SellingReport]    Script Date: 04/07/2024 9:43:48 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SellingReport]
	@FromDate DATE = Null,
	@ToDate DATE = Null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS [SrNo],u.Name,u.Email,
	SUM(o.Quantity) AS TotalOrders, SUM(o.Quantity * p.Price) AS TotalPrice
	FROM Orders o
	INNER JOIN Products p ON p.ProductID = o.ProductID
	INNER JOIN Users u ON u.UserID = o.UserID
	WHERE CAST(o.OrderDate as DATE) BETWEEN @FromDate AND @ToDate
	GROUP BY u.Name, u.Email;

	
END
