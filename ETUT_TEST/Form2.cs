using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ETUT_TEST
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-KPC6PV7\SQLEXPRESS;Initial Catalog=etuttest;Integrated Security=True");

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("select *from tbldersler", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();

        }

        private void btnders_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand kmt = new SqlCommand("delete from tbldersler where dersıd=@p1", baglanti);
            kmt.Parameters.AddWithValue("@p1",txtdersid.Text);
            kmt.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ders kayıdı silindi","bilig",MessageBoxButtons.OK,MessageBoxIcon.Information);

            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("select *from tbldersler", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtdersid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();

            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("select *from tbldersler", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }
    }
}
