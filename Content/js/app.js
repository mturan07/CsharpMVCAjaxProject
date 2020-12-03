document.getElementById("secilenIl").addEventListener("change", IlceleriGetir);
document.getElementById("secilenIlce").addEventListener("change", MahalleleriGetir);
document.getElementById("btnListele").addEventListener("click", getAll);

function getAll() {
    getSehir();
    getIlce();
    getMahalle();
}

function getSehir() {
    document.getElementById('divSehir').innerHTML = "<h4>İlinde bulunan kişi sayısı : 0</h4>";

    const il = document.getElementById('secilenIl').value;
    //const ilce = document.getElementById('secilenIlce').value;
    //const mahalle = document.getElementById('secilenMah').value;
    const cinsiyet = document.querySelector('input[name=radioCinsiyet]:checked').value;
    const yaskucuk = document.getElementById('yasKucuk').value;
    const yasbuyuk = document.getElementById('yasBuyuk').value;

    document.querySelector('#preloader').style.visibility = "visible";

    fetch('/Portal/SehirToplami?secilenIl=' + il +
        '&yasKucuk=' + yaskucuk + '&yasBuyuk=' + yasbuyuk + '&radioCinsiyet=' + cinsiyet)
        .then(function (res1) {
            return res1.text();
        })
        .then(function (data1) {
            console.log(data1);
            document.getElementById('divSehir').innerHTML =
                "<h4>İlinde bulunan kişi sayısı : " + numberWithCommas(data1) + "</h4>";
        })
        .catch(function (err1) {
            console.log(err1);
        });
}

function getIlce() {
    document.getElementById('divIlce').innerHTML = "<h4>İlçesinde bulunan kişi sayısı : 0</h4>";

    const il = document.getElementById('secilenIl').value;
    const ilce = document.getElementById('secilenIlce').value;
    //const mahalle = document.getElementById('secilenMah').value;
    const cinsiyet = document.querySelector('input[name=radioCinsiyet]:checked').value;
    const yaskucuk = document.getElementById('yasKucuk').value;
    const yasbuyuk = document.getElementById('yasBuyuk').value;

    fetch('/Portal/IlceToplami?secilenIl=' + il + '&secilenIlce=' + ilce +
        '&yasKucuk=' + yaskucuk + '&yasBuyuk=' + yasbuyuk + '&radioCinsiyet=' + cinsiyet)
        .then(function (res2) {
            return res2.text();
        })
        .then(function (data2) {
            console.log(data2);
            document.getElementById('divIlce').innerHTML =
                "<h4>İlçesinde bulunan kişi sayısı : " + numberWithCommas(data2) + "</h4>";
        })
        .catch(function (err2) {
            console.log(err2);
        });
}

function getMahalle() {
    document.getElementById('divMahalle').innerHTML = "<h4>Mahallesinde bulunan kişi sayısı : 0</h4>";

    const il = document.getElementById('secilenIl').value;
    const ilce = document.getElementById('secilenIlce').value;
    const mahalle = document.getElementById('secilenMah').value;
    const cinsiyet = document.querySelector('input[name=radioCinsiyet]:checked').value;
    const yaskucuk = document.getElementById('yasKucuk').value;
    const yasbuyuk = document.getElementById('yasBuyuk').value;

    fetch('/Portal/MahalleToplami?secilenIl=' + il + '&secilenIlce=' + ilce + '&secilenMah=' + mahalle +
        '&yasKucuk=' + yaskucuk + '&yasBuyuk=' + yasbuyuk + '&radioCinsiyet=' + cinsiyet
        )
        .then(function (res3)
        {
            return res3.text();
        })
        .then(function (data3)
        {
            //console.log(data3);
            document.getElementById('divMahalle').innerHTML =
                "<h4>Mahallesinde bulunan kişi sayısı : " + numberWithCommas(data3) + "</h4>";

            setTimeout(function () {
                document.querySelector('#preloader').style.visibility = "hidden";
            }, 1000);
        })
        .catch(function (err3)
        {
            console.log("Hata (MahalleToplami) : " + err3);
        });
}

function numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
}

// --------------------- //

window.onload = function (e) {

    const xhr = new XMLHttpRequest();

    xhr.open('GET', '/Portal/SehirListesi', true);

    xhr.onload = function () {
        //console.log('READYSTATE', xhr.readyState);
        if (this.status === 200) {
            const response = JSON.parse(this.responseText);

            let output = '';

            response.forEach(function (sehir) {
                output += '<option value=' + sehir.Adi + '>' + sehir.Adi + '</option>';
            });

            document.getElementById("secilenIl").innerHTML = output;
        }
    };

    xhr.send();

    e.preventDefault();
};

function IlceleriGetir(e) {

    if (document.getElementById("secilenMah") !== null)
    {
        document.getElementById("secilenMah").innerHTML = "";
    }

    const adi = document.querySelector('#secilenIl').value;

    const xhr = new XMLHttpRequest();

    xhr.open('GET', '/Portal/IlceListesi?sehirAdi=' + adi, true);

    xhr.onload = function () {
        //console.log('READYSTATE', xhr.readyState);
        if (this.status === 200) {
            const response = JSON.parse(this.responseText);

            let output = '';

            response.forEach(function (ilce) {
                output += '<option value=' + ilce.Adi + '>' + ilce.Adi + '</option>';
            });

            document.getElementById("secilenIlce").innerHTML = output;
        }
    };

    xhr.send();

    e.preventDefault();
}

function MahalleleriGetir(e) {

    const iladi = document.querySelector('#secilenIl').value;
    const ilceadi = document.querySelector('#secilenIlce').value;

    //console.log("İlçe Id : " + id);

    const xhr = new XMLHttpRequest();

    xhr.open('GET', '/Portal/MahalleListesi?sehirAdi=' + iladi + '&ilceAdi=' + ilceadi, true);

    //console.log('READYSTATE', xhr.readyState);

    //xhr.onreadystatechange = function () {
    //    if (this.status === 200 && this.readyState === 4) {
    //        console.log(this.responseText);
    //    }
    //};

    xhr.onprogress = function () {
        //console.log('READYSTATE', xhr.readyState);
    };

    xhr.onload = function () {
        //console.log('READYSTATE', xhr.readyState);
        if (this.status === 200) {
            const response = JSON.parse(this.responseText);

            let output = '';

            response.forEach(function (mahalle) {
                output += '<option value=' + mahalle.Adi + '>' + mahalle.Adi + '</option>';
            });

            if (document.getElementById("secilenMah") !== null)
                document.getElementById("secilenMah").innerHTML = output;
        }
    };

    xhr.onerror = function () {
        //console.log('Request error...');
    };

    xhr.send();

    e.preventDefault();
}