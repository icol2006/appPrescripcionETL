USE [DbTestImport]
GO

/****** Object:  Table [dbo].[cartera]    Script Date: 8/12/2021 2:20:33 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[cartera](
	[TIPO DOCUMENTO] [varchar](100) NULL,
	[NRO DOCUMENTO] [varchar](100) NULL,
	[COMPARENDO] [varchar](100) NULL,
	[FECHA COMPARENDO] [varchar](50) NULL,
	[PLACA] [varchar](100) NULL,
	[ESTADO] [varchar](100) NULL,
	[  SALDO  ] [varchar](100) NULL,
	[ INTERESES  ] [varchar](100) NULL,
	[TIPO COMPARENDO] [varchar](100) NULL,
	[FECHA PROCESO] [varchar](50) NULL,
	[VALOR NOMINAL] [varchar](100) NULL,
	[INFRACCION] [varchar](50) NULL,
	[DESCRIPCION INFRACCION] [varchar](1004) NULL,
	[TIPO SERVICIO] [varchar](100) NULL,
	[VALOR TOTAL] [varchar](100) NULL,
	[UVT] [varchar](50) NULL,
	[NRO RESOLUCION] [varchar](50) NULL,
	[FECHA RESOLUCION] [varchar](50) NULL,
	[FECHA FIRME] [varchar](50) NULL,
	[TIPO RESOLUCION] [varchar](100) NULL,
	[PAGOS] [varchar](100) NULL,
	[CORREO] [varchar](100) NULL,
	[TELEFONO ] [varchar](50) NULL,
	[DIRECCION] [varchar](1000) NULL,
	[NOMBRE] [varchar](150) NULL,
	[NRO MANDAMIENTO] [varchar](100) NULL,
	[FECHA MANDAMIENTO] [varchar](50) NULL,
	[NOTIFICACION] [varchar](100) NULL,
	[FECHA NOTIFICACION] [varchar](50) NULL,
	[ULTIMO PASO] [varchar](100) NULL,
	[FECHA ULTIMO PASO] [varchar](50) NULL,
	[FECHA CITACION] [varchar](50) NULL,
	[MEDIO IMPOSICION] [varchar](250) NULL,
	[FECHA SEGUNDO FIRME] [varchar](50) NULL,
	[ENVIADO SEGUNDA INSTANCIA] [varchar](100) NULL,
	[FECHA FALLO SEGUNDA INSTANCIA] [varchar](50) NULL,
	[ETAPA] [varchar](50) NULL,
	[llave color naranja] [varchar](100) NULL,
	[derive1] [date] NULL,
	[FECHA_COMPARENDO] [date] NULL,
	[FECHA_NOTIFICACION] [date] NULL,
	[FECHA_MANDAMIENTO] [date] NULL,
	[ID] [varchar](100) NULL,
	[sum_fecha_comparendo] [date] NULL,
	[sum_fecha_notificacion] [date] NULL,
	[dias_suspencion_comparendo] [int] NULL,
	[dias_suspencion_notificacion] [int] NULL,
	[res_fecha_comparendo] [date] NULL,
	[res_fecha_notificacion] [date] NULL,
	[prescrito_comparendo] [varchar](10) NULL,
	[prescrito_notificacion] [varchar](10) NULL,
	[num] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]
GO


