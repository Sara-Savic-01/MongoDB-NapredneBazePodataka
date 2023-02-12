import React from 'react';
import {useNavigate} from "react-router-dom";
import {useState} from 'react';
import { Button } from '../../components/Button'
import './Home.css'


function Home(){
    const navigate=useNavigate();
   
    const [click,setClick] =useState(false)
    const [button,setButton]=useState(true)
    
    const handleClick=()=>setClick(!click)
    const closeMobileMenu=()=>setClick(false)

   // const handleHistory=()=>{
       // history.push("/")
   // }

   const handleNavigate1=()=>{
     navigate("/hoteli")
   }

   const handleNavigate2=()=>{
       navigate('/aranzmani')
   }

    return (
     
        <div className='homePageContainer'>
         <h1 className="Naslov"> Dobrodošli na naš sajt!  </h1> 
         <h3 className="Naslov1">Odaberite idealno mesto za odmor i opuštanje!</h3>
         <div className='buttonContainer'>
               <Button
                className='btns hover-zoom'
                buttonStyle='btn--primary'
                buttonSize='btn--large'
                onClick= {handleNavigate1}
                >
                 Pogledajte listu dostupnih hotela
                </Button>
                <Button
                className='btns'
                buttonStyle='btn--primary'
                buttonSize='btn--large'
                onClick={handleNavigate2}
                >
                 Pogledajte listu dostupnih aranžmana
                </Button>
            </div>

        </div>
    )
}

export default Home