using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Data;

namespace WebApp.Models
{
    public class _TelefonNoAra : Kalitim
    {
        public IEnumerable<Kayitlar> TelefonNoKayitlari(string telefonno)
        {
            return Dbc.Kayitlar.Where(k => k.Telefon == telefonno).Take(1).ToList();
        }

        /*
        public IEnumerable<Kayitlar> TelefonNoKayitlari(string telefonno)
        {
            List<Kayitlar> liste = new List<Kayitlar>();

            if (!String.IsNullOrEmpty(telefonno))
            {
                var kayitlar = Dbc.prTelefonBul(telefonno);

                foreach (prTelefonBul_Result item in kayitlar)
                {
                    liste.Add(new Kayitlar()
                    {
                        Telefon = item.Telefon,
                        Adi = item.Adi,
                        Soyadi = item.Soyadi,
                        Cinsiyet = item.Cinsiyet,
                        DogumYeri = item.DogumYeri,
                        DogumTarihi = item.DogumTarihi,
                        Nufusil = item.Nufusil,
                        Nufusilce = item.Nufusilce,
                        Adresil = item.Adresil,
                        Adresilce = item.Adresilce,
                        AdresMahalle = item.AdresMahalle,
                        AdresBelde = item.AdresBelde,
                        AdresCaddeSokak = item.AdresCaddeSokak,
                        AdresKapiNo = item.AdresKapiNo,
                        AdresDaire = item.AdresDaire,
                        KaraListe = item.KaraListe
                    });
                }
            }

            return liste;
        }
        */
    }
}