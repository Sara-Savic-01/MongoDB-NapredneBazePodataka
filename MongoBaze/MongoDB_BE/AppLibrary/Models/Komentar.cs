using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace AppLibrary.Models
{
    public class Komentar
    {
        public ObjectId Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Tekst { get; set; }
        public int Zvezdice { get; set; }
        public ObjectId Hotel { get; set; }
    }
}
