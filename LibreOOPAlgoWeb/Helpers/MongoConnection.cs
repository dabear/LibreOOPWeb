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
        private static readonly Lazy<MongoClient> lazyMongoClient =
            new Lazy<MongoClient>(() => new MongoClient(uri));

        public static MongoClient mongoClient => lazyMongoClient.Value;
        
        public static IMongoCollection<LibreReadingModel> GetReadingsCollection() {
            
            var db = mongoClient.GetDatabase("bjorninge_libreoopweb2");
            return db.GetCollection<LibreReadingModel>("librereadings");
        }

        public static IMongoCollection<LibreCalibrationModel> GetCalibrationCollection()
        {

            var db = mongoClient.GetDatabase("bjorninge_libreoopweb2");
            return db.GetCollection<LibreCalibrationModel>("librecalibrations");
        }

        public static IMongoCollection<PasswordTemp> GetTempCollection()
        {

            var db = mongoClient.GetDatabase("bjorninge_libreoopweb2");
            return db.GetCollection<PasswordTemp>("temp");
        }

        public static IMongoCollection<PingModel> GetPingCollection()
        {
            
            var db = mongoClient.GetDatabase("bjorninge_libreoopweb2");
            return db.GetCollection<PingModel>("libreping");
        }

        public async static Task AsyncInsertCalibrationReading(LibreCalibrationModel reading)
        {

            await GetCalibrationCollection().InsertOneAsync(reading);
        }

        public async static Task AsyncInsertReading(LibreReadingModel reading){

            await GetReadingsCollection().InsertOneAsync(reading);

        }

        public async static Task AsyncInsertTempPassword(string temp)
        {
            var m = new PasswordTemp { 
                CreatedOn = DateTime.Now, 
                HashedPassword = temp 
            };

            await GetTempCollection().InsertOneAsync(m);

        }

        public async static Task<int> countNonExpiredTempEntries(string temp) {

            var filter1 = Builders<PasswordTemp>.Filter.Eq("HashedPassword", temp);

            var dateInThePast = DateTime.Now.AddHours(Config.HoursInThePastForPasswordCheck);
            var filter2 = Builders<PasswordTemp>.Filter.Gte("CreatedOn", dateInThePast);

            var complexFilter = Builders<PasswordTemp>.Filter.And(new[] { filter1, filter2 });

            var options = new FindOptions<PasswordTemp, PasswordTemp> { Limit = 100 };



            var list = await GetTempCollection().FindAsync(complexFilter, options).Result.ToListAsync();

            return list.Count;


        }

        public async static Task<bool> DeleteExpiredTempEntries()
        {
            var collection = GetTempCollection();
            var dateInThePast = DateTime.Now.AddHours(Config.HoursInThePastForPasswordCheck);
            var filter = Builders<PasswordTemp>.Filter.Lte("CreatedOn", dateInThePast);

            await collection.DeleteManyAsync(filter);
            return true;
        }

        public async static Task<LibreCalibrationModel> GetCalibration(string uuid)
        {


            var collection = GetCalibrationCollection();

            var filter = Builders<LibreCalibrationModel>.Filter.Eq("uuid", uuid);


            return await collection.FindAsync(filter).Result.FirstAsync();
        }

        public async static Task<LibreReadingModel> GetRemoteReading(string uuid)
        {
 

            var collection = GetReadingsCollection();

            var filter = Builders<LibreReadingModel>.Filter.Eq("uuid", uuid);


            return await collection.FindAsync(filter).Result.FirstAsync();
        }

        public async static Task<bool> DeleteTestReadings(){
            var collection = GetReadingsCollection();
            var filter = Builders<LibreReadingModel>.Filter.Eq("b64contents", LibreTestData.TestDataReturns63);

            await collection.DeleteManyAsync(filter);
            return true;
        }

        public async static Task<List<LibreReadingModel>> GetPendingReadingsForProcessing(int limit)
        {

            var collection = GetReadingsCollection();

            var filter = Builders<LibreReadingModel>.Filter.Eq("status", "pending");

            
            var options = new FindOptions<LibreReadingModel, LibreReadingModel> { Limit = limit };
            
            var pending = await collection.FindAsync(filter, options).Result.ToListAsync();

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
                                                    .Set("status", reading.status)
                                                    .Set("ModifiedOn", reading.ModifiedOn)
                                                    .Set("newState", reading.newState ?? "n/a");



            var res = await collection.UpdateOneAsync(updateFilter, update);

            return res.ModifiedCount == 1;


        }



    }



}
