using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RazmenaKnjiga.Models
{
	public class Knjige
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string knjigeID { get; set; }

		[BsonElement("naslov")]
		public string naslov { get; set; }

		[BsonElement("autor")]
		public string autor { get; set; }

		[BsonElement("zanr")]
		public string zanr { get; set; }

		[BsonElement("opis")]
		public string opis { get; set; }

		[BsonElement("korisnikID")]
		public string korisnikID { get; set; } // ID korisnika koji je postavio knjigu
	}
}
