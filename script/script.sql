USE [grugru]
GO
ALTER TABLE [dbo].[NhanVien] DROP CONSTRAINT [FK_NhanVien_LoaiNhanVien]
GO
ALTER TABLE [dbo].[HoaDon] DROP CONSTRAINT [FK_HoaDon_NhanVien]
GO
ALTER TABLE [dbo].[HoaDon] DROP CONSTRAINT [FK_HoaDon_KhachHang]
GO
ALTER TABLE [dbo].[ChiTietHoaDon] DROP CONSTRAINT [FK_ChiTietHoaDon_SanPham]
GO
ALTER TABLE [dbo].[ChiTietHoaDon] DROP CONSTRAINT [FK_ChiTietHoaDon_HoaDon]
GO
ALTER TABLE [dbo].[NhanVien] DROP CONSTRAINT [DF_NhanVien_soDienThoai]
GO
ALTER TABLE [dbo].[NhanVien] DROP CONSTRAINT [DF_NhanVien_cmnd]
GO
ALTER TABLE [dbo].[NhanVien] DROP CONSTRAINT [DF_NhanVien_soThangKinhNghiem]
GO
ALTER TABLE [dbo].[KhachHang] DROP CONSTRAINT [DF_KhachHang_cmnd]
GO
ALTER TABLE [dbo].[KhachHang] DROP CONSTRAINT [DF_KhachHang_diemTichLuy]
GO
ALTER TABLE [dbo].[ChiTietHoaDon] DROP CONSTRAINT [DF_ChiTietHoaDon_size]
GO
ALTER TABLE [dbo].[ChiTietHoaDon] DROP CONSTRAINT [DF_ChiTietHoaDon_soLuong]
GO
/****** Object:  Index [UN_maSanPham]    Script Date: 04/01/2019 12:26:39 CH ******/
ALTER TABLE [dbo].[SanPham] DROP CONSTRAINT [UN_maSanPham]
GO
/****** Object:  Index [UN_maNV]    Script Date: 04/01/2019 12:26:39 CH ******/
ALTER TABLE [dbo].[NhanVien] DROP CONSTRAINT [UN_maNV]
GO
/****** Object:  Index [UN_loaiNV]    Script Date: 04/01/2019 12:26:39 CH ******/
ALTER TABLE [dbo].[LoaiNhanVien] DROP CONSTRAINT [UN_loaiNV]
GO
/****** Object:  Index [UN_maKH]    Script Date: 04/01/2019 12:26:39 CH ******/
ALTER TABLE [dbo].[KhachHang] DROP CONSTRAINT [UN_maKH]
GO
/****** Object:  Index [UN_maHoaDon]    Script Date: 04/01/2019 12:26:39 CH ******/
ALTER TABLE [dbo].[HoaDon] DROP CONSTRAINT [UN_maHoaDon]
GO
/****** Object:  Table [dbo].[SanPham]    Script Date: 04/01/2019 12:26:39 CH ******/
DROP TABLE [dbo].[SanPham]
GO
/****** Object:  Table [dbo].[NhanVien]    Script Date: 04/01/2019 12:26:39 CH ******/
DROP TABLE [dbo].[NhanVien]
GO
/****** Object:  Table [dbo].[LoaiNhanVien]    Script Date: 04/01/2019 12:26:39 CH ******/
DROP TABLE [dbo].[LoaiNhanVien]
GO
/****** Object:  Table [dbo].[KhachHang]    Script Date: 04/01/2019 12:26:39 CH ******/
DROP TABLE [dbo].[KhachHang]
GO
/****** Object:  Table [dbo].[HoaDon]    Script Date: 04/01/2019 12:26:39 CH ******/
DROP TABLE [dbo].[HoaDon]
GO
/****** Object:  Table [dbo].[ChiTietHoaDon]    Script Date: 04/01/2019 12:26:39 CH ******/
DROP TABLE [dbo].[ChiTietHoaDon]
GO
/****** Object:  Table [dbo].[ChiTietHoaDon]    Script Date: 04/01/2019 12:26:39 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietHoaDon](
	[idHoaDon] [int] NOT NULL,
	[idSanPham] [int] NOT NULL,
	[soLuong] [int] NOT NULL,
	[size] [nchar](1) NOT NULL,
 CONSTRAINT [PK_ChiTietHoaDon] PRIMARY KEY CLUSTERED 
(
	[idHoaDon] ASC,
	[idSanPham] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HoaDon]    Script Date: 04/01/2019 12:26:39 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoaDon](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[maHoaDon] [nchar](10) NOT NULL,
	[thoiGianLap] [bigint] NOT NULL,
	[gia] [money] NOT NULL,
	[idKhachHangMua] [int] NULL,
	[idNhanVienLap] [int] NOT NULL,
 CONSTRAINT [PK_HoaDon] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KhachHang]    Script Date: 04/01/2019 12:26:39 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhachHang](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[maKH] [nchar](10) NOT NULL,
	[hoTen] [nvarchar](50) NOT NULL,
	[ngaySinh] [bigint] NOT NULL,
	[soDienThoai] [nchar](15) NOT NULL,
	[diemTichLuy] [int] NOT NULL,
	[cmnd] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_KhachHang] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiNhanVien]    Script Date: 04/01/2019 12:26:39 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiNhanVien](
	[id] [int] NOT NULL,
	[loaiNV] [nchar](10) NOT NULL,
 CONSTRAINT [PK_LoaiNhanVien] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhanVien]    Script Date: 04/01/2019 12:26:39 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhanVien](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[maNV] [nchar](10) NOT NULL,
	[idLoaiNV] [int] NOT NULL,
	[hoTen] [nvarchar](50) NOT NULL,
	[ngaySinh] [bigint] NOT NULL,
	[matKhau] [varchar](max) NOT NULL,
	[soThangKinhNghiem] [numeric](18, 0) NOT NULL,
	[cmnd] [nvarchar](50) NOT NULL,
	[soDienThoai] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_NhanVien] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SanPham]    Script Date: 04/01/2019 12:26:39 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SanPham](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[maSanPham] [nchar](10) NOT NULL,
	[tenSanPham] [nvarchar](50) NOT NULL,
	[gia] [money] NOT NULL,
	[thongTin] [nvarchar](max) NULL,
 CONSTRAINT [PK_SanPham] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[KhachHang] ON 

INSERT [dbo].[KhachHang] ([id], [maKH], [hoTen], [ngaySinh], [soDienThoai], [diemTichLuy], [cmnd]) VALUES (4, N'06SJO9LL0 ', N'Nguyễn Thị Bích Vân', 883587600, N'01345798520    ', 0, N'111111111')
INSERT [dbo].[KhachHang] ([id], [maKH], [hoTen], [ngaySinh], [soDienThoai], [diemTichLuy], [cmnd]) VALUES (5, N'0MK8922DP ', N'Nguyễn Sĩ Văn', 894474000, N'01345794452    ', 0, N'222222222')
INSERT [dbo].[KhachHang] ([id], [maKH], [hoTen], [ngaySinh], [soDienThoai], [diemTichLuy], [cmnd]) VALUES (6, N'0TQ4P3VQD ', N'Thái Thiên Vũ', 900003600, N'01345797752    ', 0, N'333333333')
INSERT [dbo].[KhachHang] ([id], [maKH], [hoTen], [ngaySinh], [soDienThoai], [diemTichLuy], [cmnd]) VALUES (7, N'0RLTPSGO2 ', N'Đặng Thanh Tuấn', 881686800, N'0133665452     ', 0, N'555555555')
SET IDENTITY_INSERT [dbo].[KhachHang] OFF
INSERT [dbo].[LoaiNhanVien] ([id], [loaiNV]) VALUES (1, N'Nhân viên ')
INSERT [dbo].[LoaiNhanVien] ([id], [loaiNV]) VALUES (2, N'Quản lý   ')
SET IDENTITY_INSERT [dbo].[NhanVien] ON 

INSERT [dbo].[NhanVien] ([id], [maNV], [idLoaiNV], [hoTen], [ngaySinh], [matKhau], [soThangKinhNghiem], [cmnd], [soDienThoai]) VALUES (4, N'ABCDEF0123', 2, N'Nguyễn Quản Lý', 887414400, N'202CB962AC59075B964B07152D234B70', CAST(18 AS Numeric(18, 0)), N'123456789', N'01234567890')
INSERT [dbo].[NhanVien] ([id], [maNV], [idLoaiNV], [hoTen], [ngaySinh], [matKhau], [soThangKinhNghiem], [cmnd], [soDienThoai]) VALUES (5, N'nhanvien1 ', 2, N'Trần Nhân Viên', 887414400, N'202CB962AC59075B964B07152D234B70', CAST(18 AS Numeric(18, 0)), N'1234567462', N'01234427890')
INSERT [dbo].[NhanVien] ([id], [maNV], [idLoaiNV], [hoTen], [ngaySinh], [matKhau], [soThangKinhNghiem], [cmnd], [soDienThoai]) VALUES (6, N'nhanvien2 ', 2, N'Phan Thị Nhân Viên Hai', 887414400, N'202CB962AC59075B964B07152D234B70', CAST(19 AS Numeric(18, 0)), N'1234167462', N'02334427890')
SET IDENTITY_INSERT [dbo].[NhanVien] OFF
SET IDENTITY_INSERT [dbo].[SanPham] ON 

INSERT [dbo].[SanPham] ([id], [maSanPham], [tenSanPham], [gia], [thongTin]) VALUES (1001, N'CBCDEF0123', N'Cà phê', 45000.0000, N'Cà phê chồn')
INSERT [dbo].[SanPham] ([id], [maSanPham], [tenSanPham], [gia], [thongTin]) VALUES (1002, N'MzmZo1zAR ', N'Sữa tươi trân châu đường đen', 42000.0000, N'S?a tuoi, trân châu')
INSERT [dbo].[SanPham] ([id], [maSanPham], [tenSanPham], [gia], [thongTin]) VALUES (1003, N'MO0QSyuUa ', N'Trà sữa thái xanh', 40000.0000, N'Trà, s?a, b?t thái xanh')
INSERT [dbo].[SanPham] ([id], [maSanPham], [tenSanPham], [gia], [thongTin]) VALUES (1004, N'Mr8l0XyHc ', N'Trà sữa thái đỏ', 42000.0000, N'Trà, s?a, b?t thái d?')
INSERT [dbo].[SanPham] ([id], [maSanPham], [tenSanPham], [gia], [thongTin]) VALUES (1005, N'M2l4HDynN ', N'Trà sữa truyền thống', 38000.0000, N'Trà, s?a')
INSERT [dbo].[SanPham] ([id], [maSanPham], [tenSanPham], [gia], [thongTin]) VALUES (1006, N'MeE5Xpq8G ', N'Trà sữa sô cô la', 40000.0000, N'Trà, s?a, sô cô la')
INSERT [dbo].[SanPham] ([id], [maSanPham], [tenSanPham], [gia], [thongTin]) VALUES (1007, N'Mk8uxjMg8 ', N'Trà sữa kim cương đen', 39000.0000, N'Trà, S?a, Trân châu')
INSERT [dbo].[SanPham] ([id], [maSanPham], [tenSanPham], [gia], [thongTin]) VALUES (1008, N'MyfJaQgkW ', N'Trà sữa dâu', 57000.0000, N'Trà, Sữa, Dâu')
INSERT [dbo].[SanPham] ([id], [maSanPham], [tenSanPham], [gia], [thongTin]) VALUES (1009, N'MGNRX4weq ', N'Trà sữa việt quất', 42000.0000, N'Trà, Sữa, Việt Quất')
INSERT [dbo].[SanPham] ([id], [maSanPham], [tenSanPham], [gia], [thongTin]) VALUES (1010, N'MI1ijYOmz ', N'Trà sữa Caramel', 45000.0000, N'Trà, Sữa')
INSERT [dbo].[SanPham] ([id], [maSanPham], [tenSanPham], [gia], [thongTin]) VALUES (1011, N'MmQfaQ2cv ', N'Trà sữa Kiwi', 46000.0000, N'Trà, Sữa, Kiwi')
INSERT [dbo].[SanPham] ([id], [maSanPham], [tenSanPham], [gia], [thongTin]) VALUES (1013, N'CCrJlejgM ', N'Bạc xỉu', 35000.0000, N'Cà phê, sữa, đường, đá')
INSERT [dbo].[SanPham] ([id], [maSanPham], [tenSanPham], [gia], [thongTin]) VALUES (1014, N'CgGZfWUBr ', N'Cà phê đen đá', 39000.0000, N'Cà phê')
INSERT [dbo].[SanPham] ([id], [maSanPham], [tenSanPham], [gia], [thongTin]) VALUES (1015, N'Ci0mDTyXT ', N'Cà phê sữa đá', 37000.0000, N'Cà phê, Sữa, Đá')
INSERT [dbo].[SanPham] ([id], [maSanPham], [tenSanPham], [gia], [thongTin]) VALUES (1016, N'CL089YPwT ', N'Capuchino', 70000.0000, N'Cà phê')
INSERT [dbo].[SanPham] ([id], [maSanPham], [tenSanPham], [gia], [thongTin]) VALUES (1017, N'CfADiZ6Ox ', N'Latte', 90000.0000, N'')
INSERT [dbo].[SanPham] ([id], [maSanPham], [tenSanPham], [gia], [thongTin]) VALUES (1018, N'CrwQhrdqX ', N'Cà phê nhiều sữa', 34000.0000, N'')
INSERT [dbo].[SanPham] ([id], [maSanPham], [tenSanPham], [gia], [thongTin]) VALUES (1019, N'CO6EXPNy6 ', N'Cà phê hoàng gia', 100000.0000, N'')
INSERT [dbo].[SanPham] ([id], [maSanPham], [tenSanPham], [gia], [thongTin]) VALUES (1020, N'CgDP5WjHQ ', N'Cà phê chồn', 65000.0000, N'')
INSERT [dbo].[SanPham] ([id], [maSanPham], [tenSanPham], [gia], [thongTin]) VALUES (1021, N'Cqr858Oco ', N'Cà phê Nhật', 90000.0000, N'')
INSERT [dbo].[SanPham] ([id], [maSanPham], [tenSanPham], [gia], [thongTin]) VALUES (1022, N'TPFigd6up ', N'Phô mai', 5000.0000, N'')
INSERT [dbo].[SanPham] ([id], [maSanPham], [tenSanPham], [gia], [thongTin]) VALUES (1023, N'TrsaBrzbf ', N'Pudding', 5000.0000, N'')
INSERT [dbo].[SanPham] ([id], [maSanPham], [tenSanPham], [gia], [thongTin]) VALUES (1024, N'TBQbntXZH ', N'Pudding trứng', 5000.0000, N'')
INSERT [dbo].[SanPham] ([id], [maSanPham], [tenSanPham], [gia], [thongTin]) VALUES (1025, N'TT6rvrYf7 ', N'Trân châu', 5000.0000, N'')
INSERT [dbo].[SanPham] ([id], [maSanPham], [tenSanPham], [gia], [thongTin]) VALUES (1026, N'Tt9QNVOaU ', N'Trân châu hoàng kim', 5000.0000, N'')
INSERT [dbo].[SanPham] ([id], [maSanPham], [tenSanPham], [gia], [thongTin]) VALUES (1027, N'TVzD0gpJ8 ', N'Trân châu sợi', 5000.0000, N'')
INSERT [dbo].[SanPham] ([id], [maSanPham], [tenSanPham], [gia], [thongTin]) VALUES (1028, N'TRhYEoXzW ', N'Rau câu', 5000.0000, N'')
INSERT [dbo].[SanPham] ([id], [maSanPham], [tenSanPham], [gia], [thongTin]) VALUES (1029, N'ToHOacq7R ', N'Thạch phô mai', 5000.0000, N'')
INSERT [dbo].[SanPham] ([id], [maSanPham], [tenSanPham], [gia], [thongTin]) VALUES (1030, N'TxV3cyTsd ', N'Thạch trái cây', 5000.0000, N'')
INSERT [dbo].[SanPham] ([id], [maSanPham], [tenSanPham], [gia], [thongTin]) VALUES (1031, N'Tu0DzghAK ', N'Thạch củ năng', 5000.0000, N'')
SET IDENTITY_INSERT [dbo].[SanPham] OFF
SET ANSI_PADDING ON
GO
/****** Object:  Index [UN_maHoaDon]    Script Date: 04/01/2019 12:26:39 CH ******/
ALTER TABLE [dbo].[HoaDon] ADD  CONSTRAINT [UN_maHoaDon] UNIQUE NONCLUSTERED 
(
	[maHoaDon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UN_maKH]    Script Date: 04/01/2019 12:26:39 CH ******/
ALTER TABLE [dbo].[KhachHang] ADD  CONSTRAINT [UN_maKH] UNIQUE NONCLUSTERED 
(
	[maKH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UN_loaiNV]    Script Date: 04/01/2019 12:26:39 CH ******/
ALTER TABLE [dbo].[LoaiNhanVien] ADD  CONSTRAINT [UN_loaiNV] UNIQUE NONCLUSTERED 
(
	[loaiNV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UN_maNV]    Script Date: 04/01/2019 12:26:39 CH ******/
ALTER TABLE [dbo].[NhanVien] ADD  CONSTRAINT [UN_maNV] UNIQUE NONCLUSTERED 
(
	[maNV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UN_maSanPham]    Script Date: 04/01/2019 12:26:39 CH ******/
ALTER TABLE [dbo].[SanPham] ADD  CONSTRAINT [UN_maSanPham] UNIQUE NONCLUSTERED 
(
	[maSanPham] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ChiTietHoaDon] ADD  CONSTRAINT [DF_ChiTietHoaDon_soLuong]  DEFAULT ((0)) FOR [soLuong]
GO
ALTER TABLE [dbo].[ChiTietHoaDon] ADD  CONSTRAINT [DF_ChiTietHoaDon_size]  DEFAULT ('S') FOR [size]
GO
ALTER TABLE [dbo].[KhachHang] ADD  CONSTRAINT [DF_KhachHang_diemTichLuy]  DEFAULT ((10)) FOR [diemTichLuy]
GO
ALTER TABLE [dbo].[KhachHang] ADD  CONSTRAINT [DF_KhachHang_cmnd]  DEFAULT ((310475602)) FOR [cmnd]
GO
ALTER TABLE [dbo].[NhanVien] ADD  CONSTRAINT [DF_NhanVien_soThangKinhNghiem]  DEFAULT ((0)) FOR [soThangKinhNghiem]
GO
ALTER TABLE [dbo].[NhanVien] ADD  CONSTRAINT [DF_NhanVien_cmnd]  DEFAULT (N'123456789') FOR [cmnd]
GO
ALTER TABLE [dbo].[NhanVien] ADD  CONSTRAINT [DF_NhanVien_soDienThoai]  DEFAULT (N'01234567890') FOR [soDienThoai]
GO
ALTER TABLE [dbo].[ChiTietHoaDon]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietHoaDon_HoaDon] FOREIGN KEY([idHoaDon])
REFERENCES [dbo].[HoaDon] ([id])
GO
ALTER TABLE [dbo].[ChiTietHoaDon] CHECK CONSTRAINT [FK_ChiTietHoaDon_HoaDon]
GO
ALTER TABLE [dbo].[ChiTietHoaDon]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietHoaDon_SanPham] FOREIGN KEY([idSanPham])
REFERENCES [dbo].[SanPham] ([id])
GO
ALTER TABLE [dbo].[ChiTietHoaDon] CHECK CONSTRAINT [FK_ChiTietHoaDon_SanPham]
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_KhachHang] FOREIGN KEY([idKhachHangMua])
REFERENCES [dbo].[KhachHang] ([id])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_KhachHang]
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_NhanVien] FOREIGN KEY([idNhanVienLap])
REFERENCES [dbo].[NhanVien] ([id])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_NhanVien]
GO
ALTER TABLE [dbo].[NhanVien]  WITH CHECK ADD  CONSTRAINT [FK_NhanVien_LoaiNhanVien] FOREIGN KEY([idLoaiNV])
REFERENCES [dbo].[LoaiNhanVien] ([id])
GO
ALTER TABLE [dbo].[NhanVien] CHECK CONSTRAINT [FK_NhanVien_LoaiNhanVien]
GO
