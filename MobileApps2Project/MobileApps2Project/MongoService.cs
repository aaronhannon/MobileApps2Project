using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace MobileApps2Project
{
    class MongoService
    {
        public List<Checkout> checkouts;
        public string dbName = "mongo_database";
        public string collectionName = "checkout";
        public IMongoCollection<Checkout> mongoCollection;
        public IMongoCollection<Checkout> MongoCollection
        {
            get
            {
                if(mongoCollection == null)
                {
                    MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl("mongodb://mobile:admin12@ds137863.mlab.com:37863/mongo_database"));

                    settings.SslSettings = new SslSettings { EnabledSslProtocols = SslProtocols.Tls12};

                    var mongoClient = new MongoClient(settings);
                    var db = mongoClient.GetDatabase(dbName);

                    var collectionSettings = new MongoCollectionSettings { ReadPreference = ReadPreference.Nearest };
                    mongoCollection = db.GetCollection<Checkout>(collectionName, collectionSettings);

                    
                }

                return mongoCollection;
            }

        }

        public List<Checkout> GetAllData()
        {
            checkouts = new List<Checkout>();
            var allData = MongoCollection.Find(new BsonDocument()).ToList();

            foreach (var x in allData)
            {
                foreach (var c in x.checkouts)
                {
                    checkouts.Add(new Checkout(c.score, c.checkoutString));
                    Debug.WriteLine("MONGOSERVICE: " + c.score +": "+ c.checkoutString);
                }   
            }

            return checkouts;
        }

    }
}
