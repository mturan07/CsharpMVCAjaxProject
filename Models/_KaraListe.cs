using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Data;

namespace WebApp.Models
{
    public class _KaraListe : Kalitim
    {
        public IEnumerable<Kayitlar> KayitGetir(string TelNo)
        {
            IEnumerable<Kayitlar> model = null;
            if (!String.IsNullOrEmpty(TelNo))
            {
                model = Dbc.Kayitlar.Where(k => k.Telefon == TelNo).ToList();
            }
            return model;
        }

        //public Kayitlar KayitGetir(int? id)
        //{
        //    return Dbc.Kayitlar.Where(k => k.Id == id).FirstOrDefault();
        //}

        public Kayitlar Ekle(int? id)
        {
            Kayitlar kayit = Dbc.Kayitlar.Find(id);
            kayit.KaraListe = true;
            Dbc.Entry(kayit).State = System.Data.Entity.EntityState.Modified;
            Dbc.SaveChanges();

            return kayit; 
        }

        public Kayitlar Cikar(int? id)
        {
            Kayitlar kayit = Dbc.Kayitlar.Find(id);
            kayit.KaraListe = false;
            Dbc.Entry(kayit).State = System.Data.Entity.EntityState.Modified;
            Dbc.SaveChanges();

            return kayit;
        }
    }
}