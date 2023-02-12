using AppLibrary;
using AppLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GostController : Controller
    {
        [HttpGet]
        [Route("VratiSveGoste")]
        public ActionResult VratiSveGoste()
        {
            try
            {
                return new JsonResult(DataProvider.VratiSveGoste());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpGet]
        [Route("VratiGosta/{id}")]
        public ActionResult VratiGosta([FromRoute(Name = "id")] string id)
        {
            try
            {
                return new JsonResult(DataProvider.VratiGosta(id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }
        [HttpGet]
        [Route("VratiGostaJmbg/{jmbg}")]
        public ActionResult VratiGostaJmbg([FromRoute(Name = "jmbg")] string jmbg)
        {
            try
            {
                Gost g = DataProvider.VratiGostaJmbg(jmbg);
                if (g != null)
                    return Ok(g);
                else
                    return NotFound();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpGet]
        [Route("VratiGosteZaAranzman/{sifra}")]
        public ActionResult VratiGosteZaAranzman([FromRoute(Name = "sifra")] String sifra)
        {
            try
            {
                return new JsonResult(DataProvider.VratiGosteZaAranzman(sifra));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }

        }

        [HttpPost]
        [Route("KreirajKolekcijuGosta")]
        public ActionResult KreirajKolekcijuGosta()
        {
            try
            {
                DataProvider.KreirajKolekcijuGosta();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpPost]
        [Route("KreirajGosta")]
        public ActionResult KreirajGosta([FromBody] Gost gost)
        {
            try
            {
                ObjectId retVal = DataProvider.KreirajGosta(gost);
                return new JsonResult(retVal.ToString());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpDelete]
        [Route("ObrisiGosta/{jmbg}")]
        public ActionResult ObrisiGosta([FromRoute(Name = "jmbg")] String jmbg)
        {
            try
            {
                DataProvider.ObrisiGosta(jmbg);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpPut]
        [Route("DodajRezervacijuGostu/{sifra}/{jmbg}")]
        public ActionResult DodajRezervacijuGostu([FromRoute(Name = "sifra")] String sifra,
                                                          [FromRoute(Name = "jmbg")] String jmbg)
        {
            try
            {
                DataProvider.DodajRezervacijuGostu(sifra, jmbg);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }



        [HttpPut]
        [Route("AzurirajGosta/{id}")]
        public ActionResult AzurirajGosta([FromRoute] string id, [FromBody] GostDTOUpdate gostDTOUpdate)
        {
            try
            {
                DataProvider.AzurirajGosta(id, gostDTOUpdate);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }
    }
}
