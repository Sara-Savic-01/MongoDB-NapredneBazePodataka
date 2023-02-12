using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLibrary.Models
{
   public class HotelDTO
    {
        public string Id { get; set; }

        public string Naziv { get; set; }

        public string Opis { get; set; }

        public int GodinaOsnivanja { get; set; }

        public string Grad { get; set; }

        public IList<string> Aranzmani { get; set; }

        public IList<string> Komentari { get; set; }

        public HotelDTO()
        {
            Aranzmani = new List<string>();
            Komentari = new List<string>();
        }

    }
}
