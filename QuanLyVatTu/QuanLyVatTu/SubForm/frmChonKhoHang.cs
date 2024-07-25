﻿using System;
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

        private void frmChonKhoHang_Load(object sender, EventArgs e)
        {
            // Không kiểm tra khóa ngoại
            dataSet.EnforceConstraints = false;
            this.khoTableAdapter.Connection.ConnectionString = Program.connstr;
            this.khoTableAdapter.Fill(this.dataSet.Kho);
           
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            string maKhoHang = ((DataRowView)bdsKhoHang.Current)["MAKHO"].ToString();
            Program.maKhoDuocChon = maKhoHang;
            this.Close();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
