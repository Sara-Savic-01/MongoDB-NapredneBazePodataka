import React from 'react'
import Api from '../../services/Api.js';
import Spinner from '../../components/Spinner.js';
import '../Aranzmani/Aranzmani.css'

function HotelNaziv({id}) {

    const {data:hotel, loading:load2, error:err2}=Api("Hotel/VratiHotel/"+id);

    if(err2) throw err2;
    if(load2) return <Spinner/>

    console.log(hotel);
    return (
        
            <h3 className='slova'>
                Naziv: {hotel.naziv}
            </h3>
        
    )
    
}

export default HotelNaziv