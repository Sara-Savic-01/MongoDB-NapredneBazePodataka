using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace AppLibrary.Models
{
    public class Gost
    {
        public ObjectId Id { get; set; }
        public String Jmbg { get; set; }
        public String Ime { get; set; }
        public String Prezime { get; set; }
        public String Email { get; set; }
        public String Broj_Telefona { get; set; }
        public List<ObjectId> Rezervacije { get; set; }

        public Gost()
        {
            Rezervacije = new List<ObjectId>();
        }
    }
}
