using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace AppLibrary.Models
{
    public class UslugaDTO
    {
        public string Id { get; set; }
        public string Naziv { get; set; }
        public float Cena { get; set; }
        public string  SlikaBytesBase { get; set; }
    }
}
