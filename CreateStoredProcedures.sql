-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
USE [BingoAdministration]
GO
CREATE PROCEDURE [Check_If_Password_Used]
	-- Add the parameters for the stored procedure here
	@UserEmail nvarchar(64),
	@NewPassword nvarchar(256)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Declare @PasswordTaken bit 
	set @PasswordTaken = 5
    select @PasswordTaken = Count(*)
		FROM [dbo].[OldPassword]
		where [PasswordHash] = @NewPassword AND [BingoUserId] = (
			select distinct [Id] FROM [dbo].[BingoUser]
				WHERE [Email] = @UserEmail
		)
	RETURN @PasswordTaken
END
GO

CREATE PROCEDURE [Update_Password]
	-- Add the parameters for the stored procedure here
	@UserEmail nvarchar(64),
	@NewPassword nvarchar(256)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	if (@@TRANCOUNT <> 0)
		Return 0

	declare @PasswordTaken bit
	exec @PasswordTaken = [Check_If_Password_Used] @UserEmail = @UserEmail, @NewPassword = @NewPassword

	if (@PasswordTaken <> 0)
		Return 0

	Begin Tran

		declare @UserId uniqueidentifier
		declare @OldPassword nvarchar(256)

		select @UserId = [Id], @OldPassword = [PasswordHash] from [dbo].[BingoUser] where [Email] = @UserEmail

		Insert into [dbo].[OldPassword] ([BingoUserId], [PasswordHash]) values
		(
			@UserId,
			@OldPassword
		)

		update [dbo].[BingoUser]
		set [PasswordHash] = @NewPassword
		Where [Id] = @UserId
	Commit Tran

	Return 1
END
GO