using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using AppLibrary;
using AppLibrary.Models;

namespace MongoDB_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AranzmanController : ControllerBase
    {


        [HttpPost]
        [Route("KreirajAranzman")]
        public ActionResult KreirajAranzman([FromBody] AranzmanDTO aranzman)
        {
            try
            {
                Aranzman novaAranzman = new Aranzman()
                {
                    CenaAranzmana = aranzman.CenaAranzmana,
                    DatumAranzmana = aranzman.DatumAranzmana,
                    BrojNocenja = aranzman.BrojNocenja,
                    BrojSoba = aranzman.BrojSoba,
                    BrojPreostalihSoba = aranzman.BrojPreostalihSoba,
                    TipSobe = aranzman.TipSobe,
                    Hotel = new ObjectId(aranzman.Hotel)
                };
                DataProvider.KreirajAranzman(novaAranzman);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }

        }

        [HttpGet]
        [Route("VratiSveAranzmane")]
        public ActionResult VratiSveAranzmane()
        {
            try
            {
                return new JsonResult(DataProvider.VratiSveAranzmane());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpGet]
        [Route("VratiSveAranzmaneObjectId")]
        public ActionResult VratiSveAranzmaneObjectId()
        {
            try
            {
                return new JsonResult(DataProvider.VratiSveAranzmaneObjectId());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpGet]
        [Route("VratiSveGotoveAranzmane")]
        public ActionResult VratiSveGotoveAranzmane()
        {
            try
            {
                return new JsonResult(DataProvider.VratiSveGotoveAranzmane());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpGet]
        [Route("VratiSveAktivneAranzmane")]
        public ActionResult VratiSveAktivneAranzmane()
        {
            try
            {
                return new JsonResult(DataProvider.VratiSveAktivneAranzmane());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpGet]
        [Route("VratiAranzman/{id}")]
        public ActionResult VratiAranzman([FromRoute] string id)
        {
            try
            {
                return new JsonResult(DataProvider.VratiAranzman(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpPut]
        [Route("AzurirajAranzman/{id}")]
        public ActionResult AzurirajAranzman([FromRoute] string id, [FromBody] AranzmanDTO aranzman)
        {
            try
            {
                DataProvider.AzurirajAranzman(id, aranzman);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpDelete]
        [Route("ObrisiAranzman/{id}")]
        public ActionResult ObrisiAranzman([FromRoute] string id)
        {
            try
            {
                DataProvider.ObrisiAranzman(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }
    }
}
