namespace QuanLyVatTu.ReportForm
{
    partial class frmChiTietSoLuongTriGiaHangHoaNhapXuat
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
            this.btnXuatBan = new System.Windows.Forms.Button();
            this.btnXemTruoc = new System.Windows.Forms.Button();
            this.lblChiTietSoLuongHangHoaNhapXuat = new System.Windows.Forms.Label();
            this.lblLoaiPhieu = new System.Windows.Forms.Label();
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.lblToiNgay = new System.Windows.Forms.Label();
            this.cboLoaiPhieu = new System.Windows.Forms.ComboBox();
            this.dteTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.dteToiNgay = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteToiNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteToiNgay.Properties.CalendarTimeProperties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnXuatBan
            // 
            this.btnXuatBan.BackColor = System.Drawing.Color.DarkSalmon;
            this.btnXuatBan.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXuatBan.Location = new System.Drawing.Point(439, 308);
            this.btnXuatBan.Name = "btnXuatBan";
            this.btnXuatBan.Size = new System.Drawing.Size(196, 39);
            this.btnXuatBan.TabIndex = 12;
            this.btnXuatBan.Text = "XUẤT BẢN";
            this.btnXuatBan.UseVisualStyleBackColor = false;
            this.btnXuatBan.Click += new System.EventHandler(this.btnXuatBan_Click);
            // 
            // btnXemTruoc
            // 
            this.btnXemTruoc.BackColor = System.Drawing.Color.PaleGreen;
            this.btnXemTruoc.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXemTruoc.Location = new System.Drawing.Point(142, 308);
            this.btnXemTruoc.Name = "btnXemTruoc";
            this.btnXemTruoc.Size = new System.Drawing.Size(196, 39);
            this.btnXemTruoc.TabIndex = 11;
            this.btnXemTruoc.Text = "XEM TRƯỚC";
            this.btnXemTruoc.UseVisualStyleBackColor = false;
            this.btnXemTruoc.Click += new System.EventHandler(this.btnXemTruoc_Click);
            // 
            // lblChiTietSoLuongHangHoaNhapXuat
            // 
            this.lblChiTietSoLuongHangHoaNhapXuat.AutoSize = true;
            this.lblChiTietSoLuongHangHoaNhapXuat.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChiTietSoLuongHangHoaNhapXuat.Location = new System.Drawing.Point(137, 58);
            this.lblChiTietSoLuongHangHoaNhapXuat.Name = "lblChiTietSoLuongHangHoaNhapXuat";
            this.lblChiTietSoLuongHangHoaNhapXuat.Size = new System.Drawing.Size(498, 25);
            this.lblChiTietSoLuongHangHoaNhapXuat.TabIndex = 10;
            this.lblChiTietSoLuongHangHoaNhapXuat.Text = "CHI TIẾT SỐ LƯỢNG HÀNG HÓA NHẬP XUẤT";
            // 
            // lblLoaiPhieu
            // 
            this.lblLoaiPhieu.AutoSize = true;
            this.lblLoaiPhieu.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoaiPhieu.Location = new System.Drawing.Point(102, 146);
            this.lblLoaiPhieu.Name = "lblLoaiPhieu";
            this.lblLoaiPhieu.Size = new System.Drawing.Size(89, 21);
            this.lblLoaiPhieu.TabIndex = 13;
            this.lblLoaiPhieu.Text = "Loại Phiếu";
            // 
            // lblTuNgay
            // 
            this.lblTuNgay.AutoSize = true;
            this.lblTuNgay.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTuNgay.Location = new System.Drawing.Point(102, 227);
            this.lblTuNgay.Name = "lblTuNgay";
            this.lblTuNgay.Size = new System.Drawing.Size(76, 21);
            this.lblTuNgay.TabIndex = 14;
            this.lblTuNgay.Text = "Từ Ngày";
            this.lblTuNgay.Click += new System.EventHandler(this.lblTuNgay_Click);
            // 
            // lblToiNgay
            // 
            this.lblToiNgay.AutoSize = true;
            this.lblToiNgay.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToiNgay.Location = new System.Drawing.Point(422, 227);
            this.lblToiNgay.Name = "lblToiNgay";
            this.lblToiNgay.Size = new System.Drawing.Size(80, 21);
            this.lblToiNgay.TabIndex = 15;
            this.lblToiNgay.Text = "Tới Ngày";
            this.lblToiNgay.Click += new System.EventHandler(this.lblToiNgay_Click);
            // 
            // cboLoaiPhieu
            // 
            this.cboLoaiPhieu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiPhieu.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboLoaiPhieu.FormattingEnabled = true;
            this.cboLoaiPhieu.Items.AddRange(new object[] {
            "NHAP",
            "XUAT"});
            this.cboLoaiPhieu.Location = new System.Drawing.Point(197, 138);
            this.cboLoaiPhieu.Name = "cboLoaiPhieu";
            this.cboLoaiPhieu.Size = new System.Drawing.Size(149, 29);
            this.cboLoaiPhieu.TabIndex = 16;
            // 
            // dteTuNgay
            // 
            this.dteTuNgay.EditValue = null;
            this.dteTuNgay.Location = new System.Drawing.Point(197, 220);
            this.dteTuNgay.Name = "dteTuNgay";
            this.dteTuNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 14F);
            this.dteTuNgay.Properties.Appearance.Options.UseFont = true;
            this.dteTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteTuNgay.Size = new System.Drawing.Size(149, 28);
            this.dteTuNgay.TabIndex = 17;
            this.dteTuNgay.EditValueChanged += new System.EventHandler(this.dteTuNgay_EditValueChanged);
            // 
            // dteToiNgay
            // 
            this.dteToiNgay.EditValue = null;
            this.dteToiNgay.Location = new System.Drawing.Point(518, 220);
            this.dteToiNgay.Name = "dteToiNgay";
            this.dteToiNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 14F);
            this.dteToiNgay.Properties.Appearance.Options.UseFont = true;
            this.dteToiNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteToiNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteToiNgay.Size = new System.Drawing.Size(149, 28);
            this.dteToiNgay.TabIndex = 18;
            this.dteToiNgay.EditValueChanged += new System.EventHandler(this.dteToiNgay_EditValueChanged);
            // 
            // frmChiTietSoLuongTriGiaHangHoaNhapXuat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dteToiNgay);
            this.Controls.Add(this.dteTuNgay);
            this.Controls.Add(this.cboLoaiPhieu);
            this.Controls.Add(this.lblToiNgay);
            this.Controls.Add(this.lblTuNgay);
            this.Controls.Add(this.lblLoaiPhieu);
            this.Controls.Add(this.btnXuatBan);
            this.Controls.Add(this.btnXemTruoc);
            this.Controls.Add(this.lblChiTietSoLuongHangHoaNhapXuat);
            this.Name = "frmChiTietSoLuongTriGiaHangHoaNhapXuat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CHI TIẾT SỐ LƯỢNG HÀNG HÓA NHẬP XUẤT";
            this.Load += new System.EventHandler(this.frmChiTietSoLuongTriGiaHangHoaNhapXuat_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dteTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteToiNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteToiNgay.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnXuatBan;
        private System.Windows.Forms.Button btnXemTruoc;
        private System.Windows.Forms.Label lblChiTietSoLuongHangHoaNhapXuat;
        private System.Windows.Forms.Label lblLoaiPhieu;
        private System.Windows.Forms.Label lblTuNgay;
        private System.Windows.Forms.Label lblToiNgay;
        private System.Windows.Forms.ComboBox cboLoaiPhieu;
        private DevExpress.XtraEditors.DateEdit dteTuNgay;
        private DevExpress.XtraEditors.DateEdit dteToiNgay;
    }
}