﻿using MobileApps2Project.Classes;
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
        public List<GameStats> stats;
        public string dbName = "mongo_database";
        public string collectionName = "checkout";
        public string collectionStats = "stats";
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

        public IMongoCollection<GameStats> statsCollection;
        public IMongoCollection<GameStats> StatsCollection
        {
            get
            {
                if (mongoCollection == null)
                {
                    MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl("mongodb://mobile:admin12@ds137863.mlab.com:37863/mongo_database"));

                    settings.SslSettings = new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 };

                    var mongoClient = new MongoClient(settings);
                    var db = mongoClient.GetDatabase(dbName);

                    var collectionSettings = new MongoCollectionSettings { ReadPreference = ReadPreference.Nearest };
                    statsCollection = db.GetCollection<GameStats>(collectionStats, collectionSettings);

                }

                return statsCollection;
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

        public async void saveStatsData(GameStats g)
        {
           await StatsCollection.InsertOneAsync(g);
            
            
        }

        public List<GameStats> GetAllStats()
        {
            stats = new List<GameStats>();
            var allData = StatsCollection.Find(new BsonDocument()).ToList();

            foreach (var x in allData)
            {
                stats.Add(new GameStats(x.player1Name, x.player2Name, x.player1Sets, x.player2Sets,x.winner));
                Debug.WriteLine(x.player1Name + " " + x.player2Name + " " + x.player1Sets + " " + x.player2Sets + " " + x.winner);
            }

            return stats;
        }


    }
}
