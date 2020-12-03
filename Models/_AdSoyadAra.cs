using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApp.Data;

namespace WebApp.Models
{
    public class _AdSoyadAra : Kalitim
    {
        private string IlAdiGetir(string ilKodu)
        {
            return Dbc.sehirler.First(s => s.sehirId.ToString() == ilKodu).sehirAdi;
        }

        private string IlceAdiGetir(string ilceKodu)
        {
            return Dbc.Ilceler.First(s => s.ilceId.ToString() == ilceKodu).ilceAdi;
        }

        public IEnumerable<Kayitlar> AdSoyadAra(
            string adi, string soyadi, string iladi, string ilceadi)
        {
            List<Kayitlar> liste = new List<Kayitlar>();

            SqlConnection conn =
                new SqlConnection(Dbc.Database.Connection.ConnectionString);
            conn.Open();

            string sqltxt = "SELECT * FROM dbo.Kayitlar (NOLOCK) WHERE 1 = 1 ";

            if (!String.IsNullOrEmpty(iladi) && !iladi.Equals("0"))
            {
                sqltxt += " AND Adresil LIKE '" + IlAdiGetir(iladi) + "'";
            }
            if (!String.IsNullOrEmpty(ilceadi) && !ilceadi.Equals("0"))
            {
                sqltxt += " AND Adresilce LIKE '" + IlceAdiGetir(ilceadi) + "'";
            }
            if (!String.IsNullOrEmpty(adi))
            {
                sqltxt += " AND Adi LIKE '%" + adi + "%'";
            }
            if (!String.IsNullOrEmpty(soyadi))
            {
                sqltxt += " AND Soyadi LIKE '%" + soyadi + "%'";
            }

            SqlCommand cmd = new SqlCommand(sqltxt, conn)
            {
                CommandTimeout = 600
            };

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                DateTime? dogumTarihi = null;

                liste.Add(
                    new Kayitlar() {
                        Id = Convert.ToInt32(dr["Id"]),
                        Adi = (dr["Adi"] == DBNull.Value ? "" : dr["Adi"].ToString()),
                        Soyadi = (dr["Soyadi"] == DBNull.Value ? "" : dr["Soyadi"].ToString()),
                        Cinsiyet = (dr["Cinsiyet"] == DBNull.Value ? "" : dr["Cinsiyet"].ToString()),
                        DogumYeri = (dr["DogumYeri"] == DBNull.Value ? "" : dr["DogumYeri"].ToString()),
                        DogumTarihi = (dr["DogumTarihi"] == DBNull.Value ? dogumTarihi : Convert.ToDateTime(dr["DogumTarihi"])),
                        Telefon = (dr["Telefon"] == DBNull.Value ? "" : dr["Telefon"].ToString()),
                        Nufusil = (dr["Nufusil"] == DBNull.Value ? "" : dr["Nufusil"].ToString()),
                        Nufusilce = (dr["Nufusilce"] == DBNull.Value ? "" : dr["Nufusilce"].ToString()),
                        Adresil = (dr["Adresil"] == DBNull.Value ? "" : dr["Adresil"].ToString()),
                        Adresilce = (dr["Adresilce"] == DBNull.Value ? "" : dr["Adresilce"].ToString()),
                        AdresBelde = (dr["AdresBelde"] == DBNull.Value ? "" : dr["AdresBelde"].ToString()),
                        AdresMahalle = (dr["AdresMahalle"] == DBNull.Value ? "" : dr["AdresMahalle"].ToString()),
                        AdresCaddeSokak = (dr["AdresCaddeSokak"] == DBNull.Value ? "" : dr["AdresCaddeSokak"].ToString()),
                        AdresKapiNo = (dr["AdresKapiNo"] == DBNull.Value ? "" : dr["AdresKapiNo"].ToString()),
                        AdresDaire = (dr["AdresDaire"] == DBNull.Value ? "" : dr["AdresDaire"].ToString()),
                        KaraListe = (dr["KaraListe"] == DBNull.Value ? false : Convert.ToBoolean(dr["KaraListe"]))
                    });
            }

            return liste;
        }
    }
}