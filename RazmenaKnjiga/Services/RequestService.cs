using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using RazmenaKnjiga.Models; 

public class RequestService
{
    private readonly IMongoCollection<Zahtevi> _zahtevi;

    public RequestService(IConfiguration podesavanja)
    {
        var klijent = new MongoClient(podesavanja["MongoDB:ConnectionString"]);
        var baza = klijent.GetDatabase(podesavanja["MongoDB:DatabaseName"]);
        _zahtevi = baza.GetCollection<Zahtevi>("Zahtevi");
    }

    
    public async Task KreirajAsync(Zahtevi zahtev) =>
        await _zahtevi.InsertOneAsync(zahtev);

  
    public async Task<List<Zahtevi>> VratiZahtevePosiljaocaAsync(string posiljalacId) =>
        await _zahtevi.Find(z => z.posiljalacZahtevaID == posiljalacId).ToListAsync();

   
    public async Task<List<Zahtevi>> VratiZahteveZaKnjiguAsync(string knjigaId) =>
        await _zahtevi.Find(z => z.knjigaID == knjigaId).ToListAsync();

    
    public async Task PromeniStatusAsync(string zahtevId, string noviStatus) =>
        await _zahtevi.UpdateOneAsync(
            z => z.zahteviID == zahtevId,
            Builders<Zahtevi>.Update.Set(z => z.Status, noviStatus)
        );
}