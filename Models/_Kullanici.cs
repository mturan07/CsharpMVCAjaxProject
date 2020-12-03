using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Data;

namespace WebApp.Models
{
    public class _Kullanici : Kalitim
    {
        public List<SelectListItem> Seviyeler()
        {
            List<SelectListItem> seviye = new List<SelectListItem>
            {
                new SelectListItem { Value = "SuperYonetici", Text = "Süper Yönetici" },
                new SelectListItem { Value = "Yonetici", Text = "Yönetici" },
                new SelectListItem { Value = "Kullanici", Text = "Kullanıcı" }
            };

            return seviye;
        }

        public IEnumerable<LogGirisCikis> GirisCikisLogu(int? userId)
        {
            return Dbc.LogGirisCikis.Where(k => k.KulaniciId == userId).ToList();
        }

        public IEnumerable<Kullanici> KullaniciListesi()
        {
            return Dbc.Kullanici.ToList();
        }

        public Kullanici Ekle(Kullanici model)
        {
            int yeniId = Dbc.Kullanici.DefaultIfEmpty().Max(t => t == null ? 0 : t.Id + 1);

            Kullanici kullanici = new Kullanici
            {
                Id = yeniId,
                Adi = model.Adi,
                Soyadi = model.Soyadi,
                KulAdi = model.KulAdi,
                KulSifre = model.KulSifre,
                Aktif = model.Aktif,
                Seviye = model.Seviye,
                DokumAlabilsin = model.DokumAlabilsin,
                AdSoyadSorgulasin = model.AdSoyadSorgulasin,
                Ekleyen = "",
                EklemeTar = DateTime.Now
                //TCSorgulasin = model.TCSorgulasin
            };

            Dbc.Kullanici.Add(kullanici);
            Dbc.SaveChanges();

            return model;
        }

        public Kullanici Duzenle(Kullanici model)
        {
            Kullanici kullanici = Dbc.Kullanici.Find(model.Id);

            string userId = Convert.ToString(HttpContext.Current.Session["UserId"]);

            kullanici.Adi = model.Adi;
            kullanici.Soyadi = model.Soyadi;
            kullanici.KulAdi = model.KulAdi;
            kullanici.KulSifre = model.KulSifre;
            kullanici.Aktif = model.Aktif;
            kullanici.Seviye = model.Seviye;
            kullanici.DokumAlabilsin = model.DokumAlabilsin;
            kullanici.AdSoyadSorgulasin = model.AdSoyadSorgulasin;

            kullanici.Duzenleyen = userId;
            kullanici.DuzenlemeTar = DateTime.Now;

            Dbc.Entry(kullanici).State = System.Data.Entity.EntityState.Modified;
            Dbc.SaveChanges();

            return model;
        }

        public Kullanici Goster(int? id)
        {
            return Dbc.Kullanici.Find(id);
        }

        public bool Sil(int? id)
        {
            try
            {
                Dbc.Kullanici.Remove(Dbc.Kullanici.Find(id));
                Dbc.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void LogCikis(int userId)
        {
            Kullanici kullanici = Dbc.Kullanici.First(k => k.Id == userId);
            // Log Çıkış
            int yeniId = Dbc.LogGirisCikis.DefaultIfEmpty().Max(t => t == null ? 0 : t.Id + 1);
            Dbc.LogGirisCikis.Add(new LogGirisCikis()
            {
                Id = yeniId,
                KulaniciId = kullanici.Id,
                CikisTar = DateTime.Now,
                KullaniciAdi = kullanici.KulAdi
            });
            Dbc.SaveChanges();
        }
    }
}