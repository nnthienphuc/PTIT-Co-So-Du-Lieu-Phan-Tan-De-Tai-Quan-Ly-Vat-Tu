using DevExpress.XtraReports.UI;
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
    public partial class frmChiTietSoLuongTriGiaHangHoaNhapXuat : Form
    {
        public frmChiTietSoLuongTriGiaHangHoaNhapXuat()
        {
            InitializeComponent();
        }

        private void dteTuNgay_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void lblTuNgay_Click(object sender, EventArgs e)
        {

        }

        private void lblToiNgay_Click(object sender, EventArgs e)
        {

        }

        private void dteToiNgay_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void frmChiTietSoLuongTriGiaHangHoaNhapXuat_Load(object sender, EventArgs e)
        {
            this.cboLoaiPhieu.SelectedIndex = 1;
            this.dteTuNgay.EditValue = "01/01/2017";
            this.dteToiNgay.EditValue = DateTime.Now.ToString("MM/dd/yyyy");

        }

        private void btnXemTruoc_Click(object sender, EventArgs e)
        {
            if (dteTuNgay.Text.Equals("") || dteToiNgay.Text.Equals(""))
            {
                MessageBox.Show("Vui lòng chọn ngày bắt đầu và ngày kết thúc", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (dteTuNgay.DateTime > dteToiNgay.DateTime)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //else if (dteToiNgay.DateTime > DateTime.Now)
            //{
            //    dteToiNgay.Text = DateTime.Now.ToString("MM/dd/yyyy");
            //}
            else
            {
                string vaiTro = Program.role;
                string loaiPhieu = (cboLoaiPhieu.SelectedItem.ToString() == "NHAP") ? "NHAP" : "XUAT";

                DateTime fromDate = dteTuNgay.DateTime;
                DateTime toDate = dteToiNgay.DateTime;
                rptChiTietSoLuongTriGiaHangHoaNhapXuat report = new rptChiTietSoLuongTriGiaHangHoaNhapXuat(vaiTro, loaiPhieu, fromDate, toDate);
                /*GAN TEN CHI NHANH CHO BAO CAO*/
                report.txtLoaiPhieu.Text = cboLoaiPhieu.SelectedItem.ToString().ToUpper();
                report.txtTuNgay.Text = fromDate.ToString("dd-MM-yyyy");
                report.txtToiNgay.Text = toDate.ToString("dd-MM-yyyy");
                report.txtNguoiLap.Text = Program.staff;
                report.txtNgayLap.Text = DateTime.Now.ToString("MM/dd/yyyy");
                ReportPrintTool printTool = new ReportPrintTool(report);
                printTool.ShowPreviewDialog();
            }
        }

        private void btnXuatBan_Click(object sender, EventArgs e)
        {
            string vaiTro = Program.role;
            string loaiPhieu = (cboLoaiPhieu.SelectedItem.ToString() == "NHAP") ? "NHAP" : "XUAT";

            try
            {
                if (dteTuNgay.Text.Equals("") || dteToiNgay.Text.Equals(""))
                {
                    MessageBox.Show("Vui lòng chọn ngày bắt đầu và ngày kết thúc", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (dteTuNgay.DateTime > dteToiNgay.DateTime)
                {
                    MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //else if (dteToiNgay.DateTime > DateTime.Now)
                //{
                //    dteToiNgay.Text = DateTime.Now.ToString("MM/dd/yyyy");
                //}
                else
                {
                    DateTime fromDate = dteTuNgay.DateTime;
                    DateTime toDate = dteToiNgay.DateTime;

                    rptChiTietSoLuongTriGiaHangHoaNhapXuat report = new rptChiTietSoLuongTriGiaHangHoaNhapXuat(vaiTro, loaiPhieu, fromDate, toDate);
                    report.txtLoaiPhieu.Text = cboLoaiPhieu.SelectedItem.ToString().ToUpper();
                    report.txtTuNgay.Text = fromDate.ToString("dd-MM-yyyy");
                    report.txtToiNgay.Text = toDate.ToString("dd-MM-yyyy");
                    report.txtNguoiLap.Text = Program.staff;
                    report.txtNgayLap.Text = DateTime.Now.ToString("MM/dd/yyyy");

                    string filePath = @"D:\Github\PTIT-Co-So-Du-Lieu-Phan-Tan-De-Tai-Quan-Ly-Vat-Tu\ReportFiles\ReportChiTietSoLuongTriGiaHangHoaNhapXuat.pdf";

                    if (File.Exists(filePath))
                    {
                        DialogResult dr = MessageBox.Show("File ReportChiTietSoLuongTriGiaHangHoaNhapXuat.pdf tại folder ReportFiles đã có!\nBạn có muốn tạo lại?",
                            "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dr == DialogResult.Yes)
                        {
                            report.ExportToPdf(filePath);
                            MessageBox.Show("File ReportChiTietSoLuongTriGiaHangHoaNhapXuat.pdf đã được ghi thành công tại folder ReportFiles",
                                "Xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Program.OpenPdf(filePath);
                        }
                    }
                    else
                    {
                        report.ExportToPdf(filePath);
                        MessageBox.Show("File ReportChiTietSoLuongTriGiaHangHoaNhapXuat.pdf đã được ghi thành công tại folder ReportFiles",
                            "Xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Program.OpenPdf(filePath);
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("Vui lòng đóng file ReportChiTietSoLuongTriGiaHangHoaNhapXuat.pdf",
                    "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
        }
    }
}
