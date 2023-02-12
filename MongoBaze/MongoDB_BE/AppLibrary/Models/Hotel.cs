using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLibrary.Models
{
    public class Hotel
    {

        public ObjectId Id { get; set; }

        public string Naziv { get; set; }

        public string Opis { get; set; }

        public int GodinaOsnivanja { get; set; }

        public string Grad { get; set; }

        public IList<ObjectId> Aranzmani { get; set; }

        public IList<ObjectId> Komentari { get; set; }

        public Hotel()
        {
            Aranzmani = new List<ObjectId>();
            Komentari = new List<ObjectId>();
        }
    }
}
