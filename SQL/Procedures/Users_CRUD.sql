SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE Users_CRUD
	@Action VARCHAR(20),
	@UserID INT = NULL,
	@Name VARCHAR(50) = NULL,
	@Username VARCHAR(50) = NULL,
	@Mobile VARCHAR(50) = NULL,
	@Email VARCHAR(50) = NULL,
	@Address VARCHAR(MAX) = NULL,
	@PostCode VARCHAR(50) = NULL,
	@Password VARCHAR(50) = NULL,
	@ImageURL VARCHAR(MAX) = NULL

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- SELECT FOR LOGIN
	IF(@Action = 'SELECT4LOGIN')
		BEGIN
			SELECT * FROM dbo.Users WHERE Username = @Username and Password = @Password
		END

	--SELECT FOR PROFILE
	IF(@Action = 'SELECT4PROFILE')
		BEGIN
			SELECT * FROM dbo.Users WHERE UserID = @UserID
		END

	-- INSERT (REGISTRATION)
	IF(@Action = 'INSERT')
		BEGIN
			INSERT INTO dbo.Users(Name,Username,Mobile,Email,Address,PostCode,Password,ImageURL,CreatedDate)
			VALUES (@Name,@Username,@Mobile,@Email,@Address,@PostCode,@Password,@ImageURL,GETDATE())
		END
	
	--UPDATE USER PROFILE
	IF(@Action = 'UPDATE')
		BEGIN
			DECLARE @UPDATE_IMAGE VARCHAR(20)
			SELECT @UPDATE_IMAGE = (CASE WHEN @ImageURL IS NULL THEN 'NO' ELSE 'YES' END)
			IF(@UPDATE_IMAGE = 'NO')
				BEGIN
					UPDATE dbo.Users
					SET Name = @Name,Username = @Username, Mobile = @Mobile, Email = @Email, Address = @Address, PostCode = @PostCode, Password = @Password
					WHERE UserID = @UserID
				END
			ELSE
				BEGIN
					UPDATE dbo.Users
					SET Name = @Name,Username = @Username, Mobile = @Mobile, Email = @Email, Address = @Address, PostCode = @PostCode, Password = @Password, ImageURL = @ImageURL
					WHERE UserID = @UserID
				END
		END

	-- SELECT FOR ADMIN
	IF(@Action = 'SELECT4ADMIN')
		BEGIN
			SELECT ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS [SrNo], UserID, Name, Username, Email, CreatedDate FROM Users
		END

	--DELETE FOR ADMIN
	IF(@Action = 'DELETE')
		BEGIN
			DELETE FROM dbo.Users WHERE UserID = @UserID
		END
END