USE [FANHUB]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Invoice]
	@Action VARCHAR(10),
	@PaymentId INT = NULL,
	@UserId int = NULL,
	@OrderDetailsId INT = NULL,
	@Status VARCHAR(50) = NULL
AS
BEGIN
	
	SET NOCOUNT ON;

    if @Action = 'INVOICEBYID'
		begin
			Select ROW_NUMBER() over(order by (select 1)) as [SrNo], o.OrderNo, p.Name, p.Price, o.Quantity,
			(p.Price * o.Quantity) as TotalPrice, o.OrderDate, o.Status from Orders o
			inner join Products p on p.ProductID = o.ProductID
			where o.PaymentID = @PaymentId and o.UserID = @UserId
		end
		if @Action = 'ODRHISTORY'
		begin
			SELECT DISTINCT o.paymentID, p.PaymentMode, p.CardNo from orders o
			inner join Payment p on p.PaymentID = o.PaymentID
			where o.UserID = @UserId
		end
	if @Action = 'GETSTATUS'
		begin
			SELECT o.OderID, o.OrderNo,(pr.Price * o.Quantity) as TotalPrice, o.Status,
			o.OrderDate, p.PaymentMode, pr.Name from Orders o
			inner join Payment p on p.PaymentID = o.PaymentID
			inner join Products pr on pr.ProductID = o.ProductID
		end
		if @Action = 'STATUSBYID'
		begin
			SELECT OderID, status from Orders
			where OderID = @OrderDetailsId
		end
		if @Action = 'UPDSTATUS'
		begin
			update dbo.Orders
			set Status = @Status where OderID = @OrderDetailsId
		end
END
