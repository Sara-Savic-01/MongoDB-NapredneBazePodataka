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
    public class KucniLjubimacController:Controller
    {

        [HttpPost]
        [Route("KreirajKucnogLjubimca")]
        public ActionResult KreirajKucnogLjubimca([FromBody] KucniLjubimacDTO kucniLjubimac)
        {
            try
            {
                KucniLjubimac klj = new KucniLjubimac()
                {
                    BrojKucnihLjubimaca = kucniLjubimac.BrojKucnihLjubimaca,
                    PostojiKucniLjubimac = kucniLjubimac.PostojiKucniLjubimac,
                    Rezervacija = new ObjectId(kucniLjubimac.Rezervacija)
                    
                };
                DataProvider.KreirajKucnogLjubimca(klj);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpGet]
        [Route("VratiKucnogLjubimca/{id}")]
        public ActionResult VratiKucnogLjubimca([FromRoute] string id) 
        {
            try
            {
                return new JsonResult(DataProvider.VratiKucnogLjubimca(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpGet]
        [Route("VratiSveKucneLjubimce")]
        public ActionResult VratiSveKucneLjubimce()
        {
            try
            {
                IList<KucniLjubimacDTO> returnList = new List<KucniLjubimacDTO>();
                foreach (KucniLjubimac klj in DataProvider.VratiSveKucneLjubimce())
                {
                    returnList.Add(new KucniLjubimacDTO()
                    {
                        Id = klj.Id.ToString(),
                        BrojKucnihLjubimaca=klj.BrojKucnihLjubimaca,
                        PostojiKucniLjubimac=klj.PostojiKucniLjubimac,
                        Rezervacija=klj.Rezervacija.ToString()
                      
                    });
                }
                return Ok(returnList);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        

        [HttpDelete]
        [Route("ObrisiKucnogLjubimca/{kucniLjubimacId}")]
        public ActionResult ObrisiKucnogLjubimca([FromRoute(Name = "kucniLjubimacId")] string kucniLjubimacId)
        {
            try
            {
                DataProvider.ObrisiKucnogLjubimca(new ObjectId(kucniLjubimacId));
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpPut]
        [Route("AzurirajBrojKucnihLjubimaca/{idKucnogLjubimca}/{noviBrojKucnihLjubimaca}")]
        public ActionResult AzurirajBrojKucnihLjubimaca([FromRoute] string idKucnogLjubimca,
                                                            [FromRoute(Name = "noviBrojKucnihLjubimaca")] int noviBrojKucnihLjubimaca)
        {
            try
            {
                DataProvider.AzurirajBrojKucnihLjubimaca(new ObjectId(idKucnogLjubimca), noviBrojKucnihLjubimaca);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }
        
    }
}
