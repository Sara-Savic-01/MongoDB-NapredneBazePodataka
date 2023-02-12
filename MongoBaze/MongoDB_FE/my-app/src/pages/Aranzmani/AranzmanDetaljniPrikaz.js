import React from 'react'
import {useParams} from "react-router-dom";
import Spinner from '../../components/Spinner.js';
import Api from '../../services/Api.js';
import '../Aranzmani/Aranzmani.css'
import HotelNaziv from './HotelNaziv.js';
import { Link, NavLink } from "react-router-dom";
import moment from 'moment';

function AranzmanDetaljniPrikaz() {

    const {id}=useParams();

    const {data:a, loading:load1, error:err1}=Api("Aranzman/VratiAranzman/"+id);
    const{data:gosti, loading:load2, error:err2} = Api("Rezervacija/VratiRezervacije")

    if(err1) throw err1;
    if(load1) return <Spinner/>;

    if(err2) throw err2;
    if(load2) return <Spinner/>;

    console.log(a);
    console.log(gosti);
  
    console.log(moment().isBefore(a.datumAranzmana));
    return (
        <div className={"form"}  style={{textAlign:"left"}}>
            <div className={"naslov"}  style={{width:"100%"}}>
            <h2>Ovde možete videti detaljne informacije o aranžmanima:</h2> 
            </div>
            <div className={"slova"} style={{width:"100%"}}>
            <h3>Broj noćenja : {a.brojNocenja}</h3> 
            </div>
            
            <div className={"slova"} style={{width:"100%"}}>
            <h3>Datum: {a.datumAranzmana}</h3> 
            </div>
            <div className={"slova"} style={{width:"100%"}}>
            <h3>Tip sobe: {a.tipSobe}</h3> 
            </div>
            <div className={"slova"} style={{width:"100%"}}>
            <h3>Cena aranžmana: {a.cenaAranzmana} din.</h3> 
            </div>
            <div>

            </div>
            
            <div className='dugmici' style={{width:"100%"}}>
                {a.hotel!=="" && <HotelNaziv id={a.hotel}/>}
                
                <div className="float-child" style={{width:"250%"}}>
                {a.hotel!=="" && <Link to={`/hoteli/${a.hotel}`} className="button1">Više o hotelu</Link>} 
                </div>
                <div className="float-child" style={{width:"25%"}}>
                {moment().isBefore(a.datumAranzmana) && <Link to={`/KreirajGosta/${a.id}`} className="button1">Rezerviši</Link>} </div>    
            </div>
        </div>
    )
}

export default AranzmanDetaljniPrikaz
