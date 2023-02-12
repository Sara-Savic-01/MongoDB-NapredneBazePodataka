using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using AppLibrary.Models;

namespace AppLibrary
{
   public class DataProvider
    {
        #region Hotel
        public static void KreirajHotel(Hotel hotel)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Hotel>("hotel");
            collection.InsertOne(hotel);
        }


        public static IList<HotelDTO> VratiHotele()
        {
            IMongoDatabase db = Session.MongoDatabase;
            IList<Hotel> Hoteli = db.GetCollection<Hotel>("hotel").Find(x => true).ToList<Hotel>();

            IList<HotelDTO> HotelDTO = new List<HotelDTO>();
            foreach (var h in Hoteli)
            {
                HotelDTO pom = new HotelDTO();
                pom.Id = h.Id.ToString();
                pom.Naziv = h.Naziv;
                pom.Opis = h.Opis;
                pom.GodinaOsnivanja = h.GodinaOsnivanja;
                pom.Grad = h.Grad;

                foreach (var let in h.Aranzmani)
                {
                    pom.Aranzmani.Add(let.ToString());
                }

                foreach (var k in h.Komentari)
                {
                    pom.Komentari.Add(k.ToString());
                }

                HotelDTO.Add(pom);
            }

            return HotelDTO;
        }

        public static HotelDTO VratiHotel(string id)
        {
            IMongoDatabase db = Session.MongoDatabase;
            Hotel h = db.GetCollection<Hotel>("hotel").Find(x => x.Id == new ObjectId(id)).FirstOrDefault();


            HotelDTO pom = new HotelDTO();

            if (h != null)
            {

                pom.Id = h.Id.ToString();
                pom.GodinaOsnivanja = h.GodinaOsnivanja;
                pom.Grad = h.Grad;
                pom.Naziv = h.Naziv;
                pom.Opis = h.Opis;
                foreach (var let in h.Aranzmani)
                {
                    pom.Aranzmani.Add(let.ToString());
                }

                foreach (var k in h.Komentari)
                {
                    pom.Komentari.Add(k.ToString());
                }
            }
            return pom;


        }

        public static void AzurirajHotel(string id, HotelDTOUpdate hotelDTOUpdate)
        {
            IMongoDatabase db = Session.MongoDatabase;

            IMongoCollection<Hotel> hotelCollection = db.GetCollection<Hotel>("hotel");

            Hotel hotel = hotelCollection.Find(a => a.Id == new ObjectId(id)).FirstOrDefault();

            if (hotel != null)
            {
                hotel.Naziv = hotelDTOUpdate.Naziv;
                hotel.Opis = hotelDTOUpdate.Opis;
                hotel.GodinaOsnivanja = hotelDTOUpdate.GodinaOsnivanja;
                hotel.Grad = hotelDTOUpdate.Grad;

                hotelCollection.ReplaceOne(x => x.Id == new ObjectId(id), hotel);
            }
        }

        public static void DodajHoteluAranzman(ObjectId aranzmanId, ObjectId hotelId)
        {
            IMongoDatabase db = Session.MongoDatabase;
            IMongoCollection<Hotel> hotelCollection = db.GetCollection<Hotel>("hotel");
            Hotel hotel = hotelCollection.Find(x => x.Id == hotelId).FirstOrDefault();
            hotel.Aranzmani.Add(aranzmanId);
            hotelCollection.ReplaceOne(x => x.Id == hotelId, hotel);
        }

        public static void ObrisiHoteluAranzman(ObjectId aranzmanId, ObjectId hotelId)
        {
            IMongoDatabase db = Session.MongoDatabase;
            IMongoCollection<Hotel> hotelCollection = db.GetCollection<Hotel>("hotel");
            Hotel hotel = hotelCollection.Find(x => x.Id == hotelId).FirstOrDefault();
            hotel.Aranzmani.Remove(aranzmanId);
            hotelCollection.ReplaceOne(x => x.Id == hotelId, hotel);
        }



        public static void ObrisiHotel(string id)
        {
            IMongoDatabase db = Session.MongoDatabase;

            IMongoCollection<Hotel> hotelCollection = db.GetCollection<Hotel>("hotel");

            Hotel hotelZaBrisanje = hotelCollection.Find(x => x.Id == new ObjectId(id)).FirstOrDefault();

            if (hotelZaBrisanje != null)
            {
                foreach (var aranzman in hotelZaBrisanje.Aranzmani)
                {
                    DataProvider.ObrisiAranzman(aranzman.ToString());
                }

                hotelCollection.DeleteOne(a => a.Id == new ObjectId(id));
            }
        }
        #endregion

        #region Rezervacija

        public static void KreirajRezervacije()
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Rezervacija>("rezervacije");
        }

        public static void DodajUsluge(string idRez, string[] usluge)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Rezervacija>("rezervacije");
            Rezervacija rez = db.GetCollection<Rezervacija>("rezervacije").Find(x => x.Id == new ObjectId(idRez)).FirstOrDefault();

            IList<ObjectId> retList = new List<ObjectId>();
            IList<Usluga> uslugaList = new List<Usluga>();
            foreach (String u in usluge)
            {
                Usluga pom = db.GetCollection<Usluga>("usluge").Find(x => x.Id == new ObjectId(u)).FirstOrDefault();

                retList.Add(new ObjectId(u));
                uslugaList.Add(pom);
            }
            float tempCena = rez.Cena;
            foreach(Usluga u in uslugaList)
            {
                tempCena += u.Cena;
            }

            AzurirajCenuRezervacije(rez.Sifra_Rezervacije, tempCena);

            var filter = Builders<Rezervacija>.Filter.Eq(x => x.Id, new ObjectId(idRez));
            var update = Builders<Rezervacija>.Update.Set(x => x.Niz_Usluga, retList);
            collection.UpdateOne(filter, update);
        }

        public static ObjectId KreirajRezervaciju(Rezervacija rez)
        {
            IMongoDatabase db = Session.MongoDatabase;
            Gost gost = VratiGosta(rez.Gost.ToString());
            gost.Rezervacije.Append(rez.Id);

            //DodajKucnogLjubimcaURezervaciju(rez.KucniLjubimac, rez.Id);
            var collection = db.GetCollection<Rezervacija>("rezervacije");
            collection.InsertOne(rez);
            DodajKucnogLjubimcaURezervaciju(rez.KucniLjubimac, rez.Id);
            KucniLjubimac klj = VratiKucnogLjubimca(rez.KucniLjubimac.ToString());
            float novaCena = rez.Cena + klj.BrojKucnihLjubimaca * 1500; 
            AzurirajCenuRezervacije(rez.Sifra_Rezervacije, novaCena);

            return rez.Id;
        }

        public static Rezervacija VratiRezervaciju(string sifra)
        {
            IMongoDatabase db = Session.MongoDatabase;
            return db.GetCollection<Rezervacija>("rezervacije").Find(x => x.Sifra_Rezervacije == sifra).FirstOrDefault();
        }

        public static RezervacijaDTO VratiRezervacijuId(string id)
        {
            IMongoDatabase db = Session.MongoDatabase;
            Rezervacija rez = db.GetCollection<Rezervacija>("rezervacije").Find(x => x.Id == new ObjectId(id)).FirstOrDefault();

            IList<string> niz_usluga = new List<string>();
            if (rez.Niz_Usluga != null)
            {
                foreach (ObjectId idProz in rez.Niz_Usluga)
                {
                    niz_usluga.Add(DataProvider.VratiUslugu(idProz.ToString()).Naziv);
                }
            }

            RezervacijaDTO rezDTO = new RezervacijaDTO()
            {
                Id = rez.Id,
                Status = rez.Status,
                Sifra_Rezervacije = rez.Sifra_Rezervacije,
                Cena = rez.Cena,
                Legitimacija = Convert.ToBase64String(rez.Legitimacija),
                Usluge = niz_usluga
            };
            return rezDTO;
        }

        public static IList<Rezervacija> VratiRezervacije()
        {
            IMongoDatabase db = Session.MongoDatabase;
            return db.GetCollection<Rezervacija>("rezervacije").Find(x => true).ToList<Rezervacija>();
        }

        public static void ObrisiRezervaciju(String sifra)
        {
            IMongoDatabase db = Session.MongoDatabase;
            db.GetCollection<Rezervacija>("rezervacije").DeleteOne(x => x.Sifra_Rezervacije == sifra);
        }

        public static void AzurirajRezervaciju(String sifra, String status)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var filter = Builders<Rezervacija>.Filter.Eq(x => x.Sifra_Rezervacije, sifra);
            var update = Builders<Rezervacija>.Update.Set(x => x.Status, status);
            db.GetCollection<Rezervacija>("rezervacije").UpdateOne(filter, update);
        }

        public static void AzurirajCenuRezervacije(String sifra, float novaCena)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var filter = Builders<Rezervacija>.Filter.Eq(x => x.Sifra_Rezervacije, sifra);
            var update = Builders<Rezervacija>.Update.Set(x => x.Cena, novaCena);
            db.GetCollection<Rezervacija>("rezervacije").UpdateOne(filter, update);
        }

        #endregion


        #region Aranzman

        public static void KreirajAranzman(Aranzman aranzman)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var aranzmani = db.GetCollection<Aranzman>("aranzman");
            aranzmani.InsertOne(aranzman);

            if (aranzman.Hotel != ObjectId.Empty)
            {
                DodajHoteluAranzman(aranzman.Id, aranzman.Hotel);
            }
        }

        public static IList<AranzmanDTO> VratiSveAranzmane()
        {
            IMongoDatabase db = Session.MongoDatabase;
            IList<Aranzman> aranzmani = db.GetCollection<Aranzman>("aranzman").Find(x => true).ToList<Aranzman>();
            IList<AranzmanDTO> aranzmaniDTO = new List<AranzmanDTO>();

            foreach (var a in aranzmani)
            {
                AranzmanDTO pom = new AranzmanDTO();
                pom.Id = a.Id.ToString();
                pom.CenaAranzmana = a.CenaAranzmana;
                pom.DatumAranzmana = a.DatumAranzmana;
                pom.BrojNocenja = a.BrojNocenja;
                pom.BrojSoba = a.BrojSoba;
                pom.BrojPreostalihSoba = a.BrojPreostalihSoba;
                pom.TipSobe = a.TipSobe;

                pom.ListaRezervacija = new List<string>();
                foreach (var rez in a.ListaRezervacija)
                {
                    pom.ListaRezervacija.Add(rez.ToString());
                }
                //if (l.Hotel.CompareTo(ObjectId.Empty) == 0)
                //{
                //    pom.Hotel = "";
                //}
               // else
                //{
                    Hotel hotel = db.GetCollection<Hotel>("hotel").Find(x => x.Id == a.Hotel).FirstOrDefault();
                    if (hotel != null)
                    {
                        pom.Hotel = hotel.Id.ToString();
                    }
                //}

                aranzmaniDTO.Add(pom);
            }

            return aranzmaniDTO;
        }


        public static IList<AranzmanDTO> VratiSveGotoveAranzmane()
        {
            IMongoDatabase db = Session.MongoDatabase;
            IList<Aranzman> aranzmani = db.GetCollection<Aranzman>("aranzman").Find(x => (x.DatumAranzmana < DateTime.Now)).ToList<Aranzman>();
            IList<AranzmanDTO> aranzmaniDTO = new List<AranzmanDTO>();

            foreach (var a in aranzmani)
            {
                AranzmanDTO pom = new AranzmanDTO();
                pom.Id = a.Id.ToString();
                pom.CenaAranzmana = a.CenaAranzmana;
                pom.DatumAranzmana = a.DatumAranzmana;
                pom.BrojNocenja = a.BrojNocenja;
                pom.BrojSoba = a.BrojSoba;
                pom.BrojPreostalihSoba = a.BrojPreostalihSoba;
                pom.TipSobe = a.TipSobe;

                pom.ListaRezervacija = new List<string>();
                foreach (var rez in a.ListaRezervacija)
                {
                    pom.ListaRezervacija.Add(rez.ToString());
                }
                //if (l.Hotel.CompareTo(ObjectId.Empty) == 0)
                //{
                //    pom.Hotel = "";
                //}
                // else
                //{
                Hotel hotel = db.GetCollection<Hotel>("hotel").Find(x => x.Id == a.Hotel).FirstOrDefault();
                if (hotel != null)
                {
                    pom.Hotel = hotel.Id.ToString();
                }
                //}

                aranzmaniDTO.Add(pom);
            }

            return aranzmaniDTO;
        }

        public static IList<AranzmanDTO> VratiSveAktivneAranzmane()
        {
            IMongoDatabase db = Session.MongoDatabase;
            IList<Aranzman> aranzmani = db.GetCollection<Aranzman>("aranzman").Find(x => (x.DatumAranzmana > DateTime.Now)).ToList<Aranzman>();
            IList<AranzmanDTO> aranzmaniDTO = new List<AranzmanDTO>();

            foreach (var a in aranzmani)
            {
                AranzmanDTO pom = new AranzmanDTO();
                pom.Id = a.Id.ToString();
                pom.CenaAranzmana = a.CenaAranzmana;
                pom.DatumAranzmana = a.DatumAranzmana;
                pom.BrojNocenja = a.BrojNocenja;
                pom.BrojSoba = a.BrojSoba;
                pom.BrojPreostalihSoba = a.BrojPreostalihSoba;
                pom.TipSobe = a.TipSobe;

                pom.ListaRezervacija = new List<string>();
                foreach (var rez in a.ListaRezervacija)
                {
                    pom.ListaRezervacija.Add(rez.ToString());
                }
                //if (l.Hotel.CompareTo(ObjectId.Empty) == 0)
                //{
                //    pom.Hotel = "";
                //}
                // else
                //{
                Hotel hotel = db.GetCollection<Hotel>("hotel").Find(x => x.Id == a.Hotel).FirstOrDefault();
                if (hotel != null)
                {
                    pom.Hotel = hotel.Id.ToString();
                }
                //}

                aranzmaniDTO.Add(pom);
            }

            return aranzmaniDTO;
        }

        public static IList<Aranzman> VratiSveAranzmaneObjectId()
        {
            IMongoDatabase db = Session.MongoDatabase;
            IList<Aranzman> aranzmani = db.GetCollection<Aranzman>("aranzman").Find(x => true).ToList<Aranzman>();
            return aranzmani;
        }
        public static AranzmanDTO VratiAranzman(string id)
        {
            IMongoDatabase db = Session.MongoDatabase;
            Aranzman a = db.GetCollection<Aranzman>("aranzman").Find(x => x.Id == new ObjectId(id)).FirstOrDefault();

            AranzmanDTO pom = new AranzmanDTO();

            if (a != null)
            {

                pom.Id = a.Id.ToString();
                pom.CenaAranzmana = a.CenaAranzmana;
                pom.DatumAranzmana = a.DatumAranzmana;
                pom.BrojNocenja = a.BrojNocenja;
                pom.BrojSoba = a.BrojSoba;
                pom.BrojPreostalihSoba = a.BrojPreostalihSoba;
                pom.TipSobe = a.TipSobe;

                pom.ListaRezervacija = new List<string>();
                foreach (var rez in a.ListaRezervacija)
                {
                    pom.ListaRezervacija.Add(rez.ToString());
                }
                //if (l.Hotel.CompareTo(ObjectId.Empty) == 0)
                //{
                //    pom.Hotel = "";
                //}
                // else
                //{
                Hotel hotel = db.GetCollection<Hotel>("hotel").Find(x => x.Id == a.Hotel).FirstOrDefault();
                    if (hotel != null)
                    {
                        pom.Hotel = hotel.Id.ToString();
                    }
                //}

            }
            return pom;
        }


        public static void AzurirajAranzman(String id, AranzmanDTO aranzmanDTO)
        {
            IMongoDatabase db = Session.MongoDatabase;

            Aranzman pom = new Aranzman();
            pom.Id = new ObjectId(id);
            pom.CenaAranzmana = aranzmanDTO.CenaAranzmana;
            pom.DatumAranzmana = aranzmanDTO.DatumAranzmana;
            pom.BrojNocenja = aranzmanDTO.BrojNocenja;
            pom.BrojSoba = aranzmanDTO.BrojSoba;
            pom.BrojPreostalihSoba = aranzmanDTO.BrojPreostalihSoba;
            pom.TipSobe = aranzmanDTO.TipSobe;



            pom.ListaRezervacija = new List<ObjectId>();
            foreach (var rez in aranzmanDTO.ListaRezervacija)
            {
                pom.ListaRezervacija.Add(new ObjectId(rez));
            }
            if (aranzmanDTO.Hotel.Equals(""))
                pom.Hotel = ObjectId.Empty;
            else
            {
                IMongoCollection<Hotel> collectionHotel = db.GetCollection<Hotel>("hotel");
                Hotel h = collectionHotel.Find(x => x.Id == new ObjectId(aranzmanDTO.Hotel)).FirstOrDefault();

                if (h != null)
                {

                    if (!h.Aranzmani.Contains(pom.Id))
                    {
                        h.Aranzmani.Add(pom.Id);
                        collectionHotel.ReplaceOne(x => x.Id == h.Id, h);

                    }
                    pom.Hotel = new ObjectId(aranzmanDTO.Hotel);
                }


            }


            db.GetCollection<Aranzman>("aranzman").ReplaceOne(x => x.Id == new ObjectId(id), pom);
        }

        public static void ObrisiAranzman(string id) 
        {
            IMongoDatabase db = Session.MongoDatabase;

            Aranzman aranzman = db.GetCollection<Aranzman>("aranzman").Find(x => x.Id == new ObjectId(id)).FirstOrDefault();

            if (aranzman.Hotel != ObjectId.Empty)
            {
                ObrisiHoteluAranzman(aranzman.Id, aranzman.Hotel);
            }

            db.GetCollection<Aranzman>("aranzman").DeleteOne(x => x.Id == new ObjectId(id));
        }



        #endregion

        #region Komentar
        public static void KreirajKomentar(string id, Komentar komentar)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Komentar>("komentari");
            komentar.Hotel = new ObjectId(id);
            collection.InsertOne(komentar);
        }

        public static Komentar VratiKomentar(string id)
        {
            IMongoDatabase db = Session.MongoDatabase;
            Komentar komentar = db.GetCollection<Komentar>("komentari").Find(x => x.Id == new ObjectId(id)).FirstOrDefault();

            return komentar;
        }

        public static List<Komentar> VratiSveKomentare()
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Komentar>("komentari");

            List<Komentar> komentari = new List<Komentar>();

            foreach (Komentar komentar in collection.Find(x => true).ToList())
            {
                komentari.Add(komentar);
            }

            return komentari;
        }

        public static List<Komentar> VratiKomentareZaHotel(string id)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Komentar>("komentari");

            List<Komentar> komentari = new List<Komentar>();

            foreach (Komentar komentar in collection.Find(x => x.Hotel == new ObjectId(id)).ToList())
            {
                komentari.Add(komentar);
            }

            return komentari;
        }

        public static void AzurirajKomentar(string komentarId, String text)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var filter = Builders<Komentar>.Filter.Eq(x => x.Id, new ObjectId(komentarId));
            var update = Builders<Komentar>.Update.Set(x => x.Tekst, text);


            db.GetCollection<Komentar>("komentari").UpdateOne(filter, update);
        }

        public static void ObrisiKomentar(String komentarId)
        {
            IMongoDatabase db = Session.MongoDatabase;
            db.GetCollection<Komentar>("komentari").DeleteOne(x => x.Id == new ObjectId(komentarId));
        }


        #endregion

        #region Usluga

        public static void KreirajUslugu(Usluga u)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Usluga>("usluge");
            collection.InsertOne(u);
        }

        public static void KreirajKolekcijuUsluga()
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Usluga>("usluge");
        }

        public static Usluga VratiUslugu(string id)
        {
            IMongoDatabase db = Session.MongoDatabase;
            return db.GetCollection<Usluga>("usluge").Find(x => x.Id == new ObjectId(id)).FirstOrDefault();
        }


        public static IList<Usluga> VratiUsluge()
        {
            IMongoDatabase db = Session.MongoDatabase;
            return db.GetCollection<Usluga>("usluge").Find(x => true).ToList<Usluga>();
        }
        public static void ObrisiUslugu(ObjectId uslugaId)
        {
            IMongoDatabase db = Session.MongoDatabase;
            db.GetCollection<Usluga>("usluge").DeleteOne(x => x.Id == uslugaId);
        }
        public static void AzurirajCenuUsluge(ObjectId idUsluge, float novaCena)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var filter = Builders<Usluga>.Filter.Eq(x => x.Id, idUsluge);
            var update = Builders<Usluga>.Update.Set(x => x.Cena, novaCena);

            db.GetCollection<Usluga>("usluge").UpdateOne(filter, update);
        }

        #endregion

        #region KucniLjubimac
        public static void KreirajKucnogLjubimca(KucniLjubimac kucniLjubimac)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<KucniLjubimac>("kucniLjubimac");
            collection.InsertOne(kucniLjubimac);
        }

        public static KucniLjubimac VratiKucnogLjubimca(string id)
        {
            IMongoDatabase db = Session.MongoDatabase;
            KucniLjubimac kucniLjubimac = db.GetCollection<KucniLjubimac>("kucniLjubimac").Find(x => x.Id == new ObjectId(id)).FirstOrDefault();

            return kucniLjubimac;
        }

        public static KucniLjubimacDTO VratiKucnogLjubimcaDTO(string id)
        {
            IMongoDatabase db = Session.MongoDatabase;
            KucniLjubimac klj = db.GetCollection<KucniLjubimac>("kucniLjubimac").Find(x => x.Id == new ObjectId(id)).FirstOrDefault();


            KucniLjubimacDTO pom = new KucniLjubimacDTO();

            if (klj != null)
            {

                pom.Id = klj.Id.ToString();
                pom.PostojiKucniLjubimac = klj.PostojiKucniLjubimac;
                pom.BrojKucnihLjubimaca = klj.BrojKucnihLjubimaca;
                pom.Rezervacija = klj.Rezervacija.ToString();

            }
            return pom;
        }
            public static void KreirajKolekcijuKucnogLjubimca()
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<KucniLjubimac>("kucniLjubimac");
        }

        public static List<KucniLjubimac> VratiSveKucneLjubimce()
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<KucniLjubimac>("kucniLjubimac");

            List<KucniLjubimac> kucniLjubimac = new List<KucniLjubimac>();

            foreach (KucniLjubimac klj in collection.Find(x => true).ToList())
            {
                kucniLjubimac.Add(klj);
            }

            return kucniLjubimac;
        }

        public static void AzurirajBrojKucnihLjubimaca(ObjectId idKLj, int noviBr)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var filter = Builders<KucniLjubimac>.Filter.Eq(x => x.Id, idKLj);
            var update = Builders<KucniLjubimac>.Update.Set(x => x.BrojKucnihLjubimaca, noviBr);

            db.GetCollection<KucniLjubimac>("kucniLjubimac").UpdateOne(filter, update);


        }

        public static void DodajKucnogLjubimcaURezervaciju(ObjectId idKLj, ObjectId idRez)
        {
            IMongoDatabase db = Session.MongoDatabase;

            IMongoCollection<KucniLjubimac> kucniLjubimacCollection = db.GetCollection<KucniLjubimac>("kucniLjubimac");


            KucniLjubimac kucniLjubimac = kucniLjubimacCollection.Find(a => a.Id == idKLj).FirstOrDefault();

            if (kucniLjubimac != null)
            {
                kucniLjubimac.Rezervacija = idRez;



                kucniLjubimacCollection.ReplaceOne(x => x.Id == idKLj, kucniLjubimac);
            }


        }

        public static void ObrisiKucnogLjubimca(ObjectId kucniLjubimacId)
        {
            IMongoDatabase db = Session.MongoDatabase;
            db.GetCollection<KucniLjubimac>("kucniLjubimac").DeleteOne(x => x.Id == kucniLjubimacId);
        }
        #endregion

        #region Gost
        public static void KreirajKolekcijuGosta()
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Gost>("gosti");
        }

        public static Gost VratiGosta(string id)
        {
            IMongoDatabase db = Session.MongoDatabase;
            Gost gost = db.GetCollection<Gost>("gosti").Find(x => x.Id == new ObjectId(id)).FirstOrDefault();

            return gost;
        }
        public static Gost VratiGostaJmbg(string jmbg)
        {
            IMongoDatabase db = Session.MongoDatabase;
            return db.GetCollection<Gost>("gosti").Find(x => x.Jmbg == jmbg).FirstOrDefault();
        }

        public static List<GostDTO> VratiSveGoste()
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Gost>("gosti");
            var rezervacijeCollection = db.GetCollection<Rezervacija>("rezervacije");

            List<GostDTO> gosti = new List<GostDTO>();

            foreach (Gost gost in collection.Find(x => true).ToList())
            {
                List<Rezervacija> rezervacije = new List<Rezervacija>();
                if (gost.Rezervacije != null)
                {
                    foreach (ObjectId rezId in gost.Rezervacije)
                    {
                        rezervacije.Add(rezervacijeCollection.Find(x => x.Id == rezId).First());
                    }
                }
                GostDTO newGost = new GostDTO
                {
                    Id = gost.Id,
                    Jmbg = gost.Jmbg,
                    Ime = gost.Ime,
                    Prezime = gost.Prezime,
                    Email = gost.Email,
                    Broj_Telefona = gost.Broj_Telefona,
                    Rezervacije = rezervacije
                };
                gosti.Add(newGost);
            }
            return gosti;
        }



        public static List<Gost> VratiGosteZaAranzman(String sifra)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var kolekcija = db.GetCollection<Gost>("gosti");
            var kolekcijaRezervacije = db.GetCollection<Rezervacija>("rezervacije");


            List<Gost> gosti = new List<Gost>();
            Rezervacija rezervacija = kolekcijaRezervacije.Find(x => x.Sifra_Rezervacije == sifra).First();
            foreach (Gost g in kolekcija.Find(x => x.Rezervacije.Contains(rezervacija.Id)).ToList())
            {
                gosti.Add(g);
            }
            return gosti;
        }

        public static ObjectId KreirajGosta(Gost gost)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Gost>("gosti");
            collection.InsertOne(gost);
            return gost.Id;
        }

        public static void DodajRezervacijuGostu(String sifra, String jmbg)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var gostiColleciton = db.GetCollection<Gost>("gosti");
            var rezervacijaCollection = db.GetCollection<Rezervacija>("rezervacije");

            List<ObjectId> rezervacije = new List<ObjectId>();
            ObjectId gostId = new ObjectId();
            foreach (Gost g in gostiColleciton.Find(x => x.Jmbg == jmbg).ToList())
            {
                gostId = g.Id;
                rezervacije = g.Rezervacije;
                if (rezervacije == null)
                {
                    rezervacije = new List<ObjectId>();
                }
            }
            Rezervacija rezervacija = rezervacijaCollection.Find(x => x.Sifra_Rezervacije == sifra).First();
            rezervacije.Add(rezervacija.Id);

            var filter = Builders<Gost>.Filter.Eq(x => x.Jmbg, jmbg);
            var update = Builders<Gost>.Update.Set(x => x.Rezervacije, rezervacije);

            db.GetCollection<Gost>("gosti").UpdateOne(filter, update);

            var filter1 = Builders<Rezervacija>.Filter.Eq(x => x.Sifra_Rezervacije, sifra);
            var update1 = Builders<Rezervacija>.Update.Set(x => x.Gost, gostId);
            db.GetCollection<Rezervacija>("rezervacije").UpdateOne(filter1, update1);
        }

        public static void AzurirajGosta(string id, GostDTOUpdate gostDTOUpdate)
        {


            IMongoDatabase db = Session.MongoDatabase;

            IMongoCollection<Gost> gostCollection = db.GetCollection<Gost>("gosti");

            Gost gost = gostCollection.Find(g => g.Id == new ObjectId(id)).FirstOrDefault();


            if (gost != null)
            {
                gost.Ime = gostDTOUpdate.Ime;
                gost.Prezime = gostDTOUpdate.Prezime;
                gost.Email = gostDTOUpdate.Email;
                gost.Broj_Telefona = gostDTOUpdate.Broj_Telefona;

                gostCollection.ReplaceOne(x => x.Id == new ObjectId(id), gost);
            }
        }

        public static void ObrisiGosta(String jmbg)
        {
            IMongoDatabase db = Session.MongoDatabase;
            db.GetCollection<Gost>("gosti").DeleteOne(x => x.Jmbg == jmbg);
        }
        #endregion


    }
}
