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

        /********************************************
         * đang thêm mới -> true -> đang dùng btnTHEM
         *              -> false -> có thể là btnGHI( chỉnh sửa) hoặc btnXOA
         *              
         * Mục đích: dùng biến này để phân biệt giữa btnTHEM - thêm mới hoàn toàn
         * và việc chỉnh sửa nhân viên( do mình ko dùng thêm btnXOA )
         * Trạng thái true or false sẽ được sử dụng 
         * trong btnGHI - việc này để phục vụ cho btnHOANTAC
         ********************************************/
        bool dangThemMoi = false;
        String maChiNhanh = " ";


        Stack undoList = new Stack();
        /**********************************************************
         * undoList - phục vụ cho btnHOANTAC -  chứa các thông tin của đối tượng bị tác động 
         * 
         * nó là nơi lưu trữ các đối tượng cần thiết để hoàn tác các thao tác
         * 
         * nếu btnGHI sẽ ứng với INSERT
         * nếu btnXOA sẽ ứng với DELETE
         * nếu btnCHUYENCHINHANH sẽ ứng với CHANGEBRAND
         **********************************************************/
        public frmKho()
        {
            InitializeComponent();
        }

        /*
         *Step 1: tat kiem tra khoa ngoai & do du lieu vao form
         *Step 2: lay du lieu dang nhap tu form dang nhap
         *Step 3: bat cac nut chuc nang theo nhom quyen
         */
        private void frmKho_Load(object sender, EventArgs e)
        {
            /*Step 1*/
            /*không kiểm tra khóa ngoại nữa*/

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

            /*Step 2*/
            cboChiNhanh.DataSource = Program.bindingSource;
            cboChiNhanh.DisplayMember = "TENCN";
            cboChiNhanh.ValueMember = "TENSERVER";
            cboChiNhanh.SelectedIndex = Program.brand;

            /*Step 3*/
            /*CONG TY chi xem du lieu*/
            if (Program.role == "CONGTY")
            {
                cboChiNhanh.Enabled = true;
                this.btnThem.Enabled = false;
                this.btnXoa.Enabled = false;
                this.btnGhi.Enabled = false;
                this.btnHoanTac.Enabled = false;
                this.btnLamMoi.Enabled = true;
                //this.btnChuyenChiNhanh.Enabled = false;
                this.btnThoat.Enabled = true;
                this.pnlNhapLieu.Enabled = false;
            }
            /* CHI NHANH & USER co the xem - xoa - sua du lieu nhung khong the 
            chuyen sang chi nhanh khac*/


            // CHI NHANH & USER co the xem - xoa - sua du lieu nhung khong the 
            // chuyen sang chi nhanh khac
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
            /*Step 1*/
            /*lấy vị trí hiện tại của con trỏ*/
            viTri = bdsKho.Position;
            this.pnlNhapLieu.Enabled = true;
            dangThemMoi = true;
            /*Step 2*/
            /*AddNew tự động nhảy xuống cuối thêm 1 dòng mới*/
            bdsKho.AddNew();
            txtMaChiNhanh.Text = maChiNhanh;

            /*Step 3*/
            this.txtMaKho.Enabled = true;
            this.btnThem.Enabled = false;
            this.btnXoa.Enabled = false;
            this.btnGhi.Enabled = true;

            this.btnHoanTac.Enabled = true;
            this.btnLamMoi.Enabled = false;
            this.btnThoat.Enabled = false;

            this.gcKho.Enabled = false;
            this.pnlNhapLieu.Enabled = true;
        }
        private bool kiemTraDuLieuDauVao()
        {
            /*kiem tra txtMAKHO*/
            if (txtMaKho.Text == "")
            {
                MessageBox.Show("Không bỏ trống mã kho hàng", "Thông báo", MessageBoxButtons.OK);
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
            /*kiem tra txtTenKho*/
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
            /*kiem tra txtDiaChi*/
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
            /*Step 1*/
            /*Lay du lieu truoc khi chon btnGHI - phuc vu btnHOANTAC*/
            String maKhoHang = txtMaKho.Text.Trim(); // Trim() de loai bo khoang trang thua
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
                /*khong co ket qua tra ve thi ket thuc luon*/
                if (Program.myReader == null)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thực thi Stored Procedure thất bại!\n\n" + ex.Message, "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.Message);
                return;
            }
            Program.myReader.Read();
            int result = int.Parse(Program.myReader.GetValue(0).ToString());
            //Console.WriteLine(result);
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
                DialogResult dr = MessageBox.Show("Bạn có muốn ghi dữ liệu vào cơ sở dữ liệu?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    try
                    {
                        btnThem.Enabled = true;
                        btnXoa.Enabled = true;
                        btnGhi.Enabled = true;
                        btnHoanTac.Enabled = true;

                        btnLamMoi.Enabled = true;
                        // btnChuyenChiNhanh.Enabled = true;
                        btnThoat.Enabled = true;

                        this.txtMaKho.Enabled = false;
                        this.gcKho.Enabled = true;
                        String cauTruyVanHoanTac = "";

                        /*trước khi ấn btnGHI là btnTHEM*/
                        if (dangThemMoi == true)
                        {
                            cauTruyVanHoanTac = "" +
                                "DELETE DBO.KHO" +
                                " WHERE MAKHO = '" + txtMaKho.Text.ToString().Trim() + "'";
                            Console.WriteLine("console ne +" + txtMaKho.Text.ToString().Trim());
                        }
                        /*trước khi ấn btnGHI là sửa thông tin kho*/
                        else
                        {
                            cauTruyVanHoanTac =
                                "UPDATE DBO.KHO " +
                                "SET " +
                                "TENKHO = '" + tenKhoHang + "'," +
                                "DIACHI = '" + diaChi + "'" +
                                "WHERE MAKHO = '" + maKhoHang + "'";
                        }

                        /*Đưa câu truy vấn hoàn tác vào undoList 
                         * để nếu chẳng may người dùng ấn hoàn tác thì quất luôn*/
                        undoList.Push(cauTruyVanHoanTac);

                        this.bdsKho.EndEdit();
                        this.khoTableAdapter.Update(this.dataSet.Kho);
                        /*cập nhật lại trạng thái thêm mới cho chắc*/
                        dangThemMoi = false;
                        MessageBox.Show("Ghi thành công", "Thông báo", MessageBoxButtons.OK);
                    }



                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        bdsKho.RemoveCurrent();
                        MessageBox.Show("Tên vật tư có thể đã được dùng !\n\n" + ex.Message, "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            /*Step 1*/
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

            /* Phần này phục vụ tính năng hoàn tác
                    * Đưa câu truy vấn hoàn tác vào undoList 
                    * để nếu chẳng may người dùng ấn hoàn tác*/


            string cauTruyVanHoanTac =
            "INSERT INTO DBO.KHO(MAKHO,TENKHO,DIACHI,MACN) " +
            " VALUES( '" + txtMaKho.Text + "','" +
                        txtTenKho.Text + "','" +
                        txtDiaChi.Text + "', '" +
                        txtMaChiNhanh.Text.Trim() + "' ) ";

            Console.WriteLine(cauTruyVanHoanTac);
            undoList.Push(cauTruyVanHoanTac);

            /*Step 2*/
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không ?", "Thông báo",
                MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    /*Step 3*/
                    viTri = bdsKho.Position;
                    bdsKho.RemoveCurrent();

                    this.khoTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.khoTableAdapter.Update(this.dataSet.Kho);

                    MessageBox.Show("Xóa thành công ", "Thông báo", MessageBoxButtons.OK);
                    this.btnHoanTac.Enabled = true;
                }
                catch (Exception ex)
                {
                    /*Step 4*/
                    MessageBox.Show("Lỗi xóa nhân viên. Hãy thử lại\n" + ex.Message, "Thông báo", MessageBoxButtons.OK);
                    this.khoTableAdapter.Fill(this.dataSet.Kho);
                    bdsKho.Position = viTri;
                    return;
                }
            }
            else
            {
                // xoa cau truy van hoan tac di
                undoList.Pop();
            }

        }

        private void btnHoanTac_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            /* Step 0 */
            if (dangThemMoi == true && this.btnThem.Enabled == false)
            {
                dangThemMoi = false;

                this.txtMaKho.Enabled = false;
                this.btnThem.Enabled = true;
                this.btnXoa.Enabled = true;
                this.btnGhi.Enabled = true;

                this.btnHoanTac.Enabled = false;
                this.btnLamMoi.Enabled = true;
                this.btnThoat.Enabled = true;


                this.gcKho.Enabled = true;
                this.pnlNhapLieu.Enabled = true;

                bdsKho.CancelEdit();
                /*xoa dong hien tai*/
                bdsKho.RemoveCurrent();
                /* trở về lúc đầu con trỏ đang đứng*/
                bdsKho.Position = viTri;
                return;
            }

            /*Step 1*/
            if (undoList.Count == 0)
            {
                MessageBox.Show("Không còn thao tác nào để khôi phục", "Thông báo", MessageBoxButtons.OK);
                btnHoanTac.Enabled = false;
                return;
            }

            /*Step 2*/
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
                MessageBox.Show("Lỗi Làm mới" + ex.Message, "Thông báo", MessageBoxButtons.OK);
                return;
            }
        }

        private void cboChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
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

