using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLibrary.Models
{
    public class AranzmanDTO
    {
        public string Id { get; set; }

        public float CenaAranzmana { get; set; }

        public string TipSobe { get; set; }

        public DateTime DatumAranzmana { get; set; }

        public int BrojNocenja { get; set;}

        public int BrojSoba { get; set; }

        public int BrojPreostalihSoba { get; set; }

        public IList<string> ListaRezervacija { get; set; }

        public string Hotel { get; set; }

        public AranzmanDTO()
        {
            ListaRezervacija = new List<string>();
        }
    }
}
