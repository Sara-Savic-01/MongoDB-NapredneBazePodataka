import React from 'react';
import {useParams} from "react-router-dom";
import Spinner from '../../components/Spinner.js';
import Api from '../../services/Api.js';
import Komentari from '../../pages/Komentari/Komentari.js';


function Hotel() {

    const {id}=useParams();

    const {data:hotel, loading:loading1, error:error1}=Api("Hotel/VratiHotel/"+id);


    if(error1) throw error1;
    if(loading1) return <Spinner/>;

    console.log(hotel);


    return (
        <div className='detaljnoContainer'>
            <div className='infoContainer'>

                <h1 className='naziv'>{hotel.naziv}</h1>

                <div className='sveInfo'>
                <div className='info'>
                    <div className='labela'>Godina osnivanja: </div>
                    <div className='data'>{hotel.godinaOsnivanja}</div>                    
                </div>

                <div className='info'>
                    <div className='labela'>Grad: </div>
                    <div className='data'>{hotel.grad}</div>
                    
                </div>

                <div className='info'>
                    <div className='labela'>Opis: </div>
                    <div className='data'>{hotel.opis}</div>
                    
                </div>
                </div>
                

            </div>
        

            <div className='komentarContainer'>
                <Komentari hotel={hotel.id}/>
            </div>
        
        </div>  
        
    )
}

export default Hotel
