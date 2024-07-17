using DevExpress.XtraEditors;
using QuanLyVatTu.SubForm;
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
using static DevExpress.XtraEditors.Mask.MaskSettings;

namespace QuanLyVatTu
{
    public partial class frmTaoTaiKhoan : DevExpress.XtraEditors.XtraForm
    {
        private string taiKhoan = "";
        private string matKhau = "";
        private string maNhanVien = "";
        private string vaiTro = "";

        public frmTaoTaiKhoan()
        {
            InitializeComponent();
        }

        private void btnChonNhanVien_Click(object sender, EventArgs e)
        {
            frmChonNhanVien form = new frmChonNhanVien();
            Program.dangTaoTaiKhoan = true;
            form.ShowDialog();

            txtMaNhanVien.Text = Program.maNhanVienDuocChon;
            txtHoTenNV.Text = Program.hoTen;
        }

        private bool kiemTraDuLieuDauVao()
        {
            if (txtMaNhanVien.Text == "")
            {
                MessageBox.Show("Không được để trống mã nhân viên", "Thông báo", MessageBoxButtons.OK);
                return false;
            }

            if (txtMatKhau.Text == "")
            {
                MessageBox.Show("Không được để trống mật khẩu", "Thông báo", MessageBoxButtons.OK);
                return false;
            }

            if (txtXacNhanMatKhau.Text == "")
            {
                MessageBox.Show("Không được để trống mật khẩu xác nhận", "Thông báo", MessageBoxButtons.OK);
                return false;
            }

            if (txtMatKhau.Text != txtXacNhanMatKhau.Text)
            {
                MessageBox.Show("Mật khẩu không khớp với mật khẩu xác nhận", "Thông báo", MessageBoxButtons.OK);
                return false;
            }

            return true;
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            bool ketQua = kiemTraDuLieuDauVao();
            if (ketQua == false) return;

            taiKhoan = txtMaNhanVien.Text;
            matKhau = txtMatKhau.Text.Trim();
            maNhanVien = txtMaNhanVien.Text;
            if (vaiTro != "CONGTY")
            {
                vaiTro = (rdoChiNhanh.Checked == true) ? "CHINHANH" : "USER";
            }

            Console.WriteLine(taiKhoan);
            Console.WriteLine(matKhau);
            Console.WriteLine(maNhanVien);
            Console.WriteLine(vaiTro);

            String cauTruyVan =
                    "EXEC sp_TaoTaiKhoan '" + taiKhoan + "' , '" + matKhau + "', '"
                    + maNhanVien + "', '" + vaiTro + "'";

            SqlCommand sqlCommand = new SqlCommand(cauTruyVan, Program.conn);
            try
            {
                Program.myReader = Program.ExecSqlDataReader(cauTruyVan);
                if (Program.myReader == null)
                {
                    return;
                }

                MessageBox.Show("Đăng kí tài khoản thành công\n\nTài khoản: " + taiKhoan + "\nMật khẩu: " + matKhau + "\nMã Nhân Viên: " + maNhanVien + "\nVai Trò: " + vaiTro, "Thông Báo", MessageBoxButtons.OK);
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thực thi database thất bại!\n\n" + ex.Message, "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.Message);
                return;
            }
            Program.dangTaoTaiKhoan = false;
        }

        private void frmTaoTaiKhoan_Load(object sender, EventArgs e)
        {
            if (Program.role == "CONGTY")
            {
                vaiTro = "CONGTY";
                rdoChiNhanh.Enabled = false;
                rdoUser.Enabled = false;
            }
            else
            {
                rdoChiNhanh.Enabled = true;
                rdoChiNhanh.Checked = true;
                rdoUser.Enabled = true;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
