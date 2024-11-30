using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace Users.Api.Entities
{
    public class User
    {
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
    }
}
