namespace QuanLyVatTu.ReportForm
{
    partial class frmDonHangKhongPhieuNhap
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
            this.lblDonHangKhongPhieuNhap = new System.Windows.Forms.Label();
            this.cboChiNhanh = new System.Windows.Forms.ComboBox();
            this.lblChiNhanh = new System.Windows.Forms.Label();
            this.btnXuatBan = new System.Windows.Forms.Button();
            this.btnXemTruoc = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblDonHangKhongPhieuNhap
            // 
            this.lblDonHangKhongPhieuNhap.AutoSize = true;
            this.lblDonHangKhongPhieuNhap.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDonHangKhongPhieuNhap.Location = new System.Drawing.Point(96, 45);
            this.lblDonHangKhongPhieuNhap.Name = "lblDonHangKhongPhieuNhap";
            this.lblDonHangKhongPhieuNhap.Size = new System.Drawing.Size(369, 25);
            this.lblDonHangKhongPhieuNhap.TabIndex = 0;
            this.lblDonHangKhongPhieuNhap.Text = "ĐƠN HÀNG KHÔNG PHIẾU NHẬP";
            // 
            // cboChiNhanh
            // 
            this.cboChiNhanh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChiNhanh.Enabled = false;
            this.cboChiNhanh.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboChiNhanh.FormattingEnabled = true;
            this.cboChiNhanh.Location = new System.Drawing.Point(159, 108);
            this.cboChiNhanh.Name = "cboChiNhanh";
            this.cboChiNhanh.Size = new System.Drawing.Size(342, 28);
            this.cboChiNhanh.TabIndex = 4;
            this.cboChiNhanh.SelectedIndexChanged += new System.EventHandler(this.cboChiNhanh_SelectedIndexChanged);
            // 
            // lblChiNhanh
            // 
            this.lblChiNhanh.AutoSize = true;
            this.lblChiNhanh.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChiNhanh.Location = new System.Drawing.Point(41, 115);
            this.lblChiNhanh.Name = "lblChiNhanh";
            this.lblChiNhanh.Size = new System.Drawing.Size(89, 21);
            this.lblChiNhanh.TabIndex = 3;
            this.lblChiNhanh.Text = "Chi Nhánh";
            // 
            // btnXuatBan
            // 
            this.btnXuatBan.BackColor = System.Drawing.Color.DarkSalmon;
            this.btnXuatBan.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXuatBan.Location = new System.Drawing.Point(305, 187);
            this.btnXuatBan.Name = "btnXuatBan";
            this.btnXuatBan.Size = new System.Drawing.Size(196, 39);
            this.btnXuatBan.TabIndex = 9;
            this.btnXuatBan.Text = "XUẤT BẢN";
            this.btnXuatBan.UseVisualStyleBackColor = false;
            this.btnXuatBan.Click += new System.EventHandler(this.btnXuatBan_Click);
            // 
            // btnXemTruoc
            // 
            this.btnXemTruoc.BackColor = System.Drawing.Color.PaleGreen;
            this.btnXemTruoc.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXemTruoc.Location = new System.Drawing.Point(42, 187);
            this.btnXemTruoc.Name = "btnXemTruoc";
            this.btnXemTruoc.Size = new System.Drawing.Size(196, 39);
            this.btnXemTruoc.TabIndex = 8;
            this.btnXemTruoc.Text = "XEM TRƯỚC";
            this.btnXemTruoc.UseVisualStyleBackColor = false;
            this.btnXemTruoc.Click += new System.EventHandler(this.btnXemTruoc_Click);
            // 
            // frmDonHangKhongPhieuNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 291);
            this.Controls.Add(this.btnXuatBan);
            this.Controls.Add(this.btnXemTruoc);
            this.Controls.Add(this.cboChiNhanh);
            this.Controls.Add(this.lblChiNhanh);
            this.Controls.Add(this.lblDonHangKhongPhieuNhap);
            this.Name = "frmDonHangKhongPhieuNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ĐƠN HÀNG KHÔNG PHIẾU NHẬP";
            this.Load += new System.EventHandler(this.frmDonHangKhongPhieuNhap_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDonHangKhongPhieuNhap;
        private System.Windows.Forms.ComboBox cboChiNhanh;
        private System.Windows.Forms.Label lblChiNhanh;
        private System.Windows.Forms.Button btnXuatBan;
        private System.Windows.Forms.Button btnXemTruoc;
    }
}