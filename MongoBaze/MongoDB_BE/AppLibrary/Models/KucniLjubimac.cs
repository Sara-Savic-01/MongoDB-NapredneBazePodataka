using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace AppLibrary.Models
{
    public class KucniLjubimac
    {
        public ObjectId Id { get; set; }
        public int BrojKucnihLjubimaca { get; set; }
        public bool PostojiKucniLjubimac { get; set; }

        public ObjectId Rezervacija { get; set; }

    }
}
