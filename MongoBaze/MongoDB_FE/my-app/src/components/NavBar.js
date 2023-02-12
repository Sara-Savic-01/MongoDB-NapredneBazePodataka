import React from 'react';
import {Link,useNavigate} from "react-router-dom";
import {useState} from 'react';
import logo from '../components/logoSlika.png';
import './NavBar.css'

function NavBar(){
    //const history=useHistory();
    const navigate=useNavigate();
    const [click,setClick] =useState(false)
    const [button,setButton]=useState(true)
    
    const handleClick=()=>setClick(!click)
    const closeMobileMenu=()=>setClick(false)

   // const handleHistory=()=>{
       // history.push("/")
   // }

   const handleNavigate=()=>{
     navigate.push("/")
   }

    return (
      
            <nav className='navbar'>
              <div className='navbar-container'>
              <Link to='/' className="navbar-logo" onClick={closeMobileMenu}>
                  <img className='icon' src={logo} alt=""/>
               </Link>

              
           
              <ul className={click? 'nav-menu active':'nav-menu'}>
                
                  <li className="nav-item">
                    <Link to="/" className='nav-links' onClick={closeMobileMenu}>
                        Home
                    </Link>
                  </li>
                  <li className="nav-item">
                    <Link to="/hoteli" className='nav-links' onClick={closeMobileMenu}>
                        Hoteli
                    </Link>
                  </li>  
                  <li className="nav-item">
                    <Link to="/aranzmani" className='nav-links' onClick={closeMobileMenu}>
                        Aranžmani
                    </Link>
                  </li> 
                  
                  <li className="nav-item">
                    <Link to="/proveriRezervaciju" className='nav-links' onClick={closeMobileMenu}>
                     Informacije o rezervaciji
                    </Link>
                  </li> 
                  
                   
                </ul>

              </div>
               
      </nav>
       
     
        
    )
}

export default NavBar