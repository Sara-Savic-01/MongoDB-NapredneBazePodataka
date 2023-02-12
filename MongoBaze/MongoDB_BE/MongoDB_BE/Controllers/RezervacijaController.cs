using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using AppLibrary;
using AppLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RezervacijaController : Controller
    {
        [HttpPost]
        [Route("KreirajRezervacije")]
        public ActionResult KreirajRezervacije()
        {
            try
            {
                DataProvider.KreirajRezervacije();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpPost]
        [Route("KreirajRezervaciju")]
        public ActionResult KreirajRezervaciju([FromBody] RezervacijaDTO rezervacija)
        {
            try
            {
                //IList<KucniLjubimac> llist = DataProvider.VratiSveKucneLjubimce();
                KucniLjubimac kucniLjubimac = null;
               /* foreach (KucniLjubimac klj in llist)
                {
                    if (klj.PostojiKucniLjubimac.CompareTo(true) == 0)
                    {
                        kucniLjubimac = klj;
                        break;
                    }
                }*/
                if (kucniLjubimac == null)
                {
                    kucniLjubimac = new KucniLjubimac();
                    kucniLjubimac.BrojKucnihLjubimaca = rezervacija.BrojKucnihLjubimaca;
                    kucniLjubimac.PostojiKucniLjubimac = rezervacija.PostojiKucniLjubimac;
                    DataProvider.KreirajKucnogLjubimca(kucniLjubimac);
                } 

                
                Random rnd = new Random();
                string pom = rnd.Next(100000, 999999).ToString();
                Rezervacija r = new Rezervacija
                {
                    //Id = rezervacija.Id,
                    BrSobe = rezervacija.BrSobe,
                    Legitimacija = Convert.FromBase64String(rezervacija.Legitimacija),
                    Status = rezervacija.Status,
                    Sifra_Rezervacije = pom, 
                    Cena = rezervacija.Cena,
                    Niz_Usluga = rezervacija.Niz_Usluga,
                    Gost = new ObjectId(rezervacija.Gost),
                    Aranzman = new ObjectId(rezervacija.Aranzman),
                    KucniLjubimac = kucniLjubimac.Id
                };              

                return new JsonResult(DataProvider.KreirajRezervaciju(r).ToString());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpGet]
        [Route("VratiRezervaciju/{Sifra_Rezervacije}")]
        public ActionResult VratiRezervaciju([FromRoute(Name = "Sifra_Rezervacije")] string Sifra_Rezervacije)
        {
            try
            {
                Rezervacija rez = DataProvider.VratiRezervaciju(Sifra_Rezervacije);
                if (rez != null)
                    return Ok(rez);
                else
                    return NotFound();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpGet]
        [Route("VratiRezervacijuId/{Id}")]
        public ActionResult VratiRezervacijuId([FromRoute(Name = "Id")] string Id)
        {
            try
            {
                return Ok(DataProvider.VratiRezervacijuId(Id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpGet]
        [Route("VratiRezervacije")]
        public ActionResult VratiRezervacije()
        {
            try
            {
                return Ok(DataProvider.VratiRezervacije());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpDelete]
        [Route("ObirisRezervaciju/{Sifra_Rezervacije}")]
        public ActionResult ObrisiRezervaciju([FromRoute(Name = "Sifra_Rezervacije")] string Sifra_Rezervacije)
        {
            try
            {
                DataProvider.ObrisiRezervaciju(Sifra_Rezervacije);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpPut]
        [Route("AzurirajStatus/{Sifra_Rezervacije}/{status}")]
        public ActionResult AzurirajStatusRezervaciji([FromRoute(Name = "Sifra_Rezervacije")] string Sifra_Rezervacije,
                                                    [FromRoute(Name = "status")] string status)
        {
            try
            {
                DataProvider.AzurirajRezervaciju(Sifra_Rezervacije, status);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpPut]
        [Route("DodajUsluge/{IdRez}")]
        public ActionResult DodajUsluge([FromRoute(Name = "IdRez")] string IdRez, [FromBody] String[] usluge)
        {
            try
            {
                DataProvider.DodajUsluge(IdRez, usluge);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

    }


}
