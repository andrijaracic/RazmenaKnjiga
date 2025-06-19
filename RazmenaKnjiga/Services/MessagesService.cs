using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using RazmenaKnjiga.Models;  

public class MessagesService
{
	private readonly IMongoCollection<Poruke> _poruke;

	public MessagesService(IConfiguration podesavanja)
	{
		var klijent = new MongoClient(podesavanja["MongoDB:ConnectionString"]);
		var baza = klijent.GetDatabase(podesavanja["MongoDB:DatabaseName"]);
		_poruke = baza.GetCollection<Poruke>("Poruke");
	}

	public async Task PosaljiAsync(Poruke poruka) =>
		await _poruke.InsertOneAsync(poruka);

	public async Task<List<Poruke>> VratiPrepiskeAsync(string korisnik1, string korisnik2) =>
		await _poruke.Find(p =>
			(p.posiljalacID == korisnik1 && p.primalacID == korisnik2) ||
			(p.posiljalacID == korisnik2 && p.primalacID == korisnik1))
			.SortBy(p => p.vremeSlanja)
			.ToListAsync();
}
