using Contracts.Users.GetUser;
using MassTransit;
using Users.Api.Services;

namespace Users.Api.RabbitMQ.Consumers
{
    public class UpdateConsumer(IUsersService usersService) : IConsumer<GetUserRequest>
    {
        public async Task Consume(ConsumeContext<GetUserRequest> context)
        {
            var entityId = context.Message.UserId;
            var userId = (await usersService.GetByIdAsync(entityId))?.Id;
            await context.RespondAsync(new GetUserResponse { Id = userId });
        }
    }
}
