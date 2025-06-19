using RazmenaKnjiga.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace RazmenaKnjiga.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Knjiga> _knjige;

        public BookService(IMongoClient mongoClient, IConfiguration config)
        {
            var database = mongoClient.GetDatabase(config["MongoDB:DatabaseName"]);
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
