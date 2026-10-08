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
            FiltreElemanlariniYukle();
            VerileriGuncelle();
        }

        // ComboBox içine seçenekleri kod ile otomatik ekleyen metod
        private void FiltreElemanlariniYukle()
        {
            var cmbList = Controls.Find("cmbDurumFiltre", true);
            if (cmbList.Length > 0 && cmbList[0] is ComboBox cmb)
            {
                // Olayı geçici olarak kaldırıp temizliyoruz (Tetiklenme çakışmasını önlemek için)
                cmb.SelectedIndexChanged -= cmbDurumFiltre_SelectedIndexChanged;

                if (cmb.Items.Count == 0)
                {
                    cmb.Items.Clear();
                    cmb.Items.Add("Tümü");
                    cmb.Items.Add("Aşırı Yoğun");
                    cmb.Items.Add("Yoğun");
                    cmb.Items.Add("Normal");
                    cmb.Items.Add("Akıcı");
                    cmb.Items.Add("Veri Yok");
                }

                if (cmb.SelectedIndex == -1)
                {
                    cmb.SelectedIndex = 0; // Varsayılan olarak "Tümü" seçilir
                }

                cmb.SelectedIndexChanged += cmbDurumFiltre_SelectedIndexChanged;
            }
        }

        private void btnVeriCek_Click(object sender, EventArgs e)
        {
            VerileriGuncelle();
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            VerileriGuncelle();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            VerileriGuncelle();
        }

        private void cmbDurumFiltre_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabloyuFiltreleVeSirala();
            HaritayiGuncelle();
        }

        private void VerileriGuncelle()
        {
            dtKavsaklar = new DataTable();
            dtKavsaklar.Columns.Add("Kavşak Adı", typeof(string));
            dtKavsaklar.Columns.Add("Şerit Sayısı", typeof(int));
            dtKavsaklar.Columns.Add("Toplam Araç", typeof(int));
            dtKavsaklar.Columns.Add("Şerit Başı Araç", typeof(int));
            dtKavsaklar.Columns.Add("Durum", typeof(string));
            dtKavsaklar.Columns.Add("Öncelik", typeof(int));
            dtKavsaklar.Columns.Add("Lat", typeof(double));
            dtKavsaklar.Columns.Add("Lng", typeof(double));

            var kavsakListesi = new[]
            {
                // AVRUPA YAKASI
                new { Ad = "Beşiktaş Meydan Sinyalize Kavşağı", Serit = 4, Lat = 41.0422, Lng = 29.0067 },
                new { Ad = "Taksim Meydan Sinyalize Kavşağı", Serit = 3, Lat = 41.0370, Lng = 28.9850 },
                new { Ad = "Mecidiyeköy Ortaklar Sinyalize Kavşağı", Serit = 2, Lat = 41.0661, Lng = 28.9942 },
                new { Ad = "Maslak Büyükdere Cad. Sinyalize Kavşağı", Serit = 4, Lat = 41.1115, Lng = 29.0223 },
                new { Ad = "Levent Büyükdere Cad. Sinyalize Kavşağı", Serit = 6, Lat = 41.0780, Lng = 29.0115 },
                new { Ad = "Topkapı Fetih Cad. Sinyalize Kavşağı", Serit = 3, Lat = 41.0182, Lng = 28.9248 },
                new { Ad = "Bakırköy İncirli Cad. Sinyalize Kavşağı", Serit = 2, Lat = 40.9850, Lng = 28.8685 },
                new { Ad = "Şirinevler Fetih Cad. Sinyalize Kavşağı", Serit = 3, Lat = 40.9925, Lng = 28.8475 },
                new { Ad = "Fatih Vatan Cad. Aksaray Sinyalize Kavşağı", Serit = 4, Lat = 41.0145, Lng = 28.9520 },
                new { Ad = "Beylikdüzü Sondurak Sinyalize Kavşağı", Serit = 3, Lat = 41.0125, Lng = 28.6430 },
                new { Ad = "Avcılar Reşitpaşa Cad. Sinyalize Kavşağı", Serit = 2, Lat = 40.9801, Lng = 28.7230 },
                new { Ad = "Sarıyer Hacıosman Sinyalize Kavşağı", Serit = 3, Lat = 41.1402, Lng = 29.0395 },
                new { Ad = "Zeytinburnu Bulvarı Sinyalize Kavşağı", Serit = 3, Lat = 40.9985, Lng = 28.9050 },

                // ANADOLU YAKASI
                new { Ad = "Kadıköy Rıhtım Sinyalize Kavşağı", Serit = 2, Lat = 40.9904, Lng = 29.0254 },
                new { Ad = "Üsküdar İskele Meydan Sinyalize Kavşağı", Serit = 2, Lat = 41.0270, Lng = 29.0153 },
                new { Ad = "Göztepe Minibüs Yolu Sinyalize Kavşağı", Serit = 2, Lat = 40.9822, Lng = 29.0571 },
                new { Ad = "Ataşehir Barbaros Cad. Sinyalize Kavşağı", Serit = 3, Lat = 40.9930, Lng = 29.1020 },
                new { Ad = "Ümraniye Alemdağ Cad. Sinyalize Kavşağı", Serit = 2, Lat = 41.0255, Lng = 29.0960 },
                new { Ad = "Maltepe Bağdat Cad. Sinyalize Kavşağı", Serit = 2, Lat = 40.9250, Lng = 29.1310 },
                new { Ad = "Pendik E-5 Yanyol Sinyalize Kavşağı", Serit = 3, Lat = 40.8785, Lng = 29.2315 }
            };

            foreach (var k in kavsakListesi)
            {
                int toplamArac = random.Next(10, 600);
                int seritBasiArac = toplamArac / k.Serit;

                string durum = "";
                int oncelik = 5;

                if (random.Next(1, 11) == 10)
                {
                    durum = "Veri Yok";
                    toplamArac = 0;
                    seritBasiArac = 0;
                    oncelik = 5;
                }
                else
                {
                    if (seritBasiArac >= 110)
                    {
                        durum = "Aşırı Yoğun";
                        oncelik = 1;
                    }
                    else if (seritBasiArac >= 75)
                    {
                        durum = "Yoğun";
                        oncelik = 2;
                    }
                    else if (seritBasiArac >= 40)
                    {
                        durum = "Normal";
                        oncelik = 3;
                    }
                    else
                    {
                        durum = "Akıcı";
                        oncelik = 4;
                    }
                }

                dtKavsaklar.Rows.Add(k.Ad, k.Serit, toplamArac, seritBasiArac, durum, oncelik, k.Lat, k.Lng);
            }

            TabloyuFiltreleVeSirala();
            HaritayiGuncelle();
        }

        private void TabloyuFiltreleVeSirala()
        {
            if (dtKavsaklar == null) return;

            DataView dv = dtKavsaklar.DefaultView;
            dv.Sort = "Öncelik ASC";

            var cmbList = Controls.Find("cmbDurumFiltre", true);
            if (cmbList.Length > 0 && cmbList[0] is ComboBox cmb && cmb.SelectedItem != null)
            {
                string secilenDurum = cmb.SelectedItem.ToString();
                if (secilenDurum != "Tümü")
                {
                    dv.RowFilter = $"Durum = '{secilenDurum}'";
                }
                else
                {
                    dv.RowFilter = "";
                }
            }

            var dgvList = Controls.Find("dgvKavsaklar", true);
            if (dgvList.Length == 0)
            {
                dgvList = Controls.Find("dataGridView1", true);
            }

            if (dgvList.Length > 0 && dgvList[0] is DataGridView dgv)
            {
                dgv.DataSource = dv;

                // Lat, Lng ve Öncelik Sütunlarını Tabloda Gizleme
                if (dgv.Columns["Lat"] != null) dgv.Columns["Lat"].Visible = false;
                if (dgv.Columns["Lng"] != null) dgv.Columns["Lng"].Visible = false;
                if (dgv.Columns["Öncelik"] != null) dgv.Columns["Öncelik"].Visible = false;

                TabloyuRenklendir(dgv);
            }

            var lblList = Controls.Find("lblDurum", true);
            if (lblList.Length > 0 && lblList[0] is Label lbl)
            {
                lbl.Text = $"Son Güncelleme: {DateTime.Now:HH:mm:ss} | Toplam {dv.Count} Kavşak Listeleniyor.";
            }
        }

        private void TabloyuRenklendir(DataGridView dgv)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells["Durum"].Value == null) continue;

                string durum = row.Cells["Durum"].Value.ToString();

                Color arkaPlan = Color.White;
                Color yaziRengi = Color.Black;

                switch (durum)
                {
                    case "Aşırı Yoğun":
                        arkaPlan = Color.FromArgb(248, 215, 218);
                        yaziRengi = Color.FromArgb(132, 32, 41);
                        break;
                    case "Yoğun":
                        arkaPlan = Color.FromArgb(255, 243, 205);
                        yaziRengi = Color.FromArgb(102, 77, 3);
                        break;
                    case "Normal":
                        arkaPlan = Color.FromArgb(209, 231, 221);
                        yaziRengi = Color.FromArgb(15, 81, 50);
                        break;
                    case "Akıcı":
                        arkaPlan = Color.FromArgb(207, 226, 255);
                        yaziRengi = Color.FromArgb(8, 66, 152);
                        break;
                    case "Veri Yok":
                        arkaPlan = Color.FromArgb(226, 227, 229);
                        yaziRengi = Color.FromArgb(65, 70, 75);
                        break;
                }

                row.DefaultCellStyle.BackColor = arkaPlan;
                row.DefaultCellStyle.ForeColor = yaziRengi;
                row.DefaultCellStyle.SelectionBackColor = arkaPlan;
                row.DefaultCellStyle.SelectionForeColor = yaziRengi;
            }
        }

        private void HaritayiGuncelle()
        {
            var wbList = Controls.Find("webBrowser1", true);
            if (wbList.Length == 0 || !(wbList[0] is WebBrowser wb)) return;

            DataView dv = dtKavsaklar.DefaultView;

            var cmbList = Controls.Find("cmbDurumFiltre", true);
            if (cmbList.Length > 0 && cmbList[0] is ComboBox cmb && cmb.SelectedItem != null)
            {
                string secilenDurum = cmb.SelectedItem.ToString();
                if (secilenDurum != "Tümü")
                {
                    dv.RowFilter = $"Durum = '{secilenDurum}'";
                }
                else
                {
                    dv.RowFilter = "";
                }
            }

            StringBuilder html = new StringBuilder();
            html.AppendLine("<!DOCTYPE html>");
            html.AppendLine("<html>");
            html.AppendLine("<head>");
            html.AppendLine("<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" />");
            html.AppendLine("<meta charset=\"utf-8\" />");
            html.AppendLine("<link rel=\"stylesheet\" href=\"https://unpkg.com/leaflet@1.9.4/dist/leaflet.css\" />");
            html.AppendLine("<script src=\"https://unpkg.com/leaflet@1.9.4/dist/leaflet.js\"></script>");
            html.AppendLine("<style>");
            html.AppendLine("html, body, #map { height: 100%; margin: 0; padding: 0; font-family: Arial, sans-serif; }");
            html.AppendLine(".marker-pin { width: 30px; height: 30px; border-radius: 50% 50% 50% 0; position: absolute; transform: rotate(-45deg); left: 50%; top: 50%; margin: -15px 0 0 -15px; border: 1px solid #FFFFFF; box-shadow: 0px 2px 4px rgba(0,0,0,0.4); }");
            html.AppendLine(".marker-pin::after { content: ''; width: 14px; height: 14px; margin: 8px 0 0 8px; background: #fff; position: absolute; border-radius: 50%; }");
            html.AppendLine(".bg-asiri-yogun { background: #e74c3c; }");
            html.AppendLine(".bg-yogun { background: #e67e22; }");
            html.AppendLine(".bg-normal { background: #2ecc71; }");
            html.AppendLine(".bg-akici { background: #3498db; }");
            html.AppendLine(".bg-veri-yok { background: #7f8c8d; }");
            html.AppendLine("</style>");
            html.AppendLine("</head>");
            html.AppendLine("<body>");
            html.AppendLine("<div id=\"map\"></div>");
            html.AppendLine("<script>");
            html.AppendLine("var map = L.map('map').setView([41.0082, 28.9784], 11);");
            html.AppendLine("L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', { maxZoom: 19, attribution: 'OpenStreetMap' }).addTo(map);");

            foreach (DataRowView rowView in dv)
            {
                DataRow row = rowView.Row;
                string ad = row["Kavşak Adı"].ToString().Replace("'", "\\'");
                string durum = row["Durum"].ToString();
                int serit = Convert.ToInt32(row["Şerit Sayısı"]);
                int toplamArac = Convert.ToInt32(row["Toplam Araç"]);
                int seritBasi = Convert.ToInt32(row["Şerit Başı Araç"]);
                double lat = Convert.ToDouble(row["Lat"]);
                double lng = Convert.ToDouble(row["Lng"]);

                string pinClass = "bg-veri-yok";
                if (durum == "Aşırı Yoğun") pinClass = "bg-asiri-yogun";
                else if (durum == "Yoğun") pinClass = "bg-yogun";
                else if (durum == "Normal") pinClass = "bg-normal";
                else if (durum == "Akıcı") pinClass = "bg-akici";

                html.AppendLine($"var customIcon = L.divIcon({{" +
                    $"className: 'custom-div-icon'," +
                    $"html: \"<div class='marker-pin {pinClass}'></div>\"," +
                    $"iconSize: [30, 42]," +
                    $"iconAnchor: [15, 42]" +
                    $"}});");

                string popupContent = $"<b>{ad}</b><br/>" +
                                     $"<b>Durum:</b> {durum}<br/>" +
                                     $"<b>Şerit Sayısı:</b> {serit}<br/>" +
                                     $"<b>Toplam Araç:</b> {toplamArac}<br/>" +
                                     $"<b>Şerit Başı Araç:</b> {seritBasi}";

                html.AppendLine($"L.marker([{lat.ToString(System.Globalization.CultureInfo.InvariantCulture)}, {lng.ToString(System.Globalization.CultureInfo.InvariantCulture)}], {{icon: customIcon}})" +
                    $".addTo(map).bindPopup('{popupContent}');");
            }

            html.AppendLine("</script>");
            html.AppendLine("</body>");
            html.AppendLine("</html>");

            wb.DocumentText = html.ToString();
        }
    }
}