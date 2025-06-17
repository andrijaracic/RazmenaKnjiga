using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace RazmenaKnjiga.Models
{
    public class Zahtevi
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string zahteviID { get; set; }

        [BsonElement("posiljalacZahtevaID")]
        public string posiljalacZahtevaID { get; set; }

        [BsonElement("knjigaID")]
        public string knjigaID { get; set; }

        [BsonElement("status")]
        public string Status { get; set; } = "Na cekanju"; 

    }
}
