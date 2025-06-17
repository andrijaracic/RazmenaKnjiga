using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace RazmenaKnjiga.Models
{
    public class Poruke
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string porukeID { get; set; }

        [BsonElement("posiljalacID")]
        public string posiljalacID { get; set; }

        [BsonElement("primalacID")]
        public string primalacID { get; set; }

        [BsonElement("sadrzaj")]
        public string sadrzaj { get; set; }

        [BsonElement("vremeSlanja")]
        public DateTime vremeSlanja { get; set; } = DateTime.UtcNow;

        [BsonElement("knjigeID")]
        public string knjigeID { get; set; } 
    }
}

