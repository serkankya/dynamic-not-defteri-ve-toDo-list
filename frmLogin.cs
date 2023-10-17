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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            t.BackColor = Color.Yellow;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            t.BackColor = Color.White;
        }

        private void clearAll()
        {
            foreach (Control item in Controls)
            {
                if (item is TextBox)
                {
                    item.Text = string.Empty;
                }
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            clearAll();
            this.MaximumSize = new Size(365, 208);
            this.MinimumSize = new Size(365, 208);
            this.MaximizeBox = false;
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            try
            {

                if (sql.con.State != ConnectionState.Open)
                {
                    sql.con.Open();
                }
                if (txtUserName.Text != string.Empty && txtPassword.Text != string.Empty)
                {
                    SqlCommand cmd = new SqlCommand("SELECT *FROM tbl_admin WHERE USERNAME=@p1 AND PASSWORD=@p2", sql.con);
                    cmd.Parameters.AddWithValue("@p1", txtUserName.Text);
                    cmd.Parameters.AddWithValue("@p2", txtPassword.Text);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        MessageBox.Show(txtUserName.Text + " hesabına giriş yapılıyor...", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmSecurity frm2 = new frmSecurity(txtUserName.Text);
                        frm2.Show();
                        this.Hide();
                        clearAll();
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı adı veya şifre hatalı.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        clearAll();
                    }
                    sql.con.Close();
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı ve şifreyi girmek zorundasınız.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Bir hata oldu. Hata : " + ex, "Bağlantı hatası", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}
