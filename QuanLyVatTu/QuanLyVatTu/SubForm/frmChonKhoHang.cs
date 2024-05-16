using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyVatTu.SubForm
{
    public partial class frmChonKhoHang : Form
    {
        public frmChonKhoHang()
        {
            InitializeComponent();
        }

        private void khoBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsKhoHang.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dataSet);
        }

        private void frmChonKhoHang_Load(object sender, EventArgs e)
        {
            // Không kiểm tra khóa ngoại
            dataSet.EnforceConstraints = false;
            this.khoTableAdapter.Connection.ConnectionString = Program.connstr;
            this.khoTableAdapter.Fill(this.dataSet.Kho);
            this.Close();
        }

        private Form CheckExists(Type ftype)
        {
            foreach (Form f in this.MdiChildren)
                if (f.GetType() == ftype)
                    return f;
            return null;
        }
        private void btnChon_Click(object sender, EventArgs e)
        {
            string maKhoHang = ((DataRowView)bdsKhoHang.Current)["MAKHO"].ToString();
            Program.maKhoDuocChon = maKhoHang;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
