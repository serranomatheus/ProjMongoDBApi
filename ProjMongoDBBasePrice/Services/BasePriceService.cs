using System.Collections.Generic;
using Models;
using MongoDB.Driver;
using ProjMongoDBApi.Utils;

namespace ProjMongoDBApi.Services
{
    public class BasePriceService

    {
        private readonly IMongoCollection<BasePrice> _basePrice;        

        public BasePriceService(IProjMongoDBApiSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _basePrice = database.GetCollection<BasePrice>(settings.BasePriceCollectionName);
        }
        public BasePrice GetBasePrice(string origin, string destination) =>
           _basePrice.Find<BasePrice>(basePrice => basePrice.Origin.CodeIata == origin && basePrice.Destination.CodeIata == destination).FirstOrDefault();
        public List<BasePrice> Get() =>
            _basePrice.Find(basePrice => true).ToList();

        public BasePrice Get(string id) =>
            _basePrice.Find<BasePrice>(basePrice => basePrice.Id == id).FirstOrDefault();

        public BasePrice Create(BasePrice basePrice)
        {
            _basePrice.InsertOne(basePrice);
            return basePrice;
        }

        public void Update(string id, BasePrice basePriceIn) =>
            _basePrice.ReplaceOne(basePrice => basePrice.Id == id, basePriceIn);

        public void Remove(BasePrice basePriceIn) =>
            _basePrice.DeleteOne(basePrice => basePrice.Id == basePriceIn.Id);

        public void Remove(string id) =>
            _basePrice.DeleteOne(basePrice => basePrice.Id == id);
    }
}
