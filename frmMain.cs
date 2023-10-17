using notListem.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace notListem
{
    public partial class frmMain : Form
    {
        string _name;
        public frmMain(string name)
        {
            InitializeComponent();
            lblName.Text = "Hoş geldin, " + name.ToUpper() + "." + " Bugün neler yaptın ?";
            _name = name;
        }

        bool visibleState = false;

        private void frmMain_Load(object sender, EventArgs e)
        {
            pnlOptions.Visible = false;

            flowLayoutPanel1.Controls.Clear();
            sql.con.Close();
            sql.con.Open();
            SqlCommand cmd = new SqlCommand("SELECT *FROM tbl_notes ORDER BY DATE DESC", sql.con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                int noteID = (int)dr["ID"];
                string noteText = dr["NOTE"].ToString();
                string noteTitle = dr["TITLE"].ToString();
                DateTime date = (DateTime)dr["DATE"];
                DateTime dateUpdate = (DateTime)dr["UPDATEDATE"];
                int noteCharacter = (int)dr["CHARACTER"];

                Panel pnl = new Panel();
                Button btn = new Button();
                RichTextBox txt = new RichTextBox();
                Label lbl = new Label();
                Label lbl2 = new Label();
                Label lbl3 = new Label();
                Label lbl4 = new Label();
                Label lbl5 = new Label();
                Label lblTextOfUpdateInfo = new Label();
                Label lblDateOfUpdate = new Label();
                PictureBox pct = new PictureBox();
                TextBox txtBox = new TextBox();

                pnl.BackColor = System.Drawing.Color.LightGray;
                pnl.Controls.Add(txtBox);
                pnl.Controls.Add(lbl5);
                pnl.Controls.Add(pct);
                pnl.Controls.Add(lbl);
                pnl.Controls.Add(lbl2);
                pnl.Controls.Add(btn);
                pnl.Controls.Add(txt);
                pnl.Controls.Add(lbl3);
                pnl.Controls.Add(lbl4);
                pnl.Controls.Add(lblTextOfUpdateInfo);
                pnl.Controls.Add(lblDateOfUpdate);

                pnl.Name = noteID.ToString();
                pnl.Size = new System.Drawing.Size(258, 350);

                pct.BackColor = System.Drawing.Color.Transparent;
                pct.Image = Image.FromFile("D:\\İndirilenler\\Blur-Transparent.png");
                pct.Location = new System.Drawing.Point(6, 83);
                pct.Name = "pctShow" + noteID.ToString();
                pct.Size = new System.Drawing.Size(245, 232);
                pct.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                pct.Cursor = System.Windows.Forms.Cursors.Hand;
                pct.Visible = visibleState;
                pct.Click += pctClicked_Click;
                pct.TabIndex = 8;
                pct.TabStop = false;

                lbl3.AutoSize = true;
                lbl3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                lbl3.Location = new System.Drawing.Point(142, 61);
                lbl3.Name = "lblCharachter" + noteID.ToString();
                lbl3.Size = new System.Drawing.Size(109, 20);
                lbl3.TabIndex = 5;
                lbl3.Text = noteCharacter.ToString() + " Karakter";

                lbl2.AutoSize = true;
                lbl2.Font = new System.Drawing.Font("Microsoft Tai Le", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                lbl2.Location = new System.Drawing.Point(127, 57);
                lbl2.Name = "lbl" + noteID.ToString();
                lbl2.Size = new System.Drawing.Size(16, 23);
                lbl2.TabIndex = 4;
                lbl2.Text = "|";

                lbl.AutoSize = true;
                lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                lbl.Location = new System.Drawing.Point(6, 62);
                lbl.Name = "lblCurrentDate" + noteID.ToString();
                lbl.Size = new System.Drawing.Size(122, 18);
                lbl.TabIndex = 3;
                lbl.Text = date.ToString("dd MMMM HH:mm");

                btn.BackColor = System.Drawing.Color.OrangeRed;
                btn.Cursor = System.Windows.Forms.Cursors.Hand;
                btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                btn.Location = new System.Drawing.Point(6, 321);
                btn.Name = "btnDel" + noteID.ToString();
                btn.Size = new System.Drawing.Size(122, 23);
                btn.TabIndex = 2;
                btn.Text = "SİL";
                btn.Click += btnSelected_Click;
                btn.UseVisualStyleBackColor = false;

                txtBox.BackColor = System.Drawing.Color.Gainsboro;
                txtBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
                txtBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                txtBox.Location = new System.Drawing.Point(6, 7);
                txtBox.Multiline = true;
                txtBox.Leave += txtBox_Leave;
                txtBox.Name = "txtTitle" + noteID.ToString();
                txtBox.Size = new System.Drawing.Size(245, 52);
                txtBox.TabIndex = 7;
                txtBox.Text = noteTitle;

                txt.BackColor = System.Drawing.Color.Gainsboro;
                txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                txt.Location = new System.Drawing.Point(6, 84);
                txt.Name = "text" + noteID.ToString();
                txt.Size = new System.Drawing.Size(245, 231);
                txt.TabIndex = 1;
                txt.Text = noteText;

                lblTextOfUpdateInfo.AutoSize = true;
                lblTextOfUpdateInfo.Location = new System.Drawing.Point(144, 318);
                lblTextOfUpdateInfo.Name = "lblUpdateDate" + noteID.ToString();
                lblTextOfUpdateInfo.Size = new System.Drawing.Size(107, 13);
                lblTextOfUpdateInfo.TabIndex = 10;
                lblTextOfUpdateInfo.Text = "Güncellenme Tarihi : ";

                lblDateOfUpdate.AutoSize = true;
                lblDateOfUpdate.BackColor = System.Drawing.Color.LightGreen;
                lblDateOfUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                lblDateOfUpdate.Location = new System.Drawing.Point(148, 330);
                lblDateOfUpdate.Name = "lblDateOfUpdate";
                lblDateOfUpdate.Size = new System.Drawing.Size(101, 15);
                lblDateOfUpdate.TabIndex = 6;
                lblDateOfUpdate.Text = dateUpdate.ToString("dd MMMM HH:mm");

                flowLayoutPanel1.Controls.Add(pnl);

            }
            sql.con.Close();



        }

        private void MakeUpdateDate(int RelatedID)
        {
            sql.con.Close();
            sql.con.Open();
            SqlCommand cmd2 = new SqlCommand("UPDATE tbl_notes SET UPDATEDATE=@p1 WHERE ID= @p2", sql.con);
            cmd2.Parameters.AddWithValue("@p1", DateTime.Now);
            cmd2.Parameters.AddWithValue("@p2", RelatedID);
            cmd2.ExecuteNonQuery();
            sql.con.Close();
        }

        private void txtBox_Leave(object sender, EventArgs e)
        {
            TextBox selectedTextBox = (TextBox)sender;

            string newTitle = selectedTextBox.Text;

            int IdOfNote = int.Parse(selectedTextBox.Name.Replace("txtTitle", ""));

            sql.con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE tbl_notes SET TITLE = @p1 WHERE ID = @p2", sql.con);
            cmd.Parameters.AddWithValue("@p1", newTitle);
            cmd.Parameters.AddWithValue("@p2", IdOfNote);
            cmd.ExecuteNonQuery();
            sql.con.Close();

            MakeUpdateDate(IdOfNote);

            frmMain_Load(sender, EventArgs.Empty);
        }

        private void pctClicked_Click(object sender, EventArgs e)
        {
            PictureBox pctSelected = ((PictureBox)sender);
            pctSelected.Visible = false;

        }

        private void btnSelected_Click(object sender, EventArgs e)
        {
            Button selectedButton = ((Button)sender);
            Panel relatedPanel = (Panel)selectedButton.Parent;

            sql.con.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM tbl_notes WHERE ID=@p1", sql.con);
            cmd.Parameters.AddWithValue("@p1", relatedPanel.Name);
            cmd.ExecuteNonQuery();
            sql.con.Close();

            flowLayoutPanel1.Controls.Remove(relatedPanel);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddNote frm4 = new frmAddNote();
            frm4.ShowDialog();

            frmMain_Load(sender, EventArgs.Empty);

        }

        private void btnSafeExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void optionsOpen()
        {
            pnlOptions.Visible = true;
            btnAdd.Visible = false;
            btnSafeExit.Visible = false;
            btnOptions.Text = "Kaydetmeden Çık";
            lblName.Location = new System.Drawing.Point(12, 19);
            this.Text = "Ayarlar";
            lblName.Text = "Hangi ayarları değiştirmek istersin " + _name.ToUpper() + " ?";
            flowLayoutPanel1.Visible = false;
        }

        private void optionsClose()
        {
            pnlOptions.Visible = false;
            btnAdd.Visible = true;
            btnSafeExit.Visible = true;
            btnOptions.Text = "Ayarlar";
            lblName.Location = new System.Drawing.Point(134, 19);
            this.Text = "Not Listem";
            lblName.Text = "Hoş geldin, " + _name.ToUpper() + "." + " Bugün neler yaptın ?";
            flowLayoutPanel1.Visible = true;
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            if (pnlOptions.Visible == false)
            {
                optionsOpen();
            }
            else
            {
                optionsClose();
            }
        }

        private void btnSaveFeatures_Click(object sender, EventArgs e)
        {
            if (chkHide.Checked)
            {
                visibleState = true;
                frmMain_Load(sender, e);
                optionsClose();
            }
            else if (chkHide.Checked == false)
            {
                visibleState = false;
                frmMain_Load(sender, e);
                optionsClose();
            }

            if (cmbBackColor.SelectedItem != null)
            {
                string selectedColor = cmbBackColor.SelectedItem.ToString();
                switch (selectedColor)
                {
                    case "Siyah": this.BackColor = Color.DimGray; break;
                    case "Sarı": this.BackColor = Color.YellowGreen; break;
                    case "Mor": this.BackColor = Color.MediumPurple; break;
                    case "Pembe": this.BackColor = Color.DeepPink; break;
                    case "Mavi": this.BackColor = Color.DarkSlateBlue; break;
                    case "Yeşil": this.BackColor = Color.DarkOliveGreen; break;
                    case "Turkuaz": this.BackColor = Color.Aquamarine; break;
                    case "Gri": this.BackColor = Color.LightGray; break;
                    default:
                        MessageBox.Show("Bir hata oluştu. Birazdan tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
        }

        private void cmbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLanguage.SelectedItem != null && cmbLanguage.SelectedItem.ToString() == "Türkçe")
            {
                MessageBox.Show("Uygulamanızın dili zaten Türkçe.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (cmbLanguage.SelectedItem != null && cmbLanguage.SelectedItem.ToString() == "English")
            {
                MessageBox.Show("Yakında eklenecektir...", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSetDefault_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Gainsboro;
            visibleState = false;
            chkHide.Checked = false;
            cmbBackColor.Text = string.Empty;
            optionsClose();
            frmMain_Load(sender, e);
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            frmSecurityFeatures frm5 = new frmSecurityFeatures(_name, true);
            frm5.ShowDialog();
        }

        private void btnChangeCode_Click(object sender, EventArgs e)
        {
            frmSecurityFeatures frm5 = new frmSecurityFeatures(_name, false);
            frm5.ShowDialog();
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Gerçekten bütün notları silmek istediğinize emin misiniz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                try
                {
                    sql.con.Close();
                    sql.con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM tbl_notes", sql.con);
                    cmd.ExecuteNonQuery();
                    sql.con.Close();
                    flowLayoutPanel1.Controls.Clear();
                    optionsClose();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Bir hata oluştu. Hata : " + ex);
                }
            }
        }
    }
}
