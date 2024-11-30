using MongoDB.Driver;
using Users.Api.Entities;

namespace Users.Api.Services
{
    public interface IUsersService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(string id);
        Task CreateAsync(User user);
        Task UpdateAsync(string id, User updatedUser);
        Task DeleteAsync(string id);
    }

    public class UsersService : IUsersService
    {
        private readonly IMongoCollection<User> _usersCollection;

        public UsersService(IMongoDatabase database)
        {
            _usersCollection = database.GetCollection<User>("users");
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _usersCollection.Find(_ => true).ToListAsync();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _usersCollection.Find(user => user.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(User user)
        {
            await _usersCollection.InsertOneAsync(user);
        }

        public async Task UpdateAsync(string id, User updatedUser)
        {
            await _usersCollection.ReplaceOneAsync(user => user.Id == id, updatedUser);
        }

        public async Task DeleteAsync(string id)
        {
            await _usersCollection.DeleteOneAsync(user => user.Id == id);
        }
    }
}