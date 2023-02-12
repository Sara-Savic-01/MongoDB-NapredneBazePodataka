import React, {useState} from 'react'
import ReactStars from "react-rating-stars-component";
import Spinner from '../../components/Spinner.js';

function KreirajKomentar({hotel,ime, prezime, tekst, zvezdice, handleStateChange, promeniZvezdice}) {

    const [showSpinner,setShowSpinner]=useState(false);

    async function Komentarisi()
    {
       
        setShowSpinner(true);
        

        const obj={
            ime:ime,
            prezime:prezime,
            tekst:tekst,
            zvezdice:zvezdice
        };

        await fetch("https://localhost:44335/Komentar/KreirajKomentar/"+hotel,{
        method:"POST",
        headers:{"Content-Type":"application/json"},
        body: JSON.stringify(obj)    
    }).then(p=>{
        if(p.ok){
            console.log("Uspesno dodato!");
        }
    }).catch(exc=>{
        console.log(exc);
        setShowSpinner(false);
    });
    window.location.reload();
    setShowSpinner(false);
    }
    
    
 

    return (
        <div>
             {showSpinner && <Spinner />}
        <form onSubmit={()=>Komentarisi()}>
            <div>
                <input placeholder="Ime" name="ime" value={ime} onChange={(ev)=>handleStateChange(ev)}/>
                <input placeholder="Prezime" name="prezime" value={prezime}  onChange={(ev)=>handleStateChange(ev)}/>
            </div>
            <textarea placeholder="Unesite Vaš komentar" name="tekst" cols="40" rows="5" value={tekst}  onChange={(ev)=>handleStateChange(ev)}></textarea>
            <ReactStars
            count={5}
            onChange={(broj)=>promeniZvezdice(broj)}
            size={24}
            activeColor="#ffd700"
            />

            <button className='dugmence'>Dodaj komentar</button>
        </form>
        </div>
    )
}

export default KreirajKomentar
   