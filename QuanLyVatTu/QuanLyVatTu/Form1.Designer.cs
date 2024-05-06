namespace QuanLyVatTu
{
    partial class frmChinh
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChinh));
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.pageNhapXuat = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.rbnNhapXuat = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.btnNhanVien = new DevExpress.XtraBars.BarButtonItem();
            this.btnVatTu = new DevExpress.XtraBars.BarButtonItem();
            this.btnKho = new DevExpress.XtraBars.BarButtonItem();
            this.btnLapPhieu = new DevExpress.XtraBars.BarSubItem();
            this.pageBaoCao = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.rbnBaoCao = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.btnDanhSachNhanVien = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.btnDonDatHang = new DevExpress.XtraBars.BarButtonItem();
            this.btnPhieuNhap = new DevExpress.XtraBars.BarButtonItem();
            this.btnPhieuXuat = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.EmptyAreaImageOptions.ImagePadding = new System.Windows.Forms.Padding(40, 39, 40, 39);
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.btnNhanVien,
            this.btnVatTu,
            this.btnKho,
            this.btnLapPhieu,
            this.btnDanhSachNhanVien,
            this.barButtonItem2,
            this.barButtonItem3,
            this.barButtonItem4,
            this.barButtonItem5,
            this.barButtonItem6,
            this.btnDonDatHang,
            this.btnPhieuNhap,
            this.btnPhieuXuat});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ribbonControl1.MaxItemId = 14;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.OptionsMenuMinWidth = 440;
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.pageNhapXuat,
            this.pageBaoCao});
            this.ribbonControl1.Size = new System.Drawing.Size(1298, 158);
            // 
            // pageNhapXuat
            // 
            this.pageNhapXuat.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.rbnNhapXuat});
            this.pageNhapXuat.Name = "pageNhapXuat";
            this.pageNhapXuat.Text = "NHẬP XUẤT";
            // 
            // rbnNhapXuat
            // 
            this.rbnNhapXuat.ItemLinks.Add(this.btnNhanVien);
            this.rbnNhapXuat.ItemLinks.Add(this.btnVatTu);
            this.rbnNhapXuat.ItemLinks.Add(this.btnKho);
            this.rbnNhapXuat.ItemLinks.Add(this.btnLapPhieu);
            this.rbnNhapXuat.Name = "rbnNhapXuat";
            this.rbnNhapXuat.Text = "QUẢN LÝ NHẬP XUẤT";
            // 
            // btnNhanVien
            // 
            this.btnNhanVien.Caption = "Nhân Viên";
            this.btnNhanVien.Id = 1;
            this.btnNhanVien.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnNhanVien.ImageOptions.SvgImage")));
            this.btnNhanVien.Name = "btnNhanVien";
            // 
            // btnVatTu
            // 
            this.btnVatTu.Caption = "Vật Tư";
            this.btnVatTu.Id = 2;
            this.btnVatTu.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnVatTu.ImageOptions.SvgImage")));
            this.btnVatTu.Name = "btnVatTu";
            // 
            // btnKho
            // 
            this.btnKho.Caption = "Kho Hàng";
            this.btnKho.Id = 3;
            this.btnKho.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnKho.ImageOptions.SvgImage")));
            this.btnKho.Name = "btnKho";
            // 
            // btnLapPhieu
            // 
            this.btnLapPhieu.Caption = "Lập Phiếu";
            this.btnLapPhieu.Id = 4;
            this.btnLapPhieu.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnLapPhieu.ImageOptions.SvgImage")));
            this.btnLapPhieu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnDonDatHang),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnPhieuNhap),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnPhieuXuat)});
            this.btnLapPhieu.Name = "btnLapPhieu";
            // 
            // pageBaoCao
            // 
            this.pageBaoCao.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.rbnBaoCao});
            this.pageBaoCao.Name = "pageBaoCao";
            this.pageBaoCao.Text = "BÁO CÁO";
            // 
            // rbnBaoCao
            // 
            this.rbnBaoCao.ItemLinks.Add(this.btnDanhSachNhanVien);
            this.rbnBaoCao.ItemLinks.Add(this.barButtonItem2);
            this.rbnBaoCao.ItemLinks.Add(this.barButtonItem3);
            this.rbnBaoCao.ItemLinks.Add(this.barButtonItem4);
            this.rbnBaoCao.ItemLinks.Add(this.barButtonItem5);
            this.rbnBaoCao.ItemLinks.Add(this.barButtonItem6);
            this.rbnBaoCao.Name = "rbnBaoCao";
            this.rbnBaoCao.Text = "QUẢN LÝ BÁO CÁO";
            // 
            // btnDanhSachNhanVien
            // 
            this.btnDanhSachNhanVien.Caption = "Danh Sách Nhân Viên";
            this.btnDanhSachNhanVien.Id = 5;
            this.btnDanhSachNhanVien.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnDanhSachNhanVien.ImageOptions.SvgImage")));
            this.btnDanhSachNhanVien.Name = "btnDanhSachNhanVien";
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "barButtonItem2";
            this.barButtonItem2.Id = 6;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "barButtonItem3";
            this.barButtonItem3.Id = 7;
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "barButtonItem4";
            this.barButtonItem4.Id = 8;
            this.barButtonItem4.Name = "barButtonItem4";
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.Caption = "barButtonItem5";
            this.barButtonItem5.Id = 9;
            this.barButtonItem5.Name = "barButtonItem5";
            // 
            // barButtonItem6
            // 
            this.barButtonItem6.Caption = "barButtonItem6";
            this.barButtonItem6.Id = 10;
            this.barButtonItem6.Name = "barButtonItem6";
            // 
            // btnDonDatHang
            // 
            this.btnDonDatHang.Caption = "Đơn Đặt Hàng";
            this.btnDonDatHang.Id = 11;
            this.btnDonDatHang.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnDonDatHang.ImageOptions.SvgImage")));
            this.btnDonDatHang.Name = "btnDonDatHang";
            // 
            // btnPhieuNhap
            // 
            this.btnPhieuNhap.Caption = "Phiếu Nhập";
            this.btnPhieuNhap.Id = 12;
            this.btnPhieuNhap.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnPhieuNhap.ImageOptions.SvgImage")));
            this.btnPhieuNhap.Name = "btnPhieuNhap";
            // 
            // btnPhieuXuat
            // 
            this.btnPhieuXuat.Caption = "Phiếu Xuất";
            this.btnPhieuXuat.Id = 13;
            this.btnPhieuXuat.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnPhieuXuat.ImageOptions.SvgImage")));
            this.btnPhieuXuat.Name = "btnPhieuXuat";
            // 
            // frmChinh
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1298, 699);
            this.Controls.Add(this.ribbonControl1);
            this.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("frmChinh.IconOptions.Image")));
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmChinh";
            this.Ribbon = this.ribbonControl1;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QUẢN LÝ VẬT TƯ";
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage pageNhapXuat;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rbnNhapXuat;
        private DevExpress.XtraBars.BarButtonItem btnNhanVien;
        private DevExpress.XtraBars.BarButtonItem btnVatTu;
        private DevExpress.XtraBars.BarButtonItem btnKho;
        private DevExpress.XtraBars.BarSubItem btnLapPhieu;
        private DevExpress.XtraBars.Ribbon.RibbonPage pageBaoCao;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rbnBaoCao;
        private DevExpress.XtraBars.BarButtonItem btnDonDatHang;
        private DevExpress.XtraBars.BarButtonItem btnPhieuNhap;
        private DevExpress.XtraBars.BarButtonItem btnPhieuXuat;
        private DevExpress.XtraBars.BarButtonItem btnDanhSachNhanVien;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private DevExpress.XtraBars.BarButtonItem barButtonItem6;
    }
}

