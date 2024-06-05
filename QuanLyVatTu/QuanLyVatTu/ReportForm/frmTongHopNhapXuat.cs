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

            dteTuNgay.EditValue = "14-01-2000";
            dteToiNgay.EditValue = DateTime.Today.ToString("MM-dd-yyyy");
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
            DateTime fromDate = (DateTime)dteTuNgay.DateTime;
            DateTime toDate = (DateTime)dteToiNgay.DateTime;
            string chiNhanh = cboChiNhanh.SelectedValue.ToString().Contains("1") ? "Chi Nhánh 1" : "Chi Nhánh 2";

            rptTongHopNhapXuat report = new rptTongHopNhapXuat(fromDate, toDate);
            report.txtTuNgay.Text = dteTuNgay.EditValue.ToString();
            report.txtToiNgay.Text = dteToiNgay.EditValue.ToString();
            report.txtChiNhanh.Text = chiNhanh;

            ReportPrintTool printTool = new ReportPrintTool(report);
            printTool.ShowPreviewDialog();
        }

        private void btnXuatBan_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fromDate = (DateTime)dteTuNgay.DateTime;
                DateTime toDate = (DateTime)dteToiNgay.DateTime;
                string chiNhanh = cboChiNhanh.SelectedValue.ToString().Contains("1") ? "Chi Nhánh 1" : "Chi Nhánh 2";

                rptTongHopNhapXuat report = new rptTongHopNhapXuat(fromDate, toDate);
                report.txtChiNhanh.Text = chiNhanh;
                report.txtTuNgay.Text = dteTuNgay.EditValue.ToString();
                report.txtToiNgay.Text = dteToiNgay.EditValue.ToString();

                if (cboChiNhanh.SelectedValue.ToString().Contains("1"))
                {
                    if (File.Exists(@"D:\Github\PTIT-Co-So-Du-Lieu-Phan-Tan-De-Tai-Quan-Ly-Vat-Tu\ReportFiles\ReportTongHopNhapXuatChiNhanh1.pdf"))
                    {
                        DialogResult dr = MessageBox.Show("File ReportTongHopNhapXuatChiNhanh1.pdf tại folder ReportFiles đã có!\nBạn có muốn tạo lại?",
                            "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dr == DialogResult.Yes)
                        {
                            report.ExportToPdf(@"D:\Github\PTIT-Co-So-Du-Lieu-Phan-Tan-De-Tai-Quan-Ly-Vat-Tu\ReportFiles\ReportTongHopNhapXuatChiNhanh1.pdf");
                            MessageBox.Show("File ReportTongHopNhapXuatChiNhanh1.pdf đã được ghi thành công tại folder ReportFiles",
                    "Xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                    else
                    {
                        report.ExportToPdf(@"D:\Github\PTIT-Co-So-Du-Lieu-Phan-Tan-De-Tai-Quan-Ly-Vat-Tu\ReportFiles\ReportTongHopNhapXuatChiNhanh1.pdf");
                        MessageBox.Show("File ReportTongHopNhapXuatChiNhanh1.pdf đã được ghi thành công tại folder ReportFiles",
                    "Xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (File.Exists(@"D:\Github\PTIT-Co-So-Du-Lieu-Phan-Tan-De-Tai-Quan-Ly-Vat-Tu\ReportFiles\ReportTongHopNhapXuatChiNhanh2.pdf"))
                    {
                        DialogResult dr = MessageBox.Show("File ReportTongHopNhapXuatChiNhanh2.pdf tại folder ReportFiles đã có!\nBạn có muốn tạo lại?",
                            "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dr == DialogResult.Yes)
                        {
                            report.ExportToPdf(@"D:\Github\PTIT-Co-So-Du-Lieu-Phan-Tan-De-Tai-Quan-Ly-Vat-Tu\ReportFiles\ReportTongHopNhapXuatChiNhanh2.pdf");
                            MessageBox.Show("File ReportTongHopNhapXuatChiNhanh2.pdf đã được ghi thành công tại folder ReportFiles",
                    "Xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                    else
                    {
                        report.ExportToPdf(@"D:\Github\PTIT-Co-So-Du-Lieu-Phan-Tan-De-Tai-Quan-Ly-Vat-Tu\ReportFiles\ReportTongHopNhapXuatChiNhanh2.pdf");
                        MessageBox.Show("File ReportTongHopNhapXuatChiNhanh2.pdf đã được ghi thành công tại folder ReportFiles",
                    "Xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show("Vui lòng đóng file ReportTongHopNhapXuatChiNhanh1.pdf",
                    "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    MessageBox.Show("Vui lòng đóng file ReportTongHopNhapXuatChiNhanh2.pdf",
                    "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    return;
                }
            }
        }
    }
}
