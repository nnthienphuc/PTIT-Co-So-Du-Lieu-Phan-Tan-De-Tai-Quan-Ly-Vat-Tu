/****** Author: NGuyen Ngoc Thien Phuc (RUFFA)    Script Date: 28-Aug-24 5:32:46 PM ******/
USE [QLVT_DATHANG]
GO
/****** Object:  Table [dbo].[Vattu]    Script Date: 28-Aug-24 5:32:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vattu](
	[MAVT] [nchar](4) NOT NULL,
	[TENVT] [nvarchar](30) NOT NULL,
	[DVT] [nvarchar](15) NOT NULL,
	[SOLUONGTON] [int] NOT NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
 CONSTRAINT [PK_VatTu] PRIMARY KEY CLUSTERED 
(
	[MAVT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UK_TENVT] UNIQUE NONCLUSTERED 
(
	[TENVT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[view_DanhSachVatTu]    Script Date: 28-Aug-24 5:32:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_DanhSachVatTu]
AS
SELECT        MAVT, TENVT, DVT, SOLUONGTON
FROM            dbo.Vattu WITH (INDEX = IX_TENVT)
GO
/****** Object:  Table [dbo].[NhanVien]    Script Date: 28-Aug-24 5:32:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhanVien](
	[MANV] [int] NOT NULL,
	[HO] [nvarchar](40) NOT NULL,
	[TEN] [nvarchar](10) NOT NULL,
	[SOCMND] [nvarchar](20) NULL,
	[DIACHI] [nvarchar](100) NULL,
	[NGAYSINH] [datetime] NULL,
	[LUONG] [float] NULL,
	[MACN] [nchar](10) NULL,
	[TrangThaiXoa] [bit] NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
 CONSTRAINT [PK_NhanVien] PRIMARY KEY CLUSTERED 
(
	[MANV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[view_DanhSachNhanVien]    Script Date: 28-Aug-24 5:32:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_DanhSachNhanVien]
AS
SELECT        MANV, HO, TEN, SOCMND, DIACHI, NGAYSINH, LUONG, MACN, TrangThaiXoa
FROM            dbo.NhanVien WITH (INDEX = ix_ten)
GO
/****** Object:  View [dbo].[view_DanhSachPhanManh]    Script Date: 28-Aug-24 5:32:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_DanhSachPhanManh]
AS
SELECT        PUBS.description AS TENCN, SUBS.subscriber_server AS TENSERVER
FROM            dbo.sysmergepublications AS PUBS INNER JOIN
                         dbo.sysmergesubscriptions AS SUBS ON PUBS.pubid = SUBS.pubid AND PUBS.publisher <> SUBS.subscriber_server AND PUBS.description <> N'Tra Cứu'
GO
/****** Object:  Table [dbo].[ChiNhanh]    Script Date: 28-Aug-24 5:32:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiNhanh](
	[MACN] [nchar](10) NOT NULL,
	[ChiNhanh] [nvarchar](100) NOT NULL,
	[DIACHI] [nvarchar](100) NOT NULL,
	[SoDT] [nvarchar](15) NOT NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
 CONSTRAINT [PK_ChiNhanh] PRIMARY KEY CLUSTERED 
(
	[MACN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UK_ChiNhanh] UNIQUE NONCLUSTERED 
(
	[ChiNhanh] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CTDDH]    Script Date: 28-Aug-24 5:32:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CTDDH](
	[MasoDDH] [nchar](8) NOT NULL,
	[MAVT] [nchar](4) NOT NULL,
	[SOLUONG] [int] NULL,
	[DONGIA] [float] NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
 CONSTRAINT [PK_CTDDH] PRIMARY KEY CLUSTERED 
(
	[MasoDDH] ASC,
	[MAVT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CTPN]    Script Date: 28-Aug-24 5:32:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CTPN](
	[MAPN] [nchar](8) NOT NULL,
	[MAVT] [nchar](4) NOT NULL,
	[SOLUONG] [int] NOT NULL,
	[DONGIA] [float] NOT NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
 CONSTRAINT [PK_CTPN] PRIMARY KEY CLUSTERED 
(
	[MAPN] ASC,
	[MAVT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CTPX]    Script Date: 28-Aug-24 5:32:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CTPX](
	[MAPX] [nchar](8) NOT NULL,
	[MAVT] [nchar](4) NOT NULL,
	[SOLUONG] [int] NOT NULL,
	[DONGIA] [float] NOT NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
 CONSTRAINT [PK_CTPX] PRIMARY KEY CLUSTERED 
(
	[MAPX] ASC,
	[MAVT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DatHang]    Script Date: 28-Aug-24 5:32:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DatHang](
	[MasoDDH] [nchar](8) NOT NULL,
	[NGAY] [date] NOT NULL,
	[NhaCC] [nvarchar](100) NOT NULL,
	[MANV] [int] NOT NULL,
	[MAKHO] [nchar](4) NOT NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
 CONSTRAINT [PK_DatHang] PRIMARY KEY CLUSTERED 
(
	[MasoDDH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kho]    Script Date: 28-Aug-24 5:32:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kho](
	[MAKHO] [nchar](4) NOT NULL,
	[TENKHO] [nvarchar](30) NOT NULL,
	[DIACHI] [nvarchar](100) NULL,
	[MACN] [nchar](10) NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
 CONSTRAINT [PK_Kho] PRIMARY KEY CLUSTERED 
(
	[MAKHO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UK_TENKHO] UNIQUE NONCLUSTERED 
(
	[TENKHO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhieuNhap]    Script Date: 28-Aug-24 5:32:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuNhap](
	[MAPN] [nchar](8) NOT NULL,
	[NGAY] [date] NOT NULL,
	[MasoDDH] [nchar](8) NOT NULL,
	[MANV] [int] NOT NULL,
	[MAKHO] [nchar](4) NOT NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
 CONSTRAINT [PK_PhieuNhap] PRIMARY KEY CLUSTERED 
(
	[MAPN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UK_MaSoDDH] UNIQUE NONCLUSTERED 
(
	[MasoDDH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhieuXuat]    Script Date: 28-Aug-24 5:32:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuXuat](
	[MAPX] [nchar](8) NOT NULL,
	[NGAY] [date] NOT NULL,
	[HOTENKH] [nvarchar](100) NOT NULL,
	[MANV] [int] NOT NULL,
	[MAKHO] [nchar](4) NOT NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
 CONSTRAINT [PK_PX] PRIMARY KEY CLUSTERED 
(
	[MAPX] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [MSmerge_index_901578250]    Script Date: 28-Aug-24 5:32:46 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [MSmerge_index_901578250] ON [dbo].[ChiNhanh]
(
	[rowguid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [MSmerge_index_933578364]    Script Date: 28-Aug-24 5:32:46 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [MSmerge_index_933578364] ON [dbo].[CTDDH]
(
	[rowguid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [MSmerge_index_965578478]    Script Date: 28-Aug-24 5:32:46 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [MSmerge_index_965578478] ON [dbo].[CTPN]
(
	[rowguid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [MSmerge_index_997578592]    Script Date: 28-Aug-24 5:32:46 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [MSmerge_index_997578592] ON [dbo].[CTPX]
(
	[rowguid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [MSmerge_index_1029578706]    Script Date: 28-Aug-24 5:32:46 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [MSmerge_index_1029578706] ON [dbo].[DatHang]
(
	[rowguid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [MSmerge_index_1093578934]    Script Date: 28-Aug-24 5:32:46 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [MSmerge_index_1093578934] ON [dbo].[Kho]
(
	[rowguid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [ix_ten]    Script Date: 28-Aug-24 5:32:46 PM ******/
CREATE NONCLUSTERED INDEX [ix_ten] ON [dbo].[NhanVien]
(
	[TEN] ASC,
	[HO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [MSmerge_index_1125579048]    Script Date: 28-Aug-24 5:32:46 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [MSmerge_index_1125579048] ON [dbo].[NhanVien]
(
	[rowguid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [MSmerge_index_1173579219]    Script Date: 28-Aug-24 5:32:46 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [MSmerge_index_1173579219] ON [dbo].[PhieuNhap]
(
	[rowguid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [MSmerge_index_1237579447]    Script Date: 28-Aug-24 5:32:46 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [MSmerge_index_1237579447] ON [dbo].[PhieuXuat]
(
	[rowguid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_TENVT]    Script Date: 28-Aug-24 5:32:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_TENVT] ON [dbo].[Vattu]
(
	[TENVT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [MSmerge_index_1285579618]    Script Date: 28-Aug-24 5:32:46 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [MSmerge_index_1285579618] ON [dbo].[Vattu]
(
	[rowguid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ChiNhanh] ADD  CONSTRAINT [MSmerge_df_rowguid_2D6B2484FF384B3896B4FAB816AEFBED]  DEFAULT (newsequentialid()) FOR [rowguid]
GO
ALTER TABLE [dbo].[CTDDH] ADD  CONSTRAINT [MSmerge_df_rowguid_8B0896A2C65B4653BB7E4AFBEC4E92A7]  DEFAULT (newsequentialid()) FOR [rowguid]
GO
ALTER TABLE [dbo].[CTPN] ADD  CONSTRAINT [MSmerge_df_rowguid_47458459EC2347E6A2DFEFC218D4818F]  DEFAULT (newsequentialid()) FOR [rowguid]
GO
ALTER TABLE [dbo].[CTPX] ADD  CONSTRAINT [MSmerge_df_rowguid_89CB7312F7F7491B9670EE42FAC464EC]  DEFAULT (newsequentialid()) FOR [rowguid]
GO
ALTER TABLE [dbo].[DatHang] ADD  CONSTRAINT [DF_DatHang_MasoDDH]  DEFAULT (getdate()) FOR [MasoDDH]
GO
ALTER TABLE [dbo].[DatHang] ADD  CONSTRAINT [DF_DatHang_NGAY]  DEFAULT (getdate()) FOR [NGAY]
GO
ALTER TABLE [dbo].[DatHang] ADD  CONSTRAINT [MSmerge_df_rowguid_72740CC5D67A4EE68776D6EEA029568B]  DEFAULT (newsequentialid()) FOR [rowguid]
GO
ALTER TABLE [dbo].[Kho] ADD  CONSTRAINT [MSmerge_df_rowguid_7CB87CB038254D749CE81719A5F6D177]  DEFAULT (newsequentialid()) FOR [rowguid]
GO
ALTER TABLE [dbo].[NhanVien] ADD  CONSTRAINT [DF_NhanVien_TrangThaiXoa]  DEFAULT ((0)) FOR [TrangThaiXoa]
GO
ALTER TABLE [dbo].[NhanVien] ADD  CONSTRAINT [MSmerge_df_rowguid_BA396F93ACD54CA9BEF6E67675B4868E]  DEFAULT (newsequentialid()) FOR [rowguid]
GO
ALTER TABLE [dbo].[PhieuNhap] ADD  CONSTRAINT [DF_PhieuNhap_MAPN]  DEFAULT (getdate()) FOR [MAPN]
GO
ALTER TABLE [dbo].[PhieuNhap] ADD  CONSTRAINT [DF_PhieuNhap_NGAY]  DEFAULT (getdate()) FOR [NGAY]
GO
ALTER TABLE [dbo].[PhieuNhap] ADD  CONSTRAINT [MSmerge_df_rowguid_526C72B9789D431FA1907CDE0C410731]  DEFAULT (newsequentialid()) FOR [rowguid]
GO
ALTER TABLE [dbo].[PhieuXuat] ADD  CONSTRAINT [DF_PX_NGAY]  DEFAULT (getdate()) FOR [NGAY]
GO
ALTER TABLE [dbo].[PhieuXuat] ADD  CONSTRAINT [MSmerge_df_rowguid_02DA9577430B4E028E310AE6FBC87078]  DEFAULT (newsequentialid()) FOR [rowguid]
GO
ALTER TABLE [dbo].[Vattu] ADD  CONSTRAINT [MSmerge_df_rowguid_7EB0692F3DF84C87B016E186DBAB270B]  DEFAULT (newsequentialid()) FOR [rowguid]
GO
ALTER TABLE [dbo].[CTDDH]  WITH NOCHECK ADD  CONSTRAINT [FK_CTDDH_DatHang] FOREIGN KEY([MasoDDH])
REFERENCES [dbo].[DatHang] ([MasoDDH])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[CTDDH] CHECK CONSTRAINT [FK_CTDDH_DatHang]
GO
ALTER TABLE [dbo].[CTDDH]  WITH NOCHECK ADD  CONSTRAINT [FK_CTDDH_VatTu] FOREIGN KEY([MAVT])
REFERENCES [dbo].[Vattu] ([MAVT])
ON UPDATE CASCADE
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[CTDDH] CHECK CONSTRAINT [FK_CTDDH_VatTu]
GO
ALTER TABLE [dbo].[CTPN]  WITH NOCHECK ADD  CONSTRAINT [FK_CTPN_PhieuNhap] FOREIGN KEY([MAPN])
REFERENCES [dbo].[PhieuNhap] ([MAPN])
ON UPDATE CASCADE
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[CTPN] CHECK CONSTRAINT [FK_CTPN_PhieuNhap]
GO
ALTER TABLE [dbo].[CTPN]  WITH NOCHECK ADD  CONSTRAINT [FK_CTPN_VatTu] FOREIGN KEY([MAVT])
REFERENCES [dbo].[Vattu] ([MAVT])
ON UPDATE CASCADE
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[CTPN] CHECK CONSTRAINT [FK_CTPN_VatTu]
GO
ALTER TABLE [dbo].[CTPX]  WITH NOCHECK ADD  CONSTRAINT [FK_CTPX_PX] FOREIGN KEY([MAPX])
REFERENCES [dbo].[PhieuXuat] ([MAPX])
ON UPDATE CASCADE
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[CTPX] CHECK CONSTRAINT [FK_CTPX_PX]
GO
ALTER TABLE [dbo].[CTPX]  WITH NOCHECK ADD  CONSTRAINT [FK_CTPX_VatTu] FOREIGN KEY([MAVT])
REFERENCES [dbo].[Vattu] ([MAVT])
ON UPDATE CASCADE
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[CTPX] CHECK CONSTRAINT [FK_CTPX_VatTu]
GO
ALTER TABLE [dbo].[DatHang]  WITH CHECK ADD  CONSTRAINT [FK_DatHang_Kho] FOREIGN KEY([MAKHO])
REFERENCES [dbo].[Kho] ([MAKHO])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[DatHang] CHECK CONSTRAINT [FK_DatHang_Kho]
GO
ALTER TABLE [dbo].[DatHang]  WITH NOCHECK ADD  CONSTRAINT [FK_DatHang_NhanVien] FOREIGN KEY([MANV])
REFERENCES [dbo].[NhanVien] ([MANV])
ON UPDATE CASCADE
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[DatHang] CHECK CONSTRAINT [FK_DatHang_NhanVien]
GO
ALTER TABLE [dbo].[Kho]  WITH NOCHECK ADD  CONSTRAINT [FK_Kho_Kho] FOREIGN KEY([MACN])
REFERENCES [dbo].[ChiNhanh] ([MACN])
ON UPDATE CASCADE
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[Kho] CHECK CONSTRAINT [FK_Kho_Kho]
GO
ALTER TABLE [dbo].[NhanVien]  WITH CHECK ADD  CONSTRAINT [FK_NhanVien_ChiNhanh] FOREIGN KEY([MACN])
REFERENCES [dbo].[ChiNhanh] ([MACN])
GO
ALTER TABLE [dbo].[NhanVien] CHECK CONSTRAINT [FK_NhanVien_ChiNhanh]
GO
ALTER TABLE [dbo].[PhieuNhap]  WITH NOCHECK ADD  CONSTRAINT [FK_PhieuNhap_DatHang] FOREIGN KEY([MasoDDH])
REFERENCES [dbo].[DatHang] ([MasoDDH])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[PhieuNhap] CHECK CONSTRAINT [FK_PhieuNhap_DatHang]
GO
ALTER TABLE [dbo].[PhieuNhap]  WITH CHECK ADD  CONSTRAINT [FK_PhieuNhap_Kho] FOREIGN KEY([MAKHO])
REFERENCES [dbo].[Kho] ([MAKHO])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[PhieuNhap] CHECK CONSTRAINT [FK_PhieuNhap_Kho]
GO
ALTER TABLE [dbo].[PhieuNhap]  WITH NOCHECK ADD  CONSTRAINT [FK_PhieuNhap_NhanVien] FOREIGN KEY([MANV])
REFERENCES [dbo].[NhanVien] ([MANV])
ON UPDATE CASCADE
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[PhieuNhap] CHECK CONSTRAINT [FK_PhieuNhap_NhanVien]
GO
ALTER TABLE [dbo].[PhieuXuat]  WITH CHECK ADD  CONSTRAINT [FK_PhieuXuat_Kho] FOREIGN KEY([MAKHO])
REFERENCES [dbo].[Kho] ([MAKHO])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[PhieuXuat] CHECK CONSTRAINT [FK_PhieuXuat_Kho]
GO
ALTER TABLE [dbo].[PhieuXuat]  WITH NOCHECK ADD  CONSTRAINT [FK_PX_NhanVien] FOREIGN KEY([MANV])
REFERENCES [dbo].[NhanVien] ([MANV])
ON UPDATE CASCADE
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[PhieuXuat] CHECK CONSTRAINT [FK_PX_NhanVien]
GO
ALTER TABLE [dbo].[CTDDH]  WITH NOCHECK ADD  CONSTRAINT [CK_DONGIA] CHECK NOT FOR REPLICATION (([DONGIA]>(0)))
GO
ALTER TABLE [dbo].[CTDDH] CHECK CONSTRAINT [CK_DONGIA]
GO
ALTER TABLE [dbo].[CTDDH]  WITH NOCHECK ADD  CONSTRAINT [CK_SOLUONG] CHECK NOT FOR REPLICATION (([SOLUONG]>(0)))
GO
ALTER TABLE [dbo].[CTDDH] CHECK CONSTRAINT [CK_SOLUONG]
GO
ALTER TABLE [dbo].[CTPN]  WITH NOCHECK ADD  CONSTRAINT [CK_DONGIACTPN] CHECK NOT FOR REPLICATION (([DONGIA]>=(0)))
GO
ALTER TABLE [dbo].[CTPN] CHECK CONSTRAINT [CK_DONGIACTPN]
GO
ALTER TABLE [dbo].[CTPN]  WITH NOCHECK ADD  CONSTRAINT [CK_SOLUONGCTPN] CHECK NOT FOR REPLICATION (([SOLUONG]>(0)))
GO
ALTER TABLE [dbo].[CTPN] CHECK CONSTRAINT [CK_SOLUONGCTPN]
GO
ALTER TABLE [dbo].[CTPX]  WITH NOCHECK ADD  CONSTRAINT [CK_DONGIACTPX] CHECK NOT FOR REPLICATION (([DONGIA]>=(0)))
GO
ALTER TABLE [dbo].[CTPX] CHECK CONSTRAINT [CK_DONGIACTPX]
GO
ALTER TABLE [dbo].[CTPX]  WITH NOCHECK ADD  CONSTRAINT [CK_SOLUONGCTPX] CHECK NOT FOR REPLICATION (([SOLUONG]>(0)))
GO
ALTER TABLE [dbo].[CTPX] CHECK CONSTRAINT [CK_SOLUONGCTPX]
GO
ALTER TABLE [dbo].[NhanVien]  WITH NOCHECK ADD  CONSTRAINT [CK_LUONG] CHECK NOT FOR REPLICATION (([LUONG]>=(4000000)))
GO
ALTER TABLE [dbo].[NhanVien] CHECK CONSTRAINT [CK_LUONG]
GO
ALTER TABLE [dbo].[Vattu]  WITH NOCHECK ADD  CONSTRAINT [CK_SOLUONGTON] CHECK NOT FOR REPLICATION (([SOLUONGTON]>=(0)))
GO
ALTER TABLE [dbo].[Vattu] CHECK CONSTRAINT [CK_SOLUONGTON]
GO
/****** Object:  StoredProcedure [dbo].[sp_CapNhatSoLuongVatTu]    Script Date: 28-Aug-24 5:32:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_CapNhatSoLuongVatTu]
	@CHEDO NVARCHAR(6),
	@MAVT NCHAR(4),
	@SOLUONG INT
AS
BEGIN
	-- NEU XUAT VAT TU RA
	IF( @CHEDO = 'EXPORT')
	BEGIN
		IF(EXISTS(SELECT * FROM DBO.Vattu AS VT WHERE VT.MAVT = @MAVT))
			BEGIN
				UPDATE DBO.Vattu
				SET SOLUONGTON = SOLUONGTON - @SOLUONG
				WHERE MAVT = @MAVT
			END
	END

	-- NEU NHAP VAT TU VAO
	IF( @CHEDO = 'IMPORT')
	BEGIN
		IF(EXISTS(SELECT * FROM DBO.Vattu AS VT WHERE VT.MAVT = @MAVT) )
			BEGIN
				UPDATE DBO.Vattu
				SET SOLUONGTON = SOLUONGTON + @SOLUONG
				WHERE MAVT = @MAVT
			END
	END
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ChiTietSoLuongTriGiaHangHoaNhapXuat]    Script Date: 28-Aug-24 5:32:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_ChiTietSoLuongTriGiaHangHoaNhapXuat]
@ROLE NVARCHAR(8),
@TYPE NVARCHAR(4),
@DATEFROM DATETIME,
@DATETO DATETIME
AS
BEGIN
	IF( @ROLE = 'CONGTY')
	BEGIN
		IF(@TYPE = 'NHAP')--NHAP
		BEGIN
				SELECT FORMAT(NGAY,'MM-yyyy') AS THANGNAM, 
						TENVT, 
						SUM(SOLUONG) AS TONGSOLUONG,
						SUM(SOLUONG * DONGIA) AS TRIGIA
				FROM
					(SELECT NGAY,MAPN 
						FROM LINK0.QLVT_DATHANG.DBO.PhieuNhap
						WHERE NGAY BETWEEN @DATEFROM AND @DATETO)PHIEU,
					(SELECT TENVT,MAVT FROM Vattu ) VT,
					(SELECT SOLUONG, DONGIA, MAPN,MAVT FROM LINK0.QLVT_DATHANG.DBO.CTPN) CT
				WHERE PHIEU.MAPN = CT.MAPN
				AND VT.MAVT = CT.MAVT
				GROUP BY FORMAT(NGAY,'MM-yyyy'), TENVT
				ORDER BY FORMAT(NGAY,'MM-yyyy'), TENVT
		END

		ELSE--@TYPE = 'XUAT'
		BEGIN
					SELECT FORMAT(NGAY,'MM-yyyy') AS THANGNAM, 
						TENVT, 
						SUM(SOLUONG) AS TONGSOLUONG,
						SUM(SOLUONG * DONGIA) AS TRIGIA
					FROM
						( SELECT NGAY,MAPX
							FROM LINK0.QLVT_DATHANG.DBO.PhieuXuat
							WHERE NGAY BETWEEN @DATEFROM  AND @DATETO   )PHIEU,
						( SELECT TENVT,MAVT FROM Vattu ) VT,
						( SELECT SOLUONG, DONGIA, MAPX,MAVT FROM LINK0.QLVT_DATHANG.DBO.CTPX) CT
					WHERE PHIEU.MAPX = CT.MAPX
					AND VT.MAVT = CT.MAVT
					GROUP BY FORMAT(NGAY,'MM-yyyy'), TENVT
					ORDER BY FORMAT(NGAY,'MM-yyyy'), TENVT
		END
	END

	ELSE -- CHI NHANH & USER
	BEGIN
		IF(@TYPE = 'NHAP')
		BEGIN
				SELECT FORMAT(NGAY,'MM-yyyy') AS THANGNAM, 
						TENVT, 
						SUM(SOLUONG) AS TONGSOLUONG,
						SUM(SOLUONG * DONGIA) AS TRIGIA
				FROM
					( SELECT NGAY,MAPN 
						FROM PhieuNhap
						WHERE NGAY BETWEEN @DATEFROM  AND @DATETO )PHIEU,
					( SELECT TENVT,MAVT FROM Vattu ) VT,
					( SELECT SOLUONG, DONGIA, MAPN,MAVT FROM CTPN) CT
				WHERE PHIEU.MAPN = CT.MAPN
				AND VT.MAVT = CT.MAVT
				GROUP BY FORMAT(NGAY,'MM-yyyy'), TENVT
				ORDER BY FORMAT(NGAY,'MM-yyyy'), TENVT
		END
		ELSE -- @LOAI = 'XUAT'
		BEGIN
					SELECT FORMAT(NGAY,'MM-yyyy') AS THANGNAM, 
						TENVT, 
						SUM(SOLUONG) AS TONGSOLUONG,
						SUM(SOLUONG * DONGIA) AS TRIGIA
					FROM
						( SELECT NGAY,MAPX
							FROM PhieuXuat
							WHERE NGAY BETWEEN @DATEFROM AND @DATETO   )PHIEU,
						( SELECT TENVT,MAVT FROM Vattu ) VT,
						( SELECT SOLUONG, DONGIA, MAPX,MAVT FROM CTPX) CT
					WHERE PHIEU.MAPX = CT.MAPX
					AND VT.MAVT = CT.MAVT
					GROUP BY FORMAT(NGAY,'MM-yyyy'), TENVT
					ORDER BY FORMAT(NGAY,'MM-yyyy'), TENVT
		END
	END
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ChuyenChiNhanh]    Script Date: 28-Aug-24 5:32:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_ChuyenChiNhanh] 
	@MANV INT, 
	@MACN nchar(10)
AS
DECLARE
@LGNAME VARCHAR(50),
@USERNAME VARCHAR(50)
SET XACT_ABORT ON;
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
BEGIN
	BEGIN DISTRIBUTED TRAN
		DECLARE @HONV NVARCHAR(40),
		 @TENNV NVARCHAR(10),
		 @CMND NVARCHAR(20),
		 @DIACHINV NVARCHAR(100),
		 @NGAYSINHNV DATETIME,
		 @LUONGNV FLOAT				
		 
		SELECT @HONV = HO, @TENNV = TEN, @CMND = SOCMND, @DIACHINV = DIACHI, @NGAYSINHNV = NGAYSINH, @LUONGNV = LUONG FROM NhanVien WHERE MANV = @MANV

		IF EXISTS(select MANV
				from LINK1.QLVT_DATHANG.dbo.NhanVien
				where HO = @HONV and TEN = @TENNV and SOCMND = @CMND)
		BEGIN
				UPDATE LINK1.QLVT_DATHANG.dbo.NhanVien
				SET TrangThaiXoa = 0,
				DIACHI = @DIACHINV,
				NGAYSINH = @NGAYSINHNV ,
				LUONG = @LUONGNV
				WHERE MANV = (select MANV
								from LINK1.QLVT_DATHANG.dbo.NhanVien
								where HO = @HONV and TEN = @TENNV and SOCMND = @CMND)
		END
		
		ELSE
		BEGIN
			INSERT INTO LINK1.QLVT_DATHANG.dbo.NhanVien (MANV, HO, TEN, SOCMND, DIACHI, NGAYSINH, LUONG, MACN, TRANGTHAIXOA)
			VALUES ((SELECT MAX(MANV) FROM LINK2.QLVT_DATHANG.dbo.NhanVien) + 1, @HONV, @TENNV, @CMND, @DIACHINV, @NGAYSINHNV, @LUONGNV, @MACN, 0)
		END
		UPDATE dbo.NhanVien
		SET TrangThaiXoa = 1
		WHERE MANV = @MANV
	COMMIT TRAN;

		IF EXISTS(SELECT SUSER_SNAME(sid) FROM sys.sysusers WHERE name = CAST(@MANV AS NVARCHAR))
		BEGIN
			SET @LGNAME = CAST((SELECT SUSER_SNAME(sid) FROM sys.sysusers WHERE name = CAST(@MANV AS NVARCHAR)) AS VARCHAR(50))
			SET @USERNAME = CAST(@MANV AS VARCHAR(50))
			EXEC SP_DROPUSER @USERNAME;
			EXEC SP_DROPLOGIN @LGNAME;
		END	
END
GO
/****** Object:  StoredProcedure [dbo].[sp_DangNhap]    Script Date: 28-Aug-24 5:32:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[sp_DangNhap]
	@TENLOGIN NVARCHAR( 100)
AS
	DECLARE @UID INT,
	@MANV NVARCHAR(100)
	SELECT @UID= uid , @MANV= NAME FROM sys.sysusers 
  	WHERE sid = SUSER_SID(@TENLOGIN)

	SELECT  MANV= @MANV, 
       		HOTEN = (SELECT HO+ ' '+TEN FROM dbo.NHANVIEN WHERE MANV=@MANV ), 
       		TENNHOM=NAME
  	FROM sys.sysusers
    	WHERE UID = (SELECT groupuid FROM sys.sysmembers WHERE memberuid=@uid)
GO
/****** Object:  StoredProcedure [dbo].[sp_DonHangCoPhieuNhapChua]    Script Date: 28-Aug-24 5:32:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_DonHangCoPhieuNhapChua]
@MaDonHang nchar(8)
AS
BEGIN
	IF(EXISTS(SELECT 1 FROM DBO.PhieuNhap AS P WHERE P.MasoDDH = @MaDonHang))
		RETURN 1;
	ELSE IF( EXISTS(SELECT 1 FROM LINK1.QLVT_DATHANG.DBO.PhieuNhap AS P WHERE P.MasoDDH = @MaDonHang))
		RETURN 1;
	RETURN 0;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_DonHangKhongPhieuNhap]    Script Date: 28-Aug-24 5:32:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_DonHangKhongPhieuNhap]
AS
BEGIN
SELECT DH.MasoDDH, 
	DH.Ngay, 
	DH.NhaCC, 
	HOTEN,
	TENVT,
	SOLUONG,
	DONGIA
FROM 
(SELECT MasoDDH, NGAY, NhaCC, HOTEN = (SELECT HOTEN = HO + ' ' + TEN 
										FROM NhanVien 
										WHERE DatHang.MANV = NhanVien.MANV) 
	FROM DBO.DatHang) DH,
 (SELECT MasoDDH,MAVT,SOLUONG,DONGIA FROM CTDDH ) CT,
 (SELECT TENVT, MAVT FROM Vattu ) VT
WHERE CT.MasoDDH = DH.MasoDDH
AND VT.MAVT = CT.MAVT
AND DH.MasoDDH NOT IN (SELECT MasoDDH FROM PhieuNhap)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_HoatDongNhanVien]    Script Date: 28-Aug-24 5:32:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_HoatDongNhanVien] @MANV int,
@DATEFROM DATETIME,
@DATETO DATETIME
AS
BEGIN
    -- Tạo bảng tạm để lưu kết quả
    CREATE TABLE #TEMP (
        THANGNAM NVARCHAR(7),
        NGAY DATETIME,
        MAPHIEU NVARCHAR(10),
        LOAIPHIEU NVARCHAR(10),
        HOTENKH NVARCHAR(100),
        TENVT NVARCHAR(50),
        SOLUONG INT,
        DONGIA DECIMAL(18, 2)
    );

    -- Thêm dữ liệu từ PhieuNhap vào bảng tạm
    INSERT INTO #TEMP
    SELECT FORMAT(PN.NGAY,'yyyy-MM') AS THANGNAM,
				PN.NGAY, 
				PN.MAPN AS MAPHIEU,
				N'Nhap' AS LOAIPHIEU,
				null as HOTENKH,
				TENVT, 
				SOLUONG,
				DONGIA
		FROM (SELECT NGAY, 
					MAPN,
					TENKHO = ( SELECT TENKHO FROM Kho WHERE P.MAKHO = Kho.MAKHO )
				FROM PhieuNhap AS P
				WHERE NGAY BETWEEN @DATEFROM AND @DATETO 
				AND MANV = @MANV )PN,
				CTPN,
				(SELECT MAVT, TENVT FROM Vattu ) VT
		WHERE PN.MAPN =CTPN.MAPN
		AND VT.MAVT = CTPN.MAVT

    -- Thêm dữ liệu từ PhieuXuat vào bảng tạm
    INSERT INTO #TEMP
    SELECT FORMAT(PX.NGAY,'yyyy-MM') AS THANGNAM,
				PX.NGAY, 
				PX.MAPX AS MAPHIEU,
				N'Xuat' AS LOAIPHIEU,
				HOTENKH,
				TENVT, 
				SOLUONG,
				DONGIA
		FROM (SELECT NGAY, 
					MAPX,
					P.HOTENKH,
					TENKHO = ( SELECT TENKHO FROM Kho WHERE P.MAKHO = Kho.MAKHO )
				
				FROM PhieuXuat AS P
				WHERE NGAY BETWEEN @DATEFROM AND @DATETO 
				AND MANV = @MANV )PX,
				CTPX,
				(SELECT MAVT, TENVT FROM Vattu ) VT
		WHERE PX.MAPX =CTPX.MAPX
		AND VT.MAVT = CTPX.MAVT

    -- Truy vấn kết quả từ bảng tạm và sắp xếp
    SELECT * FROM #TEMP
    ORDER BY NGAY, MAPHIEU, TENVT;

    -- Xóa bảng tạm
    DROP TABLE #TEMP;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_KiemTraChiTietPhieuNhap]    Script Date: 28-Aug-24 5:32:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_KiemTraChiTietPhieuNhap]
@MAPN NCHAR(10),
@MAVT NCHAR(10)
AS 
BEGIN
	IF EXISTS(SELECT 1 
			  FROM CTPN AS CT  
			  WHERE CT.MAPN = @MAPN
			  AND CT.MAVT = @MAVT)
			RETURN 1; 
	
	ELSE IF EXISTS(SELECT 1
				   FROM LINK1.QLVT_DATHANG.DBO.CTPN AS CT
				   WHERE CT.MAPN = @MAPN
					AND CT.MAVT = @MAVT)
			RETURN 1;
	RETURN 0;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_KiemTraMaDonDatHang]    Script Date: 28-Aug-24 5:32:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_KiemTraMaDonDatHang]
	@id nvarchar(15)
AS
BEGIN
	IF( EXISTS(SELECT * FROM DatHang AS DH WHERE DH.MasoDDH = @id) )
		RETURN 1;
	ELSE IF ( EXISTS( SELECT * FROM LINK1.QLVT_DATHANG.DBO.DATHANG AS DH WHERE DH.MasoDDH = @id ) )
		RETURN 1;
	RETURN 0;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_KiemTraMaPhieuNhap]    Script Date: 28-Aug-24 5:32:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_KiemTraMaPhieuNhap]
@MAPN nChar(8)
AS
BEGIN
	IF( EXISTS( SELECT * FROM PhieuNhap WHERE MAPN = @MAPN ) )
		RETURN 1;--TON TAI MA PHIEU NHAP

	ELSE IF( EXISTS( SELECT * FROM LINK1.QLVT_DATHANG.DBO.PhieuNhap WHERE MAPN = @MAPN ) )
		RETURN 1;--TON TAI MA PHIEU NHAP
	RETURN 0;-- KHONG TON TAI
END
GO
/****** Object:  StoredProcedure [dbo].[sp_KiemTraMaPhieuXuat]    Script Date: 28-Aug-24 5:32:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_KiemTraMaPhieuXuat]
	@MAPX nChar(8)
AS
BEGIN
	if( EXISTS( SELECT * FROM DBO.PhieuXuat AS PX WHERE PX.MAPX = @MAPX))
		RETURN 1;
	ELSE IF( EXISTS( SELECT * FROM LINK1.QLVT_DATHANG.DBO.PhieuXuat AS PX WHERE PX.MAPX = @MAPX) )
		RETURN 1;
	RETURN 0;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_KiemTraMaVatTuChiNhanhConLai]    Script Date: 28-Aug-24 5:32:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_KiemTraMaVatTuChiNhanhConLai]
	@MAVT NVARCHAR(4)
AS
BEGIN
	IF EXISTS( SELECT 1 FROM LINK1.QLVT_DATHANG.DBO.Vattu as V
				WHERE V.MAVT = @MAVT
				AND				
				(EXISTS(SELECT 1 FROM LINK1.QLVT_DATHANG.DBO.CTPN WHERE CTPN.MAVT = @MAVT))
				OR  (EXISTS(SELECT 1 FROM LINK1.QLVT_DATHANG.DBO.CTDDH WHERE CTDDH.MAVT = @MAVT))
				OR (EXISTS(SELECT 1 FROM LINK1.QLVT_DATHANG.DBO.CTPX WHERE CTPX.MAVT = @MAVT)))
		RETURN 1;

	RETURN 0;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_TaoTaiKhoan]    Script Date: 28-Aug-24 5:32:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[sp_TaoTaiKhoan]
    @LGNAME VARCHAR(20),  @PASS VARCHAR(20),
    @USERNAME VARCHAR(20),  @ROLE VARCHAR(20)     
AS
  DECLARE @RET INT
  EXEC @RET= SP_ADDLOGIN @LGNAME, @PASS, 'QLVT_DATHANG'
  IF (@RET =1)  -- LOGIN NAME BI TRUNG
  BEGIN
     RAISERROR ('Login name bị trùng', 16,1)
	 RETURN
  END 
  EXEC @RET= SP_GRANTDBACCESS @LGNAME, @USERNAME
  IF (@RET =1)  -- USER  NAME BI TRUNG
  BEGIN
       EXEC SP_DROPLOGIN @LGNAME
       RAISERROR ('Nhân viên dã có login name', 16,2)
       RETURN
  END
  EXEC sp_addrolemember @ROLE, @USERNAME
  IF @ROLE = 'CONGTY' OR @ROLE = 'CHINHANH'
  BEGIN
      EXEC sp_addsrvrolemember @LGNAME, 'SecurityAdmin'
  END
GO
/****** Object:  StoredProcedure [dbo].[sp_TongHopNhapXuat]    Script Date: 28-Aug-24 5:32:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_TongHopNhapXuat]
@FromDate DATETIME,
@ToDate DATETIME
AS
BEGIN
	--------------------phieu nhap--------------------------
	SELECT PN.NGAY,
			NHAP = SUM (CT.DONGIA * CT.SOLUONG),
			TYLENHAP = (SUM (CT.DONGIA * CT.SOLUONG)/ (SELECT SUM(DONGIA * SOLUONG)
						FROM CTPN , PhieuNhap WHERE CTPN.MAPN = PhieuNhap.MAPN
						AND PhieuNhap.NGAY BETWEEN @FromDate AND @ToDate )) INTO #PHIEUNHAPTABLE
	FROM PhieuNhap AS PN,
		CTPN AS CT
	WHERE PN.MAPN = CT.MAPN
	AND PN.NGAY BETWEEN @FromDate AND @ToDate
	GROUP BY PN.NGAY
	
	--------------------phieu xuat--------------------------
	SELECT PX.NGAY,
			XUAT = SUM (CT.DONGIA * CT.SOLUONG),
			TYLEXUAT = (SUM (CT.DONGIA * CT.SOLUONG)/ (SELECT SUM(DONGIA * SOLUONG)
						FROM CTPX , PhieuXuat WHERE CTPX.MAPX = PhieuXuat.MAPX
						AND PhieuXuat.NGAY BETWEEN @FromDate AND @ToDate )) INTO #PHIEUXUATTABLE
	FROM PhieuXuat AS PX,
		CTPX AS CT
	WHERE PX.MAPX = CT.MAPX
	AND PX.NGAY BETWEEN @FromDate AND @ToDate
	GROUP BY PX.NGAY
	-----------------------TONG HOP--------------------------------------
	SELECT 
		ISNULL(PN.NGAY,PX.NGAY) AS NGAY,
		ISNULL(PN.NHAP, 0) AS NHAP,
		ISNULL(PN.TYLENHAP,0) TILENHAP,
		ISNULL(PX.XUAT,0) XUAT,
		ISNULL(PX.TYLEXUAT,0) AS TILEXUAT
	FROM #PHIEUNHAPTABLE AS PN 
	FULL JOIN #PHIEUXUATTABLE AS PX
	ON PN.NGAY = PX.NGAY
	ORDER BY NGAY
END
GO
/****** Object:  StoredProcedure [dbo].[sp_TraCuu_KiemTraMaKho]    Script Date: 28-Aug-24 5:32:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_TraCuu_KiemTraMaKho]
	@MAKHO nchar(4)
as
begin
	if( exists( select 1 
				from LINK2.QLVT_DATHANG.DBO.KHO as K 
				where K.MAKHO = @MAKHO ) )
		return 1;
	return 0;
end
GO
/****** Object:  StoredProcedure [dbo].[sp_TraCuu_KiemTraMaNhanVien]    Script Date: 28-Aug-24 5:32:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_TraCuu_KiemTraMaNhanVien]
	@MANHANVIEN int
as
begin
	if exists( select * from LINK2.QLVT_DATHANG.DBO.NHANVIEN as NV where NV.MANV = @MANHANVIEN)
		return 1;
	return 0;
end
GO
/****** Object:  StoredProcedure [dbo].[sp_TraCuu_KiemTraMaVatTu]    Script Date: 28-Aug-24 5:32:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[sp_TraCuu_KiemTraMaVatTu]
@MAVT nchar(4)
AS
BEGIN
	IF EXISTS(SELECT 1 
			  FROM Vattu VT  
			  WHERE VT.MAVT = @MAVT)
			RETURN 1;
	RETURN 0;
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "NhanVien"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'view_DanhSachNhanVien'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'view_DanhSachNhanVien'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "PUBS"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 322
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "SUBS"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 288
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'view_DanhSachPhanManh'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'view_DanhSachPhanManh'
GO
