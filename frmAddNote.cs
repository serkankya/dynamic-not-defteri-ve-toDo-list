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
    public partial class frmAddNote : Form
    {
        public frmAddNote()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (sql.con.State != ConnectionState.Open)
                {
                    sql.con.Open();
                }
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_notes (TITLE,NOTE,CHARACTER) VALUES (@p1,@p2,@p3)", sql.con);
                cmd.Parameters.AddWithValue("@p1", txtTitle.Text);
                cmd.Parameters.AddWithValue("@p2", txtNote.Text);
                cmd.Parameters.AddWithValue("@p3", count);
                cmd.ExecuteNonQuery();
                sql.con.Close();
                MessageBox.Show("Not eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Bir hata oluştu. Hata : " + ex, "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void frmAddNote_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            lblCharachter.Text = "0 Karakter";
            lblCurrentDate.Text = DateTime.Now.ToString("dd MMMM HH:mm");
        }

        int count = 0;

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            count = txtTitle.Text.Length + txtNote.Text.Length;
            lblCharachter.Text = count.ToString() + " Karakter";
        }

        private void txtNote_TextChanged(object sender, EventArgs e)
        {
            count = txtNote.Text.Length + txtTitle.TextLength;
            lblCharachter.Text = count.ToString() + " Karakter";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblCurrentDate.Text = DateTime.Now.ToString("dd MMMM HH:mm");
        }
    }
}
