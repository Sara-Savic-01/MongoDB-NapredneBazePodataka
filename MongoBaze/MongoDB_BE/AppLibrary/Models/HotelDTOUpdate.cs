using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLibrary.Models
{
   public class HotelDTOUpdate
    {
        public string Naziv { get; set; }

        public string Opis { get; set; }

        public int GodinaOsnivanja { get; set; }

        public string Grad { get; set; }

        public HotelDTOUpdate()
        {

        }
    }
}
