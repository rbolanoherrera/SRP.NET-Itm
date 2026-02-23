USE [InventarioDEV]
GO
/****** Object:  Table [dbo].[Producto]    Script Date: 2026-02-22 8:43:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [varchar](20) NULL,
	[Nombre] [varchar](100) NULL,
	[IdTipoProducto] [int] NULL,
	[ValorCompra] [numeric](18, 0) NOT NULL,
	[ValorVenta] [numeric](18, 0) NOT NULL,
	[Iva] [decimal](6, 2) NULL,
	[Imagen] [image] NULL,
	[IdUnidadMedidaBase] [int] NULL,
	[IdUnidadMedidaCompra] [int] NULL,
	[IdUnidadMedidaVenta] [int] NULL,
	[CantEquivalente] [decimal](18, 2) NULL,
	[CodigoBarras] [varchar](20) NULL,
	[ProveedorId] [int] NULL,
	[CantMinimaAlert] [int] NULL,
	[CreatedAt] [datetime] NULL,
	[UserCreated] [varchar](128) NOT NULL,
	[UpdatedAt] [datetime] NULL,
	[UserUpdated] [varchar](128) NULL,
	[EstadoId] [int] NOT NULL,
	[FechaVencimiento] [datetime] NULL,
 CONSTRAINT [PK_Producto] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [CU_Producto_Codigo] UNIQUE NONCLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [CU_Producto_Nombre_EstadoId] UNIQUE NONCLUSTERED 
(
	[Nombre] ASC,
	[EstadoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2026-02-22 8:43:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](40) NOT NULL,
	[IdTipoDocumento] [int] NULL,
	[Documento] [varchar](20) NULL,
	[Nombres] [varchar](30) NULL,
	[Apellidos] [varchar](30) NULL,
	[Email] [varchar](80) NULL,
	[Password] [varchar](250) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Producto] ADD  CONSTRAINT [DF_Producto_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]
GO
/****** Object:  StoredProcedure [dbo].[SP_CREATE_PRODUCT]    Script Date: 2026-02-22 8:43:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Rafael Bolaños Herrera
-- Create date: 2022-04-18
-- Description:	Creación de Productos en la BD
-- =============================================
CREATE PROCEDURE [dbo].[SP_CREATE_PRODUCT]
@Id INT OUTPUT,
@Cod varchar(20) = null,
@Name varchar(100), 
@IdTipoProducto int = null,
@ValorCompra numeric(18,2), 
@ValorVenta numeric(18,2), 
@ValIva decimal(6,3) = null, 
@Imagen image = null, 
@IdUniBase int = null, 
@IdUniMedCompra int = null, 
@IdUniMedVenta int = null, 
@CantEquiva decimal(18,6) = null, 
@CodBarras varchar(20)=null, 
@cantMinimaAlert int = null,
@cantStock int = null,
@ProveedorId int = null,
@UnidadesMedidas varchar(200)=null,
@EstadoId int=null,
@UserCreate varchar(128)=null,
@fechaVencimiento datetime=null,
@fecha datetime
AS
BEGIN

	INSERT INTO Producto (Nombre, IdTipoProducto, ValorCompra, 
		ValorVenta, Iva,Imagen, IdUnidadMedidaBase,
        IdUnidadMedidaCompra, IdUnidadMedidaVenta, CantEquivalente, 
		CodigoBarras, ProveedorId, CantMinimaAlert, estadoId,
		UserCreated, FechaVencimiento, CreatedAt) 
	VALUES (@Name, @IdTipoProducto, @ValorCompra, @ValorVenta, @ValIva, 
		@Imagen, @IdUniBase, @IdUniMedCompra, 
		@IdUniMedVenta, @CantEquiva, @CodBarras, 
		@cantMinimaAlert, @ProveedorId, @EstadoId,
		@UserCreate, @fechaVencimiento, @fecha); 
		   
	--SELECT CAST(SCOPE_IDENTITY() as int);
	set @Id = @@Identity

	if @Id <> 0
	begin

		update Producto set Codigo = 'Prod' + CONVERT(varchar(12), @Id)
		where Id= @Id

		--delete from ProductoUnidadMedida  where ProductoId = @ProductoId
		--insert into ProductoUnidadMedida (ProductoId, UnidadMedidaId) 
		--select @Id, * from fnSplitString(@UnidadesMedidas,',')

		if(@cantStock is not null)
		begin
			INSERT INTO InventarioIngreso (ProductoId, ProveedorId, CantidadComprada
			 ,IdUnidadMedidaCompra, ValorCompra, ValorVenta
			 ,Fecha, FechaVencimiento, UserCreated)
			VALUES (@Id, @ProveedorId, @cantStock
			 ,@IdUniMedCompra, @ValorCompra, @ValorVenta
			 ,@Fecha, @FechaVencimiento, @UserCreate)

			INSERT INTO Inventario (ProductoId,Cantidad,LastUpdated,UserUpdated)
			VALUES (@Id, @cantStock, @fecha, @UserCreate)

		end

	end

END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cantidad equivalente entre UnidadMedicaCompra y UnidadMedidaVenta para descontarla en el Inventario al hacer una Venta' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Producto', @level2type=N'COLUMN',@level2name=N'CantEquivalente'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'La cantidad minima que existe en el Inventario para generar una alerta por que se esta acabando' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Producto', @level2type=N'COLUMN',@level2name=N'CantMinimaAlert'
GO
