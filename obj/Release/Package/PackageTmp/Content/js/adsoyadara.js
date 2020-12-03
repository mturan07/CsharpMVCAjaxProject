document.getElementById("secilenIl").addEventListener("change", IlceleriGetir);
document.getElementById("secilenIlce").addEventListener("change", MahalleleriGetir);
document.getElementById("secilenIl").required = true;

window.onload = function (e) {

    const xhr = new XMLHttpRequest();

    xhr.open('GET', '/Portal/SehirListesi', true);

    xhr.onload = function () {
        //console.log('READYSTATE', xhr.readyState);
        if (this.status === 200) {
            const response = JSON.parse(this.responseText);

            let output = '';

            response.forEach(function (sehir) {
                output += '<option value=' + sehir.Id + '>' + sehir.Adi + '</option>';
            });

            document.getElementById("secilenIl").innerHTML = output;

            //callback(function () {

            //    setTimeout(function () {
            //        if (document.getElementById("tutIlId").value !== null
            //            || document.getElementById("tutIlId").value !== "") {
            //            var selectIl = document.getElementById('selectIl');
            //            var ilOptions = selectIl.getElementsByTagName('OPTION');
            //            var tutIlId = document.getElementById("tutIlId").value;

            //            for (var i = 0; i < ilOptions.length; i++) {
            //                var il = ilOptions[i];
            //                if (il.value === tutIlId) {
            //                    il.setAttribute('selected', 'selected');
            //                }
            //            }
            //        }
            //    }, 1000);

            //    setTimeout(function () {
            //        if (document.getElementById("tutIlceId").value !== null
            //            || document.getElementById("tutIlceId").value !== "") {
            //            var selectIlce = document.getElementById('selectIlce');
            //            var ilceOptions = selectIlce.getElementsByTagName('OPTION');
            //            var tutIlceId = document.getElementById("tutIlceId").value;

            //            for (var j = 0; j < ilceOptions.length; j++) {
            //                var ilce = ilceOptions[j];
            //                if (ilce.value === tutIlceId) {
            //                    ilce.setAttribute('selected', 'selected');
            //                }
            //            }
            //        }
            //    }, 1000);

            //});
        }
    };

    xhr.send();

    e.preventDefault();
};

function IlceleriGetir(e) {

    if (document.getElementById("secilenMah") !== null) {
        document.getElementById("secilenMah").innerHTML = "";
    }

    const id = document.querySelector('#secilenIl').value;

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

function MahalleleriGetir(e) {
    const id = document.querySelector('#secilenIlce').value;

    //console.log("İlçe Id : " + id);

    const xhr = new XMLHttpRequest();

    xhr.open('GET', '/Portal/MahalleListesi?ilceId=' + id, true);

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
                output += '<option value=' + mahalle.Id + '>' + mahalle.Adi + '</option>';
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