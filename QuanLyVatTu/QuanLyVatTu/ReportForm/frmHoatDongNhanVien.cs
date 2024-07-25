using DevExpress.CodeParser;
using DevExpress.XtraPrinting.Native;
using DevExpress.XtraReports.UI;
using QuanLyVatTu.SubForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyVatTu.ReportForm
{
    public partial class frmHoatDongNhanVien : Form
    {
        public frmHoatDongNhanVien()
        {
            InitializeComponent();
            Program.dangTaoTaiKhoan = false;
        }

        private void btnXemTruoc_Click(object sender, EventArgs e)
        {            
            if (txtMaNV.Text.Equals(""))
            {
                MessageBox.Show("Vui lòng chọn Nhân viên", "Thông Báo", MessageBoxButtons.OK);
            }
            else if (dteTuNgay.Text.Equals("") || dteToiNgay.Text.Equals(""))
            {
                MessageBox.Show("Vui lòng chọn ngày bắt đầu và ngày kết thúc", "Thông Báo", MessageBoxButtons.OK);
            }
            else if (dteTuNgay.DateTime > dteToiNgay.DateTime)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc", "Thông Báo", MessageBoxButtons.OK);
            }
            //else if (dteToiNgay.DateTime > DateTime.Now)
            //{
            //    dteToiNgay.Text = DateTime.Now.ToString("MM/dd/yyyy");
            //}
            else
            {
                string maNhanVien = txtMaNV.Text;
                DateTime fromDate = dteTuNgay.DateTime;
                DateTime toDate = dteToiNgay.DateTime;
                rptHoatDongNhanVien report = new rptHoatDongNhanVien(maNhanVien, fromDate, toDate);
                report.txtHoTenNV.Text = Program.hoTen;
                report.txtTuNgay.Text = fromDate.ToString("dd/MM/yyyy");
                report.txtDenNgay.Text = toDate.ToString("dd/MM/yyyy");
                report.txtNguoiLapBaoCao.Text = Program.staff;
                report.txtNgayLapBaoCao.Text = DateTime.Now.ToString("dd/MM/yyyy");
                ReportPrintTool printTool = new ReportPrintTool(report);
                printTool.ShowPreviewDialog();
            }
        }

        private void btnChonNhanVien_Click(object sender, EventArgs e)
        {
            frmChonNhanVien form = new frmChonNhanVien();
            form.ShowDialog();

            txtMaNV.Text = Program.maNhanVienDuocChon;
            txtHoTen.Text = Program.hoTen;
        }

        private void frmHoatDongNhanVien_Load(object sender, EventArgs e)
        {
            dteTuNgay.EditValue = "08/16/2003";
            dteToiNgay.EditValue = DateTime.Now.ToString("MM/dd/yyyy");
        }

        private void btnXuatBan_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaNV.Text.Equals(""))
                {
                    MessageBox.Show("Vui lòng chọn Nhân viên", "Thông báo", MessageBoxButtons.OK);
                }
                else if (dteTuNgay.Text.Equals("") || dteToiNgay.Text.Equals(""))
                {
                    MessageBox.Show("Vui lòng chọn ngày bắt đầu và ngày kết thúc", "Thông Báo", MessageBoxButtons.OK);
                }
                else if (dteTuNgay.DateTime > dteToiNgay.DateTime)
                {
                    MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc", "Thông Báo", MessageBoxButtons.OK);
                }
                else
                {
                    string maNhanVien = txtMaNV.Text;
                    DateTime fromDate = dteTuNgay.DateTime;
                    DateTime toDate = dteToiNgay.DateTime;
                    rptHoatDongNhanVien report = new rptHoatDongNhanVien(maNhanVien, fromDate, toDate);
                    report.txtHoTenNV.Text = Program.hoTen + " - " + maNhanVien;
                    report.txtTuNgay.Text = fromDate.ToString("dd/MM/yyyy");
                    report.txtDenNgay.Text = toDate.ToString("dd/MM/yyyy");
                    report.txtNguoiLapBaoCao.Text = Program.staff;
                    report.txtNgayLapBaoCao.Text = DateTime.Now.ToString("dd/MM/yyyy");

                    string filePath = @"D:\Github\PTIT-Co-So-Du-Lieu-Phan-Tan-De-Tai-Quan-Ly-Vat-Tu\ReportFiles\ReportHoatDongNhanVien.pdf";

                    if (File.Exists(filePath))
                    {
                        DialogResult dr = MessageBox.Show("File ReportHoatDongNhanVien.pdf tại folder ReportFiles đã có!\nBạn có muốn tạo lại?",
                            "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dr == DialogResult.Yes)
                        {
                            report.ExportToPdf(filePath);
                            MessageBox.Show("File ReportHoatDongNhanVien.pdf đã được ghi thành công tại folder ReportFiles",
                                "Xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Program.OpenPdf(filePath);
                        }
                    }
                    else
                    {
                        report.ExportToPdf(filePath);
                        MessageBox.Show("File ReportHoatDongNhanVien.pdf đã được ghi thành công tại folder ReportFiles",
                            "Xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Program.OpenPdf(filePath);
                    }
                }

            }
            catch (IOException ex)
            {
                MessageBox.Show("Vui lòng đóng file ReportHoatDongNhanVien.pdf",
                    "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }
        }

        private void lblToiNgay_Click(object sender, EventArgs e)
        {

        }
    }
}
