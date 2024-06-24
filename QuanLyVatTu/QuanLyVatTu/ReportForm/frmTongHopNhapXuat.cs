using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyVatTu.ReportForm
{
    public partial class frmTongHopNhapXuat : Form
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

        public frmTongHopNhapXuat()
        {
            InitializeComponent();
        }

        private void frmTongHopNhapXuat_Load(object sender, EventArgs e)
        {
            if (Program.role == "CONGTY")
            {
                this.cboChiNhanh.Enabled = true;
            }

            if (KetNoiDatabaseGoc() == 0)
                return;

            layDanhSachPhanManh("SELECT TOP 2 * FROM view_DanhSachPhanManh");
            cboChiNhanh.SelectedIndex = Program.brand;

            dteTuNgay.EditValue = "01-01-2017";
            dteToiNgay.EditValue = DateTime.Now.ToString("dd-MM-yyyy");
        }

        private void cboChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*Neu combobox khong co so lieu thi ket thuc luon*/
            if (cboChiNhanh.SelectedValue.ToString() == "System.Data.DataRowView")
                return;

            Program.serverName = cboChiNhanh.SelectedValue.ToString();

            /*Neu chon sang chi nhanh khac voi chi nhanh hien tai*/
            if (cboChiNhanh.SelectedIndex != Program.brand)
            {
                Program.loginName = Program.remoteLogin;
                Program.loginPassword = Program.remotePassword;
            }
            /*Neu chon trung voi chi nhanh dang dang nhap o formDangNhap*/
            else
            {
                Program.loginName = Program.currentLogin;
                Program.loginPassword = Program.currentPassword;
            }

            if (Program.KetNoi() == 0)
            {
                MessageBox.Show("Xảy ra lỗi kết nối với chi nhánh hiện tại", "Thông báo", MessageBoxButtons.OK);
            }
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
                DateTime fromDate = (DateTime)dteTuNgay.DateTime;
                DateTime toDate = (DateTime)dteToiNgay.DateTime;
                string chiNhanh = cboChiNhanh.SelectedValue.ToString().Contains("1") ? "Chi Nhánh 1" : "Chi Nhánh 2";

                rptTongHopNhapXuat report = new rptTongHopNhapXuat(fromDate, toDate);
                report.txtTuNgay.Text = dteTuNgay.EditValue.ToString();
                report.txtNguoiLap.Text = Program.staff;
                report.txtNgayLap.Text = DateTime.Now.ToString("MM/dd/yyyy");
                report.txtToiNgay.Text = dteToiNgay.EditValue.ToString();
                report.txtChiNhanh.Text = chiNhanh;

                ReportPrintTool printTool = new ReportPrintTool(report);
                printTool.ShowPreviewDialog();
            }
        }

        private void btnXuatBan_Click(object sender, EventArgs e)
        {
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
                    string chiNhanh = cboChiNhanh.SelectedValue.ToString().Contains("1") ? "Chi Nhánh 1" : "Chi Nhánh 2";

                    rptTongHopNhapXuat report = new rptTongHopNhapXuat(fromDate, toDate);
                    report.txtChiNhanh.Text = chiNhanh;
                    report.txtNguoiLap.Text = Program.staff;
                    report.txtNgayLap.Text = DateTime.Now.ToString("MM/dd/yyyy");
                    report.txtTuNgay.Text = dteTuNgay.EditValue.ToString();
                    report.txtToiNgay.Text = dteToiNgay.EditValue.ToString();

                    string filePath = "";

                    if (cboChiNhanh.SelectedValue.ToString().Contains("1"))
                    {
                        filePath = @"D:\Github\PTIT-Co-So-Du-Lieu-Phan-Tan-De-Tai-Quan-Ly-Vat-Tu\ReportFiles\ReportTongHopNhapXuatChiNhanh1.pdf";
                    }
                    else
                    {
                        filePath = @"D:\Github\PTIT-Co-So-Du-Lieu-Phan-Tan-De-Tai-Quan-Ly-Vat-Tu\ReportFiles\ReportTongHopNhapXuatChiNhanh2.pdf";
                    }

                    if (File.Exists(filePath))
                    {
                        DialogResult dr = MessageBox.Show($"File {Path.GetFileName(filePath)} tại folder ReportFiles đã có!\nBạn có muốn tạo lại?",
                            "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dr == DialogResult.Yes)
                        {
                            report.ExportToPdf(filePath);
                            MessageBox.Show($"File {Path.GetFileName(filePath)} đã được ghi thành công tại folder ReportFiles",
                                "Xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Program.OpenPdf(filePath);
                        }
                    }
                    else
                    {
                        report.ExportToPdf(filePath);
                        MessageBox.Show($"File {Path.GetFileName(filePath)} đã được ghi thành công tại folder ReportFiles",
                            "Xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Program.OpenPdf(filePath);
                    }
                    if (Program.role != "CONGTY")
                    {
                        this.Close();
                    }
                }
            }
            catch (IOException ex)
            {
                string message = cboChiNhanh.SelectedValue.ToString().Contains("1") ?
                    "ReportTongHopNhapXuatChiNhanh1.pdf" : "ReportTongHopNhapXuatChiNhanh2.pdf";
                MessageBox.Show($"Vui lòng đóng file {message}",
                    "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
        }

        private void dteToiNgay_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void dteTuNgay_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
