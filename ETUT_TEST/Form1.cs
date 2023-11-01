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
using System.IO;

namespace ETUT_TEST
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-KPC6PV7\SQLEXPRESS;Initial Catalog=etuttest;Integrated Security=True");

        void etutlistesi()
        {
            SqlDataAdapter da3=new SqlDataAdapter("execute etut",baglanti);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);
            dataGridView1.DataSource = dt3;
        }
        void derslistele()
        {
            baglanti.Open();
            SqlDataAdapter da =new SqlDataAdapter("select *from tbldersler",baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbders.ValueMember = "dersıd";
            cmbders.DisplayMember = "dersad";
            cmbders.DataSource = dt;
            baglanti.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            derslistele();
            etutlistesi();

            SqlDataAdapter da4 = new SqlDataAdapter("execute  brans", baglanti);
            DataTable dt4 = new DataTable();
            da4.Fill(dt4);
            cmbbrans.DisplayMember = "dersad";
            cmbbrans.ValueMember = "dersıd";
            cmbbrans.DataSource = dt4;

        }

        private void cmbders_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataAdapter da2 = new SqlDataAdapter("select * from tblogretmen where bransıd=" +cmbders.SelectedValue, baglanti);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            cmbogretmen.ValueMember = "ogrtıd";
            cmbogretmen.DisplayMember = "ad";
            cmbogretmen.DataSource = dt2;
        }

        private void btnetutolustur_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut =new SqlCommand("insert into tbletut (dersıd,ogretmenıd,tarih,saat)values(@p1,@p2,@p3,@p4)",baglanti);
            komut.Parameters.AddWithValue("@p1",cmbders.SelectedValue);
            komut.Parameters.AddWithValue("@p2",cmbogretmen.SelectedValue);
            komut.Parameters.AddWithValue("@p3",msktarih.Text);
            komut.Parameters.AddWithValue("@p4",msksaat.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("etüt oluşturuldu","bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            etutlistesi();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            textBox2.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
        }

        private void btnetutver_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update tbletut set ogrenciid=@p1,durum=@p2 where ıd=@p3",baglanti);
            komut.Parameters.AddWithValue("@p1",textBox1.Text);
            komut.Parameters.AddWithValue("@p2","true");
            komut.Parameters.AddWithValue("@p3",textBox2.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("etüt öğrenciye verildi","bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            derslistele();

            baglanti.Close();
        }

        private void btnfotograf_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName;

        }

        private void btnogrenci_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand kmt2 = new SqlCommand("insert into tblogrenci (ad,soyad,fotograf,sınıf,telefon,mail) values (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
            kmt2.Parameters.AddWithValue("@p1", txtad.Text);
            kmt2.Parameters.AddWithValue("@p2", txtsoyad.Text);
            kmt2.Parameters.AddWithValue("@p3", pictureBox1.ImageLocation);
            kmt2.Parameters.AddWithValue("@p4", txtsınıf.Text);
            kmt2.Parameters.AddWithValue("@p5", msktelefon.Text);
            kmt2.Parameters.AddWithValue("@p6", txtmail.Text);
            kmt2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("öğrenci başarı ile kaydedildi", "bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }

        private void btnders_Click(object sender, EventArgs e)
        {
            
           
            
                baglanti.Open();
                SqlCommand kmt3 = new SqlCommand("insert into tbldersler (dersad)values(@p1)", baglanti);
                kmt3.Parameters.AddWithValue("@p1", txtders.Text);
                kmt3.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("ders başarı ile eklendi", "bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                derslistele();
            

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 frm = new Form2();
            frm.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form3 frm =new Form3();
            frm.Show();
        }

        private void cmbbrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnogretmenekle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into tblogretmen(ad,soyad,bransıd)values(@p1,@p2,@p3)",baglanti);
            komut.Parameters.AddWithValue("@p1", txtogretmenad.Text);
            komut.Parameters.AddWithValue("@p2",txtogretmensoyad.Text);
            komut.Parameters.AddWithValue("@p3",cmbbrans.SelectedValue);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("kayıt başarı ile eklendi","bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form4 frm = new Form4();
            frm.Show();
        }
    }
}
