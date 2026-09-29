using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace KavsakIzlemePaneli
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // ComboBox filtre seçeneklerini dolduruyoruz
            cmbDurumFiltre.Items.Clear();
            cmbDurumFiltre.Items.Add("Tüm Durumlar");
            cmbDurumFiltre.Items.Add("Akıcı");
            cmbDurumFiltre.Items.Add("Normal");
            cmbDurumFiltre.Items.Add("Yoğun");
            cmbDurumFiltre.Items.Add("Aşırı Yoğun");
            cmbDurumFiltre.Items.Add("Veri Yok");

            // Varsayılan olarak "Tüm Durumlar" seçilsin
            cmbDurumFiltre.SelectedIndex = 0;
        }

        private void cmbDurumFiltre_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Filtre seçimi değiştiğinde verileri otomatik olarak tekrar çekip süzer
            btnVeriCek.PerformClick();
        }

        private void btnVeriCek_Click(object sender, EventArgs e)
        {
            // 1. Tablo Yapısını Oluşturma
            DataTable dt = new DataTable();
            dt.Columns.Add("Kavşak Adı", typeof(string));
            dt.Columns.Add("Araç Sayısı", typeof(int));
            dt.Columns.Add("Sinyal Durumu", typeof(string));

            Random rnd = new Random();
            string[] durumlar = { "Akıcı", "Normal", "Yoğun", "Aşırı Yoğun", "Veri Yok" };

            // 2. 50 Adet İstanbul Sinyalize Kavşak Listesi
            string[] istanbulSinyalKavsaklari = {
                "Kadıköy Rıhtım Sinyalize Kavşağı",
                "Beşiktaş Meydan Akaretler Sinyalize Kavşağı",
                "Taksim Meydan Tünel Giriş Sinyalize Kavşağı",
                "Kızıltoprak Minibüs Caddesi Sinyalize Kavşağı",
                "Üsküdar İskele Meydan Sinyalize Kavşağı",
                "Aksaray Meydan Valilik Önü Sinyalize Kavşağı",
                "Topkapı Millet Caddesi Sinyalize Kavşağı",
                "Cevizlibağ D-100 Yanyol Sinyalize Kavşağı",
                "Bakırköy Özgürlük Meydanı Sinyalize Kavşağı",
                "Şirinevler Meydan Sinyalize Kavşağı",
                "Bahçelievler Yayla Meydan Sinyalize Kavşağı",
                "Yenibosna Kuleli Sinyalize Kavşağı",
                "Mecidiyeköy Ortaklar Caddesi Sinyalize Kavşağı",
                "Levent Gültepe Sapağı Sinyalize Kavşağı",
                "Maslak Dereboyu Caddesi Sinyalize Kavşağı",
                "Ortaköy Muallim Naci Caddesi Sinyalize Kavşağı",
                "Bebek Sahil Yolu Akıntıburnu Sinyalize Kavşağı",
                "Etiler Çamlık Sinyalize Kavşağı",
                "Ümraniye Çarşı Son Durak Sinyalize Kavşağı",
                "Ataşehir Bulvarı Mozaik Sinyalize Kavşağı",
                "Göztepe Minibüs Caddesi Sinyalize Kavşağı",
                "Bostancı Sahil Yolu İskele Sinyalize Kavşağı",
                "Maltepe Meydan Camii Önü Sinyalize Kavşağı",
                "Kartal Baba Hakkı Caddesi Sinyalize Kavşağı",
                "Pendik Doğu Mahallesi Sinyalize Kavşağı",
                "Beykoz Sahil Yolu Ortaçeşme Sinyalize Kavşağı",
                "Sarıyer Hacıosman Metro Çıkışı Sinyalize Kavşağı",
                "Kâğıthane Cendere Caddesi Sinyalize Kavşağı",
                "Alibeyköy Vardar Caddesi Sinyalize Kavşağı",
                "Eyüpsultan Bulvarı Camii Önü Sinyalize Kavşağı",
                "Gaziosmanpaşa Meydan Sinyalize Kavşağı",
                "Bayrampaşa Demirkapı Caddesi Sinyalize Kavşağı",
                "Esenler Dörtyol Meydanı Sinyalize Kavşağı",
                "Bağcılar Meydan Sinyalize Kavşağı",
                "Güngören Kale Merkez Önü Sinyalize Kavşağı",
                "Zeytinburnu Bulvar Sinyalize Kavşağı",
                "Fatih Vatan Caddesi Emniyet Önü Sinyalize Kavşağı",
                "Eminönü Mısır Çarşısı Önü Sinyalize Kavşağı",
                "Karaköy Kemeraltı Caddesi Sinyalize Kavşağı",
                "Şişli Camii Önü Sinyalize Kavşağı",
                "Sarıyer Çayırbaşı Sinyalize Kavşağı",
                "İkitelli Organize Sanayi Bölgesi Sinyalize Kavşağı",
                "Küçükçekmece Cennet Mahallesi Sinyalize Kavşağı",
                "Avcılar Reşitpaşa Caddesi Sinyalize Kavşağı",
                "Beylikdüzü Belediye Önü Sinyalize Kavşağı",
                "Esenyurt Doğan Araslı Bulvarı Sinyalize Kavşağı",
                "Büyükçekmece Sahil Giriş Sinyalize Kavşağı",
                "Tuzla İçmeler Sinyalize Kavşağı",
                "Sancaktepe Samandıra Sinyalize Kavşağı",
                "Sultanbeyli Kent Meydanı Sinyalize Kavşağı"
            };

            // 3. Tabloyu Rastgele Verilerle Doldurma
            for (int i = 0; i < 50; i++)
            {
                string kavsakAdi = istanbulSinyalKavsaklari[i];
                int aracSayisi = rnd.Next(15, 280);
                string durum = durumlar[rnd.Next(durumlar.Length)];

                dt.Rows.Add(kavsakAdi, aracSayisi, durum);
            }

            // 4. ComboBox Süzme Mantığı
            if (cmbDurumFiltre.SelectedItem != null)
            {
                string secilenDurum = cmbDurumFiltre.SelectedItem.ToString();

                if (secilenDurum != "Tüm Durumlar")
                {
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = $"[Sinyal Durumu] = '{secilenDurum}'";
                    dgvKavsaklar.DataSource = dv;
                }
                else
                {
                    dgvKavsaklar.DataSource = dt;
                }
            }
            else
            {
                dgvKavsaklar.DataSource = dt;
            }

            // 5. Hücre Renklendirme Mantığı
            foreach (DataGridViewRow row in dgvKavsaklar.Rows)
            {
                if (row.Cells["Sinyal Durumu"].Value != null)
                {
                    string durum = row.Cells["Sinyal Durumu"].Value.ToString();

                    if (durum == "Aşırı Yoğun")
                    {
                        row.DefaultCellStyle.BackColor = Color.DarkRed;
                        row.DefaultCellStyle.ForeColor = Color.White;
                    }
                    else if (durum == "Yoğun")
                    {
                        row.DefaultCellStyle.BackColor = Color.LightCoral;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else if (durum == "Normal")
                    {
                        row.DefaultCellStyle.BackColor = Color.Khaki;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else if (durum == "Akıcı")
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else if (durum == "Veri Yok")
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGray;
                        row.DefaultCellStyle.ForeColor = Color.DarkGray;
                    }
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Boş kalabilir
        }

        private void dgvKavsaklar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Boş kalabilir
        }
    }
}