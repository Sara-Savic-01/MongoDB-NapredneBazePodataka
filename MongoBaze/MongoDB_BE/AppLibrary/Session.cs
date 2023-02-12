using System;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLibrary
{
    public static class Session
    {
        private static IMongoDatabase mongoDatabase;

        public static IMongoDatabase MongoDatabase
        {
            get
            {
                if (Session.mongoDatabase == null)
                {
                    var settings = MongoClientSettings.FromConnectionString("mongodb+srv://korisnik:sifra123@cluster0.v64qykx.mongodb.net/hotelBaza?retryWrites=true&w=majority");
                    var client = new MongoClient(settings);
                    Session.mongoDatabase = client.GetDatabase("hotelBaza");
                    return Session.mongoDatabase;

                }
                return Session.mongoDatabase;
            }
        }
    }
}
