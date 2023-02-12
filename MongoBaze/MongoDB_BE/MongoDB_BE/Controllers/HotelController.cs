using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppLibrary;
using AppLibrary.Models;
using Microsoft.AspNetCore.Http;


namespace MongoDB_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HotelController : Controller
    {
        [HttpPost]
        [Route("KreirajHotel")]
        public ActionResult KreirajHotel([FromBody] Hotel hotel)
        {
            try
            {
                DataProvider.KreirajHotel(hotel);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }

        }

        [HttpGet]
        [Route("VratiHotele")]
        public ActionResult VratiHotele()
        {
            try
            {
                return new JsonResult(DataProvider.VratiHotele());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpGet]
        [Route("VratiHotel/{id}")]
        public ActionResult VratiHotel([FromRoute] string id)
        {
            try
            {
                return new JsonResult(DataProvider.VratiHotel(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpPut]
        [Route("AzurirajHotel/{id}")]
        public ActionResult AzurirajHotel([FromRoute] string id, [FromBody] HotelDTOUpdate hotel)
        {
            try
            {
                DataProvider.AzurirajHotel(id, hotel);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpDelete]
        [Route("ObrisiHotel/{id}")]
        public IActionResult ObrisiHotel([FromRoute] string id)
        {
            try
            {
                DataProvider.ObrisiHotel(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }


    }
}
