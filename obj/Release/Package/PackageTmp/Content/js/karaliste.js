document.getElementById('btnKaraListeEkle').addEventListener('click', btnKaraListeEkle);

function btnKaraListeEkle(id, telefon) {

    alert(id + " " + telefon);

    $.when(
        $.post('/Portal/KaraListeEkle', { kayitId: id },
            function () {
            })
    ).done(
        location.replace('/Portal/KaraListe?telefonno=' + telefon)
    );
}

document.getElementById('btnKaraListeCikar').addEventListener('click', btnKaraListeCikar);

function btnKaraListeCikar(id, telefon) {
    alert(id + " " + telefon);

    $.when(
        $.get('/Portal/KaraListeCikar', { kayitId: id },
            function () {
            })
    ).done(
        location.replace('/Portal/KaraListe?telefonno=' + telefon)
    );
}
