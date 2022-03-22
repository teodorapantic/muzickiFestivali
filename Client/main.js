import { DanFest } from "./DanFest.js";
import { Festival } from "./Festival.js";
import { Rezervacija} from "./Rezervacija.js";

var emailFormat = "^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$";
var listaFestivala=[];
var listaDanaFest = [];
var listaRezZaFes = [];

function uctiajRezZaFes() {
    let festivalNaziv=document.querySelector(".lblNaslov").innerHTML;
    let festivalid=listaFestivala.find(x=>x.naziv==festivalNaziv).id;
    fetch("https://localhost:5001/Rezervacija/VratiSveMail/" + festivalid)
    .then(p=>{
            p.json().then(mailovi=>{

                mailovi.forEach(p=>
                    {   
                        listaRezZaFes.push(p.email);
                    });
                })
        })
}

function ucitajFestivale()
{
    fetch("https://localhost:5001/Festival/Festivali")
    .then(p=>{
            p.json().then(festivali=>{

                  festivali.forEach(p=>
                    {
                        var f=new Festival(p.id,p.naziv, p.adresa,p.grad,p.opisFestivala,p.datumPocetka,p.datumDatumKraja);
                        
                        listaFestivala.push(f);
                    });
                    let glavnidiv=document.createElement("div");
                    glavnidiv.className="glavniDiv";
                    let naslovdiv =  document.createElement("div");
                    naslovdiv.className = "naslovDiv";
                    document.body.appendChild(naslovdiv);
                    document.body.appendChild(glavnidiv);
                    let lblNaslov=document.createElement("label");
                    lblNaslov.innerHTML="MUZICKI FESTIVALI";
                    lblNaslov.className="lblNaslov";
                    naslovdiv.appendChild(lblNaslov);
                    pocetna(glavnidiv);
                })
        })
    
}
ucitajFestivale();
//// 
function ucitajDaneFest(naziv) {
    fetch("https://localhost:5001/DanFest/PreuzmiLineUp/" + naziv)
    .then(p=>{
            p.json().then(dani=>{
                dani.forEach(x => {
                    let danf = new DanFest(x.id, x.datum, x.dan, x.cena, x.nastupi);
                    listaDanaFest.push(danf);
                })
                prikaziFestival(naziv);
                })
        })
}
//ucitajFestivale();
///PRVA STRANAAA
function pocetna(host)
{
    let info;
    let slika;
    let opis;
    let div1;
    let div2;
    let div;
    let grad;
    let adres;
    let datum;
    let festivali;
    listaFestivala.forEach(p =>
        { 

            festivali=document.createElement("div");
            festivali.className="divFestivali";
            host.appendChild(festivali);

            div=document.createElement("div");
            div.className="divZaFest";
            festivali.appendChild(div);
 
            div2=document.createElement("div");
            div2.className="divZaOpis"
            opis=document.createElement("label");
            opis.innerHTML=p.opisFestivala;
            div.appendChild(div2);
            div2.appendChild(opis);
            
            info=document.createElement("div");
            info.className="divGrad";
            opis.appendChild(info);

            grad=document.createElement("label");
            grad.innerHTML = "Mesto odrzavanja: " + p.grad;
            grad.value = p.grad;
            grad.className="lblgrad";
            info.appendChild(grad);

            adres=document.createElement("label");
            adres.innerHTML = "  , "+ p.adresa;
            info.appendChild(adres);
            
            div1=document.createElement("div");
            div1.className="divSlike";
            slika = document.createElement("img");
            slika.className = "slike"
            slika.src = "../Slike/" + p.naziv + ".png";
            slika.alt = p.naziv + ".png";
            div.appendChild(div1);
            div1.appendChild(slika);

            datum=document.createElement("label");
            
            datum.innerHTML = "<br>" +"Odrzava se: " + p.datumPocetka + " -  " + p.datumKraja;
            info.appendChild(datum);
            var dugme=document.createElement("button");
            dugme.className="dugmeIzaberi";
            dugme.innerHTML="IZABERI";
            festivali.appendChild(dugme);
            dugme.onclick = (ev) => ucitajDaneFest(p.naziv);
        })
}

function  prikaziFestival(naziv) {
    preuzmikarte(naziv);

    let glavniDiv = document.querySelector(".glavniDiv");
    let divZaPrikazFestivala = document.createElement("div");
    divZaPrikazFestivala.className = "divZaPrikazFestivala";
    obrisiFormu(glavniDiv);
    let naslov = document.querySelector(".lblNaslov");
    naslov.innerHTML = naziv;
    naslov.value=naziv;

    let divZaSveDane = document.createElement("div");
    divZaSveDane.className = "divZaSveDane";
    glavniDiv.appendChild(divZaPrikazFestivala);
    divZaPrikazFestivala.appendChild(divZaSveDane);
    let i = 0;
    listaDanaFest.forEach(x => {

        let divZaDan = document.createElement("div");
        divZaDan.className = "divZaDan";
        divZaSveDane.appendChild(divZaDan);
        let tabela = document.createElement("table");
        tabela.className = "tabela" + i;
        
        let lblDan = document.createElement("label");
        lblDan.innerHTML = x.dan;
        divZaDan.appendChild(lblDan);
        divZaDan.appendChild(tabela);
    
        let thead = document.createElement("thead");
        tabela.appendChild(thead);
    
        let tr = document.createElement("tr");
        tr.className = "row";
        thead.appendChild(tr);
    
        let tbody = document.createElement("tbody");
        tbody.className = "tbody" + i;
        tabela.appendChild(tbody);
    
        let th;
        let nizZaglavlja = ["Ime izvodjaca", "Datum i vreme"];
        nizZaglavlja.forEach(el => {
            th = document.createElement("th");
            th.innerHTML = el;
            tr.appendChild(th);
        })
        x.nastupi.forEach(y => {
        let teloTabele = document.querySelector(".tbody" + i);

        let tr = document.createElement("tr");
        tr.className = "red";
        teloTabele.appendChild(tr);

        tr.id = y.id;

        let imeIzvodjaca = document.createElement("td");
        imeIzvodjaca.innerHTML = y.imeIzvodjaca;
        tr.appendChild(imeIzvodjaca);

        let vreme = document.createElement("td");
        vreme.innerHTML = y.vreme.split("T");
        tr.appendChild(vreme);
    })
    let divh=document.createElement("div");
    divh.className="divZaCheckCena";
    divZaDan.appendChild(divh);
    
    let c=document.createElement("div");
    c.className="divZaCenu";
    divh.appendChild(c);


  

    let d=document.createElement("div");
    d.className="divZaCBox";
    divh.appendChild(d); 

    let lblCena = document.createElement("label");
    lblCena.innerHTML = "Cena:  " + x.cena;
    lblCena.className="CenaZaDan" + x.dan;
    c.appendChild(lblCena);

    let dd=document.createElement("div");
    dd.className="divZaKolicinu";
    d.appendChild(dd); 

    let str=(x.dan).replace(/\s/g,'');
    let kolicina=document.createElement("input");
    kolicina.className="kolicina"+ str;
    kolicina.type="number";
    dd.appendChild(kolicina);
    kolicina.min=1;
    kolicina.max=5;
    kolicina.addEventListener("change", ()=>
    {
    lblCena.innerHTML = "Cena: " +x.cena*kolicina.value;
    })
    

    let ds=document.createElement("div");
    ds.className="divZaCekiranje";
    d.appendChild(ds); 
    let boxZaRez = document.createElement("input");
    boxZaRez.type = "checkbox";
    
    boxZaRez.className=str;
    boxZaRez.value = x.cena;
    ds.appendChild(boxZaRez);

    i++;
   

})
    let btnIzvrsiRez = document.createElement("button");
    btnIzvrsiRez.className=("btnIzvrsiRez");
    btnIzvrsiRez.innerHTML = "NAPRAVI REZERVACIJU";
    divZaPrikazFestivala.appendChild(btnIzvrsiRez);
    btnIzvrsiRez.onclick = (ev) => prikaziFormular();
    
    
    let pregled = document.createElement("button");
    pregled.onclick = (ev) => prikaziRezervaciju();
    pregled.className=("btnPregled");
    pregled.innerHTML = "POGLEDAJ REZERVACIJU";
    divZaPrikazFestivala.appendChild(pregled);


    let formular = document.createElement("div");
    formular.className = "formular";
    
    divZaPrikazFestivala.appendChild(formular);
    let doleForma = document.createElement("div");
                    doleForma.className = "doleForma";
                    glavniDiv.appendChild(doleForma);
    
     uctiajRezZaFes();
}
/// FORMULAR ZA REGISTRACIJU
function prikaziFormular() {

    let brojKarataDani = document.querySelectorAll("input[type='number']");
    console.log(brojKarataDani);
    let daniKojeRacuna = document.querySelectorAll("input[type='checkbox']:checked");
    console.log(daniKojeRacuna);
    if(daniKojeRacuna.length === 0){
        console.log(daniKojeRacuna);
        alert("Mora biti cekiran barem jedan dan");
    }
    else{
        odrediCenu();
        daniKojeRacuna = document.querySelectorAll("input[type='checkbox']");
    brojKarataDani.forEach(x => {
        console.log("Onemoguci");
        x.disabled = true;
    })
    daniKojeRacuna.forEach(x => {
        x.disabled = true;
    })
    
    let divZaPrikazFestivala = document.querySelector(".divZaPrikazFestivala");
    let formular = document.querySelector(".formular");
    obrisiFormu(formular);
    divZaPrikazFestivala.appendChild(formular);

    let labelica1=document.createElement("label");
    labelica1.innerHTML="Ime:";
    formular.appendChild(labelica1);

    let txbIme=document.createElement("input");
    txbIme.type="text";
    txbIme.className="txbIme";

    formular.appendChild(txbIme);

    let labelica2=document.createElement("label");
    labelica2.innerHTML="Prezime:";
    formular.appendChild(labelica2);

    let txbPrezime=document.createElement("input");
    txbPrezime.type="text";
    txbPrezime.className="txbPrezime";
    formular.appendChild(txbPrezime);

    let labelica3=document.createElement("label");
    labelica3.innerHTML="Email:";
    formular.appendChild(labelica3);

    
    let txbEmail=document.createElement("input");
    txbEmail.type=emailFormat;
    txbEmail.className="txbEmail";
    formular.appendChild(txbEmail);

    let labelica4=document.createElement("label");
    labelica4.innerHTML="Cena:";
    formular.appendChild(labelica4);

    let txbCena=document.createElement("input");
    txbCena.type=emailFormat;
    txbCena.className="txbCena";
    txbCena.disabled = true;
    txbCena.value = cena;
    formular.appendChild(txbCena);


  
let divZaDugmice=document.createElement("div");
formular.appendChild(divZaDugmice);
    
    let rezervisi = document.createElement("button");
     rezervisi.onclick = (ev) => dodajRezervaciju(txbIme.value,txbPrezime.value,txbEmail.value);
     rezervisi.className=("btnRez");
     rezervisi.innerHTML = "dodaj REZERVACIJU";
     divZaDugmice.appendChild(rezervisi);
    
    
    }

}
function izmeniRezervaciju(){
    var ime=document.querySelector(".txbIme").value;
    var prezime=document.querySelector(".txbPrezime").value;
    var email=document.querySelector(".txbEmail").value;
    if(ime===undefined || ime==="" )
    {
        alert("Niste uneli ime");
        return;
    }

if(prezime===undefined || prezime==="" )
{
    alert("Niste uneli prezime");
    return;
}
if(email==="" ||!email.match(emailFormat))
{
    alert("Niste uneli email u email-formatu");
    return;
}
  
    //let optionEl=document.querySelector(".formular");
    var cena=document.querySelector(".txbCena").value;
    fetch("https://localhost:5001/Rezervacija/menjajRezervaciju/" + ime + "/" + prezime + "/" + email,{

            method:"PUT"
    
        })
    
        .then(p=>{
    
            if(p.ok)
            {
                //prikaziRezervaciju();
                console.log("Rezervacija je prihvacena");
            }
            else{
                alert("Problem pri kreiranju rezervacije");
            }
        })
}

function obrisiFormu(host) {

    while(host.firstChild) {
        
        host.removeChild(host.firstChild);
    }
}
function crtajGrafik() {
        let doleForma = document.querySelector(".doleForma");
        listaDanaFest.forEach(i => {
            let danDiv = document.createElement("div");
            danDiv.className = "danDiv";
            doleForma.appendChild(danDiv);
            let grafikaDiv = document.createElement("div");
            grafikaDiv.className = "grafikaDiv";
            danDiv.appendChild(grafikaDiv);
            let brojac = 0;
            let ljubicastiDiv;
            listaUlaznica.forEach(j => {
                ljubicastiDiv = document.createElement("div");
                ljubicastiDiv.className = "ljubicastiDiv";
                if (j.dan.dan == i.dan) {
                    grafikaDiv.appendChild(ljubicastiDiv);
                    brojac+=j.ulaznica;
        console.log(brojac);
                }
                console.log(brojac);
            })
            let lbl = document.createElement("label");
            lbl.innerHTML = i.dan;
            danDiv.appendChild(lbl);
            let lbl1 = document.createElement("label");
            lbl1.innerHTML = "Broj rezervisanih karata:" + brojac;
            danDiv.appendChild(lbl1);




        })
    

}


let listaUlaznica=[];
function preuzmikarte(naziv)
{
   console.log(document.querySelector(".lblNaslov").innerHTML);
   var festivalid;
   festivalid=listaFestivala.find(x=> x.naziv==naziv).id;
   
    fetch("https://localhost:5001/Karta/PreuzmiKartu/" +festivalid,
    ) .then(p=>{
       p.json().then(karte=>{
           karte.forEach(p=>
               {
                   listaUlaznica.push(p);

               })
               crtajGrafik();

       })
   
    })
}
function dodajRezervaciju(Ime,Prezime,email)

{
    if(Ime===undefined || Ime==="" )
        {
            alert("Niste uneli ime");
            return;
        }
      
    if(Prezime===undefined || Prezime==="" )
    {
        alert("Niste uneli prezime");
        return;
    }
    if(email==="" ||!email.match(emailFormat))
    {
        alert("Niste uneli email u email-formatu");
        return;
    }
      
        var ime=document.querySelector(".txbIme").value;
        var prezime=document.querySelector(".txbPrezime").value;
        var email=document.querySelector(".txbEmail").value;
        var cena=document.querySelector(".txbCena").value;
        let festivalNaziv=document.querySelector(".lblNaslov").innerHTML;
        let festivalid=listaFestivala.find(x=>x.naziv==festivalNaziv).id;
        console.log(email);
        console.log(ime);
        console.log(prezime);
        console.log(cena);
        fetch("https://localhost:5001/Rezervacija/NapraviRez/" +Ime+"/"+Prezime+"/"+email+"/"+cena+"/"+festivalid,{ method:"POST"})
    
        .then(p=>{
    
            if(p.ok)
            {
               alert("Rezervacija je prihvacena");
               PotvrdiRez();
               let glavniDiv = document.querySelector(".glavniDiv");
               obrisiFormu(glavniDiv);
            }
            else{
                alert("Problem pri kreiranju rezervacije");
                let glavniDiv = document.querySelector(".glavniDiv");
                obrisiFormu(glavniDiv);
            }
            prikaziFestival(document.querySelector(".lblNaslov").innerHTML);
        })
    }
    function prikaziRezervaciju()
    {
      
        let formular = document.querySelector(".formular");
        obrisiFormu(formular);
        let txtBox;
        let lbl;
         
            lbl = document.createElement("label");
            lbl.innerHTML = "Email";
            
            txtBox = document.createElement("input");
            txtBox.type = "text";
            txtBox.className="pregledEmail";
            formular.appendChild(lbl);
            formular.appendChild(txtBox);

            let pretrazi=document.createElement("button");
            pretrazi.className="btnpretrazi";
        
            pretrazi.innerHTML="PRETRAZI";
            formular.appendChild(pretrazi);
            pretrazi.onclick=(ev)=>pretraziPoEmail(txtBox.value);
    }
    let cena;
    function odrediCenu()
        {
            cena = 0
            let odabraniDani=document.querySelectorAll("input[type='checkbox']:checked");
            if (odabraniDani===null)
            {
                alert("Izaberite dan festivala");
                return;    
            }
            console.log(odabraniDani);
            let  divC=document.querySelector(".divC");
            let select = ".kolicina";
            let s;
            for (let i=0; i<odabraniDani.length;i++)
             {
                 s=document.querySelector(select+odabraniDani[i].className);
                 console.log(odabraniDani[i].value);
                 console.log(s.value);
                 cena=cena+(parseInt(odabraniDani[i].value)*parseInt(s.value));
                 console.log(cena);
             }
             if(odabraniDani.length === listaDanaFest.length){
                 cena = cena * 0.8;
                 alert("Ostvaren popust od 20%");
             }
                console.log(cena);
                let lCena=document.createElement("label");
                lCena.className="lCena";
                lCena.innerHTML=cena;
        }
       
       
    


        function PotvrdiRez()
        {
            let chk=document.querySelectorAll("input[type='checkbox']:checked");
            console.log(chk);
            let festivalNaziv=document.querySelector(".lblNaslov").innerHTML;
            let festivalid=listaFestivala.find(x=>x.naziv==festivalNaziv).id;
            let select = ".kolicina";
            let brUlaznica;
            let danNaziv;
            let email=document.querySelector(".txbEmail");
            chk.forEach(p=>{
                danNaziv=p.className;
                console.log(p.className);
                brUlaznica=document.querySelector(select+p.className).value;
                napraviKartu(brUlaznica,danNaziv,email.value, festivalid);
                console.log(email.value);
                 });
                 }




 function napraviKartu(brojUlaznica, danNaziv, email, festivalid) {
    console.log(brojUlaznica + " " + danNaziv + " " + email + " " + festivalid);
    fetch("https://localhost:5001/Karta/DodajKartu/"+brojUlaznica+"/"+danNaziv+"/"+email +"/"+festivalid,{method:"POST"})
    .then(p=>{
        if(p.ok)
        {
            console.log("Uspesno kreirana karta");
        }
        else
        {
            alert("Nemoguce je kreirati kartu");
        }
    })

}
/*function vratiNazad()
{
    let glavnaForma = document.querySelector(".glavniDiv");
    let divZaNazad= document.createElement("div");
    divZaNazad.className = "divZaNazad";

    obrisiFormu(glavnaForma);

   
    glavnaForma.appendChild(divZaNazad);
    prikaziFormular();
}*/
function pretraziPoEmail(Email)
{ 
    let festivalNaziv=document.querySelector(".lblNaslov").innerHTML;
    let festivalid=listaFestivala.find(x=>x.naziv==festivalNaziv).id;
    console.log(festivalNaziv);
    console.log(festivalid);
    let rez=null;
    let proveriEmail = listaRezZaFes.find(x => x == Email);
    console.log(listaRezZaFes);
    if(proveriEmail != null){
    fetch("https://localhost:5001/Rezervacija/Pretraga/"+festivalid +"/"+ Email)
    .then(p=>{
          p.json()
          .then(rezervacija=>
            {
            console.log(rezervacija);
             rez=new Rezervacija(rezervacija.id,rezervacija.ukupnaCena,rezervacija.ime,rezervacija.prezime,rezervacija.email, rezervacija.karte);
             console.log(rez) ;
        if(rez==null)
            {
               alert("Proverite email");
            }
        else{
            let formular=document.querySelector(".formular");
            console.log(rez.karte);
            (rez.karte).forEach(x => {
                if(x.dan.festival.id === festivalid){
                    let brKarata = document.querySelector(".kolicina" + x.dan.dan);
                    brKarata.value = x.ulaznica;
                    let cek = document.querySelector("." + x.dan.dan);
                    cek.checked = true;
                    let lbl = document.querySelector(".CenaZaDan"+x.dan.dan);
                    lbl.innerHTML = "Cena:" + x.ulaznica*x.dan.cenaZaDan;
                }
                //odrediCenu();
            })
            obrisiFormu(formular);
            let labelica1=document.createElement("label");
            labelica1.innerHTML="Ime:";
            formular.appendChild(labelica1);
        
            let txbIme=document.createElement("input");
            txbIme.type="txtBox";
            txbIme.className="txbIme";
            txbIme.value=rez.ime;
        
            formular.appendChild(txbIme);
        
            let labelica2=document.createElement("label");
            labelica2.innerHTML="Prezime:";
            formular.appendChild(labelica2);
        
            let txbPrezime=document.createElement("input");
            txbPrezime.type="txtBox";
            txbPrezime.className="txbPrezime";
            txbPrezime.value=rez.prezime;
            formular.appendChild(txbPrezime);

            
            
            let div=document.createElement("div");
            formular.appendChild(div);
            let izmeni=document.createElement("button");
            izmeni.onclick = (ev) =>Izmeni(txbIme.value,txbPrezime.value,Email);
            izmeni.className="dugmeIzmeni";
            izmeni.innerHTML="IZMENI REZERVACIJU";
            div.appendChild(izmeni);

            let obrisi=document.createElement("button");
            obrisi.onclick = (ev) =>Obrisi(Email);
            obrisi.className="dugmeObrisi";
            obrisi.innerHTML="OBRISI REZERVACIJU";
            div.appendChild(obrisi);

        

        }
    })
})}
else{
    alert("Proverite mail")
}
}
function PotvrdiRezervaziju(email)
        {
            let chk=document.querySelectorAll("input[type='checkbox']:checked");
            console.log(chk);
            let festivalNaziv=document.querySelector(".lblNaslov").innerHTML;
            let festivalid=listaFestivala.find(x=>x.naziv==festivalNaziv).id;
            let select = ".kolicina";
            let brUlaznica;
            let danNaziv;
            chk.forEach(p=>{
                danNaziv=p.className;
                console.log(p.className);
                brUlaznica=document.querySelector(select+p.className).value;
                napraviKartu(brUlaznica,danNaziv,email, festivalid);
                console.log(email.value);
                 });
}
function Izmeni(ime,prezime,email)
{
    fetch("https://localhost:5001/Karta/BrisiKartu/" + email ,{method:"DELETE"})
    .then(x => {
        if (x.ok) {
            odrediCenu();

            fetch("https://localhost:5001/Rezervacija/menjajRezervaciju/" + cena + "/" + ime + "/" + prezime + "/" + email,{method:"PUT"})
            .then(x => {
                if (x.ok) {
                             alert("Uspesno izmenjen");
                             PotvrdiRezervaziju(email);
                             let glavniDiv = document.querySelector(".glavniDiv");
                             obrisiFormu(glavniDiv);
                }
                prikaziFestival(document.querySelector(".lblNaslov").innerHTML);
            })
        }
    })
}
function Obrisi(Email)
{
    fetch("https://localhost:5001/Karta/BrisiKartu/" + Email ,{method:"DELETE"})
    .then(x => {
        if (x.ok) {
            fetch("https://localhost:5001/Rezervacija/ukloniRezervaciju/" +Email,{method:"DELETE"})
            .then(x => {
            if (x.ok) {
                alert("Uspesno izbrisana rezervacija");   
                let glavniDiv = document.querySelector(".glavniDiv");
                             obrisiFormu(glavniDiv)    
            }
            prikaziFestival(document.querySelector(".lblNaslov").innerHTML);
        })
        }
    })
}