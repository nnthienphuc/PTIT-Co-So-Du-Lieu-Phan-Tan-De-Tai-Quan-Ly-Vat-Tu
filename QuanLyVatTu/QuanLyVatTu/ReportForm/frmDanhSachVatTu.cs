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
    public partial class frmDanhSachVatTu : Form
    {
        public frmDanhSachVatTu()
        {
            InitializeComponent();
        }

        private void vattuBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.vattuBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dataSet);
        }

        private void frmDanhSachVatTu_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet.Vattu' table. You can move, or remove it, as needed.
            this.vattuTableAdapter.Fill(this.dataSet.Vattu);

        }

        private void btnXemTruoc_Click(object sender, EventArgs e)
        {
            rptDanhSachVatTu report = new rptDanhSachVatTu();
            report.txtNguoiLap.Text = Program.staff;
            report.txtNgayLap.Text = DateTime.Now.ToString("MM/dd/yyyy");
            ReportPrintTool printTool = new ReportPrintTool(report);
            printTool.ShowPreviewDialog();
        }

        private void btnXuatBan_Click(object sender, EventArgs e)
        {
            rptDanhSachVatTu report = new rptDanhSachVatTu();
            report.txtNguoiLap.Text = Program.staff;
            report.txtNgayLap.Text = DateTime.Now.ToString("MM/dd/yyyy");
            string filePath = @"D:\Github\PTIT-Co-So-Du-Lieu-Phan-Tan-De-Tai-Quan-Ly-Vat-Tu\ReportFiles\ReportDanhSachVatTu.pdf";

            try
            {
                if (File.Exists(filePath))
                {
                    DialogResult dr = MessageBox.Show("File ReportDanhSachVatTu.pdf tại folder ReportFiles đã có!\nBạn có muốn tạo lại?",
                        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Yes)
                    {
                        report.ExportToPdf(filePath);
                        MessageBox.Show("File ReportDanhSachVatTu.pdf đã được ghi thành công tại folder ReportFiles",
                            "Xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Program.OpenPdf(filePath);
                    }
                }
                else
                {
                    report.ExportToPdf(filePath);
                    MessageBox.Show("File ReportDanhSachVatTu.pdf đã được ghi thành công tại folder ReportFiles",
                        "Xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Program.OpenPdf(filePath);
                }
                this.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show("Vui lòng đóng file ReportDanhSachVatTu.pdf",
                    "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
        }
    }
}
