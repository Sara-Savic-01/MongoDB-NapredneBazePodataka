import React from 'react'
import '../Rezervacija/ProveriRezervaciju.css';
import { Button } from '../../components/Button'
import  {useState} from 'react';

function Gost() {
    const[gostJmbg, setGostJmbg]=useState("");
    const [ime,setGostIme]=useState("");
    const [prezime,setGostPrezime]=useState("");
    const [email,setGostEmail]=useState("");
    return (
        
        <div className="rezervacijaContainer">
            <h1 className="Naslov"> Detaljne informacije o korisniku rezervacije</h1>
            <div className="formContainer">
                <label className="labelica">Radi dodatne autentifikacije, unesite vaš Jmbg:</label>
                <input className="inputCode" type="text" onChange={(event)=>setGostJmbg(event.currentTarget.value)} />
            </div>
            <div className="buttonRez">
               <Button
                className='btns hover-zoom'
                buttonStyle='btn--primary'
                buttonSize='btn--medium'
                onClick= {()=>Proveri()}
                
                >
                Prikaži
                </Button>
                

            </div>
             <div className="elements">
                <h3 className="Naslovic">
                    Ime: {ime}
                </h3>
                <h3 className="Naslovic">
                    Prezime: {prezime}
                </h3>
                <h3 className="Naslovic">
                    Email: {email}
                </h3>
             </div>
            

        </div>
           
            
        
    );
    function Proveri(){
        fetch("https://localhost:44335/Gost/VratiGostaJmbg/"+gostJmbg).then(put=>{
            if(put.ok){
                put.json().then(dataa=>{
                   setGostIme(dataa.ime);
                   setGostPrezime(dataa.prezime);
                   setGostEmail(dataa.email);
                   
                    
                   
                })
                

            }else{
                
                alert("Nisu pronadjeni podaci!");
               
            }
        });
    }    
}

export default Gost