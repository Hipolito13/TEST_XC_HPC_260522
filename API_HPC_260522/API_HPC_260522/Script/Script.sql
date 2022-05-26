USE [LoggerApp]
GO
/****** Object:  Table [dbo].[Tbl_Logger]    Script Date: 25/05/2022 09:11:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Logger](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CorrelationId] [nvarchar](50) NOT NULL,
	[Method] [varchar](20) NOT NULL,
	[Url] [varchar](50) NOT NULL,
	[Request] [nvarchar](max) NOT NULL,
	[Response] [nvarchar](max) NOT NULL,
	[EstatusCode] [int] NOT NULL,
	[ErrorMessage] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Tbl_Logger_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[SP_Logger_Ins]    Script Date: 25/05/2022 09:11:37 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:Hipolito Perez Cruz
-- Create date: 2022-05-25
-- Description:	SP para agregar registro de logs
-- =============================================
CREATE PROCEDURE [dbo].[SP_Logger_Ins]
  @CorrelationId varchar(50),
  @Method varchar(20),
  @Url varchar(50),
  @Request nvarchar(max),
  @Response nvarchar(max),
  @Code int
AS
BEGIN
	SET NOCOUNT ON;
		BEGIN TRY  
		INSERT INTO [dbo].[Tbl_Logger]
        VALUES
           (@CorrelationId
           ,@Method
           ,@Url
           ,@Request
           ,@Response
           ,@Code
           ,NULL
           ,GETDATE())
		SELECT CAST(SCOPE_IDENTITY() as int) Id,  null AS ErrorText;
    END TRY  
     BEGIN CATCH   
	 SELECT 0 as Id,  ERROR_MESSAGE() AS ErrorText;
     END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Logger_Upd]    Script Date: 25/05/2022 09:11:37 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:Hipolito Pérez Cruz
-- Create date: 2022-05-25
-- Description:	Actualizar el Status del log
-- =============================================
CREATE PROCEDURE [dbo].[SP_Logger_Upd]
  @CorrelationId varchar(50),
  @ErrorMessage nvarchar(max),
  @Code int,
  @Response nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY  
	    UPDATE [dbo].[Tbl_Logger]
        SET 
         [EstatusCode] = @Code,
         [ErrorMessage] = @ErrorMessage,
         [Response] = @Response
        WHERE  [CorrelationId] = @CorrelationId
		SELECT 1 as Id,  NULL AS ErrorText;
    END TRY  
     BEGIN CATCH   
     SELECT 0 as Id,  ERROR_MESSAGE() AS ErrorText;
     END CATCH
END
GO
