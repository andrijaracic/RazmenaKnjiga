using RazmenaKnjiga.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;

namespace RazmenaKnjiga.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Knjiga> _knjige;

        public BookService(IConfiguration config)
        {
            var client = new MongoClient(config["MongoDB:ConnectionString"]);
            var database = client.GetDatabase(config["MongoDB:DatabaseName"]);
            _knjige = database.GetCollection<Knjiga>("Knjige");
        }

        public List<Knjiga> Get() =>
            _knjige.Find(knjiga => true).ToList();

        public Knjiga Get(string id) =>
            _knjige.Find(knjiga => knjiga.Id == id).FirstOrDefault();

        public Knjiga Create(Knjiga knjiga)
        {
            _knjige.InsertOne(knjiga);
            return knjiga;
        }

        public void Update(string id, Knjiga knjigaIn) =>
            _knjige.ReplaceOne(knjiga => knjiga.Id == id, knjigaIn);

        public void Remove(string id) =>
            _knjige.DeleteOne(knjiga => knjiga.Id == id);
    }
}
