CREATE PROCEDURE Dashboard
	@Action VARCHAR(20) = null
AS
BEGIN
	
	SET NOCOUNT ON;

	IF @Action = 'CATEGORY'
	BEGIN
		SELECT COUNT(*) FROM dbo.Categories
	END

	IF @Action = 'PRODUCT'
	BEGIN
		SELECT COUNT(*) FROM dbo.Products
	END

	IF @Action = 'ORDER'
	BEGIN
		SELECT COUNT(*) FROM dbo.Orders
	END

	IF @Action = 'DELIVERED'
	BEGIN
		SELECT COUNT(*) FROM dbo.Orders
		WHERE Status = 'Delivered'
	END

	IF @Action = 'PENDING'
	BEGIN
		SELECT COUNT(*) FROM dbo.Orders
		WHERE Status IN ('Pending','Dispatched')
	END

	IF @Action = 'USER'
	BEGIN
		SELECT COUNT(*) FROM dbo.Users
	END

	IF @Action = 'SOLDAMOUNT'
	BEGIN
		SELECT SUM(o.Quantity * p.Price) FROM Orders o
		INNER JOIN Products p ON p.ProductID = o.ProductID
	END

	IF @Action = 'CONTACT'
	BEGIN
		SELECT COUNT(*) FROM dbo.Contact
	END
END

