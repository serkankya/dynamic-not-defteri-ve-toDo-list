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

namespace notListem
{
    public partial class frmSecurity : Form
    {
        public frmSecurity(string name)
        {
            InitializeComponent();
            lblName.Text = name;
        }

        

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        int chance = 3;

        private void btnEnter_Click(object sender, EventArgs e)
        {
            try
            {
                if (sql.con.State != ConnectionState.Open)
                {
                    sql.con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT *FROM tbl_admin WHERE SECURITYCODE=@p1", sql.con);
                cmd.Parameters.AddWithValue("@p1", txtCode.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        frmMain frm3 = new frmMain(lblName.Text);
                        this.Hide();
                        frm3.Show();
                    }
                    else
                    {
                            if (chance >= 2)
                            {
                                chance--;
                                MessageBox.Show($"Hatalı giriş. {chance} deneme hakkınız kaldı.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtCode.Text = string.Empty;
                            }
                            else
                            {
                                MessageBox.Show("Deneme hakkınız kalmamıştır. Uygulama kapanıyor.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                Application.Exit();
                            }
                    }
                sql.con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Bir hata oldu. Hata : " + ex, "Bağlantı hatası", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void frmSecurity_Load(object sender, EventArgs e)
        {

        }
    }
}
