import React from 'react';
import { useNavigate } from "react-router-dom";
import Spinner from '../../components/Spinner.js'
import Api from '../../services/Api.js'
import './Hoteli.css'

function Hoteli(){

    const {data:hoteli, loading, error}=Api("Hotel/VratiHotele");

    let navigate = useNavigate();

    if(error) throw error;
    if(loading) return <Spinner/>

    console.log(hoteli);

    return (
        <div className='hoteliContainer'>
             <div className='top'>Pogledajte listu svih dostupnih hotela</div>
            <div className='dugmeContainer'>
        {hoteli.map(p=>
            {
                return(
                   
                                 
                    <button className='dugme' onClick={() => navigate(`/hoteli/${p.id}`)}>{p.naziv}</button>
                   
                )
            })} 
        </div>

      </div>
    )
}

export default Hoteli