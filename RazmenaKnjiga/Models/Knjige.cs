using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RazmenaKnjiga.Models
{
    public class Knjiga
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Naslov")]
        public string Naslov { get; set; }

        [BsonElement("Autor")]
        public string Autor { get; set; }

        public string Zanr { get; set; }

        public string Opis { get; set; }

        public string Stanje { get; set; }  // npr. Novo, Polovno, Oštećeno

        public string Grad { get; set; }

        public string SlikaBase64 { get; set; }  // Slika kao Base64 string

        public DateTime DatumDodavanja { get; set; } = DateTime.UtcNow;

        public string VlasnikId { get; set; }  // ID korisnika koji je postavio knjigu
    }
}
