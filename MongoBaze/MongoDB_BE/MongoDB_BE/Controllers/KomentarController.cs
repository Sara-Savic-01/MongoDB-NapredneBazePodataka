using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppLibrary;
using AppLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MongoDB_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class KomentarController: Controller

    {
        [HttpGet]
        [Route("VratiSveKomentare")]
        public ActionResult VratiSveKomentare()
        {
            try
            {
                return new JsonResult(DataProvider.VratiSveKomentare());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpGet]
        [Route("VratiKomentar/{id}")]
        public ActionResult VratiKomentar([FromRoute(Name = "id")] string id)
        {
            try
            {
                return new JsonResult(DataProvider.VratiKomentar(id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpGet]
        [Route("VratiKomentareZaHotel/{id}")]
        public ActionResult VratiKomentareZaZaHotel([FromRoute(Name = "id")] string id)
        {
            try
            {
                return new JsonResult(DataProvider.VratiKomentareZaHotel(id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpPost]
        [Route("KreirajKomentar/{hotelId}")]
        public ActionResult KreirajKomentar([FromRoute(Name = "hotelId")] string id, [FromBody] Komentar komentar)
        {
            try
            {
                DataProvider.KreirajKomentar(id, komentar);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpDelete]
        [Route("ObrisiKomentar/{komentarId}")]
        public ActionResult ObrisiKomentar([FromRoute(Name = "komentarId")] string komentarId)
        {
            try
            {
                DataProvider.ObrisiKomentar(komentarId);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpPut]
        [Route("AzurirajKomentar/{komentarId}/{text}")]
        public ActionResult AzurirajKomentar([FromRoute(Name = "komentarId")] string komentarId,
                                                    [FromRoute(Name = "text")] string text)
        {
            try
            {
                DataProvider.AzurirajKomentar(komentarId, text);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }
    }
}
