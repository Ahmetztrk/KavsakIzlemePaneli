using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace KavsakIzlemePaneli
{
    public partial class Form1 : Form
    {
        // Tüm formda kullanacağımız ana veri tablosu ve rastgele sayı üreticisi
        private DataTable dtKavsaklar;
        private Random random = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 1. ComboBox filtre seçeneklerini dolduruyoruz
            cmbDurumFiltre.Items.Clear();
            cmbDurumFiltre.Items.Add("Tüm Durumlar");
            cmbDurumFiltre.Items.Add("Aşırı Yoğun");
            cmbDurumFiltre.Items.Add("Yoğun");
            cmbDurumFiltre.Items.Add("Normal");
            cmbDurumFiltre.Items.Add("Akıcı");
            cmbDurumFiltre.Items.Add("Veri Yok");
            cmbDurumFiltre.SelectedIndex = 0;

            // 2. Tablo yapısını ilk açılışta oluşturuyoruz
            TabloYapisiniOlustur();

            // 3. Form açılır açılmaz sahadan ilk verileri çekiyoruz
            VerileriGuncelle();

            // 4. Timer'ı 10 saniyeye (10000 ms) ayarlayıp başlatıyoruz
            timer1.Interval = 10000;
            timer1.Start();
        }

        private void TabloYapisiniOlustur()
        {
            dtKavsaklar = new DataTable();
            dtKavsaklar.Columns.Add("KavsakAdi", typeof(string));
            dtKavsaklar.Columns.Add("AracSayisi", typeof(int));
            dtKavsaklar.Columns.Add("Durum", typeof(string));
            dtKavsaklar.Columns.Add("Oncelik", typeof(int)); // Sıralama için gizli sütun
        }

        private void btnVeriCek_Click(object sender, EventArgs e)
        {
            // Butona basıldığında verileri yeniler
            VerileriGuncelle();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // 10 saniyede bir otomatik olarak butona tıklar ve veriyi günceller
            btnVeriCek.PerformClick();
        }

        private void VerileriGuncelle()
        {
            if (dtKavsaklar == null) TabloYapisiniOlustur();
            dtKavsaklar.Rows.Clear();

            // İstanbul Kavşak Listesi
            string[] kavsaklar = new string[]
            {
                "Kadıköy Rıhtım Sinyalize Kavşağı", "Beşiktaş Meydan Kavşağı", "Taksim Meydan Kavşağı",
                "Kızıltoprak Minibüs Yolu Kavşağı", "Üsküdar İskele Meydanı", "Aksaray Meydan Kavşağı",
                "Topkapı Millet Caddesi", "Cevizlibağ D-100 Bağlantısı", "Bakırköy Özgürlük Meydanı",
                "Şirinevler Meydan Kavşağı", "Bahçelievler Yayla Kavşağı", "Yenibosna Kuleli Kavşağı",
                "Mecidiyeköy Ortaklar Caddesi", "Levent Gültepe Sapağı", "Maslak Dereboyu Caddesi",
                "Ortaköy Muallim Naci Caddesi", "Bebek Sahil Yolu Kavşağı", "Etiler Çamlık Sinyalize",
                "Ümraniye Çarşı Sinyalize", "Ataşehir Bulvarı Kavşağı", "Göztepe Minibüs Yolu",
                "Bostancı Sahil Yolu Kavşağı"
            };

            foreach (var kavsak in kavsaklar)
            {
                string durum = "";
                int aracSayisi = 0;
                int oncelik = 5;

                // 1 ile 5 arasında rastgele durum belirliyoruz
                int durumRastgele = random.Next(1, 6);

                switch (durumRastgele)
                {
                    case 1:
                        durum = "Aşırı Yoğun";
                        aracSayisi = random.Next(300, 500);
                        oncelik = 1; // En yüksek öncelik (En üstte görünecek)
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
                        aracSayisi = 0; // MANTIK HATASI DÜZELTİLDİ: Sinyal yoksa araç sayısı 0 olmalı!
                        oncelik = 5; // En düşük öncelik (En altta görünecek)
                        break;
                }

                dtKavsaklar.Rows.Add(kavsak, aracSayisi, durum, oncelik);
            }

            // Filtreleme ve Sıralama İşlemi
            TabloyuFiltreleVeSirala();
        }

        private void cmbDurumFiltre_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kullanıcı ComboBox'tan seçim değiştirdiğinde tabloyu yeniden filtreler
            TabloyuFiltreleVeSirala();
        }

        private void TabloyuFiltreleVeSirala()
        {
            if (dtKavsaklar == null) return;

            DataView dv = dtKavsaklar.DefaultView;
            string secilenFiltre = cmbDurumFiltre.SelectedItem?.ToString();

            // 1. Filtreleme Mantığı
            if (!string.IsNullOrEmpty(secilenFiltre) && secilenFiltre != "Tüm Durumlar")
            {
                dv.RowFilter = $"Durum = '{secilenFiltre}'";
            }
            else
            {
                dv.RowFilter = ""; // Hepsini göster
            }

            // 2. Sıralama Mantığı: Oncelik sütununa göre küçükten büyüğe (1 -> 5)
            dv.Sort = "Oncelik ASC";

            dgvKavsaklar.DataSource = dv.ToTable();

            // Oncelik sütununu ekranda gizliyoruz (Kullanıcının görmesine gerek yok)
            if (dgvKavsaklar.Columns["Oncelik"] != null)
            {
                dgvKavsaklar.Columns["Oncelik"].Visible = false;
            }

            // Sütun Başlıklarını Düzenleme
            if (dgvKavsaklar.Columns["KavsakAdi"] != null) dgvKavsaklar.Columns["KavsakAdi"].HeaderText = "Kavşak Adı";
            if (dgvKavsaklar.Columns["AracSayisi"] != null) dgvKavsaklar.Columns["AracSayisi"].HeaderText = "Araç Sayısı";
            if (dgvKavsaklar.Columns["Durum"] != null) dgvKavsaklar.Columns["Durum"].HeaderText = "Sinyal Durumu";

            // Renklendirme Mantığını Çağırıyoruz
            SatirlariRenklendir();
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
    }
}