CREATE PROCEDURE [dbo].[CreateUser]
	@Email VARCHAR(100),
	@Pseudo VARCHAR(50),
	@Password VARCHAR(30)

AS
BEGIN

	DECLARE @Salt VARCHAR(50)
	DECLARE @HashedPassword VARBINARY(64)

	SET @Salt = CONVERT(VARCHAR(50), NEWID())
	SET @HashedPassword = HASHBYTES('SHA2_512', CONCAT(@Salt, @Password))

	INSERT INTO [User_] (Pseudo, Email, [Password], Salt)
	OUTPUT inserted.Id
	VALUES(@Pseudo, @Email, @HashedPassword, @Salt)

END