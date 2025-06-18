using MongoDB.Driver;
using RazmenaKnjiga.Models;

namespace RazmenaKnjiga.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IConfiguration config)
        {
            var connectionString = config["MongoDB:ConnectionString"];
            var databaseName = config["MongoDB:DatabaseName"];

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);

            _users = database.GetCollection<User>("Korisnici");
        }

        public async Task<User> RegisterUserAsync(RegisterDto dto)
        {
            var noviKorisnik = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                Password = dto.Password,
                Zanrovi = dto.Zanrovi
            };

            await _users.InsertOneAsync(noviKorisnik);
            return noviKorisnik;
        }
    }
}
