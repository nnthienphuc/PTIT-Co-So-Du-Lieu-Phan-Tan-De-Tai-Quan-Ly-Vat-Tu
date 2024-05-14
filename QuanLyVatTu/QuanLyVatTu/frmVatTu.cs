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
    public partial class frmVatTu : Form
    {
        int viTri = 0;

        bool dangThemMoi = false;

        String maChiNhanh = "";

        Stack undoList = new Stack();

        public frmVatTu()
        {
            InitializeComponent();
        }

        private void vattuBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsVatTu.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dataSet);
        }

        private void frmVatTu_Load(object sender, EventArgs e)
        {
            dataSet.EnforceConstraints = false;

            this.vattuTableAdapter.Connection.ConnectionString = Program.connstr;
            this.vattuTableAdapter.Fill(this.dataSet.Vattu);

            this.ctddhTableAdapter.Connection.ConnectionString = Program.connstr;
            this.ctddhTableAdapter.Fill(this.dataSet.CTDDH);

            this.ctpnTableAdapter.Connection.ConnectionString = Program.connstr;
            this.ctpnTableAdapter.Fill(this.dataSet.CTPN);

            this.ctpxTableAdapter.Connection.ConnectionString = Program.connstr;
            this.ctpxTableAdapter.Fill(this.dataSet.CTPX);

            cboChiNhanh.DataSource = Program.bindingSource; /*sao chep bingding source tu form dang nhap*/
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
                this.txtMaVT.Enabled = false;

            }
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
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
                this.vattuTableAdapter.Connection.ConnectionString = Program.connstr;
                this.vattuTableAdapter.Fill(this.dataSet.Vattu);

                this.ctpxTableAdapter.Connection.ConnectionString = Program.connstr;
                this.ctpxTableAdapter.Fill(this.dataSet.CTPX);

                this.ctddhTableAdapter.Connection.ConnectionString = Program.connstr;
                this.ctddhTableAdapter.Fill(this.dataSet.CTDDH);

                this.ctpnTableAdapter.Connection.ConnectionString = Program.connstr;
                this.ctpnTableAdapter.Fill(this.dataSet.CTPN);
            }
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            // lấy vị trí hiện tại của con trỏ
            viTri = bdsVatTu.Position;
            this.pnlNhapLieu.Enabled = true;
            dangThemMoi = true;

            // AddNew tự động nhảy xuống cuối thêm 1 dòng mới
            bdsVatTu.AddNew();
            txtSoLuongTon.Value = 1;

            this.txtMaVT.Enabled = true;
            this.btnThem.Enabled = false;
            this.btnXoa.Enabled = false;
            this.btnGhi.Enabled = true;

            this.btnHoanTac.Enabled = true;
            this.btnLamMoi.Enabled = false;
            this.btnThoat.Enabled = false;


            this.gclVatTu.Enabled = false;
            this.pnlNhapLieu.Enabled = true;
        }

        /**********************************************************************
         * moi lan nhan btnHOANTAC thi nen nhan them btnLAMMOI de 
         * tranh bi loi khi an btnTHEM lan nua
         * 
         * statement: chua cau y nghia chuc nang ngay truoc khi an btnHOANTAC.
         * Vi du: statement = INSERT | DELETE | CHANGEBRAND
         * 
         * bdsNhanVien.CancelEdit() - phuc hoi lai du lieu neu chua an btnGHI
         * Step 0: trường hợp đã ấn btnTHEM nhưng chưa ấn btnGHI
         * Step 1: kiểm tra undoList có trống hay không ?
         * Step 2: Neu undoList khong trống thì lấy ra khôi phục
         *********************************************************************/
        private void btnHoanTac_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            /* Step 0 */
            if (dangThemMoi == true && this.btnThem.Enabled == false)
            {
                dangThemMoi = false;

                this.txtMaVT.Enabled = false;
                this.btnThem.Enabled = true;
                this.btnXoa.Enabled = true;
                this.btnGhi.Enabled = true;

                this.btnHoanTac.Enabled = false;
                this.btnLamMoi.Enabled = true;
                this.btnThoat.Enabled = true;


                this.gclVatTu.Enabled = true;
                this.pnlNhapLieu.Enabled = true;

                bdsVatTu.CancelEdit();
                /*xoa dong hien tai*/
                bdsVatTu.RemoveCurrent();
                /* trở về lúc đầu con trỏ đang đứng*/
                bdsVatTu.Position = viTri;
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
            bdsVatTu.CancelEdit();
            String cauTruyVanHoanTac = undoList.Pop().ToString();
            Console.WriteLine(cauTruyVanHoanTac);
            int n = Program.ExecSqlNonQuery(cauTruyVanHoanTac);
            this.vattuTableAdapter.Fill(this.dataSet.Vattu);
        }

        private bool kiemTraDuLieuDauVao()
        {
            if (txtMaVT.Text == "")
            {
                MessageBox.Show("Không bỏ trống mã vật tư", "Thông báo", MessageBoxButtons.OK);
                txtMaVT.Focus();
                return false;
            }
            if (Regex.IsMatch(txtMaVT.Text, @"^[a-zA-Z0-9]+$") == false)
            {
                MessageBox.Show("Mã vật tư chỉ có chữ cái và số", "Thông báo", MessageBoxButtons.OK);
                txtMaVT.Focus();
                return false;
            }
            if (txtMaVT.Text.Length > 4)
            {
                MessageBox.Show("Mã vật tư không quá 4 kí tự", "Thông báo", MessageBoxButtons.OK);
                txtMaVT.Focus();
                return false;
            }

            if (txtTenVT.Text == "")
            {
                MessageBox.Show("Không bỏ trống tên vật tư", "Thông báo", MessageBoxButtons.OK);
                txtTenVT.Focus();
                return false;
            }
            if (txtTenVT.Text.Length > 30)
            {
                MessageBox.Show("Tên vật tư không quá 30 kí tự", "Thông báo", MessageBoxButtons.OK);
                txtTenVT.Focus();
                return false;
            }

            if (txtDonViTinh.Text == "")
            {
                MessageBox.Show("Không bỏ trống đơn vị tính", "Thông báo", MessageBoxButtons.OK);
                txtDonViTinh.Focus();
                return false;
            }
            if (Regex.IsMatch(txtDonViTinh.Text, @"^[a-zA-Z ]+$") == false)
            {
                MessageBox.Show("Đơn vị vật tư chỉ có chữ cái", "Thông báo", MessageBoxButtons.OK);
                txtDonViTinh.Focus();
                return false;
            }
            if (txtDonViTinh.Text.Length > 15)
            {
                MessageBox.Show("Đơn vị vật tự không quá 15 kí tự", "Thông báo", MessageBoxButtons.OK);
                txtDonViTinh.Focus();
                return false;
            }

            if (txtSoLuongTon.Value < 0)
            {
                MessageBox.Show("Số lượng tồn phải ít nhất bằng 0", "Thông báo", MessageBoxButtons.OK);
                txtSoLuongTon.Focus();
                return false;
            }

            return true;
        }

        /***********************************************************************
         * viTriConTro: vi tri con tro chuot dang dung
         * viTriMaVatTu: vi tri cua ma nhan vien voi btnTHEM or hanh dong sua du lieu
         * sp_KiemTraMaVatTu tra ve 0 neu khong ton tai
         *                          1 neu ton tai
         *                                    
         * Step 0 : Kiem tra du lieu dau vao
         * Step 1 : Dung stored procedure sp_KiemTraMaVatTu de kiem tra txtMANV
         * Step 2 : Ket hop ket qua tu Step 1 & vi tri cua txtMAVT co 2 truong hop xay ra
         * + TH0: ketQua = 1 && viTriConTro != viTriMaVatTu -> them moi nhung MANV da ton tai
         * + TH1: ketQua = 1 && viTriConTro == viTriMaVatTu -> sua nhan vien cu
         * + TH2: ketQua = 0 && viTriConTro == viTriMaVatTu -> co the them moi nhan vien
         * + TH3: ketQua = 0 && viTriConTro != viTriMaVatTu -> co the them moi nhan vien
         *          
         * Step 3 : Neu khong phai TH0 thi cac TH1 - TH2 - TH3 deu hop le 
         ***********************************************************************/
        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            /* Step 0 */
            bool ketQua = kiemTraDuLieuDauVao();
            if (ketQua == false)
                return;


            /*Step 1*/
            /*Lay du lieu truoc khi chon btnGHI - phuc vu btnHOANTAC*/
            String maVatTu = txtMaVT.Text.Trim();// Trim() de loai bo khoang trang thua
            DataRowView drv = ((DataRowView)bdsVatTu[bdsVatTu.Position]);
            String tenVatTu = drv["TENVT"].ToString();
            String donViTinh = drv["DVT"].ToString();
            String soLuongTon = (drv["SOLUONGTON"].ToString());

            String cauTruyVan =
                    "DECLARE	@result int " +
                    "EXEC @result = sp_TraCuu_KiemTraMaVatTu '" +
                    maVatTu + "' " +
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
                MessageBox.Show("Thực thi database thất bại!\n\n" + ex.Message, "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.Message);
                return;
            }
            Program.myReader.Read();
            int result = int.Parse(Program.myReader.GetValue(0).ToString());
            //Console.WriteLine(result);
            Program.myReader.Close();



            /*Step 2*/
            int viTriConTro = bdsVatTu.Position;
            int viTriMaVatTu = bdsVatTu.Find("MAVT", txtMaVT.Text);

            if (result == 1 && viTriConTro != viTriMaVatTu)
            {
                MessageBox.Show("Mã vật tư này đã được sử dụng !", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            else
            {
                DialogResult dr = MessageBox.Show("Bạn có chắc muốn ghi dữ liệu vào cơ sở dữ liệu ?", "Thông báo",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    try
                    {
                        /*bật các nút về ban đầu*/
                        btnThem.Enabled = true;
                        btnXoa.Enabled = true;
                        btnGhi.Enabled = true;
                        btnHoanTac.Enabled = true;

                        btnLamMoi.Enabled = true;
                        btnThoat.Enabled = true;

                        this.txtMaVT.Enabled = false;
                        this.gclVatTu.Enabled = true;

                        /*lưu 1 câu truy vấn để hoàn tác yêu cầu*/
                        String cauTruyVanHoanTac = "";
                        /*trước khi ấn btnGHI là btnTHEM*/
                        if (dangThemMoi == true)
                        {
                            cauTruyVanHoanTac = "" +
                                "DELETE DBO.VATTU " +
                                "WHERE MAVT = '" + txtMaVT.Text.Trim() + "'";
                        }
                        /*trước khi ấn btnGHI là sửa thông tin nhân viên*/
                        else
                        {
                            cauTruyVanHoanTac =
                                "UPDATE DBO.VATTU " +
                                "SET " +
                                "TENVT = '" + tenVatTu + "'," +
                                "DVT = '" + donViTinh + "'," +
                                "SOLUONGTON = " + soLuongTon + " " +
                                "WHERE MAVT = '" + maVatTu + "'";
                        }

                        /*Đưa câu truy vấn hoàn tác vào undoList 
                         * để nếu chẳng may người dùng ấn hoàn tác thì quất luôn*/
                        undoList.Push(cauTruyVanHoanTac);

                        this.bdsVatTu.EndEdit();
                        this.vattuTableAdapter.Update(this.dataSet.Vattu);
                        /*cập nhật lại trạng thái thêm mới cho chắc*/
                        dangThemMoi = false;
                        MessageBox.Show("Ghi thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        bdsVatTu.RemoveCurrent();
                        MessageBox.Show("Tên vật tư có thể đã được dùng !\n\n" + ex.Message, "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

            }
        }

        private void btnLamMoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.vattuTableAdapter.Fill(this.dataSet.Vattu);
                this.gclVatTu.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Làm mới" + ex.Message, "Thông báo", MessageBoxButtons.OK);
                return;
            }
        }

        /***************************************************************************
         * có thể ở chi nhánh này vật tư X không được sử dụng trong phiếu nhập | xuất nào
         * cả. Nhưng ở chi nhánh khác vật tư X được sử dụng trong phiếu nhập hoặc xuất
         * nếu xóa thì gây mâu thuẫn dữ liệu.
         * 
         * Nếu vật tư được sử dụng ở chi nhánh khác thì không được xóa
         * 
         * Trả về 1 nếu tồn tại. Trả về 0 nếu chưa tham gia vào phiếu nhập | xuất nào
         ***************************************************************************/
        private int kiemTraVatTuCoSuDungTaiChiNhanhKhac(String maVatTu)
        {
            String cauTruyVan =
                    "DECLARE	@result int " +
                    "EXEC @result = sp_KiemTraMaVatTuChiNhanhConLai '" +
                    maVatTu + "' " +
                    "SELECT 'Value' = @result";
            SqlCommand sqlCommand = new SqlCommand(cauTruyVan, Program.conn);
            try
            {
                Program.myReader = Program.ExecSqlDataReader(cauTruyVan);
                /*khong co ket qua tra ve thi ket thuc luon*/
                if (Program.myReader == null)
                {
                    return 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thực thi database thất bại!\n\n" + ex.Message, "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.Message);
                return 1;
            }
            Program.myReader.Read();
            int result = int.Parse(Program.myReader.GetValue(0).ToString());
            //Console.WriteLine("line 535");
            //Console.WriteLine(result);
            Program.myReader.Close();

            /*result = 1 nghia la vat tu nay dang duoc su dung o chi nhanh con lai*/
            int ketQua = (result == 1) ? 1 : 0;

            return ketQua;
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            /*Step 1*/
            if (bdsVatTu.Count == 0)
            {
                btnXoa.Enabled = false;
            }

            if (bdsCTDDH.Count > 0)
            {
                MessageBox.Show("Không thể xóa vật tư này vì đã lập đơn đặt hàng", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            if (bdsCTPN.Count > 0)
            {
                MessageBox.Show("Không thể xóa vật tư này vì đã lập phiếu nhập", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            if (bdsCTPX.Count > 0)
            {
                MessageBox.Show("Không thể xóa vật tư này vì đã lập phiếu xuất", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            /*kiem tra xem no co dang duoc su dung tai chi nhanh khac hay khong ?*/
            String maVatTu = txtMaVT.Text.Trim();// Trim() de loai bo khoang trang thua
            int ketQua = kiemTraVatTuCoSuDungTaiChiNhanhKhac(maVatTu);

            if (ketQua == 1)
            {
                MessageBox.Show("Không thể xóa vật tư này vì đang được sử dụng ở chi nhánh khác", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            /* Phần này phục vụ tính năng hoàn tác
            * Đưa câu truy vấn hoàn tác vào undoList 
            * để nếu chẳng may người dùng ấn hoàn tác thì quất luôn*/
            string cauTruyVanHoanTac =
            "INSERT INTO DBO.VATTU( MAVT,TENVT,DVT,SOLUONGTON) " +
            " VALUES( '" + txtMaVT.Text + "','" +
                        txtTenVT.Text + "','" +
                        txtDonViTinh.Text + "', " +
                        txtSoLuongTon.Value + " ) ";

            Console.WriteLine(cauTruyVanHoanTac);
            undoList.Push(cauTruyVanHoanTac);

            /*Step 2*/
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không ?", "Thông báo",
                MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    /*Step 3*/
                    viTri = bdsVatTu.Position;
                    bdsVatTu.RemoveCurrent();

                    this.vattuTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.vattuTableAdapter.Update(this.dataSet.Vattu);

                    MessageBox.Show("Xóa thành công ", "Thông báo", MessageBoxButtons.OK);
                    this.btnHoanTac.Enabled = true;
                }
                catch (Exception ex)
                {
                    /*Step 4*/
                    MessageBox.Show("Lỗi xóa nhân viên. Hãy thử lại\n" + ex.Message, "Thông báo", MessageBoxButtons.OK);
                    this.vattuTableAdapter.Fill(this.dataSet.Vattu);
                    bdsVatTu.Position = viTri;
                    return;
                }
            }
            else
            {
                undoList.Pop();
            }
        }
    }
}