using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LibreOOPWeb.Models;
using LibreOOPWeb.Utils;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core;


namespace LibreOOPWeb.Helpers
{
    public class MongoConnection
    {
        public static string uri = Config.MongoUrl;
        public static IMongoCollection<LibreReadingModel> GetReadingsCollection() {
            var client = new MongoClient(uri);
            var db = client.GetDatabase("bjorninge_libreoopweb");
            return db.GetCollection<LibreReadingModel>("librereadings");
        }

        public static IMongoCollection<PingModel> GetPingCollection()
        {
            var client = new MongoClient(uri);
            var db = client.GetDatabase("bjorninge_libreoopweb");
            return db.GetCollection<PingModel>("libreping");
        }

        public async static Task AsyncInsertReading(LibreReadingModel reading){

            await GetReadingsCollection().InsertOneAsync(reading);
        }

        public async static Task<LibreReadingModel> GetRemoteReading(string uuid)
        {
 

            var collection = GetReadingsCollection();

            var filter = Builders<LibreReadingModel>.Filter.Eq("uuid", uuid);


            return await collection.FindAsync(filter).Result.FirstAsync();
        }

        public async static Task<List<LibreReadingModel>> GetPendingReadingsForProcessing()
        {

            var collection = GetReadingsCollection();

            var filter = Builders<LibreReadingModel>.Filter.Eq("status", "pending");

            var pending = await collection.FindAsync(filter).Result.ToListAsync();

            // Removes pending entries.
            // Next call to GetPendingReadingsForProcessing will not
            // include these entries
            //var update = Builders<LibreReadingModel>.Update.Set("status", "processing");
            //await collection.UpdateManyAsync(filter, update);

            return pending;
        }

        public async static Task<DateTime?> GetLatestPing(){
            var collection = GetPingCollection();
            var filter = Builders<PingModel>.Filter.Eq("Desc", "lastfetch");

            try
            {
                return (await collection.FindAsync(filter).Result.FirstAsync()).ModifiedOn;
            }
            catch (InvalidOperationException) {
            }

            return null;

        }

        public async static Task<bool> UpdatePingCollection()
        {
            var collection = GetPingCollection();
            var now = DateTime.Now;
            var updateFilter = Builders<PingModel>.Filter.Eq("Desc", "lastfetch");
            var update = Builders<PingModel>.Update
                                            .Set("ModifiedOn", now);
            
            var res = await collection.UpdateOneAsync(updateFilter, update);

            //insert new reading if there was no reading to update
            if(res.ModifiedCount == 0)
            {
                await collection.InsertOneAsync(new PingModel {
                    Desc = "lastfetch",
                    ModifiedOn = now
                });
            }

            return false;
        }

        public async static Task<bool> AsyncUpdateReading(LibreReadingModel reading)
        {
           

            var collection = GetReadingsCollection();

            var updateFilter = Builders<LibreReadingModel>.Filter.Eq("uuid", reading.uuid);
            var update = Builders<LibreReadingModel>.Update.Set("result", reading.result)
                                                      .Set("status", reading.status).Set("ModifiedOn", reading.ModifiedOn);



            var res = await collection.UpdateOneAsync(updateFilter, update);

            return res.ModifiedCount == 1;


        }



    }



}
