using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyVatTu
{
    public partial class frmKho : Form
    {
        int viTri = 0;

        bool dangThemMoi = false;
        String maChiNhanh = " ";


        Stack undoList = new Stack();

        public frmKho()
        {
            InitializeComponent();
        }
        private void frmKho_Load(object sender, EventArgs e)
        {
            dataSet.EnforceConstraints = false;

            this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
            this.datHangTableAdapter.Fill(this.dataSet.DatHang);

            this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
            this.phieuNhapTableAdapter.Fill(this.dataSet.PhieuNhap);

            this.phieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
            this.phieuXuatTableAdapter.Fill(this.dataSet.PhieuXuat);

            this.khoTableAdapter.Connection.ConnectionString = Program.connstr;
            this.khoTableAdapter.Fill(this.dataSet.Kho);
            maChiNhanh = ((DataRowView)bdsKho[0])["MACN"].ToString();

            cboChiNhanh.DataSource = Program.bindingSource;
            cboChiNhanh.DisplayMember = "TENCN";
            cboChiNhanh.ValueMember = "TENSERVER";
            cboChiNhanh.SelectedIndex = Program.brand;

            if (Program.role == "CONGTY")
            {
                cboChiNhanh.Enabled = true;

                this.btnThem.Enabled = false;
                this.btnXoa.Enabled = false;
                this.btnGhi.Enabled = false;
                this.btnHoanTac.Enabled = false;
                this.btnLamMoi.Enabled = true;
                this.btnThoat.Enabled = true;

                this.pnlNhapLieu.Enabled = false;
            }

            if (Program.role == "CHINHANH" || Program.role == "USER")
            {
                cboChiNhanh.Enabled = false;

                this.btnThem.Enabled = true;
                this.btnXoa.Enabled = true;
                this.btnGhi.Enabled = true;
                this.btnHoanTac.Enabled = false;
                this.btnLamMoi.Enabled = true;
                this.btnThoat.Enabled = true;

                this.pnlNhapLieu.Enabled = true;
                this.txtMaKho.Enabled = false;
            }
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            viTri = bdsKho.Position;
            this.pnlNhapLieu.Enabled = true;
            dangThemMoi = true;

            bdsKho.AddNew();
            txtMaChiNhanh.Text = maChiNhanh;

            this.btnThem.Enabled = false;
            this.btnXoa.Enabled = false;
            this.btnGhi.Enabled = true;
            this.btnHoanTac.Enabled = true;
            this.btnLamMoi.Enabled = false;
            this.btnThoat.Enabled = false;

            this.gcKho.Enabled = false;
            this.pnlNhapLieu.Enabled = true;
            this.txtMaKho.Enabled = true;
        }
        private bool kiemTraDuLieuDauVao()
        {
            if (txtMaKho.Text == "")
            {
                MessageBox.Show("Không được bỏ trống mã kho hàng", "Thông báo", MessageBoxButtons.OK);
                txtMaKho.Focus();
                return false;
            }
            if (Regex.IsMatch(txtMaKho.Text, @"^[A-Za-z 0-9]+$") == false)
            {
                MessageBox.Show("Mã kho chỉ chấp nhận chữ và số", "Thông báo", MessageBoxButtons.OK);
                txtMaKho.Focus();
                return false;
            }
            if (txtMaKho.Text.Length > 4)
            {
                MessageBox.Show("Mã kho không thể lớn hơn 4 kí tự", "Thông báo", MessageBoxButtons.OK);
                txtMaKho.Focus();
                return false;
            }

            if (txtTenKho.Text == "")
            {
                MessageBox.Show("Không bỏ trống tên kho hàng", "Thông báo", MessageBoxButtons.OK);
                txtTenKho.Focus();
                return false;
            }
            if (Regex.IsMatch(txtTenKho.Text, @"^[A-Za-z 0-9 ]+$") == false)
            {
                MessageBox.Show("Tên kho bao gồm chữ cái, số và khoảng trắng", "Thông báo", MessageBoxButtons.OK);
                txtTenKho.Focus();
                return false;
            }
            if (txtTenKho.Text.Length > 30)
            {
                MessageBox.Show("Tên kho không thể quá 30 kí tự", "Thông báo", MessageBoxButtons.OK);
                txtTenKho.Focus();
                return false;
            }

            if (txtDiaChi.Text == "")
            {
                MessageBox.Show("Không bỏ trống địa chỉ kho hàng", "Thông báo", MessageBoxButtons.OK);
                txtDiaChi.Focus();
                return false;
            }
            if (Regex.IsMatch(txtDiaChi.Text, @"^[A-Za-z 0-9 ,]+$") == false)
            {
                MessageBox.Show("Địa chỉ chỉ gồm chữ cái, số và khoảng trắng", "Thông báo", MessageBoxButtons.OK);
                txtDiaChi.Focus();
                return false;
            }
            if (txtDiaChi.Text.Length > 100)
            {
                MessageBox.Show("Địa chỉ không quá 100 kí tự", "Thông báo", MessageBoxButtons.OK);
                txtDiaChi.Focus();
                return false;
            }
            return true;
        }
        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool ketQua = kiemTraDuLieuDauVao();
            if (ketQua == false)
                return;

            String maKhoHang = txtMaKho.Text.Trim();
            DataRowView drv = ((DataRowView)bdsKho[bdsKho.Position]);
            String tenKhoHang = drv["TENKHO"].ToString();
            String diaChi = drv["DIACHI"].ToString();
            String cauTruyVan =
                    "DECLARE	@result int " +
                    "EXEC @result = sp_TraCuu_KiemTraMaKho '" +
                    maKhoHang + "' " +
                    "SELECT 'Value' = @result";
            SqlCommand sqlCommand = new SqlCommand(cauTruyVan, Program.conn);
            try
            {
                Program.myReader = Program.ExecSqlDataReader(cauTruyVan);
                if (Program.myReader == null)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể thực hiện hành động này: \n" + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.Message);
                return;
            }
            Program.myReader.Read();
            int result = int.Parse(Program.myReader.GetValue(0).ToString());

            Program.myReader.Close();
            int viTriConTro = bdsKho.Position;
            int viTriMaKhoHang = bdsKho.Find("MAKHO", txtMaKho.Text);

            if (result == 1 && viTriConTro != viTriMaKhoHang)
            {
                MessageBox.Show("Mã kho hàng này đã sử dụng", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            else
            {
                DialogResult dr = MessageBox.Show("Bạn có muốn ghi dữ liệu vào cơ sở dữ liệu?", "Thông báo",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    try
                    {
                        btnThem.Enabled = true;
                        btnXoa.Enabled = true;
                        btnGhi.Enabled = true;
                        btnHoanTac.Enabled = true;
                        btnLamMoi.Enabled = true;
                        btnThoat.Enabled = true;

                        this.txtMaKho.Enabled = false;
                        this.gcKho.Enabled = true;

                        String cauTruyVanHoanTac = "";

                        // Trước khi ấn btnGhi là btnThem
                        if (dangThemMoi == true)
                        {
                            cauTruyVanHoanTac = "" +
                                "DELETE DBO.KHO" +
                                " WHERE MAKHO = '" + txtMaKho.Text.ToString().Trim() + "'";
                            Console.WriteLine("console ne +" + txtMaKho.Text.ToString().Trim());
                        }
                        // Trước khi ấn btnGhi là btnSua
                        else
                        {
                            cauTruyVanHoanTac =
                                "UPDATE DBO.KHO " +
                                "SET " +
                                "TENKHO = '" + tenKhoHang + "'," +
                                "DIACHI = '" + diaChi + "'" +
                                "WHERE MAKHO = '" + maKhoHang + "'";
                        }

                        undoList.Push(cauTruyVanHoanTac);

                        this.bdsKho.EndEdit();
                        this.khoTableAdapter.Update(this.dataSet.Kho);

                        dangThemMoi = false;
                        MessageBox.Show("Ghi thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        bdsKho.RemoveCurrent();
                        MessageBox.Show("Đã xảy ra lỗi khi ghi dữ liệu: \n" + ex.Message, "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (bdsKho.Count == 0)
            {
                btnXoa.Enabled = false;
            }

            if (bdsDatHang.Count > 0)
            {
                MessageBox.Show("Không thể xóa kho hàng này vì đã lập đơn đặt hàng", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            if (bdsPhieuNhap.Count > 0)
            {
                MessageBox.Show("Không thể xóa kho hàng này vì đã lập phiếu nhập", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            if (bdsPhieuXuat.Count > 0)
            {
                MessageBox.Show("Không thể xóa kho hàng này vì đã lập phiếu xuất", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            string cauTruyVanHoanTac =
            "INSERT INTO DBO.KHO(MAKHO,TENKHO,DIACHI,MACN) " +
            " VALUES( '" + txtMaKho.Text + "','" +
                        txtTenKho.Text + "','" +
                        txtDiaChi.Text + "', '" +
                        txtMaChiNhanh.Text.Trim() + "' ) ";

            Console.WriteLine(cauTruyVanHoanTac);
            undoList.Push(cauTruyVanHoanTac);

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo",
                MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    viTri = bdsKho.Position;
                    bdsKho.RemoveCurrent();

                    this.khoTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.khoTableAdapter.Update(this.dataSet.Kho);

                    MessageBox.Show("Xóa thành công ", "Thông báo", MessageBoxButtons.OK);
                    this.btnHoanTac.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể xóa kho này:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.khoTableAdapter.Fill(this.dataSet.Kho);
                    bdsKho.Position = viTri;
                    return;
                }
            }
            else
            {
                undoList.Pop();
            }
        }

        private void btnHoanTac_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dangThemMoi == true && this.btnThem.Enabled == false)
            {
                dangThemMoi = false;

                this.btnThem.Enabled = true;
                this.btnXoa.Enabled = true;
                this.btnGhi.Enabled = true;
                this.btnHoanTac.Enabled = false;
                this.btnLamMoi.Enabled = true;
                this.btnThoat.Enabled = true;

                this.gcKho.Enabled = true;
                this.pnlNhapLieu.Enabled = true;
                this.txtMaKho.Enabled = false;

                bdsKho.CancelEdit();
                bdsKho.RemoveCurrent();
                bdsKho.Position = viTri;
                return;
            }

            if (undoList.Count == 0)
            {
                MessageBox.Show("Không còn thao tác nào để hoàn tác", "Thông báo", MessageBoxButtons.OK);
                btnHoanTac.Enabled = false;
                return;
            }

            bdsKho.CancelEdit();
            String cauTruyVanHoanTac = undoList.Pop().ToString();
            Console.WriteLine(cauTruyVanHoanTac);
            int n = Program.ExecSqlNonQuery(cauTruyVanHoanTac);
            bdsKho.Position = viTri;
            this.khoTableAdapter.Fill(this.dataSet.Kho);

        }
        

        private void btnLamMoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.khoTableAdapter.Fill(this.dataSet.Kho);
                this.gcKho.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Làm mới: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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
            else
            {
                this.khoTableAdapter.Connection.ConnectionString = Program.connstr;
                this.khoTableAdapter.Fill(this.dataSet.Kho);

                this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
                this.phieuNhapTableAdapter.Fill(this.dataSet.PhieuNhap);

                this.phieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
                this.phieuXuatTableAdapter.Fill(this.dataSet.PhieuXuat);

                this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
                this.datHangTableAdapter.Fill(this.dataSet.DatHang);
            }
        }
    }
}

