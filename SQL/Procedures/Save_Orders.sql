CREATE PROCEDURE [dbo].[Save_Orders] @tblOrders OrderDetails READONLY
AS
BEGIN
	SET NOCOUNT ON;

		INSERT INTO Orders(OrderNo, ProductID, Quantity, UserID, Status, PaymentId, OrderDate)
		SELECT OrderNo, ProductID, Quantity, UserID, Status, PaymentId, OrderDate FROM @tblOrders
END