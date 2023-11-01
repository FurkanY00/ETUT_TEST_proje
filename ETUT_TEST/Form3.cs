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


namespace ETUT_TEST
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-KPC6PV7\SQLEXPRESS;Initial Catalog=etuttest;Integrated Security=True");


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("select *from tblogrenci",baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }

        private void btnkayitsil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand kmt = new SqlCommand("delete from tblogrenci where ogrıd=@p1", baglanti);
            kmt.Parameters.AddWithValue("@p1", txtogrid.Text);
            kmt.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci kayıdı silindi", "bilig", MessageBoxButtons.OK, MessageBoxIcon.Information);

            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("select *from tblogrenci", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtogrid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString() ;
        }
    }
}
