import './App.css';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';

import Home from './pages/HomeStrana/Home.js';
import NavBar from './components/NavBar';
import Hoteli from './pages/Hoteli/Hoteli.js';
import HotelDetaljno from './pages/Hoteli/HotelDetaljno';
import Aranzmani from './pages/Aranzmani/Aranzmani.js';
import AranzmanDetaljniPrikaz from './pages/Aranzmani/AranzmanDetaljniPrikaz';
import ProveriRezervaciju from './pages/Rezervacija/ProveriRezervaciju';
import KreirajGosta from './pages/Aranzmani/KreirajGosta';
import Komentari from './pages/Komentari/Komentari';
import KreirajKomentar from './pages/Komentari/KreirajKomentar';
import Usluge from './pages/Aranzmani/Usluge.js';
import ProveriDetaljnoRezervaciju from './pages/Rezervacija/ProveriDetaljnoRezervaciju';
import Gost from './pages/Rezervacija/Gost';


function App() {
  return (
    <Router>
      <NavBar/>
      <Routes>
        <Route exact path="/" element={<Home/>}/>
        <Route path="/hoteli" element={<Hoteli/>}/>
        <Route exact path = "/hoteli/:id" element = {<HotelDetaljno/>} />
        <Route path="/aranzmani" element={<Aranzmani/>}/>
        <Route exact path = "/aranzmani/:id" element = {<AranzmanDetaljniPrikaz/>} />
        <Route exact path = "/KreirajGosta/:id" element = {<KreirajGosta/>}/>
        <Route path="/proveriRezervaciju" element={<ProveriRezervaciju/>}/>
        <Route path="/komentari" element={<Komentari/>}/>
        <Route exact path = "/KreirajKomentar/:id" element = {<KreirajKomentar/>}/>
        <Route path="usluge/:idRez" element={<Usluge/>}/>
        <Route exact path="/ProveriDetaljnoRezervaciju/:kodRez" element={<ProveriDetaljnoRezervaciju/>}/>
        <Route  path="/Gost" element={<Gost/>}/>



      </Routes>
    </Router>
  );
}

export default App;
