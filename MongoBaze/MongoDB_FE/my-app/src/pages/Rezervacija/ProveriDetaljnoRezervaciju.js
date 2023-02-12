import React from 'react';
import  '../Rezervacija/ProveriRezervaciju.css'
import {useParams} from "react-router-dom";
import Api from '../../services/Api.js';
import  '../Rezervacija/ProveriRezervaciju.js';
import Gost from '../Rezervacija/Gost.js';
import Spinner from '../../components/Spinner.js';
import  {useState} from 'react';
import { Button } from '../../components/Button';
import {useNavigate} from "react-router-dom";


function ProveriDetaljnoRezervaciju(){
    const {kodRez}=useParams();
    //const[kodRez, setKodRez]=useState("");
    
    const navigatePageGost=useNavigate();

   

    const {data:rez, loading:load3, error:err3}=Api("Rezervacija/VratiRezervaciju/"+kodRez);
    

    if(err3) throw err3;
    if(load3) return <Spinner/>;
    
    const handleNavigatePageGost=()=>{
        navigatePageGost("/Gost");
      }

    return (
        <div className='rezervacijaContainer'>
            <h1 className='Naslov2'> Ovde možete videti sve informacije o korisniku rezervacije </h1>
            <div>
            <Button
                className='btns hover-zoom'
                buttonStyle='btn--primary'
                buttonSize='btn--medium'
                onClick={handleNavigatePageGost}
                
                >
                Proverite 
                </Button>
            
          
            
            </div>
        </div>
    )

    
    
   
    
   
}

export default  ProveriDetaljnoRezervaciju