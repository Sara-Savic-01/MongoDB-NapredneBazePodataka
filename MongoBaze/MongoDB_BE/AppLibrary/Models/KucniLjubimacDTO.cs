using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace AppLibrary.Models
{
    public class KucniLjubimacDTO
    {
        public string  Id { get; set; }
        public int BrojKucnihLjubimaca { get; set; }
        public bool PostojiKucniLjubimac { get; set; }

        public string Rezervacija { get; set; }
    }
}
