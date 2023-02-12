import React from 'react';
import Spinner from '../../components/Spinner.js'
import  {useState} from 'react';
import { Button } from '../../components/Button'
import  './ProveriRezervaciju.css'
import {useNavigate} from "react-router-dom";




function ProveriRezervaciju(){
    const[kodRez, setKodRez]=useState("");
    const [showSpinner,setShowSpinner]=useState(false);
    const [brSobe,setStatusSobe]=useState("");
    const [status,setStatus]=useState("");
    const [cena,setCena]=useState("");
    const navigatePage=useNavigate();
   
    const handleNavigatePage=()=>{
        navigatePage(`/ProveriDetaljnoRezervaciju/${kodRez}`)
      }
    
    return (
        <div className="rezervacijaContainer">
            <h1 className="Naslov2">Na ovoj stranici možete proveriti osnovne informacije o vašoj rezervaciji!  </h1>
            {showSpinner && <Spinner />}
            <div className="formContainer">
                <label className="labelica">Unesite kod rezervacije:</label>
                <input className="inputCode" type="text" onChange={(event)=>setKodRez(event.currentTarget.value)} />
            </div>
            <div className="buttonRez">
               <Button
                className='btns hover-zoom'
                buttonStyle='btn--primary'
                buttonSize='btn--medium'
                onClick= {()=>ProveriRezervacijuu()}
                
                >
                Proverite rezervaciju
                </Button>
                

            </div>
            <div className="elements">
                    <h3 className="Naslovic">Status rezervacije:{status} </h3>
                    <h3 className="Naslovic" >Broj sobe: {brSobe} </h3> 
                    <h3 className="Naslovic" >Cena rezervacije: {cena} </h3>
                
             
             </div>
             
             {(status==="na cekanju" || status==="aktivan") && <Button
                className='btns hover-zoom'
                buttonStyle='btn--primary'
                buttonSize='btn--medium'
                onClick= {()=>OtkaziRezervacijuu()}
                
                >
                Otkažite rezervaciju
                </Button>}

             <div className='dugmeProvera'>
               {(status==="na cekanju" || status==="aktivan") && 
               <Button
                className='btns hover-zoom'
                buttonStyle='btn--primary'
                buttonSize='btn--medium'
                onClick= {handleNavigatePage}
                
                >
                Detaljne informacije
                </Button>}

               </div>
               

            

                       
        </div>
        
         
    );
    function ProveriRezervacijuu(){
        setShowSpinner(true);
        fetch("https://localhost:44335/Rezervacija/VratiRezervaciju/"+kodRez).then(p=>{
            if(p.ok){
                p.json().then(data=>{
                    setStatus(data.status);
                    setStatusSobe(data.brSobe);
                    setCena(data.cena);
                    setShowSpinner(false);
                   
                    
                   
                })
                

            }else{
                setStatus("Nije pronadjena rezervacija sa prosledjenim kodom!");
                setStatusSobe("Nije pronadjena rezervacija sa prosledjenim kodom!");
                setCena("Nije pronadjena rezervacija sa prosledjenim kodom!");
                setShowSpinner(false);
            }
        });

    }

    function OtkaziRezervacijuu(){
        

        fetch("https://localhost:44335/Rezervacija/ObirisRezervaciju/"+kodRez,{
            method:"DELETE"
        }).then(p=>{
                if(p.ok){
                
                    
                    alert("Vaša rezervacija je uspešno uklonjena!");
                    setStatus("");
                    setStatusSobe("");
                    setCena("");
                    document.body.querySelector(".inputCode").value= " ";
                

                }else{
                    
                    alert("Vaša rezervacija nije uspešno uklonjena!");
                    setShowSpinner(false);
                }
        });

    }
    

   


}

export default ProveriRezervaciju