import React, {useState} from 'react'
import {useParams} from "react-router-dom";
import Spinner from '../../components/Spinner.js'
import Api from '../../services/Api.js'
import './Usluge.css'


function Usluge(){
    const {idRez}=useParams();

    const {data:usluge, loading, error}=Api("Usluga/VratiUsluge");
    const {data:rezervacija, loading2, error2}=Api("Rezervacija/VratiRezervacijuId/"+idRez);
    const [showSpinner,setShowSpinner]=useState(false);
    const [dodateUsluge,setDodateUsluge]=useState([]);

    if(error) throw error;
    if(loading) return <Spinner/>

    if(error2) throw error2;
    if(loading2) return <Spinner/>

    console.log(usluge)
    console.log(dodateUsluge)
    console.log(idRez)
    console.log(rezervacija)

    return(
        <div className="float-containersila" style={{textAlign:"center"}}>
            {usluge.map(el=>{
                return (
                <div  className="float-containerforma" style={{textAlign:"center"}}>
                    <div className="float-child" style={{width:"30%"}}>
                        <p style={{color:"rgb(63,3,3)"}}> {el.naziv}</p><br/>
                    </div>

                    <div className="float-child" style={{width:"10%"}}>
                        <input type="checkbox" onChange={(event)=>{
                            if(event.currentTarget.checked){
                                dodateUsluge.push(el.id);
                            }
                            else{
                                let index=dodateUsluge.indexOf(el.id);
                                if(index!==-1){
                                    dodateUsluge.splice(index,1);
                                }
                            }
                            console.log(dodateUsluge);
                        }}/>
                    </div>
                    

                    <div className="float-child" style={{width:"30%"}}>
                        <img src={"data:image/jpg;base64,"+el.slikaBytesBase} alt="" style={{"width":"100px","height":"100px"}}/>
                    </div>

                    <div className="float-child" style={{width:"30%"}}>
                        <p style={{color:"rgb(63,3,3)"}}>Cena: {el.cena} din.</p><br/>
                    </div>
                </div>);
            })}
            <div className="float-child" style={{width:"100%"}}>
            <button style={{backgroundColor:"rgb(63,3,3)", color:"rgb(214,175,114)", fontSize: 20, fontfamily:'Times New Roman', cursor:"pointer"}} type="button" onClick={()=>DodajUslugeRezervaciji()}>Potvrdi</button> </div>
        </div>

    );

    function DodajUslugeRezervaciji(){
        setShowSpinner(true);
        fetch("https://localhost:44335/Rezervacija/DodajUsluge/"+idRez,{
            method:"PUT",
            headers:{"Content-Type":"application/json"},
            body: JSON.stringify(dodateUsluge)
        }).then(p=>{
            if(p.ok){
                //window.location.replace("../rezervacija/"+idRez);
                alert("Uspesno izvrsena rezervacija, vas kod za proveru je " + rezervacija.sifra_Rezervacije)
                window.location.replace("/proveriRezervaciju");

            }
        }).catch(exc=>{
            console.log(exc);
        })
        setShowSpinner(false);

    }

}

export default Usluge