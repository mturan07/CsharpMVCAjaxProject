using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Data;
using WebApp.Models;
using WebApp.ViewModel;

namespace WebApp.Controllers
{
    [ControlLogin]

    public class PortalController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GirisCikis(int? id)
        {
            return View(new _Kullanici().GirisCikisLogu(id));
        }

        public ActionResult KaraListe()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KaraListe(string telefonno)
        {
            return View(new _KaraListe().KayitGetir(telefonno));
        }

        public ActionResult KaraListeEkle(int kayitId)
        {
            Kayitlar kayit = new _KaraListe().Ekle(kayitId);
            string tel = kayit.Telefon;
            ViewBag.Mesaj = tel + " nolu telefon karalisteye eklendi!";
            return RedirectToAction("KaraListe");
            //return RedirectToAction("KaraListe", "Portal", new { telefonno = kayit.Telefon } );
        }

        public ActionResult KaraListeCikar(int kayitId)
        {
            Kayitlar kayit = new _KaraListe().Cikar(kayitId);
            string tel = kayit.Telefon;
            ViewBag.Mesaj = tel + " nolu telefon karalisteden çıkarıldı!";
            return RedirectToAction("KaraListe");
            //return Redirect("/Portal/KaraListe?id=" + kayitId.ToString() + "&telefonno=" + tel);
        }

        public ActionResult Test()
        {
            return View();
        }

        public ActionResult ExportData(string dosyaadi)
        {
            //string data = "";
            //Response.Clear();
            //Response.ContentType = "application/CSV";
            //Response.AddHeader("content-disposition", "attachment; filename=C:\\Csv\\" + dosyaadi);
            //Response.Write(data);
            //Response.End();

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=\"" + dosyaadi + "\"");
            Response.TransmitFile(Path.Combine(@"C:\Csv\", dosyaadi));

            return new EmptyResult();
        }

        //public ActionResult DownloadFile(string dosyaadi, string sqltxt)
        public ActionResult DownloadFile(string dosyaadi, string kuladi, object[] function_param)
        {
            string sqltxt = "";
            foreach (var item in function_param)
            {
                sqltxt += " " + item;
            }

            new _DataDokum().CSVOlustur(dosyaadi, kuladi, sqltxt);

            /*
            string filename = dosyaadi;
            string filepath = @"C:\Csv\" + dosyaadi;
                //AppDomain.CurrentDomain.BaseDirectory + "/Path/To/File/" + filename;
            byte[] filedata = System.IO.File.ReadAllBytes(filepath);
            string contentType = MimeMapping.GetMimeMapping(filepath);

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = filename,
                Inline = true,
            };

            Response.Clear();
            Response.ContentType = "application/CSV";
            Response.AppendHeader("Content-Disposition", cd.ToString());
            //Response.AddHeader("content-disposition", "attachment; filename=\"" + exportReport + ".csv\"");
            //Response.Write(data);
            Response.End();

            System.IO.File.Delete(filepath);

            return File(filedata, contentType);
            */

            return new EmptyResult();
        }

        public ActionResult KullaniciListesi()
        {
            return View(new _Kullanici().KullaniciListesi());
        }

        public ActionResult Kullanici()
        {
            return View(new _Kullanici().KullaniciListesi());
        }

        public ActionResult KullaniciDuzenle(int? id)
        {
            ViewBag.Seviyeler = new _Kullanici().Seviyeler();
            return View(new _Kullanici().Goster(id));
        }

        [HttpPost]
        public ActionResult KullaniciDuzenle(Kullanici model)
        {
            ViewBag.Seviyeler = new _Kullanici().Seviyeler();
            return View(new _Kullanici().Duzenle(model));
        }

        public ActionResult KullaniciEkle()
        {
            string userId = Convert.ToString(HttpContext.Session["UserId"]);
            ViewBag.Seviyeler = new _Kullanici().Seviyeler();
            return View(new Kullanici() {
                EklemeTar = DateTime.Now, Ekleyen = userId,
                Seviye = "Kullanici",
                TCSorgulasin = false, AdSoyadSorgulasin = false, DokumAlabilsin = false
            } );
        }

        [HttpPost]
        public ActionResult KullaniciEkle(Kullanici model)
        {
            ViewBag.Seviyeler = new _Kullanici().Seviyeler();
            return View(new _Kullanici().Ekle(model));
        }

        public ActionResult DataSayilari()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DataSayilari(string secilenIl, string secilenIlce, string secilenMah,
            string yasKucuk, string yasBuyuk, string radioCinsiyet)
        {
            if (String.IsNullOrEmpty(secilenIl))
                ViewBag.SehirToplam = 0;
            else
                ViewBag.SehirToplam = new _DataSorgula().SehirToplam(secilenIl,
                    yasKucuk, yasBuyuk, radioCinsiyet);

            if (String.IsNullOrEmpty(secilenIlce))
                ViewBag.IlceToplam = 0;
            else
                ViewBag.IlceToplam = new _DataSorgula().IlceToplam(secilenIl, secilenIlce,
                yasKucuk, yasBuyuk, radioCinsiyet);

            // Gelecek olan mahalle listesinin döngüsü şeklinde olacak

            //Hashtable mahListe = new Hashtable();
            //mahListe.Add(99, "Muratpaşa");

            //if (String.IsNullOrEmpty(secilenMah))
            //    ViewBag.MahalleToplam = 0;
            //else
            //    ViewBag.MahalleToplam = new _DataSorgula().MahalleToplam(secilenIl, secilenIlce, secilenMah,
            //    yasKucuk, yasBuyuk, radioCinsiyet);

            return View();
        }

        public ActionResult DataDokum()
        {
            ViewBag.KulAdi = HttpContext.Session["UserName"].ToString();
            return View();
        }

        [HttpPost]
        public ActionResult DataDokum(string secilenIl, string secilenIlce, string secilenMah,
            string yasKucuk, string yasBuyuk, string radioCinsiyet, int adet)
        {
            if (String.IsNullOrEmpty(secilenIl))
                ViewBag.SehirToplam = 0;
            else
                ViewBag.SehirToplam = new _DataSorgula().SehirToplam(secilenIl,
                    yasKucuk, yasBuyuk, radioCinsiyet);

            if (String.IsNullOrEmpty(secilenIlce))
                ViewBag.IlceToplam = 0;
            else
                ViewBag.IlceToplam = new _DataSorgula().IlceToplam(secilenIl, secilenIlce,
                yasKucuk, yasBuyuk, radioCinsiyet);

            // Gelecek olan mahalle listesinin döngüsü şeklinde olacak

            //Hashtable mahListe = new Hashtable();
            //mahListe.Add(99, "Muratpaşa");

            //if (String.IsNullOrEmpty(secilenMah))
            //    ViewBag.MahalleToplam = 0;
            //else
            //    ViewBag.MahalleToplam = new _DataSorgula().MahalleToplam(secilenIl, secilenIlce, secilenMah,
            //    yasKucuk, yasBuyuk, radioCinsiyet);

            return View();
        }

        public ActionResult AdSoyadAra()
        {
            ViewBag.inputAdi = "";
            ViewBag.inputSoyadi = "";
            ViewBag.secIl = "";
            ViewBag.secIlce = "";
            return View();
        }

        [HttpPost]
        public ActionResult AdSoyadAra(string inputAdi, string inputSoyadi, string secilenIl, string secilenIlce)
        {
            if (secilenIl.Equals("0"))
            {
                //ViewBag.Hata = "İl seçimi yapmalısınız!";
                return View();
            }
            else
            {
                ViewBag.Hata = "";
                ViewBag.inputAdi = inputAdi;
                ViewBag.inputSoyadi = inputSoyadi;
                ViewBag.secIl = secilenIl;
                ViewBag.secIlce = secilenIlce;

                return View(new _AdSoyadAra().AdSoyadAra(inputAdi, inputSoyadi, secilenIl, secilenIlce));
            }
        }
        public ActionResult TelefonNoAra()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TelefonNoAra(string telefonno)
        {
            return View(new _TelefonNoAra().TelefonNoKayitlari(telefonno));
        }

        public ActionResult DataSorgula()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DataSorgula(string secilenIl, string secilenIlce, string secilenMah,
            string yasKucuk, string yasBuyuk, string radioCinsiyet)
        {
            if (String.IsNullOrEmpty(secilenIl))
                ViewBag.SehirToplam = 0;
            else
                ViewBag.SehirToplam = new _DataSorgula().SehirToplam(secilenIl,
                    yasKucuk, yasBuyuk, radioCinsiyet);

            if (String.IsNullOrEmpty(secilenIlce))
                ViewBag.IlceToplam = 0;
            else
                ViewBag.IlceToplam = new _DataSorgula().IlceToplam(secilenIl, secilenIlce,
                yasKucuk, yasBuyuk, radioCinsiyet);

            if (String.IsNullOrEmpty(secilenMah))
                ViewBag.MahalleToplam = 0;
            else
                ViewBag.MahalleToplam = new _DataSorgula().MahalleToplam(secilenIl, secilenIlce, secilenMah,
                yasKucuk, yasBuyuk, radioCinsiyet);

            return View();
        }

        //public ActionResult DataSorgula(string sehirId = "", int ilceId = 0)
        //{
        //    ViewModelDataSorgula model = new ViewModelDataSorgula
        //    {
        //        Sehirler = new _DataSorgula().IlListesi(),
        //        Ilceler = new _DataSorgula().IlceListesi(sehirId),
        //        Mahalleler = new _DataSorgula().MahalleListesi(ilceId)
        //    };
        //    return View(model);
        //}

        public int SehirToplami(string secilenIl,
            string yasKucuk, string yasBuyuk, string radioCinsiyet)
        {
            int toplam = 0;
            if (String.IsNullOrEmpty(secilenIl))
                toplam = 0;
            else
                toplam = new _DataSorgula().SehirToplam(
                    secilenIl, yasKucuk, yasBuyuk, radioCinsiyet);
            return toplam;
        }

        public int IlceToplami(string secilenIl, string secilenIlce,
            string yasKucuk, string yasBuyuk, string radioCinsiyet)
        {
            int toplam = 0;
            if (String.IsNullOrEmpty(secilenIl))
                toplam = 0;
            else
                toplam = new _DataSorgula().IlceToplam(
                    secilenIl, secilenIlce, yasKucuk, yasBuyuk, radioCinsiyet);
            return toplam;
        }

        public int MahalleToplami(string secilenIl, string secilenIlce, string secilenMah,
            string yasKucuk, string yasBuyuk, string radioCinsiyet)
        {
            int toplam = 0;
            if (String.IsNullOrEmpty(secilenIl))
                toplam = 0;
            else
                toplam = new _DataSorgula().MahalleToplam(
                    secilenIl, secilenIlce, secilenMah, yasKucuk, yasBuyuk, radioCinsiyet);
            return toplam;
        }

        public JsonResult SehirListesi()
        {
            return Json(new _DataSorgula().JSonSehirler(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IlceListesi(string sehirAdi)
        {
            return Json(new _DataSorgula().JSonIlceler(sehirAdi), JsonRequestBehavior.AllowGet);
        }

        public JsonResult MahalleListesi(string sehirAdi, string ilceAdi)
        {
            return Json(new _DataSorgula().JSonMahalleler(sehirAdi, ilceAdi), JsonRequestBehavior.AllowGet);
        }

        public JsonResult MahalleListesiYeni(int ilceId)
        {
            return Json(new _DataSorgula().JSonMahallelerYeni(ilceId), JsonRequestBehavior.AllowGet);
        }

        #region Ornek
        public ActionResult Basic()
        {
            return View();
        }

        public ActionResult Advanced()
        {
            return View();
        }
        #endregion
    }
}