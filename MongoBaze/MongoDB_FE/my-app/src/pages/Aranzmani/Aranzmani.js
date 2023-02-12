import React, {useState} from 'react';
import { useNavigate } from "react-router-dom";

import Spinner from '../../components/Spinner.js'
import Api from '../../services/Api.js'
import './Aranzmani.css'

function Aranzmani(){

    const [tipAranzmana, setTipAranzmana]=useState("sve");

    let navigate = useNavigate();
    let pathUpita="Aranzman/VratiSveAranzmane";
    if(tipAranzmana==="gotove")
        pathUpita="Aranzman/VratiSveGotoveAranzmane";
    if(tipAranzmana==='trenutne')
        pathUpita="Aranzman/VratiSveAktivneAranzmane";
    const {data:aranzmani, loading, error}=Api(pathUpita);

    if(aranzmani){
        aranzmani.forEach(element => {
        console.log(typeof(element.datumAranzmana))
        let datum = new Date(element.datumAranzmana)
              .toISOString()
              .replace(/T/, " ")
              .replace(/\..+/, "");

        element.datumAranzmana = datum;
        
        
    });

    }   
    if(error) throw error;
    if(loading) return <Spinner/>

    console.log(aranzmani);


    return (
        <div className ="parent" style={{textAlign:"center"}}>
            <div className = "searchbar" style={{width:"60%"}}>
            <select style={{width:"100%", height:"30px"}} className="form-control" value={tipAranzmana} onChange={(ev)=>setTipAranzmana(ev.target.value)}>

                <option className="slova" key={"sve"} value={"sve"}>Svi aranžmani</option>
                <option className="slova" key={"gotove"} value={"gotove"}>Završeni aranžmani</option>
                <option className="slova" key={"trenutne"} value={"trenutne"}>Dostupni aranžmani</option>
            </select>
            </div>
        <div className = "tabele" style={{width:"100%"}}>
        {aranzmani.map(v=>
            {
                return(
                    <table className = "tabela" >
                        <tbody>
                            <tr>
                                <td>Tip sobe: {v.tipSobe}</td>
                            </tr>
                            <tr>
                                <td>Broj noćenja: {v.brojNocenja}</td>
                            </tr>
                            
                            <tr>
                                <td>Početak trajanja aranžmana: {v.datumAranzmana}</td>
                            </tr>
                            <tr>
                                <td>
                                    <button className="button1" onClick={() => navigate(`/aranzmani/${v.id}`)}>Saznaj više</button>
                                </td>
                            </tr>

                        </tbody>
                       

                    </table>
                    


                )
            })} 
        

      </div>
      </div>
    )
}

export default Aranzmani

