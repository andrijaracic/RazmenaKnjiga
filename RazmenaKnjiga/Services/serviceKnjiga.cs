using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using RazmenaKnjiga.Models; 

public class serviceKnjiga
{
	private readonly IMongoCollection<Knjige> _knjige;

	public serviceKnjiga(IConfiguration podesavanja)
	{
		var klijent = new MongoClient(podesavanja["MongoDB:ConnectionString"]);
		var baza = klijent.GetDatabase(podesavanja["MongoDB:DatabaseName"]);
		_knjige = baza.GetCollection<Knjige>("Knjige"); 
	}

	public async Task<List<Knjige>> VratiKnjigeKorisnikaAsync(string korisnikId) =>
		await _knjige.Find(k => k.korisnikID == korisnikId).ToListAsync();

	public async Task<Knjige> VratiPoIdAsync(string id) =>
		await _knjige.Find(k => k.knjigeID == id).FirstOrDefaultAsync();

	public async Task AzurirajAsync(string id, Knjige knjiga) =>
		await _knjige.ReplaceOneAsync(k => k.knjigeID == id, knjiga);

	public async Task ObrisiAsync(string id) =>
		await _knjige.DeleteOneAsync(k => k.knjigeID == id);
}
