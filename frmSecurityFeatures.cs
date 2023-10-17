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
    public partial class frmSecurityFeatures : Form
    {
        string _userName;
        bool _selection;
        public frmSecurityFeatures(string userName, bool selection)
        {
            InitializeComponent();
            _userName = userName;
            _selection = selection;
        }

        private void txtControl_Enter(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.BackColor = Color.Yellow;
        }

        private void txtControl_Leave(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.BackColor = Color.Silver;
        }

        private void txtControl_TextChanged(object sender, EventArgs e)
        {

        }


        private void btnCheck_Click(object sender, EventArgs e)
        {
            try
            {
                sql.con.Close();
                sql.con.Open();
                SqlCommand cmd = new SqlCommand("SELECT *FROM tbl_admin WHERE USERNAME = @p1 AND PASSWORD = @p2 AND SECURITYCODE = @p3", sql.con);
                cmd.Parameters.AddWithValue("@p1", _userName);
                cmd.Parameters.AddWithValue("@p2", txtControl.Text);
                cmd.Parameters.AddWithValue("@p3", txtControlCode.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    btnCheck.Enabled = false;

                    txtControl.BackColor = Color.Green;
                    txtControlCode.BackColor = Color.Green;
                    txtControl.Enabled = false;
                    txtControlCode.Enabled = false;

                    txtNew1.Enabled = true;

                    txtCode1.Enabled = true;
                }
                else if (txtControl.Text == string.Empty || txtControlCode.Text == string.Empty)
                {
                    MessageBox.Show("Şifre veya güvenlik kısmını boş bırakamazsınız.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Girdiğiniz şifre veya güvenlik kodu hatalı.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                dr.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Bir hata oluştu. Hata : " + ex);
            }
            sql.con.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtNew1.Text != string.Empty || txtNew2.Text != string.Empty)
            {
                try
                {
                    sql.con.Close();
                    sql.con.Open();
                    if (txtNew1.Text == txtNew2.Text)
                    {
                        try
                        {
                            SqlCommand cmd2 = new SqlCommand("UPDATE tbl_admin SET PASSWORD=@p3", sql.con);
                            cmd2.Parameters.AddWithValue("@p3", txtNew2.Text);
                            cmd2.ExecuteNonQuery();
                            sql.con.Close();
                            MessageBox.Show("Şifre başarıyla güncellendi.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnSave.Enabled = false;
                            txtNew1.Enabled = false;
                            txtNew2.Enabled = false;
                            btnCancel.Text = "Formu kapat.";
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show("Bir hata oluştu. Hata : " + ex);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Girdiğiniz şifreler aynı olmak zorunda.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    sql.con.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Bir hata oluştu. Hata : " + ex);
                }
            }
            else
            {
                MessageBox.Show("Yeni şifre kısmını boş bırakamazsınız.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmSecurityFeatures_Load(object sender, EventArgs e)
        {
            lblCodeWarning.Visible = false;
            lblPasswordWarning.Visible = false;

            txtNew1.Enabled = false;
            txtNew2.Enabled = false;
            btnSave.Enabled = false;

            txtCode1.Enabled = false;
            txtCode2.Enabled = false;
            btnSaveCode.Enabled = false;

            if (_selection == true)
            {
                pnlSecurityCode.Visible = false;
                pnlPassword.Visible = true;
            }
            else
            {
                pnlPassword.Visible = false;
                pnlSecurityCode.Visible = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveCode_Click(object sender, EventArgs e)
        {
            if (txtCode1.Text != string.Empty || txtCode2.Text != string.Empty)
            {
                try
                {
                    sql.con.Close();
                    sql.con.Open();
                    if (txtCode1.Text == txtCode2.Text)
                    {
                        try
                        {
                            SqlCommand cmd = new SqlCommand("UPDATE tbl_admin SET SECURITYCODE = @p1", sql.con);
                            cmd.Parameters.AddWithValue("@p1", txtCode2.Text);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Kod başarıyla güncellendi.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnSaveCode.Enabled = false;
                            txtCode1.Enabled = false;
                            txtCode2.Enabled = false;
                            btnCancel.Text = "Formu kapat.";
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show("Bir hata oluştu. Hata : " + ex);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Girdiğiniz kodlar aynı olmak zorunda.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    sql.con.Close();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                MessageBox.Show("Yeni şifre kısmını boş bırakamazsınız.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtNew1_TextChanged(object sender, EventArgs e)
        {
            if (txtNew1.Text.Length < 7)
            {
                lblPasswordWarning.Text = "Girdiğiniz şifre en az 7 karakter içermelidir.";
                lblPasswordWarning.Visible = true;
                txtNew2.Enabled = false;
            }
            else if (txtNew1.Text.Length > 16)
            {
                lblPasswordWarning.Text = "Girdiğiniz şifre en fazla 16 karakter içermelidir.";
                lblPasswordWarning.Visible = true;
                txtNew2.Enabled = false;
            }
            else
            {
                lblPasswordWarning.Visible = false;
                txtNew2.Enabled = true;
            }
        }

        private void txtNew2_TextChanged(object sender, EventArgs e)
        {
            if (txtNew1.Text != txtNew2.Text)
            {
                lblPasswordWarning.Text = "Girdiğiniz şifreler uyuşmak zorunda.";
                lblPasswordWarning.Visible = true;
                btnSave.Enabled = false;
            }
            else
            {
                lblPasswordWarning.Visible = false;
                btnSave.Enabled = true;
            }
        }

        private void txtCode1_TextChanged(object sender, EventArgs e)
        {
            if(txtCode1.Text.Length < 4)
            {
                lblCodeWarning.Text = "Girdiğiniz kod en az 4 karakter içermelidir.";
                lblCodeWarning.Visible = true;
                txtCode2.Enabled = false;
            }
            else if (txtCode1.Text.Length > 8)
            {
                lblCodeWarning.Text = "Girdiğiniz kod en fazla 8 karakter içermelidir.";
                lblCodeWarning.Visible = true;
                txtCode2.Enabled = false;
            }
            else
            {
                lblCodeWarning.Visible = false;
                txtCode2.Enabled = true;
            }
        }

        private void txtCode2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
