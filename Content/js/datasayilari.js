var selectIlce = document.getElementById('selectIlce');
var selectMahListe = document.getElementById('selectMahListe');
var selectMahHazirla = document.getElementById('selectMahHazirla');

document.getElementById("secilenIl").addEventListener("change", IlceleriGetir);
document.getElementById('btnIlceEkle').addEventListener("click", IlceEkle);
document.getElementById('btnIlceCikar').addEventListener("click", IlceCikar);
document.getElementById('btnMahEkle').addEventListener("click", MahalleEkle);
document.getElementById('btnMahCikar').addEventListener("click", MahalleCikar);
document.getElementById('btnListele').addEventListener("click", Listele);

//document.getElementById('selectMahListe').addEventListener("dblclick", MahalleEkle);
//document.getElementById('selectMahHazirla').addEventListener("dblclick", MahalleCikar);

var ilceId;

window.onload = function (e) {

    const xhr = new XMLHttpRequest();

    xhr.open('GET', '/Portal/SehirListesi', true);

    xhr.onload = function () {
        if (this.status === 200) {
            const response = JSON.parse(this.responseText);

            let output = '';

            response.forEach(function (sehir) {
                output += '<option value=' + sehir.Id + '>' + sehir.Adi + '</option>';
            });

            document.getElementById("secilenIl").innerHTML = output;
        }
    };

    xhr.send();

    e.preventDefault();
};

function IlceleriGetir(e) {

    const id = document.getElementById('secilenIl').value;

    const xhr = new XMLHttpRequest();

    xhr.open('GET', '/Portal/IlceListesi?sehirId=' + id, true);

    xhr.onload = function () {
        //console.log('READYSTATE', xhr.readyState);
        if (this.status === 200) {
            const response = JSON.parse(this.responseText);

            let output = '';

            response.forEach(function (ilce) {
                output += '<option value=' + ilce.Id + '>' + ilce.Adi + '</option>';
            });

            document.getElementById("secilenIlce").innerHTML = output;
        }
    };

    xhr.send();
        
    e.preventDefault();
}

function Listele(e) {
    e.preventDefault();

    if (document.getElementById('girilenAdet') !== null) {

        if (document.getElementById('girilenAdet').value === "") {
            $.confirm({
                title: 'Hata',
                content: 'Adet girilmemiş!',
                type: 'red',
                typeAnimated: true,
                buttons: {
                    close: {
                        text: 'Kapat',
                        btnClass: 'btn-red',
                        action: function () {
                            jQuery('#girilenAdet').focus();
                        }
                    }
                }
            });
            return;
        }
    }

    if (document.getElementById('secilenIl') !== null) {

        if (document.getElementById('secilenIl').value === "0") {
            $.confirm({
                title: 'Hata',
                content: 'İl seçilmemiş!',
                type: 'red',
                typeAnimated: true,
                buttons: {
                    close: {
                        text: 'Kapat',
                        btnClass: 'btn-red',
                        action: function () {
                            jQuery('#secilenIl').focus();
                        }
                    }
                }
            });
            return;
        }
    }

    const il = document.getElementById('secilenIl').value;
    const cinsiyet = document.querySelector('input[name=radioCinsiyet]:checked').value;
    const yaskucuk = document.getElementById('yasKucuk').value;
    const yasbuyuk = document.getElementById('yasBuyuk').value;

    var options = selectMahHazirla.getElementsByTagName('OPTION');

    var tbody = document.getElementById('dataTable').getElementsByTagName('tbody')[0];
    tbody.innerHTML = "";

    let output = "";
    var toplam = 0;

    document.querySelector('#preloader').style.visibility = "visible";

    for (var i = 0; i < options.length; i++) {

        const ilce = options[i].title;
        const mahalle = options[i].value;

        //$.get(
        //    '/Portal/MahalleToplami?secilenIl=' + il + '&secilenIlce=' + ilce + '&secilenMah=' + mahalle +
        //    '&yasKucuk=' + yaskucuk + '&yasBuyuk=' + yasbuyuk + '&radioCinsiyet=' + cinsiyet,
        //        function (data) {
        //            alert(data);
        //        }
        //);

        $.when(
            $.ajax({
                url: '/Portal/MahalleToplami?secilenIl=' + il + '&secilenIlce=' + ilce + '&secilenMah=' + mahalle +
                    '&yasKucuk=' + yaskucuk + '&yasBuyuk=' + yasbuyuk + '&radioCinsiyet=' + cinsiyet,
                async: false,
                type: 'post'
                //success: function (data) {
                //    toplam = data;
                //}
            })
        ).done(
            function (data) {

                output +=
                    `<tr><td>${options[i].text}</td>
                         <td class="text-right">${numberWithCommas(data)}</td></tr>`;

                toplam = toplam + parseInt(data.toString(), 10);
            });
    }

    setTimeout(function () {
        document.querySelector('#preloader').style.visibility = "hidden";

        document.getElementById('btnHazirla').disabled = false;
    }, 1000);

    tbody.innerHTML = output;

    var tfoot = document.getElementById('dataTable').getElementsByTagName('tfoot')[0];
    tfoot.innerHTML = `<tr><td><strong>Mahalle Toplamı : </strong></td>
                         <td class="text-right"><strong>${numberWithCommas(toplam)}</strong></td></tr>`;
}

function toplamSecilen(obj) {
    let say = 0;
    for (let index = 0; index < obj.options.length; index++) {
        const element = obj.options[index];
        if (element.selected) {
            say++;
            alert(element.value + " - " + element.text);
        }
    }
    return say;
}

function IlceEkle(e) {
    e.preventDefault();

    var opt = secilenIlce.options[secilenIlce.selectedIndex];

    if (opt.text !== "") {
        this.ilceId = opt.value;
        opt.title = secilenIl.options[secilenIl.selectedIndex].text;
        selectIlce.options.add(opt);

        mahalleleriEkle(this.ilceId);
    }
}

function IlceCikar(e) {
    e.preventDefault();

    var opt = selectIlce.options[selectIlce.selectedIndex];
    let title = opt.value;

    var optionsMahListe = selectMahListe.getElementsByTagName('OPTION');

    for (var i = 0; i < optionsMahListe.length; i++) {
        if (optionsMahListe[i].title === title) {
            selectMahListe.removeChild(optionsMahListe[i]);
            i--;
        }
    }

    var optionsMahHazirla = selectMahHazirla.getElementsByTagName('OPTION');

    for (var j = 0; j < optionsMahHazirla.length; j++) {
        if (optionsMahHazirla[j].title === title) {
            selectMahHazirla.removeChild(optionsMahHazirla[j]);
            j--;
        }
    }

    secilenIlce.options.add(opt);
}

function MahalleEkle(e) {
    e.preventDefault();

    //var options = selectMahListe.getElementsByTagName('OPTION');

    //for (let index = 0; index < options.length; index++) {
    //    const element = options[index];
    //    if (element.selected) {
    //        alert(element.text);
    //        selectMahHazirla.options.add(element);
    //    }
    //}

    var opt = selectMahListe.options[selectMahListe.selectedIndex];
    selectMahHazirla.options.add(opt);
}

function MahalleCikar(e) {
    e.preventDefault();

    var opt = selectMahHazirla.options[selectMahHazirla.selectedIndex];
    selectMahListe.options.add(opt);
}

function mahalleleriEkle(ilceId) {

    const xhr = new XMLHttpRequest();

    xhr.open('GET', '/Portal/MahalleListesiYeni?ilceId=' + ilceId, true);

    xhr.onload = function () {
        //console.log('READYSTATE', xhr.readyState);
        if (this.status === 200) {
            const response = JSON.parse(this.responseText);

            response.forEach(
                function (mahalle) {
                    var option = document.createElement("OPTION");

                    option.value = mahalle.Id;
                    option.text = mahalle.Adi;
                    option.title = ilceId;

                    selectMahListe.options.add(option);
                });
        }
    };

    xhr.send();
}

function getMahalle(il, ilce, mahalle, cinsiyet, yaskucuk, yasbuyuk) {

    let output;

    $.get(
        '/Portal/MahalleToplami?secilenIl=' + il + '&secilenIlce=' + ilce + '&secilenMah=' + mahalle +
        '&yasKucuk=' + yaskucuk + '&yasBuyuk=' + yasbuyuk + '&radioCinsiyet=' + cinsiyet,

        function (data, status) {
            //alert("Data: " + data + "\nStatus: " + status);
            output = data;
        }
    );

    /*
    fetch('/Portal/MahalleToplami?secilenIl=' + il + '&secilenIlce=' + ilce + '&secilenMah=' + mahalle +
        '&yasKucuk=' + yaskucuk + '&yasBuyuk=' + yasbuyuk + '&radioCinsiyet=' + cinsiyet
    )
        .then(function (res3) {
            return res3.text();
        })
        .then(function (data3) {
            //console.log(data3);
            output = numberWithCommas(data3);

            setTimeout(function () {
                document.querySelector('#preloader').style.visibility = "hidden";
            }, 1000);
        })
        .catch(function (err3) {
            console.log("Hata (MahalleToplami) : " + err3);
        });
    */
    return output;
}

function numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
}