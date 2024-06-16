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

namespace QuanLyVatTu
{
    public partial class frmDangNhap : DevExpress.XtraEditors.XtraForm
    {
        private SqlConnection connPublisher = new SqlConnection();
        // Kết nối tới DB gốc -> chỉ chạy cục bộ trên frmDangNhap nên khai báo private
        private int ketNoiDatabaseGoc()
        {
            // kiểm tra xem nếu 1 connection đang mở thì phải đóng nó trước khi mở cái mới
            // Tại sao phải kiểm tra? Vì .net mới có thể tự động đóng connection
            // nên nếu ta đã mở trước đó cũng có thể đã bị tự động đóng
            if (connPublisher != null && connPublisher.State == ConnectionState.Open)
                connPublisher.Close();
            // bắt đầu kết nối
            try
            {
                connPublisher.ConnectionString = Program.connstrPublisher;
                connPublisher.Open();
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối về cơ sở dữ liệu gốc.\nBạn xem lại Tên Server của Publisher, và tên CSDL trong chuỗi kết nối.\n" + ex.Message, "", MessageBoxButtons.OK);
                return 0;
            }
        }


        // Lấy dữ liệu từ view_DANHSACHPHANMANH
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

        /******************************************************************
         * Để tránh việc người dùng ấn vào 1 form đến 2 lần chúng ta 
         * cần sử dụng hàm này để kiểm tra xem cái form hiện tại đã 
         * có trong bộ nhớ chưa
         * Nếu có trả về "f"
         * Nếu không trả về "null"
         ******************************************************************/
        private Form CheckExists(Type ftype)
        {
            foreach (Form f in this.MdiChildren)
                if (f.GetType() == ftype)
                    return f;
            return null;
        }
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            txtTaiKhoan.Text = "NH";
            txtMatKhau.Text = "123456";
            if (ketNoiDatabaseGoc() == 0)
                return;

            layDanhSachPhanManh("SELECT TOP 2 * FROM view_DanhSachPhanManh");
            //cboChiNhanh.SelectedIndex = 0;
            cboChiNhanh.SelectedIndex = 1;
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (txtTaiKhoan.Text.Trim() == "" || txtMatKhau.Text.Trim() == "")
            {
                MessageBox.Show("Tài khoản & mật khẩu không thể bỏ trống", "Thông Báo", MessageBoxButtons.OK);
                return;
            }

            Program.loginName = txtTaiKhoan.Text.Trim();
            Program.loginPassword = txtMatKhau.Text.Trim();
            if (Program.KetNoi() == 0)
                return;

            Program.brand = cboChiNhanh.SelectedIndex;
            Program.currentLogin = Program.loginName;
            Program.currentPassword = Program.loginPassword;

            String statement = "EXEC sp_DangNhap '" + Program.loginName + "'";
            Program.myReader = Program.ExecSqlDataReader(statement);
            if (Program.myReader == null)
                return;
            // đọc một dòng của myReader - điều này là hiển nhiên vì kết quả chỉ có 1 dùng duy nhất
            // Nếu nhiều dòng thì phải dùng vòng lặp for để đọc
            Program.myReader.Read();

            Program.userName = Program.myReader.GetString(0);// lấy userName
            if (Convert.IsDBNull(Program.userName))
            {
                MessageBox.Show("Tài khoản này không có quyền truy cập\nHãy thử tài khoản khác", "Thông Báo", MessageBoxButtons.OK);
            }

            Program.staff = Program.myReader.GetString(1);
            Program.role = Program.myReader.GetString(2);

            Program.myReader.Close();
            Program.conn.Close();

            Program.frmChinh.maNhanVien.Text = "MÃ NHÂN VIÊN: " + Program.userName;
            Program.frmChinh.hoTen.Text = "HỌ TÊN: " + Program.staff;
            Program.frmChinh.vaiTro.Text = "VAI TRÒ: " + Program.role;

            this.Visible = false;
            Program.frmChinh.enableButtons();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            Program.frmChinh.Close();
        }

        private void cboChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Program.serverName = cboChiNhanh.SelectedValue.ToString();
            }
            catch (Exception) {}
        }
    }
}
