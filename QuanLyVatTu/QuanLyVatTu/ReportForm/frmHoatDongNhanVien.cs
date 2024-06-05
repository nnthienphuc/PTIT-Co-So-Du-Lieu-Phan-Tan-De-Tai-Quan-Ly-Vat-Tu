using DevExpress.XtraReports.UI;
using QuanLyVatTu.SubForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        }

        private void btnXemTruoc_Click(object sender, EventArgs e)
        {
            string maNhanVien = txtMaNV.Text;
            string loaiPhieu = (cboLoaiPhieu.SelectedItem.ToString() == "NHAP") ? "NHAP" : "XUAT";
            DateTime fromDate = dteTuNgay.DateTime;
            DateTime toDate = dteToiNgay.DateTime;
            rptHoatDongNhanVien1 report = new rptHoatDongNhanVien1(maNhanVien, loaiPhieu, fromDate, toDate);
            report.txtHoTenNV.Text = Program.hoTen;
            report.txtTuNgay.Text = dteTuNgay.EditValue.ToString();
            report.txtDenNgay.Text = dteToiNgay.EditValue.ToString();
            report.txtNgayLapBaoCao.Text = toDate.ToString();
            ReportPrintTool printTool = new ReportPrintTool(report);
            printTool.ShowPreviewDialog();
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
            cboLoaiPhieu.SelectedIndex = 1;
            dteTuNgay.EditValue = "01-01-2017";
            dteToiNgay.EditValue = DateTime.Today.ToString("MM-dd-yyyy");
        }

        private void btnXuatBan_Click(object sender, EventArgs e)
        {
            try
            {
                string maNhanVien = txtMaNV.Text;
                string loaiPhieu = (cboLoaiPhieu.SelectedItem.ToString() == "NHAP") ? "NHAP" : "XUAT";

                DateTime fromDate = dteTuNgay.DateTime;
                DateTime toDate = dteToiNgay.DateTime;

                rptHoatDongNhanVien1 report = new rptHoatDongNhanVien1(maNhanVien, loaiPhieu, fromDate, toDate);
                report.txtHoTenNV.Text = Program.hoTen;
                report.txtTuNgay.Text = dteTuNgay.EditValue.ToString();
                report.txtDenNgay.Text = dteToiNgay.EditValue.ToString();
                report.txtNgayLapBaoCao.Text = toDate.ToString();

                if (File.Exists(@"D:\Github\PTIT-Co-So-Du-Lieu-Phan-Tan-De-Tai-Quan-Ly-Vat-Tu\ReportFiles\ReportHoatDongNhanVien.pdf"))
                {
                    DialogResult dr = MessageBox.Show("File ReportHoatDongNhanVien.pdf tại folder ReportFiles đã có!\nBạn có muốn tạo lại?",
                        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Yes)
                    {
                        report.ExportToPdf(@"D:\Github\PTIT-Co-So-Du-Lieu-Phan-Tan-De-Tai-Quan-Ly-Vat-Tu\ReportFiles\ReportHoatDongNhanVien.pdf");
                        MessageBox.Show("File ReportHoatDongNhanVien.pdf đã được ghi thành công tại folder ReportFiles",
                "Xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    report.ExportToPdf(@"D:\Github\PTIT-Co-So-Du-Lieu-Phan-Tan-De-Tai-Quan-Ly-Vat-Tu\ReportFiles\ReportHoatDongNhanVien.pdf");
                    MessageBox.Show("File ReportHoatDongNhanVien.pdf đã được ghi thành công tại folder ReportFiles",
                "Xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("Vui lòng đóng file ReportHoatDongNhanVien.pdf",
                    "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }
        }
    }
}
