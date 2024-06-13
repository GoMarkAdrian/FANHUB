USE [FANHUB]
GO
/****** Object:  StoredProcedure [dbo].[Cart_CRUD]    Script Date: 6/13/2024 4:10:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Cart_CRUD]
@Action VARCHAR(10),
@ProductID INT = NULL,
@Quantity INT = NULL,
@UserID INT = NULL
AS 
BEGIN 
SET NOCOUNT ON;

--SELECT 
IF @Action = 'SELECT'
	BEGIN
		SELECT c.ProductID,p.Name,p.ImageURL,p.Price,c.Quantity, c.Quantity AS Qty,p.Quantity AS PrdQty
		FROM dbo.Carts c
		INNER JOIN dbo.Products p ON p.ProductID = c.ProductID
		WHERE C.UserID = @UserID
	END

--INSERT 
IF @Action = 'INSERT'
	BEGIN
		INSERT INTO dbo.Carts(ProductID,Quantity,UserID)
		VALUES(@ProductID,@Quantity,@UserID)
	END

--UPDATE 
IF @Action = 'UPDATE'
	BEGIN
		UPDATE dbo.Carts
		SET Quantity = @Quantity 
		WHERE ProductID = @ProductID AND UserID = @UserID
	END

--DELETE 
IF @Action = 'DELETE'
	BEGIN
		DELETE FROM dbo.Carts
		WHERE ProductID = @ProductID AND UserID = @UserID
	END
	
--GET BY ID( PRODUCTID AND USERID)
IF @Action = 'GETBYID'
	BEGIN
		SELECT * FROM dbo.Carts
		WHERE ProductID = @ProductID AND UserID = @UserID
	END


END