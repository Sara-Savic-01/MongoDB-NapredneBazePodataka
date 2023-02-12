import React, {useState} from 'react'
import Api from '../../services/Api.js'
import Spinner from '../../components/Spinner.js';
import { useNavigate } from "react-router-dom";
import { ImagePicker,FilePicker } from 'react-file-picker'
import {useParams} from "react-router-dom";
import ReactFileReader from 'react-file-reader';
import './KreirajGosta.css'

function KreirajGosta(){
    const {id}=useParams();
    const {data:v, loading:load1, error:err1}=Api("Aranzman/VratiAranzman/"+id);
    let navigate = useNavigate();




    const [passportBase64,setPassportBase64]=useState("");
    const [fullPassportBase64,setFullPassportBase64]=useState("");
    const [jmbg,setJmbg]=useState("");
    const [email,setEmail]=useState("");
    const [ime,setIme]=useState("");
    const [prezime,setPrezime]=useState("");
    const [kucniLjubimacExists,setKucniLjubimacExists]=useState("");
    const [brojKucnihLjubimaca,setBrojKucnihLjubimaca]=useState(0);
    
    const [brojTelefona, setBrojTelefona]=useState("");
    const [showSpinner,setShowSpinner]=useState(false);

    //console.log(id);

    if(err1) throw err1;
    if(load1) return <Spinner/>;

    console.log(v);

    return(
        <div className="parent">
            <form>
                <ul className="form-style-1">
                    <li><label style={{color:"rgb(63,3,3)", fontFamily:"verdana"}}>Ime i prezime <span className="required">*</span></label><input type="text" name="field1" className="field-divided" placeholder="Ime" onChange={(event)=>setIme(event.currentTarget.value)} /> <input type="text" name="field2" class="field-divided" placeholder="Prezime"  onChange={(event)=>setPrezime(event.currentTarget.value)}/></li>
                    <li>
                        <label style={{color:"rgb(63,3,3)", fontFamily:"verdana"}}>Email <span className="required">*</span></label>
                        <input type="email" name="field3" className="field-long" onChange={(event)=>setEmail(event.currentTarget.value)} />
                    </li>
                    <li>
                        <label style={{color:"rgb(63,3,3)", fontFamily:"verdana"}}>JMBG <span className="required">*</span></label>
                        <input type="text" name="field4" className="field-long" onChange={(event)=>setJmbg(event.currentTarget.value)} />
                    </li>
                    <li>
                        <label style={{color:"rgb(63,3,3)", fontFamily:"verdana"}}>Broj Telefona <span className="required">*</span></label>
                        <input type="text" name="field7" className="field-long" onChange={(event)=>setBrojTelefona(event.currentTarget.value)} />
                    </li>
                    <li>
                        <ImagePicker
                            extensions={['jpg', 'jpeg', 'png']}
                            maxSize={5}
                            dims={{minWidth: 100, maxWidth: 2200, minHeight: 100, maxHeight: 2200}}
                            onChange={base64 => {setPassportBase64(base64.slice(23,base64.length));setFullPassportBase64(base64);}}
                            onError={errMsg => alert(errMsg)}
                        >
                            <button class="btn btn-light" type="button" style={{color:"#FFFFFF", fontFamily:"verdana"}}>
                                Pronadji sliku pasosa ili licne karte
                            </button>
                        </ImagePicker>
                        <img src={fullPassportBase64} style={{"width":"250px","height":"250px"}}></img>
                        {(passportBase64=="")&&<label style={{"color":"rgb(63,3,3)"}}>*Obavezna slika</label>}<br/>
                        <br/><br/>
                    </li>

                    <li>
                    <label style={{color:"rgb(63,3,3)", fontFamily:"verdana" }} for="field4"><span>Imate kućnog Ljubimca?</span><select name="field4" class="select-field" onChange={(event)=>setKucniLjubimacExists(event.target.value)}>
                    <option value="" >Ne</option>
                    <option value="true" >Da</option>

                    </select ></label>
                    </li>
                    {(kucniLjubimacExists !== "") && 
                    <li>

                        <label style={{color:"rgb(63,3,3)", fontFamily:"verdana" }}>Broj Kućnih Ljubimaca <span className="required">*</span></label>
                        <input type="number" name="field5" className="field-divided" onChange={(event)=>setBrojKucnihLjubimaca(event.currentTarget.value)}/>
                    </li>}
                    

                    
                    
                    
                   
                    <li>
                        <input type="button" onClick={()=>KreirajGostaIRezervaciju()} value="Rezervisi" style={{backgroundColor:"rgb(63,3,3)", color:"rgb(214,175,114)",fontFamily:"verdana" }} />
                    </li>
                   
                </ul>
            </form>
        </div>

    )

    function KreirajGostaIRezervaciju() {
        let numKucnihLjubimaca = 0
        let kucniLjubimacPostoji = false
        setShowSpinner(true);
        if (kucniLjubimacExists === "true"){
            numKucnihLjubimaca = brojKucnihLjubimaca;
            kucniLjubimacPostoji = true
        }
        fetch("https://localhost:44335/Gost/KreirajGosta",{
          method:"POST",
          headers:{"Content-Type":"application/json"},
          body: JSON.stringify({
              "jmbg":jmbg,
              "ime":ime,
              "prezime":prezime,
              "email":email,
              "broj_Telefona":brojTelefona
            })
        }).then(p=>{
            if(p.ok){
              p.json().then(data=>{
                console.log(data)
                fetch("https://localhost:44335/Rezervacija/KreirajRezervaciju",{
                  method:"POST",
                  headers:{"Content-Type":"application/json"},
                  body:JSON.stringify({
                      "brSobe":Math.round(Math.random()*v.brojPreostalihSoba),
                      "legitimacija":passportBase64,
                      
                      "status":"na cekanju",
                      "gost":data,
                      "aranzman":id,
                      "cena":v.cenaAranzmana,
                      "brojKucnihLjubimaca":numKucnihLjubimaca,
                      "postojiKucniLjubimac":kucniLjubimacPostoji
                    })
                }).then(p=>{
                    if(p.ok)
                    {
                        p.json().then(data2=>{
                            window.location.replace("/usluge/"+data2)
                            //navigate(`/usluge/${data}`)
                        });
                    }
                }).catch(exc=>{
                  console.log("EXC"+exc);
                })
              });
            }
        }).catch(exc=>{
          console.log(exc);
          setShowSpinner(false);
        })
        setShowSpinner(false);

      

    }
}

export default KreirajGosta