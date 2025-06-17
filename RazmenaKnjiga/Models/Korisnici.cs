using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace RazmenaKnjiga.Models
{
    public class Korisnici
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string korisnikID { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("sifra")]
        public string sifra { get; set; }

        [BsonElement("omiljeniZanr")]
        public List<string> omiljeniZanr { get; set; } = new List<string>();
    }
}
