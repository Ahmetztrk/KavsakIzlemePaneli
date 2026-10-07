using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KavsakIzlemePaneli
{
    public partial class Form1 : Form
    {
        private DataTable dtKavsaklar;
        private Random random = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // ComboBox doldurma
            cmbDurumFiltre.Items.Clear();
            cmbDurumFiltre.Items.Add("Tüm Durumlar");
            cmbDurumFiltre.Items.Add("Aşırı Yoğun");
            cmbDurumFiltre.Items.Add("Yoğun");
            cmbDurumFiltre.Items.Add("Normal");
            cmbDurumFiltre.Items.Add("Akıcı");
            cmbDurumFiltre.Items.Add("Veri Yok");
            cmbDurumFiltre.SelectedIndex = 0;

            TabloYapisiniOlustur();
            VerileriGuncelle();

            // Timer ayarı (10 saniye)
            timer1.Interval = 10000;
            timer1.Start();
        }

        private void TabloYapisiniOlustur()
        {
            dtKavsaklar = new DataTable();
            dtKavsaklar.Columns.Add("KavsakAdi", typeof(string));
            dtKavsaklar.Columns.Add("AracSayisi", typeof(int));
            dtKavsaklar.Columns.Add("Durum", typeof(string));
            dtKavsaklar.Columns.Add("Oncelik", typeof(int));
            dtKavsaklar.Columns.Add("Enlem", typeof(double));
            dtKavsaklar.Columns.Add("Boylam", typeof(double));
        }

        private void btnVeriCek_Click(object sender, EventArgs e)
        {
            VerileriGuncelle();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            btnVeriCek.PerformClick();
        }

        private void VerileriGuncelle()
        {
            if (dtKavsaklar == null) TabloYapisiniOlustur();
            dtKavsaklar.Rows.Clear();

            // İstanbul Kavşakları ve Yaklaşık Koordinatları
            var kavsakListesi = new[]
            {
                new { Ad = "Kadıköy Rıhtım Sinyalize Kavşağı", Lat = 40.9904, Lng = 29.0254 },
                new { Ad = "Beşiktaş Meydan Kavşağı", Lat = 41.0422, Lng = 29.0067 },
                new { Ad = "Taksim Meydan Kavşağı", Lat = 41.0370, Lng = 28.9850 },
                new { Ad = "Mecidiyeköy Ortaklar Caddesi", Lat = 41.0661, Lng = 28.9942 },
                new { Ad = "Maslak Dereboyu Caddesi", Lat = 41.1115, Lng = 29.0223 },
                new { Ad = "Üsküdar İskele Meydanı", Lat = 41.0270, Lng = 29.0153 },
                new { Ad = "Cevizlibağ D-100 Bağlantısı", Lat = 41.0156, Lng = 28.9142 },
                new { Ad = "Bakırköy Özgürlük Meydanı", Lat = 40.9782, Lng = 28.8741 },
                new { Ad = "Şirinevler Meydan Kavşağı", Lat = 40.9925, Lng = 28.8475 },
                new { Ad = "Göztepe Minibüs Yolu", Lat = 40.9822, Lng = 29.0571 }
            };

            foreach (var k in kavsakListesi)
            {
                string durum = "";
                int aracSayisi = 0;
                int oncelik = 5;

                int durumRastgele = random.Next(1, 6);

                switch (durumRastgele)
                {
                    case 1:
                        durum = "Aşırı Yoğun";
                        aracSayisi = random.Next(300, 500);
                        oncelik = 1;
                        break;
                    case 2:
                        durum = "Yoğun";
                        aracSayisi = random.Next(200, 300);
                        oncelik = 2;
                        break;
                    case 3:
                        durum = "Normal";
                        aracSayisi = random.Next(100, 200);
                        oncelik = 3;
                        break;
                    case 4:
                        durum = "Akıcı";
                        aracSayisi = random.Next(1, 100);
                        oncelik = 4;
                        break;
                    case 5:
                        durum = "Veri Yok";
                        aracSayisi = 0;
                        oncelik = 5;
                        break;
                }

                dtKavsaklar.Rows.Add(k.Ad, aracSayisi, durum, oncelik, k.Lat, k.Lng);
            }

            TabloyuFiltreleVeSirala();
        }

        private void cmbDurumFiltre_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabloyuFiltreleVeSirala();
        }

        private void TabloyuFiltreleVeSirala()
        {
            if (dtKavsaklar == null) return;

            DataView dv = dtKavsaklar.DefaultView;
            string secilenFiltre = cmbDurumFiltre.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(secilenFiltre) && secilenFiltre != "Tüm Durumlar")
            {
                dv.RowFilter = $"Durum = '{secilenFiltre}'";
            }
            else
            {
                dv.RowFilter = "";
            }

            dv.Sort = "Oncelik ASC";
            dgvKavsaklar.DataSource = dv.ToTable();

            string[] gizliSutunlar = { "Oncelik", "Enlem", "Boylam" };
            foreach (var col in gizliSutunlar)
            {
                if (dgvKavsaklar.Columns[col] != null)
                    dgvKavsaklar.Columns[col].Visible = false;
            }

            if (dgvKavsaklar.Columns["KavsakAdi"] != null) dgvKavsaklar.Columns["KavsakAdi"].HeaderText = "Kavşak Adı";
            if (dgvKavsaklar.Columns["AracSayisi"] != null) dgvKavsaklar.Columns["AracSayisi"].HeaderText = "Araç Sayısı";
            if (dgvKavsaklar.Columns["Durum"] != null) dgvKavsaklar.Columns["Durum"].HeaderText = "Sinyal Durumu";

            SatirlariRenklendir();
            HaritayiGuncelle();
        }

        private void SatirlariRenklendir()
        {
            foreach (DataGridViewRow row in dgvKavsaklar.Rows)
            {
                if (row.Cells["Durum"].Value == null) continue;

                string durum = row.Cells["Durum"].Value.ToString();

                switch (durum)
                {
                    case "Aşırı Yoğun":
                        row.DefaultCellStyle.BackColor = Color.DarkRed;
                        row.DefaultCellStyle.ForeColor = Color.White;
                        break;
                    case "Yoğun":
                        row.DefaultCellStyle.BackColor = Color.LightCoral;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        break;
                    case "Normal":
                        row.DefaultCellStyle.BackColor = Color.Khaki;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        break;
                    case "Akıcı":
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        break;
                    case "Veri Yok":
                        row.DefaultCellStyle.BackColor = Color.LightGray;
                        row.DefaultCellStyle.ForeColor = Color.DarkGray;
                        break;
                }
            }
        }

        private void HaritayiGuncelle()
        {
            if (webBrowser1 == null || dgvKavsaklar.DataSource == null) return;

            StringBuilder html = new StringBuilder();
            html.AppendLine("<!DOCTYPE html>");
            html.AppendLine("<html>");
            html.AppendLine("<head>");
            html.AppendLine("   <meta charset='utf-8' />");
            html.AppendLine("   <meta http-equiv='X-UA-Compatible' content='IE=edge' />");
            html.AppendLine("   <link rel='stylesheet' href='https://unpkg.com/leaflet@1.9.4/dist/leaflet.css' />");
            html.AppendLine("   <script src='https://unpkg.com/leaflet@1.9.4/dist/leaflet.js'></script>");
            html.AppendLine("   <style>html, body, #map { width: 100%; height: 100%; margin: 0; padding: 0; }</style>");
            html.AppendLine("</head>");
            html.AppendLine("<body>");
            html.AppendLine("   <div id='map'></div>");
            html.AppendLine("   <script>");
            html.AppendLine("       var map = L.map('map').setView([41.0082, 28.9784], 11);");
            html.AppendLine("       L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {");
            html.AppendLine("           maxZoom: 19,");
            html.AppendLine("           attribution: 'OpenStreetMap'");
            html.AppendLine("       }).addTo(map);");

            DataTable dtFiltreli = (DataTable)dgvKavsaklar.DataSource;

            foreach (DataRow row in dtFiltreli.Rows)
            {
                string ad = row["KavsakAdi"].ToString().Replace("'", "\\'");
                string durum = row["Durum"].ToString();
                int arac = Convert.ToInt32(row["AracSayisi"]);
                double lat = Convert.ToDouble(row["Enlem"]);
                double lng = Convert.ToDouble(row["Boylam"]);

                string renk = "gray";
                if (durum == "Aşırı Yoğun") renk = "darkred";
                else if (durum == "Yoğun") renk = "red";
                else if (durum == "Normal") renk = "orange";
                else if (durum == "Akıcı") renk = "green";

                html.AppendLine($"   L.circleMarker([{lat.ToString(System.Globalization.CultureInfo.InvariantCulture)}, {lng.ToString(System.Globalization.CultureInfo.InvariantCulture)}], {{");
                html.AppendLine($"       color: '{renk}',");
                html.AppendLine($"       fillColor: '{renk}',");
                html.AppendLine($"       fillOpacity: 0.8,");
                html.AppendLine($"       radius: 10");
                html.AppendLine($"   }}).addTo(map).bindPopup('<b>{ad}</b><br>Durum: {durum}<br>Araç Sayısı: {arac}');");
            }

            html.AppendLine("   </script>");
            html.AppendLine("</body>");
            html.AppendLine("</html>");

            webBrowser1.DocumentText = html.ToString();
        }
    }
}