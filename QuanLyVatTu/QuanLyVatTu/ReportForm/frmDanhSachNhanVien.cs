using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyVatTu.ReportForm;
using System.IO;
using System.Diagnostics;

namespace QuanLyVatTu.ReportForm
{
    public partial class frmDanhSachNhanVien : Form
    {
        private SqlConnection connPublisher = new SqlConnection();
        private string chiNhanh = "";

        private void layDanhSachPhanManh(String cmd)
        {
            if (connPublisher.State == ConnectionState.Closed)
            {
                connPublisher.Open();
            }
            DataTable dt = new DataTable();
            // adapter dùng để đưa dữ liệu từ view sang database
            SqlDataAdapter da = new SqlDataAdapter(cmd, connPublisher);
            // dùng adapter thì mới đổ vào data table được
            da.Fill(dt);

            connPublisher.Close();
            Program.bindingSource.DataSource = dt;

            cboChiNhanh.DataSource = Program.bindingSource;
            cboChiNhanh.DisplayMember = "TENCN";
            cboChiNhanh.ValueMember = "TENSERVER";

        }
        public frmDanhSachNhanVien()
        {
            InitializeComponent();
        }

        private int KetNoiDatabaseGoc()
        {
            if (connPublisher != null && connPublisher.State == ConnectionState.Open)
                connPublisher.Close();
            try
            {
                connPublisher.ConnectionString = Program.connstrPublisher;
                connPublisher.Open();
                return 1;
            }

            catch (Exception e)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.\nBạn xem lại user name và password.\n " + e.Message, "", MessageBoxButtons.OK);
                return 0;
            }
        }

        private void frmDanhSachNhanVien_Load(object sender, EventArgs e)
        {
            if (Program.role == "CONGTY")
            {
                this.cboChiNhanh.Enabled = true;
            }
            dataSet.EnforceConstraints = false;

            if (KetNoiDatabaseGoc() == 0)
                return;

            layDanhSachPhanManh("SELECT TOP 2 * FROM view_DanhSachPhanManh");
            cboChiNhanh.SelectedIndex = 0;
            cboChiNhanh.SelectedIndex = 1;
            cboChiNhanh.SelectedIndex = Program.brand;
        }

        private void cboChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboChiNhanh.SelectedValue.ToString() == "System.Data.DataRowView")
                return;

            Program.serverName = cboChiNhanh.SelectedValue.ToString();

            if (cboChiNhanh.SelectedIndex != Program.brand)
            {
                Program.loginName = Program.remoteLogin;
                Program.loginPassword = Program.remotePassword;
            }
            else
            {
                Program.loginName = Program.currentLogin;
                Program.loginPassword = Program.currentPassword;
            }

            if (Program.KetNoi() == 1)
            {
                MessageBox.Show("Không thể kết nối tới chi nhánh hiện tại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            chiNhanh = cboChiNhanh.SelectedValue.ToString().Contains("1") ? "Chi Nhánh 1" : "Chi Nhánh 2";

            this.nhanVienTableAdapter.Connection.ConnectionString = Program.connstr;
            this.nhanVienTableAdapter.Fill(this.dataSet.NhanVien);
        }

        private void btnXemTruoc_Click(object sender, EventArgs e)
        {
            rptDanhSachNhanVien report = new rptDanhSachNhanVien();

            report.txtChiNhanh.Text = chiNhanh.ToUpper();
            report.txtNguoiLap.Text = Program.staff;
            report.txtNgayLap.Text = DateTime.Now.ToString("MM/dd/yyyy");

            ReportPrintTool printTool = new ReportPrintTool(report);
            printTool.ShowPreviewDialog();
        }

        private void btnXuatBan_Click(object sender, EventArgs e)
        {
            try
            {
                rptDanhSachNhanVien report = new rptDanhSachNhanVien();

                report.txtChiNhanh.Text = chiNhanh.ToUpper();
                report.txtNguoiLap.Text = Program.staff;
                report.txtNgayLap.Text = DateTime.Now.ToString("MM/dd/yyyy");

                string filePath;
                if (cboChiNhanh.SelectedValue.ToString().Contains("1"))
                {
                    filePath = @"D:\Github\PTIT-Co-So-Du-Lieu-Phan-Tan-Quan-Ly-Vat-Tu-2024\ReportFiles\ReportDanhSachNhanVienChiNhanh1.pdf";

                    if (File.Exists(filePath))
                    {
                        DialogResult dr = MessageBox.Show("File ReportDanhSachNhanVienChiNhanh1.pdf tại folder ReportFiles đã có!\nBạn có muốn tạo lại?",
                            "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dr == DialogResult.Yes)
                        {
                            report.ExportToPdf(filePath);
                            MessageBox.Show("File ReportDanhSachNhanVienChiNhanh1.pdf đã được ghi thành công tại folder ReportFiles",
                                "Xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Program.OpenPdf(filePath);
                        }
                    }
                    else
                    {
                        report.ExportToPdf(filePath);
                        MessageBox.Show("File ReportDanhSachNhanVienChiNhanh1.pdf đã được ghi thành công tại folder ReportFiles",
                            "Xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Program.OpenPdf(filePath);
                    }
                }
                else
                {
                    filePath = @"D:\Github\PTIT-Co-So-Du-Lieu-Phan-Tan-Quan-Ly-Vat-Tu-2024\ReportFiles\ReportDanhSachNhanVienChiNhanh2.pdf";

                    if (File.Exists(filePath))
                    {
                        DialogResult dr = MessageBox.Show("File ReportDanhSachNhanVienChiNhanh2.pdf tại folder ReportFiles đã có!\nBạn có muốn tạo lại?",
                            "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dr == DialogResult.Yes)
                        {
                            report.ExportToPdf(filePath);
                            MessageBox.Show("File ReportDanhSachNhanVienChiNhanh2.pdf đã được ghi thành công tại folder ReportFiles",
                                "Xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Program.OpenPdf(filePath);
                        }
                    }
                    else
                    {
                        report.ExportToPdf(filePath);
                        MessageBox.Show("File ReportDanhSachNhanVienChiNhanh2.pdf đã được ghi thành công tại folder ReportFiles",
                            "Xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Program.OpenPdf(filePath);
                    }
                }

                if (Program.role != "CONGTY")
                {
                    this.Close();
                }
            }
            catch (IOException ex)
            {
                if (cboChiNhanh.SelectedValue.ToString().Contains("1"))
                {
                    MessageBox.Show("Vui lòng đóng file ReportDanhSachNhanVienChiNhanh1.pdf",
                        "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Vui lòng đóng file ReportDanhSachNhanVienChiNhanh2.pdf",
                        "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                }
            }
        }

        
    }
}
