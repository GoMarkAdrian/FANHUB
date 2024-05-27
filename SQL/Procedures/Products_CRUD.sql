SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE Products_CRUD
	@Action VARCHAR(15),
	@ProductID INT = NULL,
	@Name VARCHAR(100) = NULL,
	@Description VARCHAR(MAX) = NULL,
	@Price DECIMAL(18,2) = 0,
	@Quantity INT = NULL,
	@ImageURL VARCHAR(MAX) = NULL,
	@CategoryID INT = NULL,
	@IsActive BIT = false


AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- SELECT EVERYTHING
	IF(@Action = 'SELECT')
	BEGIN
		SELECT p.*, c.Name AS CategoryName FROM dbo.Products p INNER JOIN dbo.Categories c ON c.CategoryID = p.CategoryID ORDER BY p.CreatedDate DESC
	END

	-- INSERT
	IF(@Action = 'INSERT')
	BEGIN
		INSERT INTO dbo.Products(Name, Description, Price, Quantity, ImageURL, CategoryID, IsActive, CreatedDate) VALUES (@Name, @Description, @Price, @Quantity, @ImageURL, @CategoryID, @IsActive, GETDATE())
	END


	-- UPDATE
	IF(@Action = 'UPDATE')
	BEGIN
		DECLARE @UPDATE_IMAGE VARCHAR(20)
		SELECT @UPDATE_IMAGE = (CASE WHEN @ImageURL IS NULL THEN 'NO' ELSE 'YES' END)
		IF @UPDATE_IMAGE = 'NO'
			BEGIN
				UPDATE dbo.Products
				SET Name = @Name, Description = @Description, Price = @Price, Quantity = @Quantity, CategoryID = @CategoryID, IsActive = @IsActive WHERE ProductID = @ProductID
			END
		ELSE
			BEGIN
				UPDATE dbo.Products
				SET Name = @Name, Description = @Description, Price = @Price, Quantity = @Quantity, ImageURL = @ImageURL, CategoryID = @CategoryID, IsActive = @IsActive WHERE ProductID = @ProductID
			END
	END

	--UPDATE QUANTITY
	IF(@Action = 'QTYUPDATE')
	BEGIN
		UPDATE dbo.Products SET Quantity = @Quantity
		WHERE ProductID = @ProductID
	END

	-- DELETE
	IF(@Action = 'DELETE')
	BEGIN
		DELETE FROM dbo.Products WHERE ProductID = @ProductID
	END

	-- GET ID
	IF(@Action = 'GETBYID')
	BEGIN
		SELECT * FROM dbo.Products WHERE ProductID = @ProductID
	END
END
