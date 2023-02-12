using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLibrary.Models
{
    public class RezervacijaDTO
    {
        public ObjectId Id { get; set; }
        public int BrSobe { get; set; }
        public string Legitimacija { get; set; }
        public string Status { get; set; }
        public string Sifra_Rezervacije { get; set; }
        public float Cena { get; set; }
        public IList<ObjectId> Niz_Usluga { get; set; }
        public IList<string> Usluge { get; set; }
        public string Gost { get; set; }
        public string Aranzman { get; set; }
        public string KucniLjubimac { get; set; }

        public int BrojKucnihLjubimaca { get; set; }
        public bool PostojiKucniLjubimac { get; set; }
    }
}
