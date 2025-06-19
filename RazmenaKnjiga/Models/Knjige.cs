using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;  

namespace RazmenaKnjiga.Models
{
    public class Knjiga
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Naslov")]
        [Required(ErrorMessage = "Naslov je obavezan")]
        [StringLength(100, ErrorMessage = "Naslov ne sme biti duži od 100 karaktera")]
        public string Naslov { get; set; }

        [BsonElement("Autor")]
        [Required(ErrorMessage = "Autor je obavezan")]
        [StringLength(100, ErrorMessage = "Autor ne sme biti duži od 100 karaktera")]
        public string Autor { get; set; }

        [StringLength(50)]
        public string Zanr { get; set; }

        public string Opis { get; set; }

        [StringLength(50)]
        public string Stanje { get; set; }  

        [Required(ErrorMessage = "Grad je obavezan")]
        [StringLength(50)]
        public string Grad { get; set; }

        public string SlikaBase64 { get; set; }  

        public DateTime DatumDodavanja { get; set; } = DateTime.UtcNow;

        public string VlasnikId { get; set; }  
    }
}
