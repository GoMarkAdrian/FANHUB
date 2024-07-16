USE [FANHUB]
GO
/****** Object:  StoredProcedure [dbo].[ContactSp]    Script Date: 04/07/2024 9:42:10 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[ContactSp]
	@Action VARCHAR(10),
	@ContactId INT = NULL,
	@Name VARCHAR(50) = NULL,
	@Email VARCHAR(50) = NULL,
	@Subject VARCHAR(200) = NULL,
	@Message VARCHAR(Max) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    If @Action ='INSERT'
	BEGIN
			INSERT INTO dbo.Contact(Name, Email, Subject, Message, CreatedDate)
			VALUES (@Name, @Email, @Subject, @Message, GETDATE())
	END

	If @Action ='SELECT'
	BEGIN
			SELECT ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS [SrNo],* FROM dbo.Contact
	END

	If @Action ='DELETE'
	BEGIN
			DELETE FROM dbo.Contact WHERE ContactID = @ContactId
	END

END
