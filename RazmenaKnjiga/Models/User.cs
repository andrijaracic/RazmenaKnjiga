using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RazmenaKnjiga.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public List<string> Zanrovi { get; set; } = new List<string>();

    }
}
