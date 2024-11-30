using Contracts.Users.GetUser;
using MassTransit;

namespace TimeTracker.Api.Application.RabbitMq;

public class UserService(IRequestClient<GetUserRequest> requestClient)
{
    public async Task<GetUserResponse?> GetUserAsync(string id)
    {
        var request = new GetUserRequest { UserId = id };
        var response = await requestClient.GetResponse<GetUserResponse>(request);
        return response.Message;
    }
}