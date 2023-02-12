using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppLibrary;
using AppLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace MongoDB_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UslugaController:Controller
    {
        [HttpPost]
        [Route("KreirajUslugu")]
        public ActionResult KreirajUslugu([FromBody] UslugaDTO usluga)
        {
            try
            {
                Usluga u = new Usluga
                {
                    Naziv = usluga.Naziv,
                    Cena=usluga.Cena,
                    SlikaBytes=Convert.FromBase64String(usluga.SlikaBytesBase)

               
                };

                DataProvider.KreirajUslugu(u);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpGet]
        [Route("VratiUsluge")]
        public ActionResult VratiUsluge()
        {
            try
            {
                IList<Usluga> usluge = DataProvider.VratiUsluge();
                IList<UslugaDTO> returnList = new List<UslugaDTO>();
                foreach (Usluga u in usluge)
                {
                    if (u.Naziv!= null)
                    {
                        returnList.Add(new UslugaDTO()
                        {
                            Id = u.Id.ToString(),
                            Naziv= u.Naziv,
                            Cena=u.Cena,
                            SlikaBytesBase=Convert.ToBase64String(u.SlikaBytes)
                          
                        });
                    }
                }
                return Ok(returnList);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpPut]
        [Route("AzurirajCenuUsluge/{idUsluge}/{novaCena}")]
        public ActionResult AzurirajCenuUsluge([FromRoute(Name = "idUsluge")] string idUsluge,
                                                    [FromRoute(Name = "novaCena")] int novaCena)
        {
            try
            {
                DataProvider.AzurirajCenuUsluge(new ObjectId(idUsluge), novaCena);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpDelete]
        [Route("ObrisiUslugu/{uslugaId}")]
        public ActionResult ObrisiUslugu([FromRoute(Name = "uslugaId")] string uslugaId)
        {
            try
            {
                DataProvider.ObrisiUslugu(new ObjectId(uslugaId));
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }
    }
}
