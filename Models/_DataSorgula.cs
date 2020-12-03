using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using WebApp.Data;

namespace WebApp.Models
{
    public class _DataSorgula : Kalitim
    {
        private string SehirAdiGetir(string sehirid)
        {
            string adi = "";
            if (!String.IsNullOrEmpty(sehirid))
                adi = Dbc.sehirler.First(s => s.sehirId.ToString() == sehirid).sehirAdi;
            return adi;
        }

        private string IlceAdiGetir(string Ilceid)
        {
            string adi = "";
            if (!String.IsNullOrEmpty(Ilceid) & !Ilceid.Equals("0"))
                adi = Dbc.Ilceler.First(s => s.ilceId.ToString() == Ilceid).ilceAdi;
            return adi;
        }

        private string MahalleAdiGetir(string MahalleId)
        {
            string adi = "";
            if (!String.IsNullOrEmpty(MahalleId) & !MahalleId.Equals("0"))
                adi = Dbc.Mahalle.First(s => s.mahalleId.ToString() == MahalleId).mahalleAdi;
            return adi;
        }

        // Şehir Toplamı
        public int SehirToplam(string secilenIl, 
            string yasKucuk, string yasBuyuk, string radioCinsiyet)
        {
            string cmdtxt = "";
            SqlConnection conn = 
                new SqlConnection(Dbc.Database.Connection.ConnectionString);
            conn.Open();

            cmdtxt = "select count(distinct(Telefon)) from dbo.Kayitlar (nolock) where 1=1";

            string sehir = "";
            if (!String.IsNullOrEmpty(secilenIl))
            {
                sehir = " and Adresil = '" + secilenIl + "'";
            }

            string kucukyas = "";
            if (!String.IsNullOrEmpty(yasKucuk))
            {
                kucukyas = " and dbo.fnYas2(DogumTarihi, Yas) >= " + yasKucuk;
            }

            string buyukyas = "";
            if (!String.IsNullOrEmpty(yasBuyuk))
            {
                buyukyas = " and dbo.fnYas2(DogumTarihi, Yas) <= " + yasBuyuk;
            }

            string cinsiyet = "";
            if (!String.IsNullOrEmpty(radioCinsiyet))
            {
                cinsiyet = " and Cinsiyet = '" + radioCinsiyet + "'";
            }

            int count = 0;

            if (!String.IsNullOrEmpty(sehir))
            {
                cmdtxt = cmdtxt + sehir + kucukyas + buyukyas + cinsiyet;

                SqlCommand cmd = new SqlCommand(cmdtxt, conn)
                {
                    CommandTimeout = 600
                };

                count = (Int32)cmd.ExecuteScalar();
            }

            return count;
        }

        // İlçe Toplamı
        public int IlceToplam(string secilenIl, string secilenIlce,
            string yasKucuk, string yasBuyuk, string radioCinsiyet)
        {
            string cmdtxt = "";
            SqlConnection conn =
                new SqlConnection(Dbc.Database.Connection.ConnectionString);
            conn.Open();

            cmdtxt = "select count(distinct(Telefon)) from dbo.Kayitlar (nolock) where 1=1";

            string sehir = "";
            if (!String.IsNullOrEmpty(secilenIl))
            {
                sehir = " and Adresil = '" + secilenIl + "'";
            }

            string ilce = "";
            if (!String.IsNullOrEmpty(secilenIlce))
            {
                ilce = " and Adresilce = '" + secilenIlce + "'";
            }

            string kucukyas = "";
            if (!String.IsNullOrEmpty(yasKucuk))
            {
                kucukyas = " and dbo.fnYas2(DogumTarihi, Yas) >= " + yasKucuk;
            }

            string buyukyas = "";
            if (!String.IsNullOrEmpty(yasBuyuk))
            {
                buyukyas = " and dbo.fnYas2(DogumTarihi, Yas) <= " + yasBuyuk;
            }

            string cinsiyet = "";
            if (!String.IsNullOrEmpty(radioCinsiyet))
            {
                cinsiyet = " and Cinsiyet = '" + radioCinsiyet + "'";
            }

            int count = 0;

            if (!String.IsNullOrEmpty(ilce))
            {
                cmdtxt = cmdtxt + sehir + ilce + kucukyas + buyukyas + cinsiyet;

                SqlCommand cmd = new SqlCommand(cmdtxt, conn)
                {
                    CommandTimeout = 600
                };

                count = (Int32)cmd.ExecuteScalar();
            }

            return count;
        }

        // Mahalle Toplamı
        public int MahalleToplam(string secilenIl, string secilenIlce, string secilenMah,
            string yasKucuk, string yasBuyuk, string radioCinsiyet)
        {
            string cmdtxt = "";
            SqlConnection conn =
                new SqlConnection(Dbc.Database.Connection.ConnectionString);
            conn.Open();

            cmdtxt = "select count(distinct(Telefon)) from dbo.Kayitlar (nolock) where 1=1";

            string sehir = "";
            if (!String.IsNullOrEmpty(secilenIl) & !secilenIl.Equals("0"))
            {
                sehir = " and Adresil = '" + secilenIl + "'";
            }

            string ilce = "";
            if (!String.IsNullOrEmpty(secilenIlce) & !secilenIlce.Equals("0"))
            {
                ilce = " and Adresilce = '" + secilenIlce + "'";
            }

            string mahalle = "";
            if (!String.IsNullOrEmpty(secilenMah) & !secilenMah.Equals("0"))
            {
                mahalle = " and AdresMahalle = '" + secilenMah + "'";
            }

            string kucukyas = "";
            if (!String.IsNullOrEmpty(yasKucuk))
            {
                kucukyas = " and dbo.fnYas2(DogumTarihi, Yas) >= " + yasKucuk;
            }

            string buyukyas = "";
            if (!String.IsNullOrEmpty(yasBuyuk))
            {
                buyukyas = " and dbo.fnYas2(DogumTarihi, Yas) <= " + yasBuyuk;
            }

            string cinsiyet = "";
            if (!String.IsNullOrEmpty(radioCinsiyet))
            {
                cinsiyet = " and Cinsiyet = '" + radioCinsiyet + "'";
            }

            int count = 0;

            if (!String.IsNullOrEmpty(mahalle))
            {
                cmdtxt = cmdtxt + sehir + ilce + mahalle + kucukyas + buyukyas + cinsiyet;

                SqlCommand cmd = new SqlCommand(cmdtxt, conn)
                {
                    CommandTimeout = 600
                };

                count = (Int32)cmd.ExecuteScalar();
            }

            return count;
        }

        // JSON fonksiyonları
        public List<Sehir> JSonSehirler()
        {
            List<Sehir> liste = new List<Sehir>
            {
                new Sehir() { Adi = " " }
            };
            foreach (var item in Dbc.sehirler.ToList())
            {
                liste.Add(new Sehir() { Adi = item.sehirAdi });
            }
            return liste;
        }

        public class Sehir
        {
            public string Adi { get; set; }
        }

        public List<Ilce> JSonIlceler(string sehirAdi)
        {
            List<Ilce> liste = new List<Ilce>
            {
                new Ilce() { Adi = " " }
            };
            List<string> ilceler = Dbc.prIlce(sehirAdi).ToList();
            foreach (string ilce in ilceler)
            {
                liste.Add(new Ilce() { Adi = ilce });
            }
            //foreach (var item in Dbc.Ilceler.Where(i => i.sehirId == sehirId).ToList())
            //{
            //    liste.Add(new Ilce() { Id = item.ilceId, Adi = item.ilceAdi });
            //}
            return liste;
        }

        public class Ilce
        {
            public string Adi { get; set; }
        }

        public List<Mahalle> JSonMahalleler(string SehirAdi, string ilceAdi)
        {
            List<Mahalle> liste = new List<Mahalle>
            {
                new Mahalle() { Adi = " " }
            };

            List<string> mahalleler = Dbc.prMahalle(SehirAdi, ilceAdi).ToList();
            foreach (string mah in mahalleler)
            {
                liste.Add(new Mahalle() { Adi = mah });
            }
            return liste;
        }

        public List<Mahalle> JSonMahallelerYeni(int ilceId)
        {
            List<Mahalle> liste = new List<Mahalle>();

            foreach (var item in Dbc.Mahalle.Where(i => i.ilceId == ilceId).ToList())
            {
                liste.Add(new Mahalle() { Adi = item.mahalleAdi });
            }
            return liste;
        }

        public class Mahalle
        {
            public string Adi { get; set; }
        }
    }
}