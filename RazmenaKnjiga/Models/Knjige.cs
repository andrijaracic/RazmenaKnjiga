using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        [BindNever]
        public string? Id { get; set; }

        [BsonElement("Naslov")]
        [Required(ErrorMessage = "Naslov je obavezan")]
        [StringLength(100)]
        public string Naslov { get; set; }

        [BsonElement("Autor")]
        [Required(ErrorMessage = "Autor je obavezan")]
        [StringLength(100)]
        public string Autor { get; set; }

        [StringLength(50)]
        public string Zanr { get; set; }

        public string Opis { get; set; }

        [StringLength(50)]
        public string Stanje { get; set; }

        [Required(ErrorMessage = "Grad je obavezan")]
        [StringLength(50)]
        public string Grad { get; set; }

        public string PutanjaSlike { get; set; }

        public DateTime DatumDodavanja { get; set; } = DateTime.UtcNow;

        public string VlasnikId { get; set; }
    }
}
