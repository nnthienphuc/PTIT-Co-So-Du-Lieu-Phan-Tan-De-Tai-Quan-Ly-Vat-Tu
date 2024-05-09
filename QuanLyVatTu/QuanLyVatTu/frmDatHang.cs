using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyVatTu
{
    public partial class frmDatHang : Form
    {
        int viTri = 0;
        public frmDatHang()
        {
            InitializeComponent();
        }

        private void datHangBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsDatHang.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dataSet);

        }

        private void frmDatHang_Load(object sender, EventArgs e)
        {

            dataSet.EnforceConstraints = false;
            this.ChiTietDonDatHangTableAdapter.Connection.ConnectionString = Program.connstr;
            this.ChiTietDonDatHangTableAdapter.Fill(this.dataSet.CTDDH);

            this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
            this.datHangTableAdapter.Fill(this.dataSet.DatHang);

     
            this.PhieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
            this.PhieuNhapTableAdapter.Fill(this.dataSet.PhieuNhap);
            

        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }

        private void btnLamMoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                // do du lieu moi tu dataSet vao gridControl NHANVIEN
                this.datHangTableAdapter.Fill(this.dataSet.DatHang);
                this.ChiTietDonDatHangTableAdapter.Fill(this.dataSet.CTDDH);

                this.gcDatHang.Enabled = true;
                this.gcChiTietDonDatHang.Enabled = true;

                bdsDatHang.Position = viTri;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Làm mới" + ex.Message, "Thông báo", MessageBoxButtons.OK);
                return;
            }
        }
    }
}
