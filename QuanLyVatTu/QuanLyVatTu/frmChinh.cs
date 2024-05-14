using DevExpress.XtraBars;
using QuanLyVatTu.ReportForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuanLyVatTu
{
    public partial class frmChinh : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmChinh()
        {
            InitializeComponent();
        }

        private Form CheckExists(Type ftype)
        {
            foreach (Form f in this.MdiChildren)
                if (f.GetType() == ftype)
                    return f;
            return null;
        }

        public void enableButtons()
        {

            btnDangNhap.Enabled = false;
            btnDangXuat.Enabled = true;

            pageNhapXuat.Visible = true;
            pageBaoCao.Visible = true;
            btnTaoTaiKhoan.Enabled = true;

            if (Program.role == "USER")
            {
                btnTaoTaiKhoan.Enabled = false;
            }
        }

        private void frmChinh_Load(object sender, EventArgs e)
        {
            pageNhapXuat.Visible = false;
            pageBaoCao.Visible = false;
            btnDangXuat.Enabled = false;
            btnTaoTaiKhoan.Enabled = false;
        }

        private void logout()
        {
            foreach (Form f in this.MdiChildren)
                f.Dispose();
        }

        private void btnDangNhap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form f = this.CheckExists(typeof(frmDangNhap));
            if (f != null)
            {
                f.Activate();
            }
            else
            {
                frmDangNhap form = new frmDangNhap();
                //form.MdiParent = this;
                form.Show();
            }
        }

        private void btnDangXuat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            logout();

            btnDangNhap.Enabled = true;
            btnThoat.Enabled = true;
            btnTaoTaiKhoan.Enabled = false;
            btnDangXuat.Enabled = false;

            pageNhapXuat.Visible = false;
            pageBaoCao.Visible = false;
            //pageTaiKhoan.Visible = false;

            Form f = this.CheckExists(typeof(frmDangNhap));
            if (f != null)
            {
                f.Activate();
            }
            else
            {
                frmDangNhap form = new frmDangNhap();
                //form.MdiParent = this;
                form.Show();
            }

            Program.frmChinh.maNhanVien.Text = "MÃ NHÂN VIÊN";
            Program.frmChinh.hoTen.Text = "HỌ TÊN";
            Program.frmChinh.vaiTro.Text = "VAI TRÒ";
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnNhanVien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form f = this.CheckExists(typeof(frmNhanVien));
            if (f != null)
            {
                f.Activate();
            }
            else
            {
                frmNhanVien form = new frmNhanVien();
                form.MdiParent = this;
                form.Show();
            }
        }
 
        private void btnVatTu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form f = this.CheckExists(typeof(frmVatTu));
            if (f != null)
            {
                f.Activate();
            }
            else
            {
                frmVatTu form = new frmVatTu();
                form.MdiParent = this;
                form.Show();
            }
        }

        private void btnKho_ItemClick(object sender, ItemClickEventArgs e)
        {
            Console.WriteLine("click kho");
            Form f = this.CheckExists(typeof(frmKho));
            if (f != null)
            {
                f.Activate();
            }
            else
            {
                frmKho form = new frmKho();
                form.MdiParent = this;
                form.Show();
            }
        }

        private void btnDonDatHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form f = this.CheckExists(typeof(frmDatHang));
            if (f != null)
            {
                f.Activate();
            }
            else
            {
                frmDatHang form = new frmDatHang();
                form.MdiParent = this;
                form.Show();
            }
        }

        private void btnDSNhanVien_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form f = this.CheckExists(typeof(frmDanhSachNhanVien));
            if (f != null)
            {
                f.Activate();
            }
            else
            {
                frmDanhSachNhanVien form = new frmDanhSachNhanVien();
                form.Show();
            }
        }

        private void btnDSVatTu_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form f = this.CheckExists(typeof(frmDanhSachVatTu));
            if (f != null)
            {
                f.Activate();
            }
            else
            {
                frmDanhSachVatTu form = new frmDanhSachVatTu();
                form.Show();
            }
        }

        private void btnTongHopNhapXuat_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form f = this.CheckExists(typeof(frmTongHopNhapXuat));
            if (f != null)
            {
                f.Activate();
            }
            else
            {
                frmTongHopNhapXuat form = new frmTongHopNhapXuat();
                form.Show();
            }
        }

        private void btnTaoTaiKhoan_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}
