using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Data;

namespace WebApp.Models
{
    public class _Giris : Kalitim
    {
        public Kullanici IsLoginSuccess(string user, string pass)
        {
            //Kullanici resultUser = Dbc.Kullanici.FirstOrDefault();
            Kullanici resultUser = Dbc.Kullanici.Where(k => k.KulAdi.Equals(user) && k.KulSifre.Equals(pass)).FirstOrDefault();
            if (resultUser != null)
            {
                resultUser.SonGirisTar = DateTime.Now;
                Dbc.Entry(resultUser).State = System.Data.Entity.EntityState.Modified;
                Dbc.SaveChanges();

                // Log Giriş
                int yeniId = Dbc.LogGirisCikis.DefaultIfEmpty().Max(t => t == null ? 0 : t.Id + 1);
                Dbc.LogGirisCikis.Add(new LogGirisCikis()
                {
                    Id = yeniId,
                    KulaniciId = resultUser.Id,
                    GirisTar = DateTime.Now,
                    KullaniciAdi = resultUser.KulAdi
                });

                Dbc.SaveChanges();

                HttpContext.Current.Session.Add("UserId", resultUser.Id.ToString());
                HttpContext.Current.Session.Add("UserName", resultUser.KulAdi.ToString());
                HttpContext.Current.Session.Add("UserLevel", resultUser.Seviye.ToString());
                //HttpContext.Current.Session.Add("TCSorgulasin", resultUser.TCSorgulasin);
                HttpContext.Current.Session.Add("DokumAlabilsin", resultUser.DokumAlabilsin);
                HttpContext.Current.Session.Add("AdSoyadSorgulasin", resultUser.AdSoyadSorgulasin);
                return resultUser;
            }
            return null;
        }
    }
}