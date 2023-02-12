using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace AppLibrary.Models
{
    public class Aranzman
    {
        public ObjectId Id { get; set; }

        public float CenaAranzmana { get; set; }

        public string TipSobe { get; set; }

        public DateTime DatumAranzmana { get; set; }

        public int BrojNocenja { get; set;}

        public int BrojSoba { get; set; }

        public int BrojPreostalihSoba { get; set; }

        public IList<ObjectId> ListaRezervacija { get; set; }

        public ObjectId Hotel { get; set; }

        public Aranzman()
        {
            ListaRezervacija = new List<ObjectId>();
        }
    }
}
