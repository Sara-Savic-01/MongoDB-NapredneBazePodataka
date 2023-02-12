using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLibrary.Models
{
    public class Rezervacija
    {
        public ObjectId Id { get; set; }
        public int BrSobe { get; set; }
        public byte[] Legitimacija { get; set; }
        public string Status { get; set; }
        public string Sifra_Rezervacije { get; set; }
        public float Cena { get; set; }
        public IList<ObjectId> Niz_Usluga { get; set; }
        public ObjectId Gost { get; set; }
        public ObjectId Aranzman { get; set; }
        public ObjectId KucniLjubimac { get; set; }

        public static explicit operator Rezervacija(ObjectId o)
        {
            throw new NotImplementedException();
        }
    }
}
