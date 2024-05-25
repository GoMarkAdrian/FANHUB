
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- CRUD FOR CATEGORY TABLE
CREATE PROCEDURE Category_CRUD
	@Action VARCHAR(15),
	@CategoryID INT = NULL,
	@Name VARCHAR(100) = NULL,
	@ImageURL VARCHAR(MAX) = NULL,
	@IsActive BIT = FALSE
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- GET ALL CATEGORY
 	IF(@Action = 'SELECT')
	BEGIN
		SELECT * FROM dbo.Categories ORDER BY CreatedDate DESC
	END

	-- INSERT CATEGORY
	IF(@Action = 'INSERT')
	BEGIN
		INSERT INTO dbo.Categories(Name, ImageUrl, IsActive, CreatedDate)
		VALUES(@Name, @ImageUrl, @IsActive, GETDATE())
	END

	-- UPDATE CATEGORY
	IF(@Action = 'UPDATE')
	BEGIN
		DECLARE @UPDATE_IMAGE VARCHAR(20)
		SELECT @UPDATE_IMAGE = (CASE WHEN @ImageUrl IS NULL THEN 'NO' ELSE 'YES' END)
		IF (@UPDATE_IMAGE = 'NO')
			BEGIN
				UPDATE dbo.Categories
				SET Name = @Name, IsActive = @IsActive
				WHERE @CategoryID = @CategoryID
			END
		ELSE
			BEGIN
				UPDATE dbo.Categories
				SET Name = @Name, @ImageUrl= @ImageUrl, IsActive = @IsActive
				WHERE @CategoryID = @CategoryID				
			END
	END


	-- DELETE CATEGORY
 	IF(@Action = 'DELETE')
	BEGIN
		DELETE FROM dbo.Categories WHERE CategoryID = @CategoryID
	END

	-- GET ACTIVE Category by ID
	IF(@Action = 'GETBYID')
	BEGIN
		SELECT * FROM dbo.Categories WHERE CategoryID = @CategoryID
	END

END
GO
