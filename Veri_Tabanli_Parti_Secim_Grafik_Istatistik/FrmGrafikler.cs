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

namespace Veri_Tabanli_Parti_Secim_Grafik_Istatistik
{
    public partial class FrmGrafikler : Form
    {
        public FrmGrafikler()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-FT6FLSV\SQLEXPRESS;Initial Catalog=DBSECIMPROJE;Integrated Security=True");

        private void FrmGrafikler_Load(object sender, EventArgs e)
        {
            //İlçe adlarını comboBox'a getirme...
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select ILCEAD from TBLILCE", baglanti);
            SqlDataReader dr=komut.ExecuteReader();
            while (dr.Read())
            {
                comboBoxIlceSeciniz.Items.Add(dr[0]);
            }
            baglanti.Close();
            
            //Grafiğe Toplam Sonuçları Getirme
            baglanti.Open();
                        SqlCommand komut2 = new SqlCommand("Select Sum(APARTI),Sum(BPARTI),Sum(CPARTI),Sum(DPARTI),Sum(EPARTI) from TBLILCE", baglanti);
            SqlDataReader dr2=komut2.ExecuteReader();
            while (dr2.Read())
            {
                chart1.Series["PARTİLER"].Points.AddXY("A PARTİ", dr2[0]);
                chart1.Series["PARTİLER"].Points.AddXY("B PARTİ", dr2[1]);
                chart1.Series["PARTİLER"].Points.AddXY("C PARTİ", dr2[2]);
                chart1.Series["PARTİLER"].Points.AddXY("D PARTİ", dr2[3]);
                chart1.Series["PARTİLER"].Points.AddXY("E PARTİ", dr2[4]);
            }
            baglanti.Close();
        }

        private void comboBoxIlceSeciniz_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * from TBLILCE Where ILCEAD=@P1", baglanti);
            komut.Parameters.AddWithValue("@P1", comboBoxIlceSeciniz.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                progressBarA.Value = int.Parse(dr[2].ToString());
                progressBarB.Value = int.Parse(dr[3].ToString());
                progressBarC.Value = int.Parse(dr[4].ToString());
                progressBarD.Value = int.Parse(dr[5].ToString());
                progressBarE.Value = int.Parse(dr[6].ToString());

                LblA.Text = dr[2].ToString();
                LblB.Text = dr[3].ToString();
                LblC.Text = dr[4].ToString();
                LblD.Text = dr[5].ToString();
                LblE.Text = dr[6].ToString();

            }
            baglanti.Close();

        }
    }
}
