document.getElementById('btnHazirla').addEventListener("click", DosyaKaydet);

function DosyaKaydet(e) {
    e.preventDefault();

    var sehir = document.getElementById("secilenIl");
    var ilAdi = sehir.options[sehir.selectedIndex].text;

    const cinsiyet = document.querySelector('input[name="radioCinsiyet"]:checked').value;
    const yaskucuk = document.getElementById('yasKucuk').value;
    const yasbuyuk = document.getElementById('yasBuyuk').value;
    const adet = document.getElementById('girilenAdet').value;

    if (sehir === null) {
        alert("Lütfen il seçiniz!");
        return;
    }

    if (adet === null) {
        alert("Lütfen adet seçiniz!");
        return;
    }

    var sqltext = "";
    var ek = "";

    sqltext += "SELECT TOP " + adet + " Telefon ";
    sqltext += " FROM Kayitlar (nolock) WHERE 1 = 1 ";

    sqltext += " AND KaraListe <> 1 ";

    if (cinsiyet !== "")
        sqltext += " AND (Cinsiyet = '" + cinsiyet + "') ";

    if (yaskucuk !== "")
        sqltext += " AND dbo.fnYas2(DogumTarihi, Yas) >= " + yaskucuk;

    if (yasbuyuk !== "")
        sqltext += " AND dbo.fnYas2(DogumTarihi, Yas) <= " + yasbuyuk;

    sqltext += " AND (Adresil = '" + ilAdi + "') ";
    sqltext += " AND ( ";

    var ilceOptions = selectIlce.getElementsByTagName('OPTION');
    var mahOptions = selectMahHazirla.getElementsByTagName('OPTION');

    for (var i = 0; i < ilceOptions.length; i++) {
        var ilce = ilceOptions[i];

        for (var j = 0; j < mahOptions.length; j++) {
            var mahalle = mahOptions[j];

            if (ilce.value === mahalle.title) {

                if (i === ilceOptions.length - 1 && j === mahOptions.length - 1) {
                    ek = "";
                } else {
                    ek = " OR ";
                }

                sqltext +=
                    "(Adresilce = '" + ilce.text + "' and AdresMahalle = '" + mahalle.text + "') " + ek;
            }
        }
    }

    sqltext += ") ";

    sqltext += "ORDER BY RAND(CHECKSUM(NEWID()))";

    //var sayi = Math.floor(Math.random() * 100);

    var username = document.getElementById('hiKulAdi').value;

    var currentdate = new Date();

    const yil = currentdate.getFullYear().toString();
    const ay = (currentdate.getMonth() + 1).toString();
    const gun = currentdate.getDay().toString();
    //if (gun.length < 2) { gun = "0" + gun; }
    const saat = currentdate.getHours().toString();
    //if (saat.length < 2) { saat = "0" + saat; }
    const dakika = currentdate.getMinutes().toString();
    //if (dakika.length < 2) { dakika = "0" + dakika; }
    const saniye = currentdate.getSeconds().toString();
    var tarih = yil + ay + gun + saat + dakika + saniye;
    var filename = "data_" + username + "_" + tarih + ".csv";

    var dizi = sqltext.split(" ");

    //$.get(
    //    '/Portal/DownloadFile?dosyaadi="' + filename + '"&sqltxt="' + sqltext + '"',       
    //    function () {
    //    }
    //);

    //$.ajax({
    //    type: "POST",
    //    traditional: true,
    //    url: "/Portal/DownloadFile",
    //    data: { dosyaadi: filename, kuladi: username, function_param: dizi }
    //});

    $.when(
        $.ajax({
            type: "POST",
            traditional: true,
            url: "/Portal/DownloadFile",
            data: { dosyaadi: filename, kuladi: username, function_param: dizi }
        })
    ).done(
        function () {
            $("#btnIndir").show(500);
            $("#btnIndir").attr("href", "/Portal/ExportData?dosyaadi=" + filename);
        });

    /*
    $.when(
        $.ajax({
            url: "/Portal/Test",
            async: false,
            type: 'POST',
            data: dizi
            success: function (data) {
                toplam = data;
            }
        })
    ).done(
        function (data) {
        });
    */
}